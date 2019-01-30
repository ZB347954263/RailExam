<%@ Page Language="C#" AutoEventWireup="true" Codebehind="FocalPointStudyInfo.aspx.cs"
    Inherits="RailExamWebApp.Main.FocalPointStudyInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>重点学习信息</title>

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">       
		function OpenIndex(id)
		{
		    var re = window.open('../Online/Book/' + id + '/index.html','index','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
		    re.focus();
		}
		
	function GetExercise(id)
    {
        var   cleft;   
        var   ctop;   
       cleft=(screen.availWidth-800)*.5;   
       ctop=(screen.availHeight-600)*.5; 
       var re= window.open("/RailExamBao/Exercise/ExerciseManage.aspx?id="+id,"ExerciseManage",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=no,status=no scrollbars=yes');		
       re.focus(); 
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="grid">
            <ComponentArt:Grid ID="Grid1" runat="server" PageSize="20" Width="98%">
                <Levels>
                    <ComponentArt:GridLevel DataKeyField="bookId">
                        <Columns>
                            <ComponentArt:GridColumn DataField="bookId" Visible="false" />
                            <ComponentArt:GridColumn DataField="bookName" HeadingText="教材名称" Align="Left" />
                            <ComponentArt:GridColumn DataField="knowledgeId" Visible="false" />
                            <ComponentArt:GridColumn DataField="trainTypeId" Visible="false" />
                            <ComponentArt:GridColumn DataField="bookNo" HeadingText="编号" />
                            <ComponentArt:GridColumn DataField="publishOrgName" HeadingText="编制单位" />
                            <ComponentArt:GridColumn DataField="publishOrg" HeadingText="编制单位" Visible="false" />
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
    </form>
</body>
</html>
