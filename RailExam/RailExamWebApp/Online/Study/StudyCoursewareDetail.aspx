<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudyCoursewareDetail.aspx.cs" Inherits="RailExamWebApp.Online.Study.StudyCoursewareDetail" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>学习教材</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table style="width: 750; height: 550">
                <tr>
                    <td width="750" height="35" align="left">
                        <img src="../image/kjlb1.gif" width="541" height="36" /></td>
                </tr>
                <tr style="height: 125px">
                    <td valign="top">
                        <ComponentArt:Grid ID="gvCourse" runat="server" AllowPaging="true" PageSize="20" Width="750">
                            <Levels>
                                <ComponentArt:GridLevel DataKeyField="coursewareId">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="CoursewareId" Visible="false" />
                                        <ComponentArt:GridColumn DataField="CoursewareName" HeadingText="课件名称" />
                                        <ComponentArt:GridColumn DataField="CoursewareTypeId" Visible="false" />
                                        <ComponentArt:GridColumn DataField="TrainTypeId" Visible="false" />
                                        <ComponentArt:GridColumn DataField="ProvideOrgName" HeadingText="编著单位" />
                                        <ComponentArt:GridColumn DataField="Authors" HeadingText="编著者" />
                                        <ComponentArt:GridColumn DataField="KeyWord" HeadingText="关键字" />
                                     </Columns>           
                                </ComponentArt:GridLevel>     
                            </Levels>
                        </ComponentArt:Grid>
                    </td>
                </tr>
            </table>
    </div>
    </form>
</body>
</html>
