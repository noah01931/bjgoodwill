<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="EMR.Index" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<html>
<head runat="server">
    <title></title>
    <meta http-equiv="description" content="this is my page">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8">
    <link rel="stylesheet" type="text/css" href="./styles/index.css">
    <script type="text/javascript" src="./scripts/jquery-1.7.2.js"></script>
    <script type="text/javascript" src="./scripts/index.js"></script>
    <script type="text/javascript">

        function GetHtmlStr(file_id) {
            $.get("Ajax.ashx?file_id=" + file_id, function (data) {

                $("#content").html(data);
                $("#right-content").attr("class", "bg");
            });

        }
    </script>
    <style type="text/css">
        .bg
        {
            background: #005758;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="head" style="display: none;">
        <div class="jh-logo">
        </div>
        <div class="patient-info">
            <div class="info-item">
                <asp:Label CssClass="info-label" ID="lb_PatientId" runat="server" Text="患者编号:"></asp:Label>
                <asp:TextBox CssClass="info-textbox long" ID="txt_PatientId" Enabled="false" runat="server"></asp:TextBox>
            </div>
            <div class="info-item">
                <asp:Label CssClass="info-label" ID="lb_InhospitalNum" runat="server" Text="住院次:"></asp:Label>
                <asp:TextBox CssClass="info-textbox" ID="txt_InhospitalNum" Enabled="false" runat="server"></asp:TextBox>
            </div>
            <div class="info-item short-item">
                <asp:Label CssClass="info-label" ID="lb_medicalNum" runat="server" Text="病案号:"></asp:Label>
                <asp:TextBox CssClass="info-textbox" ID="txt_medicalNum" Enabled="false" runat="server"></asp:TextBox>
            </div>
            <div class="info-item">
                <asp:Label ID="lb_bednum" CssClass="info-label" runat="server" Text="床号:"></asp:Label>
                <asp:TextBox ID="txt_bednum" CssClass="info-textbox" Enabled="false" runat="server"></asp:TextBox>
            </div>
            <div class="info-item">
                <asp:Label ID="lb_patientname" CssClass="info-label" runat="server" Text="患者姓名:"></asp:Label>
                <asp:TextBox ID="txt_patientname" CssClass="info-textbox long" Enabled="false" runat="server"></asp:TextBox>
            </div>
            <div class="info-item">
                <asp:Label ID="lb_sex" CssClass="info-label" runat="server" Text="性别:"></asp:Label>
                <asp:TextBox ID="txt_sex" CssClass="info-textbox" Enabled="false" runat="server"></asp:TextBox>
            </div>
            <div class="info-item short-item">
                <asp:Label ID="lb_birthday" CssClass="info-label " runat="server" Text="出生日期:"></asp:Label>
                <asp:TextBox ID="txt_birthday" CssClass="info-textbox" Enabled="false" runat="server"></asp:TextBox>
            </div>
            <div class="info-item">
                <asp:Label ID="lb_doctor" CssClass="info-label" runat="server" Text="主治医生:"></asp:Label>
                <asp:TextBox ID="txt_doctor" CssClass="info-textbox" Enabled="false" runat="server"></asp:TextBox>
            </div>
            <div class="info-item">
                <asp:Label ID="lb_joindept" CssClass="info-label" runat="server" Text="入院科室:"></asp:Label>
                <asp:TextBox ID="txt_joindept" CssClass="info-textbox long" Enabled="false" runat="server"></asp:TextBox>
            </div>
            <div class="info-item">
                <asp:Label ID="lb_jointime" CssClass="info-label" runat="server" Text="入院时间:"></asp:Label>
                <asp:TextBox ID="txt_jointime" CssClass="info-textbox " Enabled="false" runat="server"></asp:TextBox>
            </div>
            <div class="info-item short-item">
                <asp:Label ID="lb_outdept" CssClass="info-label" runat="server" Text="出院科室:"></asp:Label>
                <asp:TextBox ID="txt_outdept" CssClass="info-textbox" Enabled="false" runat="server"></asp:TextBox>
            </div>
            <div class="info-item">
                <asp:Label ID="lb_outtime" CssClass="info-label" runat="server" Text="出院时间:"></asp:Label>
                <asp:TextBox ID="txt_outtime" CssClass="info-textbox" Enabled="false" runat="server"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="page-content">
        <!-- 页面左侧菜单  start -->
        <div class="main-menu" id="main-menu">
            <%
                    
                ////hid = "40068980X4";
                ////pid = "T000955266";
                ////fid = "2";
                ////vid = "2";

                try
                {
                    EMR.JHCDRService.JHCDRServiceClient client = new EMR.JHCDRService.JHCDRServiceClient();

                    System.Data.DataSet ds = client.GetFileIndexs(hid, pid, vid, fid);
                    int cnt = ds.Tables[0].Rows.Count;

                    if (cnt > 0)
                    {

                        ArrayList al = new ArrayList();
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            string name = ds.Tables[0].Rows[i]["catalog_name"].ToString();

                            string FILE_UNIQUE_ID = ds.Tables[0].Rows[i]["FILE_UNIQUE_ID"].ToString();
                            string tile = ds.Tables[0].Rows[i]["topic"].ToString();
                            string time = ds.Tables[0].Rows[i]["create_date_time"].ToString();
                            if (!al.Contains(name))
                            {
                                al.Add(name);
                            }
                        }
                        System.Data.DataRow[] dr = null;
                        for (int j = 0; j < al.Count; j++)
                        {
                            string newname = (string)al[j];
                            dr = ds.Tables[0].Select("catalog_name='" + newname + "'", "create_date_time  desc");   
            %>
            <div class="menu-item" style="overflow: auto;">
                <div class="menu-head">
                    <span class="menu-title">
                        <%=al[j]%>
                    </span><span class="menu-switch" id="menu-switch" open="icon_open.png" close="icon_close.png">
                    </span>
                </div>
                <div class="subItems" id="subItems">
                    <%
                        for (int k = 0; k < dr.Length; k++)
                        {
                    %>
                    <a href='#' class="subItem" onclick="GetHtmlStr('<%=dr[k]["FILE_UNIQUE_ID"]%>')"
                        title='<%=dr[k]["topic"]%>' target="_self">&nbsp;<%=DateTime.Parse(dr[k]["create_date_time"].ToString()).ToShortDateString()%>&nbsp;<%=CutLength(dr[k]["topic"], 7)%></a>
                    <%
                        }
                    
                    %>
                </div>
            </div>
            <%}
                    }
                    else
                    {
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alte", "<script>alert('没有此记录')</script>");
                    }
                }
                catch (Exception ee)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "err", "<script>alert('出现异常Message:" + ee.Message.ToString() + "')</script>");
                }
            %>
        </div>
        <!-- 页面左侧菜单  end   -->
        <!-- 页面右侧内容  start  -->
        <div class="right-content" id="right-content" style="overflow: auto; background: #005758;">
            <div class="index-page" id="content" runat="server" style="overflow: auto;">
                <img class="discase-logo" src="images/logo_discase.png" alt="Disease Case Logo" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
