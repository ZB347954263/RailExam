<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="SelectPaper.aspx.cs" Inherits="RailExamWebApp.Paper.SelectPaper" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>—°‘Ò ‘æÌ</title>
    <script type="text/javascript">     
        function tvPaperCategory_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node){            
                document.getElementById("iframe1").src = "SelectPaperDetail.aspx?id="+ node.get_value(); 
            }
        }
    </script>
</head>
<body>
    <form id="form2" runat="server">
        <div id="page">           
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                         ‘æÌ∑÷¿‡</div>
                    <div id="leftContent">
                        <ComponentArt:TreeView ID="tvPaperCategory" runat="server" EnableViewState="true">
                            <ClientEvents>
                                <NodeSelect EventHandler="tvPaperCategory_onNodeSelect" />
                            </ClientEvents>
                        </ComponentArt:TreeView>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContentWithNoHead">
                        <iframe id="iframe1" frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        if(tvPaperCategory && tvPaperCategory.get_nodes().get_length() > 0)
        {
            tvPaperCategory.get_nodes().getNode(0).select();
        }
    </script>
</body>
</html>
