<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Select.aspx.cs" Inherits="EMR.Select" %>

<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPager" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v10.2, Version=10.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxwschsc" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v10.2, Version=10.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v10.2, Version=10.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v10.2, Version=10.2.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .label_css
        {
            margin-right: 40px;
        }
        .rep_table
        {
            border-left: 1px solid black;
            border-right: 1px solid black;
        }
        .rep_table_top_tr
        {
            height: 25px;
            font-size: 14px;
            background-color: Silver;
            border-bottom: 1px solid Black;
        }
        .rep_table td
        {
            border-right: 1px solid black;
            border-bottom: 1px solid black;
            font-size: small;
        }
        .rep_table th
        {
            border-right: 1px solid black;
            border-bottom: 1px solid black;
            font-size: small;
        }
        .div_table
        {
            text-align: right;
            width: 100%;
            height: 150px;
            font-size: 14px;
            float: left;
            border-bottom: #003300 1px ridge;
        }
        .txt
        {
            border: 1px solid #7C7678;
            width: 90px;
        }
        .btn
        {
            background-image: url('images/search.gif');
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 600px; width: 100%">
        <div style="position: absolute; left: 70px; width: 930px; height: 152px; float: left;
            vertical-align: bottom;">
            <table cellspacing="0" class="div_table">
                <tr>
                    <td width="75">
                        姓名:
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txt_name" runat="server" CssClass="txt"></asp:TextBox>
                    </td>
                    <td width="75">
                        病人ID:
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txt_pat_id" runat="server" CssClass="txt"></asp:TextBox>
                    </td>
                    <td width="75">
                        年龄:
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txt_start_age" runat="server" Width="32px" BorderColor="#7C7678"
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        &nbsp-&nbsp<asp:TextBox ID="txt_end_age" runat="server" Width="32px" BorderColor="#7C7678"
                            BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                    <td rowspan="2" width="75">
                        出院日期:
                    </td>
                    <td rowspan="2" align="center">
                        <dx:ASPxDateEdit ID="de_start_in_date" runat="server" Height="20px" Width="90px">
                        </dx:ASPxDateEdit>
                        &nbsp-&nbsp
                        <dx:ASPxDateEdit ID="de_end_in_date" runat="server" Height="20px" Width="90px">
                        </dx:ASPxDateEdit>
                    </td>
                    <td align="center">
                    </td>
                </tr>
                <tr>
                    <td>
                        诊断编码:
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txt_DIAGNOSIS_CODE" runat="server" CssClass="txt"></asp:TextBox>
                    </td>
                    <td>
                        患者拼音:
                    </td>
                    <td align="center">
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="txt"></asp:TextBox>
                    </td>
                    <td>
                        住院医生:
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txt_" runat="server" CssClass="txt"></asp:TextBox>
                    </td>
                    <td align="center">
                    </td>
                </tr>
                <tr>
                    <td>
                        第一诊断:
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton1" Text="是" runat="server" GroupName="first" />&nbsp;
                        <asp:RadioButton ID="RadioButton2" Text="否" runat="server" GroupName="first" />
                    </td>
                    <td>
                        性别:
                    </td>
                    <td align="center">
                        <asp:RadioButton ID="RadioButton3" Text="男" runat="server" GroupName="sex" />&nbsp;
                        <asp:RadioButton ID="RadioButton4" Text="女" runat="server" GroupName="sex" />
                    </td>
                    <td>
                        诊断名称包含:
                    </td>
                    <td align="center">
                        <asp:TextBox ID="TextBox12" runat="server" CssClass="txt"></asp:TextBox>
                    </td>
                    <td rowspan="2">
                        出生日期:
                    </td>
                    <td rowspan="2" align="center">
                        <dx:ASPxDateEdit ID="de_birth_start" runat="server" Height="20px" Width="90px">
                        </dx:ASPxDateEdit>
                        &nbsp;-&nbsp;<dx:ASPxDateEdit ID="de_birth_end" runat="server" Height="20px" Width="90px">
                        </dx:ASPxDateEdit>
                    </td>
                    <td align="center">
                    </td>
                </tr>
                <tr>
                    <td>
                        出院科室:
                    </td>
                    <td align="center">
                        <asp:DropDownList ID="DropDownList1" runat="server" Height="18px" CssClass="txt">
                        </asp:DropDownList>
                    </td>
                    <td>
                        手术编码:
                    </td>
                    <td align="center">
                        <asp:DropDownList ID="DropDownList2" runat="server" Height="18px" CssClass="txt">
                        </asp:DropDownList>
                    </td>
                    <td>
                        病理诊断码:
                    </td>
                    <td align="center">
                        <asp:DropDownList ID="DropDownList3" runat="server" Height="18px" CssClass="txt">
                        </asp:DropDownList>
                    </td>
                    <td align="center">
                        <asp:Button ID="Button1" runat="server" Text="检索" OnClick="Button1_Click" Width="65px"
                            ToolTip="检索" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="position: absolute; left: 70px; top: 185px; width: 930px; height: 382px;
            border: 2px">
            <table style="width: 100%; background-image: url('images/_toolbar.gif'); height: 25px;
                text-align: left; font-size: 15px; vertical-align: center;">
                <tr>
                    <td>
                        住院患者信息列表
                    </td>
                </tr>
            </table>
            <div style="height: 320px; border: 2px; border-color: Black">
                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Btn_View_Click">
                    <HeaderTemplate>
                        <table cellspacing="1" class="rep_table">
                            <tr class="rep_table_top_tr">
                                <th style="overflow: hidden">
                                    ID号
                                </th>
                                <th style="overflow: hidden">
                                    住院号
                                </th>
                                <th style="overflow: hidden">
                                    住院次
                                </th>
                                <th style="overflow: hidden">
                                    姓名
                                </th>
                                <th style="overflow: hidden">
                                    性别
                                </th>
                                <th style="overflow: hidden">
                                    出生日期
                                </th>
                                <th style="overflow: hidden">
                                    入院日期
                                </th>
                                <th style="overflow: hidden">
                                    出院科室
                                </th>
                                <th style="overflow: hidden">
                                    出院日期
                                </th>
                                <th style="overflow: hidden">
                                    住院医生
                                </th>
                                <th style="overflow: hidden">
                                    主治医生
                                </th>
                                <th style="border-right: none;">
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:TextBox ID="txt_pid" Text='<%# DataBinder.Eval(Container.DataItem, "PATIENT_ID")%>'
                                    ReadOnly="true" runat="server" Width="70px" BorderStyle="Solid" BorderWidth="1"
                                    BorderColor="#7C7678"></asp:TextBox>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "INP_NO")%>
                            </td>
                            <td>
                                <asp:TextBox ID="txt_vid" Text='<%# DataBinder.Eval(Container.DataItem, "VISIT_ID")%>'
                                    ReadOnly="true" runat="server" Width="70px" BorderStyle="Solid" BorderWidth="1"
                                    BorderColor="#7C7678"></asp:TextBox>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "NAME")%>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "SEX")%>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "DATE_OF_BIRTH")%>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "ADMISSION_DATE_TIME")%>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "DEPT_DISCHARGE_FROM")%>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "DISCHARGE_DATE_TIME")%>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "DOCTOR_IN_CHARGE")%>
                            </td>
                            <td>
                                <%# DataBinder.Eval(Container.DataItem, "ATTENDING_DOCTOR")%>
                            </td>
                            <td align="center" style="border-right: none;">
                                <asp:Button ID="Button3" runat="server" Text="查看" Width="60px" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div style="color: #FF0000; padding-top: 8px; border: 1px solid black; border-top: none;">
                <%--<asp:Button ID="Button2" runat="server" Text="上一页" OnClick="Button2_Click" />
                <asp:Button ID="Button4" runat="server" Text="下一页" OnClick="Button4_Click" />
                共<asp:Label ID="lab_totle" runat="server" Text="Label"></asp:Label>条数据， 当前第<asp:Label
                    ID="lab_page" runat="server" Text="Label"></asp:Label>页， 共<asp:Label ID="lab_totle_page"
                        runat="server" Text="Label"></asp:Label>页--%>
                <asp:Label ID="lab_totle" runat="server" Text="Label"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lab_page" runat="server" Text="Label" Font-Size="Small"></asp:Label>&nbsp;/
                <asp:Label ID="lab_totle_page" runat="server" Text="Label" Font-Size="Small" CssClass="label_css"></asp:Label>
                <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="Small" OnClick="Button2_Click"><< 上一页</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="LinkButton2" runat="server" Font-Size="Small" OnClick="Button4_Click">下一页 >></asp:LinkButton>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
