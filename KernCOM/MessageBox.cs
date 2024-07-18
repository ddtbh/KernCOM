using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace KernDriver
{
    static class MessageBox
    {
        [DllImport("user32.dll", EntryPoint = "MessageBox")]
        public static extern int ShowMessage(int hWnd, string text, string caption, uint type);
    }
}
