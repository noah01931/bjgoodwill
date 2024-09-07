using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace JHEMR.JHEMRAutoUpdate
{
    public partial class JHFormAutoUpdate : Form
    {
        DateTime dtStart, dtEnd;
        public JHFormAutoUpdate()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;//弹出位置居中
        }

        private void JHFormAutoUpdate_Load(object sender, EventArgs e)
        {
            if (Config.Instance().worker == null)
            {
                Config.Instance().worker = new BackgroundWorker();
                Config.Instance().worker.WorkerReportsProgress = true;
                Config.Instance().worker.WorkerSupportsCancellation = true;
                Config.Instance().worker.WorkerReportsProgress = true;
                Config.Instance().worker.WorkerSupportsCancellation = true;
                Config.Instance().worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                Config.Instance().worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                Config.Instance().worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                Config.Instance().worker.RunWorkerAsync();
            }
            timer1.Start();
            this.ControlBox = false;
            FormBorderStyle = FormBorderStyle.None;
            dtStart = DateTime.Now;
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            JHFileDown bll = new JHFileDown();
            bll.file_Down(Config.Instance().worker);
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                string aa = e.UserState.ToString();
                if (aa != "")
                {
                    this.lblLog.Text = aa;
                }
            }
        }
         System.Threading.Thread thd = null;
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {

                if (e.Error != null)
                { }
                else if (e.Cancelled)
                { }
                else
                {
                    Config.Instance().worker.CancelAsync();
                    Config.Instance().worker.Dispose();
                    Config.Instance().worker = null;
                    Application.DoEvents();
                   // Process.Start("C:\\嘉禾电子病历\\" + "JHEMRCentral.Win.exe");//去配置文件中的应用程序D:\QQ\Bin
                    string name = Config.Instance().GetfullProgramName();//获取要启动的应用程序的名字
                    Process.Start(name);//去配置文件中的应用程序
                    this.Hide();
                    timer1.Stop();
                    timer1.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
            finally
            {  }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dtEnd = DateTime.Now;
            TimeSpan tp = dtEnd - dtStart;
            lab_Time.Text =tp.Seconds+ "秒";
        }

    }
}
