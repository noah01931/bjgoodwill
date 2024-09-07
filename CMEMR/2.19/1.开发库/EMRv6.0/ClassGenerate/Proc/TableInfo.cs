using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ClassGenerate.Proc;
using System.Linq;
namespace ClassGenerate.Proc
{
    /// <summary>
    /// 列集合
    /// </summary>
    public enum ColumnsType { AllColumns, NonIdentityColumns, KeyColumns };
    public class TableInfo
    {
        List<ColumnInfo> columnList=new List<ColumnInfo>();
        String keyType;

        public String KeyType
        {
            get { return keyType; }
            set { keyType = value; }
        }
        public List<ColumnInfo> ColumnsList
        {
            get { return columnList; }
            set { columnList = value; }
        }
        public TableInfo()
        {
        }
        public TableInfo(string strPrefix, string strTableName)
        {
            this.dsTableInfo = pt.GetTableInfo(strTableName);
            this.strPrefix = strPrefix;
            className = ps.GetClassNameByTableName(strTableName, strPrefix);
            this.strTableName = strTableName;// dsTableInfo.Tables[0].Rows[0][0].ToString();
            dsTableInfo.Tables[1].TableName = strTableName;
            tableDescription = pt.GetTableDesciption(strTableName);
            //20101112--lsj--
            if (dsTableInfo.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < dsTableInfo.Tables[1].Rows.Count; i++)
                {
                    ColumnInfo col = new ColumnInfo();
                    col.ColumnName = dsTableInfo.Tables[1].Rows[i]["Column_name"].ToString();
                    col.TypeName = dsTableInfo.Tables[1].Rows[i]["Type"].ToString();
                    col.Length = dsTableInfo.Tables[1].Rows[i]["Length"].ToString();
                    columnList.Add(col);
                }
                keyType = ColumnsList[0].TypeName;//20110920
            }
            int rows;
            #region 标识列 Field1,Field2..
            if (dsTableInfo.Tables[2].Rows[0]["Type"].ToString() == "NUMBER")
            {
                rows = dsTableInfo.Tables[2].Rows.Count;
                if (dsTableInfo.Tables[2].Rows.Count > 0)
                {
                    indentityColumns = new string[rows];
                    for (int i = 0; i < rows; i++)
                    {
                        string strColumn = dsTableInfo.Tables[2].Rows[i][0].ToString();
                        //string t=dsTableInfo.Tables[2].Rows[i][3].ToString();
                        indentityColumns[i] = strColumn.Trim();
                    }
                }
            }
            #endregion

            #region 主键列 Field1,Field2..
            
            if (dsTableInfo.Tables.Count > 5 && dsTableInfo.Tables[5].Rows.Count > 0)
            {
                strPKName = dsTableInfo.Tables[5].Rows[0]["constraint_name"].ToString().Trim();
                strKeyColumns = "";
                for (int i = 0; i < dsTableInfo.Tables[5].Rows.Count; i++)
                {
                    strKeyColumns += dsTableInfo.Tables[5].Rows[i][0].ToString().Trim()+",";
                }
                strKeyColumns = strKeyColumns.Substring(0, strKeyColumns.Length - 1);
            }
            else
            {
                strPKName = "";
                //throw new Exception(dsTableInfo.Tables[1].TableName+":表没缺少主键");
            }
            #endregion

            #region 列名数组
            DataTable dtColumnsType = dsTableInfo.Tables[1];
            rows = dtColumnsType.Rows.Count;

            if (rows <= 0)
            {
                throw new Exception("没有表列类型相关信息");
            }
            columns = new string[rows];
            for (int i = 0; i < rows; i++)
            {
                columns[i] = dtColumnsType.Rows[i][0].ToString().Trim();
            }
            #endregion
        }
        public TableInfo(string strNameSpace, string strPrefix,string strTableName)
            : this(strPrefix, strTableName)
        {
            this.strNamespace = strNameSpace;
            ps.SetTabs(strNameSpace);
        }
        public bool CheckColumnName(string strColumnName)
        {
            foreach (string field in columns)
            {
                if (strColumnName == field.ToUpper())
                    return true;
            }
            return false;
        }
        ProcTable pt = new ProcTable();
        ProcString ps = new ProcString();
        /// <summary>
        /// 空间空间
        /// </summary>
        string strNamespace = "";
        /// <summary>
        /// 表的前缀
        /// </summary>
        string strPrefix;
        public DataTable ColumnType { get { return dsTableInfo.Tables[1]; } }
        /// <summary>
        /// 表的相关信息 Tables[0]表名, Tables[1]列类型信息, Tables[2] 标识列,Tables[5]主键列信息信息
        /// </summary>
        public DataSet dsTableInfo;
        /// <summary>
        /// 表描述
        /// </summary>
        public string tableDescription;
        /// <summary>
        /// 表名
        /// </summary>
        string strTableName;
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return strTableName; }
            set { strTableName = value; }
        }
        /// <summary>
        /// 表对应的类名
        /// </summary>
        string className;
        /// <summary>
        /// 获取表对应的类名
        /// </summary>
        public string ClassName
        {
            get
            {
                return className;
            }
            set { className = value; }
        }
        /// <summary>
        /// 获取表对应的类的对象
        /// </summary>
        public string ClassObject
        {
            get
            {
                return ps.GetObjectByTableName(strTableName, strPrefix);
            }
        }
        /// <summary>
        /// 主键列  field1,field2
        /// </summary>
        public string strKeyColumns = "";
        public string strPKName = "";
        /// <summary>
        /// 获取主键列
        /// </summary>
        public string[] KeyColumns
        {
            get
            {
                if (string.IsNullOrEmpty(strKeyColumns))
                {
                    //throw new Exception("缺少主键");
                    return null;
                }

                return strKeyColumns.Split(',');
            }
        }

        /// <summary>
        /// 主键列  field1,field2
        /// </summary>
        public string strForeignKeyColumns = "";
        /// <summary>
        /// 获取主键列
        /// </summary>
        public string[] ForeignKeyColumns
        {
            get
            {
                return strForeignKeyColumns.Split(',');
            }
        }
        /// <summary>
        /// 是否为外键
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool IsForeignKey( String columnName)
        {
            bool bFkey = false;
            foreach (string FKey in ForeignKeyColumns)
            {
                if (FKey == columnName)
                {
                    bFkey = true;
                    break;
                }
            }
            return bFkey;
        }
        /// <summary>
        /// 是否为主键
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public bool IsKey(String columnName)
        {
            bool bFkey = false;
            foreach (string FKey in KeyColumns)
            {
                if (FKey == columnName)
                {
                    bFkey = true;
                    break;
                }
            }
            return bFkey;
        }
        /// <summary>
        /// 表所有列
        /// </summary>
        string[] columns;
        /// <summary>
        /// 表所有列
        /// </summary>
        public string[] AllColumns
        {
            get { return columns; }
        }
        /// <summary>
        /// 获取所有字段名  field1,field2
        /// </summary>
        public string AllColumnsString
        {
            get 
            {
                StringBuilder sb = new StringBuilder();
                foreach (string field in columns)
                {
                    sb.Append(field + ",");
                }

                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// 标识列 Field1,Field2..
        /// </summary>
        string[] indentityColumns;
        /// <summary>
        /// 获取非标识列
        /// </summary>
        public string[] NonIdentityColumns
        {
            get
            {
                List<string> t = new List<string>();
                foreach (string column in columns)
                {
                    //if (!ps.IsContains(column, indentityColumns))
                    if (!ps.IsContains(column, KeyColumns))
                    {
                        t.Add(column);
                    }
                }
                string[] list = new string[t.Count];
                t.CopyTo(list);
                return list;
            }
             
        }
        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="isContainIndentity">true包含标识列,false不包含标识列</param>
        /// <returns></returns>
        public List<NameValue> GetProperties(ColumnsType type)
        {
            return GetProperties(type,"");
        }
        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="isContainIndentity">true包含标识列,false不包含标识列</param>
        /// <returns></returns>
        public List<NameValue> GetProperties(ColumnsType type, string classObject)
        {
            List<NameValue> list = new List<NameValue>();
            if (columns == null)
            {
                throw new Exception("Param.columns 为空");
            }
            else
            {
                string[] cols = null;
                switch (type)
                {
                    case ColumnsType.AllColumns:
                        cols = columns;
                        break;
                    case ColumnsType.NonIdentityColumns:
                        cols = NonIdentityColumns;
                        break;
                    case ColumnsType.KeyColumns:
                        cols = KeyColumns;
                        break;
                }
                ProcString ps = new ProcString();
                //对象首字符小写
                string strObject = "";
                if (!string.IsNullOrEmpty(classObject))
                {
                    strObject = ps.GetObjectByTableName(strTableName.Trim(), strPrefix) + ".";

                    foreach (string field in cols)
                    {
                        string columnName = field.Trim();//字段
                        string propertyName = ps.ConvertStringToUpperOrLower(columnName, false);//属性
                        NameValue nv = new NameValue();
                        nv.Name = "@" + columnName;
                        nv.FieldName = ps.ConvertToSpecial(columnName,strTableName) ;
                        nv.MemberName = propertyName;
                        nv.Value = strObject + propertyName;
                        nv.FieldType = this.columnList.Where(c => c.ColumnName == columnName).Single().TypeName;
                        nv.ValueIsNotNull = ValueIsNotNull(nv);
                        nv.ValueIsNotEmpty = ValueIsNotEmpty(nv);
                        nv.FieldNameIsNotNull = ValueIsNotNull(nv,classObject);
                        list.Add(nv);
                    }
                }
                else
                {
                    strObject = classObject.Trim();

                    foreach (string field in cols)
                    {
                        string columnName = field.Trim();//字段
                        string propertyName = ps.ConvertStringToUpperOrLower(columnName, false);//属性
                        NameValue nv = new NameValue();
                        nv.Name = "@" + columnName;
                        nv.FieldName = ps.ConvertToSpecial(columnName, strTableName);
                        nv.MemberName = propertyName;
                        nv.Value = strObject + propertyName;
                        nv.FieldType = this.columnList.Where(c=>c.ColumnName==columnName).Single().TypeName;
                        nv.ValueIsNotNull = ValueIsNotNull(nv);
                        nv.ValueIsNotEmpty = ValueIsNotEmpty(nv);
                        nv.FieldNameIsNotNull = ValueIsNotNull(nv, classObject);
                        list.Add(nv);
                    }
                }
            }
            return list;
        }
        string ValueIsNotNull(NameValue nv)
        {
            //if (nv.FieldName.ToLower() == "sex")
            //{
            //    return "this." + nv.FieldName + "!=Sex.UnKnown";
            //}
            //switch (nv.FieldType)
            //{
                
                //case "DATE":
                //    return "this." + nv.FieldName + "!=Convert.ToDateTime(\"1900-1-1 0:00:00\")";
                //case "NUMBER":
                //    return "this." + nv.FieldName + "!=0";
                //case "VARCHAR2":
                    
                //default:
            return "!String.IsNullOrEmpty(this." + nv.MemberName + ")";
            //}
        }
        string ValueIsNotNull(NameValue nv, string classObject)
        {
            //if (nv.FieldName.ToLower() == "sex")
            //{
            //    return classObject + "." + nv.FieldName + "!=Sex.UnKnown";
            //}
            //switch (nv.FieldType)
            //{

            //    case "DATE":
            //        return classObject + "." + nv.FieldName + "!=Convert.ToDateTime(\"1900-1-1 0:00:00\")";
            //    case "NUMBER":
            //        return "this." + nv.FieldName + "!=0";
            //    case "VARCHAR2":

            //    default:
            return "!String.IsNullOrEmpty(" + classObject + "." + nv.MemberName + ")";
            //}
        }
        string ValueIsNotEmpty(NameValue nv)
        {
            //if (nv.FieldName.ToLower() == "sex")
            //{
            //    return "this." + nv.FieldName + "!=Sex.UnKnown";
            //}
            //switch (nv.FieldType)
            //{

            //    case "DATE":
            //        return "this." + nv.FieldName + ".ToShortDateString()!=\"1900-1-1\"";
            //    case "NUMBER":
            //        return "this." + nv.FieldName + "!=0";
            //    case "VARCHAR2":

                //default:
            return "this." + nv.MemberName + "!=\"\"";
            //}
        }
        /// <summary>
        /// 获取INSERT字段  Field1,Field2... or  @Field1,@Field2... 
        /// </summary>
        /// <param name="strChar">字段前要加的符号</param>
        /// <returns></returns>
        public string GetInsertFields(string strChar)
        {
            StringBuilder sbFields = new StringBuilder();
            if (columns == null)
            {
                throw new Exception("Param.columns 为空");
            }
            else
            {
                string[] list = AllColumns;
                foreach (string field in list)
                {
                    string columnName = field.Trim();//列名
                    sbFields.Append(strChar + columnName);
                    sbFields.Append(",");
                }
                if (sbFields.Length > 0)
                {//去掉最后的逗号
                    sbFields.Remove(sbFields.Length - 1, 1);
                }
            }

            return sbFields.ToString();
        }
        public string GetInsertValues(string classObject)
        {
            StringBuilder sbAddParams = new StringBuilder();
            List<NameValue> list = GetProperties(ColumnsType.AllColumns, classObject);
            sbAddParams.AppendLine("\" +");
            foreach (NameValue nv in list)
            {
                //if (nv.FieldName.ToLower() == "sex")//oracle
                //    sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "\"'\"+(int)" + nv.Value.ToString() + "+\"',\"+");
                //else
                //{
                    switch (nv.FieldType)
                    {
                        case "DATE":
                            sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "(String.IsNullOrEmpty(" + nv.Value.ToString() + ")?\"NULL\":"
                            + "JHDBCustomFunctionUse.ToDate(" + nv.Value.ToString() + ",false,false))+\",\"+");
                            break;
                        case "BLOB":
                            sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "\":fs,\"+");
                            break;
                        default:
                            sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "(String.IsNullOrEmpty(" + nv.Value.ToString() + ")?\"NULL\":" + "\"'\"+ESC(" + nv.Value.ToString() + ")+\"'\")+\",\"+");
                            //(data_source_ == "" ? "NULL" : "'"+ESC(data_source_)+"'") + "," +
                            break;
                    }
                    
                //}
            }
            if (sbAddParams.Length > 0)
            {//去掉最后的逗号 ,\+
                sbAddParams.Remove(sbAddParams.Length - ",\"+\r\n".Length, ",\"+\r\n".Length);
            }
            
            return sbAddParams.ToString();
        }
        /// <summary>
        /// 获取UPDATE字段  Field1=@Field1,Field1=@Field1...
        /// </summary>
        /// <returns></returns>
        public string GetUpdateFields()
        {
            StringBuilder sbFields = new StringBuilder();
            if (columns == null)
            {
                throw new Exception("Param.columns 为空");
            }
            else
            {
                string[] cols = NonIdentityColumns;
                foreach (string columnName in cols)
                {
                    string c = columnName.Trim();
                    sbFields.Append(c);
                    sbFields.Append("=@");
                    sbFields.Append(c);
                    sbFields.Append(",");
                }
                if (sbFields.Length > 0)
                {//去掉最后的逗号
                    sbFields.Remove(sbFields.Length - 1, 1);
                }
            }

            return sbFields.ToString();
        }
        public string GetUpdateValues(string classObject)
        {
            StringBuilder sbAddParams = new StringBuilder();
            List<NameValue> list = GetProperties(ColumnsType.NonIdentityColumns, classObject);
            sbAddParams.AppendLine("\"+");
            foreach (NameValue nv in list)
            {
                //if (nv.FieldName.ToLower() == "sex")//oracle
                //    sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "='\"+" + "(int)" + nv.Value.ToString() + "+\"',\"+");
                //else
                //{
                    switch (nv.FieldType)
                    {
                        case "DATE":
                            sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "=\"+" + "(String.IsNullOrEmpty(" + nv.Value.ToString() + ")?\"NULL\":"
                            + "JHDBCustomFunctionUse.ToDate(" + nv.Value.ToString() + " ,false,false))+\",\"+");
                            break;
                        case "BLOB":
                            sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "=:fs\"+\",\"+");
                            break;
                        default:
                            sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "=\"+(String.IsNullOrEmpty(" + nv.Value.ToString() + ")?\"NULL\":" + "\"'\"+ESC(" + nv.Value.ToString() + ")+\"'\")+\",\"+");
                            //(syn_data_count_.ToString()==""?"NULL": "'"+ESC(syn_data_count_)+"'")
                            break;
                    }
                //}
            }
            if (sbAddParams.Length > 4)
            {//去掉最后的逗号 ,\+
                sbAddParams.Remove(sbAddParams.Length - "+\",\"+\r\n".Length, "+\",\"+\r\n".Length);
            }
            list = GetProperties(ColumnsType.KeyColumns, classObject);
            sbAddParams.AppendLine("+\" WHERE \"+");
            foreach (NameValue nv in list)
            {
                switch (nv.FieldType)
                {
                    case "DATE":
                        sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "=\"+"
                            + "JHDBCustomFunctionUse.ToDate(" + nv.Value.ToString() + ",false,false)+\" and \"+");
                        break;
                    default:
                        sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "='\"+ESC(" + nv.Value.ToString() + ")+\"'and \"+");
                        break;
                }

            }
            sbAddParams.Remove(sbAddParams.Length - "+\"and \"+".Length, "+\"and \"+".Length);
            if (list[list.Count - 1].FieldType == "DATE")
            {
                sbAddParams.Remove(sbAddParams.Length - "+".Length, "+".Length);
            }
            else
            {
                sbAddParams.Append(" \"'\"");
            }
            sbAddParams.Append(" ;");
            return sbAddParams.ToString();
        }
        public string GetUpdateValuesForPID()
        {
            string classObject = "";
            StringBuilder sbAddParams = new StringBuilder();
            List<NameValue> list = GetProperties(ColumnsType.NonIdentityColumns, classObject);
            sbAddParams.AppendLine("\"+");
            List<NameValue> listPID=  list.Where(n => n.FieldName == "PIX_PATIENT_ID").ToList();
            if (listPID.Count == 1)
            {
                sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "\"PIX_PATIENT_ID='\"+" + listPID[0].Value.ToString() + "+\"'\"+");
            }
            
            list = GetProperties(ColumnsType.KeyColumns, classObject);
            sbAddParams.AppendLine(" \" WHERE \"+");
            foreach (NameValue nv in list)
            {
                switch (nv.FieldType)
                {
                    case "DATE":
                        sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "=\"+"
                            + "JHDBCustomFunctionUse.ToDate(" + nv.Value.ToString() + ",false,false)+\" and \"+");
                        break;
                    default:
                        sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "='\"+ESC(" + nv.Value.ToString() + ")+\"'and \"+");
                        break;
                }

            }
            //sbAddParams.Remove(sbAddParams.Length - "+\"and \"+".Length, "+\"and \"+".Length);
            //sbAddParams.Append("  \"'\";");
            sbAddParams.Remove(sbAddParams.Length - "+\"and \"+".Length, "+\"and \"+".Length);
            if (list[list.Count - 1].FieldType == "DATE")
            {
                sbAddParams.Remove(sbAddParams.Length - "+".Length, "+".Length);
            }
            else
            {
                sbAddParams.Append(" \"'\"");
            }
            sbAddParams.Append(" ;");
            return sbAddParams.ToString();
        }

        /// <summary>
        /// sxx20120502
        /// </summary>
        /// <param name="classObject"></param>
        /// <returns></returns>
        //public string GetUpdateValuesForObj(string classObject)
        //{
        //    StringBuilder sbAddParams = new StringBuilder();
        //    List<NameValue> list = GetProperties(ColumnsType.NonIdentityColumns, classObject);
        //    sbAddParams.AppendLine("\";");
        //    for (int i=0;i<list.Count;i++)
        //    {
        //        NameValue nv = list[i];
        //        sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "if (" + nv.FieldNameIsNotNull + "   && this." + nv.MemberName + " != " + classObject + "." + nv.FieldName + ")");
        //        sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "{");
        //        //if (nv.FieldName.ToLower() == "sex")//oracle
        //        //    sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "strSQL += \"" + nv.FieldName + "='\"+" + "(int)" + nv.Value.ToString() + "+\"',\";");
        //        //else
        //        //{
        //            switch (nv.FieldType)
        //            {
        //                case "DATE":
        //                    sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "strSQL += \"" + nv.FieldName + "=\"+" + "(String.IsNullOrEmpty(" + nv.Value.ToString() + ")?\"NULL\":"
        //                    + "JHDBCustomFunctionUse.ToDate(" + nv.Value.ToString() + ",false,false))+\",\";");
        //                    break;
        //                case "BLOB":
        //                    sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "strSQL += \"" + nv.FieldName + "=:fs,\";");
        //                    break;
        //                default:
        //                    sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "strSQL += \"" + nv.FieldName + "=\"+(String.IsNullOrEmpty(" + nv.Value.ToString() + ")?\"NULL\":" + "\"'\"+ESC(" + nv.Value.ToString() + ")+\"'\")+\",\";");
        //                    //"\"+(" + nv.Value.ToString() + "==\"\"?\" IS NULL\":" + "\"='\"+ESC(" + nv.Value.ToString() + ")+\"'and \"+");
        //                    //"=\"+(" + nv.Value.ToString() + "==\"\"?\"NULL\":" + "\"'\"+ESC(" + nv.Value.ToString() + ")+\"'\"),\"+");
        //                    break;
        //            }
        //        //}
        //        //if (i == list.Count - 1)//去掉最后的逗号 ,
        //        //{
        //        //    sbAddParams.Remove(sbAddParams.Length - "',\";\r\n".Length, "',\";\r\n".Length);
        //        //    sbAddParams.AppendLine("'\";");
        //        //}
        //        sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "bChanged = true;");
        //        sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "}");
        //    }
        //    sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "if (bChanged == true) ");
        //    sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "strSQL = strSQL.Substring(0, strSQL.Length - 1);");
        //    sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "else");
        //    sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "return \"\";");
        //    //if (sbAddParams.Length > 0)
        //    //{\+
        //    //    sbAddParams.Remove(sbAddParams.Length - ",\"+\r\n".Length, ",\"+\r\n".Length);
        //    //}
        //    list = GetProperties(ColumnsType.KeyColumns, classObject);
        //    sbAddParams.AppendLine(" strSQL += \" WHERE \"+");
        //    foreach (NameValue nv in list)
        //    {
        //        //sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "='\"+" + nv.Value.ToString() + "+\"'and \"+");
        //        switch (nv.FieldType)
        //        {
        //            case "DATE":
        //                sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "=\"+"
        //                    + "JHDBCustomFunctionUse.ToDate(" + nv.Value.ToString() + ",false,false)+\" and \"+");
        //                break;
        //            case "VARCHAR2":
        //                sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "='\"+ESC(" + nv.Value.ToString() + ")+\"'and \"+");
        //                break;
        //            default:
        //                sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "='\"+ESC(" + nv.Value.ToString() + ")+\"'and \"+");
        //                break;
        //        }
        //    }
        //    //sbAddParams.Remove(sbAddParams.Length - "+\"and \"+".Length, "+\"and \"+".Length);
        //    //sbAddParams.Append("  \"'\";");
        //    sbAddParams.Remove(sbAddParams.Length - "+\"and \"+".Length, "+\"and \"+".Length);
        //    if (list[list.Count - 1].FieldType == "DATE")
        //    {
        //        sbAddParams.Remove(sbAddParams.Length - "+".Length, "+".Length);
        //    }
        //    else
        //    {
        //        sbAddParams.Append(" \"'\"");
        //    }
        //    sbAddParams.Append(" ;");
        //    return sbAddParams.ToString();
        //}

        //sxx20120502
        public string GetUpdateValuesForObj(string classObject)
        {
            StringBuilder sbAddParams = new StringBuilder();
            List<NameValue> list = GetProperties(ColumnsType.NonIdentityColumns, classObject);
            sbAddParams.AppendLine("\";");
            for (int i = 0; i < list.Count; i++)
            {
                NameValue nv = list[i];
                sbAddParams.AppendLine(ps.tabLocalVar + "if(" + classObject + "." + nv.FieldName + " == null)");
                sbAddParams.AppendLine(ps.tabLocalVar + "{");
                sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "strSQL += \"" + nv.FieldName + "= NULL" + ",\";");
                sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "bChanged = true;");
                sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + classObject + "." + nv.FieldName + " = String.Empty;");
                sbAddParams.AppendLine(ps.tabLocalVar + "}");
                sbAddParams.AppendLine(ps.tabLocalVar + "else");
                sbAddParams.AppendLine(ps.tabLocalVar + "{");
                sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "if (" + nv.FieldNameIsNotNull + "   && this." + nv.MemberName + " != " + classObject + "." + nv.FieldName + ")");
                sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "{");
                //if (nv.FieldName.ToLower() == "sex")//oracle
                //    sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "strSQL += \"" + nv.FieldName + "='\"+" + "(int)" + nv.Value.ToString() + "+\"',\";");
                //else
                //{
                switch (nv.FieldType)
                {
                    case "DATE":
                        sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "strSQL += \"" + nv.FieldName + "=\"+" + "(String.IsNullOrEmpty(" + nv.Value.ToString() + ")?\"NULL\":"
                        + "JHDBCustomFunctionUse.ToDate(" + nv.Value.ToString() + ",false,false))+\",\";");
                        break;
                    case "BLOB":
                        sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "strSQL += \"" + nv.FieldName + "=:fs,\";");
                        break;
                    default:
                        sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "strSQL += \"" + nv.FieldName + "=\"+(String.IsNullOrEmpty(" + nv.Value.ToString() + ")?\"NULL\":" + "\"'\"+ESC(" + nv.Value.ToString() + ")+\"'\")+\",\";");
                        //"\"+(" + nv.Value.ToString() + "==\"\"?\" IS NULL\":" + "\"='\"+ESC(" + nv.Value.ToString() + ")+\"'and \"+");
                        //"=\"+(" + nv.Value.ToString() + "==\"\"?\"NULL\":" + "\"'\"+ESC(" + nv.Value.ToString() + ")+\"'\"),\"+");
                        break;
                }
                //}
                //if (i == list.Count - 1)//去掉最后的逗号 ,
                //{
                //    sbAddParams.Remove(sbAddParams.Length - "',\";\r\n".Length, "',\";\r\n".Length);
                //    sbAddParams.AppendLine("'\";");
                //}
                sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "bChanged = true;");
                sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "}");
                sbAddParams.AppendLine(ps.tabLocalVar + "}");
            }
            sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "if (bChanged == true) ");
            sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "strSQL = strSQL.Substring(0, strSQL.Length - 1);");
            sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "else");
            sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "return \"\";");
            //if (sbAddParams.Length > 0)
            //{\+
            //    sbAddParams.Remove(sbAddParams.Length - ",\"+\r\n".Length, ",\"+\r\n".Length);
            //}
            list = GetProperties(ColumnsType.KeyColumns, classObject);
            sbAddParams.AppendLine(" strSQL += \" WHERE \"+");
            foreach (NameValue nv in list)
            {
                //sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "='\"+" + nv.Value.ToString() + "+\"'and \"+");
                switch (nv.FieldType)
                {
                    case "DATE":
                        sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "=\"+"
                            + "JHDBCustomFunctionUse.ToDate(" + nv.Value.ToString() + ",false,false)+\" and \"+");
                        break;
                    case "VARCHAR2":
                        sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "='\"+ESC(" + nv.Value.ToString() + ")+\"'and \"+");
                        break;
                    default:
                        sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "='\"+ESC(" + nv.Value.ToString() + ")+\"'and \"+");
                        break;
                }
            }
            //sbAddParams.Remove(sbAddParams.Length - "+\"and \"+".Length, "+\"and \"+".Length);
            //sbAddParams.Append("  \"'\";");
            sbAddParams.Remove(sbAddParams.Length - "+\"and \"+".Length, "+\"and \"+".Length);
            if (list[list.Count - 1].FieldType == "DATE")
            {
                sbAddParams.Remove(sbAddParams.Length - "+".Length, "+".Length);
            }
            else
            {
                sbAddParams.Append(" \"'\"");
            }
            sbAddParams.Append(" ;");
            return sbAddParams.ToString();
        }


        public string GetUpdateValuesForPartial(string classObject)
        {
            StringBuilder sbAddParams = new StringBuilder();
            List<NameValue> list = GetProperties(ColumnsType.NonIdentityColumns, classObject);
            sbAddParams.AppendLine("\";");
            for (int i = 0; i < list.Count; i++)
            {
                NameValue nv = list[i];
                sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "if (" + nv.ValueIsNotNull + "   && this." + nv.MemberName + " != " + classObject + "." + nv.FieldName + ")");
                sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "{");
                //if (nv.FieldName.ToLower() == "sex")//oracle
                //    sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "strSQL += \"" + nv.FieldName + "='\"+" + "(int)" + nv.FieldName.ToString() + "+\"',\";");
                //else
                //{
                    switch (nv.FieldType)
                    {
                        case "DATE":
                            sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "strSQL += \"" + nv.FieldName + "=\"+" + "(String.IsNullOrEmpty(" + classObject + "." + nv.FieldName + ")?\"NULL\":"
                            + "JHDBCustomFunctionUse.ToDate(" + classObject + "." + nv.FieldName.ToString() + ",false,false))+\",\";");
                            break;
                        case "BLOB":
                            sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "strSQL += \"" + nv.FieldName + "=:fs,\";");
                            break;
                        default:
                            sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "strSQL += \"" + nv.FieldName + "=\"+(" + nv.FieldName.ToString() + "==\"\"?\"NULL\":" + "\"'\"+ESC(" + nv.FieldName.ToString() + ")+\"'\")+\",\";");//nv.FieldName.ToString()
                            //"\"+(" + nv.Value.ToString() + "==\"\"?\" IS NULL\":" + "\"='\"+ESC(" + nv.Value.ToString() + ")+\"'and \"+");
                            //"=\"+(" + nv.Value.ToString() + "==\"\"?\"NULL\":" + "\"'\"+ESC(" + nv.Value.ToString() + ")+\"'\"),\"+");
                            break;
                    }
                //}
                //if (i == list.Count - 1)//去掉最后的逗号 ,
                //{
                //    sbAddParams.Remove(sbAddParams.Length - "',\";\r\n".Length, "',\";\r\n".Length);
                //    sbAddParams.AppendLine("'\";");
                //}
                sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "bChanged = true;");
                sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "}");
            }
            sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "if (bChanged == true) ");
            sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "strSQL = strSQL.Substring(0, strSQL.Length - 1);");
            sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "else");
            sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "return \"\";");
            //if (sbAddParams.Length > 0)
            //{\+
            //    sbAddParams.Remove(sbAddParams.Length - ",\"+\r\n".Length, ",\"+\r\n".Length);
            //}
            list = GetProperties(ColumnsType.KeyColumns, classObject);
            sbAddParams.AppendLine(" strSQL += \" WHERE \"+");
            foreach (NameValue nv in list)
            {
                //sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "='\"+" + nv.Value.ToString() + "+\"'and \"+");
                switch (nv.FieldType)
                {
                    case "DATE":
                        sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "=\"+"
                            + "JHDBCustomFunctionUse.ToDate(" + nv.FieldName.ToString() +  ",false,false)+\" and \"+");
                        break;
                    case "VARCHAR2":
                        sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "='\"+ESC(" + nv.FieldName.ToString() + ")+\"'and \"+");
                        break;
                    default:
                        sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "='\"+ESC(" + nv.FieldName.ToString() + ")+\"'and \"+");
                        break;
                }
            }
            //sbAddParams.Remove(sbAddParams.Length - "+\"and \"+".Length, "+\"and \"+".Length);
            //sbAddParams.Append("  \"'\";");
            sbAddParams.Remove(sbAddParams.Length - "+\"and \"+".Length, "+\"and \"+".Length);
            if (list[list.Count - 1].FieldType == "DATE")
            {
                sbAddParams.Remove(sbAddParams.Length - "+".Length, "+".Length);
            }
            else
            {
                sbAddParams.Append(" \"'\"");
            }
            sbAddParams.Append(" ;");
            return sbAddParams.ToString();
        }

        public string GetUpdatePropFromNotNull(string classObject)
        {
            StringBuilder sbAddParams = new StringBuilder();
            List<NameValue> list = GetProperties(ColumnsType.NonIdentityColumns, classObject);
            for (int i = 0; i < list.Count; i++)
            {
                NameValue nv = list[i];
                sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "if (" + nv.FieldNameIsNotNull + "  )");
                sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "{");
                sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "this." + nv.MemberName + " = " + classObject + "." + nv.FieldName + ";");
                sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + "}");
            }
            return sbAddParams.ToString();
        }
        /// <summary>
        /// 获取DELETE字段  AND Field1=@Field1 AND Field1=@Field1...
        /// </summary>
        /// <returns></returns>
        public string GetDeleteFields()
        {
            StringBuilder sbFields = new StringBuilder();
            if (columns == null)
            {
                throw new Exception("Param.columns 为空");
            }
            else
            {
                string[] cols = KeyColumns;
                foreach (string columnName in cols)
                {
                    string c = columnName.Trim();
                    sbFields.Append(" AND ");
                    sbFields.Append(c);
                    sbFields.Append("=@");
                    sbFields.Append(c);
                }
            }

            return sbFields.ToString();
        }
        public string GetDeleteValues(string classObject)
        {
            StringBuilder sb = new StringBuilder();
            ProcString ps = new ProcString();
            DataTable dtType = dsTableInfo.Tables[1].Copy();
            List<NameValue> list = GetProperties(ColumnsType.KeyColumns, classObject);
            string[] keys = KeyColumns;
            sb.Append("\"+");
            foreach (string field in keys)
            {
                string f = field.Trim();
                DataRow[] drs = dtType.Select("Column_name='" + f + "'");
                if (drs.Length > 0)
                {
                    string type = ps.ConvertType(drs[0]["Type"].ToString(), f) + " ";
                    string var = ps.ConvertStringToUpperOrLower(f, false);
                    //sb.Append(" \"" + f + "='\"+" + var + "+\"'and \"+");
                    //----
                    //switch (type.Trim())
                    //{
                    //    case "DateTime":
                    //        sb.Append(" \"" + f + "=\"+"
                    //            + "JHDBCustomFunctionUse.ToDate(" + var + ".ToString(),false,false)+\" and \"+");
                    //        break;
                    //    case "String":
                    //    default:
                    //        sb.Append(" \"" + f + "='\"+" + var + "+\"'and \"+");
                    //        break;
                    //}
                    foreach (NameValue nv in list)
                    {
                        f = f.ToUpper();
                        if (f == nv.FieldName.ToUpper())
                        {
                            //sbAddParams.Append(ps.tabIfLocalVarTop1 + "\"" + nv.FieldName + "='\"+" + nv.Value.ToString() + "+\"'and \"+");
                            switch (nv.FieldType)
                            {
                                case "DATE":
                                    sb.Append(" \"" + f + "=\"+"
                                + "JHDBCustomFunctionUse.ToDate(" + var + ".ToString(),false,false)+\" and \"+");
                                    break;
                                default:
                                    sb.Append(" \"" + f + "='\"+ESC(" + var + ")+\"'and \"+");
                                    //"\"+(" + nv.Value.ToString() + "==\"\"?\" IS NULL\":" + "\"='\"+ESC(" + nv.Value.ToString() + ")+\"'and \"+");
                                    //"=\"+(" + nv.Value.ToString() + "==\"\"?\"NULL\":" + "\"'\"+ESC(" + nv.Value.ToString() + ")+\"'\"),\"+");
                                    break;
                            }
                        }
                    }

                }
            }
            //if (sb.Length > 0)
            //{
            //    sb.Remove(sb.Length - "+\"and \"+".Length, "+\"and \"+".Length);
            //}
            //sb.Append(" \"'\";");
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - "+\"and \"+".Length, "+\"and \"+".Length);
                string Type= this.columnList.Where(c => c.ColumnName == keys[keys.Length - 1]).Single().TypeName;
                if (Type == "DATE")
                {
                    sb.Remove(sb.Length - "+".Length, "+".Length);
                }
                else
                {
                    sb.Append(" \"'\"");
                }
            }
            sb.Append(" ;");
            return sb.ToString();
        
        }
        public string GetIndexSQL(string classObject)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                ProcString ps = new ProcString();
                DataTable dtType = dsTableInfo.Tables[1].Copy();
                string[] keys = KeyColumns;
                if (keys != null)
                {
                    foreach (string field in keys)
                    {
                        string f = field.Trim();
                        sb.Append(f + ",");
                        //    sb.Append(" \"" + f + "=\"+"
                        //+ "JHDBCustomFunctionUse.ToDate(" + var + ".ToString(),false,false)+\" and \"+");

                    }
                    if (keys.Length > 0 && !string.IsNullOrEmpty(keys[0]))
                    {
                        sb.Remove(sb.Length - ",".Length, ",".Length);
                    }
                    return sb.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                string st = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// 获取WHERE字段  Field1=@Field1 strCondition Field1=@Field1...
        /// </summary>
        /// <returns></returns>
        public string GetWhereFields(ColumnsType type,string strCondition)
        {
            StringBuilder sbFields = new StringBuilder();
            if (columns == null)
            {
                throw new Exception("Param.columns 为空");
            }
            else
            {
                sbFields.Append(strCondition);
                string[] cols = null;
                switch (type)
                {
                    case ColumnsType.AllColumns:
                        cols = columns;
                        break;
                    case ColumnsType.NonIdentityColumns:
                        cols = NonIdentityColumns;
                        break;
                    case ColumnsType.KeyColumns:
                        cols = KeyColumns;
                        break;
                }
                foreach (string columnName in cols)
                {
                    sbFields.Append(columnName);// + "=@" + columnName);
                    sbFields.Append("=@");
                    sbFields.Append(columnName);
                    sbFields.Append(strCondition);
                }
                if (sbFields.Length > 0)
                {
                    sbFields.Remove(sbFields.Length - strCondition.Length, strCondition.Length);
                }
            }

            return sbFields.ToString();
        }
        /// <summary>
        /// 获取参数字符串  "idb.AddParameter("@colName",value);"
        /// </summary>
        /// <param name="type">列集合类型</param>
        /// <param name="daoObject">数据访问对象名</param>
        /// <returns></returns>
        public string GetParamaters(ColumnsType type, string daoObject)
        {
            StringBuilder sbAddParams = new StringBuilder();
            List<NameValue> list = GetProperties(type);
            foreach (NameValue nv in list)
            {
                string strValue = ps.ConvertStringToUpperOrLower(nv.Value.ToString(), false);
                sbAddParams.AppendLine(ps.tabLocalVar + daoObject + ".AddParameter(\"" + nv.Name + "\", " + strValue + ");");
            }
            return sbAddParams.ToString();
        }
        /// <summary>
        /// 获取参数字符串  "idb.AddParameter("@colName",value);"
        /// </summary>
        /// <param name="type">列集合类型</param>
        /// <param name="daoObject">数据访问对象名</param>
        /// <returns></returns>
        public string GetParamaters(ColumnsType type, string daoObject, string classObject)
        {
            StringBuilder sbAddParams = new StringBuilder();
            List<NameValue> list = GetProperties(type, classObject);
            foreach (NameValue nv in list)
            {
                sbAddParams.AppendLine(ps.tabLocalVar + "if (" + nv.Value.ToString() + " == null)");
                sbAddParams.AppendLine(ps.tabLocalVar + "{");
                sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + daoObject + ".AddParameter(\"" + nv.Name + "\", DBNull.Value);");
                sbAddParams.AppendLine(ps.tabLocalVar + "}");
                sbAddParams.AppendLine(ps.tabLocalVar + "else");
                sbAddParams.AppendLine(ps.tabLocalVar + "{");
                sbAddParams.AppendLine(ps.tabIfLocalVarTop1 + daoObject + ".AddParameter(\"" + nv.Name + "\", " + nv.Value.ToString()+");");
                sbAddParams.AppendLine(ps.tabLocalVar + "}");
            }
            return sbAddParams.ToString();
        }
        /// <summary>
        /// 获取参数字符串  "new SqlParameter("@WorkerID", SqlDbType.Int),"
        /// </summary>
        /// <param name="type">列集合类型</param>
        /// <param name="daoObject">数据访问对象名</param>
        /// <returns></returns>
        public string GetParamaters_AData(ColumnsType type, string classObject)
        {
            StringBuilder sbAddParams = new StringBuilder();
            StringBuilder sbAddValues = new StringBuilder();
            //List<NameValue> list = GetProperties(type, classObject);
            int index=0;
            sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "#region 参数");
            sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "SqlParameter[] parms = {");
            foreach (ColumnInfo col in this.columnList)
            {
                if (type == ColumnsType.NonIdentityColumns)
                    if (col.ColumnName == ColumnsList[0].ColumnName )//如果是主键，待改进
                        continue;
                if (col.ColumnName == "RowStamp" || col.ColumnName == "IsDeleted")//如果是时间戳
                        continue;
                string columnName = col.ColumnName;//列名
                string columnType = ps.ConvertSqlDbType(col.TypeName);//列数据类型
                if (col.TypeName=="varchar")
                    sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "     new SqlParameter(\"@" + columnName + "\", SqlDbType." + columnType + "," +col.Length+ "),");
                else
                    sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "     new SqlParameter(\"@" + columnName + "\", SqlDbType." + columnType + "),");
                sbAddValues.AppendLine(ps.tabIfLocalVarTop2 + "parms[" + index++ + "].Value = " + classObject + "." + columnName + ";");
            }
            sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "     #endregion");
            sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "     };");
            sbAddParams.Append(sbAddValues);
            return sbAddParams.ToString();
        }

        public string GetParamaters_Proc(ColumnsType type, string classObject)
        {
            StringBuilder sbAddParams = new StringBuilder();
            StringBuilder sbAddValues = new StringBuilder();
            //List<NameValue> list = GetProperties(type, classObject);
            int index = 0;
            foreach (ColumnInfo col in this.columnList)
            {
                if (type == ColumnsType.NonIdentityColumns)
                    if (col.ColumnName == ColumnsList[0].ColumnName)//如果是主键，待改进
                        continue;
                if (col.ColumnName == "RowStamp" || col.ColumnName == "IsDeleted")//如果是时间戳
                    continue;
                string columnName = col.ColumnName;//列名
                string columnType = col.TypeName;//列数据类型
                if (col.TypeName == "varchar")
                    sbAddParams.AppendLine(ps.tabMember + "@" + columnName + " " + columnType + "(" + col.Length + "),");
                else
                    sbAddParams.AppendLine(ps.tabMember + "@" + columnName + " " + columnType + ",");
                sbAddValues.AppendLine(ps.tabMember + "parms[" + index++ + "].Value = " + classObject + "." + columnName + ";");
            }
            sbAddParams.Append(sbAddValues);
            return sbAddParams.ToString();
        }
        public string GetDelete_AData( string classObject)
        {
            StringBuilder sbAddParams = new StringBuilder();
            StringBuilder sbAddValues = new StringBuilder();
            
            String DeleteParam1 = ColumnsList[0].ColumnName;
            String DeleteParam1Type="";
            String DeleteParam2="";
            String DeleteParam2Type="";
            //List<NameValue> list = GetProperties(type, classObject);
            int index = 0;
            sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "#region 参数");
            sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "SqlParameter[] parms = {");
            bool HaveUserID = false;
            foreach (ColumnInfo col in this.columnList)
            {
                bool boolContinue=false;
                if (col.ColumnName == "UserID")//如果是UserID
                {
                    boolContinue = true;
                    HaveUserID = true;

                    DeleteParam2 = col.ColumnName;
                    DeleteParam2Type = ps.ConvertType(col.TypeName, col.ColumnName);
                }
                if (col.ColumnName == ColumnsList[0].ColumnName )//如果是主键，待改进
                {
                    DeleteParam1Type = ps.ConvertType(col.TypeName, col.ColumnName);
                    boolContinue = true;
                }
                if (boolContinue == true)
                {
                    string columnName = col.ColumnName;//列名
                    string columnType = ps.ConvertSqlDbType(col.TypeName);//列数据类型
                    if (col.TypeName == "varchar")
                        sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "new SqlParameter(\"@" + columnName + "\", SqlDbType." + columnType + "," + col.Length + "),");
                    else
                        sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "new SqlParameter(\"@" + columnName + "\", SqlDbType." + columnType + "),");
                    sbAddValues.AppendLine(ps.tabIfLocalVarTop2 + "parms[" + index++ + "].Value = " +  columnName + ";");
                }
            }
            sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "#endregion");
            sbAddParams.AppendLine(ps.tabIfLocalVarTop2 + "};");
            sbAddParams.Append(sbAddValues);


            String DeleteParams=DeleteParam1Type + " " + DeleteParam1;
            String DeleteParamsIf = DeleteParam1  + " <= 0";
            if (HaveUserID)
            {
                DeleteParams += "," + DeleteParam2Type + " " + DeleteParam2;
                DeleteParamsIf += " || " + DeleteParam2 + " <= 0";
            }
            StringBuilder sbBody = new StringBuilder();
            String className=ps.ConvertStringToUpperOrLower( classObject,true);
            sbBody.AppendLine(ps.tabMember + "public virtual Int32 Delete" + className + "(" + DeleteParams + ")");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "if (" + DeleteParamsIf + ")");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "return Error.DALError.PARM_NULL;");
            sbBody.AppendLine(ps.tabLocalVar + "SqlDataReader reader = null;");
            sbBody.AppendLine(ps.tabLocalVar + "try");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.Append(sbAddParams);//---------------
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "Int32 affected = db_helper_.ExecuteReader(command_type_, \"P_" + className + "_DEL\", out reader, parms);");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "if (affected != 0)");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "System.Diagnostics.Debug.Assert(false, \"Delete" + className + "Cache\");");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabLocalVar + "catch (System.Exception ex)");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "Exception.OrimsException.Instance.WriteLog(ex);");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "return Error.DBError.ConvertDBException(ex);");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabLocalVar + "finally");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "if (reader != null && !reader.IsClosed)");
            sbBody.AppendLine(ps.tabIfLocalVarTop2 + "reader.Close();");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabLocalVar + "return Error.DALError.SUCC;");
            sbBody.AppendLine(ps.tabMember + "}");
            return sbBody.ToString();
        }
        /// <summary>
        /// 获取 对对象的属性的设置的字符串  user.ID = Convert.ToInt32(dr["columnName"]);
        /// </summary>
        /// <param name="columnName">列名也即对象的属性</param>
        /// <returns></returns>
        public string GetPropertyString(string columnName)
        {
            string ret = "";
            string obj = ClassObject;
            ProcString ps = new ProcString();
            string propertyName = ps.ConvertStringToUpperOrLower(columnName, true);
            DataTable dtType = dsTableInfo.Tables[1];
            DataRow[] drs = dtType.Select("Column_name='" + columnName+"'");
            if (drs.Length > 0)
            {                               
                //ret = "if (dr[\"" + columnName + "\"] != DBNull.Value) " + obj + "." + propertyName + " = (" + ps.ConvertType(drs[0]["Type"].ToString()) + ")dr[\"" + columnName + "\"];";
                ret = "if (dr[\"" + columnName + "\"] != DBNull.Value) " + obj + "." + propertyName + " = " + ps.ConvertField(drs[0]["Type"].ToString(), "dr[\"" + columnName + "\"]")+";";
            }
            return ret;
        }
        /// <summary>
        /// 获取方法参数字符串  string columnName, ......
        /// </summary>
        /// <returns></returns>
        public string GetFunctionParams()
        {
            return GetFunctionParams("");
        }
        /// <summary>
        /// 获取方法参数字符串  string varColumnName, ......
        /// </summary>
        /// <returns></returns>
        public string GetFunctionParams(string varPrefix)
        {
            StringBuilder sb = new StringBuilder();
            ProcString ps = new ProcString();
            DataTable dtType = dsTableInfo.Tables[1].Copy();
            string[] keys = KeyColumns;
            foreach (string field in keys)
            {
                string f = field.Trim();
                DataRow[] drs = dtType.Select("Column_name='" + f + "'");
                if (drs.Length > 0)
                {
                    string type = ps.ConvertType(drs[0]["Type"].ToString(), f,this) + " ";
                    string var = varPrefix + ps.ConvertStringToUpperOrLower(f,false) + ",";
                    sb.Append(type + var);
                }
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }

    }
}
