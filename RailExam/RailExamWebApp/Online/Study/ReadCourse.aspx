<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ReadCourse.aspx.cs" Inherits="RailExamWebApp.Online.Study.ReadCourse" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>学习课件</title>

    <script type="text/javascript">
        function $F(objId)
        {
            return document.getElementById(objId);
        }
        
        function tvCourseware_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node){
                window.frames["ifReadCourseDetail"].location = "ReadCourseDetail.aspx?type=Courseware&id=" + node.get_id(); 
            }
        }       
        
        function tvTrainType_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node){            
                window.frames["ifReadCourseDetail"].location = "ReadCourseDetail.aspx?type=TrainType&id="+ node.get_id();
            }
        }   
       
//        function tvCourseware_onContextMenu(sender, eventArgs)
//        {
//            switch(eventArgs.get_node().get_text()){
//                default:
//                    treeContextMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_node());
//                    eventArgs.get_node().select();
//                    return false;
//                break;
//            }
//        }
        
        function rbnClicked(btn)
        {
            if(btn.checked)
            {
                if(btn.id == "rbnCourseware")
                {
                    $F("divCourseware").style.display = "";
                    $F("divTrainType").style.display = "none";
                    
                    if(tvCourseware.get_selectedNode())
                    {
                        temp = tvCourseware.get_selectedNode();
                        window.frames["ifReadCourseDetail"].location = "ReadCourseDetail.aspx?type=Courseware&id="+ temp.get_id();
                    }
                    else
                    {
                        window.frames["ifReadCourseDetail"].location = "ReadCourseDetail.aspx?type=Courseware&id=-1";
                    }                      
               }
                else
                {
                    $F("divCourseware").style.display = "none";
                    $F("divTrainType").style.display = "";
                    
                    if(tvTrainType.get_selectedNode())
                    {
                        temp = tvTrainType.get_selectedNode();
                        window.frames["ifReadCourseDetail"].location = "ReadCourseDetail.aspx?type=TrainType&id="+ temp.get_id();
                   }
                    else
                    {
                         window.frames["ifReadCourseDetail"].location = "ReadCourseDetail.aspx?type=TrainType&id=-1";
                    }      
                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="left">
                    <div id="leftHead" style="white-space: nowrap;">
                        分类:
                        <input id="rbnCourseware" name="CoursewareOrTrainType" onclick="rbnClicked(this);"
                            type="radio" checked="checked" />按课件体系
                        <input id="rbnTrainType" name="CoursewareOrTrainType" onclick="rbnClicked(this);"
                            type="radio" />按培训类别
                    </div>
                    <div id="leftContentWithNoHeight">
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
                    <div id="rightHead">
                        课件信息</div>
                    <div id="rightContentWithNoHeight">
                        <iframe id="ifReadCourseDetail" src="ReadCourseDetail.aspx?type=Courseware&id=-1"
                            frameborder="0" scrolling="auto" width="100%"></iframe>
                    </div>
                </div>
            </div>
        </div>

        <script type="text/javascript" language="javascript">
      //document.getElementById("leftContentWithNoHeight").height = 0.9*screen.availHeight;
      document.getElementById("rightContentWithNoHeight").height = 0.9*screen.availHeight;
      document.getElementById("ifReadCourseDetail").height = 0.9*screen.availHeight;
        </script>

    </form>
</body>
</html>
