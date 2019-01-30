<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetRandomExam.aspx.cs" Inherits="RailExamWebApp.RandomExam.ResetRandomExam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择补考考生</title>
   <base target="_self"/> 
   <script type="text/javascript">
   	    function showConfirm() {
	         if(! confirm("您确定要为“" + document.getElementById("hfName").value + "”生成补考考试吗？"))
            {
                return false;
            }
   	    	return true;
   	    }
   </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <br/>
            <asp:DropDownList runat="server" ID="ddlSelect">
                <asp:ListItem  Value="0" Text="不及格和未参加考试考生"></asp:ListItem>
               <asp:ListItem  Value="1" Text="不及格考生"></asp:ListItem>
               <asp:ListItem  Value="2" Text="未参加考试考生"></asp:ListItem> 
            </asp:DropDownList>
        <br/>
         <asp:Button runat="server" ID="btnOK" OnClick="btnOK_Click" OnClientClick="return showConfirm();" CssClass="button" Text="确  定"/>
    </div>
   <asp:HiddenField runat="server" ID="hfName"/> 
    </form>
</body>
</html>
