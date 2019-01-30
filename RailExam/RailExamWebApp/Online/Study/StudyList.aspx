<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudyList.aspx.cs" Inherits="RailExamWebApp.Online.Study.StudyList" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>在线学习</title>
    <script type="text/javascript">       
        function SelectTrainType()
        {
            //var re = window.open("Train/TrainTypeEmployeeSelect.aspx",'ShowEditor','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;           
            var re = window.open("../../Train/TrainTypeEmployeeSelect.aspx","TrainTypeEmployeeSelect",' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);		
            re.focus();
        }
        
        function StudyByKnowledge()
        {
            var   cleft;   
            var   ctop;   
           cleft=(screen.availWidth-800)*.5;   
           ctop=(screen.availHeight-600)*.5;   
            var re = window.open("StudyByKnowledge.aspx","StudyByKnowledge",' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);		
            re.focus();
        }

        function GetExercise(id)
        {
            var   cleft;   
            var   ctop;   
           cleft=(screen.availWidth-800)*.5;   
           ctop=(screen.availHeight-600)*.5; 
           var re= window.open("../../Exercise/ExerciseManage.aspx?id="+id,"ExerciseManage",' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no,scrollbars=yes',true);		
	       re.focus(); 
        }
        
        function ShowBookListMore()
        {
           var   cleft;   
           var   ctop;   
           cleft=(screen.availWidth-800)*.5;   
           ctop=(screen.availHeight-600)*.5;           
           var re = window.open("StudyBookDetail.aspx","StudyBookDetail",' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);		
            re.focus();
        }
        
        function ShowCoursewareListMore()
        {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
            var re = window.open("StudyCoursewareDetail.aspx","StudyCoursewareDetail",' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);		
            re.focus();
        }
        
        function OpenIndex(id)
		{
		    var re = window.open('../Book/' + id + '/index.html','index','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
		    re.focus();
		}

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        在线学习</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        教材课件信息</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
             </div>
             <div id="content">
            <table width="98%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td height="45" valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="8" colspan="4" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td width="20%" align="left">
                                    <img src="../image/CurrentTrainType.gif" width="106" height="21" onclick="SelectTrainType()" style="cursor: hand;"/></td>
                                <td width="6%" align="center">
                                    <img src="../image/tb02.gif" width="14" height="7" alt="" /></td>
                                <td width="52%" align="left">
                                    <asp:Label ID="lblType" runat="server" Text="Label"></asp:Label></td>
                                <td align="left">
                                    <img src="../image/StudyByKnowledge.gif" alt="" onclick="StudyByKnowledge()" style="cursor: hand;"
                                        width="105" height="22" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="250" valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="100%" height="35" align="left">
                                    <img src="../image/BookList.gif" border="0" width="541" height="34" usemap="#MapBookMore" /></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <ComponentArt:Grid ID="gvBook"  runat="server" AllowPaging="true" PageSize="15"  Width="90%">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="bookId">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="bookId" Visible="false" />
                                                <ComponentArt:GridColumn DataField="bookName" HeadingText="教材名称" />
                                                <ComponentArt:GridColumn DataField="knowledgeId" Visible="false" />
                                                <ComponentArt:GridColumn DataField="trainTypeId" Visible="false" />
                                                <ComponentArt:GridColumn DataField="bookNo" HeadingText="教材编号" />
                                                <ComponentArt:GridColumn DataField="publishOrgName" HeadingText="编制单位" />
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
                    </td>
                </tr>
                <tr>
                    <td height="250" valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="100%" align="left">
                                    <img src="../image/CoursewareList.gif" border="0" width="541" height="34" usemap="#MapCoursewareMore" /></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                     <ComponentArt:Grid ID="gvCourse" runat="server" AllowPaging="true" PageSize="4" Width="90%">
                                        <Levels>
                                            <ComponentArt:GridLevel DataKeyField="coursewareId">
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="CoursewareId" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="CoursewareName" HeadingText="课件名称" />
                                                    <ComponentArt:GridColumn DataField="CoursewareTypeId" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="TrainTypeId" Visible="false" />
                                                    <ComponentArt:GridColumn DataField="ProvideOrgName" HeadingText="编著单位" />
                                                    <ComponentArt:GridColumn DataField="Authors" HeadingText="编著者" />
                                                 </Columns>           
                                            </ComponentArt:GridLevel>     
                                        </Levels>
                                    </ComponentArt:Grid>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            </div>
        </div>
        <map id="MapBookMore">
            <area shape="rect" coords="498,12,542,28" href="#" onclick="ShowBookListMore()"/>
        </map>
        <map id="MapCoursewareMore">
            <area shape="rect" coords="496,10,538,27" href="#" onclick="ShowCoursewareListMore()" />
        </map>
    </form>
</body>
</html>
