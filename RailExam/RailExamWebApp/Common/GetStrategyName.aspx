<%@ Page Language="C#" AutoEventWireup="true" Codebehind="GetStrategyName.aspx.cs"
    Inherits="RailExamWebApp.Common.GetStrategyName" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>策略信息</title>

    <script type="text/javascript"> 
     
        function searchButton_onClick()
        {
           var id= document.getElementById('txtname').value;
            if(id !="")
	        {     	            
	            window.returnValue = id;
	            window.close();	    		
           }
           else
           {
           alert("请输入名称！");
           }
        }        
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        输入策略名称</div>
                </div>
            </div>
            <div id="content">
                &nbsp;策略名称：<asp:TextBox runat="server" ID="txtname" MaxLength="50" Width="230px"></asp:TextBox>
                <br />
                <input type="button" id="searchButton" class="buttonSearch" value="确定" onclick="return searchButton_onClick();" />
            </div>
        </div>
    </form>
</body>
</html>
