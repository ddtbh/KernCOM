using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KernDriver;

namespace KernTest
{
    class Program
    {
        public void CBF_Opened(KernDriver.SerialDevice sender)
        {
            Console.WriteLine("CBF_OPENED");
        }

        public void CBF_Closing(KernDriver.SerialDevice sender)
        {
            Console.WriteLine("CBF_CLOSING");
        }

        static void Main(string[] args)
        {
            /* Test events */
            if (Math.Max(0, 1) == 0)
            {
                Program p = new Program();
                
                KernDriver.SerialDevice sd = new KernDriver.SerialDevice();
                SD_reCr sd_recr = new SD_reCr();
                sd.StreamDecoder = sd_recr;
                sd.COMEventPortOpened.Event += p.CBF_Opened;
                sd.COMEventPortClosing.Event += p.CBF_Closing;
                sd.Open("COM2", 9600);
                //sd.Close();
                sd_recr.QueryInterval = 500;
                Console.ReadLine();

                sd_recr.QueryInterval = 1000;
                Console.ReadLine();

                sd_recr.QueryInterval = 0;
                Console.ReadLine();


                Console.ReadLine();
            }

            /* Test parser */
            if (Math.Max(0, 1) == 1)
            {
                Console.WriteLine(KernDriver.UniParser.Parse("       0.445 kg ", ""));
                Console.ReadLine();
            }
        }
    }
}
