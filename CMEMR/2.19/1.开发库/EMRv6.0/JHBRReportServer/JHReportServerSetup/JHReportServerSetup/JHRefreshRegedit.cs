using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
//////////////////////////////////////////////////////////////////////////
//设置环境变量后，不能立即生效，需要刷新注册表，
//李海威 2013-05-17
//////////////////////////////////////////////////////////////////////////
namespace JHEMR.JHReportServerSetup
{
    class JHRefreshRegedit
    {
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
        public static void RefreshReg()
        {
            try
            {
           
            const int HWND_BROADCAST = 0xffff;
            // DWORD dwMsgResult = 0L;
            const uint WM_SETTINGCHANGE = 26;
            const long SMTO_ABORTIFHUNG = 0x2;
            System.UInt32 dwMsgResult1 = 0;
            long s;
            SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, 0, (string)"Environment", SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 5000, out s);
            System.Threading.Thread.Sleep(1000);
            SendMessageTimeout(HWND_BROADCAST, WM_SETTINGCHANGE, 0, (string)"Environment", SendMessageTimeoutFlags.SMTO_ABORTIFHUNG, 5000, out s);

            }
            catch (System.Exception ex)
            {
            }

        }
    }
}
