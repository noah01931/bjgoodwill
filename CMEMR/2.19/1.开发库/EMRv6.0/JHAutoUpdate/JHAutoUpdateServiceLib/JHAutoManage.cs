using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JHEMR.JHServicesLib.Client;
using JHEMR.JHServicesLib.Provider;
using JHEMR.JHCommonLib.Entity;
using JHEMR.JHCommonLib.Entity.TableObject;
using System.Data;
using System.Configuration;
using System.Data.OracleClient;

namespace JHEMR.JHAutoUpdateServiceLib
{
    public class JHAutoManage
    {

        /// <summary>
        /// 数据库链接类型
        /// </summary>
        private static string _connType;
        public static string connType
        {
            get
            {
                if (string.IsNullOrEmpty(_connType))
                {
                    _connType = ConfigurationManager.AppSettings["DataBaseType"].ToString();
                }
                return _connType;
            }
        }

        /// <summary>
        /// 链接字符串
        /// </summary>
        private static string _ConnecString;

        public static string ConnecString
        {
            get
            {
                //string connecString = string.Empty;
                switch (connType)
                {
                    case "Oracle":
                        _ConnecString = ConfigurationManager.ConnectionStrings["Oracle"].ConnectionString;
                        break;
                    case "SQLServer":
                        _ConnecString = ConfigurationManager.ConnectionStrings["SQLServer"].ConnectionString;
                        break;
                    default:
                        _ConnecString = ConfigurationManager.ConnectionStrings["Oracle"].ConnectionString;
                        break;
                }
                return _ConnecString;
            }

        }

        //public string get_ConnecStr()
        //{
        //    string connType = ConfigurationManager.AppSettings["DataBaseType"].ToString();
        //    string connecString = string.Empty;
        //    switch (connType)
        //    {
        //        case "Oracle":
        //            connecString = ConfigurationManager.AppSettings["Oracle"].ToString();
        //            break;
        //        case "SQLServer":
        //            connecString = ConfigurationManager.AppSettings["SQLServer"].ToString();
        //            break;
        //        default:
        //            connecString = ConfigurationManager.AppSettings["Oracle"].ToString();
        //            break;
        //    }
        //    return connecString;
        //}


        /// <summary>
        /// 执行一条计算查询结果语句，返回查询结果（object）。
        /// </summary>
        /// <param name="SQLString">计算查询结果语句</param>
        /// <returns>查询结果（object）</returns>
        public string GetSingle(string SQLString)
        {
            //string connectionString = connecString;//获取链接字符串
            using (OracleConnection connection = new OracleConnection(ConnecString))
            {
                using (OracleCommand cmd = new OracleCommand(SQLString, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj.ToString();
                        }
                    }
                    catch (System.Data.OracleClient.OracleException e)
                    {
                        connection.Close();
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string SQLString)
        {
            using (OracleConnection connection = new OracleConnection(ConnecString))
            {
                if (connection.State != ConnectionState.Open)
                    connection.Open();
                using (OracleCommand cmd = new OracleCommand(SQLString, connection))
                {
                    cmd.CommandType = CommandType.Text;
                    //cmd.Connection = connection;
                    //cmd.CommandText = SQLString;
                    //PrepareCommand(cmd, connection, SQLString);
                    using (OracleDataAdapter da = new OracleDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        try
                        {
                            da.Fill(ds, "ds");
                            cmd.Parameters.Clear();
                        }
                        catch (System.Data.OracleClient.OracleException ex)
                        {
                            throw new Exception(ex.Message);
                        }
                        return ds;
                    }
                }
            }
        }


        //private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, string cmdText)
        //{
        //    if (conn.State != ConnectionState.Open)
        //        conn.Open();
        //    cmd.Connection = conn;
        //    cmd.CommandText = cmdText;
        //    cmd.CommandType = CommandType.Text;
        //}
    }
}
