using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;

namespace KernDriver
{
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface _COMEvent
    {
        void SetCallback([MarshalAs(UnmanagedType.FunctionPtr)] CallBackFunction callback);
        void ResetCallback();
        void SetCallbackByObjectAndEventName(object cb_obj, string cb_eventname);
        void ResetCallbackByObjectAndEventName();
        void Notify();
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void CallBackFunction(SerialDevice sender);

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("KernCOM.COMEvent")]
    public class COMEvent: _COMEvent
    {
        /* Serial device reference */
        private SerialDevice sender;
        
        /* Delegate reference */
        private Delegate cb = null;

        /* COM callback references and properties */
        private string cb_eventname = "";
        private object int_cb_obj = null;
        private object cb_obj
        {
            get
            {
                return int_cb_obj;
            }
            set
            {
                try
                {
                    Marshal.FinalReleaseComObject(int_cb_obj);
                } catch (Exception)
                {
                }
                int_cb_obj = value;
            }
        }

        /* Event callback reference */
        public event CallBackFunction Event=null;

        public COMEvent(SerialDevice sender)
        {
            this.sender = sender;
        }

        public void SetCallback([MarshalAs(UnmanagedType.FunctionPtr)] CallBackFunction callback)
        {
            this.cb = callback;
        }

        public void ResetCallback()
        {
            cb = null;
        }

        public void SetCallbackByObjectAndEventName(object cb_obj, string cb_eventname)
        {
            this.cb_obj = cb_obj;
            this.cb_eventname = cb_eventname;
        }

        public void ResetCallbackByObjectAndEventName()
        {
            cb_obj = null;
            cb_eventname = null;
        }

        public void Notify()
        {
            if (cb != null)
                cb.DynamicInvoke(new object[1] { sender });

            if ((cb_obj != null) && (cb_eventname.Length > 0))
                this.cb_obj.GetType().InvokeMember(this.cb_eventname, BindingFlags.InvokeMethod, null, cb_obj, new object[1] { sender });

            if (Event != null)
                Event(sender);
        }

        ~COMEvent()
        {
            this.cb_obj = null;
        }
    }
}
