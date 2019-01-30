<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="NewStudyInfo.aspx.cs" Inherits="RailExamWebApp.Online.Study.NewStudyInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript">
    function OpenIndex(id)
	{
	    var re = window.open('../Book/' + id + '/index.html','index','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
	    re.focus();
	}
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="98%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td height="10">
                    </td>
                </tr>
                <tr>
                    <td height="177" valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="50%" height="35" align="left">
                                    <img src="../image/newbook.gif" width="542" height="34" /></td>
                            </tr>
                            <tr>
                                <td height="135" align="center">
                                    <table width="98%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="left">
                                                <asp:GridView ID="gvBook"  runat="server" ShowHeader="false" BorderWidth="0"
                                                    AutoGenerateColumns="False" DataKeyNames="bookId" BorderColor="ActiveBorder" CellPadding="3"
                                                    HeaderStyle-BackColor="ActiveBorder">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="教材名称">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="left" />
                                                            <ItemTemplate>
                                                                <img src="../image/tb01.gif" alt="" />
                                                                <asp:Label ID="lblBook" runat="server" Text='<%# Bind("bookName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td width="50%" height="35" align="left">
                        <img src="../image/newcourse.gif" width="541" height="36" /></td>
                </tr>
                <tr>
                    <td height="135" align="center">
                        <table width="98%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="left">
                                    <asp:GridView ID="gvCourse" runat="server"  ShowHeader="false" BorderWidth="0"
                                        AutoGenerateColumns="False" DataKeyNames="coursewareId" BorderColor="ActiveBorder"  CellPadding="3"
                                        HeaderStyle-BackColor="ActiveBorder">
                                        <Columns>
                                            <asp:TemplateField HeaderText="课件名称">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" />
                                                <ItemTemplate>
                                                    <img src="../image/tb01.gif" alt="" />
                                                    <asp:HyperLink ID="hlName" Text='<%# Bind("coursewareName") %>' runat="server" NavigateUrl='<%# Bind("url") %>'
                                                        Target="_blank"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
