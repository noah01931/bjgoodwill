using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Collections;
using JHEMR.JHWinUIControlLib;
namespace JHEMR.JHWidgetConfig
{
    public partial class JHWidgetUploadPlatFile : JHEMR.JHWinUIControlLib.JHWidget
    {
        DataTable dtSelect = new DataTable();
        public JHWidgetUploadPlatFile()
        {
            InitializeComponent();
            this.LastVerSion = "2";
            this.LastUpdateTime = DateTime.Parse("2013-05-02");
            this.LastAuthor = "王海林";
            DataTable dt = new DataTable();
            dt.Columns.Add("IS_CURVERSION");
            dt.Columns.Add("IS_CURVERSION_CN");
            DataRow dr = dt.NewRow();
            dr[0] = "0";
            dr[1] = "否";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "1";
            dr[1] = "是";
            dt.Rows.Add(dr);
            repositoryItemLookUpEdit2.DataSource = dt;
            this.comboBox1.SelectedIndexChanged -= new EventHandler(txtTitle_TextChanged);
            this.comboBox1.SelectedIndex = 0;
            dtSelect.Columns.Add("FILE_NAME");
            dtSelect.Columns.Add("FILE_NAME_ALL");
            dtSelect.Columns.Add("VERSION_NAME");
            dtSelect.Columns.Add("SYS_CODE");
            this.gridControl1.DataSource = dtSelect;
        }

        

        //选择文件
        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.Multiselect = true;
            diag.InitialDirectory = Path.Combine(Application.StartupPath, "WidgetDll");
            if (diag.ShowDialog() == DialogResult.OK)
            {
                if(diag.CheckFileExists)
                {
                    string strSysCode = "";
                    foreach (string strFileNameTemp in diag.FileNames)
                    {
                        string strFileName = strFileNameTemp.Substring(diag.FileName.LastIndexOf('\\') + 1);
                        if (Is_Get_SysFile(strFileName, out strSysCode))
                        {
                            DataRow[] drs = dtSelect.Select(" FILE_NAME='" + strFileName + "'");
                            if (drs.Length > 0)
                            {
                                dtSelect.Rows.Remove(drs[0]);
                            }
                            DataRow dr = dtSelect.NewRow();
                            dr["FILE_NAME"] = strFileName;
                            dr["FILE_NAME_ALL"] = strFileNameTemp;
                            FileVersionInfo info = FileVersionInfo.GetVersionInfo(strFileNameTemp);
                            string version = info.FileMajorPart + "." + info.FileMinorPart + "." + info.FileBuildPart;
                            dr["VERSION_NAME"] = version;
                            dr["SYS_CODE"] = strSysCode;
                            dtSelect.Rows.Add(dr);
                            dtSelect.AcceptChanges();
                        }
                    }
                }
            }
        }

        private DataTable dtAll = null;
        public override void WidgetRefresh(object sender, JHWinUIControlLib.JHWidgetEventArgs e)
        {
            this.comboBox1.SelectedIndexChanged += new EventHandler(txtTitle_TextChanged);
            BindGrid();
           
            base.WidgetRefresh(sender, e);
        }

        public void BindGrid()
        {
            dtAll = get_All_UploadFile("");
            Filter();
            this.grdList.DataSource = dtAll.DefaultView;
        }

        public void Filter()
        {
            string strFilter = "1=1";
            if (this.txtTitle.Text != "")
            {
                strFilter += " AND FILE_NAME LIKE '%" + this.txtTitle.Text + "%'";
            }
            if(this.comboBox1.SelectedIndex!=0)
            {
                strFilter += " AND IS_CURVERSION  ='" + (this.comboBox1.SelectedIndex-1).ToString()+ "'";
            }
            if (this.txtNo.Text != "")
            {
                strFilter += " AND   DISTRIBUTE_NO =" + this.txtNo.Text;
            }
            this.dtAll.DefaultView.RowFilter = strFilter;

        }

        /// <summary>
        /// 是否注册文件字典
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static bool Is_Get_SysFile(string fileName, out string strSys)
        {
            string strSql = " SELECT * FROM JHSYS_FILE_DICT WHERE 1=1";
            if( fileName!="")
            {
                strSql+=" AND FILE_NAME ='"+fileName+"'";
            }
            DataSet  dst=  JHEMRGlobalLib.JHGlobal.DBInstance().GetDataSet(strSql);
            if (dst.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show(fileName + "在JHSYS_FILE_DICT表中没有注册");
                strSys = "";
                return false;
            }
            #region 暂时不用判断是否框架文件
              //else
            //{
            //    if (dst.Tables[0].Rows[0]["IS_FRAME_FILE"].ToString() == "1")
            //    {
            //        MessageBox.Show(fileName + "在JHSYS_FILE_DICT表中是框架文件不能添加");
            //        return false;
            //    }
            //    return true;
            //}
            #endregion
            else if (dst.Tables[0].Rows.Count > 1)
            {
                    JHDiaSysCode code = new JHDiaSysCode(dst.Tables[0]);
                    if (code.ShowDialog()==DialogResult.OK)
                    {
                        strSys = code.SysCode;
                        return true;
                    }
                    MessageBox.Show("放弃上传!");
                    strSys = "";
                    return false;
            }
            else
            {
                strSys = dst.Tables[0].Rows[0]["SYS_CODE"].ToString();
                return true;
            }
        }

        public static DataTable get_All_UploadFile(string   isCursion)
        {
            string strSql = @"SELECT A.FILE_NAME,
       A.UPLOAD_DATE,
       A.LAST_UPDATE,
       A.VERSION_NAME,
       A.USER_ID,
       A.FILE_LENGTH,
       A.HOSPITAL_NO,
       A.IS_CURVERSION,
       A.DISTRIBUTE_NO,B.USER_NAME ,C.DESCRIBE,C.PATH FROM  JHSYS_FILE_UPLOAD   A
INNER JOIN USERS B  ON  A.USER_ID=B.USER_ID AND A.HOSPITAL_NO=B.HOSPITAL_NO
INNER JOIN JHSYS_FILE_DICT C ON A.FILE_NAME=C.FILE_NAME ";
            if(isCursion!="")
            {
                strSql+=" where A.IS_CURVERSION="+isCursion;

            }

            strSql += "  ORDER BY A.DISTRIBUTE_NO desc ,A.LAST_UPDATE DESC ";
            DataTable dt = JHEMRGlobalLib.JHGlobal.DBInstance().GetDataSet(strSql).Tables[0];
            return dt;
        }

        /// <summary>
        /// 返回可以用的发布序号
        /// </summary>
        /// <returns></returns>
        public static int GetSerialNo()
        {
            string  strSerialNo = JHEMRGlobalLib.JHGlobal.DBInstance().GetSingle("SELECT MAX (DISTRIBUTE_NO) AS MAXNO FROM JHSYS_FILE_UPLOAD ");
            int iSerialNo = 0;
            if (string.IsNullOrEmpty(strSerialNo))
            {
                iSerialNo = 1;

            }
            else
            {
                iSerialNo=int.Parse(strSerialNo) + 1; ;

            }
            return iSerialNo;
        }
       
        
        
        private void btnUpload_Click(object sender, EventArgs e)
        {
            //this.M_JHWidgetInOrOutArg = new List<JHWidgetInOrOutArg>();
            //JHWidgetInOrOutArg aa = new JHWidgetInOrOutArg();
            //aa.ParamName = "0";
            //aa.ParamValue = "999";
            //this.M_JHWidgetInOrOutArg.Add(aa);
            //this.CloseDiaLogForm();
            //return;
            if (this.dtSelect.Rows.Count > 0)
            {
                if (JHEMRGlobalLib.MessageBoxs.ShowYesNo("是否确认上传列表文件 ,上传后客户端会下载新的文件"))
                {
                   
                    int iSerialNo = 0;
                    iSerialNo = GetSerialNo();
                    string lastUpdate = JHEMR.JHEMRGlobalLib.JHGlobal.DBInstance().ServerNowDateTime().ToString();
                    foreach (DataRow dr in dtSelect.Rows)
                    {
                        try
                        {
                            string fileFullName = dr["FILE_NAME_ALL"].ToString();
                            string strFileName = dr["FILE_NAME"].ToString();
                            string version = dr["VERSION_NAME"].ToString();
                            string sysCode = dr["SYS_CODE"].ToString();
                            FileStream fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);//图片转为数据流
                            byte[] imageByte = null;
                            imageByte = new byte[fs.Length];//定义数据长度
                            fs.Read(imageByte, 0, imageByte.Length);//读取数据流到数组
                            //System.IO.File.WriteAllBytes("c:\\JHServicesLib.dll", imageByte);
                            string strSql = "";
                            strSql = " UPDATE JHSYS_FILE_UPLOAD SET IS_CURVERSION=0 WHERE FILE_NAME='" + strFileName + "'";
                            JHEMRGlobalLib.JHGlobal.DBInstance().SQLExecute(strSql);
                            strSql = @" INSERT INTO JHSYS_FILE_UPLOAD
                        (FILE_NAME, UPLOAD_DATE, LAST_UPDATE, VERSION_NAME, FILE_BODY, USER_ID, FILE_LENGTH,HOSPITAL_NO,IS_CURVERSION ,DISTRIBUTE_NO,SYS_CODE) VALUES('" + strFileName + "'," + JHEMRGlobalLib.JHGlobal.DBInstance().ToDate(lastUpdate, false, false) + "," + JHEMRGlobalLib.JHGlobal.DBInstance().ToDate(lastUpdate, false, false) + ",'" + version + "',:fs,'" + JHEMRGlobalLib.JHGlobal.CurrentUser.userId + "'," + imageByte.Length.ToString() + ",'" + JHEMRGlobalLib.JHGlobal.CurrentUser.hospitalCode + "',0,0,'" + sysCode + "')";
                            fs.Close();
                            fs.Dispose();
                            JHEMRGlobalLib.JHGlobal.DBInstance().SQLInsertImage(strSql, imageByte);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }

                      
                    }
                    ArrayList arl = new ArrayList();
                    foreach (DataRow dr in dtSelect.Rows)
                    {
                        string strFileName = dr["FILE_NAME"].ToString();
                        string strSql = "";
                        strSql += "UPDATE JHSYS_FILE_UPLOAD   SET IS_CURVERSION=1,DISTRIBUTE_NO=" + iSerialNo + "  WHERE FILE_NAME='" + strFileName + "' AND LAST_UPDATE=" + JHEMRGlobalLib.JHGlobal.DBInstance().ToDate(lastUpdate, false, false) + " AND DISTRIBUTE_NO=0";
                        arl.Add(strSql);
                    }
                    
                    if (JHEMRGlobalLib.JHGlobal.DBInstance().SQLExecute(arl))
                    {
                        try
                        {
                            //删除大于三次的文件
                            string strSql = " SELECT * FROM (SELECT FILE_NAME,COUNT(*)  AA  FROM JHSYS_FILE_UPLOAD T  GROUP BY FILE_NAME) WHERE AA>3 ";
                            DataSet dst=JHEMRGlobalLib.JHGlobal.DBInstance().GetDataSet(strSql);
                            if (dst.Tables[0].Rows.Count > 0)
                            {
                                for(int i=0;i<dst.Tables[0].Rows.Count;i++)
                                {
                                    string strFileName = dst.Tables[0].Rows[i]["FILE_NAME"].ToString();
                                    string strSql1="SELECT *  FROM JHSYS_FILE_UPLOAD  WHERE FILE_NAME ='"+strFileName+"' ORDER BY  DISTRIBUTE_NO DESC";
                                    DataSet dst1=JHEMRGlobalLib.JHGlobal.DBInstance().GetDataSet(strSql1);
                                    for (int j = 0; j < dst1.Tables[0].Rows.Count; j++)
                                    {
                                        if (j < 3)
                                            continue;
                                        else
                                        {
                                            string strSql2 = "DELETE FROM JHSYS_FILE_UPLOAD WHERE FILE_NAME='" + strFileName + "' AND DISTRIBUTE_NO="+dst1.Tables[0].Rows[j]["DISTRIBUTE_NO"].ToString();
                                            JHEMRGlobalLib.JHGlobal.DBInstance().SQLExecute(strSql2);
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                        MessageBox.Show("发布成功！");
                        this.BindGrid();
                        this.dtSelect.Rows.Clear();
                    }
                    else
                    {
                        MessageBox.Show("发布失败！");
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            else
            {
                MessageBox.Show("请选择上传文件");
            }
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            Filter();
        }

        private void dgList_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo aa=this.dgList.CalcHitInfo(e.X, e.Y);
            int gg=aa.RowHandle;
            if (gg >= 0)
            {
                this.dgList.FocusedRowHandle = gg;
                this.dgList.SelectRow(gg);
                
            }
            else
                return;
            if (this.dgList.FocusedRowHandle >= 0)
            {
                DataRow dr=this.dgList.GetDataRow(this.dgList.FocusedRowHandle); 
                string isCur = dr["IS_CURVERSION"].ToString();
                if (isCur == "1")
                {
                    this.重新启用ToolStripMenuItem.Text = "停用当前版本";

                }
                else
                {
                    this.重新启用ToolStripMenuItem.Text = "启用当前版本";
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(this.grdList.PointToScreen(new Point(e.X,e.Y)));
            }
        }

        private void 重新启用ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dgList.FocusedRowHandle >= 0)
            {
                DataRow dr = this.dgList.GetDataRow(this.dgList.FocusedRowHandle);
                string fileName = dr["FILE_NAME"].ToString();
                string lastUpdate = dr["LAST_UPDATE"].ToString();
                string isCur = dr["IS_CURVERSION"].ToString();
                string iCurrentNo = dr["DISTRIBUTE_NO"].ToString();
                int iSerialNo = GetSerialNo();
                if(isCur=="0")
                {
                    if (JHEMRGlobalLib.MessageBoxs.ShowYesNo("是否重新启用" + fileName + ":" + lastUpdate + ",上传后客户端会下载此文件"))
                    {
                        string strSql = "";

                        strSql += " UPDATE JHSYS_FILE_UPLOAD SET IS_CURVERSION=0 WHERE FILE_NAME='" + fileName + "'";
                        JHEMRGlobalLib.JHGlobal.DBInstance().AddCommand(JHServicesLib.Provider.JHDBCommandType.SQLExecute, strSql);
                        strSql = @"UPDATE JHSYS_FILE_UPLOAD
 SET  LAST_UPDATE=" + JHEMRGlobalLib.JHGlobal.DBInstance().ServerNowFunctionName() + ", USER_ID='" + JHEMRGlobalLib.JHGlobal.CurrentUser.userId + "', hospital_no='" + JHEMRGlobalLib.JHGlobal.CurrentUser.hospitalCode + "',IS_CURVERSION=1, DISTRIBUTE_NO =" + iSerialNo + " where file_name='" + fileName + "' and DISTRIBUTE_NO=" + iCurrentNo + "";
                        JHEMRGlobalLib.JHGlobal.DBInstance().AddCommand(JHServicesLib.Provider.JHDBCommandType.SQLExecute, strSql);

                        if (JHEMRGlobalLib.JHGlobal.DBInstance().Commit())
                            MessageBox.Show("重新启用成功！");
                        else
                        {
                            MessageBox.Show("重新启用失败！");
                            return;
                        }
                    }
                    else
                        return;
                }
                else
                {
                    if (JHEMRGlobalLib.MessageBoxs.ShowYesNo("是否停用当前版本" + fileName + ":" + lastUpdate + ",停用后客户端不会及时更新"))
                    {
                        string strSql = "";
                        strSql = @" UPDATE JHSYS_FILE_UPLOAD SET IS_CURVERSION=0  WHERE FILE_NAME='" + fileName + "'  AND DISTRIBUTE_NO=" + iCurrentNo + "";
                        JHEMRGlobalLib.JHGlobal.DBInstance().AddCommand(JHServicesLib.Provider.JHDBCommandType.SQLExecute, strSql);

                        if (JHEMRGlobalLib.JHGlobal.DBInstance().Commit())
                            MessageBox.Show("停用成功！");
                        else
                        {
                            MessageBox.Show("停用失败！");
                            return;
                        }
                    }
                    else
                        return;

                }
                    BindGrid();
            }
        }

        string m_strInitialDirectory="";
      
        
        private void 导出选择版本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.dgList.FocusedRowHandle >= 0)
            {
                string fileName = this.dtAll.DefaultView[this.dgList.FocusedRowHandle]["FILE_NAME"].ToString();
                string strFileFilter = "文件 *.dll|*.dll|所有文件 *.*|*.*";
                SaveFileDialog openFile = new SaveFileDialog();
                openFile.InitialDirectory = m_strInitialDirectory;
                openFile.Filter = strFileFilter;
                openFile.FilterIndex = 1;
                openFile.FileName = fileName;
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    m_strInitialDirectory = openFile.FileName;
                    string strFileName = openFile.FileName;
                    string tempFileName=this.dtAll.DefaultView[this.dgList.FocusedRowHandle]["FILE_NAME"].ToString();
                    string distributeNo = this.dtAll.DefaultView[this.dgList.FocusedRowHandle]["DISTRIBUTE_NO"].ToString();
                    string strFileNameTemp = " SELECT  FILE_BODY  FROM JHSYS_FILE_UPLOAD WHERE  FILE_NAME='" + tempFileName + "' and DISTRIBUTE_NO=" + distributeNo;
                    DataSet dst= JHEMRGlobalLib.JHGlobal.DBInstance().GetDataSet(strFileNameTemp);
                    if (dst.Tables[0].Rows.Count > 0)
                    {
                        byte[] temp = (byte[])dst.Tables[0].Rows[0]["FILE_BODY"];

                        try
                        {
                            System.IO.File.WriteAllBytes(strFileName, temp);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);

                        }
                    }
                }
            }
        }

        private void repositoryItemButtonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataRow drItem = this.dgList.GetFocusedDataRow();
            if (drItem != null)
            {
                if (this.dgList.FocusedRowHandle >= 0)
                {
                    string fileName = this.dtAll.DefaultView[this.dgList.FocusedRowHandle]["FILE_NAME"].ToString();
                    string lastUpdate = this.dtAll.DefaultView[this.dgList.FocusedRowHandle]["LAST_UPDATE"].ToString();
                    string isCur = this.dtAll.DefaultView[this.dgList.FocusedRowHandle]["IS_CURVERSION"].ToString();
                    string curVer = this.dtAll.DefaultView[this.dgList.FocusedRowHandle]["DISTRIBUTE_NO"].ToString();
                    string strSql = "";

                    if (isCur == "1")
                    {
                        MessageBox.Show("当前版本不能删除！");
                        return;
                    }
                    int  strtemp = GetSerialNo()- 1;
                    if (strtemp.ToString() == curVer)
                    {
                        MessageBox.Show("最新发布序号不能删除！");
                        return;
                    }
                    strSql = @" DELETE FROM  JHSYS_FILE_UPLOAD   WHERE FILE_NAME='" + fileName + "' AND LAST_UPDATE=" + JHEMRGlobalLib.JHGlobal.DBInstance().ToDate(lastUpdate, false, false) + "";
                    JHEMRGlobalLib.JHGlobal.DBInstance().AddCommand(JHServicesLib.Provider.JHDBCommandType.SQLExecute, strSql);

                    if (JHEMRGlobalLib.JHGlobal.DBInstance().Commit())
                    {
                        this.BindGrid();
                        
                        MessageBox.Show("删除成功！");
                    }
                    else
                    {
                        MessageBox.Show("删除失败！");
                        return;
                    }
                }

            }
        }

        //删除
        private void repositoryItemButtonEdit2_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.gridView1.FocusedRowHandle >= 0)
            {
                string fileName = this.dtSelect.DefaultView[this.gridView1.FocusedRowHandle]["FILE_NAME"].ToString();
                this.dtSelect.Rows.Remove(dtSelect.Select(" FILE_NAME='" + fileName + "'")[0]);
            }
        }
    }
}
