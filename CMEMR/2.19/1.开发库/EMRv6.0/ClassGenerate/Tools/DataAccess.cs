using System;
using System.Collections;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using JHEMR.JHDBLib;

/// <summary>
/// 访问数据库的工具类
/// 所有对表的操作,只需要提供对应的sql
/// </summary>
public class DataAccess
{

    public string strConnection = @"server=服务器\实例名;database=数据库名;uid=用户;pwd=密码";
    DBConnectType dbConnectType_;

    public DBConnectType DbConnectType
    {
        get { return dbConnectType_; }
        set { dbConnectType_ = value; }
    }
    public DataAccess(DBConnectType ConnectType)
    {
        dbConnectType_ = ConnectType;
        //strConnection = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
    }

    /// <summary>
    /// 用来存储sql参数和值
    /// </summary>
    Hashtable htParameters = new Hashtable();

    /// <summary>
    /// 执行sql前,为sql添加参数
    /// </summary>
    /// <param name="paraName">参数名 @paraName</param>
    /// <param name="paraVale">参数值</param>
    public void AddParameter(string paraName, object paraVale)
    {
        htParameters.Add(paraName, paraVale);
    }
    /// <summary>
    /// 清除参数
    /// </summary>
    public void ClearParameters()
    {
        htParameters.Clear();
    }

    /// <summary>
    /// AddParameter方法添加后的参数集合转成SqlParameter[] 数组
    /// </summary>
    /// <returns>SqlParameter[] 数组</returns>
    SqlParameter[] GetParametes()
    {
        int i = 0;
        int htLenght = htParameters.Count;
        SqlParameter[] mParametes = new SqlParameter[htLenght];

        foreach (DictionaryEntry item in htParameters)
        {
            mParametes[i] = new SqlParameter(item.Key.ToString(), item.Value);
            i++;
        }
        return mParametes;
    }

    /// <summary>
    /// 执行sqlSelect 返回 数据集
    /// </summary>
    /// <param name="sqlSelect">select类型的sql</param>
    /// <returns></returns>
    public DataSet ReturnDataSet(string sqlSelect)
    {
        DataSet ds = new DataSet();

        try
        {
            switch (dbConnectType_)
            {
            	case DBConnectType.ORACLE:
                    ds = JHDBUse.Query(sqlSelect);
                	break;
                case DBConnectType.SQLSERVER:
                	SqlConnection cn = new SqlConnection(strConnection);
                    SqlCommand cmd = new SqlCommand(sqlSelect, cn);

                    SqlParameter[] mParametes = GetParametes();
                    if (null != mParametes && mParametes.Length > 0)
                    {
                        cmd.Parameters.AddRange(mParametes);
                    }

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);//通过适配器填充数据集
                	break;
            }
        }
        catch (Exception e)
        {
            throw e;
        }

        return ds;
    }

    /// <summary>
    /// 执行查询sql 
    /// </summary>
    /// <param name="sqlSelect"></param>
    /// <param name="startIndex">起始条数</param>
    /// <param name="size">大小</param>
    /// <param name="tableName">表名(非空字符串)</param>
    /// <returns></returns>
    public DataSet ReturnDataSet(string sqlSelect, int startIndex, int size, string tableName)
    {
        DataSet ds = new DataSet();

        try
        {
            SqlConnection cn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sqlSelect, cn);

            SqlParameter[] mParametes = GetParametes();
            if (null != mParametes && mParametes.Length > 0)
            {
                cmd.Parameters.AddRange(mParametes);
            }

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(ds);//通过适配器填充数据集
            sda.Fill(ds, startIndex, size, tableName);
        }
        catch (Exception e)
        {
            throw e;
        }

        return ds;
    }

    /// <summary>
    /// 执行sqlSelect 返回 数据集
    /// </summary>
    /// <param name="sqlSelect">select类型的sql</param>
    /// <param name="paras">sql 对应的参数数组</param>
    /// <returns></returns>
    public DataSet ReturnDataSet(string sqlSelect, SqlParameter[] paras)
    {
        DataSet ds = new DataSet();

        try
        {
            SqlConnection cn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(sqlSelect, cn);

            cmd.Parameters.AddRange(paras);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);//通过适配器填充数据集
        }
        catch (Exception e)
        {
            throw e;
        }

        return ds;
    }

    /// <summary>
    /// 执行sqlSelect 返回 DataTable
    /// </summary>
    /// <param name="sqlSelect"></param>
    /// <returns></returns>
    public DataTable ReturnDataTable(string sqlSelect)
    {
        DataTable ret = new DataTable();
        try
        {
            ret = ReturnDataSet(sqlSelect).Tables[0];
        }
        catch (Exception e)
        {
            throw e;
        }
        return ret;
    }

    /// <summary>
    /// 执行sqlSelect 返回 DataTable
    /// </summary>
    /// <param name="sqlSelect">select 语句</param>
    /// <param name="paras">sql 对应的参数数组</param>
    /// <returns></returns>
    public DataTable ReturnDataTable(string sqlSelect, SqlParameter[] paras)
    {
        DataTable ret = new DataTable();
        try
        {
            ret = ReturnDataSet(sqlSelect, paras).Tables[0];
        }
        catch (Exception e)
        {
            throw e;
        }
        return ret;
    }

    /// <summary>
    /// 返回首行首列的值
    /// </summary>
    /// <param name="sql"></param>
    /// <returns></returns>
    public object ReturnScalar(string sql)
    {
        object ret = new object();
        SqlConnection cn = new SqlConnection(strConnection);
        try
        {
            switch (dbConnectType_)
            {
                case DBConnectType.ORACLE:
                    ret= JHDBUse.GetSingle(sql);
                    break;
                case DBConnectType.SQLSERVER:
                    SqlCommand cmd = new SqlCommand(sql, cn);

                    SqlParameter[] mParametes = GetParametes();
                    if (null != mParametes && mParametes.Length > 0)
                    {
                        cmd.Parameters.AddRange(mParametes);
                    }
                    cn.Open();
                    ret = cmd.ExecuteScalar();
                    cn.Close();
                    break;
            }
            
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
        }
        return ret;
    }

    /// <summary>
    /// 执行sql(update,insert, delete)语句,返回据影响到的行数
    /// </summary>
    /// <param name="sqlUpdate"></param>
    /// <returns></returns>
    public int ExeCmd(string sql)
    {
        int ret = -1;//失败
        SqlConnection cn = new SqlConnection(strConnection);
        try
        {
            SqlCommand cmd = new SqlCommand(sql, cn);

            SqlParameter[] mParametes = GetParametes();
            if (null != mParametes && mParametes.Length > 0)
            {
                cmd.Parameters.AddRange(mParametes);
            }
            cn.Open();
            ret = cmd.ExecuteNonQuery();
            cn.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
        }
        return ret;

    }
    /// <summary>
    /// 执行sql(update,insert, delete)语句,返回据影响到的行数
    /// </summary>
    /// <param name="sqlUpdate">sql(update,insert, delete)语句</param>
    /// <param name="paras">sql 对应的参数数组</param>
    /// <returns></returns>
    public int ExeCmd(string sql, SqlParameter[] paras)
    {
        int ret = -1;//失败
        SqlConnection cn = new SqlConnection(strConnection);
        try
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            ret = cmd.ExecuteNonQuery();
            cn.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        finally
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
        }
        return ret;

    }


    /// <summary>
    /// 运行存储过程,返回DataTable
    /// 如果存储过程有参数需提前添加(AddParameter())
    /// </summary>
    /// <param name="procName">存储过程名</param>
    /// <returns>DataSet对象</returns>
    public DataSet RunProcReturnDataSet(string procName)
    {
        DataSet Ds = new DataSet();
        try
        {
            SqlConnection cn = new SqlConnection(strConnection);
            SqlCommand cmd = new SqlCommand(procName, cn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter[] mParametes = GetParametes();
            if (null != mParametes && mParametes.Length > 0)
            {
                cmd.Parameters.AddRange(mParametes);
            }

            SqlDataAdapter Da = new SqlDataAdapter(cmd);
            Da.Fill(Ds);
        }
        catch (Exception Ex)
        {
            throw new Exception(procName + Ex.Message);
        }
        return Ds;
    }

    public bool TestConnect()
    {
        string sql = "";
        switch (dbConnectType_)
        {
            case DBConnectType.ORACLE:
                sql = "SELECT count(*) FROM  User_tables";
                break;
            case DBConnectType.SQLSERVER:
                sql = "select count(1) from sysobjects";
                break;
        }
        
        object ret = ReturnScalar(sql);
        if (null != ret)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
public enum DBConnectType
{
    ORACLE,
    SQLSERVER,
    CACHE,
    OLEDB,
    DB2,
}