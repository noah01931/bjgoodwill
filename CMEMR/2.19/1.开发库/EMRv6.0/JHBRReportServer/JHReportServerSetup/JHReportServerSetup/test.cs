using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace JHEMR.JHReportServerSetup
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.CreateNoWindow = false;

                process.Start();
                //process.StandardInput.WriteLine("javac");
                process.StandardInput.WriteLine("e:");

                string ss = @"cd E:\JHBRReportServer\ReportServer\bin";
                process.StandardInput.WriteLine(ss);
                process.StandardInput.WriteLine("service.bat install");
                //process.StandardInput.WriteLine("");


                //new JHRSSetupLog().Writefile(JHCustomWayModel.StrPath + "JHBRReportServer", DateTime.Now + process.StandardOutput.ReadToEnd().ToString());
                process.StandardInput.WriteLine("exit");
                //labelControl1.Text = process.StandardOutput.ReadToEnd().ToString();
                //MessageBox.Show(process.StandardOutput.ReadToEnd().ToString());
                string netMessage = process.StandardOutput.ReadToEnd();

                process.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        [DllImport("Kernel32.DLL ", SetLastError = true)]
        public static extern bool SetEnvironmentVariable(string lpName, string lpValue);



        public static void SetPath(string pathValue)
        {

            //SetEnvironmentVariable("PATH", ";");

        }
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg,
             int wParam, StringBuilder lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]//SendMessageTimeout是在user32.dll中定义的
        public static extern IntPtr SendMessageTimeout(
     int windowHandle,
     uint Msg,
     int wParam,
     string lParam,
     SendMessageTimeoutFlags flags,
     uint timeout,
     out long result
     );

        [Flags]
        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0000,
            SMTO_BLOCK = 0x0001,
            SMTO_ABORTIFHUNG = 0x0002,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x0008
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            
            JHReportServerSetPath.SetPathAfter(@"C:\JHBRReportServer\jdk1.6.0_16\bin");
            JHRefreshRegedit.RefreshReg();
            System.Threading.Thread.Sleep(1000);
            MessageBox.Show("sfsdfs");
            //SetEnvironmentVariable("JHBR_HOME", @"C:\JHBRReportServer\jdk1.6.0_16");
            const int buffer_size = 1024;
            StringBuilder buffer = new StringBuilder(buffer_size);
            //const int WM_GETTEXT = 0x004A;
        //int t=  SendMessage(this.Handle, WM_GETTEXT, buffer_size, buffer);
           //SendMessageTimeout(HWND_BROADCAST,WM_SETTINGCHANGE,NULL,(LPARAM)(LPTSTR)lpD&shy;ata,SMTO_NORMAL,1000,&dwResult);


        //const int HWND_BROADCAST = 0xffff;
        //// DWORD dwMsgResult = 0L;
        //const uint WM_SETTINGCHANGE = 26;
        //const long SMTO_ABORTIFHUNG = 0x2;
        //System.UInt32 dwMsgResult1 = 0;
        //long s;
        //SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, 0, (string)"Environment", SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 5000, out s);

        }


        private static RegistryKey OpenSysEnvironment()
        {
            RegistryKey regLocalMachine = Registry.LocalMachine;
            RegistryKey regSYSTEM = regLocalMachine.OpenSubKey("SYSTEM", true);//打开HKEY_LOCAL_MACHINE下的SYSTEM 
            RegistryKey regControlSet001 = regSYSTEM.OpenSubKey("ControlSet001", true);//打开ControlSet001 
            RegistryKey regControl = regControlSet001.OpenSubKey("Control", true);//打开Control 
            RegistryKey regManager = regControl.OpenSubKey("Session Manager", true);//打开Control 

            RegistryKey regEnvironment = regManager.OpenSubKey("Environment", true);
            return regEnvironment;
        }

        /// <summary>
        /// 设置系统环境变量
        ///  李海威 2013-05-11
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="strValue">值</param>
        public static void SetSysEnvironment(string name, string strValue)
        {
            OpenSysEnvironment().SetValue(name, strValue);
        }


       


        //protected override void DefWndProc(ref System.Windows.Forms.Message m)
        //{

        //    switch (m.Msg)
        //    {

        //        case 0x201:

        //            ///string与MFC中的CString的Format函数的使用方法有所不同 

        //            string message = string.Format("收到消息!参数为:{0},{1}", m.WParam, m.LParam);

        //            MessageBox.Show(message);///显示一个消息框 

        //            break;
        //        case  0x004A://0x0400:
        //            MessageBox.Show("全局消息");
        //            break;

        //        default:

        //            base.DefWndProc(ref m);///调用基类函数处理非自定义消息。 

        //            break;

        //    }

        //} 


    }
}
