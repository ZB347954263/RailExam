<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AttendExamStart.aspx.cs" EnableEventValidation="true"
    Inherits="RailExamWebApp.RandomExam.AttendExamStart" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>请求考试</title>

    <script type="text/javascript">
 
        /*function showTime()
		{
		    var search = window.location.search;        
            if(document.getElementById("hfNow").value == "1")
            {
                 window.location = "/RailExamBao/RandomExam/AttendExamLeft.aspx"+search;
            	 //window.location = "/RailExamBao/RandomExam/CheckAttendExamInfo.aspx"+search;
            } 
            else  if(document.getElementById("hfNow").value == "-1")
            {
               window.close();
            }    				
			setTimeout("showTime()", 10000);				
		}	*/
		
		
    	function CallBack1_CallbackComplete() {
    		var search = window.location.search;        
    		if(document.getElementById("hfNow").value == "1")
    		{
    			window.location = "/RailExamBao/RandomExam/AttendExamLeft.aspx"+search;
    		} 
    		else  if(document.getElementById("hfNow").value == "-1")
    		{
    			window.close();
    		}    
    	}
		
    	function closedel()
    	{
    		return confirm("关闭此页面将中断此次考试请求，您确定关闭此页面吗？");
    	}
        
    </script>

</head>
<body ><!--onload="showTime()"-->
    <form id="form1" runat="server">
        <div style="text-align: center">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <font style="color: #2D67CF; font-size: 30pt;">
                            <asp:Label ID="lblTitle" runat="server" Text="请输入考试验证码"></asp:Label></font>
                        <br />
                        <br />
                        <asp:TextBox ID="txtCode" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                        <br />
                        <asp:Button ID="btnOK" Text="请求考试" CssClass="button" runat="server" OnClick="btnOK_Click" />&nbsp;&nbsp;&nbsp;
                        <font style="color: #2D67CF; font-size: 30pt;">
                            <asp:Label ID="lblApply" runat="server"></asp:Label></font><br />
                        <br />
                        <asp:Image ID="img" runat="server" ImageUrl="../Common/Image/Progress.gif" Visible="false" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <asp:Button ID="btnClose" Text="关  闭" OnClientClick="return closedel();" CssClass="button"
                runat="server" OnClick="btnClose_Click" Visible="false" />
            <ComponentArt:CallBack OnCallback="CallBack1_Callback" PostState="true" ID="CallBack1"
                RefreshInterval="10000" runat="server">
                <Content>
                    <asp:HiddenField ID="hfNow" runat="server" />
                </Content>
               <ClientEvents>
                   <CallbackComplete EventHandler="CallBack1_CallbackComplete"></CallbackComplete>
               </ClientEvents> 
            </ComponentArt:CallBack>
        </div>
        <asp:HiddenField ID="hfID" runat="server" />
    </form>
   <script type="text/javascript">
   	if(document.getElementById("txtCode")) {
    	document.getElementById("txtCode").focus();
   	}
   </script> 
</body>
</html>
