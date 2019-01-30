<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeeWithRailEdu.aspx.cs"
    Inherits="RailExamWebApp.Systems.EmployeeWithRailEdu" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        考试管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        员工合并</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                </div>
            </div>
            <div id="content">
                <table>
                    <tr>
                        <td style="text-align: left;">
                            <table>
                                <tr style="height: 15px;">
                                    <td style="width: 50%; ">
                                        适用范围：组织机构
                                    </td>
                                    <td style="width: 50%; ">
                                        适用范围：工作岗位</td>
                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:TreeView ID="tvOrganization" EnableViewState="true" runat="server"
                                            Height="600" Width="300">
                                        </ComponentArt:TreeView><br />
                                    </td>
                                    <td>
                                        <ComponentArt:TreeView ID="tvPost" EnableViewState="true" runat="server" Height="600" Width="300">
                                        </ComponentArt:TreeView>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
