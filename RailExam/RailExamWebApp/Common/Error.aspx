<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="RailExamWebApp.Common.Error" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat="server">
    <title>无标题页</title>
    <link href="CSS/Error.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0">
            <tr align="center">
                <td>
			        <div id="ErrorArea">
				        <div id="ErrorTitle">错误原因：</div>
						<div id="ErrorContent"><%=_errorMessage%></div>
					</div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
