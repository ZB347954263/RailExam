<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudyAssistBookDetail.aspx.cs" Inherits="RailExamWebApp.Online.Study.StudyAssistBookDetail" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>学习辅导教材</title>
    <script type="text/javascript">
  	function OpenIndex(id)
	{
	    var re = window.open('../AssistBook/' + id + '/index.html','index','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
	    re.focus();
	}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td width="550" height="35" align="left">
                        <img src="../image/jclb01.gif" width="541" height="34" /></td>
                </tr>
                <tr>
                    <td valign="top">
                        <ComponentArt:Grid ID="gvBook" runat="server" AllowPaging="true" PageSize="20" Width="550">
                            <Levels>
                                <ComponentArt:GridLevel DataKeyField="AssistbookId">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="AssistbookId" Visible="false" />
                                        <ComponentArt:GridColumn DataField="BookName" HeadingText="辅导教材名称" />
                                        <ComponentArt:GridColumn DataField="KnowledgeId" Visible="false" />
                                        <ComponentArt:GridColumn DataField="BookNo" HeadingText="辅导教材编号" />
                                        <ComponentArt:GridColumn DataField="PublishOrgName" HeadingText="编制单位" />
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
