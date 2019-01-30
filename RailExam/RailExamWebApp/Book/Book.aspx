
<%@ Page Language="C#" AutoEventWireup="True" Codebehind="Book.aspx.cs" Inherits="RailExamWebApp.Book.Book" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>教材管理</title>

    <script type="text/javascript">     
        function $F(objId)
        {
            return document.getElementById(objId);
        }
        
        function tvKnowledge_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {
            	var postId = document.getElementById("hfPostID").value;
            	if(postId=="") {
                    window.frames["ifBookInfo"].location = "BookInfo.aspx?id="+ node.get_id();
            	}
            	else {
                    window.frames["ifBookInfo"].location = "BookInfo.aspx?id="+ node.get_id()+"&postID="+postId;
            	}
                node.expand(); 
            }
        }
       
        function tvTrainType_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {
                window.frames["ifBookInfo"].location = "BookInfo.aspx?id1="+ node.get_id();
               node.expand(); 
            }
        }   
        
        function rbnClicked(btn)
        {
            if(btn.checked)
            {
                if(btn.id == "rbnKnowledge")
                {
                    $F("divKnowledge").style.display = "";
                    $F("divCategory").style.display = "none";
                  
                    if(tvKnowledge.get_selectedNode())
                    {
                        temp = tvKnowledge.get_selectedNode();
                        window.frames["ifBookInfo"].location = "BookInfo.aspx?id="+ temp.get_id();
                    }
                    else
                    {
                        window.frames["ifBookInfo"].location = "BookInfo.aspx?id=0";
                    }
                }
                else
                {
                    $F("divKnowledge").style.display = "none";
                    $F("divCategory").style.display = "";
                    
                    if(tvTrainType.get_selectedNode())
                    {
                        temp = tvTrainType.get_selectedNode();
                        window.frames["ifBookInfo"].location = "BookInfo.aspx?id1="+ temp.get_id();
                   }
                    else
                    {
                         window.frames["ifBookInfo"].location = "BookInfo.aspx?id1=0";
                    }
                }
            }
        }

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
		
		    if($F("divCategory").style.display == "none")
		    {
		     	var node = tvKnowledge.get_selectedNode();
				
				
				if(node)
				{
				　	if(node.get_nodes().get_length() > 0)
				　	{
				　	    alert("请选择知识体系树的叶子节点！");
				　	   return; 
				　	}
	                var ret = window.open("BookDetail.aspx?Mode=Add&knowledgeId="+node.get_id(),'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
			        ret.focus();
			    }
			    else
			    {
			        //var ret = window.open("BookDetail.aspx?Mode=Add&knowledgeId="+1,'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
			        //ret.focus();
			        alert("请选择一个知识体系！");
			        return;
			    }
		    }
		    else
		    {
	      	    var node = tvTrainType.get_selectedNode();
				if(node)
				{
					if(node.get_nodes().get_length() > 0)
				　	{
				　	    alert("请选择培训类别树的叶子节点！");
				　	   return; 
				　	}
	                var ret = window.open("BookDetail.aspx?TrainTypeId="+node.get_id(),'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
			        ret.focus();
			    }
			     else
                {
                    alert("请选择一个培训类别！");
                    return;
                } 
		    }
		}
		
		function  QueryRecord()
        {
            if(window.frames["ifBookInfo"].document.getElementById("query").style.display == "none")
                window.frames["ifBookInfo"].document.getElementById("query").style.display = "";
            else
                window.frames["ifBookInfo"].document.getElementById("query").style.display = "none";
        }
        
        function InputBook()
        {
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-850)*.5;   
          ctop=(screen.availHeight-600)*.5;    
          
           
           var flagupdate=document.getElementById("HfUpdateRight").value;
        	                 if(flagupdate=="False")
                      {
                        alert("您没有权限使用该操作！");                       
                        return;
                      }
                      
            var ret = window.open("BookImportNew.aspx",'BookImportNew','Width=850px,Height=600px,status=false,resizable=yes,left='+cleft+',top='+ctop+',scrollbars=yes',true);
	        ret.focus();
        }
       
       function QueryUpdate()
        {
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5;    
           var ret = window.open("BookUpdateInfo.aspx",'BookImportNew','Width=800px,Height=600px,status=false,resizable=yes,left='+cleft+',top='+ctop+',scrollbars=yes',true);
	       ret.focus();
        } 
       
      function updateEmployee() 
      {
      	  var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-400)*.5;   
          ctop=(screen.availHeight-300)*.5;    
           var ret = window.open("UpdateEmployee.aspx",'UpdateEmployee','Width=400px,Height=300px,status=false,resizable=yes,left='+cleft+',top='+ctop+',scrollbars=yes',true);
	       ret.focus();
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
                        教材管理</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
<%--                    <img id="Button1" onclick="AddRecord();" src="../Common/Image/add.gif" alt="" />
                    <img id="btnQuery" onclick="QueryRecord();" src="../Common/Image/find.gif" alt="" />
                    <img id="btnQueryUpdate" onclick="QueryUpdate();" src="../Common/Image/bookupdate.gif"
                        alt="" />--%>
                    <input type="button" onclick="AddRecord()" class="button" value="新  增" />
                   <input type="button" onclick="QueryRecord()" class="button" value="查   询" /> 
                    <input type="button" onclick="QueryUpdate()" class="buttonLong" value="教材更新记录" />
                    <input type="button" onclick="updateEmployee()" class="buttonLong" value="更新负责人" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        <input id="rbnKnowledge" name="KnowledgeOrTrainType" onclick="rbnClicked(this);"
                            type="radio" checked="checked" style="display: none" />知识体系
                        <input id="rbnTrainType" name="KnowledgeOrTrainType" onclick="rbnClicked(this);"
                            type="radio" style="display: none" /></div>
                    <div id="leftContent">
                        <div id="divKnowledge">
                            <ComponentArt:TreeView ID="tvKnowledge" runat="server" DefaultTarget="ifBookInfo">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvKnowledge_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                        <div id="divCategory" style="display: none;">
                            <ComponentArt:TreeView ID="tvTrainType" runat="server" DefaultTarget="ifBookInfo">
                                <ClientEvents>
                                    <NodeSelect EventHandler="tvTrainType_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContentWithNoHead">
                        <iframe id="ifBookInfo" src="BookInfo.aspx?id=0" frameborder="0" scrolling="auto"
                            class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <input type="hidden" name="Refresh" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
         <asp:HiddenField ID="hfPostID" runat="server" />
    </form>
</body>
</html>
