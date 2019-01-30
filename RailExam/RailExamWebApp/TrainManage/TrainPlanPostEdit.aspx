<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TrainPlanPostEdit.aspx.cs"
    Inherits="RailExamWebApp.TrainManage.TrainPlanPostAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>新增计划职名</title>

    <script type="text/javascript">
    function SelectPost()
    {
    	var selectedIDs = document.getElementById("hfPostIDs").value;
    	var selectedNames= document.getElementById("hfPostNames").value;
    	var url1 = "../Common/MultiSelectPost.aspx?ids=" + selectedIDs+"&names="+selectedNames;

    	var selectedPost = window.showModalDialog(url1,
    		'', 'help:no; status:no; dialogWidth:300px;dialogHeight:620px;scroll:no;');
    	if (selectedPost != "" && selectedPost != undefined)
    	{
    	  var arrIDS = [];
	    		var arr = selectedPost.split("|")[0].split(",");
	    		for (var i = 0; i < arr.length; i++) {
	    			arrIDS.push(arr[i]);
	    		}
    		  document.getElementById("hfPostIDs").value = arrIDS.join(",");
	    	 document.getElementById("btnPost").click();

    	}
    	if (!selectedPost)
    	{
    		return;
    	}
    }
    function check() {
    	
    	if(document.getElementById("hfPostNames").value=="") {
    		alert('请选择职名！');
    		return false;
    	}
    	  if(document.getElementById("hfPostNames").value.length>1000) {
    	  	alert('输入长度超过1000个字符！');
    	  	return false;
    	  }
    	return true;
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head" style="width: 430px">
                <div id="location">
                    <div id="current" runat="server">
                        新增培训计划职名</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable" width="400px">
                    <tr>
                        <td style="width: 15%">
                            是否班组长</td>
                        <td>
                            <asp:CheckBox ID="chkIsLeader" runat="server" /></td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            职名</td>
                        <td>
                            <asp:TextBox ID="txtPostName" runat="server" Enabled="False" width="300px" MaxLength="1000"></asp:TextBox>
                          
                            <a href="#" title="选择职名"  onclick="SelectPost()"> 
                                <asp:Image runat="server" ID="Image" ImageUrl="../Common/Image/search.gif" AlternateText="选择职名">
                                </asp:Image> </a>
                        </td>
                    </tr>
                </table>
                <div align="center" style="margin-top: 20px">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="保  存" onClientClick="return check()" OnClick="btnSave_Click" />
                    <asp:Button ID="btnClose" runat="server" CssClass="button" Text="关  闭" OnClientClick="javascript:window.close()" />
                </div>
            </div>
        </div>
        <asp:Button ID="btnPost" runat="server" Text="" style="display: none" OnClick="btnPost_Click" />
        <asp:HiddenField ID="hfPostIDs" runat="server" />
        <asp:HiddenField ID="hfselectedPostIDs" runat="server" />
        <asp:HiddenField ID="hfPostNames" runat="server" />
    </form>
</body>
</html>
