<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StudyBookTree.aspx.cs"
    Inherits="RailExamWebApp.Online.Study.StudyBookTree" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>学习教材</title>

    <script type="text/javascript">     
         function $F(objId)
        {
            return document.getElementById(objId);
        }
        
        function TreeView1_onNodeSelect(sender, eventArgs)
        {
            var search = window.location.search;
            var str= search.substring(search.indexOf("?")+1); 
            var node = eventArgs.get_node();
            if(node)
            {            
                window.frames["iframe1"].location = "StudyBook.aspx?KnowledgeID="+ node.get_id()+"&"+str;
            }
        }       
       
        function TreeView2_onNodeSelect(sender, eventArgs)
        {
              var search = window.location.search;
             var str= search.substring(search.indexOf("?")+1);           
              var node = eventArgs.get_node();
            if(node)
            {            
                window.frames["iframe1"].location = "StudyBook.aspx?TrainTypeID="+ node.get_id()+"&"+str;
            }
        }   
        
        
        function rbnClicked(btn)
        {
            var search = window.location.search;
             var str= search.substring(search.indexOf("?")+1);           
 
            if(btn.checked)
            {
                if(btn.id == "rbnKnowledge")
                {
                    $F("divKnowledge").style.display = ""; 
                    $F("divCategory").style.display = "none"; 
                  
                    if(TreeView1.get_selectedNode())
                    {
                        temp = TreeView1.get_selectedNode();
                        window.frames["iframe1"].location = "StudyBook.aspx?KnowledgeID="+ temp.get_id()+"&"+str;
                    }
                    else
                      {
                                if(TreeView1 && TreeView1.get_nodes().get_length() > 0)
                                {
                                    TreeView1.get_nodes().getNode(0).select();
                                }
                      } 
                }
                else
                {
                    $F("divKnowledge").style.display = "none"; 
                    $F("divCategory").style.display = ""; 
                   
                   if(TreeView2.get_selectedNode())
                    {
                        temp = TreeView2.get_selectedNode();
                        window.frames["iframe1"].location = "StudyBook.aspx?TrainTypeID="+ temp.get_id()+"&"+str;
                   }
                    else
                  {
                            if(TreeView2 && TreeView2.get_nodes().get_length() > 0)
                            {
                                TreeView2.get_nodes().getNode(0).select();
                            }                  
                   } 
                }
            }
        } 
        
        //开始记录学习时间
        var i = 0;
        var employeeName = window.location.search.substring(window.location.search.indexOf("?")+1).split("=")[window.location.search.substring(window.location.search.indexOf("?")+1).split("=").length - 1];
        function starttime()
        {
           if(document.getElementById("hfIswuhan").value == "1")
           {
              return;
           }

           i++;
           
           var diff = "姓名：" + employeeName + " 学习时间："    
           var hours = Math.floor(i%(60*60*24)/3600);
           var minutes = Math.floor((i%(60*60*24)-3600*hours)/60);
           var second1 = i-3600*hours-60*minutes;
           diff += (hours>=10?"":"0") + hours + "小时";
           diff += (minutes>=10?"":"0") + minutes + "分";
           diff += (second1>=10?"":"0") + second1 + "秒";    
           document.getElementById("lblTime").innerHTML= diff ;  
           window.setTimeout("starttime()",1000);
        }
        
        function logout()
        {
           //如果是武汉则不执行下列操作
           if(document.getElementById("hfIswuhan").value == "1")
           {
              return;
           }
           
           top.returnValue =new Date().toLocaleString();
//           alert(top.returnValue);
        }
		
    </script>

</head>
<body onload="starttime()" onbeforeunload ="logout()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="parent">
                        在线学习</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        选择学习</div>
                </div>
                <div id="chapterNamePath">
                    <asp:Label ID="lblTime" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        <div id="divSwitchBtns" style="text-align: center; white-space: nowrap;">
                            分类:
                            <input id="rbnKnowledge" name="knowledgeOrCategory" onclick="rbnClicked(this);" type="radio"
                                checked="checked" />按知识体系
                            <input id="rbnCategory" name="knowledgeOrCategory" onclick="rbnClicked(this);" type="radio" />按培训类别
                        </div>
                    </div>
                    <div style="width: 100%; height: expression(0.90*screen.availHeight - 50 + 'px');">
                        <div id="divKnowledge">
                            <ComponentArt:TreeView ID="TreeView1" runat="server" EnableViewState="false">
                                <ClientEvents>
                                    <NodeSelect EventHandler="TreeView1_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                        <div id="divCategory" style="display: none;">
                            <ComponentArt:TreeView ID="TreeView2" runat="server" EnableViewState="false">
                                <ClientEvents>
                                    <NodeSelect EventHandler="TreeView2_onNodeSelect" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                        </div>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        教材和课件信息</div>
                    <div id="rightContentWithNoHeight">
                        <iframe id="iframe1" frameborder="0" scrolling="auto" width="100%"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <input id="Test1" type="hidden" name="Test1" />
        <asp:HiddenField ID="hfIswuhan" Value=""  runat="server"/>
         
         <%--<componentart:callback id="Callback1" runat="server" oncallback="Callback1_Callback" >
        </componentart:callback>--%>
    </form>

    <script type="text/javascript">
        if(TreeView1 && TreeView1.get_nodes().get_length() > 0)
        {
            TreeView1.get_nodes().getNode(0).select();
        }
      document.getElementById("rightContentWithNoHeight").height = 0.9*screen.availHeight- 45;
      document.getElementById("iframe1").height = 0.9*screen.availHeight- 45; 
    </script>

</body>
</html>
