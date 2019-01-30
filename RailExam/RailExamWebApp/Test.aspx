<%@ Page Language="C#" AutoEventWireup="True" Codebehind="Test.aspx.cs" Inherits="RailExamWebApp.Test" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="~/App_Themes/Default/treeStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/RailExamBao/Common/JS/jquery1.3.2.js"></script>

    <script type="text/javascript">
        // Functions used by this page
        function Grid1_onContextMenu(sender, eventArgs)
        {
            Grid1Menu.showContextMenu(); 
            
            return true;
        }    
        
        
        function show(num) 
        { 
            var params = '{str:"'+ num+'"}'; 
            $.ajax(
            { 
                type: "POST", 
                url: "Test.aspx/aa", 
                data: params, 
                dataType: "text", 
                contentType: "application/json; charset=utf-8", 
                beforeSend: function(XMLHttpRequest) 
                { 
                    $('#show').text("正在查询"); 
                }, 
                success: function(msg) 
                { 
                    $('#show').text(eval("(" + msg + ")").d); 
                }, 
                error: function(xhr, msg, e) 
                { 
                    alert(msg); 
                } 
            }); 
        }    
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="click" onclick="show('执行成功');">
            点击我！
        </div>
        <div id="show">
        </div>
<%--        <div>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetOrganizations1"
                TypeName="RailExam.BLL.OrganizationBLL"></asp:ObjectDataSource>
            <ComponentArt:Grid ID="Grid1" runat="server" DataSourceID="ObjectDataSource1">
                <Levels>
                    <ComponentArt:GridLevel DataKeyField="OrganizationId">
                        <Columns>
                            <ComponentArt:GridColumn DataField="OrganizationId" HeadingText="机构ID" />
                            <ComponentArt:GridColumn DataField="ShortName" HeadingText="机构简称" />
                            <ComponentArt:GridColumn DataField="FullName" HeadingText="机构全称" />
                            <ComponentArt:GridColumn DataField="Phone" HeadingText="联系电话" />
                            <ComponentArt:GridColumn DataField="Address" HeadingText="地址" />
                        </Columns>
                    </ComponentArt:GridLevel>
                </Levels>
                <ClientEvents>
                    <ContextMenu EventHandler="Grid1_onContextMenu" />
                </ClientEvents>
            </ComponentArt:Grid>
            <ComponentArt:Menu ID="Grid1Menu" runat="server" EnableViewState="false" SiteMapXmlFile="system/PostMenu.xml">
            </ComponentArt:Menu>
        </div>--%>
    </form>
</body>
</html>
