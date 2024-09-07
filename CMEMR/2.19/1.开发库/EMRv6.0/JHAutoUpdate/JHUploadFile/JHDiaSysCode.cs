using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JHEMR.JHWidgetConfig
{
    public partial class JHDiaSysCode : JHEMR.JHWinUIControlLib.JHDialogForm
    {

        DataTable dtSouce;//数据源
        public string SysCode = "";
        public JHDiaSysCode()
        {
            InitializeComponent();
        }

        public JHDiaSysCode(DataTable dt)
        {
            InitializeComponent();
            dtSouce = dt;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SysCode = lupSysCode.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void JHDiaSysCode_Load(object sender, EventArgs e)
        {
            lupSysCode.Properties.DataSource = dtSouce;
            lupSysCode.Properties.DisplayMember = "SYS_CODE";
            lupSysCode.Properties.ValueMember = "SYS_CODE";
            lupSysCode.Properties.ShowHeader = false;
        }
    }
}
