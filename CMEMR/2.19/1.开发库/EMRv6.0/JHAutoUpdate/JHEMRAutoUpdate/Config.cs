using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.IO;
namespace JHEMR.JHEMRAutoUpdate
{
    public class Config
    {
        private static Config _config;



        public static Config Instance()
        {
            if (null == _config)
            {
                _config = new Config();
            }
            return _config;
        }

        string xmlPath = "C:\\jhemrLocal";
        public DataSet ds = new DataSet();
        /// <summary>
        /// 最大序号
        /// </summary>
        /// <returns></returns>
        public string MaxSerialNo
        {
            get
            {
                if (!File.Exists(xmlPath + "\\" + "AutoUpdate.xml"))
                {
                    ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("MaxSerialNo");
                    ds.Tables.Add(dt);
                    DataRow dr = dt.NewRow();
                    dr[0] = "0";
                    dt.Rows.Add(dr);
                    ds.WriteXml(xmlPath + "\\" + "AutoUpdate.xml", XmlWriteMode.WriteSchema);
                    return "0";
                }
                else
                {
                    ds.ReadXml(xmlPath + "\\" + "AutoUpdate.xml");
                    return ds.Tables[0].Rows[0][0].ToString();

                }

            }
            set
            {
                ds.Tables[0].Rows[0][0] = value;
                ds.WriteXml(xmlPath + "\\" + "AutoUpdate.xml", XmlWriteMode.WriteSchema);
            }
        }

        private string _ExecutePath="";
        /// <summary>
        /// 返回要启动的程序的路径和可执行文件名称
        /// </summary>
        /// <returns></returns>
        public  string  ExecutePath
        {
            get
            {
                if (_ExecutePath == "")
                {
                    _ExecutePath = ConfigurationManager.AppSettings["ProgramPath"].ToString();
                }
                
                 return _ExecutePath; 
            }
        }
        
        
        
        private string _ProgramName = "";
        /// <summary>
        /// 返回要启动的程序的路径和可执行文件名称
        /// </summary>
        /// <returns></returns>
        public string  ProgramName
        {
            get
            {
                if (_ProgramName == "")
                {
                    _ExecutePath = ConfigurationManager.AppSettings["ExecuteProgram"].ToString();
                }

                return _ExecutePath;
            }
        }


        public string GetfullProgramName()
        {


            return ExecutePath +"\\"+ ProgramName;

        }



        public BackgroundWorker worker = null;

    }
}
