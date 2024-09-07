using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
//
using Common.DataAccess;
//
namespace ClassGenerate.Proc
{
    public class ProcTable
    {
        //
        //IDBOperator idb = DBOperator.GetInstance();
        //

        public ProcTable()
        {
            da = ClassGenerator.da;
        }

        /// <summary>
        /// 数据库访问
        /// </summary>
        DataAccess da;// = new DataAccess();

        #region 设置连接字符串
        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="cn"></param>
        public void SetConnectionString(Connection cn)
        {
            da.strConnection = cn.ConnectionString;
        }
        #endregion

        #region 获取服务器上的所有数据库
        /// <summary>
        /// 获取服务器上的所有数据库
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllDataBase(bool includeSysDB)
        {
            DataTable ret = new DataTable();
            try
            {

                string sql="";
                switch (da.DbConnectType)
                {
                    case DBConnectType.ORACLE:
                        sql = "SELECT Table_name as name FROM  User_tables ";
                        break;
                    case DBConnectType.SQLSERVER:
                        if (includeSysDB)
                        {
                            sql = "select * from master.dbo.sysdatabases";
                        }
                        else
                        {
                            sql = "select * from master.dbo.sysdatabases where dbid > 4";
                        }
                        break;
                }
                
                ret = da.ReturnDataTable(sql);
            }
            catch (Exception e)
            {
                throw e;
//                MessageBox.Show(e.Message);
            }
            return ret;
        }
        #endregion

        #region 读取当前数据库中所有用户表
        /// <summary>
        /// 读取当前数据库中所有用户表
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllTable()
        {
            DataTable ret = new DataTable();
            try
            {
                //insert into dbo.tablename([TableName]) select [name] From sysobjects where xtype='u' and name<>'sysdiagrams' order by [name]
                //string sql = "select [name] from sysobjects where xtype='u' and name<>'sysdiagrams' order by [name]";
                string sql = "";
                //insert into dbo.tablename([TableName]) select [name] From sysobjects where xtype='u' and name<>'sysdiagrams' order by [name]
                //string sql = "select [name] from sysobjects where xtype='u' and name<>'sysdiagrams' order by [name]";
                switch (da.DbConnectType)
                {
                    case DBConnectType.ORACLE:
                        //sql = "SELECT Table_name as TableName,0 as IsDeleted  FROM  User_tables where Table_name like 'JH%'";
                        sql = "SELECT Table_name as TableName,0 as IsDeleted  FROM  User_tables   order by Table_name";
                        break;
                    case DBConnectType.SQLSERVER:
                        sql = "select TableName,IsDeleted From dbo.TableName ";
                        break;
                }
                ret = da.ReturnDataTable(sql);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                throw e;
            }
            return ret;
        }
        #endregion

        #region 获取所有表的列说明信息
        /// <summary>
        /// 获取所有表的列说明信息
        /// </summary>
        /// <returns></returns>
        public DataSet GetTablesColumnDecription(List<string> tableNames)
        {
            DataSet ret = new DataSet();
            string sql = "";
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < tableNames.Count; i++)
            {
                switch (da.DbConnectType)
                {
                    case DBConnectType.ORACLE:
                        sql = "SELECT USER_TAB_COLS.COLUMN_NAME as ColumnName ,user_col_comments.comments as ColumnDescription FROM USER_TAB_COLS inner join user_col_comments on user_col_comments.TABLE_NAME=USER_TAB_COLS.TABLE_NAME and user_col_comments.COLUMN_NAME=USER_TAB_COLS.COLUMN_NAME where USER_TAB_COLS.TABLE_NAME = '" + tableNames[i] + "'";
                        DataTable daReturnDataSetTablesClone = da.ReturnDataSet(sql).Tables[0].Copy();
                        daReturnDataSetTablesClone.TableName = tableNames[i];
                        //去除换行符--LSJ--2012-11-20
                        for (int j = 0; j < daReturnDataSetTablesClone.Rows.Count; j++)
                        {
                            daReturnDataSetTablesClone.Rows[j]["ColumnDescription"] = TrimLine(daReturnDataSetTablesClone.Rows[j]["ColumnDescription"].ToString());  
                        }
                        //-------------
                        ret.Tables.Add( daReturnDataSetTablesClone);
                        break;
                    case DBConnectType.SQLSERVER:
                        sb.AppendFormat(@"
 SELECT objname as ColumnName, [value] as ColumnDescription
 FROM ::fn_listextendedproperty('MS_Description', 'user', 'dbo', 'table', '{0}', 'column', DEFAULT);", tableNames[i]);
                        sql = sb.ToString();
                        ret = da.ReturnDataSet(sql);
                        break;
                }
                
                
            }
            
            if (ret.Tables.Count > 0)
            {
                for (int i = 0; i < tableNames.Count; i++)
                {
                    ret.Tables[i].TableName = tableNames[i];
                }
            }

            return ret;
        }
        #endregion

        #region 获取表的相关信息 Tables[0]表名, Tables[1]列类型信息, Tables[2] 标识列,Tables[5]主键列信息信息
        /// <summary>
        /// 获取表的相关信息 Tables[0]表名, Tables[1]列类型信息, Tables[2] 标识列,Tables[5]主键列信息信息
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>DataSet  Tables[0]表名, Tables[1]列类型信息, Tables[2] 标识列,Tables[5]主键列信息信息</returns>
        public DataSet GetTableInfo(string tableName)
        {
            DataSet ret = new DataSet();
            try
            {
                switch (da.DbConnectType)
                {
                    case DBConnectType.ORACLE:
                        ret.Tables.Add(GetOracleTableDesciption(tableName).Tables[0].Copy());
                        ret.Tables[0].TableName = "TableInfo";
                        ret.Tables.Add(da.ReturnDataSet("SELECT  USER_TAB_COLS.COLUMN_NAME, USER_TAB_COLS.DATA_TYPE as TYPE,  USER_TAB_COLS.COLUMN_NAME, USER_TAB_COLS.DATA_LENGTH as LENGTH, USER_TAB_COLS.DATA_Precision as NumLENGTH FROM USER_TAB_COLS inner join user_col_comments on user_col_comments.TABLE_NAME=USER_TAB_COLS.TABLE_NAME and user_col_comments.COLUMN_NAME=USER_TAB_COLS.COLUMN_NAME where USER_TAB_COLS.TABLE_NAME = '" + tableName + "'").Tables[0].Copy());
                        ret.Tables[1].TableName = "TableColumnInfo";
                        ret.Tables.Add(da.ReturnDataSet("SELECT USER_TAB_COLS.COLUMN_NAME,USER_TAB_COLS.DATA_TYPE as TYPE   FROM USER_TAB_COLS inner join user_col_comments on user_col_comments.TABLE_NAME=USER_TAB_COLS.TABLE_NAME and user_col_comments.COLUMN_NAME=USER_TAB_COLS.COLUMN_NAME where USER_TAB_COLS.TABLE_NAME = '" + tableName + "' and  rownum <=1").Tables[0].Copy());
                        ret.Tables[2].TableName = "ColumnName";
                        ret.Tables.Add(new DataTable("table3"));
                        ret.Tables.Add(new DataTable("table4"));
                        //DataTable table = ret.Tables[1].Copy();
                        //table.TableName = "KeyColumn";
                        //ret.Tables.Add(table);
                        ret.Tables.Add(da.ReturnDataSet("select col.column_name,con.constraint_name from user_constraints con,  user_cons_columns col where con.constraint_name = col.constraint_name and con.constraint_type='P' and col.table_name = '" + tableName + "'").Tables[0].Copy());
                        ret.Tables[5].TableName = "KeyColumn";
                        break;
                    case DBConnectType.SQLSERVER:
                        da.ClearParameters();
                        da.AddParameter("@objname", tableName);
                        ret = da.RunProcReturnDataSet("sp_help");// dsColumnType.Tables[1] 字段数据类型 
                        break;
                }
                
            }
            catch (Exception e)
            {
                throw e;
            }
            return ret;
        } 
        #endregion

        #region 获取表的列说明信息
        /// <summary>
        /// 获取表的列说明信息
        /// </summary>
        /// <param name="tableName">要查询的表</param>
        /// <returns></returns>
        public DataTable GetTableColumnDecription(string tableName)
        {
            DataTable ret = new DataTable();
            try
            {
                string sql = @"
 SELECT objname as ColumnName, [value] as ColumnDescription
 FROM ::fn_listextendedproperty('MS_Description', 'user', 'dbo', 'table', '" + tableName + "', 'column', DEFAULT);";
                ret = da.ReturnDataTable(sql);
                ret.TableName = tableName;
            }
            catch (Exception e)
            {
                throw e;
            }
            return ret;
        }
        #endregion
        //去除换行符--LSJ--2012-11-20
        private string TrimLine(string strComment)
        {
            string strReturn = strComment.Replace("\n", " ");
            return strReturn;
        }
        #region 获取表描述
        /// <summary>
        /// 获取表描述
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public string GetTableDesciption(string tableName)
        {
            string tableDecription = "";
            try
            {
                switch (da.DbConnectType)
                {
                    case DBConnectType.ORACLE:
                        DataSet ret = GetOracleTableDesciption(tableName);
                        if (ret.Tables[0].Rows.Count > 0)
                        {
                            tableDecription =TrimLine( ret.Tables[0].Rows[0]["comments"].ToString());
                        }
                        break;
                    case DBConnectType.SQLSERVER:
                        string sql = @"
        SELECT objname as TableName, [value] as TableDescription
        FROM fn_listextendedproperty (
        'MS_Description', 'user', 'dbo', 'table', '" + tableName + @"', NULL, NULL)";
                        DataTable dtTableDescription = da.ReturnDataTable(sql);
                        if (dtTableDescription.Rows.Count > 0)
                        {
                            tableDecription = dtTableDescription.Rows[0]["TableDescription"].ToString();
                        }
                        break;
                }
                
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                throw e;
            }
            return tableDecription;
        }
        DataSet GetOracleTableDesciption(string tableName)
        {
            DataSet ret = da.ReturnDataSet("select TABLE_NAME,comments From user_tab_comments where user_tab_comments.TABLE_NAME = '" + tableName + "'");
            ret.Tables[0].TableName = tableName;
            return ret;
        }
#endregion

        #region 多表时,获取要生成的表
        /// <summary>
        /// 多表时,获取要生成的表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<string> GetTables(ListView list)
        {
            List<string> ret = new List<string>();
            foreach (int i in list.CheckedIndices)
            {
                ret.Add(list.Items[i].SubItems[1].Text);
            }
            return ret;
        }
        #endregion

        #region 多表时,生成单表实体字符串
        
        /// <summary>
        /// 多表时,生成单表实体字符串
        /// </summary>
        /// <param name="dsColumnType">列类型表</param>
        /// <param name="dtColumnDescription">列描述表</param>
        /// <param name="dtDescription">表描述(表)</param>
        /// <returns></returns>
        public string GetTable(DataTable dtColumnType
            , DataTable dtColumnDescription
            , string strTableDescription
            , string strPrefix
            , string strNamespace)
        {
            StringBuilder ret = new StringBuilder();
            string strTableName = dtColumnType.TableName;
            
            
            //lsj--构造方法1所用的参数默认值
            string strDefaultValue="";
            //lsj--构造方法2所用的参数列表
            string strPara = "";
            //lsj--构造方法3的赋值
            int index = 0;
            string strIndex = "";
            string strValue = "";
            //lsj--构造方法1所用的赋值语句。
            string[] strSetValues1 = new string[dtColumnType.Rows.Count];
            //lsj--构造方法2所用的赋值语句。
            string[] strSetValues2 = new string[dtColumnType.Rows.Count];
           
            ProcString ps = new ProcString();
            ps.SetTabs(strNamespace);
            //lsj--类名
            string strClassName = strTableName;//ps.GetClassName(strTableName);//lsj
            
            //lsj--小写的类名--在此做类的对象来用。
            string strLowerClassName = ps.ConvertStringToUpperOrLower(strClassName, false);

            TableInfo tableInfo = new TableInfo(strNamespace, strPrefix, strTableName);
            if (string.IsNullOrEmpty(tableInfo.strPKName))
                return "";
            if (strTableName == "JHPIX_PAT_ADDRESS")
                tableInfo.strForeignKeyColumns = "PIX_ADDRESS_ID";
            //---------------------------------------------
            #region  Member Variables / Attributes  属性
            StringBuilder sbBody = new StringBuilder();

            sbBody.AppendLine(ps.tabMember + "#region Member Variables");
            for (int i=0;i< dtColumnType.Rows.Count;i++)
            {
                DataRow drType = dtColumnType.Rows[i];
                string columnName = drType["Column_name"].ToString();//列名
                string columnType = drType["Type"].ToString();//列数据类型
                string firstLowerColumnName = "";//首字母小写,用于字段
                
                #region 以下为字段
                firstLowerColumnName = ps.ConvertStringToUpperOrLower(columnName, false);
                //if (tableInfo.KeyColumns.Length>1 && tableInfo.KeyColumns[0] == columnName && tableInfo.KeyType == "NUMBER")
                //    sbBody.AppendLine(ps.tabMember + "private Int64 " + firstLowerColumnName + ";");
                //else
                //{
                    sbBody.AppendLine(ps.tabMember + "private " + ps.ConvertType(columnType, columnName, tableInfo) + " " + firstLowerColumnName + ";");
                //}
                #endregion
            }
            sbBody.AppendLine(ps.tabMember + "#endregion");
            sbBody.AppendLine(ps.tabMember + "#region Attributes");
            for (int n = 0; n < dtColumnType.Rows.Count; n++)
            {
                DataRow drType = dtColumnType.Rows[n];
                string columnName = drType["Column_name"].ToString();//列名
                string columnType = drType["Type"].ToString();//列数据类型
                string firstLowerColumnName = "";//首字母小写,用于字段
                string firstUpperColumnName = "";//首字母大写,用于属性
                string strColumnDescription = "";//列描述
                firstLowerColumnName = ps.ConvertStringToUpperOrLower(columnName, false);
                firstUpperColumnName = ps.ConvertStringToUpperOrLower(columnName, true);
                if (null != dtColumnDescription)
                {
                    for (int i = 0; i < dtColumnDescription.Rows.Count; i++)
                    {
                        string strColumnName = dtColumnDescription.Rows[i]["ColumnName"].ToString();

                        if (strColumnName == columnName)
                        {
                            strColumnDescription = dtColumnDescription.Rows[i]["ColumnDescription"].ToString();

                            dtColumnDescription.Rows.RemoveAt(i);
                            break;
                        }
                    }
                }

                #region 以下为属性
                if (!string.IsNullOrEmpty(strColumnDescription))
                {
                    sbBody.AppendLine(ps.tabMember + "/// <summary>");
                    sbBody.AppendLine(ps.tabMember + "/// " + strColumnDescription);
                    sbBody.AppendLine(ps.tabMember + "/// <summary>");
                    
                }
                sbBody.AppendLine(ps.tabMember + "[DataMember]");//lsj
                firstUpperColumnName = ps.ConvertStringToUpperOrLower(columnName, true);
                firstUpperColumnName= ps.ConvertToSpecial(firstUpperColumnName,strTableName);
                
                //if (n == 0 && tableInfo.KeyColumns[0] == columnName && tableInfo.KeyType == "NUMBER")
                //    sbBody.AppendLine(ps.tabMember + "public Int64 " + firstUpperColumnName);
                //else
                //{
                        sbBody.AppendLine(ps.tabMember + "public " + ps.ConvertType(columnType, columnName, tableInfo) + " " + firstUpperColumnName);
                //}
                sbBody.AppendLine(ps.tabMember + "{");
                sbBody.AppendLine(ps.tabLocalVar + "get{ return " + firstLowerColumnName + "; }");
                sbBody.Append(ps.tabLocalVar + "set{ " + firstLowerColumnName + " = value; ");
                if (columnName != "IsDeleted")
                    sbBody.AppendLine("NotifyPropertyChanged(\"" + firstUpperColumnName + "\");}");
                else
                    sbBody.AppendLine("}");
                sbBody.AppendLine(ps.tabMember + "}");
                //----------------------------------------------
                strSetValues1[index] = ps.tabLocalVar + "this." + firstLowerColumnName + " = " + firstLowerColumnName + ";//" + strColumnDescription;
                strSetValues2[index] = ps.tabLocalVar + "this." + firstLowerColumnName + " = " + strLowerClassName + "." + firstLowerColumnName + ";//" + strColumnDescription;
                if (index == 0)
                {
                    //lsj--构造方法1所用的参数默认值
                    strDefaultValue +=  ps.ConvertDefaultValue(columnType,columnName);
                    //lsj--构造方法2所用的参数列表
                    //if (n == 0 && tableInfo.KeyColumns[0] == columnName && tableInfo.KeyType == "NUMBER")
                    //    strPara += " Int64 " + firstLowerColumnName;
                    //else
                        strPara += ps.ConvertType(columnType, columnName, tableInfo) + " " + firstLowerColumnName;
                    //lsj--构造方法3的赋值
                    strIndex +=  "{" + index.ToString() + "}";
                    strValue += "this." + firstLowerColumnName;
                }
                else//前面加，
                {
                    //lsj--构造方法1所用的参数默认值
                    if (columnName == "")
                    {
                        strDefaultValue += ",0";
                    }
                    else
                    {
                        strDefaultValue += "," + ps.ConvertDefaultValue(columnType,columnName);
                    }
                    //lsj--构造方法2所用的参数列表
                    strPara += "," + ps.ConvertType(columnType, columnName,tableInfo) + " " + firstLowerColumnName;
                    //lsj--构造方法3的赋值
                    strIndex += "," + "{" + index.ToString() + "}";
                    strValue += ",this." + firstLowerColumnName;
                }
                
                //\"{0},{1},{2},{3}\", this.id, this.code, this.name, this.frequency
                index++;
                #endregion
            }
            sbBody.AppendLine(ps.tabMember + "#endregion ");
            #endregion

            #region Constructors--构造方法//lsj
            sbBody.AppendLine(ps.tabMember + "#region Constructors");
            //构造方法1
            sbBody.AppendLine(ps.tabMember + "public " + strClassName + " (): this(" + strDefaultValue + "){}");
            //构造方法2
            sbBody.AppendLine(ps.tabMember + "public " + strClassName + " (" + strPara + ")");
            sbBody.AppendLine(ps.tabMember + "{");
            foreach (string setValue in strSetValues1)
            {
                sbBody.AppendLine(setValue);
            }
            sbBody.AppendLine(ps.tabMember + "}");
            //构造方法3
            
            sbBody.AppendLine(ps.tabMember + "public " + strClassName + " (" + strClassName + " " + strLowerClassName + ")");
            sbBody.AppendLine(ps.tabMember + "{");
            foreach (string setValue in strSetValues2)
            {
                sbBody.AppendLine(setValue);
            }
            sbBody.AppendLine(ps.tabMember + "}");

            sbBody.AppendLine(ps.tabMember + "#endregion ");
            #endregion

            #region IModel Function-- 接口IModel 成员//lsj
            sbBody.AppendLine(ps.tabMember + "#region IModel Function");
            sbBody.AppendLine(ps.tabMember + "public string GetAsString()");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "return String.Format(\"" + strIndex + "\", " + strValue + ");");
            sbBody.AppendLine(ps.tabMember + "}");
            sbBody.AppendLine(ps.tabMember + "#endregion");

            #endregion
           

            #region  IValidate Function--接口IValidate 成员//lsj
            sbBody.AppendLine(ps.tabMember + "#region IValidate Function");
            sbBody.AppendLine(ps.tabMember + "public int Validate()");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "return 1;");
            sbBody.AppendLine(ps.tabMember + "}");
            sbBody.AppendLine(ps.tabMember + "#endregion");
            #endregion

            #region INotifyPropertyChanged Function
            sbBody.AppendLine(ps.tabMember + "#region INotifyPropertyChanged Function");
            sbBody.AppendLine(ps.tabMember + "public event PropertyChangedEventHandler PropertyChanged;");
            sbBody.AppendLine(ps.tabMember + "public void NotifyPropertyChanged(string name)");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "if (PropertyChanged != null)");
            sbBody.AppendLine(ps.tabLocalVar + "{");
            sbBody.AppendLine(ps.tabIfLocalVarTop1 + "PropertyChanged(this, new PropertyChangedEventArgs(name));");
            sbBody.AppendLine(ps.tabLocalVar + "}");
            sbBody.AppendLine(ps.tabMember + "}");
            sbBody.AppendLine(ps.tabMember + "#endregion");
            #endregion

            #region ICloneable
            sbBody.AppendLine(ps.tabMember + "#region ICloneable Function");
            sbBody.AppendLine(ps.tabMember + "public object Clone()");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "return base.CloneEntity<" + strClassName + ">(this);");
            sbBody.AppendLine(ps.tabMember + "}");
            sbBody.AppendLine(ps.tabMember + "public " + strClassName + " DeepClone()");
            sbBody.AppendLine(ps.tabMember + "{");
            sbBody.AppendLine(ps.tabLocalVar + "return (" + strClassName + ")this.Clone();");
            sbBody.AppendLine(ps.tabMember + "}");
            sbBody.AppendLine(ps.tabMember + "#endregion ICloneable");
            #endregion ICloneable

            sbBody.Append(ps.GetDataAccessClassBody_SQL(strTableName, strPrefix, strNamespace));

            string strClass = ps.GetCLassByModel(strNamespace, strPrefix, strTableName, sbBody.ToString());
            
            ret.Append(strClass);
            
            return ret.ToString();
        }
        public string GetIndexSQL(string strTableName, out string strPKTableName, out string strWithoutPKTableName
            ,out string strNotReadTableName,  string HOSPITAL_NO)
        {
            strNotReadTableName="";
            strWithoutPKTableName = "";
            strPKTableName = "";
            try
            {
                StringBuilder ret = new StringBuilder();


                ProcString ps = new ProcString();
                //lsj--类名
                string strClassName = strTableName;//ps.GetClassName(strTableName);//lsj

                //lsj--小写的类名--在此做类的对象来用。
                string strLowerClassName = ps.ConvertStringToUpperOrLower(strClassName, false);

                TableInfo tableInfo = new TableInfo("", "", strTableName);
                StringBuilder sbBody = new StringBuilder();
                
                //---------------------------------------------
                //if (strTableName.Substring(0, 2) != "JH"
                //    && (tableInfo.CheckColumnName("PATIENT_ID") //表中需要有PATIENT_ID
                //    && tableInfo.CheckColumnName("HOSPITAL_NO"))
                //    && tableInfo.strPKName != "")//有主键
                //{
                //    strReadTableName = strTableName;
                //}
                
                if (strTableName.Substring(0, 2) != "JH"
                    //&& (tableInfo.CheckColumnName("PATIENT_ID") //表中需要有PATIENT_ID
                    && !tableInfo.CheckColumnName("HOSPITAL_NO"))
                   // && tableInfo.strPKName != "")//有主键
                {
                    
                    sbBody.AppendLine(" ------------" + strTableName + "------------------------");
                    sbBody.AppendLine(" EXECUTE IMMEDIATE 'alter table " + strTableName + " add HOSPITAL_NO VARCHAR2(16) default " + HOSPITAL_NO + " not null';");
                    if (!string.IsNullOrEmpty(tableInfo.strPKName))
                    {
                        strPKTableName = strTableName;
                        sbBody.AppendLine(" EXECUTE IMMEDIATE 'alter table " + strTableName + "  drop constraint " + tableInfo.strPKName + " cascade';");
                        sbBody.AppendLine(" select count(*) into index_count from user_indexes where index_name='" + tableInfo.strPKName + "';");
                        sbBody.AppendLine(" if index_count > 0 then");
                        sbBody.AppendLine("     EXECUTE IMMEDIATE 'drop index " + tableInfo.strPKName + "';");
                        sbBody.AppendLine(" end if;");

                        sbBody.AppendLine(" EXECUTE IMMEDIATE 'alter table  " + strTableName + " add constraint " + tableInfo.strPKName
                            + " primary key (" + ps.GetIndexSQL(tableInfo) + ", HOSPITAL_NO)';");
                    }
                    else//无主键的表不修改主键也不增加主键
                    {
                        strWithoutPKTableName = strTableName;
                    }
                    
                    ret.Append(sbBody.ToString());
                }
                else
                {
                    strNotReadTableName = strTableName;
                    ret.Append(sbBody.ToString());
                }
                return ret.ToString();
            }
            catch (Exception ex)
            {
                string st = ex.Message;
                return null;
            }
        }
        #endregion
    }
}
