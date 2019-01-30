<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeeWorkEdit.aspx.cs" 
    Inherits="RailExamWebApp.RandomExamTai.EmployeeWorkEdit" %>
<%@ Register TagPrefix="uc1" TagName="Date" Src="~/Common/Control/Date/Date.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>新增工作动态</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
     <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
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
    
    function  checkInfo() {
    	var newOrg = document.getElementById("txtNewOrg");
    	if(newOrg.value=="") {
    		alert("现机构不能为空！");
    		newOrg.focus();
    		return false;
    	}
       
    	var Post = document.getElementById("txtNewPost");
    	if(Post.value=="") {
    		alert("现职名不能为空！");
    		Post.focus();
    		return false;
    	}
    	
    	var Transfer = document.getElementById("txtTransfer_DateBox");
    	var date = document.getElementById("txtTransfer_compareValidator");
    	if(date.style.display!="none") {
    		alert("异动时间格式不正确！");
    		Transfer.focus();
    		return false;
    	}
    	if(Transfer.value.indexOf("-")<0 && Transfer.value.indexOf("/")<0) {
    		alert("异动时间不能为空！");
    		Transfer.focus();
    		return false;
    	}
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
         <div id="head" style="width: 610px">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current" runat="server">
                        新增工作动态</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 13%;">
                            原机构 
                        </td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtOldOrg" runat="server" Width="200px"  ReadOnly="true"></asp:TextBox>
                            
                        </td>
                        <td style="width: 13%;">
                            现机构<span class="require">*</span>
                        </td>
                        <td style="width: 40%;">
                            <asp:TextBox ID="txtNewOrg" runat="server" Width="200px" ReadOnly="true"></asp:TextBox>
                            <a onclick="selectOrg()" href="#">
                                <asp:Image runat="server" ID="imgNewOrg" ImageUrl="../Common/Image/search.gif" AlternateText="选择职名">
                                </asp:Image>
                            </a>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 13%;">
                            原职名
                        </td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtOldPost" runat="server" Width="200px"  ReadOnly="true"></asp:TextBox>
                            
                        </td>
                        <td style="width: 13%;">
                            现职名<span class="require">*</span>
                        </td>
                        <td style="width: 40%;">
                            <asp:TextBox ID="txtNewPost" runat="server" Width="200px" ReadOnly="true" ></asp:TextBox>
                             <a onclick="selectPost()" href="#">
                                <asp:Image runat="server" ID="imgPost" ImageUrl="../Common/Image/search.gif" AlternateText="选择职名">
                                </asp:Image>
                            </a>
                        </td>
                    </tr>
                      <tr>
                        <td style="width: 13%;">
                            调令
                        </td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtmobilizationorderno" runat="server" Width="98%" MaxLength="20"></asp:TextBox>
                        </td>
                        <td style="width: 13%;">
                            异动时间<span class="require">*</span>
                        </td>
                        <td style="width: 40%;">
                           <uc1:Date ID="txtTransfer" runat="server" />
                        </td>
                    </tr>
                </table>
                <div align="center" style="margin-top: 20px">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="保  存" OnClientClick="return checkInfo()" OnClick="btnSave_Click" />
                    <asp:Button ID="btnClose" runat="server" CssClass="button" Text="关  闭" OnClientClick="javascript:window.close()" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfPostID" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <asp:HiddenField ID="hfLoginOrgID" runat="server"/>
        <asp:HiddenField ID="oldPostID" runat="server" /> 
        <asp:HiddenField ID="oldOrgID" runat="server" />
        <asp:HiddenField ID="empID" runat="server" /> 
        
    </form>
</body>
</html>
