using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace KernDriver
{
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface _SD_reCr
    {
        int QueryInterval { get; set; }
        String DefaultUnits { get; set; }
        void Query();
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("KernCOM.SD_reCr")]
    public class SD_reCr: _SD_reCr, StreamDecoder
    {
        private int queryinterval = 0;
        private String units_default = "kg";
        private SerialDevice sd = null;
        private Thread t_qi=null;

        public SD_reCr()
        {
        }

        public int QueryInterval
        {
            get
            {
                return queryinterval;
            }
            set
            {
                /* Check for zero or invalid query interval and query thread is active */
                if ((value < 1) &&
                    (QueryThreadAlive()))
                    QueryThreadStop();

                /* Assign value */
                queryinterval = value;

                /* Check for case serial device assigned, query thread not alive, port is open and query interval is valid */
                if ((sd != null) && 
                    (! QueryThreadAlive()) &&
                    (sd.IsOpen()) &&
                    (queryinterval > 0))
                    QueryThreadStart();
            }
        }

        public String DefaultUnits
        {
            get
            {
                return units_default;
            }
            set
            {
                units_default = value;
            }
        }

        public Response Decode(String data)
        {
            return UniParser.Parse(data, units_default);
        }

        public void Associate(SerialDevice sd)
        {
            /* Associate events */
            sd.COMEventPortOpened.Event += CE_PortOpened;
            sd.COMEventPortClosing.Event += CE_PortClosing;

            /* Save serial device reference */
            this.sd = sd;
        }

        public void Deassociate(SerialDevice sd)
        {
            /* Deasssociate events */
            sd.COMEventPortOpened.Event -= CE_PortOpened;
            sd.COMEventPortClosing.Event -= CE_PortClosing;

            /* Clear serial device reference */
            this.sd = null;
        }

        public void CE_PortOpened(SerialDevice sd)
        {
            Console.WriteLine("CE_PORTOPENED");
            if (queryinterval > 0)
                QueryThreadStart();
        }

        public void CE_PortClosing(SerialDevice sd)
        {
            if (QueryThreadAlive())
                QueryThreadStop();
        }

        public void Query()
        {
            if (sd == null)
                throw new StreamDecoderException("Not associated.");
            sd.Send("w");
        }

        private void QueryThreadStart()
        {
            /* Check for query interval */
            if (queryinterval < 1)
                throw new StreamDecoderException("Invalid query interval.");

            /* Check for query thread already alive */
            if (QueryThreadAlive())
                throw new StreamDecoderException("Query thread already started.");

            /* Create thread */
            t_qi = new Thread(QueryThreadEP);
            t_qi.IsBackground = true;
            t_qi.Start();
        }

        private void QueryThreadStop()
        {
            if (!t_qi.IsAlive)
                throw new StreamDecoderException("Query thread not alive.");
            t_qi.Interrupt();
            t_qi.Join();
        }

        private bool QueryThreadAlive()
        {
            return (t_qi != null) && (t_qi.IsAlive);
        }

        protected void QueryThreadEP()
        {
            try
            {

                while (true)
                {
                    Query();
                    Thread.Sleep(queryinterval);
                }
            }
            catch (ThreadInterruptedException)
            {
                return;
            }
        }
    }
}
