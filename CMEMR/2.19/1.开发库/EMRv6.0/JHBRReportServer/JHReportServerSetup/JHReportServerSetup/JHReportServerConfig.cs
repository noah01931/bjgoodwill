using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
//////////////////////////////////////////////////////////////////////////
/// <summary>此类为修改报表服务连接字符串配置 </summary>
///李海威  2013-05-14
//////////////////////////////////////////////////////////////////////////
namespace JHEMR.JHReportServerSetup
{
    class JHReportServerConfig
    {
        public bool SetServerConfig(string strIp,string strUserName,string strPwd)
        {
            string xmlpath =JHCustomWayModel.StrPath+ @"JHBRReportServer\ReportServer\conf\server.xml";
            if (!System.IO.File.Exists(xmlpath))
            {
                //MessageBox.Show("SDE配置文件缺失！", "读取SDE配置信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //return;
            }
            try
            {
           
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlpath);
            XmlNode node = doc.SelectSingleNode("Server");
            XmlNodeList nodeList = doc.SelectSingleNode("Server").ChildNodes;
            XmlNodeList xnl = doc.GetElementsByTagName("Resource");

            foreach (XmlElement xn in xnl)
            {
                if (xn.Attributes["name"].Value == "JHEMR_DS")
                {
                    string strurl = xn.Attributes["url"].Value;
                    xn.Attributes["url"].Value =strurl.Replace("127.0.0.1",strIp);
                    xn.Attributes["username"].Value = strUserName;
                    xn.Attributes["password"].Value = strPwd;
                }
            }
            doc.Save(xmlpath);
            //MessageBox.Show("修改成功!", "提示");
            }
            catch (System.Exception ex)
            {
            }
            return true;
        }
    }
}
