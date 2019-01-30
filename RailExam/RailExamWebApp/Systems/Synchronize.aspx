<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" Codebehind="Synchronize.aspx.cs"
    Inherits="RailExamWebApp.Systems.Synchronize" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script type="text/javascript">
    function deleteLog(logid)
   {
          var flagDelete=document.getElementById("HfDeleteRight").value;   
                  
	       if(flagDelete=="False")
           {                     
                alert("您没有权限使用该操作！");
                return;
           }  
            if(! confirm("您确定要删除该记录吗？"))
            {
                return;
            }
         form1.LogID.value =logid;
         form1.submit();
         form1.LogID.value = "";  
   }  
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        系统管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        同步设置</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        站段单位</div>
                    <div id="leftContent">
                        <asp:ListBox ID="OrgList" runat="server" Width="100%" Height="100%" OnSelectedIndexChanged="OrgList_SelectedIndexChanged"
                            AutoPostBack="True"></asp:ListBox>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        同步设置</div>
                    <div id="rightContent">
                        <table style="border: solid 1px #E0E0E0; border-collapse: collapse; background-color: #ffffff;
                            width: 100%; height: 50px;">
                            <tr>
                                <td>
                                    <b>公共设置</b></td>
                                <td colspan="3" style="text-align: right;">
                                    <asp:Button ID="btnModify1" CausesValidation="false" runat="server" CssClass="button"
                                        Text="编辑" OnClick="btnModify1_Click" />&nbsp;
                                    <asp:Button ID="btnSave1" CausesValidation="True" Visible="false" runat="server"
                                        CssClass="button" Text="保存" OnClick="btnSave1_Click" />&nbsp;
                                    <asp:Button ID="btnCancel1" CausesValidation="false" runat="server" CssClass="button"
                                        Text="取消" OnClick="btnCancel1_Click" Visible="False" />
                                </td>
                            </tr>
                            <tr style="height: 50%;">
                                <td style="width: 25%; text-align: right; padding: 5px; border: solid 1px #E0E0E0;
                                    white-space: nowrap;">
                                    检测间隔时间：
                                </td>
                                <td style="width: 25%; text-align: left; padding: 5px; border: solid 1px #E0E0E0;
                                    white-space: nowrap;">
                                    <asp:DropDownList ID="ddlInterval" runat="server">
                                    </asp:DropDownList>分钟
                                </td>
                                <td style="width: 25%; text-align: right; padding: 5px; border: solid 1px #E0E0E0;
                                    white-space: nowrap;">
                                    同步重试次数：
                                </td>
                                <td style="width: 25%; text-align: left; padding: 5px; border: solid 1px #E0E0E0;
                                    white-space: nowrap;">
                                    &nbsp;
                                    <asp:DropDownList ID="ddlRetryCount" runat="server">
                                    </asp:DropDownList></td>
                            </tr>
                        </table>
                        <br />
                        <table style="border: solid 1px #E0E0E0; border-collapse: collapse; background-color: #ffffff;
                            width: 100%; height: 50px;">
                            <tr>
                                <td>
                                    <b>站段设置</b>
                                </td>
                                <td colspan="3" style="text-align: right;">
                                    <asp:Button ID="btnModify2" CausesValidation="false" runat="server" CssClass="button"
                                        Text="编辑" OnClick="btnModify2_Click" />&nbsp;
                                    <asp:Button ID="btnSave2" CausesValidation="True" Visible="false" runat="server"
                                        CssClass="button" Text="保存" OnClick="btnSave2_Click" />&nbsp;
                                    <asp:Button ID="btnCancel2" CausesValidation="false" runat="server" CssClass="button"
                                        Text="取消" OnClick="btnCancel2_Click" Visible="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; padding: 5px; border: solid 1px #E0E0E0; white-space: nowrap;
                                    text-align: right;">
                                    同步时间（hh:mm）：
                                </td>
                                <td style="text-align: left; width: 75%; padding: 5px; border: solid 1px #E0E0E0;
                                    white-space: nowrap;">
                                    <asp:TextBox ID="txtTime" runat="server"></asp:TextBox></td>
                                <td style="width: 25%; padding: 5px; border: solid 1px #E0E0E0; white-space: nowrap;
                                    text-align: right;">
                                    服务器IP地址：
                                </td>
                                <td style="text-align: left; width: 75%; padding: 5px; border: solid 1px #E0E0E0;
                                    white-space: nowrap;">
                                    <asp:TextBox ID="txtIPAddress" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%; padding: 5px; border: solid 1px #E0E0E0; white-space: nowrap;
                                    text-align: right;">
                                    上传模式：</td>
                                <td style="text-align: left; width: 75%; padding: 5px; border: solid 1px #E0E0E0;
                                    white-space: nowrap;" colspan="3">
                                    <asp:CheckBox ID="chkUpload" runat="server" Text="自动上传成绩和答卷" /></td>
                            </tr>
                        </table>
                        <br />
                        <ComponentArt:Grid ID="Grid1" runat="server" PageSize="10" Width="97%">
                            <Levels>
                                <ComponentArt:GridLevel DataKeyField="SynchronizeLogID">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="SynchronizeLogID" Visible="False" />
                                        <ComponentArt:GridColumn DataField="SynchronizeContent" HeadingText="同步操作" />
                                        <ComponentArt:GridColumn DataField="BeginTime" HeadingText="开始时间" />
                                        <ComponentArt:GridColumn DataField="EndTime" HeadingText="结束时间" />
                                        <ComponentArt:GridColumn DataField="StatusContent" HeadingText="操作状态" />
                                        <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </Levels>
                            <ClientTemplates>
                                <ComponentArt:ClientTemplate ID="CTEdit">
                                    <a onclick="deleteLog(##DataItem.getMember('SynchronizeLogID').get_value()##);" href="#">
                                        <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a>
                                </ComponentArt:ClientTemplate>
                            </ClientTemplates>
                        </ComponentArt:Grid>
                    </div>
                </div>
            </div>
        </div>
        <input type="hidden" name="LogID" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        &nbsp;
    </form>
</body>
</html>
