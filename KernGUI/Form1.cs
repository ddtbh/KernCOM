using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Security.Principal;
using Microsoft.Win32;
using KernDriver;

namespace KernGUI
{
    public partial class Form_KernGUI : Form
    {
        SerialDevice sd=new SerialDevice(new SD_AUPr());
        static RegistryKey rk=Registry.CurrentUser.CreateSubKey("Software\\MYKYS\\KernGUI");

        public Form_KernGUI()
        {
            InitializeComponent();
            
            /* Attach data received event */
            sd.COMEventDataAvailable.Event += EventDataReceived;

            /* Rescan ports */
            BT_Port_Rescan_Click(null, null);

            /* Set platform */
            TB_Platform.Text = GetPlatformString();
            TB_COMPath.Text = GetCOMName();
        }

        private String persistentConfigGet(String name, String str_default="")
        {
            try
            {
                if (rk.GetValueKind(name) == RegistryValueKind.String)
                    return (String)rk.GetValue(name);
            }
            catch (Exception)
            {
            }
            return str_default;
        }

        private void persistentConfigSet(String name, String value)
        {
            try
            {
                rk.SetValue(name, value, RegistryValueKind.String);
            }
            catch (Exception)
            {
            }
        }

        private void ShowException(Exception e)
        {
            MessageBox.Show(e.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowInformation(String s)
        {
            MessageBox.Show(s, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BT_Port_Rescan_Click(object sender, EventArgs e)
        {
            /* Clear items */
            CB_Port.Items.Clear();

            /* Obtain serial port list */
            foreach (String s in new SerialPortList().GetPorts())
                CB_Port.Items.Add(s);

            /* Select 1st item if any exist */
            if (CB_Port.Items.Count > 0)
                CB_Port.SelectedIndex = 0;

            /* Read config values */
            {
                String port=persistentConfigGet("Port");
                if ((port.Length > 0) &&
                    (CB_Port.Items.IndexOf(port) >= 0))
                    CB_Port.SelectedIndex = CB_Port.Items.IndexOf(port);

                String speed = persistentConfigGet("Speed");
                if ((speed.Length > 0) &&
                    (CB_Speed.Items.IndexOf(speed) >= 0))
                    CB_Speed.SelectedIndex = CB_Speed.Items.IndexOf(speed);

                String sd = persistentConfigGet("SD");
                if ((sd.Length > 0) &&
                    (CB_SD.Items.IndexOf(sd) >= 0))
                    CB_SD.SelectedIndex = CB_SD.Items.IndexOf(sd);
                if ((CB_SD.Items.Count > 0) &&
                    (CB_SD.SelectedIndex < 0))
                    CB_SD.SelectedIndex = 0;
            }
        }

        private void Timer_Port_Tick(object sender, EventArgs e)
        {
            /* Obtain states */
            bool flag_isopen = sd.IsOpen(),
                flag_portandspeedselected = (CB_Port.SelectedIndex >= 0) && (CB_Speed.SelectedIndex >= 0);

            /* Handle port is open condition */
            if (flag_isopen)
            {
                CB_Port.Enabled = false;
                CB_Speed.Enabled = false;
                BT_Port_Rescan.Enabled = false;
                BT_Port_Open.Enabled = false;
                BT_Port_Close.Enabled = true;
                return;
            }
            else
            {
                /* Handle port is closed condition */
                BT_Port_Rescan.Enabled = true;
                CB_Port.Enabled = true;
                CB_Speed.Enabled = true;
            }

            /* Handle condition port is not selected */
            BT_Port_Open.Enabled = flag_portandspeedselected;
            BT_Port_Close.Enabled = false;
        }

        private void BT_Port_Open_Click(object sender, EventArgs e)
        {
            /* Obtain port */
            Object obj_port = CB_Port.SelectedItem;
            if ((obj_port == null) ||
                (!(obj_port is String)))
                return;

            /* Obtain speed */
            Object obj_speed = CB_Speed.SelectedItem;
            if ((obj_speed == null) ||
                (!(obj_speed is String)))
                return;

            /* Save settings */
            persistentConfigSet("Port", (String)obj_port);
            persistentConfigSet("Speed", (String)obj_speed);

            /* Try to open port */
            try
            {
                sd.Open((String)obj_port, int.Parse((String)obj_speed));
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void BT_Port_Close_Click(object sender, EventArgs e)
        {
            if (!sd.IsOpen())
                return;
            try
            {
                sd.Close();
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void EventDataReceived(SerialDevice sd)
        {
            /* Obtain responses */
            if (TB_Response_Text.InvokeRequired)
                TB_Response_Text.Invoke(new MethodInvoker(delegate { TB_Response_Text.Text = sd.LastResponseString; }));
            if (TB_Response_Type_Weight.InvokeRequired)
                TB_Response_Type_Weight.Invoke(new MethodInvoker(delegate { TB_Response_Type_Weight.Text = sd.LastResponseStruct.ToString() + " " + DateTime.Now.ToString("HH:mm:ss tt"); }));

        }

        private void CB_Speed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CB_Speed.SelectedIndex < 0)
                return;
            persistentConfigSet("Speed", (String)CB_Speed.SelectedItem);
        }

        private void CB_SD_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* Check selected index */
            if (CB_SD.SelectedIndex < 0)
                return;

            /* Save configuration entry */
            persistentConfigSet("SD", (String)CB_SD.SelectedItem);

            /* Switch stream decoder */
            switch (((String)CB_SD.SelectedItem).ToLower())
            {
                case "none":
                    {
                        sd.StreamDecoder = null;
                        break;
                    }
                case "aupr":
                    {
                        sd.StreamDecoder = new SD_AUPr();
                        break;
                    }
                case "recr":
                    {
                        sd.StreamDecoder = new SD_reCr();
                        ((SD_reCr) sd.StreamDecoder).QueryInterval = 250;
                        break;
                    }
            }
        }

        private void Form_KernGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sd.IsOpen())
                BT_Port_Close_Click(sender, e);
        }

        private String GetPlatformString()
        {
            return Environment.Is64BitProcess ? "x64" : "x86";
        }

        private String GetRegAsmName()
        {
            String name=Path.Combine((String) Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\.NETFramework").GetValue("InstallRoot"),
                String.Format("v{0}.{1}.{2}\\", Environment.Version.Major, Environment.Version.Minor, Environment.Version.Build), "regasm.exe");
            if (!File.Exists(name))
                throw new FileNotFoundException("Cannot find " + name);
            return name;
        }

        private String GetCOMName()
        {
            String name=Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "kerncom-"+GetPlatformString()+".dll");
            return name;
        }

        private String GetCOMNameCheckExists()
        { 
            String name = GetCOMName();
            if (!File.Exists(name))
                throw new FileNotFoundException("Cannot find " + name);
            return name;
        }

        private bool HasAdministratorPrivileges()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void ExecRegAsmWait(String args)
        {
            /* Check administrator privileges */
            if (!HasAdministratorPrivileges())
                throw new Exception("Administrator privileges required.");

            /* Prepare process and process info objects */
            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(GetRegAsmName(), args);
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.UseShellExecute = false;

            /* Start and wait for process */
            p.Start();
            p.WaitForExit();

            /* Check exit code */
            if (p.ExitCode != 0)
                throw new Exception("KernCOM registration failed, code: "+p.ExitCode);
        }

        private void BT_Install_Click(object sender, EventArgs e)
        {
            try
            {
                ExecRegAsmWait("/codebase \""+GetCOMNameCheckExists()+"\"");
                ShowInformation("COM driver has been installed.");
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void BT_Uninstall_Click(object sender, EventArgs e)
        {
            try
            {
                ExecRegAsmWait("/unregister /codebase \""+GetCOMNameCheckExists()+"\"");
                ShowInformation("COM driver has been uninstalled.");
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void BT_Try_Click(object sender, EventArgs e)
        {
            try
            {
                Type t = Type.GetTypeFromProgID("KernCOM.SerialDevice");
                if (t == null)
                    throw new Exception("Cannot obtain type for KernCOM.SerialDevice.");
                Object o = Activator.CreateInstance(t);
                if (! (o is Object))
                    throw new Exception("Cannot create KernCOM.SerialDevice object.");
                ShowInformation("KernCOM object available.");
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void Form_KernGUI_Load(object sender, EventArgs e)
        {
        }
    }
}
