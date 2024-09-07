using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClassGenerate
{
    public partial class LoadServer : Form
    {
        public static Connection cn = new Connection();
        public LoadServer()
        {
            InitializeComponent();
        }
        
        private void LoadServer_Load(object sender, EventArgs e)
        {
            //SQL------------
            //this.txtServer.Text = "192.168.1.10";
            //this.cbMethod.SelectedIndex = 1;
            //this.txtPwd.Text = "123456";
            //-------------
            this.txtServer.Text = "JHEMR20";
            this.cbMethod.SelectedIndex = 2;
            this.txtUserName.Text = "JHEMR";
            this.txtPwd.Text = "JHEMR";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (1 == cbMethod.SelectedIndex)
            {
                this.Size = new Size(this.Size.Width, this.Size.Height + 100);
                groupBox1.Size = new Size(groupBox1.Size.Width, groupBox1.Size.Height + 100);
                btnLogin.Top += 100;
                btnExit.Top += 100;
                lblMsg.Top += 100;
                pnlValidate.Visible = true;
            }
            else
            {
                btnLogin.Top -= 100;
                btnExit.Top -= 100;
                groupBox1.Size = new Size(groupBox1.Size.Width, groupBox1.Size.Height - 100);
                this.Size = new Size(this.Size.Width, this.Size.Height - 100);
                pnlValidate.Visible = false;
                lblMsg.Top -= 100;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            //ProcWord pword = new ProcWord();
            ////pword.SaveAs();
            //pword.WriteWordFileTest();
            //return;

            lblMsg.Text = "";
            cn.Server = txtServer.Text.Trim();         
            cn.Database = "master";
            if (0==cbMethod.SelectedIndex)
            {
                cn.IsWindows = true;
            }
            else
            {
                cn.Uid = txtUserName.Text;
                cn.Pwd = txtPwd.Text;
                cn.IsWindows = false;
            }
            try
            {
                DataAccess da;
                //string sql = "";
                if (this.cbMethod.SelectedItem.ToString() == "Oracle")
                {
                    da = new DataAccess(DBConnectType.ORACLE);
                }
                else
                {
                    da = new DataAccess(DBConnectType.SQLSERVER);
                    da.strConnection = cn.ConnectionString;
                }
                if (da.TestConnect())
                {
                    lblMsg.Text = "连接成功";
                    ClassGenerator g = new ClassGenerator(da.DbConnectType);
                    g.Show();
                    this.Hide();
                }
                else
                {
                    lblMsg.Text = "连接失败";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
