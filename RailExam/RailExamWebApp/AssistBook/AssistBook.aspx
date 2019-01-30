<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssistBook.aspx.cs" Inherits="RailExamWebApp.AssistBook.AssistBook" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>辅导教材管理</title>
    <script type="text/javascript">     
        function $F(objId)
        {
            return document.getElementById(objId);
        }
        
        function tvAssist_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {
                window.frames["ifBookInfo"].location = "AssistBookInfo.aspx?id="+ node.get_id();
               node.expand(); 
            }
        }
       
//        function tvTrainType_onNodeSelect(sender, eventArgs)
//        {
//            var node = eventArgs.get_node();
//            if(node)
//            {
//                window.frames["ifBookInfo"].location = "AssistBookInfo.aspx?id1="+ node.get_value();
//            }
//        }   
        
//        function rbnClicked(btn)
//        {
//            if(btn.checked)
//            {
//                if(btn.id == "rbnAssist")
//                {
//                    $F("divAssist").style.display = "";
//                    $F("divCategory").style.display = "none";
//                  
//                    var temp = tvAssist.get_nodes().getNode(0);
//                    if(tvAssist.get_selectedNode())
//                    {
//                        temp = tvAssist.get_selectedNode();
//                    }
//                    temp.select();
//                }
//                else
//                {
//                    $F("divAssist").style.display = "none";
//                    $F("divCategory").style.display = "";
//                    
//                    var temp = tvTrainType.get_nodes().getNode(0);
//                    if(tvTrainType.get_selectedNode())
//                    {
//                        temp = tvTrainType.get_selectedNode();
//                    }
//                    temp.select();
//                }
//            }
//        }

		function AddRecord()
		{
	    	var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            }  
            
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;    
		
//		    if($F("divCategory").style.display == "none")
//		    {
		     	var node = tvAssist.get_selectedNode();
				
				
				if(node)
				{
				　	if(node.get_nodes().get_length() > 0)
				　	{
				　	    alert("请选择辅导教材体系树的叶子节点！");
				　	   return; 
				　	}
	                var ret = window.open("AssistBookDetail.aspx?Mode=Add&knowledgeId="+node.get_id(),'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
			        ret.focus();
			    }
			    else
			    {
			        //var ret = window.open("BookDetail.aspx?Mode=Add&knowledgeId="+1,'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
			        //ret.focus();
			        alert("请选择一个辅导教材体系！");
			        return;
			    }
//		    }
//		    else
//		    {
//	      	    var node = tvTrainType.get_selectedNode();
//				if(node)
//				{
//	                var ret = window.open("AssistBookDetail.aspx?TrainTypeId="+node.get_id(),'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//			        ret.focus();
//			    }
//		    }
		}
		
		function  QueryRecord()
        {
            if(window.frames["ifBookInfo"].document.getElementById("query").style.display == "none")
                window.frames["ifBookInfo"].document.getElementById("query").style.display = "";
            else
                window.frames["ifBookInfo"].document.getElementById("query").style.display = "none";
        }
       
       function QueryUpdate()
        {
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5;    
           var ret = window.open("AssistBookUpdateInfo.aspx",'BookImportNew','Width=800px,Height=600px,status=false,resizable=yes,left='+cleft+',top='+ctop+',scrollbars=yes',true);
	       ret.focus();
        } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        知识管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        辅导教材管理</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img id="Button1"  onclick="AddRecord();" src="../Common/Image/add.gif" alt="" />
                    <img  id="btnQuery" onclick="QueryRecord();" src="../Common/Image/find.gif" alt="" />
                    <img  id="btnQueryUpdate" onclick="QueryUpdate();" src="../Common/Image/bookupdate.gif" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">辅导体系
<%--                        <input id="rbnAssist" name="KnowledgeOrTrainType" onclick="rbnClicked(this);"type="radio" checked="checked" />辅导体系
                        <input id="rbnTrainType" name="KnowledgeOrTrainType" onclick="rbnClicked(this);" type="radio" />培训类别--%>
                        </div>
                    <div id="leftContent">
                        <div id="divAssist">
                            <ComponentArt:TreeView ID="tvAssist" runat="server" DefaultTarget="ifBookInfo">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvAssist_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
<%--                        <div id="divCategory" style="display: none;">
                            <ComponentArt:TreeView ID="tvTrainType" runat="server" DefaultTarget="ifBookInfo">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvTrainType_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>--%>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContentWithNoHead">
                        <iframe id="ifBookInfo" src="AssistBookInfo.aspx?id=0" frameborder="0" scrolling="auto"
                            class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <input type="hidden" name="Refresh" />
           <asp:HiddenField ID="HfUpdateRight" runat="server" /> 
    </form>
</body>
</html>
