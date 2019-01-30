<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Courseware.aspx.cs" Inherits="RailExamWebApp.Courseware.Courseware" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�μ�����</title>
    <script type="text/javascript">
        var flag=0;
        function $F(objId)
        {
            return document.getElementById(objId);
        }
        
        function tvCourseware_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node){
                window.frames["ifCoursewareInfo"].location = "CoursewareInfo.aspx?type=Courseware&id=" + node.get_id();
               node.expand(); 
            }
        }
        
        function tvTrainType_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node){
                window.frames["ifCoursewareInfo"].location = "CoursewareInfo.aspx?type=TrainType&id="+ node.get_id();
               node.expand();  
            }
        }
        
                function rbnClicked(btn)
        {
            if(btn.checked)
            {
                if(btn.id == "rbnCoursewareType")
                {
                    $F("divCourseware").style.display = "";
                    $F("divTrainType").style.display = "none";
                  
                    if(tvCourseware.get_selectedNode())
                    {
                        temp = tvCourseware.get_selectedNode();
                       window.frames["ifCoursewareInfo"].location = "CoursewareInfo.aspx?type=Courseware&id=" + temp.get_id(); 
                    }
                    else
                    {
                        window.frames["ifCoursewareInfo"].location = "CoursewareInfo.aspx?type=Courseware&id=0";
                    }
                }
                else
                {
                    $F("divCourseware").style.display = "none";
                    $F("divTrainType").style.display = "";
                    
                    if(tvTrainType.get_selectedNode())
                    {
                        temp = tvTrainType.get_selectedNode();
                       window.frames["ifCoursewareInfo"].location = "CoursewareInfo.aspx?type=TrainType&id="+ temp.get_id(); 
                    }
                    else
                    {
                         window.frames["ifCoursewareInfo"].location = "CoursewareInfo.aspx?type=TrainType&id=0";
                    }
                }
            }
        }
        
        function imgBtns_onClick(btn)
        {
            if(btn.action == "new")
            {
            	var node = tvCourseware.get_selectedNode();
				if(node)
				{
				��	if(node.get_nodes().get_length() > 0)
				��	{
				��	    alert("��ѡ��֪ʶ��ϵ����Ҷ�ӽڵ㣡");
				��	   return; 
				��	}
			    }
			    else
			    {
			        alert("��ѡ��һ��֪ʶ��ϵ��");
			        return;
			    }
            	window.frames["ifCoursewareInfo"].document.getElementById(btn.id).onclick();
            }
            else
            {
                if(window.frames["ifCoursewareInfo"].document.getElementById("query").style.display == "none")
                    window.frames["ifCoursewareInfo"].document.getElementById("query").style.display = "";
                else
                    window.frames["ifCoursewareInfo"].document.getElementById("query").style.display = "none";
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
                        ֪ʶ����</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        �μ�����</div>
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
                        <input id="rbnCoursewareType" name="CoursewareOrTrainType" onclick="rbnClicked(this);" type="radio" checked="checked" style="display: none;" />֪ʶ��ϵ
                        <input id="rbnTrainType" name="CoursewareOrTrainType" onclick="rbnClicked(this);" type="radio" style="display: none;"/>
                    </div>
                    <div id="leftContent">
                        <div id="divCourseware">
                            <ComponentArt:TreeView ID="tvCourseware" runat="server" EnableViewState="false">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvCourseware_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                        <div id="divTrainType" style="display: none;">
                            <ComponentArt:TreeView ID="tvTrainType" runat="server" EnableViewState="false">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvTrainType_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContentWithNoHead">
                        <iframe id="ifCoursewareInfo" src="CoursewareInfo.aspx?type=Courseware&id=0" frameborder="0" 
                            scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <input type="hidden" name="DeleteID" />
          <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>
</body>
</html>
