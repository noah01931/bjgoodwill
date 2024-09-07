using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace EMR
{
    /// <summary>
    /// Ajax 的摘要说明
    /// </summary>
    public class Ajax : IHttpHandler
    {
        string file_id = "";
        JHCDRService.JHCDRServiceClient client = new JHCDRService.JHCDRServiceClient();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";

            if (HttpContext.Current.Request["file_id"] != null)
            {
                file_id = HttpContext.Current.Request["file_id"];
                HttpContext.Current.Response.Write(getHtmlstr(file_id));
            }

        }

        /// <summary>
        ///获取生成的html
        /// </summary>
        /// <param name="file_id"></param>
        protected string getHtmlstr(string file_id)
        {

            //JHCDRService.JHMR_FILE_CONTENT_HTM html = client.GetFileContentHtm(file_id);

            //return html.MR_CONTENT.ToString();

            try
            {
                JHCDRService.JHMR_FILE_CONTENT_HTM htm = client.GetFileContentHtm(file_id);
                byte[] bts = Convert.FromBase64String(htm.MR_CONTENT);

                string str = Encoding.Default.GetString(bts);

                return str;
            }
            catch
            {
                return "还没有数据请联系管理员";
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}