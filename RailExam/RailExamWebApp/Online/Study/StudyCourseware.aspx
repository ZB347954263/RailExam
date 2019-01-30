<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StudyCourseware.aspx.cs"
    Inherits="RailExamWebApp.Online.Study.StudyCourseware" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>学习课件</title>
    <script type="text/javascript">              
   function OpenIndex(id)
	{
	    var re = window.open('/RailExamBao/Courseware/ViewCourseware.aspx?id=' + id,'ViewCourseware','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
	    re.focus();
	} 
    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div id="gridPage">
            <div id="grid">
                <ComponentArt:Grid ID="gvCourse" runat="server" AllowPaging="true" PageSize="25" Width="98%">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="coursewareId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="CoursewareId" Visible="false" />
                                <ComponentArt:GridColumn DataField="CoursewareName" HeadingText="课件名称" Align="left" />
                                <ComponentArt:GridColumn DataField="CoursewareTypeId" Visible="false" />
                                <ComponentArt:GridColumn DataField="TrainTypeId" Visible="false" />
                                <ComponentArt:GridColumn DataField="ProvideOrgName" HeadingText="编著单位" />
                                <ComponentArt:GridColumn DataField="PublishDate" FormatString="yyyy-MM-dd" HeadingText="完成日期" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                </ComponentArt:Grid>
            </div>
        </div>
    </form>
</body>
</html>
