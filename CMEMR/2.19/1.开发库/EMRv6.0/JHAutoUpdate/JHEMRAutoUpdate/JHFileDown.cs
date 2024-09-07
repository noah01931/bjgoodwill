using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;
using System.IO;
using JHEMR.JHEMRAutoUpdate.JHAutoUpdateClient;

namespace JHEMR.JHEMRAutoUpdate
{
    public class JHFileDown
    {
        public void file_Down(BackgroundWorker bg)
        {
            int iMaxCurNo = 0;
            try
            {
                bg.ReportProgress(0, "开始下载最新版本");
                DataTable dtDown = new DataTable();
                string lastCurVerSion = Config.Instance().MaxSerialNo;
                //调用服务
                using (JHAutoUpdateServiceClient client = new JHAutoUpdateServiceClient())
                {
                    //string str = client.getStr("开始调用远程服务");//测试
                    bg.ReportProgress(0, "开始调用远程服务");
                    DataSet ds = client.get_All_UploadFile(out iMaxCurNo, lastCurVerSion);
                    if (ds.Tables.Count > 0)
                    {
                        dtDown = ds.Tables[0];
                    }
                    bg.ReportProgress(0, "文件下载成功");
                }
                foreach (DataRow dr in dtDown.Rows)
                {
                    string path = dr["PATH"].ToString();
                    byte[] temp = (byte[])dr["FILE_BODY"];
                    string fileName = dr["FILE_NAME"].ToString();
                    bg.ReportProgress(0, "开始更新" + fileName);
                    //系统文件
                    string strFileName = "";
                    if (path.Substring(0, 1) != "\\")
                    {
                        strFileName = path + fileName;
                        //System.IO.File.WriteAllBytes(path, temp);
                    }
                    else
                    {
                        strFileName = Config.Instance().ExecutePath + path + fileName;
                    }
                    if (!Directory.Exists(Config.Instance().ExecutePath + path))
                    {
                        string newPath = strFileName.Substring(0, strFileName.LastIndexOf("\\"));
                        Directory.CreateDirectory(newPath);
                    }
                    System.IO.File.WriteAllBytes(strFileName, temp);
                }
                bg.ReportProgress(0, "文件更新完毕");
                Config.Instance().MaxSerialNo = iMaxCurNo.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + "请重新启动下载最新版本！");
                //Process.GetCurrentProcess().Kill();
            }
        }
    }
}
