using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;

namespace JHEMR.JHReportServerSetup
{
    class JHReportTestDataset
    {
        public bool test(string strIp, string strUserName, string strPwd)
        {
            string connectionString="Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST="+strIp+")(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=jhemr)));User Id="+strUserName+";Password="+strPwd;
            OracleConnection conn = new OracleConnection(connectionString);
            OracleCommand cmd = conn.CreateCommand();
            if (strIp=="")
            {
                return false;
            }
            try
            {
                conn.Open();
                conn.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }
    }
}
