<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudyByKnowledgeDetail.aspx.cs" Inherits="RailExamWebApp.Online.Study.StudyByKnowledgeDetail" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript">
    function GetExercise(id)
    {
       var re= window.open("../../Exercise/ExerciseManage.aspx?id="+id,"ExerciseManage"," Width=800px; Height=600px,status=false,resizable=no,scrollbars=yes",true);		
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
    <div id="gridPage" >
            <div id="grid" style="height:550px;">
                <ComponentArt:Grid ID="gvBook" runat="server" PageSize="20" AllowPaging="true" Width="95%">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="bookId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="bookId" Visible="false" />
                                <ComponentArt:GridColumn DataField="bookName" HeadingText="书名" />
                                <ComponentArt:GridColumn DataField="knowledgeId" Visible="false" />
                                <ComponentArt:GridColumn DataField="trainTypeId" Visible="false" />
                                <ComponentArt:GridColumn DataField="bookNo" HeadingText="编号" />
                                <ComponentArt:GridColumn DataField="authors" HeadingText="作者" />
                                <ComponentArt:GridColumn DataField="keyWords" HeadingText="关键字" />
                                <ComponentArt:GridColumn DataField="publishOrgName" HeadingText="编制单位" />
                                <ComponentArt:GridColumn DataField="publishOrg" HeadingText="编制单位"  Visible="false"/>                                
                                <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTEdit">
                            <a onclick="GetExercise(##DataItem.getMember('bookId').get_value()##)" href="#">
                                <b>练习</b></a>
                            </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
                <ComponentArt:Grid ID="gvCourse" runat="server" PageSize="20" AllowPaging="true" Width="97%">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="CoursewareID">
                            <Columns>
                                <ComponentArt:GridColumn DataField="CoursewareID" Visible="false" />
                                <ComponentArt:GridColumn DataField="CoursewareName" HeadingText="课件名称" />
                                <ComponentArt:GridColumn DataField="CoursewareTypeID" Visible="false" />
                                <ComponentArt:GridColumn DataField="TrainTypeID" Visible="false" />
                                <ComponentArt:GridColumn DataField="Authors" HeadingText="编著者" />
                                <ComponentArt:GridColumn DataField="KeyWord" HeadingText="关键字" />
                                <ComponentArt:GridColumn DataField="ProvideOrgName" HeadingText="资料提供单位" />
                                <ComponentArt:GridColumn DataField="ProvideOrg" HeadingText="资料提供单位ID" Visible="false" />
                                <ComponentArt:GridColumn DataField="Revisers" HeadingText="主审" />
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
