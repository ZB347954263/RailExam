<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainAimExercise.aspx.cs"
    Inherits="RailExamWebApp.Train.TrainAimExercise" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择练习</title>

    <script type="text/javascript">     
        
        function TreeView1_onNodeSelect(sender, eventArgs)
        {
            var search = window.location.search;
            var str = search.substring(search.indexOf("?")+1);
            var node = eventArgs.get_node();
            if(node)
            {            
                document.getElementById("iframe1").src = "TrainAimExerciseDetail.aspx?"+str+"&id="+ node.get_id(); 
            }
        }       
        
       	function AddRecord()
		{			
		    var node= TreeView1.get_selectedNode();
			if(node)
			{  
	            var re= window.open("../Paper/PaperManageEdit.aspx?categoryId="+node.get_id(),'PaperManageEdit',' Width=800px; Height=600px,status=false,resizable=no',true);		
			    re.focus();  
			}	
			else				   
			{
			   var re= window.open("../Paper/PaperManageEdit.aspx?categoryId="+1,'PaperManageEdit',' Width=800px; Height=600px,status=false,resizable=no',true);		
			   re.focus(); 
			}			    
		}
         
       function  QueryRecord()
       {
          var search = window.location.search;
          var str = search.substring(search.indexOf("?")+1);
          var node= TreeView1.get_selectedNode();
		  if(node)
		  {  
		   document.getElementById("iframe1").src = "TrainAimExerciseDetail.aspx?"+str+"&Flag=1&&id="+ node.get_id(); 
		  }
		  else
		  {
		   document.getElementById("iframe1").src = "TrainAimExerciseDetail.aspx?"+str+"&Flag=1&&id="+1; 
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
                        练习管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        练习信息</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img id="Button1" onclick="AddRecord();" src="../Common/Image/add.gif" alt="" />&nbsp;
                    <img id="Img1" onclick="QueryRecord();" src="../Common/Image/find.gif" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        练习分类树</div>
                    <div id="leftContent">
                        <ComponentArt:TreeView ID="TreeView1" runat="server" height="575" EnableViewState="true" DragAndDropEnabled="false"
                            NodeEditingEnabled="False" KeyboardEnabled="False" MultipleSelectEnabled="true"
                            CollapseNodeOnSelect="false" KeyboardCutCopyPasteEnabled="true" DisplayMargin="False"
                            ShowLines="true" DefaultTarget="iframe1">
                            <ClientEvents>
                                <NodeSelect EventHandler="TreeView1_onNodeSelect" />
                            </ClientEvents>
                        </ComponentArt:TreeView>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContent">
                        <iframe id="iframe1" frameborder="0" scrolling="auto" height="600" width="630"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <input id="Test1" type="hidden" name="Test1" />
    </form>
    <script type="text/javascript">
       var search = window.location.search;
       var str = search.substring(search.indexOf("?")+1);
        var theFrame = window.frames["iframe1"];
        theFrame.location = "TrainAimExerciseDetail.aspx?"+str+"&id=1";
        
        // Preload CSS-referenced images 
        (new Image()).src = '~/App_Themes/Default/images/Menu/group_background.gif';
        (new Image()).src = '~/App_Themes/Default/images/Menu/break_bg.gif';
    </script>
</body>
</html>
