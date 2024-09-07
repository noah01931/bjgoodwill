using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using JHEMR.JHServicesLib.Provider;
using JHEMR.JHCommonLib.Entity.TableObject;
using System.Data;

namespace JHEMR.JHAutoUpdateServiceLib
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true,
        ConcurrencyMode = ConcurrencyMode.Multiple,
        InstanceContextMode = InstanceContextMode.PerCall,
        MaxItemsInObjectGraph = Int32.MaxValue)]
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    [JHSRVThrottling(Int32.MaxValue, Int32.MaxValue, Int32.MaxValue)]
    public class JHAutoUpdateService : IJHAutoUpdateService
    {
        public DataSet get_All_UploadFile(string strDown_NO_From, out int imax_No)
        {
            //string connType = new JHAutoManage().get_ConnecStr();
            //string ConnecString = JHAutoManage.ConnecString;
            JHAutoManage autoclient = new JHAutoManage();

            DataSet dsQuery = new DataSet();
            string strSerialNo = string.Empty;
            int iSerialNo = 0;
            string strSql = @"SELECT C.FILE_NAME, C.FILE_BODY,C.LAST_UPDATE,B.PATH
              FROM JHSYS_FILE_UPLOAD C INNER JOIN JHSYS_FILE_DICT B ON C.FILE_NAME=B.FILE_NAME  WHERE IS_CURVERSION=1 ";
            if (!string.IsNullOrEmpty(strDown_NO_From))
            { strSql += " AND  C.DISTRIBUTE_NO >" + strDown_NO_From; }
            strSql += " ORDER BY  C.DISTRIBUTE_NO ASC, C.LAST_UPDATE ASC";
            dsQuery = autoclient.Query(strSql);

            #region 获取本次下载最新版本序列号
            strSql = @"SELECT MAX (DISTRIBUTE_NO) AS MAXNO FROM JHSYS_FILE_UPLOAD WHERE IS_CURVERSION=1";
            object objSerialNo = autoclient.GetSingle(strSql);
            if (objSerialNo != null)
            {
                strSerialNo = objSerialNo.ToString();
                if (string.IsNullOrEmpty(strSerialNo))
                { iSerialNo = 0; }
                else
                { iSerialNo = int.Parse(strSerialNo); }
            }
            #endregion
            imax_No = iSerialNo;
            return dsQuery;
        }

        public string getStr(string str)
        {
            return "输入了" + str;
        }

        public DataSet getList(string strDown_NO_From, out int imax_No)
        {
            throw new NotImplementedException();
        }

        //public System.IO.Stream getDll(string strDown_NO_From, out int imax_No)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
