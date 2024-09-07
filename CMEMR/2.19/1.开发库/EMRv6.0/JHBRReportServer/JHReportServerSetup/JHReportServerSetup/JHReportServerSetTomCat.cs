using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//////////////////////////////////////////////////////////////////////////
//Tomcat注册类，负责Tomcat的注册
//李海威 2013-05-11
//////////////////////////////////////////////////////////////////////////
namespace JHEMR.JHReportServerSetup
{
    class JHReportServerSetTomCat
    {
        /// <summary>
        /// 注册tomcat服务
        /// 李海威 2013-05-11
        /// </summary>
        public static void TomcatInstall()
        {
            try
            {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine("");
            process.StandardInput.WriteLine(JHCustomWayModel.StrPath.Substring(0, 1) + ":");
            process.StandardInput.WriteLine(@"cd " + JHCustomWayModel.StrPath + @"JHBRReportServer\ReportServer\bin");
            process.StandardInput.WriteLine("service.bat install");
            process.StandardInput.WriteLine("");
            process.StandardInput.WriteLine("exit");
            process.Close();
            }
              catch (System.Exception ex)
            {
            }
        }
    }
}
