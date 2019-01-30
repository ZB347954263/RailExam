<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudyBookDetail.aspx.cs" Inherits="RailExamWebApp.Online.Study.StudyBookDetail" %>
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
                        <img src="../image/jclb01.gif" width="541" height="34" /></td>
                </tr>
                <tr>
                    <td valign="top">
                        <ComponentArt:Grid ID="gvBook" runat="server" AllowPaging="true" PageSize="20" Width="750">
                            <Levels>
                                <ComponentArt:GridLevel DataKeyField="bookId">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="bookId" Visible="false" />
                                        <ComponentArt:GridColumn DataField="bookName" HeadingText="教材名称" />
                                        <ComponentArt:GridColumn DataField="knowledgeId" Visible="false" />
                                        <ComponentArt:GridColumn DataField="trainTypeId" Visible="false" />
                                        <ComponentArt:GridColumn DataField="publishOrgName" HeadingText="编制单位" />
                                        <ComponentArt:GridColumn DataField="bookNo" HeadingText="教材编号" />
                                        <ComponentArt:GridColumn DataField="authors" HeadingText="编著者" />
                                        <ComponentArt:GridColumn DataCellClientTemplateId="ct1" HeadingText="操作" AllowSorting="False" />
                                     </Columns>           
                                </ComponentArt:GridLevel>     
                           </Levels>
                           <ClientTemplates>
                                <ComponentArt:ClientTemplate ID="ct1">
                                    <A onclick="GetExercise(##DataItem.getMember('bookId').get_value()## )" href="#"><b>练习</b></A>
                                </ComponentArt:ClientTemplate>
                            </ClientTemplates>
                        </ComponentArt:Grid>
                    </td>
                </tr>
            </table>
    </div>
    </form>
</body>
</html>
