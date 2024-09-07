using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ClassGenerate.Proc
{
    public class ProcString
    {
        #region strNameSpace 默认名称空间的引用
        /// <summary>
        /// 默认名称空间的引用
        /// </summary>
        public string strImportNameSpace = @"using System;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
using JHEMR.JHCommonLib.DBFunction;
";//lsj
//        using JHEMR.JHServicesLib.Provider;
//using JHEMR.JHServicesLib.Client;
        public string strImportNameSpace_AData = @"using System;
using System.Data;
//using Orims.Common.Model;
using System.Data.SqlClient;
using System.Collections.Generic;
";
        public string strImportNameSpace_ACache = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Orims.Common.Model;
";
        public string strImportNameSpace_FullCache = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Orims.Config;
//using Orims.Common.Model;
using System.Data.SqlClient;
using Orims.Exception;
";
        #endregion

        #region 数据库类型到C#类型的转换
        /// <summary>
        /// 数据库类型到C#类型的转换
        /// lsj
        /// </summary>
        /// <param name="dbtype">数据库中的类型</param>
        /// <returns></returns>
        public string ConvertType(string dbtype,string strSpecial)
        {
            string ret = "String";
            return ret;
        }
        public string ConvertType(string dbtype, string columnName, TableInfo tableInfo)
        {
            string ret = "String";
            return ret;
        }
        /// <summary>
        /// 根据数据库类型 转换其默认值
        /// lsj
        /// </summary>
        /// <param name="dbtype">数据库中的类型</param>
        /// <returns></returns>
        public string ConvertDefaultValue(string dbtype, string strSpecial)
        {
            string ret = "\"\"";
            return ret;
        }

        public string ConvertSqlDbType(string dbtype)
        {
            string ret = "VarChar";
            switch (dbtype.ToLower())
            {
                case "bit":
                case "int":
                case "money":
                case "decimal":
                case "number"://oracle
                case "numeric":
                case "float":
                    ret = ConvertStringToUpperOrLower(dbtype, true);
                    break;
                case "date"://oracle
                case "datetime":
                    ret = "DateTime";
                    break;
                case "varchar":
                    ret = "VarChar";
                    break;
                case "smallint":
                    ret = "SmallInt";
                    break;
                case "tinyint":
                    ret = "TinyInt";
                    break;
                case "bigint":
                    ret = "BigInt";
                    break;
                case "smallmoney":
                    ret = "SmallMoney";
                    break;
                case "smalldatetime":
                    ret = "SmallDateTime";
                    break;
                default:
                    ret = "VarChar";
                    break;
            }
            return ret;
        }
        /// <summary>
        /// 根据表名获取类名
        /// lsj ---ORIMS3数据表名的命名规则前面加  S_ ,应在生成类时删除
        /// </summary>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        public string GetClassName(string strTableName)
        {
            //string strClass = strTableName.Remove(0, 2);
            string strClass = strTableName;
            switch (strClass)
            {
                case "Drug":
                case "Disease":
                case "DiseaseType":
                case "Operation":                
                case "Event":
                case "InHospital":
                case "Model":
                    strClass += "Info";
                    break;
                case "OpeType":
                    strClass = "OperationTypeInfo";
                    break;
                default:
                    
                    break;
            }
            return strClass;
        }
        public string GetClassName_ACache(string strTableName)
        {
            return "A" + strTableName + "Cache";
        }
        public string GetClassName_FullCache(string strTableName)
        {
           return strTableName + "FullCache";
        }
        public string GetClassName_AData(string strTableName)
        {
            return "A" + strTableName + "Manager";
        }
        public string GetClassName_FullData(string strTableName)
        {
            return strTableName + "FullManager";
        }
        /// <summary>
        /// 返回转换类型的字段  Convert.ToInt32(feild);
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="feild"></param>
        /// <returns></returns>
        public string ConvertField(string dbtype, string feild)
        {
            string ret = "Convert.ToString(" + feild + ")";
            return ret;
        }
        #endregion

        #region 把字符串首字母转换为大写
        /// <summary>
        /// 把字符串首字母转换为大写
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <param name="isUpper">true:大写,false:小写</param>
        /// <returns></returns>
        public string ConvertStringToUpperOrLower(string str, bool isUpper)
        {
            string ret = "";
            if (isUpper)
            {// 把propName变成首字母为大写的字符串
                //ret = str.Substring(0, 1).ToUpper() + str.Substring(1);
                ret = str.ToUpper();
            }
            else
            {
                //ret = str.Substring(0, 1).ToLower() + str.Substring(1);
                ret = str.ToLower() + "_";
            }
            return ret;
        }
        #endregion

        #region 测试字符串是否包含 在集体中
        /// <summary>
        /// 测试字符串是否包含 在集体中
        /// </summary>
        /// <param name="test">待测试列</param>
        /// <param name="arr">找到集合</param>
        /// <returns></returns>
        public bool IsContains(string test, string[] arr)
        {
            foreach (string column in arr)
            {
                if (test == column)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 获取tab占位字符串
        /// <summary>
        /// 获取tab占位字符串
        /// </summary>
        /// <param name="i">个数</param>
        /// <returns></returns>
        public string GetTabs(int i)
        {
            StringBuilder tabs = new StringBuilder();
            string strTab = "   ";//一个tab占位
            for (int j = 0; j < i; j++)
            {
                tabs.Append(strTab);
            }
            return tabs.ToString();
        }
        #endregion

        #region 设置带名称空间时,类等tab占位
        /// <summary>
        /// 类级tab
        /// </summary>
        public string tabClass = "";
        /// <summary>
        /// 类成员tab
        /// </summary>
        public string tabMember = "";
        /// <summary>
        /// 局部变量tab
        /// </summary>
        public string tabLocalVar = "";
        /// <summary>
        /// 一级if for foreach内局部变量
        /// </summary>
        public string tabIfLocalVarTop1 = "";
        /// <summary>
        /// 二级if for foreach内局部变量
        /// </summary>
        public string tabIfLocalVarTop2 = "";
        public string tabIfLocalVarTopNext = "";
        public string tabIfLocalVarTopNextMore = "";
        public string tabIfLocalVarTopNextMoreMore = "";
        public string tabIfLocalVarTopNextMoreMoreMore = "";
        /// <summary>
        /// 设置名称空间,类等tab占位
        /// </summary>
        public void SetTabs(string txtNameSpace)
        {
            int tab = 0;
            int increase = 0;
            if (txtNameSpace.Trim().Length > 0)
            {
                tabClass = GetTabs(tab + (++increase));
                tabMember = GetTabs(tab + (++increase));
                tabLocalVar = GetTabs(tab + (++increase));
                tabIfLocalVarTop1 = GetTabs(tab + (++increase));
                tabIfLocalVarTop2 = GetTabs(tab + (++increase));
                tabIfLocalVarTopNext = GetTabs(tab + (++increase));
                tabIfLocalVarTopNextMore = GetTabs(tab + (++increase));
                tabIfLocalVarTopNextMoreMore = GetTabs(tab + (++increase));
                tabIfLocalVarTopNextMoreMoreMore = GetTabs(tab + (++increase));
            }
            else
            {
                increase = -1;
                tabClass = GetTabs(tab + (++increase));
                tabMember = GetTabs(tab + (++increase));
                tabLocalVar = GetTabs(tab + (++increase));
                tabIfLocalVarTop1 = GetTabs(tab + (++increase));
                tabIfLocalVarTop2 = GetTabs(tab + (++increase));
            }
        }
        #endregion

        #region 类名处理(去前缀,首字母大写)
        /// <summary>
        /// 类名处理(去前缀,首字母大写)
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strPrefix">前缀</param>
        /// <returns></returns>
        public string GetClassNameByTableName(string strTableName, string txtPrefix)
        {
            
            if (!string.IsNullOrEmpty(txtPrefix) && strTableName.StartsWith(txtPrefix))
            {//有前缀,去除前缀
                strTableName = strTableName.Remove(0, txtPrefix.Length);
            }
            strTableName = GetClassName(strTableName);
            //转成大写
            strTableName = ConvertStringToUpperOrLower(strTableName, true);
            return strTableName;
        }
        #endregion

        #region 获取表对应的类的对象(去前缀,首字母小写)
        /// <summary>
        /// 获取表对应的类的对象(去前缀,首字母小写)
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="txtPrefix">表名前缀</param>
        /// <returns></returns>
        public string GetObjectByTableName(string strTableName, string txtPrefix)
        {
            if (!string.IsNullOrEmpty(txtPrefix) && strTableName.StartsWith(txtPrefix))
            {//有前缀,去除前缀
                strTableName = strTableName.Remove(0, txtPrefix.Length);
            }
            //转成小写
            strTableName = ConvertStringToUpperOrLower(strTableName, false);
            return strTableName;
        } 
        #endregion

        #region 生成Class字符串
        /// <summary>
        /// 生成Class字符串
        /// </summary>
        /// <param name="strNameSpace">窗空间</param>
        /// <param name="strPrefix">表前缀</param>
        /// <param name="tableName">表名</param>
        /// <param name="strTableDescription">表描述</param>
        /// <param name="strClassBody">要加入的类主体</param>
        /// <returns></returns>
        public string GetCLassByModel(string strNameSpace, string strPrefix
            , string tableName
            , string strClassBody)
        {
            return GetCLassByModel(strNameSpace, strPrefix, tableName,"", "", strClassBody);
        }  
        #endregion

        #region 生成Class(带成员)字符串
        /// <summary>
        /// 生成带Class(带成员)字符串
        /// 实体类
        /// </summary>
        /// <param name="strNameSpace">窗空间</param>
        /// <param name="strPrefix">表前缀</param>
        /// <param name="tableName">表名</param>
        /// <param name="strTableDescription">表描述</param>
        /// <param name="strMember">要加入的类成员</param>
        /// <param name="strClassBody">要加入的类主体</param>
        /// <returns></returns>
        public string GetCLassByModel(string strNameSpace, string strPrefix
            , string tableName
            , string strClassPrefix
            , string strMember
            , string strClassBody)
        {
            StringBuilder ret = new StringBuilder();

            //类名大写
            string strClassName = GetClassNameByTableName(tableName, strPrefix.Trim());
            
            ProcTable pt = new ProcTable();
            string strTableDescription = pt.GetTableDesciption(tableName);
            SetTabs(strNameSpace.Trim());

            #region 写名称空间 {
            ret.AppendLine(strImportNameSpace);
            if (strNameSpace.Trim().Length > 0)
            {
                ret.AppendLine("namespace " + strNameSpace.Trim());
                ret.AppendLine("{");
            }
            #endregion

            #region 写类描述
            if (strTableDescription.Length > 0)
            {//有表描述,
                ret.AppendLine(tabClass + "/// <summary>");
                ret.AppendLine(tabClass + "/// " + strTableDescription);
                ret.AppendLine(tabClass + "/// <summary>");
            }
            #endregion
            //strClassName = GetClassName(strClassName);//lsj
            strClassName += " : JHATable, INotifyPropertyChanged, ICloneable";//lsj
            ret.AppendLine(tabClass + "[DataContract]");//lsj
            ret.AppendLine(tabClass + "public class " + strClassPrefix + strClassName);
            ret.AppendLine(tabClass + "{");
            //strMember = "JHDBClient dbClient = new JHDBClient();";
            if (!string.IsNullOrEmpty(strMember)) { ret.AppendLine(tabMember + strMember); }
            ret.Append(strClassBody);
            ret.AppendLine(tabClass + "}");
            #region 写名称空间 }
            if (strNameSpace.Trim().Length > 0)
            {
                ret.AppendLine("}");
            }
            #endregion
            return ret.ToString();
        }
        public string GetCLassByModel_SQL(string strNameSpace, string strPrefix
            , string tableName
            , string strClassPrefix
            , string strMember
            , string strClassBody)
        {
            StringBuilder ret = new StringBuilder();

            //类名大写
            string strClassName = GetClassNameByTableName(tableName, strPrefix.Trim());

            ProcTable pt = new ProcTable();
            string strTableDescription = pt.GetTableDesciption(tableName);
            SetTabs(strNameSpace.Trim());

            #region 写名称空间 {
            ret.AppendLine(strImportNameSpace);
            if (strNameSpace.Trim().Length > 0)
            {
                ret.AppendLine("namespace " + strNameSpace.Trim());
                ret.AppendLine("{");
            }
            #endregion

            #region 写类描述
            if (strTableDescription.Length > 0)
            {//有表描述,
                ret.AppendLine(tabClass + "/// <summary>");
                ret.AppendLine(tabClass + "/// " + strTableDescription);
                ret.AppendLine(tabClass + "/// <summary>");
            }
            #endregion
            ret.AppendLine(tabClass + "public class " + strClassName +"_"+ strClassPrefix );
            ret.AppendLine(tabClass + "{");
            if (!string.IsNullOrEmpty(strMember)) { ret.AppendLine(tabMember + strMember); }
            ret.Append(strClassBody);
            ret.AppendLine(tabClass + "}");
            #region 写名称空间 }
            if (strNameSpace.Trim().Length > 0)
            {
                ret.AppendLine("}");
            }
            #endregion
            return ret.ToString();
        }
        #endregion

        
        #region 生成访问层类的主体
        /// <summary>
        /// 生成访问层类的主体
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strPrefix">表名前缀(要处理的)</param>
        /// <param name="strNamespace">名称空间</param>
        /// <returns></returns>
        public string GetDataAccessClassBody(string strTableName
            , string strPrefix
            , string strNamespace)
        {
            StringBuilder ret = new StringBuilder();

            ProcString ps = new ProcString();
            ps.SetTabs(strNamespace);

            TableInfo tableInfo = new TableInfo(strNamespace, strPrefix, strTableName);
            StringBuilder sbBody = new StringBuilder();
            string className = tableInfo.ClassName;
            string classObject = tableInfo.ClassObject;
            string tableDescription = string.IsNullOrEmpty(tableInfo.tableDescription) ? "" : tableInfo.tableDescription + " ";

            string strAddOrUpdateParameters = tableInfo.GetParamaters(ColumnsType.NonIdentityColumns, "idb",classObject);
            #region public ind Add()
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 添加" + tableDescription + className + "对象(即:一条记录");
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "public int Add(" + className + " " + classObject + ")");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "string sql = \"INSERT INTO " + strTableName + " (" + tableInfo.GetInsertFields("") + ") VALUES (" + tableInfo.GetInsertFields("@") + ")\";");
            sbBody.AppendLine(strAddOrUpdateParameters);
            sbBody.AppendLine(ps.tabLocalVar + "return idb.ExeCmd(sql);");
            sbBody.AppendLine(ps.tabMember + "}");
            #endregion

            #region public ind Update()
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 更新" + tableDescription + className + "对象(即:一条记录");
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "public int Update(" + className + " " + classObject + ")");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "string sql = \"UPDATE " + strTableName + " SET " + tableInfo.GetUpdateFields() + " WHERE 1=1 \";");
            sbBody.AppendLine(strAddOrUpdateParameters);
            sbBody.AppendLine(ps.tabLocalVar + "return idb.ExeCmd(sql);");
            sbBody.AppendLine(ps.tabMember + "}");
            #endregion

            string strKeyParameters = tableInfo.GetParamaters(ColumnsType.KeyColumns, "idb");
            #region public ind Delete(string var1,.....)
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 删除" + tableDescription + className + "对象(即:一条记录");
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "public int Delete(" +tableInfo.GetFunctionParams()+ ")");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "string sql = \"DELETE " + strTableName + " WHERE 1=1 " + tableInfo.GetDeleteFields() + " \";");
            sbBody.AppendLine(strKeyParameters);
            sbBody.AppendLine(ps.tabLocalVar + "return idb.ExeCmd(sql);");
            sbBody.AppendLine(ps.tabMember + "}");
            #endregion

            strKeyParameters = tableInfo.GetParamaters(ColumnsType.KeyColumns, "idb");
            #region public ind GetByKey(string var1,.....)
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 获取指定的" + tableDescription + className + "对象(即:一条记录");
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "public List<"+className+"> GetByKey(" +tableInfo.GetFunctionParams()+")");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "List<"+className+"> ret = new List<"+className+">();");
            sbBody.AppendLine(ps.tabLocalVar + "string sql = \"SELECT  " + tableInfo.AllColumnsString + " FROM " + strTableName + " WHERE 1=1 " + tableInfo.GetWhereFields(ColumnsType.KeyColumns, " AND ") + " \";");
            sbBody.AppendLine(strKeyParameters);
            sbBody.AppendLine(ps.tabLocalVar + "DataTable dt = idb.ReturnDataTable(sql);");
            sbBody.AppendLine(ps.tabLocalVar + "foreach (DataRow dr in dt.Rows)");
            sbBody.AppendLine(ps.tabLocalVar + "{");  
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + className +" "+ classObject + " = new " + className + "();");
            string[] all = tableInfo.AllColumns;
            foreach (string field in all)
            {
                sbBody.AppendLine(ps.tabIfLocalVarTop1 + tableInfo.GetPropertyString(field));
            }
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "ret.Add(" + classObject + ");");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabLocalVar + "return ret;");
            sbBody.AppendLine(ps.tabMember + "}");
            #endregion

            //strKeyParameters = tableInfo.GetParamaters(ColumnsType.KeyColumns, "idb", classObject);
            #region public ind GetAll()
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 获取所有的" + tableDescription + className + "对象(即:一条记录");
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "public List<" + className + "> GetAll()");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "List<" + className + "> ret = new List<" + className + ">();");
            sbBody.AppendLine(ps.tabLocalVar + "string sql = \"SELECT  " + tableInfo.AllColumnsString + " FROM " + strTableName + " \";");
            //sbBody.AppendLine(strKeyParameters);
            sbBody.AppendLine(ps.tabLocalVar + "DataTable dt = idb.ReturnDataTable(sql);");
            sbBody.AppendLine(ps.tabLocalVar + "foreach (DataRow dr in dt.Rows)");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + className + " " + classObject + " = new " + className + "();");
            //string[] all = tableInfo.AllColumns;
            foreach (string field in all)
            {
                sbBody.AppendLine(ps.tabIfLocalVarTop1 + tableInfo.GetPropertyString(field));
            }
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "ret.Add(" + classObject + ");");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabLocalVar + "return ret;");
            sbBody.AppendLine(ps.tabMember + "}");
            #endregion

            ret.Append(sbBody.ToString());
            return ret.ToString();
        }
        /// <summary>
        /// 生成访问层类的主体
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strPrefix">表名前缀(要处理的)</param>
        /// <param name="strNamespace">名称空间</param>
        /// <returns></returns>
        public string GetDataAccessClassBody_SQL(string strTableName
            , string strPrefix
            , string strNamespace)
        {
            StringBuilder ret = new StringBuilder();

            ProcString ps = new ProcString();
            ps.SetTabs(strNamespace);
            TableInfo tableInfo = new TableInfo(strNamespace, strPrefix, strTableName);
            
            StringBuilder sbBody = new StringBuilder();
            sbBody.AppendLine(ps.tabMember + "#region GetSQL Function");
              #region public ind Add()
            //-----------
            if (strTableName.Substring(0, 5) == "JHPIX")
            {
                sbBody.AppendLine(ps.tabMember + "/// <summary>");
                sbBody.AppendLine(ps.tabMember + "/// 获取添加数据的SQL语句,忽视主键（即主键有值）。此方法用于PIX");
                sbBody.AppendLine(ps.tabMember + "/// <summary>");
                sbBody.Append(ps.tabMember + "public string GetInsertSQL_IgnoreID()");
                sbBody.AppendLine(ps.tabMember + "{");
                sbBody.AppendLine(ps.tabLocalVar + "string strSQL = \"INSERT INTO " + strTableName + " (" + tableInfo.GetInsertFields("") + ") VALUES (" + tableInfo.GetInsertValues("") + ")\";");
                sbBody.AppendLine(ps.tabLocalVar + "return strSQL;");
                sbBody.AppendLine(ps.tabMember + "}");
            }
            //-----------------------------
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 获取添加数据的SQL语句");
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.Append(ps.tabMember + "public string GetInsertSQL(");
            //if (tableInfo.KeyType == "NUMBER" && tableInfo.KeyColumns.Length == 1)
            //{
            //    sbBody.Append( "JHEMR.JHServicesLib.Client.JHDBClient dbClient)");
            //}
            //else
            //{
            sbBody.AppendLine(")");
            //}
            sbBody.AppendLine(ps.tabMember + "{");
            //if (strTableName != "JHPIX_PAT_MASTER_INDEX")// && strTableName != "JHPIX_PAT_IDENTITY" && strTableName != "JHPIX_PAT_CROSS_REF")
            //{
            //    if (tableInfo.KeyType == "NUMBER" && tableInfo.KeyColumns.Length == 1)
            //    {
            //        sbBody.AppendLine(ps.tabLocalVar + tableInfo.strKeyColumns + " = dbClient.GetNewID(\"" + strTableName + "\"); ");
            //    }
            //    else if (tableInfo.KeyType == "Varchar2")
            //    {
            //        sbBody.AppendLine(ps.tabLocalVar + tableInfo.strKeyColumns + " = new JHEMR.JHCommonLib.JHCommonFunction().NewGUID();");
            //    }
            //}
            sbBody.AppendLine(ps.tabLocalVar + "string strSQL = \"INSERT INTO " + strTableName + " (" + tableInfo.GetInsertFields("") + ") VALUES (" + tableInfo.GetInsertValues("") + ")\";");
            sbBody.AppendLine(ps.tabLocalVar + "return strSQL;");
            sbBody.AppendLine(ps.tabMember + "}");
            #endregion
            #region public ind Update()
            //-----------
            if (tableInfo.KeyColumns.Length != tableInfo.AllColumns.Length)//关系表没有数据需要更新，只有主外键。如需更新，删除重新添加。
            {
                sbBody.AppendLine(ps.tabMember + "/// <summary>");
                sbBody.AppendLine(ps.tabMember + "/// 获取更新数据的SQL语句");
                sbBody.AppendLine(ps.tabMember + "/// <summary>");
                sbBody.AppendLine(ps.tabMember + "public string GetUpdateSQL()");
                sbBody.AppendLine(ps.tabMember + "{");
                sbBody.AppendLine(ps.tabLocalVar + "string strSQL = \"UPDATE " + strTableName + " SET " + tableInfo.GetUpdateValues(""));
                sbBody.AppendLine(ps.tabLocalVar + "return strSQL;");
                sbBody.AppendLine(ps.tabMember + "}");
                //-----------
                if (strTableName.Substring(0, 5) == "JHPIX")
                {
                    sbBody.AppendLine(ps.tabMember + "/// <summary>");
                    sbBody.AppendLine(ps.tabMember + "/// 获取更新数据的SQL语句,只更新PIX_PATIENT_ID， 此方法用于PIX");
                    sbBody.AppendLine(ps.tabMember + "/// <summary>");
                    sbBody.AppendLine(ps.tabMember + "public string GetUpdatePIDSQL()");
                    sbBody.AppendLine(ps.tabMember + "{");
                    sbBody.AppendLine(ps.tabLocalVar + "string strSQL = \"UPDATE " + strTableName + " SET " + tableInfo.GetUpdateValuesForPID());
                    sbBody.AppendLine(ps.tabLocalVar + "return strSQL;");
                    sbBody.AppendLine(ps.tabMember + "}");
                }
                //lsj--类名
                string strClassName = strTableName;//ps.GetClassName(strTableName);//lsj

                //lsj--小写的类名--在此做类的对象来用。
                string strLowerClassName = ps.ConvertStringToUpperOrLower(strClassName, false);
                sbBody.AppendLine(ps.tabMember + "/// <summary>");
                sbBody.AppendLine(ps.tabMember + "/// 获取更新数据的SQL语句");
                sbBody.AppendLine(ps.tabMember + "/// <summary>");
                sbBody.AppendLine(ps.tabMember + "public string GetUpdateSQL(" + strClassName + " " + strLowerClassName + ")");
                sbBody.AppendLine(ps.tabMember + "{");
                sbBody.AppendLine(ps.tabLocalVar + "bool bChanged = false;");
                sbBody.AppendLine(ps.tabLocalVar + "string strSQL = \"UPDATE " + strTableName + " SET " + tableInfo.GetUpdateValuesForObj(strLowerClassName));
                sbBody.AppendLine(ps.tabLocalVar + "return strSQL;");
                sbBody.AppendLine(ps.tabMember + "}");


                //------------------------------
                string strfullLowerClassName = "full_" + strLowerClassName;
                sbBody.AppendLine(ps.tabMember + "/// <summary>");
                sbBody.AppendLine(ps.tabMember + "/// 获取更新数据的SQL语句");
                sbBody.AppendLine(ps.tabMember + "/// <summary>");
                sbBody.AppendLine(ps.tabMember + "public string GetUpdatePartialSQL(" + strClassName + " " + strfullLowerClassName + ")");
                sbBody.AppendLine(ps.tabMember + "{");
                sbBody.AppendLine(ps.tabLocalVar + "bool bChanged = false;");
                sbBody.AppendLine(ps.tabLocalVar + "string strSQL = \"UPDATE " + strTableName + " SET " + tableInfo.GetUpdateValuesForPartial(strfullLowerClassName));
                sbBody.AppendLine(ps.tabLocalVar + "return strSQL;");
                sbBody.AppendLine(ps.tabMember + "}");
                //--------------------------
                sbBody.AppendLine(ps.tabMember + "/// <summary>");
                sbBody.AppendLine(ps.tabMember + "/// 更新非空属性。");
                sbBody.AppendLine(ps.tabMember + "/// <summary>");
                sbBody.AppendLine(ps.tabMember + "public void UpdatePropFromNotNull(" + strClassName + " " + strLowerClassName + ")");
                sbBody.AppendLine(ps.tabMember + "{");
                sbBody.AppendLine(tableInfo.GetUpdatePropFromNotNull(strLowerClassName));
                sbBody.AppendLine(ps.tabMember + "}");
            }
            #endregion
            #region public ind Delete(string var1,.....)
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 获取删除数据的SQL语句");
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "public string GetDeleteSQL()");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "string strSQL = \"DELETE " + strTableName +" WHERE "+  tableInfo.GetDeleteValues("") );
            sbBody.AppendLine(ps.tabLocalVar + "return strSQL;");
            sbBody.AppendLine(ps.tabMember + "}");
            sbBody.AppendLine(ps.tabMember + "#endregion GetSQL Function");
            #endregion
            ret.Append(sbBody.ToString());
            return ret.ToString();
        }
        public string ConvertToSpecial(string strColumnName,string strTableName)
        {
            if (strTableName.Trim() == "USER_DEPT" && strColumnName.Trim() == "USER_DEPT")
            {
                strColumnName += "_ID";
            }
            if (strTableName.Trim() == "BACKGROUNDIMAGE" && strColumnName.Trim() == "BACKGROUNDIMAGE")
            {
                strColumnName += "_BLOB";
            }
            return strColumnName;
        }
        public string GetIndexSQL(TableInfo tableInfo)
        {
            StringBuilder ret = new StringBuilder();
            ProcString ps = new ProcString();
            StringBuilder sbBody = new StringBuilder();
            #region 
            sbBody.AppendLine(tableInfo.GetIndexSQL(""));
            #endregion
            ret.Append(sbBody.ToString());
            return ret.ToString();
        }
        /// <summary>
        /// 生成访问层类的主体
        /// </summary>
        /// <param name="strTableName">表名</param>
        /// <param name="strPrefix">表名前缀(要处理的)</param>
        /// <param name="strNamespace">名称空间</param>
        /// <returns></returns>
        public string GetClassBody_AData(TableInfo tableInfo, DataTable dtColumnType, string strPrefix, string strNamespace)
        {
            StringBuilder ret = new StringBuilder();

            ProcString ps = new ProcString();
            ps.SetTabs(strNamespace);

            //TableInfo tableInfo = new TableInfo(strNamespace, strPrefix, tableInfo.ta);
            StringBuilder sbBody = new StringBuilder();
            string className = tableInfo.ClassName;
            //className = ps.GetClassName(className);
            //string className_Lower = ps.ChangeToFirstCharUpperOrLower(className, false);//用于实体对象
            string classObject = ps.ConvertStringToUpperOrLower(className, false);//用于实体对象
            string tableDescription = string.IsNullOrEmpty(tableInfo.tableDescription) ? "" : tableInfo.tableDescription + " ";
            String KeyName = tableInfo.KeyColumns[0].ToString();
            String KeyVar = ps.ConvertStringToUpperOrLower(KeyName, false);
            String KeyType = ConvertType(tableInfo.KeyType,KeyName);

            sbBody.AppendLine(ps.tabMember + "#region " + className);
            sbBody.AppendLine(ps.tabMember + "public abstract Int32 Get" + className + "ById(" + KeyType + " " + KeyName + ", out " + className + " " + classObject + ");");
            sbBody.AppendLine(ps.tabMember + "public abstract Int32 Get" + className + "s(out List<" + className + "> " + classObject + "s);");

            string strAddOrUpdateParameters = tableInfo.GetParamaters_AData(ColumnsType.NonIdentityColumns, classObject);
            #region public  virtual Int32  Add()
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 添加" + tableDescription + className + "对象(即:一条记录");
            sbBody.AppendLine(ps.tabMember + "/// </summary>");
            sbBody.AppendLine(ps.tabMember + "public virtual Int32 Add" + className + "(ref " + className + " " + classObject + ")");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "SqlDataReader reader = null;");
            sbBody.AppendLine(ps.tabLocalVar + "if (" + classObject + " != null)");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "try");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "{");
            sbBody.AppendLine(strAddOrUpdateParameters);//---------------
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "Int32 affected = db_helper_.ExecuteReader(command_type_, \"P_" + className + "_INS\", out reader, parms);");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "if (affected != 0)");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "    System.Diagnostics.Debug.Assert(false, \"Add" + className + "Cache\");");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "if (reader != null && reader.Read() && reader.Get" + KeyType + "(0) != -1)");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "    " + classObject + "." + tableInfo.ColumnsList[0].ColumnName + " = reader.Get" + KeyType + "(0);");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "}");

            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "catch (System.Exception ex)");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "Exception.OrimsException.Instance.WriteLog(ex);");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "return Error.DBError.ConvertDBException(ex);");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "}");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "finally");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "if (reader != null && !reader.IsClosed)");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 +    "    reader.Close();");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "}");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabLocalVar + "return Error.DALError.SUCC;");
            sbBody.AppendLine(ps.tabMember + "}");
            #endregion

            #region public virtual Int32 Update()
            strAddOrUpdateParameters = tableInfo.GetParamaters_AData(ColumnsType.AllColumns, classObject);
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 更新" + tableDescription + className + "对象(即:一条记录");
            sbBody.AppendLine(ps.tabMember + "/// </summary>");
            sbBody.AppendLine(ps.tabMember + "public virtual Int32 Update" + className + "(ref " + className + " " + classObject + ")");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "if (" + classObject + " == null || " + classObject + "." + tableInfo.ColumnsList[0].ColumnName + " == -1)");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "return Error.DALError.PARM_NULL;");
            sbBody.AppendLine(ps.tabLocalVar + "SqlDataReader reader = null;");
            sbBody.AppendLine(ps.tabLocalVar + "if (" + classObject + " != null)");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "try");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "{");
            sbBody.AppendLine(strAddOrUpdateParameters);//---------------
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "Int32 affected = db_helper_.ExecuteReader(command_type_, \"P_" + className + "_UPD\", out reader, parms);");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "if (affected != 0)");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "    System.Diagnostics.Debug.Assert(false, \"Update" + className + "Cache\");");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "}");

            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "catch (System.Exception ex)");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "Exception.OrimsException.Instance.WriteLog(ex);");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "return Error.DBError.ConvertDBException(ex);");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "}");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "finally");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "if (reader != null && !reader.IsClosed)");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "    reader.Close();");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "}");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabLocalVar + "return Error.DALError.SUCC;");
            sbBody.AppendLine(ps.tabMember + "}");
            #endregion

            string strDeleteFunction = tableInfo.GetDelete_AData(classObject);
            #region public ind Delete(int ID,int UserID)
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 删除" + tableDescription + className + "对象(即:一条记录");
            sbBody.AppendLine(ps.tabMember + "/// </summary>");
            //sbBody.AppendLine(ps.tabMember + "public int Delete(" + tableInfo.GetFunctionParams() + ")");
            //sbBody.AppendLine(ps.tabMember + "{");
            //sbBody.AppendLine(ps.tabLocalVar + "string sql = \"DELETE " + tableInfo.TableName + " WHERE 1=1 " + tableInfo.GetDeleteFields() + " \";");
            //sbBody.AppendLine(strKeyParameters);
            //sbBody.AppendLine(ps.tabLocalVar + "return idb.ExeCmd(sql);");
            //sbBody.AppendLine(ps.tabMember + "}");

            sbBody.AppendLine(strDeleteFunction);
            #endregion

            sbBody.AppendLine(ps.tabMember + "#endregion " + className);

            ret.Append(sbBody.ToString());
            return ret.ToString();
        }
        /// <summary>
        /// 生成带Class(带成员)字符串
        /// 实体类
        /// </summary>
        /// <param name="strNameSpace">窗空间</param>
        /// <param name="strPrefix">表前缀</param>
        /// <param name="tableName">表名</param>
        /// <param name="strTableDescription">表描述</param>
        /// <param name="strMember">要加入的类成员</param>
        /// <param name="strClassBody">要加入的类主体</param>
        /// <returns></returns>
        public string GetClassByModel_AData(string strNameSpace, string strPrefix, string tableName, string strClassPrefix, string strClassBody)
        {
            StringBuilder ret = new StringBuilder();

            //类名大写
            string strTableName = GetClassNameByTableName(tableName, strPrefix.Trim());
            string strClassName_Entity = GetClassName(strTableName);//lsj
            string strClassName_AData = GetClassName_AData(strClassName_Entity);//lsj
            ProcTable pt = new ProcTable();
            string strTableDescription = pt.GetTableDesciption(tableName);
            SetTabs(strNameSpace.Trim());

            #region 写名称空间 {
            ret.AppendLine(strImportNameSpace_AData);
            if (strNameSpace.Trim().Length > 0)
            {
                ret.AppendLine("namespace " + strNameSpace.Trim());
                ret.AppendLine("{");
            }
            #endregion

            #region 写类描述
            if (strTableDescription.Length > 0)
            {//有表描述,
                ret.AppendLine(tabClass + "/// <summary>");
                ret.AppendLine(tabClass + "/// " + strTableDescription);
                ret.AppendLine(tabClass + "/// </summary>");
            }
            #endregion

            ret.AppendLine(tabClass + "public class " + strClassName_AData + " :  ADate");
            ret.AppendLine(tabClass + "{");
            //if (!string.IsNullOrEmpty(strMember)) { ret.AppendLine(tabMember + strMember); }
            ret.Append(strClassBody);
            ret.AppendLine(tabClass + "}");
            #region 写名称空间 
            if (strNameSpace.Trim().Length > 0)
            {
                ret.AppendLine("}");
            }
            #endregion
            return ret.ToString();
        }
        public string GetClassBody_FullManage(TableInfo tableInfo, DataTable dtColumnType, string strPrefix, string strNamespace)
        {
            StringBuilder ret = new StringBuilder();

            ProcString ps = new ProcString();
            ps.SetTabs(strNamespace);

            //TableInfo tableInfo = new TableInfo(strNamespace, strPrefix, tableInfo.ta);
            StringBuilder sbBody = new StringBuilder();
            string className = tableInfo.ClassName;
            //className = ps.GetClassName(className);
            //string className_Lower = ps.ChangeToFirstCharUpperOrLower(className, false);//用于实体对象
            string classObject = ps.ConvertStringToUpperOrLower(className, false);//用于实体对象
            string tableDescription = string.IsNullOrEmpty(tableInfo.tableDescription) ? "" : tableInfo.tableDescription + " ";
            String KeyName = tableInfo.KeyColumns[0].ToString();
            String KeyVar = ps.ConvertStringToUpperOrLower(KeyName, false);
            String KeyType = ConvertType(tableInfo.KeyType,KeyName);

            sbBody.AppendLine(ps.tabMember + "#region " + className);
            sbBody.AppendLine(ps.tabMember + "public override Int32 Get" + className + "ById(" + KeyType + " " + KeyName + ", out " + className + " " + classObject + ")");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + classObject + " = null;");
            sbBody.AppendLine(ps.tabLocalVar + "if (CacheVisitManger.Instance.OnOperationCache != null) ");
            sbBody.AppendLine(ps.tabLocalVar + "{ ");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "return CacheVisitManger.Instance.OnOperationCache.Get" + className + "ById(" + KeyName + ", out " + classObject + "); ");
            sbBody.AppendLine(ps.tabLocalVar + "} ");
            sbBody.AppendLine(ps.tabLocalVar + "return Error.DALError.SUCC; ");
            sbBody.AppendLine(ps.tabMember + "} ");
            sbBody.AppendLine(ps.tabMember + "public override Int32 Get" + className + "s(out List<" + className + "> " + classObject + "s)");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + classObject + "s = null;");
            sbBody.AppendLine(ps.tabLocalVar + "if (CacheVisitManger.Instance.OnOperationCache != null) ");
            sbBody.AppendLine(ps.tabLocalVar + "{ ");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "return CacheVisitManger.Instance.OnOperationCache.Get" + className + "s(out " + classObject + "s); ");
            sbBody.AppendLine(ps.tabLocalVar + "} ");
            sbBody.AppendLine(ps.tabLocalVar + "return Error.DALError.SUCC; ");
            sbBody.AppendLine(ps.tabMember + "} ");
            string strAddOrUpdateParameters = tableInfo.GetParamaters_AData(ColumnsType.NonIdentityColumns, classObject);
            #region public  override Int32  Add()
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 添加" + tableDescription + className + "对象(即:一条记录");
            sbBody.AppendLine(ps.tabMember + "/// </summary>");
            sbBody.AppendLine(ps.tabMember + "public override Int32 Add" + className + "(ref " + className + " " + classObject + ")");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "int resval = base.Add" + className + "(ref " + classObject + ");");
            sbBody.AppendLine(ps.tabLocalVar + "if (Error.DALError.SUCCEEDED(resval))");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "if (CacheVisitManger.Instance.OnOperationCache != null)");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTopNext + "CacheVisitManger.Instance.OnOperationCache.Add" + className + "(ref " + classObject + ");");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "}");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabLocalVar + "return resval;");
            sbBody.AppendLine(ps.tabMember + "}");
            #endregion

            #region public virtual Int32 Update()
            strAddOrUpdateParameters = tableInfo.GetParamaters_AData(ColumnsType.AllColumns, classObject);
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 更新" + tableDescription + className + "对象(即:一条记录");
            sbBody.AppendLine(ps.tabMember + "/// </summary>");
            sbBody.AppendLine(ps.tabMember + "public override Int32 Update" + className + "(ref " + className + " " + classObject + ")");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "int resval = base.Update" + className + "(ref " + classObject + ");");
            sbBody.AppendLine(ps.tabLocalVar + "if (Error.DALError.SUCCEEDED(resval))");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "if (CacheVisitManger.Instance.OnOperationCache != null)");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTopNext + "CacheVisitManger.Instance.OnOperationCache.Update" + className + "(ref " + classObject + ");");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "}");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabLocalVar + "return resval;");
            sbBody.AppendLine(ps.tabMember + "}");
            #endregion

            
            #region public ind Delete(int ID,int UserID)
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 删除" + tableDescription + className + "对象(即:一条记录");
            sbBody.AppendLine(ps.tabMember + "/// </summary>");
            sbBody.AppendLine(ps.tabMember + "public override Int32 Delete" + className + "(" + KeyType + " " + KeyName + ")");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "int resval = base.Delete" + className + "(" + KeyName + ");");
            sbBody.AppendLine(ps.tabLocalVar + "if (Error.DALError.SUCCEEDED(resval))");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "if (CacheVisitManger.Instance.OnOperationCache != null)");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTopNext + "CacheVisitManger.Instance.OnOperationCache.Delete" + className + "(" + KeyName + ");");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "}");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabLocalVar + "return resval;");
            sbBody.AppendLine(ps.tabMember + "}");
            #endregion

            sbBody.AppendLine(ps.tabMember + "#endregion " + className);

            ret.Append(sbBody.ToString());
            return ret.ToString();
        }
        public string GetClassBody_ACache(TableInfo tableInfo, DataTable dtColumnType ,string strPrefix, string strNamespace)
        {
            StringBuilder ret = new StringBuilder();
            ProcString ps = new ProcString();
            ps.SetTabs(strNamespace);
            StringBuilder sbBody = new StringBuilder();
            String className = tableInfo.ClassName;
            //className = ps.GetClassName(className);
            String classObject = ps.ConvertStringToUpperOrLower(className, false);//用于实体对象
            String tableDescription = String.IsNullOrEmpty(tableInfo.tableDescription) ? "" : tableInfo.tableDescription + " ";

            String CacheObject = classObject + "_cache";
            String KeyName = tableInfo.KeyColumns[0].ToString();
            String KeyVar = ps.ConvertStringToUpperOrLower(KeyName, false);
            String KeyType = ConvertType(tableInfo.KeyType,KeyName);
            #region //构造函数
            //sbBody.AppendLine(ps.tabMember + "//public A" + className + "Cache(String conn_string, String reader) : base(conn_string, reader)");
            //sbBody.AppendLine(ps.tabMember + "//{");
            //sbBody.AppendLine(ps.tabLocalVar + "//if (cache_control_type_ == CacheControlType.Open)");
            //sbBody.AppendLine(ps.tabIfLocalVarTop1 + "//Init(); ");
            //sbBody.AppendLine(ps.tabMember + "//} ");
            #endregion
            sbBody.AppendLine(ps.tabMember + "#region " + className);
            sbBody.AppendLine(ps.tabMember + "protected Dictionary<" + KeyType + ", " + className + "> " + CacheObject + ";");
            sbBody.AppendLine(ps.tabMember + "public Dictionary<" + KeyType + ", " + className + "> " + className + "Cache");
            sbBody.AppendLine(ps.tabMember + "{" );
            sbBody.AppendLine(ps.tabLocalVar + "get { return " + classObject + "_cache; }");
            sbBody.AppendLine(ps.tabMember + "} " );
            //string strAddOrUpdateParameters = tableInfo.GetParamaters_AData(ColumnsType.NonIdentityColumns, classObject);
            #region public Int32 GetObjectById()
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 根据ID获取" + tableDescription + className + "对象");
            sbBody.AppendLine(ps.tabMember + "/// </summary>");
            sbBody.AppendLine(ps.tabMember + "/// <param name=\"" + KeyVar + "\">" + tableDescription + className + "ID</param>");
            sbBody.AppendLine(ps.tabMember + "/// <param name=\"" + classObject + "\">获取到的" + tableDescription + className + "对象</param>");
            sbBody.AppendLine(ps.tabMember + "public  Int32 Get" + className + "ById(" + KeyType + " " + KeyVar + ",out " + className + " " + classObject + ")");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "lock (mutex_)");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + classObject + " = new " + className + "();");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "if (" + CacheObject + ".ContainsKey(" + KeyVar + "))");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 +     classObject + " = " + CacheObject + "[" + KeyVar + "];");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "else");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 +    "return Error.DALError.KEY_NOT_EXISTS;");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "return Error.DALError.SUCC;");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabMember + "}");

            #endregion

            #region public Int32 GetObjectList()
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 获取" + tableDescription + className + "列表");
            sbBody.AppendLine(ps.tabMember + "/// </summary>");
            sbBody.AppendLine(ps.tabMember + "/// <param name=\"" + classObject + "\">获取到的" + tableDescription + className + "列表</param>");
            sbBody.AppendLine(ps.tabMember + "public  Int32 Get" + className + "s(out List<" + className + "> " + classObject + "s)");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "lock (mutex_)");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + classObject + "s =  " + CacheObject + ".Values.ToList();");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "return Error.DALError.SUCC;");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabMember + "}");
            #endregion

            #region public Int32 Add(ref object)
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 添加" + tableDescription + className );
            sbBody.AppendLine(ps.tabMember + "/// </summary>");
            sbBody.AppendLine(ps.tabMember + "/// <param name=\"" + classObject + "\">新" + tableDescription + className + "</param>");
            sbBody.AppendLine(ps.tabMember + "public  Int32 Add" + className + "(ref " + className + " " + classObject + ")");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "lock (mutex_)");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "if (!" + CacheObject + ".ContainsKey(" + classObject + "." + KeyName + ")) ");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 +    CacheObject + ".Add(" + classObject + "." + KeyName + ", " + classObject + ");");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "return Error.DALError.SUCC;");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabMember + "}");
            #endregion

            #region public Int32 Update(object)
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 修改" + tableDescription + className);
            sbBody.AppendLine(ps.tabMember + "/// </summary>");
            sbBody.AppendLine(ps.tabMember + "/// <param name=\"" + classObject + "\">" + tableDescription + className + "</param>");
            sbBody.AppendLine(ps.tabMember + "public  Int32 Update" + className + "(ref " + className + " " + classObject + ")");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "lock (mutex_)");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "if (" + CacheObject + ".ContainsKey(" + classObject + "." + KeyName + ")) ");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 +    CacheObject + "[" + classObject + "." + KeyName + "] = " + classObject + ";");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "else");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 +    "return Error.DALError.KEY_NOT_EXISTS;");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "return Error.DALError.SUCC;");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabMember + "}");
            #endregion

            #region public Int32 Delete(Int32 ID)
            sbBody.AppendLine(ps.tabMember + "/// <summary>");
            sbBody.AppendLine(ps.tabMember + "/// 删除" + tableDescription + className);
            sbBody.AppendLine(ps.tabMember + "/// </summary>");
            sbBody.AppendLine(ps.tabMember + "/// <param name=\"" + classObject + "\">" + tableDescription + className + "</param>");
            sbBody.AppendLine(ps.tabMember + "public  Int32 Delete" + className + "(" + KeyType + " " + KeyVar + ")");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "lock (mutex_)");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "if (" + CacheObject + ".ContainsKey(" + KeyVar + ")) ");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 +    className + " " + classObject + " = null;");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 +    "Get" + className + "ById(" + KeyVar + ",out " +  classObject + ");");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 +    CacheObject + ".Remove(" + KeyVar + ");");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "}");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "return Error.DALError.SUCC;");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabMember + "}");
            #endregion

            sbBody.AppendLine(ps.tabMember + "#endregion " + className);

            ret.Append(sbBody.ToString());
            return ret.ToString();
        }

        
        /// <summary>
        /// 生成带Class(带成员)字符串
        /// 实体类
        /// </summary>
        /// <param name="strNameSpace">窗空间</param>
        /// <param name="strPrefix">表前缀</param>
        /// <param name="tableName">表名</param>
        /// <param name="strTableDescription">表描述</param>
        /// <param name="strMember">要加入的类成员</param>
        /// <param name="strClassBody">要加入的类主体</param>
        /// <returns></returns>
        public string GetClassByModel_ACache(string strNameSpace, string strPrefix, string tableName, string strClassPrefix, string strClassBody)
        {
            StringBuilder ret = new StringBuilder();

            //类名大写
            string strTableName = GetClassNameByTableName(tableName, strPrefix.Trim());
            //string strClassName_Entity = GetClassName(strTableName);//lsj
            string strClassName_ACache = GetClassName_ACache(strTableName);//lsj
            ProcTable pt = new ProcTable();
            string strTableDescription = pt.GetTableDesciption(tableName);
            SetTabs(strNameSpace.Trim());

            #region 写名称空间 {
            ret.AppendLine(strImportNameSpace_ACache);
            if (strNameSpace.Trim().Length > 0)
            {
                ret.AppendLine("namespace " + strNameSpace.Trim());
                ret.AppendLine("{");
            }
            #endregion

            #region 写类描述
            if (strTableDescription.Length > 0)
            {//有表描述,
                ret.AppendLine(tabClass + "/// <summary>");
                ret.AppendLine(tabClass + "/// " + strTableDescription);
                ret.AppendLine(tabClass + "/// <summary>");
            }
            #endregion

            ret.AppendLine(tabClass + "public abstract class " + strClassName_ACache + " :  ACache");
            ret.AppendLine(tabClass + "{");
            ret.Append(strClassBody);
            ret.AppendLine(tabClass + "}");
            #region 写名称空间 }
            if (strNameSpace.Trim().Length > 0)
            {
                ret.AppendLine("}");
            }
            #endregion
            return ret.ToString();
        }

        public string GetClassBody_FullCache(TableInfo tableInfo, DataTable dtColumnType, string strPrefix, string strNamespace)
        {
            StringBuilder ret = new StringBuilder();
            ProcString ps = new ProcString();
            ps.SetTabs(strNamespace);
            StringBuilder sbBody = new StringBuilder();
            String className = tableInfo.ClassName;
            //className = ps.GetClassName(className);
            String classObject = ps.ConvertStringToUpperOrLower(className, false);//用于实体对象
            String tableDescription = String.IsNullOrEmpty(tableInfo.tableDescription) ? "" : tableInfo.tableDescription + " ";

            String FullCacheClassName = className + "FullCache";
            String CacheObject = classObject + "_cache";

            String KeyName = tableInfo.KeyColumns[0].ToString();
            String KeyVar = ps.ConvertStringToUpperOrLower(KeyName, false);
            String KeyType = ConvertType(tableInfo.KeyType,KeyName);
            sbBody.AppendLine(ps.tabMember + "#region Init" + className + "Cache()");
            sbBody.AppendLine(ps.tabMember + "protected Dictionary<" + KeyType + ", " + className + "> " + CacheObject + ";");
            
            sbBody.AppendLine(ps.tabMember + "private void Init" + className + "Cache()");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + CacheObject + " = new Dictionary<" + KeyType + ", " + className + ">();");
            sbBody.AppendLine(ps.tabLocalVar + "SqlDataReader reader = null; ");
            sbBody.AppendLine(ps.tabLocalVar + "Int32 result = Error.DALError.SUCC; ");
            sbBody.AppendLine(ps.tabLocalVar + "try");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "lock (" + CacheObject + ")");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTopNext + "SqlParameter[] parms = new SqlParameter[1];");
            sbBody.AppendLine(ps.tabIfLocalVarTopNext + "parms[0] = new SqlParameter(\"@table_name\", System.Data.SqlDbType.NVarChar, 20);");
            sbBody.AppendLine(ps.tabIfLocalVarTopNext + "parms[0].Value = \"" + tableInfo.TableName + "\";");
            sbBody.AppendLine(ps.tabIfLocalVarTopNext + "result = db_helper_.ExecuteReader(base.command_type_, \"P_SEL_ALL\", out reader, parms);");
            sbBody.AppendLine(ps.tabIfLocalVarTopNext + "if (Error.DALError.FAILED(result))");
            sbBody.AppendLine(ps.tabIfLocalVarTopNext + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTopNextMore + "throw new System.Exception(Error.ErrorManger.Instance.GetErrorDesc(result));");
            sbBody.AppendLine(ps.tabIfLocalVarTopNext + "}");
            sbBody.AppendLine(ps.tabIfLocalVarTopNext + "if (reader != null)");
            sbBody.AppendLine(ps.tabIfLocalVarTopNext + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTopNextMore + "while (reader.Read())");
            sbBody.AppendLine(ps.tabIfLocalVarTopNextMore + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTopNextMoreMore + className + " " + classObject + " = CreateModel<" + className + ">(reader);");
            sbBody.AppendLine(ps.tabIfLocalVarTopNextMoreMore + CacheObject + ".Add(" + classObject + "." + KeyName + ", " + classObject + ");");
            sbBody.AppendLine(ps.tabIfLocalVarTopNextMore + "}");
            sbBody.AppendLine(ps.tabIfLocalVarTopNext + "}");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "}");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabLocalVar + "catch (System.Exception e)");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "OrimsException.Instance.WriteLog(e);");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabLocalVar + "finally");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "if (reader != null && !reader.IsClosed)reader.Close();");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabMember + "} ");
            //string strAddOrUpdateParameters = tableInfo.GetParamaters_AData(ColumnsType.NonIdentityColumns, classObject);


            sbBody.AppendLine(ps.tabMember + "#endregion  Init" + className + "Cache()");

            ret.Append(sbBody.ToString());
            return ret.ToString();
        }

        public string GetClassByModel_FullCache(string strNameSpace, string strPrefix, string tableName, string strClassPrefix, string strClassBody)
        {
            StringBuilder ret = new StringBuilder();

            //类名大写
            string strTableName = GetClassNameByTableName(tableName, strPrefix.Trim());
            //string strClassName_Entity = GetClassName(strTableName);//lsj
            string strClassName_FullCache = GetClassName_FullCache(strTableName);//lsj
            string strClassName_ACache = GetClassName_ACache(strTableName);//lsj
            ProcTable pt = new ProcTable();
            string strTableDescription = pt.GetTableDesciption(tableName);
            SetTabs(strNameSpace.Trim());

            #region 写名称空间 {
            ret.AppendLine(strImportNameSpace_FullCache);
            if (strNameSpace.Trim().Length > 0)
            {
                ret.AppendLine("namespace " + strNameSpace.Trim());
                ret.AppendLine("{");
            }
            #endregion

            #region 写类描述
            if (strTableDescription.Length > 0)
            {//有表描述,
                ret.AppendLine(tabClass + "/// <summary>");
                ret.AppendLine(tabClass + "/// " + strTableDescription);
                ret.AppendLine(tabClass + "/// <summary>");
            }
            #endregion

            ret.AppendLine(tabClass + "public abstract class " + strClassName_FullCache + " : " + strClassName_ACache);
            ret.AppendLine(tabClass + "{");
            ret.Append(strClassBody);
            ret.AppendLine(tabClass + "}");
            #region 写名称空间 }
            if (strNameSpace.Trim().Length > 0)
            {
                ret.AppendLine("}");
            }
            #endregion
            return ret.ToString();
        }

        public string GetSQL_Proc(TableInfo tableInfo, DataTable dtColumnType, string strPrefix, string strNamespace)
        {
            StringBuilder ret = new StringBuilder();
            ProcString ps = new ProcString();
            ps.SetTabs(strNamespace);
            StringBuilder sbBody = new StringBuilder();
            String tableName = tableInfo.TableName;
            String className = tableInfo.ClassName;
            String classObject = ps.ConvertStringToUpperOrLower(className, false);//用于实体对象
            String tableDescription = String.IsNullOrEmpty(tableInfo.tableDescription) ? "" : tableInfo.tableDescription + " ";

            String FullCacheClassName = className + "FullCache";
            String CacheObject = classObject + "_cache";

            String KeyName = tableInfo.KeyColumns[0].ToString();
            String KeyVar = ps.ConvertStringToUpperOrLower(KeyName, false);
            String KeyType = tableInfo.KeyType;
            sbBody.AppendLine("-- begin " + tableName + "--");
            #region insert
            String ProcName="P_" + className + "_INS";
            sbBody.AppendLine("SET ANSI_NULLS ON");
            sbBody.AppendLine("GO");
            sbBody.AppendLine("SET QUOTED_IDENTIFIER ON");
            sbBody.AppendLine("GO");
            //sbBody.AppendLine("if exists(select * from sysobjects where name = '" + ProcName + "')");
            //sbBody.AppendLine(ps.tabMember + "Drop Proc " + ProcName);
            //sbBody.AppendLine("GO");
            sbBody.AppendLine("Create PROC [dbo].[" + ProcName + "]");

            StringBuilder sbParams = new StringBuilder();
            StringBuilder sbParams_Delete = new StringBuilder();
            StringBuilder Columns = new StringBuilder();
            StringBuilder ColumnsValue = new StringBuilder();
            StringBuilder ColumnsAndValue_Update = new StringBuilder();
            StringBuilder ColumnUserID_Delete = new StringBuilder();
            foreach (ColumnInfo col in tableInfo.ColumnsList)
            {
                string columnName = col.ColumnName;//列名
                string columnType = col.TypeName;//列数据类型
                if (columnName == tableInfo.ColumnsList[0].ColumnName)//如果是主键，待改进
                {
                    continue;
                }
                if (columnName == "UserID")//如果是主键，待改进
                {
                    sbParams_Delete.AppendLine("," + ps.tabMember + "@" + columnName + " " + columnType + "");
                    ColumnUserID_Delete.Append(",[" + columnName + "]=").Append("@" + columnName + "");
                }
                if (columnName == "RowStamp" || columnName == "IsDeleted" || columnName == "RegisterTime")//如果是时间戳
                    continue;
                
                if (col.TypeName == "varchar")
                    sbParams.AppendLine(ps.tabMember + "@" + columnName + " " + columnType + "(" + col.Length + "),");
                else
                    sbParams.AppendLine(ps.tabMember + "@" + columnName + " " + columnType + ",");
                
                Columns.Append("[" + columnName + "],");
                ColumnsValue.Append("@" + columnName + ",");
                ColumnsAndValue_Update.Append("[" + columnName + "]=").Append("@" + columnName + ",");
            }
            sbParams.Remove(sbParams.Length - 3, 1);
            Columns.Remove(Columns.Length - 1,1);
            ColumnsValue.Remove(ColumnsValue.Length - 1, 1);
            ColumnsAndValue_Update.Remove(ColumnsAndValue_Update.Length - 1, 1);
            
            sbBody.Append(sbParams);
            sbBody.AppendLine("as");
            
            sbBody.AppendLine("begin");
            sbBody.AppendLine(ps.tabMember + "declare @" + KeyName + " " + KeyType);
            sbBody.AppendLine(ps.tabMember + "set @" + KeyName + " = 0");
            sbBody.AppendLine(ps.tabMember + "begin try");
            sbBody.AppendLine(ps.tabLocalVar + "insert into " + tableName + "(" + Columns + ")");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "values(" + ColumnsValue + ")");
            sbBody.AppendLine(ps.tabLocalVar + "set @" + KeyName + " = @@IDENTITY");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "select @" + KeyName);
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "return 0");
            sbBody.AppendLine(ps.tabMember + "end try");
            sbBody.AppendLine(ps.tabMember + "begin catch");
            sbBody.AppendLine(ps.tabLocalVar + "if @@ERROR <> 0");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "begin");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "return ERROR_NUMBER()");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "end");
            sbBody.AppendLine(ps.tabMember + "end catch");
            sbBody.AppendLine("end");
            sbBody.AppendLine("GO");
            //-----------------
            #endregion
            #region update
            ProcName = "P_" + className + "_UPD";
            //sbBody.AppendLine("if exists(select * from sysobjects where name = '" + ProcName + "')");
            //sbBody.AppendLine(ps.tabMember + "Drop Proc " + ProcName);
            //sbBody.AppendLine("GO");
            sbBody.AppendLine("Create PROC [dbo].[" + ProcName + "]");
            sbBody.Append("@" + KeyName + " " + KeyType + ",");
            sbBody.Append(sbParams);
            sbBody.AppendLine("as");

            sbBody.AppendLine("begin");
            sbBody.AppendLine(ps.tabMember + "begin try");
            sbBody.AppendLine(ps.tabLocalVar + "update " + tableName + " set " + ColumnsAndValue_Update + "");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + " where " + KeyName + "=@" + KeyName);
            sbBody.AppendLine(ps.tabLocalVar + "return 0");
            sbBody.AppendLine(ps.tabMember + "end try");
            sbBody.AppendLine(ps.tabMember + "begin catch");
            sbBody.AppendLine(ps.tabLocalVar + "if @@ERROR <> 0");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "begin");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "return ERROR_NUMBER()");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "end");
            sbBody.AppendLine(ps.tabMember + "end catch");
            sbBody.AppendLine("end");
            sbBody.AppendLine("GO");
            #endregion
            #region delete
            ProcName = "P_" + className + "_DEL";
            sbBody.AppendLine("if exists(select * from sysobjects where name = '" + ProcName + "')");
            sbBody.AppendLine(ps.tabMember + "Drop Proc " + ProcName);
            sbBody.AppendLine("GO");
            sbBody.AppendLine("Create PROC [dbo].[" + ProcName + "]");
            sbBody.Append("@" + KeyName + " " + KeyType);
            sbBody.Append(sbParams_Delete);
            sbBody.AppendLine(" as");

            sbBody.AppendLine("begin");
            sbBody.AppendLine(ps.tabMember + "begin try");
            if (ColumnUserID_Delete.ToString() == "")
                sbBody.Append(ps.tabLocalVar + "delete " + tableName );
            else
                sbBody.Append(ps.tabLocalVar + "update " + tableName + " set IsDeleted=1" + ColumnUserID_Delete);
            
            sbBody.AppendLine(" where " + KeyName + "=@" + KeyName);
            sbBody.AppendLine(ps.tabLocalVar + "return 0");
            sbBody.AppendLine(ps.tabMember + "end try");
            sbBody.AppendLine(ps.tabMember + "begin catch");
            sbBody.AppendLine(ps.tabLocalVar + "if @@ERROR <> 0");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "begin");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "return ERROR_NUMBER()");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "end");
            sbBody.AppendLine(ps.tabMember + "end catch");
            sbBody.AppendLine("end");
            sbBody.AppendLine("GO");
            #endregion
            sbBody.AppendLine("-- End " + tableName + "--");
            ret.Append(sbBody.ToString());
            return ret.ToString();
        }
        #endregion
    }
}
