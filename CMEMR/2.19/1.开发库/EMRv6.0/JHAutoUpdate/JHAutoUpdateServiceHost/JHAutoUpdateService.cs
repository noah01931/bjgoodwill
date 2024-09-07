using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using JHEMR.JHServicesLib.Provider;
using JHEMR.JHServicesLib.ErrorHandler;
using JHEMR.JHAutoUpdateServiceLib;

namespace JHAutoUpdateServiceHost
{
    partial class JHAutoUpdateService : ServiceBase
    {
        JHSRVServiceManager service;
        public JHAutoUpdateService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            try
            {
                service = new JHSRVServiceManager();
                service.StartServiceHost(typeof(IJHAutoUpdateService), typeof(JHEMR.JHAutoUpdateServiceLib.JHAutoUpdateService), "JHAutoUpdateService");
            }
            catch { }
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
    }
}
