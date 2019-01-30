<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeeTransferManageDetail.aspx.cs"
    Inherits="RailExamWebApp.Systems.EmployeeTransferManageDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择单位</title>

    <script type="text/javascript">
      function  selectPost() {
    	var selectedPost = window.showModalDialog('../Common/SelectPost.aspx', 
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:580px');
            if(! selectedPost)
            {
                return;
            }
            document.getElementById("hfPostID").value = selectedPost.split('|')[0];
            document.getElementById("txtNewPost").value = selectedPost.split('|')[1];
         }
    
        function  selectOrg() {
        	var orgid = document.getElementById("hfLoginOrgID").value;
    	var selectedOrg = window.showModalDialog('../Common/SelectOrganization.aspx?OrgID='+orgid, 
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:580px');
            if(! selectedOrg)
            {
                return;
            }
            document.getElementById("hfOrgID").value = selectedOrg.split('|')[0];
            document.getElementById("txtNewOrg").value = selectedOrg.split('|')[1];
    }
        function ColAndReturn() {
        	var orgID = document.getElementById("hfOrgID").value;
        	var postID = document.getElementById("hfPostID").value;
        	var orgIDAndPostID = [];
        	orgIDAndPostID.push(orgID);
        	orgIDAndPostID.push(postID);
        	if(orgID=="" && postID=="") {
        		alert("至少选择一项！");
        		return;
        	}
        	window.returnValue = orgIDAndPostID;
        	window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head" style="width: 370px">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current" runat="server">
                        选择单位</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 13%;">
                            机构 
                        </td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtNewOrg" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                            <a onclick="selectOrg()" href="#">
                                <asp:Image runat="server" ID="imgNewOrg" ImageUrl="../Common/Image/search.gif" AlternateText="选择职名">
                                </asp:Image>
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 13%;">
                            职名 
                        </td>
                        <td style="width: 40%;">
                            <asp:TextBox ID="txtNewPost" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                            <a onclick="selectPost()" href="#">
                                <asp:Image runat="server" ID="imgPost" ImageUrl="../Common/Image/search.gif" AlternateText="选择职名">
                                </asp:Image>
                            </a>
                        </td>
                    </tr>
                </table>
                <div align="center" style="margin-top: 20px">
                    <input type="button" value="保  存" onclick="ColAndReturn()" class="button" id="btnOK" />
                    <input type="button" value="关  闭" onclick="javascript:window.close();" class="button"
                        id="btnCancel" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfPostID" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <asp:HiddenField ID="hfLoginOrgID" runat="server" />
    </form>
</body>
</html>
