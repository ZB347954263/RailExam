<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Admin_Top.aspx.cs" Inherits="RailExamWebApp.Main.Admin_Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="Style/css.css" rel="stylesheet" type="text/css" />
   <script type="text/javascript">
        function showDesktop() {
        	var search = window.location.search;
        	
        	if(search.indexOf("type")>=0) {
    	        var type = search.substring(search.indexOf("type=") + 5);
        		if(type == "dangan") {
        		    window.parent.frames('mainFrame').frames('I2').location = 'DanganDesktop.aspx';
        		}
        		else if(type=="employee") {
        		    window.parent.frames('mainFrame').frames('I2').location = 'EmployeeDesktop.aspx';
        		}

        	}
        	else {
        		window.parent.frames('mainFrame').frames('I2').location = 'Desktop.aspx';
        	}
        }
   </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" cellpadding="0" cellspacing="0" width="100%" background="../Common/Image/flashbg.png"><!--#1082E9-->
                <tr>
                    <td align="left">
<%--                        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"
                            width="650" height="72">
                            <param name="movie" value="../Common/Image/MainHeadLeft.swf" />
                            <param name="quality" value="high" />
                            <embed src="../Common/Image/MainHeadLeft.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer"
                                type="application/x-shockwave-flash" width="650" height="72"></embed>
                        </object>--%>
                        <img  src="../Common/Image/MainHeadLeft.jpg" width="650" height="74"/>
                    </td>
                    <td align="right">
<%--                        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"
                            width="350" height="72">
                            <param name="movie" value="../Common/Image/MainHeadRight.swf" />
                            <param name="quality" value="high" />
                            <embed src="../Common/Image/MainHeadRight.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer"
                                type="application/x-shockwave-flash" width="350" height="74"></embed>
                        </object>--%>
                    </td>
                </tr>
            </table>
         <table id="topbar" width="100%" border="0" cellpadding="0" cellspacing="0" background="images/top_bg2.gif">
            <tr>
                <td width="170" height="29">
                    &nbsp;
                </td>
                <td width="490" height="29">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <img src="images/16_home.png" alt="首页" width="16" height="16" /><a style="cursor:hand; color:white" onclick="showDesktop();">首页</a>
                            </td>
                            <td>
                                <img src="images/16_back.png" alt="后退" width="16" height="16" /><a href="javascript:history.go(-1)">后退</a>
                            </td>
                            <td>
                                <img src="images/16_forward.png" alt="前进" width="16" height="16" /><a href="javascript:history.go(1)">前进</a>
                            </td>
                            <td>
                                <img src="images/16_refresh.png" alt="刷新" width="16" height="16" /><a href="javascript:window.parent.frames('mainFrame').frames('I2').location.reload();">刷新</a>
                            </td>
                            <td style="display:none;">
                                <img src="images/16_print.png" alt="打印" width="16" height="16" /><a href="#" onclick="javascript:parent.mainFrame.I2.focus();parent.mainFrame.I2.print();">打印</a>
                            </td>
                            <td style="display:none;">
                                <img src="images/16_relogin.png" alt="注销" width="16" height="16" /><a href=""
                                    target="_top" onclick="if (!window.confirm('您确认要注消当前登录用户吗？')){return false;}">注销</a>
                            </td>
                            <td>
                                <img src="images/16_exit.png" alt="退出" width="16" height="16" target="_top" /><a
                                    href="javascript:window.parent.location='/RailExamBao/Default.aspx'" onclick="if (!window.confirm('您确认要退出吗？')){return false;}">退出</a>
                            </td>
                        </tr>
                    </table>
                </td>
                <td height="29">
                    &nbsp;
                    </td>
            </tr>
        </table>
        <table width="100%"  border="0" cellpadding="0" cellspacing="0" background="images/main_31.gif">
            <tr>
                <td width="8" height="29">
                    <img src="images/main_28.gif" width="8" height="29" />
                </td>
                <td width="147" height="29" background="images/main_29.gif">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="24%">
                                &nbsp;
                            </td>
                            <td width="43%" height="20" valign="bottom">
                                管理菜单
                            </td>
                            <td width="33%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="39" height="29">
                    <img src="images/main_30.gif" width="39" height="29" />
                </td>
                <td height="30">
                当前系统服务器：
                   <asp:Label ID="lblDate" runat="server" ForeColor="#003366" ></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                服务器编号为：<asp:Label ID="lblServerNo" runat="server"></asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;
               服务器名称为：<asp:Label ID="lblServerName" runat="server"></asp:Label> 
                </td>
                <td width="17" height="29">
                    <img src="images/main_32.gif" width="17" height="29" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>