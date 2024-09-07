using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using Word=Microsoft.Office.Interop.Word;
using System.Windows.Forms;

namespace ClassGenerate.Proc
{
    public class Generator
    {
        /// <summary>
        /// 表名前缀
        /// </summary>
        public string strPrefix;
        /// <summary>
        /// 名称空间
        /// </summary>
        public string strNameSpace;
        /// <summary>
        /// 生成路径
        /// </summary>
        public string path;
        /// <summary>
        /// 数据访问类前缀
        /// </summary>
        string strADO = "ADO";

        #region 写文件
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="fullpath">保存路径</param>
        /// <param name="content">要写的内容</param>
        /// <returns></returns>
        public bool Write(string fullpath, string content)
        {
            bool ret = false;
            try
            {
                //FileStream fs = File.Create(fullpath);//这样会直接把已有文件覆盖或替换
                //FileStream fs = File.Open(fullpath, FileMode.CreateNew); //如何有文件就不允许创建
                if(File.Exists(fullpath))
                    File.Delete(fullpath);
                using (FileStream fs = File.Open(fullpath, FileMode.CreateNew))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(content);
                    }
                    fs.Close();
                }
                ret = true;
            }
            catch (Exception e)
            {

                throw e;
            }
            return ret;
        } 
        #endregion

        ///////////////////////以下生成 Model
        #region 多表时,生成实例类文件
        /// <summary>
        /// 多表时,生成实例类文件
        /// </summary>
        /// <param name="ds"></param>
        public string ToModels(bool confirmWrite, List<string> tablesName)
        {
            StringBuilder ret = new StringBuilder();
            ProcTable pt = new ProcTable();
            ProcString ps = new ProcString();
            ps.SetTabs(strNameSpace); 
            DataSet dsColumnsDescription = pt.GetTablesColumnDecription(tablesName);

            foreach (string tableName in tablesName)
            {
                //if (tableName == " JHPIX_PAT_MASTER_INDEX")
                //    MessageBox.Show("JHPIX_PAT_MASTER_INDEX类的GetUpdateSQL方法特殊！不可自动生成");
                string strClass = "";
                TableInfo tableInfo = new TableInfo(strNameSpace, strPrefix, tableName);
                DataSet dsTableInfo = tableInfo.dsTableInfo;
                DataTable dtColumnType = dsTableInfo.Tables[1];
                //DataColumnCollection dtcols = dtColumnType.Columns;
                DataTable dtColumnsDescription = null;
                string tableDecription = pt.GetTableDesciption(tableName);
                
                for (int i = 0; i < dsColumnsDescription.Tables.Count; i++ )
                {
                    dtColumnsDescription = dsColumnsDescription.Tables[i].Copy();
                    
                    if (dtColumnsDescription.TableName == tableName)
                    {
                        dsColumnsDescription.Tables.RemoveAt(i);
                        break;
                    }
                }
                //缺少主键校验
                strClass = pt.GetTable(dtColumnType, dtColumnsDescription
                    , tableDecription
                    , strPrefix, strNameSpace);
                ret.AppendLine(strClass);
                #region 写文件
                if (confirmWrite)
                {
                    string tem = path + "/Model/";
                    if (!Directory.Exists(tem))
                    {
                        Directory.CreateDirectory(tem);
                    }
                    string className = ps.GetClassNameByTableName(tableName, strPrefix.Trim());//类名大写 
                    //className = ps.GetClassName(className);//lsj
                    string fullPath = tem + tableName + ".cs";

                    Write(fullPath, strClass);
                }
                #endregion
            }
            return ret.ToString();
        }

        public string ToIndexSQL(bool confirmWrite, List<string> tablesName, string HOSPITAL_NO)
        {
            StringBuilder ret = new StringBuilder();
            ProcTable pt = new ProcTable();
            ProcString ps = new ProcString();
            ps.SetTabs(strNameSpace);
            ret.AppendLine("declare index_count number(10);");
            ret.AppendLine("begin");
            int nReadTable = 0;
            string strPKTableNames = "";//有主键的表
            int nPKTable = 0;
            string strWithoutPKTableNames = "";//无主键的表
            int nWithoutPKTable = 0;
            string strNotReadTableNames = "";//没有更新的表

            foreach (string tableName in tablesName)
            {
                //if (tableName == " JHPIX_PAT_MASTER_INDEX")
                //    MessageBox.Show("JHPIX_PAT_MASTER_INDEX类的GetUpdateSQL方法特殊！不可自动生成");
                string strClass = "";
                

                string strPKTableName = "";//有主键的表
                string strWithoutPKTableName = "";//无主键的表
                string strNotReadTableName = "";//没有更新的表
                strClass = pt.GetIndexSQL(tableName, out strPKTableName, out strWithoutPKTableName
                    ,out strNotReadTableName, HOSPITAL_NO);
                if (!string.IsNullOrEmpty(strPKTableName))
                {
                    strPKTableNames += strPKTableName + "\r\n";
                    nPKTable++;
                }
                if (!string.IsNullOrEmpty(strWithoutPKTableName))
                {
                    strWithoutPKTableNames += strWithoutPKTableName + "\r\n";
                    nWithoutPKTable++;
                }
                
                if (!string.IsNullOrEmpty(strNotReadTableName))
                {
                    strNotReadTableNames += strNotReadTableName + "\r\n";
                }else
                {
                    nReadTable++;
                }
                ret.AppendLine(strClass);
                #region 写文件
                if (confirmWrite)
                {
                    string tem = path + "/Model/";
                    if (!Directory.Exists(tem))
                    {
                        Directory.CreateDirectory(tem);
                    }
                    string className = ps.GetClassNameByTableName(tableName, strPrefix.Trim());//类名大写 
                    //className = ps.GetClassName(className);//lsj
                    string fullPath = tem + tableName + ".sql";

                    Write(fullPath, strClass);
                }
                #endregion
            }
            ret.AppendLine("end;");
            ret.AppendLine("/* =========修改过" + nReadTable + "个表：==========\r\n");
            strPKTableNames = " --------其中有主键的表" + nPKTable + "个：\r\n" + strPKTableNames + "\r\n";
            strWithoutPKTableNames = " ---------无主键的表" + nWithoutPKTable + "个：\r\n" + strWithoutPKTableNames + "*/\r\n";
            strNotReadTableNames = "/* ===========未修改的表有" + (tablesName.Count- nReadTable) + "个：=============\r\n" + strNotReadTableNames + "*/\r\n";
            
            ret.AppendLine(strPKTableNames);
            ret.AppendLine(strWithoutPKTableNames);
            ret.AppendLine(strNotReadTableNames);
            return ret.ToString();
        }
        #endregion

        #region 单表时,生成实例类文件
        /// <summary>
        /// 单表时,生成实例类文件
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public string ToModel(bool confirmWrite, string tableName)
        {
            StringBuilder ret = new StringBuilder();
            ProcTable pt = new ProcTable();
            ProcString ps = new ProcString();
            ps.SetTabs(strNameSpace);
            DataTable dtColumnDescription = pt.GetTableColumnDecription(tableName);

            string strTableDescription = pt.GetTableDesciption(tableName);

            DataTable dtColumnType = pt.GetTableInfo(tableName).Tables[1];

            string strClassName = ps.GetClassNameByTableName(tableName, strPrefix);

            StringBuilder sbBody = new StringBuilder();
            TableInfo tableInfo = new TableInfo(strNameSpace, strPrefix, tableName);
            foreach (DataRow drType in dtColumnDescription.Rows)
            {
                string columnName = drType["ColumnName"].ToString();//列名
                string columnType = drType["ColumnDescription"].ToString();//列数据类型
                string firstLowerColumnName = "";//首字母小写,用于字段
                string firstUpperColumnName = "";//首字母大写,用于属性

                #region 以下为字段
                foreach (DataRow drDescription in dtColumnDescription.Rows)
                {
                    if (drDescription["ColumnName"].ToString() == columnName)
                    {
                        string strColumnDescription = drDescription["ColumnDescription"].ToString();
                        sbBody.AppendLine(ps.tabMember + "/// <summary>");
                        sbBody.AppendLine(ps.tabMember + "/// " + strColumnDescription);
                        sbBody.AppendLine(ps.tabMember + "/// <summary>");
                    }
                }
                firstLowerColumnName = ps.ConvertStringToUpperOrLower(columnName, false);
                sbBody.AppendLine(ps.tabMember + "private " + ps.ConvertType(columnType, columnName,tableInfo) + " " + firstLowerColumnName + ";");
                #endregion

                #region 以下为属性
                foreach (DataRow drDescription in dtColumnDescription.Rows)
                {
                    if (drDescription["ColumnName"].ToString() == columnName)
                    {
                        string strColumnDescription = drDescription["ColumnDescription"].ToString();
                        sbBody.AppendLine(ps.tabMember + "/// <summary>");
                        sbBody.AppendLine(ps.tabMember + "/// " + strColumnDescription);
                        sbBody.AppendLine(ps.tabMember + "/// <summary>");
                    }
                }
                firstUpperColumnName = ps.ConvertStringToUpperOrLower(columnName, true);
                sbBody.AppendLine(ps.tabMember + "public " + ps.ConvertType(columnType, columnName, tableInfo) + " " + firstUpperColumnName);
                sbBody.AppendLine(ps.tabMember + "{");
                sbBody.AppendLine(ps.tabLocalVar + "get{ return " + firstLowerColumnName + "; }");
                sbBody.AppendLine(ps.tabLocalVar + "set{ " + firstLowerColumnName + " = value; }");
                sbBody.AppendLine(ps.tabMember + "}");
                #endregion
                sbBody.AppendLine();
            }
            string strClass = ps.GetCLassByModel(strNameSpace, strPrefix, tableName , sbBody.ToString());
            ret.Append(strClass);

            #region 写文件
            if (confirmWrite)
            {
                string tem = path + "/Model/";
                if (!Directory.Exists(tem))
                {
                    Directory.CreateDirectory(tem);
                }
                string fullpath = tem + strClassName + ".cs";

                Write(fullpath, ret.ToString());
            } 
            #endregion

            return ret.ToString();
        }
        #endregion
        ///////////////////////以上生成 Model

        ////////////////////////////以下生成 数据访问层
        #region 生成单表数据访问层
        /// <summary>
        /// 生成单表数据访问层
        /// </summary>
        /// <param name="confirmWrite">是否生成文件</param>
        /// <returns></returns>
        public string ToDataAccess(bool confirmWrite, List<string> tablesName)
        {
            StringBuilder ret = new StringBuilder();
            ProcTable pt = new ProcTable();
            ProcString ps = new ProcString();
            ps.SetTabs(strNameSpace);
            ps.strImportNameSpace = @"using System;
using System.Data;
using System.Collections.Generic;
";
            foreach (string tableName in tablesName)
            {
                string strMember = @"IDBOperator idb =  DBOperator.GetInstance();";
                string strClassBody = ps.GetDataAccessClassBody(tableName, strPrefix, strNameSpace);
                string str = ps.GetCLassByModel(strNameSpace, strPrefix, tableName, strADO, strMember, strClassBody);
                ret.Append(str);

                #region 写文件
                if (confirmWrite)
                {
                    string tem = path + "/ADO/";
                    string strClassName = ps.GetClassNameByTableName(tableName, strPrefix);
                    if (!Directory.Exists(tem))
                    {
                        Directory.CreateDirectory(tem);
                    }

                    string fullpath = tem + strADO + strClassName + ".cs";

                    Write(fullpath, ret.ToString());
                }
                #endregion
            }
            return ret.ToString();
        }
        public string ToDataAccess_SQL(bool confirmWrite, List<string> tablesName)
        {
            StringBuilder ret = new StringBuilder();
            ProcTable pt = new ProcTable();
            ProcString ps = new ProcString();
            ps.SetTabs(strNameSpace);
            ps.strImportNameSpace = @"using System;
using System.Data;
using System.Collections.Generic;
using JHEMR.JHServicesLib.Client;
";
            string strMember = "";// @"JHDBClient DBClient = new JHDBClient();";
            foreach (string tableName in tablesName)
            {
                
                string strClassBody = ps.GetDataAccessClassBody_SQL(tableName, strPrefix, strNameSpace);
                string str = ps.GetCLassByModel_SQL(strNameSpace, strPrefix, tableName, strADO, strMember, strClassBody);
                ret.Append(str);

                #region 写文件
                if (confirmWrite)
                {
                    string tem = path + "/ADO/";
                    string strClassName = ps.GetClassNameByTableName(tableName, strPrefix);
                    if (!Directory.Exists(tem))
                    {
                        Directory.CreateDirectory(tem);
                    }

                    string fullpath = tem + strClassName + "_" + strADO + ".cs";

                    Write(fullpath, str.ToString());
                }
                #endregion
            }
            return ret.ToString();
        }
        #endregion

        #region 多表时,生成数据访问类
        /// <summary>
        /// 多表时,生成数据访问类
        /// </summary>
        /// <param name="ds"></param>
        public string ToDataAccessForTables(bool confirmWrite, List<string> tablesName)
        {
            StringBuilder ret = new StringBuilder();
            ProcTable pt = new ProcTable();
            ProcString ps = new ProcString();
            ps.SetTabs(strNameSpace);

            ps.strImportNameSpace = @"using System;
using System.Data;
using Orims.Common.Model;
using System.Data.SqlClient;
using System.Collections.Generic;
";
            strNameSpace = "Orims.DAL";
            //string strMember = @"IDBOperator idb =  DBOperator.GetInstance();";
            DataSet dsColumnsDescription = pt.GetTablesColumnDecription(tablesName);
            foreach (string tableName in tablesName)
            {
                
                //string strClass = "";
                TableInfo tableInfo = new TableInfo(strNameSpace, strPrefix, tableName);
                DataSet dsTableInfo = tableInfo.dsTableInfo;
                DataTable dtColumnType = dsTableInfo.Tables[1];
                DataTable dtColumnsDescription = null;
                string tableDecription = pt.GetTableDesciption(tableName);

                for (int i = 0; i < dsColumnsDescription.Tables.Count; i++)
                {
                    dtColumnsDescription = dsColumnsDescription.Tables[i];

                    if (dtColumnsDescription.TableName == tableName)
                    {
                        dsColumnsDescription.Tables.RemoveAt(i);
                        break;
                    }
                }
                //strClass = pt.GetTable(dtColumnType, dtColumnsDescription
                //    , tableDecription
                //    , strPrefix, strNameSpace);



                string strClassBody = ps.GetClassBody_AData(tableInfo, dtColumnType, strPrefix, strNameSpace);
                string str = ps.GetClassByModel_AData(strNameSpace, strPrefix, tableName, strADO, strClassBody);
                ret.Append(strClassBody);

                #region 写文件
                if (confirmWrite)
                {
                    string tem = path + "/ADO/";
                    string strClassName = ps.GetClassNameByTableName(tableName, strPrefix);
                    if (!Directory.Exists(tem))
                    {
                        Directory.CreateDirectory(tem);
                    }

                    string fullpath = tem + strADO + strClassName + ".cs";

                    Write(fullpath, str);
                }
                #endregion               
            }

            return ret.ToString();
        }
        public string ToFullManage(bool confirmWrite, List<string> tablesName)
        {
            StringBuilder ret = new StringBuilder();
            ProcTable pt = new ProcTable();
            ProcString ps = new ProcString();
            ps.SetTabs(strNameSpace);

            ps.strImportNameSpace = @"using System;
using System.Data;
using Orims.Common.Model;
using System.Data.SqlClient;
using System.Collections.Generic;
";
            strNameSpace = "Orims.DAL";
            DataSet dsColumnsDescription = pt.GetTablesColumnDecription(tablesName);
            foreach (string tableName in tablesName)
            {
                TableInfo tableInfo = new TableInfo(strNameSpace, strPrefix, tableName);
                DataSet dsTableInfo = tableInfo.dsTableInfo;
                DataTable dtColumnType = dsTableInfo.Tables[1];
                DataTable dtColumnsDescription = null;
                string tableDecription = pt.GetTableDesciption(tableName);
                for (int i = 0; i < dsColumnsDescription.Tables.Count; i++)
                {
                    dtColumnsDescription = dsColumnsDescription.Tables[i];

                    if (dtColumnsDescription.TableName == tableName)
                    {
                        dsColumnsDescription.Tables.RemoveAt(i);
                        break;
                    }
                }
                string strClassBody = ps.GetClassBody_FullManage(tableInfo, dtColumnType, strPrefix, strNameSpace);
                ret.Append(strClassBody);
                #region 写文件
                if (confirmWrite)
                {
                    string tem = path + "/ADO/";
                    string strClassName = ps.GetClassNameByTableName(tableName, strPrefix);
                    if (!Directory.Exists(tem))
                    {
                        Directory.CreateDirectory(tem);
                    }

                    string fullpath = tem + strADO + strClassName + ".cs";

                    Write(fullpath, strClassBody);
                }
                #endregion
            }
            return ret.ToString();
        }
        #endregion


        public string ToCache(bool confirmWrite, List<string> tablesName)
        {
            StringBuilder ret = new StringBuilder();
            ProcTable pt = new ProcTable();
            ProcString ps = new ProcString();
            ps.SetTabs(strNameSpace);

            strNameSpace = "Orims.DAL.Cache";
            DataSet dsColumnsDescription = pt.GetTablesColumnDecription(tablesName);
            foreach (string tableName in tablesName)
            {
                TableInfo tableInfo = new TableInfo(strNameSpace, strPrefix, tableName);
                DataSet dsTableInfo = tableInfo.dsTableInfo;
                DataTable dtColumnType = dsTableInfo.Tables[1];
                DataTable dtColumnsDescription = null;
                string tableDecription = pt.GetTableDesciption(tableName);

                for (int i = 0; i < dsColumnsDescription.Tables.Count; i++)
                {
                    dtColumnsDescription = dsColumnsDescription.Tables[i];

                    if (dtColumnsDescription.TableName == tableName)
                    {
                        dsColumnsDescription.Tables.RemoveAt(i);
                        break;
                    }
                }



                string strClassBody = ps.GetClassBody_ACache(tableInfo, dtColumnType, strPrefix, strNameSpace);
                string str = ps.GetClassByModel_ACache(strNameSpace, strPrefix, tableName, strADO, strClassBody);
                ret.Append(strClassBody);

                #region 写文件
                if (confirmWrite)
                {
                    string tem = path + "/Cache/";
                    //string strClassName = ps.GetACacheClassNameByTableName(tableName, strPrefix);
                    string strClassName = ps.GetClassNameByTableName(tableName, strPrefix);
                    if (!Directory.Exists(tem))
                    {
                        Directory.CreateDirectory(tem);
                    }

                    string fullpath = tem + strADO + strClassName + ".cs";

                    Write(fullpath, str);
                }
                #endregion
            }

            return ret.ToString();
        }
        public string ToFullCache(bool confirmWrite, List<string> tablesName)
        {
            StringBuilder ret = new StringBuilder();
            ProcTable pt = new ProcTable();
            ProcString ps = new ProcString();
            ps.SetTabs(strNameSpace);

            strNameSpace = "Orims.DAL.Cache";
            DataSet dsColumnsDescription = pt.GetTablesColumnDecription(tablesName);
            foreach (string tableName in tablesName)
            {
                TableInfo tableInfo = new TableInfo(strNameSpace, strPrefix, tableName);
                DataSet dsTableInfo = tableInfo.dsTableInfo;
                DataTable dtColumnType = dsTableInfo.Tables[1];
                DataTable dtColumnsDescription = null;
                string tableDecription = pt.GetTableDesciption(tableName);

                for (int i = 0; i < dsColumnsDescription.Tables.Count; i++)
                {
                    dtColumnsDescription = dsColumnsDescription.Tables[i];

                    if (dtColumnsDescription.TableName == tableName)
                    {
                        dsColumnsDescription.Tables.RemoveAt(i);
                        break;
                    }
                }



                string strClassBody = ps.GetClassBody_FullCache(tableInfo, dtColumnType, strPrefix, strNameSpace);
                string str = ps.GetClassByModel_FullCache(strNameSpace, strPrefix, tableName, strADO, strClassBody);
                ret.Append(strClassBody);

                #region 写文件
                if (confirmWrite)
                {
                    string tem = path + "/Cache/";
                    //string strClassName = ps.GetFullCacheClassNameByTableName(tableName, strPrefix);
                    string strClassName = ps.GetClassNameByTableName(tableName, strPrefix);
                    if (!Directory.Exists(tem))
                    {
                        Directory.CreateDirectory(tem);
                    }

                    string fullpath = tem + strADO + strClassName + ".cs";

                    Write(fullpath, str);
                }
                #endregion
            }

            return ret.ToString();
        }

        public string ToProc(bool confirmWrite, List<string> tablesName)
        {
            StringBuilder ret = new StringBuilder();
            ProcTable pt = new ProcTable();
            ProcString ps = new ProcString();
            ps.SetTabs(strNameSpace);

            strNameSpace = "Orims.DAL";
            DataSet dsColumnsDescription = pt.GetTablesColumnDecription(tablesName);
            foreach (string tableName in tablesName)
            {
                TableInfo tableInfo = new TableInfo(strNameSpace, strPrefix, tableName);
                DataSet dsTableInfo = tableInfo.dsTableInfo;
                DataTable dtColumnType = dsTableInfo.Tables[1];
                DataTable dtColumnsDescription = null;
                string tableDecription = pt.GetTableDesciption(tableName);

                for (int i = 0; i < dsColumnsDescription.Tables.Count; i++)
                {
                    dtColumnsDescription = dsColumnsDescription.Tables[i];

                    if (dtColumnsDescription.TableName == tableName)
                    {
                        dsColumnsDescription.Tables.RemoveAt(i);
                        break;
                    }
                }



                String strClassBody = ps.GetSQL_Proc(tableInfo, dtColumnType, strPrefix, strNameSpace);
                //string str = ps.GetClassByModel_FullCache(strNameSpace, strPrefix, tableName, strADO, strClassBody);
                ret.Append(strClassBody);

                #region 写文件
                if (confirmWrite)
                {
                    string tem = path + "/Proc/";
                    //string strClassName = ps.GetFullCacheClassNameByTableName(tableName, strPrefix);
                    string strClassName = ps.GetClassNameByTableName(tableName, strPrefix);
                    if (!Directory.Exists(tem))
                    {
                        Directory.CreateDirectory(tem);
                    }

                    string fullpath = tem + strADO + strClassName + ".sql";

                    Write(fullpath, strClassBody);
                }
                #endregion
            }

            return ret.ToString();
        }
        ////////////////////////////以上生成 数据访问层

        /// <summary>
        /// 生成数据库文档
        /// </summary>
        /// <param name="tablesName"></param>
        public void ToWord(List<string> tablesName)
        {
            ProcTable pt = new ProcTable();
            DataSet dsColumnsDescription = pt.GetTablesColumnDecription(tablesName);

            string fullPath = path + "/数据库文档.doc";
            ProcWord word = new ProcWord();
            word.FileName = fullPath;

            foreach (string tableName in tablesName)
            {
                TableInfo tableInfo = new TableInfo(strNameSpace, strPrefix, tableName);
                DataSet dsTableInfo = tableInfo.dsTableInfo;//0表名  1列类型  2标识列
                DataTable dtColumnType = tableInfo.ColumnType;//列名0 类型1 长度3 可空6
                DataTable dtIdentity = tableInfo.dsTableInfo.Tables[2];//列名0 seed 1   increment 2
                string[] keyColumns = tableInfo.KeyColumns;//主键列
                DataTable dtColumnsDescription = pt.GetTableColumnDecription(tableName);//列名0 说明1
                
                //默认值 
                DataTable dtConstraint = null;//constraint_type(DEFAULT)   constraint_keys默认值
                //表关系
                DataTable dtRelation = null;
                if (tableInfo.dsTableInfo.Tables.Count > 6)
                {
                    dtConstraint = tableInfo.dsTableInfo.Tables[6];//constraint_type(DEFAULT)   constraint_keys默认值
                    dtRelation = tableInfo.dsTableInfo.Tables[6];//column 0
                }

                word.WriteText("表名:" + tableName + "  [" + tableInfo.tableDescription + "]");
                int rows = dtColumnType.Rows.Count;
                word.AddParagraph();
                word.AddTable(rows+1, 7);
                #region 添加列头
                word.AddTableContent(1, 1, "列名", 1, Microsoft.Office.Interop.Word.WdColor.wdColorBlack);
                word.AddTableContent(1, 2, "类型(长度)", 1, Microsoft.Office.Interop.Word.WdColor.wdColorBlack);
                word.AddTableContent(1, 3, "可空", 1, Microsoft.Office.Interop.Word.WdColor.wdColorBlack);
                word.AddTableContent(1, 4, "默认值", 1, Microsoft.Office.Interop.Word.WdColor.wdColorBlack);
                word.AddTableContent(1, 5, "标识", 1, Microsoft.Office.Interop.Word.WdColor.wdColorBlack);
                word.AddTableContent(1, 6, "主键", 1, Microsoft.Office.Interop.Word.WdColor.wdColorBlack);
                word.AddTableContent(1, 7, "列说明", 1, Microsoft.Office.Interop.Word.WdColor.wdColorBlack);
                #endregion
                ProcString ps = new ProcString();
                string strColName = "";
                string strIdentity = "";
                string strPrimary = "";
                string strDefault = "";
                string strColDescription = "";
                string tem = "";
                int j = 1;
                for (int i = 0; i < rows; i++)
                {
                    j ++ ;
                    //列名
                    strColName = dtColumnType.Rows[i][0].ToString();
                    word.AddTableContent(j, 1, strColName);
                    //列类型(长度)
                    word.AddTableContent(j, 2, dtColumnType.Rows[i][1].ToString() + "(" + dtColumnType.Rows[i][3].ToString() + ")");
                    //可空
                    word.AddTableContent(j, 3, dtColumnType.Rows[i][6].ToString());
                    //默认值
                    #region 默认值
                    if (null != dtConstraint && dtConstraint.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtConstraint.Rows)
                        {
                            tem = dr[0].ToString();
                            if (tem.IndexOf("DEFAULT") >= 0)
                            {
                                tem = tem.Substring(18);
                                if (tem == strColName)
                                {
                                    strDefault = dr["constraint_keys"].ToString();
                                    break;
                                }
                            }
                            strDefault = "";
                        }
                    } 
                    #endregion
                    word.AddTableContent(j, 4, strDefault);
                    //标识
                    #region 标识
                    if (null != dtIdentity && dtIdentity.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtIdentity.Rows)
                        {
                            if (strColName == dr[0].ToString())
                            {
                                strIdentity = "yes(" + dr[1].ToString() + "," + dr[2].ToString() + ")";
                                break;
                            }
                            strIdentity = "";
                        }
                    } 
                    #endregion
                    word.AddTableContent(j, 5, strIdentity);
                    //主键
                    strPrimary = ps.IsContains(strColName, tableInfo.KeyColumns) ? "yes" : "";
                    word.AddTableContent(j, 6, strPrimary);
                    //列说明
                    #region 列说明
                    if (null != dtColumnsDescription && dtColumnsDescription.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtColumnsDescription.Rows)
                        {
                            if (strColName == dr[0].ToString())
                            {
                                strColDescription = dr[1].ToString();
                                break;
                            }
                            strColDescription = "";
                        }
                    } 
                    #endregion
                    word.AddTableContent(j,7, strColDescription);
                }

                #region 表关系
                if (dtRelation.Rows.Count > 0)
                {
                    tem = "";
                    foreach (DataRow dr in dtRelation.Rows)
                    {
                        if (dr["status_enabled"].ToString() == "Enabled")
                        {
                            tem += dr["constraint_name"].ToString() + "[" + dr["constraint_keys"].ToString() + "]" + " , ";
                        }
                    }
                    if (tem.EndsWith(" , "))
                    {
                        tem = tem.Substring(0, tem.Length - 3);

                        word.WriteText("表关系:" + tem);
                    }
                }
                #endregion

                word.AddParagraph();
                word.AddParagraph();
            }//end foreach
            word.SaveAs();
        }



    }
}
