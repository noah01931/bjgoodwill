using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ClassGenerate.Proc;

namespace ClassGenerate
{
    public partial class ClassGenerator : Form
    {
        /// <summary>
        /// 生成路径
        /// </summary>
        string path = "";
        /// <summary>
        /// 数据库访问
        /// </summary>
        public static DataAccess da;
        /// <summary>
        /// 表处理
        /// </summary>
        ProcTable pt ;
        /// <summary>
        /// 文件操作对象
        /// </summary>
        Generator g ;

        public ClassGenerator(DBConnectType ConnectType)
        {
            InitializeComponent();
            da = new DataAccess(ConnectType);
            pt = new ProcTable();
            g = new Generator();
            pt.SetConnectionString(LoadServer.cn);
            if (TestAndLoad())
            {
                btnPreview.Enabled = true;
                btnGenerate.Enabled = true;
            } 
        }

        #region 初始化
        private void ClassGenerator_Load(object sender, EventArgs e)
        {
            DataTable dtDatabase = pt.GetAllDataBase(false);
            foreach (DataRow dr in dtDatabase.Rows)
            {
                treeView1.Nodes.Add(dr["name"].ToString());
            }

            lblMsg.Text = "请在左边选择数据库";
        } 
        #endregion
        
        #region 退出程序
        private void ClassGenerator_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        } 
        #endregion

        #region 点击树设置数据库
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            LoadServer.cn.Database = e.Node.Text;
            gbOpition.Text = e.Node.Text + ":生成选项";
            pt.SetConnectionString(LoadServer.cn);

            if (TestAndLoad())
            {
                btnPreview.Enabled = true;
                btnGenerate.Enabled = true;
            } 

            lblMsg.Text = "";
        } 
        #endregion

        #region 测试连接
        private void btnTest_Click(object sender, EventArgs e)
        {
            TestAndLoad();
        }
        /// <summary>
        /// 测试,加载数据
        /// </summary>
        private bool TestAndLoad()
        {
            bool ret = false;
            if (string.IsNullOrEmpty(LoadServer.cn.ConnectionString))
            {
                MessageBox.Show("请输入连接字符串,测试通过后才可使用");
                lblMsg.Text = "代码生成器:连接失败或没有表";
                ret = false;
            }
            da.strConnection = LoadServer.cn.ConnectionString;
            if (da.TestConnect())
            {
                lblMsg.Text = "代码生成器:连接成功";
                BindAllTables();
                ret = true;
            }
            return ret;
        } 
        #endregion

        #region 绑定当前数据库中所有用户表
        /// <summary>
        /// 绑定所有用户表
        /// </summary>
        void BindAllTables()
        {
            try
            {
                DataTable dt = pt.GetAllTable();

                lvTables.Items.Clear();
                for(int i=0; i<dt.Rows.Count; i ++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Checked = false;
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dt.Rows[i][0].ToString()));
                    //item.SubItems.Add(new ListViewItem.ListViewSubItem(item, pt.GetTableDesciption(dt.Rows[i][0].ToString())));
                    //item.Checked =!Convert.ToBoolean( dt.Rows[i]["IsDeleted"]);
                    lvTables.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        void BindAllTables(DataTable dt, bool boolChecked)
        {
            try
            {
                lvTables.Items.Clear();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem item = new ListViewItem();
                    item.Checked = boolChecked;
                    item.SubItems.Add(new ListViewItem.ListViewSubItem(item, dt.Rows[i][0].ToString()));
                    //item.SubItems.Add(new ListViewItem.ListViewSubItem(item, pt.GetTableDesciption(dt.Rows[i][0].ToString())));
                    //item.Checked =!Convert.ToBoolean( dt.Rows[i]["IsDeleted"]);
                    lvTables.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        } 
        #endregion
        
        #region 设置生成属性
        /// <summary>
        /// 设置生成属性
        /// </summary>
        /// <param name="g"></param>
        void SetGenerator(Generator g)
        {
            g.strNameSpace = txtNamespace.Text.Trim();
            g.strPrefix = txtPrefix.Text.Trim();
            g.path = path;
        } 
        #endregion

        #region 获取用户选中的生成选项
        /// <summary>
        /// 获取用户选中的生成选项
        /// </summary>
        /// <returns></returns>
        List<string> GetChecked()
        {
            List<string> list = new List<string>();
            foreach (Control c in gbOpition.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox ck = c as CheckBox;
                    if (ck.Checked && ck.Text != "所有表")
                    {
                        list.Add(ck.Text);
                    }
                }
            }
            return list;
        } 
        #endregion

        void Go(bool isWrite)
        {
            if (lvTables.Items.Count <= 0)
            {
                MessageBox.Show("连不上数据库");
                return;
            }
            List<string> option = GetChecked();
            if (option.Count <= 0)
            {
                MessageBox.Show("请选择至少一项生成选项(实体类/数据访问层)");
                return;
            }
            txtPreview.Text = "";
            
            SetGenerator(g);
            if (lvTables.CheckedItems.Count > 0)
            {
                List<string> tablesName = pt.GetTables(lvTables);
                //lsj--获取全部表名
                StringBuilder ret = new StringBuilder();
                ProcString ps = new ProcString();
                ret.AppendLine("/*");
                for (int i = 0; i < tablesName.Count; i++)
                {
                    ret.AppendLine(tablesName[i].ToString());
                }
                ret.AppendLine("*/");
                txtPreview.Text = ret.ToString();
                if (ckModel.Checked)
                {
                    txtPreview.Text += "//实体类\r\n";
                    txtPreview.Text += g.ToModels(isWrite, tablesName);
                }
                if (ckIndex.Checked)
                {
                    if (string.IsNullOrEmpty(txtHOSPITAL_NO.Text.Trim()))
                    {
                        MessageBox.Show("HOSPITAL_NO不可为空！");
                        return ;
                    }
                    ret = new StringBuilder();
                    txtPreview.Text = "";
                    txtPreview.Text += "----修改主键SQL\r\n";
                    txtPreview.Text += g.ToIndexSQL(isWrite, tablesName,txtHOSPITAL_NO.Text .Trim());
                }
                if (ckDataAccess.Checked)
                {
                    txtPreview.Text += "//数据访问层\r\n";
                    txtPreview.Text += g.ToDataAccessForTables(isWrite, tablesName);
                }
                if (ckOracleSQL.Checked)
                {
                    txtPreview.Text += "//数据访问层\r\n";
                    txtPreview.Text += g.ToDataAccess_SQL(isWrite, tablesName);
                    
                }
                if (ckFullManage.Checked)
                {
                    txtPreview.Text += "//FullManage\r\n";
                    txtPreview.Text += g.ToFullManage(isWrite, tablesName);
                }
                if (ckProc.Checked)
                {
                    txtPreview.Text += "--存储过程\r\n";
                    txtPreview.Text += g.ToProc(isWrite, tablesName);
                }
                if (ckCache.Checked)
                {
                    txtPreview.Text += "//缓存Cache\r\n";
                    txtPreview.Text += g.ToCache(isWrite, tablesName);
                }
                if (ckFullCache.Checked)
                {
                    txtPreview.Text += "//缓存FullCache\r\n";
                    txtPreview.Text += g.ToFullCache(isWrite, tablesName);
                }
                if (!string.IsNullOrEmpty(path) && ckDocument.Checked)
                {
                    g.ToWord(tablesName);
                }

                tabControl1.SelectedIndex = 1; 
            }
            else
            {
                lblMsg.Text = "请选择表!";
                MessageBox.Show(lblMsg.Text);
            }          
        }

        #region 预览效果
        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                Go(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成失败:" + ex.Message);
                lblMsg.Text = "代码生成器:生成失败";
            }
        }
        #endregion

        #region 生成
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = "";
                path = "";
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK)
                {
                    path = fbd.SelectedPath;
                    Go(true);
                }
                else
                {
                    fbd.SelectedPath = "";
                    lblMsg.Text = "重新选择存储路径";
                    MessageBox.Show("重新选择存储路径");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("生成失败:" + ex.Message); 
                lblMsg.Text = "代码生成器:生成失败";
            }
        } 
        #endregion

        #region 表选择(全选/全不选)
        private void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (ckSelect.Checked)
            {
                foreach (ListViewItem item in lvTables.Items)
                {
                    item.Checked = true;
                }
            }
            else
            {
                foreach (ListViewItem item in lvTables.Items)
                {
                    item.Checked = false;
                }
            }
        } 
        #endregion

        //lsj
        private void btnFilter_Click(object sender, EventArgs e)
        {
            List<String> tables = new List<String>();
            DataSet ds = da.ReturnDataSet(GetFilterSQL());
            BindAllTables(ds.Tables[0],false);
            
        }
        private string GetFilterSQL()
        {
            return "SELECT Table_name as name FROM  User_tables where User_tables.Table_name like 'JHOUTPAT%'";
        }
        private void btnFilter_Click_Backup()
        {

            List<String> tables = new List<String>();
            DataSet ds = da.ReturnDataSet("SELECT Table_name as name FROM  User_tables where " +
                "User_tables.Table_name like 'JHINPAT_FP%'"
                + " or User_tables.Table_name = 'JHBD_CV_ITEMS'"
                + " or User_tables.Table_name = 'JHPAT_MASTER_INDEX'"
                + " or User_tables.Table_name = 'JHINPAT_ADT_LOG'"
                //------------------------
                + " or User_tables.Table_name like 'JHBD_%'"
                + " or User_tables.Table_name like 'JHINPAT_MR%'"
                + " or User_tables.Table_name like 'JHINPAT_PAT%'"
                + " or User_tables.Table_name like 'JHMR_TEMPLET%'"
                + " or User_tables.Table_name like 'JHPAT_PREV%'"
                + " or User_tables.Table_name like 'JHPIX_%'"
                );
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tables.Add(ds.Tables[0].Rows[i][0].ToString());
            }
            //tables.Add("JHBD_CV_ITEMS");
            //tables.Add("JHPIX_ADDRESS");
            //tables.Add("JHPIX_ENTITY_INDEX");
            //tables.Add("JHPIX_MERGE_LOG");
            //tables.Add("JHPIX_PAT_ADDRESS");
            //tables.Add("JHPIX_PAT_CROSS_REF");
            //tables.Add("jhpix_pat_identity");
            //tables.Add("JHPIX_PAT_VISIT");
            //tables.Add("JHPIX_PAT_VISIT_RELATION");

            //tables.Add("JHINPAT_FP_BLOOD_TRANS");
            //tables.Add("JHINPAT_FP_DIAGNOSIS");
            //tables.Add("JHINPAT_FP_DIAG_COMPARING");
            //tables.Add("JHINPAT_FP_MEDICAL_COSTS");
            //tables.Add("JHINPAT_FP_OPERATION");
            //tables.Add("JHINPAT_FP_TUMOUR");
            //tables.Add("JHINPAT_FP_TUMOUR_DETAIL");
            //tables.Add("JHINPAT_PAT_NEWBORN_BABY");
            //tables.Add("JHINPAT_PAT_VISIT");
            //tables.Add("JHINPAT_PAT_VISIT_MR");
            //tables.Add("JHINPAT_PAT_VISIT_RELATION");
            //tables.Add("JHINPAT_PAT_VITAL_SIGNS");

            //tables.Add("JHPAT_MASTER_INDEX");
            //tables.Add("JHPAT_PREV_ALERGY");
            //tables.Add("JHPAT_PREV_COMPLAINED");
            //tables.Add("JHPAT_PREV_DIAGNOSE");
            //tables.Add("JHPAT_PREV_DRUGS");
            //tables.Add("JHPAT_PREV_OPERATION");
            //tables.Add("JHPAT_PREV_REACTIONS");
            //JHINPAT_FP
            foreach (ListViewItem item in lvTables.Items)
            {
                item.Checked = false;
            }
            foreach (String table in tables)
            {
                foreach (ListViewItem item in lvTables.Items)
                {
                    string tablename = item.SubItems[1].Text.ToLower();
                    if (tablename == table.ToLower())
                        item.Checked = true;
                }
            }
        }
        private void btnOnOperation_Click(object sender, EventArgs e)
        {
            //tables = new Dictionary<String, Boolean>();
            //tables.Add("T_UseComm", true);
            //tables.Add("T_UseDrug", true);
            //tables.Add("T_OccurEvent", true);
            //tables.Add("T_UseSap", true);
            //tables.Add("T_PhysioData", true);
            //tables.Add("T_AnalyseSap", true);
            //tables.Add("T_FactDisease", true);
            //tables.Add("T_FactOpe", true);
            //tables.Add("T_FactWorker", true);
            //tables.Add("T_DoOperation", true);
            //foreach (ListViewItem item in lvTables.Items)
            //{
            //    string tablename = item.SubItems[1].Text;
            //    foreach (var pair in tables)
            //    {
            //        if (pair.Key.ToString() == tablename)
            //        {
            //            item.Checked = tables[tablename];
            //            break;
            //        }
            //    }
            //    if (item.Checked)
            //        continue;
            //    item.Checked = false;
            //}
        }

    }
}
