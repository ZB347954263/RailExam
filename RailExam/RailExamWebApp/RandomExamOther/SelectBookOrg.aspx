<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectBookOrg.aspx.cs" Inherits="RailExamWebApp.RandomExamOther.SelectBookOrg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择组织机构</title>
   <script type="text/javascript">
        function valid()
        {
            var node = tvOrganization.get_selectedNode();
            if(! node)
            {
                alert("请选择一个组织机构！");
                return false;
            }
            
            if(!confirm("您确定将该本教材设置为“"+node.get_text()+"”吗？"))
            {
               return false; 
            }
            
            document.getElementById("hfOrg").value = node.get_id();
            
            return true;
        }
   </script> 

    <script src="../Common/JS/JSHelper.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="contentHead">
                    组织机构&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnOk" runat="server" CssClass="button" Text="确  定" OnClientClick="return valid();" OnClick="btnOk_Click" />
                </div>
                <div id="mainContent">
                    <ComponentArt:TreeView ID="tvOrganization" runat="server" EnableViewState="false">
                    </ComponentArt:TreeView>
                </div>
            </div>
        </div>
        <asp:HiddenField  runat="server" ID="hfOrg"/>
    </form>
</body>
</html>
