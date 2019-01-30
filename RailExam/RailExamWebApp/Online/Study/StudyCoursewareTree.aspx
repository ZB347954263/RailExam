<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudyCoursewareTree.aspx.cs" Inherits="RailExamWebApp.Online.Study.StudyCoursewareTree" %>


<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>学习课件</title>
    <script type="text/javascript">
        function $F(objId)
        {
            return document.getElementById(objId);
        }
        
        function tvCourseware_onNodeSelect(sender, eventArgs)
        {
             var search = window.location.search;
             var str= search.substring(search.indexOf("?")+1);  
            var node = eventArgs.get_node();
            if(node){
                window.frames["ifReadCourseDetail"].location = "StudyCourseware.aspx?CoursewareTypeID=" + node.get_id()+"&"+str;
            }
        }       
        
        function tvTrainType_onNodeSelect(sender, eventArgs)
        {
            var search = window.location.search;
            var str= search.substring(search.indexOf("?")+1);             
            var node = eventArgs.get_node();
            if(node){            
                window.frames["ifReadCourseDetail"].location = "StudyCourseware.aspx?TrainTypeID="+ node.get_id()+"&"+str;
            }
        }   
        
        function rbnClicked(btn)
        {
             var search = window.location.search;
             var str= search.substring(search.indexOf("?")+1);        
            if(btn.checked)
            {
                if(btn.id == "rbnCourseware")
                {
                    $F("divCourseware").style.display = "";
                    $F("divTrainType").style.display = "none";
                    
                    if(tvCourseware.get_selectedNode())
                    {
                        temp = tvCourseware.get_selectedNode();
                        window.frames["ifReadCourseDetail"].location = "StudyCourseware.aspx?CoursewareTypeID="+ temp.get_id()+"&"+str;
                    }
                     else
                      {
                            if(tvCourseware && tvCourseware.get_nodes().get_length() > 0)
                            {
                                tvCourseware.get_nodes().getNode(0).select();
                            }                      
                    }               
                }
                else
                {
                    $F("divCourseware").style.display = "none";
                    $F("divTrainType").style.display = "";
                    
                   if(tvTrainType.get_selectedNode())
                    {
                        temp = tvTrainType.get_selectedNode();
                        window.frames["ifReadCourseDetail"].location = "StudyCourseware.aspx?TrainTypeID="+ temp.get_id()+"&"+str;
                   }
                    else
                  {
                            if(tvTrainType && tvTrainType.get_nodes().get_length() > 0)
                            {
                                tvTrainType.get_nodes().getNode(0).select();
                            }                    
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
                    <div id="leftHead" style="white-space: nowrap;">分类:
                        <input id="rbnCourseware" name="CoursewareOrTrainType" onclick="rbnClicked(this);" type="radio" checked="checked" />按课件体系
                        <input id="rbnTrainType" name="CoursewareOrTrainType" onclick="rbnClicked(this);" type="radio" />按培训类别

                    </div>
                    <div id="leftContentWithNoHeight">
                        <div id="divCourseware">
                            <ComponentArt:TreeView ID="tvCourseware"  runat="server" EnableViewState="false">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvCourseware_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                        <div id="divTrainType" style="display: none;">
                            <ComponentArt:TreeView ID="tvTrainType"  runat="server" EnableViewState="false">
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
                        <iframe id="ifReadCourseDetail"  frameborder="0" scrolling="auto"  width="100%"></iframe>
                    </div>
                </div>
            </div>
        </div>
    </form>
       <script type="text/javascript">       
        if(tvCourseware && tvCourseware.get_nodes().get_length() > 0)
        {
            tvCourseware.get_nodes().getNode(0).select();
        }
      document.getElementById("rightContentWithNoHeight").height = 0.9*screen.availHeight;
      document.getElementById("ifReadCourseDetail").height = 0.9*screen.availHeight;  
    </script>  
</body>
</html>

