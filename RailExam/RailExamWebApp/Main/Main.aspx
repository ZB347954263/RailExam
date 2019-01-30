<%@ Page Language="C#" AutoEventWireup="True" Codebehind="Main.aspx.cs" Inherits="RailExamWebApp.Main.Main" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=RailExamWebApp.Common.Class.PrjPub.GetRailName()%>
        在线学习考试管理系统</title>

    <script type="text/javascript">
    // Opens a new browser window with the specified URL
    function goToUrl(url)
    {
        window.frames["ContentFrame"].location = url;
    }
   
      function logout()
   {
       var employeeID=document.getElementById("hfEmployeeID").value;
      //alert(employeeID); 
       top.returnValue = "false|"+employeeID;
   }  
    </script>

    <script src="../Common/JS/JSHelper.js"></script>

</head>
<body onbeforeunload="logout()">
    <form id="form1" runat="server">
        <div>
            <ComponentArt:Splitter ID="Splitter1" runat="server">
                <Content>
                    <ComponentArt:SplitterPaneContent id="HeaderPane">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" bgcolor="#1082E9">
                            <tr>
                                <td align="left">
                                    <img src="../Common/Image/MainHeadLeft.jpg" width="650" height="74" />
                                    <%--                              <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"
                                        width="650" height="72">
                                        <param name="movie" value="../Common/Image/MainHeadLeft.swf" />
                                        <param name="quality" value="high" />
                                        <embed src="../Common/Image/MainHeadLeft.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer"
                                            type="application/x-shockwave-flash" width="650" height="72"></embed>
                                    </object>--%>
                                </td>
                                <td align="right">
                                    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"
                                        width="350" height="72">
                                        <param name="movie" value="../Common/Image/MainHeadRight.swf" />
                                        <param name="quality" value="high" />
                                        <embed src="../Common/Image/MainHeadRight.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer"
                                            type="application/x-shockwave-flash" width="350" height="72"></embed>
                                    </object>
                                </td>
                            </tr>
                        </table>
                    </ComponentArt:SplitterPaneContent>
                    <ComponentArt:SplitterPaneContent id="MenuPane">
                        <ComponentArt:NavBar id="SystemMenu" runat="server">
                        </ComponentArt:NavBar>
                    </ComponentArt:SplitterPaneContent>
                    <ComponentArt:SplitterPaneContent id="BodyPane">
                        <iframe id="ContentFrame" name="ContentFrame" src="Desktop.aspx" frameborder="0"
                            height="100%" width="100%" scrolling="auto"></iframe>
                    </ComponentArt:SplitterPaneContent>
                </Content>
                <Layouts>
                    <ComponentArt:SplitterLayout>
                        <Panes Orientation="Vertical" Resizable="false" SplitterBarCssClass="VerticalSplitterBar"
                            SplitterBarActiveCssClass="ActiveSplitterBar" SplitterBarWidth="5">
                            <ComponentArt:SplitterPane PaneContentId="HeaderPane" Height="72" CssClass="SplitterPane">
                            </ComponentArt:SplitterPane>
                            <ComponentArt:SplitterPane Height="100%">
                                <panes orientation="Horizontal" resizable="false" splitterbarcssclass="HorizontalSplitterBar"
                                    splitterbarcollapseimageurl="horCol.gif" splitterbarcollapsehoverimageurl="horColHover.gif"
                                    splitterbarexpandimageurl="horExp.gif" splitterbarexpandhoverimageurl="horExpHover.gif"
                                    splitterbarcollapseimagewidth="5" splitterbarcollapseimageheight="116" splitterbarcollapsedcssclass="CollapsedHorizontalSplitterBar"
                                    splitterbaractivecssclass="ActiveSplitterBar" splitterbarwidth="6">
                                    <ComponentArt:SplitterPane PaneContentId="MenuPane" Width="100" MinWidth="100" CssClass="SplitterPane" />
                                    <ComponentArt:SplitterPane PaneContentId="BodyPane" AllowScrolling="true" CssClass="SplitterPane" /> 
                                </panes>
                            </ComponentArt:SplitterPane>
                        </Panes>
                    </ComponentArt:SplitterLayout>
                </Layouts>
            </ComponentArt:Splitter>
            <asp:HiddenField ID="hfEmployeeID" runat="server" />
            <ComponentArt:CallBack ID="CallBack" runat="server" RefreshInterval="30000" OnCallback="CallBack_OnCallback">
            </ComponentArt:CallBack>
        </div>
    </form>
</body>
</html>

<script type="text/javascript">
    window.SystemMenu.style.height = window.screen.availHeight - 150;
</script>

