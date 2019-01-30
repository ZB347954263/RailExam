<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ExamManage.aspx.cs" Inherits="RailExamWebApp.Exam.ExamManage" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考试设计</title>
    <script type="text/javascript">       
        function tvExamCategory_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {            
                window.frames["ifExamManageInfo"].location = "ExamManageInfo.aspx?id="+ node.get_value(); 
            }
             else
            {
             window.frames["ifExamManageInfo"].location = "ExamManageInfo.aspx?id="+ tvExamCategory.get_nodes().getNode(0).get_value(); 
            }
        }       
                 
        function imgBtns_onClick(btn)
        {
          　var flagupdate=document.getElementById("HfUpdateRight").value;
        	  if(flagupdate=="False"&&btn.action == "new")
              {
                alert("您没有权限使用该操作！");                       
                return;
              }
                      
            if(btn.action == "new")
            {
                   if(!tvExamCategory.get_selectedNode())
                   {
                        alert("请选择一个考试类别！");
                        return;
                   } 
　                window.frames["ifExamManageInfo"].document.getElementById(btn.id).onclick();
            } 
            else
            {
                if(window.frames["ifExamManageInfo"].document.getElementById("query").style.display == "none")
                    window.frames["ifExamManageInfo"].document.getElementById("query").style.display = "";
                else
                    window.frames["ifExamManageInfo"].document.getElementById("query").style.display = "none";
            }
        }      
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        考试管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        考试设计</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img id="NewButton" onclick="imgBtns_onClick(this);" src="../Common/Image/add.gif"
                        action="new" alt="" />
                    <img id="FindButton" onclick="imgBtns_onClick(this);" src="../Common/Image/find.gif"
                        action="find" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        考试分类</div>
                    <div id="leftContent">
                        <ComponentArt:TreeView ID="tvExamCategory" runat="server" EnableViewState="true">
                            <ClientEvents>
                                <NodeSelect EventHandler="tvExamCategory_onNodeSelect" />
                            </ClientEvents>
                        </ComponentArt:TreeView>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContentWithNoHead">
                        <iframe id="ifExamManageInfo" src="ExamManageInfo.aspx?id=0" frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div> <asp:HiddenField ID="HfUpdateRight" runat="server" />
    </form>
</body>
</html>
