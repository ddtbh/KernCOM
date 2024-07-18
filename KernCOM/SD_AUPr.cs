using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace KernDriver
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("KernCOM.SD_AUPr")]
    public class SD_AUPr: StreamDecoder
    {
        public SD_AUPr()
        {
        }

        public Response Decode(String data)
        {
            return UniParser.Parse(data);
        }

        public void Associate(SerialDevice sd)
        {
        }

        public void Deassociate(SerialDevice sd)
        {
        }
    }
}
