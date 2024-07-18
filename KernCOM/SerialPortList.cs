using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Runtime.InteropServices;


namespace KernDriver
{
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface _SerialPortList
    {
        String[] GetPorts();
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("KernCOM.SerialPortList")]
    public class SerialPortList: _SerialPortList
    {
        public String[] GetPorts()
        {
            return SerialPort.GetPortNames();
        }
    }
}
