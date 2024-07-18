using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace KernDriver
{
    public enum ResponseType
    {
        Error,
        Stable,
        Unstable
    }

    public struct Response
    {
        public ResponseType Type;
        public double Weight;

        public Response(ResponseType rt, double weight)
        {
            Type = rt;
            Weight = weight;
        }

        public static Response Error = new Response(ResponseType.Error, 0);

        public bool IsStable()
        {
            return Type == ResponseType.Stable;
        }

        public bool IsUnstable()
        {
            return Type == ResponseType.Unstable;
        }

        public bool IsError()
        {
            return ((this.GetHashCode() == ResponseType.Error.GetHashCode()) ||
                (Type == ResponseType.Error));
        }

        public override string ToString()
        {
            string res = "Type=";
            switch (Type)
            {
                case ResponseType.Error: res += "ERROR"; break;
                case ResponseType.Stable: res += "STABLE"; break;
                case ResponseType.Unstable: res += "UNSTABLE"; break;
            }
            return res + ", Weight=" + Weight.ToString();
        }
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface _StreamDecoderException
    {
    }

    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("KernCOM.SerialDeviceException")]
    public class StreamDecoderException : Exception, _StreamDecoderException
    {
        public StreamDecoderException(String msg)
            : base(msg)
        {
        }
    }

    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface StreamDecoder
    {
        Response Decode(String data);
        void Associate(SerialDevice sd);
        void Deassociate(SerialDevice sd);
    }
}
