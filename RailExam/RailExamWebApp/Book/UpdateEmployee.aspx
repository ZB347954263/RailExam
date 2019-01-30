<%@ Page Language="C#" AutoEventWireup="true" Codebehind="UpdateEmployee.aspx.cs"
    Inherits="RailExamWebApp.Book.UpdateEmployee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>更改教材负责人</title>
    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
   		 function selectEmployee() 
   		 {
	 	 var ret = showCommonDialog("/RailExamBao/RandomExam/RandomExamManageFourthAdd.aspx?type=Book",'dialogWidth:800px;dialogHeight:600px;');
        
	 	if(ret != "" && ret!=undefined) {
	 	    document.getElementById("hfEmployeeID").value = ret.split('|')[0];
	 		document.getElementById("txtAuthors").value = ret.split('|')[1];
	 	}
	 	else {
	 		document.getElementById("hfEmployeeID").value ="";
	 		document.getElementById("txtAuthors").value = "";
	 	}
	 	

	 }
   		 
   			 function selectNewEmployee() 
      {
	 	 var ret = showCommonDialog("/RailExamBao/RandomExam/RandomExamManageFourthAdd.aspx?type=Book",'dialogWidth:800px;dialogHeight:600px;');
        
	 	if(ret != "" && ret!=undefined) {
	 	    document.getElementById("hfNewEmployeeID").value = ret.split('|')[0];
	 		document.getElementById("txtNewAuthors").value = ret.split('|')[1];
	 	}
	 	else {
	 		document.getElementById("hfNewEmployeeID").value ="";
	 		document.getElementById("txtNewAuthors").value = "";
	 	}
	 	

	 } 
   			 
     function valid() {
     	 if(document.getElementById("txtAuthors").value == "") {
     	 	alert("请选择原负责人！");
     	 	return false;
     	 }
     	
     	 if(document.getElementById("txtNewAuthors").value == "") {
     	 	alert("请选择现负责人！");
     	 	return false;
     	 }
     	
     	if(!confirm("您确定要更新教材负责人吗？")) {
     		return false;
     	}

     	return true;
     }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <br/>
           <br/> 
            <div >
                <table>
                    <tr>
                        <td>
                            原负责人</td>
                        <td>
                            <asp:TextBox ID="txtAuthors" MaxLength="100" runat="server"  ReadOnly="true">
                            </asp:TextBox>
                            <asp:HiddenField ID="hfEmployeeID" runat="server" />
                            <img id="ImgSelectEmployee" style="cursor: hand;" onclick="selectEmployee();" src="../Common/Image/search.gif"
                                alt="选择负责人" border="0" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            现负责人</td>
                        <td>
                            <asp:TextBox ID="txtNewAuthors" MaxLength="100" runat="server"  ReadOnly="true">
                            </asp:TextBox>
                            <asp:HiddenField ID="hfNewEmployeeID" runat="server" />
                            <img id="Img1" style="cursor: hand;" onclick="selectNewEmployee();" src="../Common/Image/search.gif"
                                alt="选择负责人" border="0" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button runat="server" ID="btnOk" CssClass="button" Text="确  定" OnClientClick="return valid();" OnClick="btnOK_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
