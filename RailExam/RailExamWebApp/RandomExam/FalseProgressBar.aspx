<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FalseProgressBar.aspx.cs" Inherits="RailExamWebApp.RandomExam.FalseProgressBar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title> <%=RailExamWebApp.Common.Class.PrjPub.GetRailNameBao()%>职工教育培训考试系统</title>
   <script type="text/javascript">
   			function check()
			{
				if(WaitFrame.window.load)
				{
				    top.returnValue = "true";
					window.close();  
				}
			}
   </script> 
</head>
<body onload="check()" >
    <form id="form1" runat="server">
    <div>
            <table style="vertical-align: middle; height: 30px;">
            <tr>
                <td>
                    <font face="Verdana, Arial, Helvetica" size="2" color="#ea9b02">
                    <asp:Label ID="lblInfo" runat="server" Font-Bold="true" ></asp:Label></font><br />
                    <img id="img1" src="../Common/Image/Progress.gif" /> 
                </td>
            </tr>
        </table>
        <iframe id="WaitFrame"  frameBorder="no" width="0" height="0">
		</iframe>
		<script type="text/javascript"> 
            var search = window.location.search;
            window.frames["WaitFrame"].location = "DealPage.aspx"+search;
        </script>
    </div>
    </form>
</body>
</html>
