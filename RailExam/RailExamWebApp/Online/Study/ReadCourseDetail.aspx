<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ReadCourseDetail.aspx.cs" Inherits="RailExamWebApp.Online.Study.ReadCourseDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>学习课件详细信息</title>
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
                <ComponentArt:Grid ID="Grid1" runat="server" PageSize="25"  Width="98%">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="CoursewareID">
                            <Columns>
                                <ComponentArt:GridColumn DataField="CoursewareID" Visible="false" />
                                <ComponentArt:GridColumn DataField="CoursewareName" HeadingText="课件名称"   Align="Left"  />
                                <ComponentArt:GridColumn DataField="CoursewareTypeID" Visible="false" />
                                <ComponentArt:GridColumn DataField="TrainTypeID" Visible="false" />
                                <ComponentArt:GridColumn DataField="ProvideOrgName" HeadingText="资料提供单位"   />
                                <ComponentArt:GridColumn DataField="PublishDate" FormatString="yyyy-MM-dd"   HeadingText="完成日期" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                </ComponentArt:Grid>
            </div>
        </div>
        <asp:HiddenField ID="hfId" runat="server" />
    </form>
</body>
</html>
