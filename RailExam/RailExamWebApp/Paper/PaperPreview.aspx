<%@ Page Language="C#" AutoEventWireup="True" Codebehind="PaperPreview.aspx.cs" Inherits="RailExamWebApp.Paper.PaperPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>‘§¿¿ ‘æÌ</title>
    <link href="../Common/CSS/Paper.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="95%">
                <tr>
                    <td >
                        <table width='98%'>
                            <tr>
                                <td id="PaperName">                                   
                                    <asp:Label ID="lblTitle" runat="server"></asp:Label></td>
                                <td id="PaperInfo">
                                    <asp:Label ID="lblTitleRight" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>                   
                    <td style="width: 100%; vertical-align: top">
                        <table width='98%' style="vertical-align: top">
                            <tr>
                                <td>
                                    <%FillPaper(); %>
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
