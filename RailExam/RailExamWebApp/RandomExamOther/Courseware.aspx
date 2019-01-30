<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Courseware.aspx.cs" Inherits="RailExamWebApp.RandomExamOther.Courseware" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>课件管理</title>

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
        
        function tvPost_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node){
                window.frames["ifCoursewareInfo"].location = "CoursewareInfo.aspx?type=Post&id="+ node.get_id();
               node.expand();  
            }
        }
        
       function ddlView_onChange(btn)
        {
            var ddlView = $F("ddlView");
            switch(ddlView.selectedIndex)
            {
                case 0:
                {
                    $F("divCourseware").style.display = "";
                    $F("divTrainType").style.display = "none";
                    $F("divPost").style.display = "none";
                  
                    if(tvCourseware.get_selectedNode())
                    {
                        temp = tvCourseware.get_selectedNode();
                       window.frames["ifCoursewareInfo"].location = "CoursewareInfo.aspx?type=Courseware&id=" + temp.get_id(); 
                    }
                    else
                    {
                        window.frames["ifCoursewareInfo"].location = "CoursewareInfo.aspx?type=Courseware&id=0";
                    }
                    break;
                }
                case 1:
                {
                    $F("divCourseware").style.display = "none";
                    $F("divTrainType").style.display = "";
                    $F("divPost").style.display = "none";
                    
                    if(tvTrainType.get_selectedNode())
                    {
                        temp = tvTrainType.get_selectedNode();
                       window.frames["ifCoursewareInfo"].location = "CoursewareInfo.aspx?type=TrainType&id="+ temp.get_id(); 
                    }
                    else
                    {
                         window.frames["ifCoursewareInfo"].location = "CoursewareInfo.aspx?type=TrainType&id=0";
                    }
                    break;
                }
                case 2:
                {
                    $F("divCourseware").style.display = "none";
                    $F("divTrainType").style.display = "none";
                    $F("divPost").style.display = "";
                    
                    if(tvPost.get_selectedNode())
                    {
                        temp = tvPost.get_selectedNode();
                       window.frames["ifCoursewareInfo"].location = "CoursewareInfo.aspx?type=Post&id="+ temp.get_id(); 
                    }
                    else
                    {
                         window.frames["ifCoursewareInfo"].location = "CoursewareInfo.aspx?type=Post&id=0";
                    }
                    break;
                }
                default:
                {
                    break;
                }
            } 
        }
        
        function imgBtns_onClick(btn)
        {
            if(btn.action == "new")
                window.frames["ifCoursewareInfo"].document.getElementById(btn.id).onclick();
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
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        知识管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        课件管理</div>
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
                        分类: 查看方式
                        <select id="ddlView" onchange="ddlView_onChange();">
                            <option>按知识体系</option>
                            <option>按培训类别</option>
                            <option>按工作岗位</option>
                        </select>
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
                        <div id="divPost" style="display: none;">
                            <ComponentArt:TreeView ID="tvPost" runat="server" DefaultTarget="ifBookInfo">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvPost_onNodeSelect" />
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
