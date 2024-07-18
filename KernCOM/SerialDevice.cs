using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Runtime.InteropServices;

namespace KernDriver
{
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface _SerialDeviceException
    {
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("KernCOM.SerialDeviceException")]
    public class SerialDeviceException : Exception, _SerialDeviceException
    {
        public SerialDeviceException(String msg): base(msg)
        {
        }
    }


    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface _SerialDevice
    {
        int IOThreadLockTime { get; set; }
        COMEvent COMEventDataAvailable { get; }
        StreamDecoder StreamDecoder { get; set; }
        Response LastResponseStruct { get; }
        String LastResponseString { get; }
        double LastResponseWeight { get; }
        ResponseType LastResponseType { get; }
        Boolean LastResponseTypeStable { get; }
        Boolean LastResponseTypeUnstable { get; }
        Boolean LastResponseTypeError { get; }
        Object PortAccessLockObject { get; }
        Boolean PortOpen { get; }

        void Open(String pn, Int32 baudrate);
        void Close();
        void Send(String str);
        bool IsOpen();
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("KernCOM.SerialDevice")]
    public class SerialDevice: _SerialDevice
    {
        private volatile SerialPort sp=new SerialPort();
        private Thread t_streamreader=null;
        private int streamreader_iowait = 500;
        private volatile Object lock_term_streamreader = new Object();
        private volatile Object lock_port_access = new Object();
        private volatile bool flag_term_streamreader;
        private COMEvent ce_port_opened;
        private COMEvent ce_data_available;
        private COMEvent ce_port_closing;
        private StreamDecoder sd=null;
        private Response response_last = Response.Error;
        private String string_last = String.Empty;

        public SerialDevice(StreamDecoder sd)
        {
            ce_port_opened = new COMEvent(this);
            ce_data_available = new COMEvent(this);
            ce_port_closing = new COMEvent(this);
            this.sd = sd;
        }

        public SerialDevice()
            :this(null)
        {
        }

        public COMEvent COMEventPortOpened
        {
            get
            {
                return ce_port_opened;
            }
        }

        public COMEvent COMEventDataAvailable
        {
            get
            {
                return ce_data_available;
            }
        }

        public COMEvent COMEventPortClosing
        {
            get
            {
                return ce_port_closing;
            }
        }

        public int IOThreadLockTime
        {
            get
            {
                return streamreader_iowait;
            }
            set
            {
                if (value < 1)
                    throw new SerialDeviceException("IOWait must be greather equal 1.");
                streamreader_iowait = value;
            }
        }

        public StreamDecoder StreamDecoder
        {
            get
            {
                return sd;
            }
            set
            {
                if (sd != null)
                    sd.Deassociate(this);
                sd = value;
                if (sd != null)
                    sd.Associate(this);
            }
        }

        public Response LastResponseStruct
        {
            get
            {
                return response_last;
            }
        }

        public String LastResponseString
        {
            get
            {
                return string_last;
            }
        }

        public double LastResponseWeight
        {
            get
            {
                return response_last.Weight;
            }
        }

        public ResponseType LastResponseType
        {
            get
            {
                return response_last.Type;
            }
        }

        public Boolean LastResponseTypeStable
        {
            get
            {
                return response_last.IsStable();
            }
        }

        public Boolean LastResponseTypeUnstable
        {
            get
            {
                return response_last.IsUnstable();
            }
        }

        public Boolean LastResponseTypeError
        {
            get
            {
                return response_last.IsError();
            }
        }
        
        private bool StreamReaderThreaderTermination
        {
            get
            {
                lock (lock_term_streamreader)
                {
                    return flag_term_streamreader;
                }
            }
            set
            {
                lock (lock_term_streamreader)
                {
                    flag_term_streamreader = value;
                }
            }
        }

        public Object PortAccessLockObject
        {
            get
            {
                return lock_port_access;
            }
        }

        public Boolean PortOpen
        {
            get
            {
                return IsOpen();
            }
        }

        public void Open(String pn, Int32 baudrate)
        {
            /* Check open */
            if (sp.IsOpen)
                throw new SerialDeviceException("Port already open.");

            /* Set connection properties */
            sp.PortName = pn;
            sp.BaudRate = baudrate;

            /* Open */
            lock (lock_port_access)
            {
                sp.Open();
            }

            /* Set rest of properties */
            sp.DtrEnable = false;
            sp.RtsEnable = false;
            sp.BreakState = false;
            sp.Parity = Parity.None;
            sp.DataBits = 8;

            /* Set read timeout */
            sp.ReadTimeout = streamreader_iowait;

            /* Reset stream reader termination flag */
            flag_term_streamreader=false;

            /* Create stream reader thread */
            t_streamreader=new Thread(StreamReaderThreadEP);
            t_streamreader.IsBackground = true;
            t_streamreader.Start();

            /* Notify */
            ce_port_opened.Notify();
        }

        public void Close()
        {
            /* Check port is open */
            if (!IsOpen())
                throw new SerialDeviceException("Port is not open.");

            /* Notify */
            ce_port_closing.Notify();

            /* Shtudown stream reader thread */
            if (t_streamreader is Thread)
            {
                /* Set thread termination flag */
                StreamReaderThreaderTermination = true;

                /* Check thread status and join it if it is running */
                if ((t_streamreader.ThreadState == ThreadState.Running) ||
                    (t_streamreader.ThreadState == ThreadState.WaitSleepJoin))
                    t_streamreader.Join();

                /* Set stream reader thread object holder to null */
                t_streamreader = null;
            }

            /* Close port */
            lock (lock_port_access)
            {
                sp.Close();
            }
        }

        protected void StreamReaderThreadEP()
        {
            StreamReader sr = new StreamReader(sp.BaseStream, Encoding.GetEncoding("ISO-8859-1"));

            while(true)
            {
                /* Read port */
                string_last=String.Empty;
                try
                {
                    string_last = sr.ReadLine();
                }
                catch (TimeoutException)
                {
                    /* Check termination flag */
                    if (StreamReaderThreaderTermination)
                        return;
                    continue;
                }
                catch (IOException)
                {
                    /* Check termination flag */
                    if (StreamReaderThreaderTermination)
                        return;
                    continue;
                }

                /* Check termination flag */
                if (StreamReaderThreaderTermination)
                    return;

                /* Decode stream if required */
                if (sd == null)
                    response_last = Response.Error;
                else try
                {
                        response_last = sd.Decode(string_last);
                }
                catch (Exception)
                {
                    response_last = Response.Error;
                }

                /* Invoke COM event */
                ce_data_available.Notify();
            }
        }

        public void Send(String str)
        {
            lock (lock_port_access)
            {
                if (!sp.IsOpen)
                    throw new SerialDeviceException("Port is not open.");
                    sp.WriteLine(str);
            }
        }

        public bool IsOpen()
        {
            lock (lock_port_access)
            {
                return sp.IsOpen;
            }
        }
    }
}
