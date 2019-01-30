<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="PaperManage.aspx.cs" Inherits="RailExamWebApp.Paper.PaperManage" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�Ծ���Ϣ</title>
    <script type="text/javascript">     
        function tvPaperCategory_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node){
                document.getElementById("ifPaperManageInfo").src = "PaperManageInfo.aspx?id="+ node.get_value(); 
            }
        }
         
        function imgBtns_onClick(btn)
        {
           var flagupdate=document.getElementById("HfUpdateRight").value;
          if(flagupdate=="False"&&btn.action == "new")
          {
            alert("��û��Ȩ��ʹ�øò�����");                       
            return;
          }
                      
           
            if(btn.action == "new")
            {
                if(!tvPaperCategory.get_selectedNode())
               {
                    alert("��ѡ��һ���Ծ����");
                    return;
               } 
                window.frames["ifPaperManageInfo"].document.getElementById(btn.id).onclick();
            } 
            else
            {
                if(window.frames["ifPaperManageInfo"].document.getElementById("query").style.display == "none")
                    window.frames["ifPaperManageInfo"].document.getElementById("query").style.display = "";
                else
                    window.frames["ifPaperManageInfo"].document.getElementById("query").style.display = "none";
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
                        �Ծ����
                    </div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        �Ծ���Ϣ</div>
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
                        �Ծ����</div>
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
                        <iframe id="ifPaperManageInfo" src="PaperManageInfo.aspx?id=0" frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div> <asp:HiddenField ID="HfUpdateRight" runat="server" />  
    </form>
    <script type="text/javascript">
    </script>
</body>
</html>
