<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudyCourseDetail.aspx.cs" Inherits="RailExamWebApp.Online.Study.StudyCourseDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>详细信息</title>
    <script type="text/javascript">
        function ManagePaper(id)
        {
            var re = window.open("../../Paper/ExerciseAnswer.aspx?id="+id,"ExerciseAnswer"," Width=800px; Height=600px,status=false,resizable=yes,scrollbars=yes",true);		
	        re.focus(); 
        }
        
        function GetExercise(id)
        {
           var re= window.open("../../Exercise/ExerciseManage.aspx?id="+id,"ExerciseManage"," Width=800px; Height=600px,status=false,resizable=no,scrollbars=yes",true);		
	       re.focus(); 
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <table style="width: 550px; height: 50px" class="contentTable"> 
                <tr>
                    <td>
                        描述：
                        <asp:Label ID="lblDescription" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        备注：
                        <asp:Label ID="lblMemo" runat="server"></asp:Label></td>
                </tr>
            </table>
            <table style="width: 550px; height: 210px">
                <tr>
                    <td width="550px" height="35" align="left">
                        <img src="../image/jclb01.gif" width="541" height="34" /></td>
                </tr>
                <tr style="height: 125px">
                    <td valign="top">
                        <ComponentArt:Grid ID="gvBook" runat="server" AllowPaging="true" PageSize="4" Width="550px">
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
            <table style="width: 550px; height: 210px">
                <tr>
                    <td width="550px" height="35" align="left">
                        <img src="../image/kjlb1.gif" width="541" height="36" /></td>
                </tr>
                <tr style="height: 125px">
                    <td valign="top">
                        <ComponentArt:Grid ID="gvCourse" runat="server" AllowPaging="true" PageSize="4" Width="550px">
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
            <!--
            <table style="width: 550px; height: 160px">
                <tr>
                    <td width="550px" height="35" align="left">
                        <img src="../image/exercise.gif" width="541" height="34" /></td>
                </tr>
                <tr style="height: 125px">
                    <td valign="top">
                        <ComponentArt:Grid ID="gvExercise" runat="server" AllowPaging="true" PageSize="4">
                            <Levels>
                                <ComponentArt:GridLevel DataKeyField="ObjPaper.PaperId">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="ObjPaper.PaperId" Visible="false" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.PaperName" HeadingText="练习名称" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.CategoryId" HeadingText="categoryId"
                                            Visible="false" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.CreateMode" HeadingText="出题方式" Visible="false" />
                                        <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate1" HeadingText="出题方式" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.ItemCount" HeadingText="试题总数" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.TotalScore" HeadingText="总分" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.CreatePerson" HeadingText="出卷人" />
                                        <ComponentArt:GridColumn DataCellClientTemplateId="CTedit" HeadingText="操作" />
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </Levels>
                            <ClientTemplates>
                                <ComponentArt:ClientTemplate ID="CTedit">
                                    <a onclick="ManagePaper(##DataItem.getMember('ObjPaper.PaperId').get_value()## )"
                                        title="预览练习" href="#"><b>预览练习</b></a>&nbsp;
                                </ComponentArt:ClientTemplate>
                            </ClientTemplates>
                            <ClientTemplates>
                                <ComponentArt:ClientTemplate ID="ClientTemplate1">
                                    ## DataItem.getMember("ObjPaper.CreateMode").get_value() == 1 ? "手工出题":"随机出题" ##
                                </ComponentArt:ClientTemplate>
                            </ClientTemplates>
                        </ComponentArt:Grid>
                    </td>
                </tr>
            </table>
            <table style="width: 550px; height: 160px">
                <tr>
                    <td width="550px" align="left">
                        <img src="../image/task.gif" width="541" height="34" />
                    </td>
                </tr>
                <tr style="height: 125px">
                    <td valign="top">
                        <ComponentArt:Grid ID="gvTask" runat="server" AllowPaging="true" PageSize="4" Width="550px">
                            <Levels>
                                <ComponentArt:GridLevel DataKeyField="ObjPaper.PaperId">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="ObjPaper.PaperId" Visible="false" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.PaperName" HeadingText="作业名称" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.CategoryId" HeadingText="categoryId"
                                            Visible="false" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.CreateMode" HeadingText="出题方式" Visible="false" />
                                        <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="出题方式" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.ItemCount" HeadingText="试题总数" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.TotalScore" HeadingText="总分" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.CreatePerson" HeadingText="出卷人" />
                                        <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate2" HeadingText="操作" />
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </Levels>
                            <ClientTemplates>
                                <ComponentArt:ClientTemplate ID="ClientTemplate2">
                                    <a onclick="ManagePaper(##DataItem.getMember('ObjPaper.PaperId').get_value()## )"
                                        title="预览作业" href="#"><b>预览作业</b></a>&nbsp;
                                </ComponentArt:ClientTemplate>
                            </ClientTemplates>
                            <ClientTemplates>
                                <ComponentArt:ClientTemplate ID="ClientTemplate3">
                                    ## DataItem.getMember("ObjPaper.CreateMode").get_value() == 1 ? "手工出题":"随机出题" ##
                                </ComponentArt:ClientTemplate>
                            </ClientTemplates>
                        </ComponentArt:Grid>
                    </td>
                </tr>
            </table>
            -->
        </div>
    </form>
</body>
</html>
