using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace JHEMR.JHReportServerSetup
{
    /// <summary>
    /// 将环境变量添加到系统
    /// </summary>
    class JHReportServerSetPath
    {

        /// <summary>
        /// 获取系统环境变量
        ///  李海威 2013-05-11
        /// </summary>
        /// <param name="name">环境变量名字</param>
        /// <returns>环境变量路径</returns>
        public static string GetSysEnvironmentByName(string name)
        {
            string result = string.Empty;
            try
            {
                result = OpenSysEnvironment().GetValue(name).ToString();//读取
            }
            catch (Exception ex)
            {


                return string.Empty;
            }
            return result;

        }

        /// <summary>
        /// 打开系统环境变量注册表
        ///  李海威 2013-05-11
        /// </summary>
        /// <returns>RegistryKey</returns>
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

        /// <summary>
        /// 检测系统环境变量是否存在
        ///  李海威 2013-05-11
        /// </summary>
        /// <param name="name">环境变量名字</param>
        /// <returns></returns>
        public bool CheckSysEnvironmentExist(string name)
        {
            if (!string.IsNullOrEmpty(GetSysEnvironmentByName(name)))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 添加到PATH环境变量（会检测路径是否存在，存在就不重复）
        ///  李海威 2013-05-11
        /// </summary>
        /// <param name="strHome">环境变量路径</param>
        public static void SetPathAfter(string strHome)
        {
            string pathlist;
            //SetPathBefore(strHome);
            pathlist = GetSysEnvironmentByName("PATH");
            //检测是否以;结尾
            if (pathlist.Substring(pathlist.Length - 1, 1) != ";")
            {
                SetSysEnvironment("PATH", pathlist + ";");
                pathlist = GetSysEnvironmentByName("PATH");
            }
            string[] list = pathlist.Split(';');
            bool isPathExist = false;

            foreach (string item in list)
            {
                if (item == strHome)
                    isPathExist = true;
            }
            if (!isPathExist)
            {
                SetSysEnvironment("PATH", pathlist + strHome+@"JHBRReportServer\jdk1.6.0_16\bin" + ";");
            }
            SetSysEnvironmentVariable.SetPath(strHome);
        }
        /// <summary>
        /// 设置Tomcat环境变量JHBR_HOME之前
        /// 李海威 2013-05-11
        /// </summary>
        /// <param name="strHome">环境变量路径</param>
        public static void SetPathBefore(string strHome)
        {

                SetSysEnvironment("JHBR_HOME", strHome + @"JHBRReportServer\jdk1.6.0_16" );


        }
#region 备份
        //public static void SetPathBefore(string strHome)
        //{

        //    string pathlist;
        //    pathlist = GetSysEnvironmentByName("JHBR_HOME");
        //    string[] list = pathlist.Split(';');
        //    bool isPathExist = false;

        //    foreach (string item in list)
        //    {
        //        if (item == strHome)
        //            isPathExist = true;
        //    }
        //    if (!isPathExist)
        //    {
        //        SetSysEnvironment("JHBR_HOME", strHome + @"JHBRReportServer\jdk1.6.0_16" + ";" + pathlist);
        //        new JHRSSetupLog().Writefile(strHome + "JHBRReportServer", DateTime.Now + "JHBR_HOME环境变量安装设置成功!路径为：" + strHome + @"JHBRReportServer\jdk1.6.0_16");

        //    }

        //}
#endregion

    
        /// <summary>
        /// 设置环境变量
        /// </summary>
        /// <param name="strHome">环境变量路径</param>
        public static void SetPath(string strHome)
        {

            string pathlist;
            pathlist = GetSysEnvironmentByName("PATH");
            string[] list = pathlist.Split(';');
            bool isPathExist = false;

            foreach (string item in list)
            {
                if (item == strHome)
                    isPathExist = true;
            }
            if (!isPathExist)
            {
                SetSysEnvironment("PATH", pathlist + strHome + ";");

            }

        }



    }
   // Kernel32.DLL内有SetEnvironmentVariable函数用于设置系统环境变量
/// <summary>
/// 
/// </summary>
//C#调用要用DllImport，代码封装如下：
class SetSysEnvironmentVariable
    {
        [DllImport("Kernel32.DLL ", SetLastError = true)]
        public static extern bool SetEnvironmentVariable(string lpName, string lpValue);

        public static void SetPath(string pathValue)
        {
            string pathlist;
            pathlist =  JHReportServerSetPath.GetSysEnvironmentByName("PATH");
            string[] list = pathlist.Split(';');
            bool isPathExist = false;

            foreach (string item in list)
            {
                if (item == pathValue)
                    isPathExist = true;
            }
            if (!isPathExist)
            {
                SetEnvironmentVariable("PATH", pathlist + pathValue+";");
               
            }

        }
    }
}
