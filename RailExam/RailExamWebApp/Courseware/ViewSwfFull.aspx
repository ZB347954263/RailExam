<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ViewSwfFull.aspx.cs" Inherits="RailExamWebApp.Courseware.ViewSwfFull" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>收看课件</title>
</head>
<body oncontextmenu='return false' ondragstart='return false' onselectstart='return false'
    oncopy='document.selection.empty()' onbeforecopy='return false'>
    <form id="form1" runat="server">
        <div>
           <asp:HiddenField ID="hfUrl" runat="server" />
            <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"
                width="100%" height="100%">
                <param name="movie" value='<%=hfUrl.Value%>' />
                <param name="quality" value="high" />
                <embed src='<%=hfUrl.Value%>' quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer"
                    type="application/x-shockwave-flash" width="100%" height="100％"></embed>
            </object>
        </div>
    </form>
</body>
</html>
