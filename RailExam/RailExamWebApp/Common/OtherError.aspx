<%@ Page Language="C#" AutoEventWireup="true" Codebehind="OtherError.aspx.cs" Inherits="RailExamWebApp.Common.OtherError" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="Head1" runat="server">
    <title>系统提示</title>
    <link href="CSS/Error.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0">
            <tr align="center">
                <td>
                    <div id="ErrorArea">
                        <div id="ErrorTitle">
                            系统提示：</div>
                        <div id="ErrorContent">
                            <%=_errorMessage%>
                        </div>
                    </div>
                </td>
            </tr>
           <tr>
               <td align="center">
                    <input type="button" class="button" value="关  闭" onclick="window.close();" />
               </td>
           </tr> 
        </table>
    </form>
</body>
</html>
