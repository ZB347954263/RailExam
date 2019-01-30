<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="SelectOrganizations.aspx.cs" Inherits="RailExamWebApp.Notice.SelectOrganizations" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择接收机构</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="contentHead">
                    组织机构</div>
                <div id="mainContent">
                    <ComponentArt:TreeView ID="tvOrg" runat="server" EnableViewState="true" KeyboardEnabled="true">
                    </ComponentArt:TreeView>
                </div>
                <div>
                    <br />
                    <asp:ImageButton ID="btnSave" runat="server" CausesValidation="False" OnClick="btnSave_Click"
                        ImageUrl="../Common/Image/confirm.gif" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
