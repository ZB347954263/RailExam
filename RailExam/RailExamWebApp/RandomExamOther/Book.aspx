<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Book.aspx.cs" Inherits="RailExamWebApp.RandomExamOther.Book" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>教材管理</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

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
                window.frames["ifBookInfo"].location = "BookInfo.aspx?id="+ node.get_id();
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
        
        function tvPost_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {
                window.frames["ifBookInfo"].location = "BookInfo.aspx?id2="+ node.get_id();
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
                    $F("divKnowledge").style.display = "";
                    $F("divCategory").style.display = "none";
                    $F("divPost").style.display = "none";
                  
                    if(tvKnowledge.get_selectedNode())
                    {
                        temp = tvKnowledge.get_selectedNode();
                        window.frames["ifBookInfo"].location = "BookInfo.aspx?id="+ temp.get_id();
                    }
                    else
                    {
                        window.frames["ifBookInfo"].location = "BookInfo.aspx?id=0";
                    }
                    break;
                }
                case 1:
                {
                    $F("divKnowledge").style.display = "none";
                    $F("divCategory").style.display = "";
                     $F("divPost").style.display = "none";
                    
                    if(tvTrainType.get_selectedNode())
                    {
                        temp = tvTrainType.get_selectedNode();
                        window.frames["ifBookInfo"].location = "BookInfo.aspx?id1="+ temp.get_id();
                   }
                    else
                    {
                         window.frames["ifBookInfo"].location = "BookInfo.aspx?id1=0";
                    }
                    break;
                }
                case 2:
                {
                    $F("divKnowledge").style.display = "none";
                    $F("divCategory").style.display = "none";
                    $F("divPost").style.display = "";
                    
                    if(tvPost.get_selectedNode())
                    {
                        temp = tvPost.get_selectedNode();
                        window.frames["ifBookInfo"].location = "BookInfo.aspx?id2="+ temp.get_id();
                   }
                    else
                    {
                         window.frames["ifBookInfo"].location = "BookInfo.aspx?id2=0";
                    }
                    break;
                }
                default:
                {
                    break;
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
		
		    if($F("divKnowledge").style.display == "")
		    {
		     	var node = tvKnowledge.get_selectedNode();
				
				
				if(node)
				{
				　	if(node.get_nodes().get_length() > 0)
				　	{
				　	    alert("请选择知识体系树的叶子节点！");
				　	   return; 
				　	}
//	                var ret = window.open("/RailExamBao/Book/BookDetail.aspx?Mode=Add&knowledgeId="+node.get_id(),'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//			        ret.focus();
                    var ret = showCommonDialog("/RailExamBao/Book/BookDetail.aspx?Mode=Add&knowledgeId="+node.get_id(),'dialogWidth:800px;dialogHeight:650px');
                    if(ret == "true")
                    {
                        window.frames["ifBookInfo"].form1.Refresh.value = ret;
                        window.frames["ifBookInfo"].form1.submit();
                        window.frames["ifBookInfo"].form1.Refresh.value = "";
                    }
			    }
			    else
			    {
			        alert("请选择一个知识体系！");
			        return;
			    }
		    }
		    else if($F("divCategory").style.display == "")
		    {
	      	    var node = tvTrainType.get_selectedNode();
				if(node)
				{
				    if(node.get_nodes().get_length() > 0)
				　	{
				　	    alert("请选择培训类别树的叶子节点！");
				　	   return; 
				　	}
//	                var ret = window.open("/RailExamBao/Book/BookDetail.aspx?Mode=Add&TrainTypeId="+node.get_id(),'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//			        ret.focus();
                    var ret = showCommonDialog("/RailExamBao/Book/BookDetail.aspx?Mode=Add&TrainTypeId="+node.get_id(),'dialogWidth:800px;dialogHeight:650px');
                    if(ret == "true")
                    {
                        window.frames["ifBookInfo"].form1.Refresh.value = ret;
                        window.frames["ifBookInfo"].form1.submit();
                        window.frames["ifBookInfo"].form1.Refresh.value = "";
                    }
			    }
			    else
			    {
			        alert("请选择一个培训类别！");
			        return;
			    } 
		    }
		    else 
		    {
		        var node = tvPost.get_selectedNode();
				if(node)
				{
				    if(node.get_nodes().get_length() > 0)
				　	{
				　	    alert("请选择工作岗位树的叶子节点！");
				　	   return; 
				　	}
//	                var ret = window.open("/RailExamBao/Book/BookDetail.aspx?Mode=Add&PostId="+node.get_id(),'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//			        ret.focus();
                    var ret = showCommonDialog("/RailExamBao/Book/BookDetail.aspx?Mode=Add&PostId="+node.get_id(),'dialogWidth:800px;dialogHeight:650px');
                    if(ret == "true")
                    {
                        window.frames["ifBookInfo"].form1.Refresh.value = ret;
                        window.frames["ifBookInfo"].form1.submit();
                        window.frames["ifBookInfo"].form1.Refresh.value = "";
                    }
			    }
			    else
			    {
			        alert("请选择一个工作岗位！");
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
                      
            var ret = window.open("/RailExamBao/Book/BookImportNew.aspx",'BookImportNew','Width=850px,Height=600px,status=false,resizable=yes,left='+cleft+',top='+ctop+',scrollbars=yes',true);
	        ret.focus();
        }
       
       function QueryUpdate()
        {
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5;    
//         var ret = window.open("/RailExamBao/Book/BookUpdateInfo.aspx",'BookImportNew','Width=800px,Height=600px,status=false,resizable=yes,left='+cleft+',top='+ctop+',scrollbars=yes',true);
//	       ret.focus();
           var ret = showCommonDialog("/RailExamBao/Book/BookUpdateInfo.aspx",'dialogWidth:800px;dialogHeight:650px');

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
                    <img id="Button1" onclick="AddRecord();" src="../Common/Image/add.gif" alt="" />
                    <img id="btnQuery" onclick="QueryRecord();" src="../Common/Image/find.gif" alt="" />
                    <img id="btnQueryUpdate" onclick="QueryUpdate();" src="../Common/Image/bookupdate.gif"
                        alt="" />
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
                        <iframe id="ifBookInfo" src="BookInfo.aspx?id=0" frameborder="0" scrolling="auto"
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
