<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InformationStudyInfo.aspx.cs"
    Inherits="RailExamWebApp.AssistBook.InformationStudyInfo" %>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>资料信息</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
		function OpenIndex(id)
		{
		    var re = window.open('../Online/AssistBook/' + id + '/index.html','index','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
		    re.focus();
//		   	var re = window.open('BookCount.aspx?id='+id,'index','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
//		    re.focus();
		}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        资料查阅</div>
                </div>
            </div>
            <div id="cotent">
                <div id="query">
                    资料名称
                    <asp:TextBox ID="txtBookName" runat="server" Width="10%"></asp:TextBox>
                    关键字
                    <asp:TextBox ID="txtKeyWords" runat="server" Width="10%"></asp:TextBox>
                    编制单位
                    <asp:TextBox ID="txtOrg" runat="server" Width="10%"></asp:TextBox>
                    <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="确   定" OnClick="btnQuery_Click" />
                </div>
                <div id="grid">
                    <componentart:grid id="Grid1" runat="server" pagesize="20" width="98%">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="Information_ID">
                            <Columns>
                                <ComponentArt:GridColumn DataField="Information_ID" Visible="false" />
                                <ComponentArt:GridColumn DataField="Information_Name" HeadingText="资料名称" Align="Left" />
                                <ComponentArt:GridColumn DataField="Information_System_ID" Visible="false" />
                                 <ComponentArt:GridColumn DataField="Create_Org_ID" Visible="false" />
                                <ComponentArt:GridColumn DataField="Information_Level_ID" Visible="false" />
                                <ComponentArt:GridColumn DataField="Information_No" HeadingText="编号" Visible="false" />
                                <ComponentArt:GridColumn DataField="Publish_Org" HeadingText="编制单位" />
                                <ComponentArt:GridColumn DataField="Authors" HeadingText="作者" />
                                <ComponentArt:GridColumn DataField="Publish_Date" HeadingText="编制时间"/>
                                <ComponentArt:GridColumn DataField="CreatePersonName" HeadingText="录入人" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                </componentart:grid>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
