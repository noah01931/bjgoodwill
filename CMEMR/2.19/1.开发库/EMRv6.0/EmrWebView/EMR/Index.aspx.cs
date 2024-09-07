using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Collections;

namespace EMR
{
    public partial class Index : System.Web.UI.Page
    {
        public string hid = ""; //医院id
        public string pid = "";//患者id
        public string vid = "";//住院次数
        public string fid = "";//就诊类型

        JHCDRService.JHCDRServiceClient client = new JHCDRService.JHCDRServiceClient();
        public string HtmlStr = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            //?hospital_no=40068980X4&file_visit_type_id=2&patient_id=T000955266&visit_id=2

            //hid = "40068980X4";
            //pid = "T000955266";
            //fid = "2";
            //vid = "2";

            try
            {
                if ((Request.Params["hospital_no"] != null && Request.Params["hospital_no"].Trim() != "") && (Request.Params["patient_id"] != null && Request.Params["patient_id"].Trim() != "") && (Request.Params["visit_id"] != null && Request.Params["visit_id"].Trim() != "") && (Request.Params["file_visit_type_id"] != null && Request.Params["file_visit_type_id"].Trim() != ""))
                {
                    hid = (Request.Params["hospital_no"]).ToString();
                    pid = (Request.Params["patient_id"]).ToString();
                    vid = (Request.Params["visit_id"]).ToString();
                    fid = (Request.Params["file_visit_type_id"]).ToString();

                    //InintPatentInfo(hid, pid, vid, fid); //初始化病人基本信息

                    //BindMenuDataSet();

                    // InintPatentInfo(hid, pid, vid, fid);
                    FistInintRightHtml(hid, pid, vid, fid);
                }
                else
                {


                }

            }
            catch (Exception ee)
            {

                ClientScript.RegisterClientScriptBlock(this.GetType(), "err", "<script>alert('出现异常Message:" + ee.Message.ToString() + "')</script>");
            }

        }

        /// <summary>
        /// 初始化病人信息
        /// </summary>
        private void InintPatentInfo(string hospital_id, string patient_id, string visit_id, string vist_type_id)
        {
            try
            {

                JHCDRService.PAT_VISIT pat_visit = new JHCDRService.PAT_VISIT();
                JHCDRService.PAT_MASTER_INDEX pmid = client.GetPatientInfo(out pat_visit, pid, vid, hid);
                pmid = client.GetPatientInfo(out pat_visit, pid, vid, hid);

                this.txt_PatientId.Text = pmid.PATIENT_ID;
                this.txt_InhospitalNum.Text = pmid.INP_NO;
                this.txt_sex.Text = pmid.SEX;
                this.txt_InhospitalNum.Text = pat_visit.VISIT_ID; //住院次数
                this.txt_medicalNum.Text = pmid.INP_NO; //住院号
                this.txt_bednum.Text = pat_visit.BED_LABEL;//床号
                txt_patientname.Text = pmid.NAME;//患者姓名
                txt_birthday.Text = CusttimeStr(pmid.DATE_OF_BIRTH);
                txt_doctor.Text = pat_visit.ATTENDING_DOCTOR_ID;//主治医生
                txt_joindept.Text = pat_visit.DEPT_ADMISSION_TO;//入院科室
                txt_jointime.Text = CusttimeStr(pat_visit.ADMISSION_DATE_TIME);//入院时间
                txt_outdept.Text = CusttimeStr(pat_visit.DEPT_DISCHARGE_FROM);//出院科室
                txt_outtime.Text = CusttimeStr(pat_visit.DISCHARGE_DATE_TIME);//出院时间
            }
            catch (Exception ee)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "err", "<script>alert('出现异常Message:" + ee.Message.ToString() + "')</script>");
            }



        }



        /// <summary>
        /// 第一次绑定右侧html
        /// </summary>
        private void FistInintRightHtml(string hospital_id, string patient_id, string visit_id, string vist_type_id)
        {
            try
            {
                System.Data.DataSet ds = client.GetFileIndexs(hospital_id, patient_id, visit_id, vist_type_id);

                int cnt = ds.Tables[0].Rows.Count;

                if (cnt > 0)
                {

                    ArrayList al = new ArrayList();
                    string FILE_UNIQUE_ID = "";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string name = ds.Tables[0].Rows[i]["catalog_name"].ToString();

                        if (!al.Contains(name))
                        {
                            al.Add(name);
                        }
                    }
                    System.Data.DataRow[] dr = null;
                    string newname = "入院记录";
                    dr = ds.Tables[0].Select("catalog_name='" + newname + "'");

                    for (int k = 0; k < dr.Length; k++)
                    {
                        FILE_UNIQUE_ID = dr[0]["FILE_UNIQUE_ID"].ToString();
                    }

                    getHtmlstr(FILE_UNIQUE_ID); //绑定右侧默认的文档
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "error", "<script>alert('没有此记录')</script>");
                }

            }
            catch (Exception ee)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "err", "<script>alert('出现异常Message:" + ee.Message.ToString() + "')</script>");
            }



        }

        ///// <summary>
        ///// 绑定左侧菜单 dataset
        ///// </summary>
        //private void BindMenuDataSet()
        //{
        //    JHCDRService.JHCDRServiceClient client = new JHCDRService.JHCDRServiceClient();

        //    DataSet ds = client.GetFileIndexs(hid, pid, vid, fid);

        //    //this.dlMenu.DataSource = ds.Tables[0].DefaultView;
        //    //this.dlMenu.DataBind();

        //    ArrayList al = new ArrayList();

        //    DataTable dt = new DataTable();

        //    DataSet newds = new DataSet();

        //    string name = "";

        //    DataTable dmp = newds.Tables.Add("tmp");

        //    Hashtable ht = new Hashtable();
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        //Response.Write(ds.Tables[0].Rows[i]["catalog_name"]);

        //        name = ds.Tables[0].Rows[i]["catalog_name"].ToString();

        //        if (ht.ContainsKey(name))
        //        {
        //            ht.Add(name, name);
        //        }
        //        //newds.Tables.Add(new DataTable("tmp"));

        //    }


        //    //this.dlMenu.DataSource = newds.Tables[0].DefaultView;
        //    //this.dlMenu.DataBind();
        //}



        /// <summary>
        /// 切割字符串长度
        /// </summary>
        /// <param name="o"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        protected string CutLength(object o, int length)
        {
            string str = "";
            if (o != null)
            {
                if (o.ToString().Length > length)
                {

                    str = o.ToString().Substring(0, length) + "...";
                }
                else
                {
                    str = o.ToString();
                }
            }

            return str;
        }


        /// <summary>
        ///获取生成的html
        /// </summary>
        /// <param name="file_id"></param>
        protected void getHtmlstr(string file_id)
        {
            try
            {
                JHCDRService.JHMR_FILE_CONTENT_HTM htm = client.GetFileContentHtm(file_id);
                byte[] bts = Convert.FromBase64String(htm.MR_CONTENT);

                string str = Encoding.Default.GetString(bts);

                HtmlStr = str;
                //Response.Write(str);
                this.content.InnerHtml = HtmlStr;
            }
            catch (Exception ee)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "err", "<script>alert('出现异常Message:" + ee.Message.ToString() + "')</script>");

            }

            //JHCDRService.JHMR_FILE_CONTENT_HTM html = client.GetFileContentHtm(file_id);

            //HtmlStr = html.MR_CONTENT;


            // Response.Write(str);

        }

        /// <summary>
        /// 截取时间
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string CusttimeStr(object o)
        {
            string str = "";
            if (o != null && o.ToString() != "")
            {

                str = DateTime.Parse(o.ToString()).ToShortDateString();
            }

            return str;
        }



    }
}