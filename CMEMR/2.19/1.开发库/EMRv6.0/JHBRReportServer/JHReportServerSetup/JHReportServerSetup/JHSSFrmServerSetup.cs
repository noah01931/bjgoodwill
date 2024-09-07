using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using System.Runtime.InteropServices;
//////////////////////////////////////////////////////////////////////////
//安装程序主界面类
//李海威 2013-05-02
//////////////////////////////////////////////////////////////////////////

namespace JHEMR.JHReportServerSetup
{
    public partial class JHSSFrmServerSetup : Form
    {
        public JHSSFrmServerSetup()
        {
            InitializeComponent();
        }
        Thread t1;//拷贝文件线程
        bool b = false;//文件拷贝是否执行成功
        public string strPath;
        int i = 0;//组件注册的数量
        private void wc_setup_CancelClick(object sender, CancelEventArgs e)
        {
      
            Application.Exit();
        }

        private void JHSSFrmServerSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!b)
            {
                if (DialogResult.No == MessageBox.Show("安装程序未完成安装,如果您现在退出,您的程序将不能安装。\r\n您可以以后再运行程序完成安装。\r\n 您真的要退出吗??", "提示:", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    e.Cancel = true;

                }
            }
            if (wc_setup.SelectedPage == FinishWizardPage1)
            {

                if (chk_web.Checked)
                {
                    System.Diagnostics.Process.Start(@"http://127.0.0.1:8888/report/");
                }

            }
        }

        private void btn_browser_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                txt_way.Text = folderBrowserDialog1.SelectedPath;
            }
            JHCustomWayModel.StrPath = strPath;
        }
        DevExpress.XtraWizard.WizardCommandButtonClickEventArgs eX;
        private void wc_setup_NextClick(object sender, DevExpress.XtraWizard.WizardCommandButtonClickEventArgs e)
        {
         

            if (wc_setup.SelectedPage == wizardPage2)//进入程序拷贝阶段
            {
                e.Page.AllowNext = false;
                eX = e;
                t1 = new Thread(new ThreadStart(CopyFiles));
                if (!string.IsNullOrEmpty(txt_way.Text.Trim()))
                {
                    t1.IsBackground = true;
                    t1.Start();
                    timer1.Enabled = true;
                    timer1.Start();
                    timer2.Enabled = true;

                }

            }

        }
        DateTime stdate;//服务开始拷贝时间
        public void CopyFiles()
        {
            stdate = DateTime.Now;
            b = JHReportFileCopy.CopyDir(Application.StartupPath + @"\JHBRReportServer", strPath + "JHBRReportServer");
            timer1.Enabled = false;
            timer2.Enabled = false;
            aa xx = new aa(dd);
            labelControl3.Invoke(xx, "组件安装完成！");

            //eX.Page.AllowNext = true;
        }

        delegate void aa(string mes);
        public void dd(string mes)
        {
            progressBarControl1.EditValue = JHCustomWayModel.ProMax;
            labelControl3.Text = mes;
            groupBox1.Text = mes;
            if (b)
            {

                #region 设置环境变量
                JHReportServerSetPath.SetPathAfter(strPath);
                JHReportServerSetPath.SetPathBefore(strPath);
                #endregion 设置环境变量
                #region 刷新注册表
                JHRefreshRegedit.RefreshReg();
                #endregion 刷新注册表
                #region 注册Tomcat服务
                JHReportServerSetTomCat.TomcatInstall();
                #endregion 注册Tomcat服务
              
                wc_setup.SelectedPage = FinishWizardPage1;

            }
            else
            {
                MessageBox.Show("组件注册失败，请检查路径重新安装");
            }


        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            JHCustomWayModel.ProCurr = 0;
            JHCustomWayModel.ProCurr += 10;



            progressBarControl1.EditValue = JHCustomWayModel.ProCurr;
            if (Convert.ToInt32(progressBarControl1.EditValue) == JHCustomWayModel.ProMax)
            {
                i++;
                labelControl3.Visible = true;
                labelControl3.Text = string.Format("正在注册第{0}个组建，请稍后···", i);
                JHCustomWayModel.ProCurr -= new Random().Next(9, 111);

            }
        }

        private void wc_setup_FinishClick(object sender, CancelEventArgs e)
        {
            Application.Exit();
        }

        private void wc_setup_SelectedPageChanging(object sender, DevExpress.XtraWizard.WizardPageChangingEventArgs e)
        {
            if (wc_setup.SelectedPage == wizardPage1)
            {
                if (!chk_treaty.Checked)
                {
                    e.Cancel = true;
                }
            }
            if (wc_setup.SelectedPage == wizardPage2)
            {
                e.Page.AllowNext = false;
            }
            if (wc_setup.SelectedPage == wizardPage2 && txt_way.Text == "")
            {
                MessageBox.Show("安装路径为空！安装无法继续！", "提示:");

                e.Cancel = true;
            }
           
            if (wc_setup.SelectedPage == FinishWizardPage1)
            {
                e.Page.AllowBack = false;
            }
            if (chk_web.Checked)
            {
                System.Diagnostics.Process.Start(@"http://127.0.0.1:8888/report/");
            }
                
            //}
           
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            groupBox1.BackgroundImage = imageCollection1.Images[new Random().Next(0, 3)];
        }

        private void txt_way_TextChanged(object sender, EventArgs e)
        {
            if (txt_way.Text.Trim().Substring(txt_way.Text.Trim().Length - 1, 1) != @"\")//检查安装路径后面是否包含\如果不包含则进行添加
            {
                strPath = txt_way.Text.Trim() + @"\";
            }
            else
            {
                strPath = txt_way.Text.Trim();
            }

        }
       




    }
}
