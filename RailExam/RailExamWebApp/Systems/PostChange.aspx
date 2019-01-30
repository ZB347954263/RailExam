<%@ Page Language="C#" AutoEventWireup="true" Codebehind="PostChange.aspx.cs" Inherits="RailExamWebApp.Systems.PostChange" %>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>工作岗位</title>

    <script type="text/javascript">
        function selectOtherPost()
        {
        	var name = escape(document.getElementById("txtOldPost").value);
            var selectedPost = window.showModalDialog('../Common/SelectOtherPost.aspx?id='+ document.getElementById("hfOldPostID").value+"&names="+name, 
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:580px');

            if(! selectedPost)
            {
                return;
            }
            
            document.getElementById("hfOldPostID").value = selectedPost.split('|')[0];
            document.getElementById("txtOldPost").value = selectedPost.split('|')[1];
        }
        
       function selectPost()
        {
            var selectedPost = window.showModalDialog('../Common/SelectSecondPost.aspx', 
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:580px');

            if(! selectedPost)
            {
                return;
            }
            
            document.getElementById("hfNewPostID").value = selectedPost.split('|')[0];
            document.getElementById("txtNewPost").value = selectedPost.split('|')[1];
        } 
       
       function savePost() {
       	 if( document.getElementById("txtOldPost").value  =="") {
       	 	alert("请选择“其他”下职名");
       	 	return false;
       	 }
       	 
       	 if( document.getElementById("txtNewPost").value  =="") {
       	 	alert("请选择修改至工种");
       	 	return false;
       	 }
       	return true;
       }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        系统管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        工作岗位变动</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <asp:Button runat="server" ID="btnSave" CssClass="button" Text="保存" OnClientClick="return savePost();" OnClick="btnSave_Click"/>
                </div>
            </div>
            <div id="content">
                <table>
                    <tr>
                        <td>
                            选择职名</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtOldPost"  Enabled="False"/>
                            <a onclick="selectOtherPost()" href="#">
                                <asp:Image runat="server" ID="img2" ImageUrl="../Common/Image/search.gif" AlternateText="选择“其他”下职名">
                                </asp:Image>
                            </a>
                            <asp:HiddenField runat="server" ID="hfOldPostID" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            选择修改至工种</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtNewPost" Enabled="False"/>
                            <a onclick="selectPost()" href="#">
                                <asp:Image runat="server" ID="Image1" ImageUrl="../Common/Image/search.gif" AlternateText="选择修改至工种">
                                </asp:Image>
                            </a>
                            <asp:HiddenField runat="server" ID="hfNewPostID" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:HiddenField ID="hfSelectedMenuItem" runat="server" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="hfMaxID" runat="server" Value="0" />
        <input type="hidden" name="IsWuhanOnly" value='<%=PrjPub.IsWuhanOnly()%>' />
    </form>
</body>
</html>
