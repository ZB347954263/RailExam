<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StudyBook.aspx.cs" Inherits="RailExamWebApp.Online.Study.StudyBook" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>学习教材</title>

    <script type="text/javascript">
  	function OpenIndex(id)
	{
	    var re = window.open('../Book/' + id + '/index.html','index','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
	    re.focus();
	}
	
    function GetExercise(id)
    {
        var   cleft;   
        var   ctop;   
       cleft=(screen.availWidth-800)*.5;   
       ctop=(screen.availHeight-600)*.5; 
       var re= window.open("/RailExamBao/Exercise/ExerciseManage.aspx?id="+id,"ExerciseManage",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=no,status=no');		
       re.focus(); 
    }
   
   function OpenCourse(id)
	{
	    var re = window.open('/RailExamBao/Courseware/ViewCourseware.aspx?id=' + id,'ViewCourseware','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
	    re.focus();
	}  
	

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color: White;">
            <img src="/RailExamBao/Online/image/jclb01.jpg" alt="" />
        </div>
        <div style="text-align: center; height: 260px;">
            <ComponentArt:Grid ID="Grid1" runat="server" PageSize="10" Width="98%">
                <Levels>
                    <ComponentArt:GridLevel DataKeyField="bookId">
                        <Columns>
                            <ComponentArt:GridColumn DataField="bookId" HeadingText="ID" Visible="false" />
                            <ComponentArt:GridColumn DataField="bookName" HeadingText="教材名称" Align="Left" />
                            <ComponentArt:GridColumn DataField="knowledgeId" HeadingText="knowledgeId" Visible="false" />
                            <ComponentArt:GridColumn DataField="trainTypeId" HeadingText="trainTypeId" Visible="false" />
                            <ComponentArt:GridColumn DataField="bookNo" HeadingText="教材编号" />
                            <ComponentArt:GridColumn DataField="publishOrgName" HeadingText="编制单位" />
                            <ComponentArt:GridColumn DataCellClientTemplateId="ct1" HeadingText="操作" AllowSorting="False" />
                        </Columns>
                    </ComponentArt:GridLevel>
                </Levels>
                <ClientTemplates>
                    <ComponentArt:ClientTemplate ID="ct1">
                        <a onclick="GetExercise(##DataItem.getMember('bookId').get_value()## )" href="#"><b>
                            练习</b></a>
                    </ComponentArt:ClientTemplate>
                </ClientTemplates>
            </ComponentArt:Grid>
        </div>
        <div style="background-color: White;">
            <img src="/RailExamBao/Online/image/kjlb1.gif" alt="" /></div>
        <div style="text-align: center; height: 240px;">
            <ComponentArt:Grid ID="Grid2" runat="server" PageSize="10" Width="98%">
                <Levels>
                    <ComponentArt:GridLevel DataKeyField="CoursewareID">
                        <Columns>
                            <ComponentArt:GridColumn DataField="CoursewareID" Visible="false" />
                            <ComponentArt:GridColumn DataField="CoursewareName" HeadingText="课件名称" Align="Left" />
                            <ComponentArt:GridColumn DataField="CoursewareTypeID" Visible="false" />
                            <ComponentArt:GridColumn DataField="TrainTypeID" Visible="false" />
                            <ComponentArt:GridColumn DataField="ProvideOrgName" HeadingText="资料提供单位" />
                            <ComponentArt:GridColumn DataField="PublishDate" FormatString="yyyy-MM-dd" HeadingText="完成日期" />
                        </Columns>
                    </ComponentArt:GridLevel>
                </Levels>
            </ComponentArt:Grid>
        </div>
    </form>
</body>
</html>
