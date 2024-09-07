using System;
using System.Data;
using System.Web.UI.WebControls;
//using JHEMR.JHEMRGlobalLib;
//using JHEMR.JHDBServiceLib;



namespace EMR
{
    public partial class Select : System.Web.UI.Page
    {

        public string m_pat_name;//患者姓名
        public string m_dept;//科室

        public string str_SQl_count = "SELECT COUNT(A.PATIENT_ID)  FROM PAT_VISIT A,PAT_MASTER_INDEX B WHERE A.PATIENT_ID=B.PATIENT_ID";
        public string strSQL = "SELECT A.PATIENT_ID,A.VISIT_ID,A.INP_NO,A.ADMISSION_DATE_TIME,A.DEPT_DISCHARGE_FROM,A.ATTENDING_DOCTOR,A.DISCHARGE_DATE_TIME,A.DOCTOR_IN_CHARGE,B.NAME,B.SEX,B.DATE_OF_BIRTH,ROWNUM AS PAGE FROM PAT_VISIT A,PAT_MASTER_INDEX B WHERE A.PATIENT_ID=B.PATIENT_ID";

        public int totle;
        public int totle_page;
        public int page;
        DataSet ds;



        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                BandDdl();
                lab_page.Text = "0";
                lab_totle.Text = "0";
                lab_totle_page.Text = "0";
            }
        }

        /// <summary>
        /// 传出连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_View_Click(object sender, RepeaterCommandEventArgs e)
        {

            string pat_id = ((TextBox)e.Item.FindControl("txt_pid")).Text;
            string visit_id = ((TextBox)e.Item.FindControl("txt_vid")).Text;
            string strUrl = "Index.aspx";


            strUrl += "?hospital_no=40068980X4&patient_id=";
            strUrl += pat_id;
            strUrl += "&visit_id=";
            strUrl += visit_id;
            strUrl += "&file_visit_type_id=2";

            Response.Redirect(strUrl);
        }


        /// <summary>
        /// 查询，绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            str_SQl_count = Test(str_SQl_count);
            strSQL = Test(strSQL);
            totle = Convert.ToInt32( new JHDBService.JHDBServiceClient().GetDataSet(str_SQl_count).Tables[0].Rows[0][0].ToString());

            lab_totle.Text = totle.ToString();
            if (totle % 10 == 0)
            {
                totle_page = totle / 10;
                lab_totle_page.Text = totle_page.ToString();
            }
            else
            {
                totle_page = totle / 10 + 1;
                lab_totle_page.Text = totle_page.ToString();
            }

            #region MyRegion
            //if (txt_name.Text.Trim().Length > 0)//是否有姓名
            //{
            //    string name = txt_name.Text.Trim();
            //    strSQL += " AND B.NAME ='";
            //    strSQL += name + "'";

            //}
            //if (txt_pat_id.Text.Trim().Length > 0)//是否有PATIENT_ID
            //{
            //    string pat_id = txt_pat_id.Text.Trim();
            //    strSQL += " AND A.PATIENT_ID ='";
            //    strSQL += pat_id + "'";;
            //}
            //if ((txt_start_age.Text.Trim().Length > 0) && (txt_end_age.Text.Trim().Length > 0))//判断年龄区间
            //{
            //    string start_age = txt_start_age.Text.Trim();
            //    string end_age = txt_end_age.Text.Trim();
            //    strSQL += " AND A.AGE BETWEEN '";
            //    strSQL += start_age + "' AND '";
            //    strSQL += end_age + "'";
            //}
            //if ((de_start_in_date.Text.Trim().Length > 0) && (de_end_in_date.Text.Trim().Length > 0))//判断出院日期区间
            //{
            //    string start_in = de_start_in_date.Text.Trim();
            //    string end_in = de_end_in_date.Text.Trim();
            //    strSQL += " AND  A.ADMISSION_DATE_TIME BETWEEN TO_DATE('";
            //    strSQL += start_in + "','YYYY-MM-DD') AND TO_DATE('";
            //    strSQL += end_in + "','YYYY-MM-DD')";
            //}

            //if (TextBox4.Text.Trim().Length > 0)//患者拼音码
            //{
            //    string pinyin = TextBox4.Text.Trim();
            //    strSQL += " AND B.NAME_PHONETIC LIKE '%";
            //    strSQL += pinyin + "%'";

            //}

            //if (txt_.Text.Trim().Length > 0)//住院医生
            //{
            //    string DOCTOR_IN_CHARGE = txt_.Text.Trim();
            //    strSQL += " AND B.DOCTOR_IN_CHARGE LIKE '%";
            //    strSQL += DOCTOR_IN_CHARGE + "%'";
            //}

            //if (RadioButton3.Checked || RadioButton4.Checked)//性别
            //{
            //    if (RadioButton3.Checked)
            //    {
            //        string sex = "男";
            //        strSQL += " AND B.SEX ='";
            //        strSQL += sex + "'";
            //    }
            //    else if (RadioButton4.Checked)
            //    {
            //        string sex = "女";
            //        strSQL += " AND B.SEX ='";
            //        strSQL += sex + "'";
            //    }

            //}

            //if (txt_DIAGNOSIS_CODE.Text.Trim().Length > 0)//诊断编码
            //{
            //    string DIAGNOSIS_NAME = txt_DIAGNOSIS_CODE.Text.Trim();
            //    strSQL += " AND A.THREE_DAY_DIAGNOSIS_CODE LIKE '%";
            //    strSQL += DIAGNOSIS_NAME + "%'";
            //}


            //if (TextBox12.Text.Trim().Length > 0)//诊断名称
            //{
            //    string DIAGNOSIS_NAME = TextBox12.Text.Trim();
            //    strSQL += " AND A.THREE_DAY_DIAGNOSIS_NAME LIKE '%";
            //    strSQL += DIAGNOSIS_NAME + "%'";
            //}
            //if (DropDownList1.SelectedItem.Text.Trim().Length > 0)//出院科室
            //{
            //    string DEPT_DISCHARGE_FROM = DropDownList1.SelectedItem.Text.Trim();
            //    strSQL += " AND A.DEPT_DISCHARGE_FROM LIKE '%";
            //    strSQL += DEPT_DISCHARGE_FROM + "%'";
            //}

            //if ((de_start_in_date.Text.Trim().Length > 0) && (de_end_in_date.Text.Trim().Length > 0))//判断出生日期区间
            //{
            //    string start_in = de_birth_start.Text.Trim();
            //    string end_in = de_birth_end.Text.Trim();
            //    strSQL += " AND  B.DATE_OF_BIRTH BETWEEN TO_DATE('";
            //    strSQL += start_in + "','YYYY-MM-DD') AND TO_DATE('";
            //    strSQL += end_in + "','YYYY-MM-DD')";
            //}
            #endregion

            #region MyRegion
            //if (DropDownList2.SelectedItem.Text.Trim().Length > 0)//手术编码
            //{
            //    string OPERATION_CODE = DropDownList2.SelectedItem.Text.Trim();
            //    strSQL += " AND C.OPERATION_CODE LIKE '%";
            //    strSQL += OPERATION_CODE + "%'";
            //}

            //if (RadioButton1.Checked || RadioButton2.Checked)//第一诊断
            //{
            //    if (RadioButton1.Checked)
            //    { 

            //    }
            //    else if (RadioButton2.Checked)
            //    {

            //    }
            //    string DOCTOR_IN_CHARGE = txt_.Text.Trim();
            //    strSQL += " AND B.DOCTOR_IN_CHARGE LIKE '%";
            //    strSQL += DOCTOR_IN_CHARGE + "%'";
            //}
            #endregion
            strSQL = pager(strSQL, 1);
            lab_page.Text = "1";
            
            ds = new JHDBService.JHDBServiceClient().GetDataSet(strSQL);
            BandDate();

        }

        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        private string Test(string strSQL)
        {
            if (txt_name.Text.Trim().Length > 0)//是否有姓名
            {
                string name = txt_name.Text.Trim();
                strSQL += " AND B.NAME ='";
                strSQL += name + "'";

            }
            if (txt_pat_id.Text.Trim().Length > 0)//是否有PATIENT_ID
            {
                string pat_id = txt_pat_id.Text.Trim();
                strSQL += " AND A.PATIENT_ID ='";
                strSQL += pat_id + "'"; ;
            }
            if ((txt_start_age.Text.Trim().Length > 0) && (txt_end_age.Text.Trim().Length > 0))//判断年龄区间
            {
                string start_age = txt_start_age.Text.Trim();
                string end_age = txt_end_age.Text.Trim();
                strSQL += " AND A.AGE BETWEEN '";
                strSQL += start_age + "' AND '";
                strSQL += end_age + "'";
            }
            if ((de_start_in_date.Text.Trim().Length > 0) && (de_end_in_date.Text.Trim().Length > 0))//判断出院日期区间
            {
                string start_in = de_start_in_date.Text.Trim();
                string end_in = de_end_in_date.Text.Trim();
                strSQL += " AND  A.ADMISSION_DATE_TIME BETWEEN TO_DATE('";
                strSQL += start_in + "','YYYY-MM-DD') AND TO_DATE('";
                strSQL += end_in + "','YYYY-MM-DD')";
            }

            if (TextBox4.Text.Trim().Length > 0)//患者拼音码
            {
                string pinyin = TextBox4.Text.Trim();
                strSQL += " AND B.NAME_PHONETIC LIKE '%";
                strSQL += pinyin + "%'";

            }

            if (txt_.Text.Trim().Length > 0)//住院医生
            {
                string DOCTOR_IN_CHARGE = txt_.Text.Trim();
                strSQL += " AND B.DOCTOR_IN_CHARGE LIKE '%";
                strSQL += DOCTOR_IN_CHARGE + "%'";
            }

            if (RadioButton3.Checked || RadioButton4.Checked)//性别
            {
                if (RadioButton3.Checked)
                {
                    string sex = "男";
                    strSQL += " AND B.SEX ='";
                    strSQL += sex + "'";
                }
                else if (RadioButton4.Checked)
                {
                    string sex = "女";
                    strSQL += " AND B.SEX ='";
                    strSQL += sex + "'";
                }

            }

            if (txt_DIAGNOSIS_CODE.Text.Trim().Length > 0)//诊断编码
            {
                string DIAGNOSIS_NAME = txt_DIAGNOSIS_CODE.Text.Trim();
                strSQL += " AND A.THREE_DAY_DIAGNOSIS_CODE LIKE '%";
                strSQL += DIAGNOSIS_NAME + "%'";
            }


            if (TextBox12.Text.Trim().Length > 0)//诊断名称
            {
                string DIAGNOSIS_NAME = TextBox12.Text.Trim();
                strSQL += " AND A.THREE_DAY_DIAGNOSIS_NAME LIKE '%";
                strSQL += DIAGNOSIS_NAME + "%'";
            }
            if (DropDownList1.SelectedItem.Text.Trim().Length > 0)//出院科室
            {
                string DEPT_DISCHARGE_FROM = DropDownList1.SelectedItem.Text.Trim();
                strSQL += " AND A.DEPT_DISCHARGE_FROM LIKE '%";
                strSQL += DEPT_DISCHARGE_FROM + "%'";
            }

            if ((de_start_in_date.Text.Trim().Length > 0) && (de_end_in_date.Text.Trim().Length > 0))//判断出生日期区间
            {
                string start_in = de_birth_start.Text.Trim();
                string end_in = de_birth_end.Text.Trim();
                strSQL += " AND  B.DATE_OF_BIRTH BETWEEN TO_DATE('";
                strSQL += start_in + "','YYYY-MM-DD') AND TO_DATE('";
                strSQL += end_in + "','YYYY-MM-DD')";
            }

            return strSQL;
        }


        /// <summary>
        /// 分页显示
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public string pager(string strSQL, int page)
        {
            int pages = (page - 1) * 10;
            int pageend = pages + 10;
            string strSQL1 = "SELECT * FROM (" + Test(strSQL) + " ORDER BY A.PATIENT_ID,A.VISIT_ID) WHERE PAGE BETWEEN " + pages + " AND " + pageend;
            //strSQL += " AND ROWNUM BETWEEN " + pages + " AND " + pageend + " ORDER BY A.PATIENT_ID,A.VISIT_ID ";
            return strSQL1;
        }


        /// <summary>
        /// 绑定数据
        /// </summary>
        public void BandDate()
        {
            PagedDataSource pds = new PagedDataSource();
            pds.AllowPaging = true;
            pds.PageSize = 10;
            pds.CurrentPageIndex = 0;
            pds.DataSource = ds.Tables[0].DefaultView;
            Repeater1.DataSource = pds;
            Repeater1.DataBind();
        }





        /// <summary>
        /// 绑定DropDownList
        /// </summary>
        public void BandDdl()
        {
            #region 出院科室
            string str_DEPT_NAME = "SELECT DEPT_NAME FROM DEPT_DICT";

            DataSet dt1 =  new JHDBService.JHDBServiceClient().GetDataSet(str_DEPT_NAME);
            DropDownList1.DataSource = dt1.Tables[0].DefaultView;
            DropDownList1.DataTextField = "DEPT_NAME";
            DropDownList1.DataValueField = "DEPT_NAME";

            DropDownList1.DataBind();
            DropDownList1.Items.Insert(0, new ListItem("", ""));
            #endregion
            #region 手术编码
            //string str_DEPT_NAME = "SELECT DEPT_NAME FROM DEPT_DICT";
            //DataTable dt1 = JHGlobal.DBInstance().GetDataSet(str_DEPT_NAME).Tables[0];
            //DropDownList1.DataSource = dt1.DefaultView;
            //DropDownList1.DataTextField = "DEPT_NAME";
            //DropDownList1.DataValueField = "DEPT_NAME";
            //DropDownList1.DataBind();
            //#endregion
            //#region 病历诊断码
            //string str_DEPT_NAME = "SELECT DEPT_NAME FROM DEPT_DICT";
            //DataTable dt1 = JHGlobal.DBInstance().GetDataSet(str_DEPT_NAME).Tables[0];
            //DropDownList1.DataSource = dt1.DefaultView;
            //DropDownList1.DataTextField = "DEPT_NAME";
            //DropDownList1.DataValueField = "DEPT_NAME";
            //DropDownList1.DataBind();
            #endregion
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button4_Click(object sender, EventArgs e)
        {
            page = Convert.ToInt32(lab_page.Text);
            if (page < Convert.ToInt32(lab_totle_page.Text))
            {
                page = page + 1;
                lab_page.Text = page.ToString();

                strSQL = pager(strSQL, page);
                ds =   new JHDBService.JHDBServiceClient().GetDataSet(strSQL);
                BandDate();
            }
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button2_Click(object sender, EventArgs e)
        {
            page = Convert.ToInt32(lab_page.Text);
            if (page > 1)
            {
                page = page - 1;
                lab_page.Text = page.ToString();

                strSQL = pager(strSQL, page);
                ds = new JHDBService.JHDBServiceClient().GetDataSet(strSQL);
                BandDate();
              
            }
        }
    }
}