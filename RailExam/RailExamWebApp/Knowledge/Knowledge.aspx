<%@ Page Language="C#" AutoEventWireup="True" Codebehind="Knowledge.aspx.cs" Inherits="RailExamWebApp.Knowledge.Knowledge" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>知识体系</title>

    <script type="text/javascript">
        function tvKnowledge_onContextMenu(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem").value;
            var menuItemNewBrother = tvKnowledgeMenu.findItemByProperty("Text", "新增同级");
             var menuItemNewChild = tvKnowledgeMenu.findItemByProperty("Text", "新增下级");
            var menuItemEdit = tvKnowledgeMenu.findItemByProperty("Text", "编辑");
            var menuItemSave = tvKnowledgeMenu.findItemByProperty("Text", "保存");
            var menuItemCancel = tvKnowledgeMenu.findItemByProperty("Text", "取消");
            var menuItemDelete = tvKnowledgeMenu.findItemByProperty("Text", "删除");                   
            var menuItemUp = tvKnowledgeMenu.findItemByProperty("Text", "上移");
            var menuItemDown = tvKnowledgeMenu.findItemByProperty("Text", "下移");
            
            
                      
            if(theItem == "新增同级" || theItem == "编辑"  || theItem=="新增下级")
            {
                menuItemNewBrother.set_enabled(false);
               menuItemNewChild.set_enabled(false); 
                menuItemEdit.set_enabled(false);
                menuItemSave.set_enabled(true);
                menuItemCancel.set_enabled(true);
                menuItemDelete.set_enabled(false); 
               menuItemUp.set_enabled(false);
                menuItemDown.set_enabled(false);
            }
            else
            {
                menuItemNewBrother.set_enabled(true);
                menuItemNewChild.set_enabled(true); 
                menuItemEdit.set_enabled(true);
                menuItemSave.set_enabled(false);
                menuItemCancel.set_enabled(false);
                menuItemDelete.set_enabled(true); 
               menuItemUp.set_enabled(true);
                menuItemDown.set_enabled(true);
           }
                      
            
              var flagUpdate=document.getElementById("HfUpdateRight").value;              
        	        if(flagUpdate=="False")
                      {
                        menuItemNewBrother.set_enabled(false);
                       menuItemNewChild.set_enabled(false); 
                        menuItemEdit.set_enabled(false); 
                         menuItemUp.set_enabled(false); 
                        menuItemDown.set_enabled(false); 
                      }               
                      
                var flagDelete=document.getElementById("HfDeleteRight").value;
        	                 if(flagDelete=="False")
                      {                       
                        menuItemDelete.set_enabled(false); 
                      }                           
                      
                      
            if(! tvKnowledge.get_selectedNode()
              || tvKnowledge.get_selectedNode().get_id() != eventArgs.get_node().get_id())
            {
                eventArgs.get_node().select();
            }

            switch(eventArgs.get_node().get_text())
            {
                default:
                    if(tvKnowledge.get_selectedNode().get_id() == eventArgs.get_node().get_id())
                    {
                        tvKnowledgeMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_node());
                    }
                    
                    break;
            }                      
            
                                            
            return true;
        }
        
        function tvKnowledgeMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();
            var contextDataNode = menuItem.get_parentMenu().get_contextData();
            var theItem = document.getElementById("hfSelectedMenuItem");
            var theForm = window.frames["ifKnowledgeDetail"].document.forms[0];
                              
            switch(menuItem.get_text())
            {
                case '展开':
                    contextDataNode.expand();
                    
                    break;
                    
                case '折叠':
                    contextDataNode.collapse();
                    
                    break;
                    
                case '展开全部':
                    tvKnowledge.expandAll();
                    
                    break;
                    
                case '折叠全部':
                    tvKnowledge.collapseAll();
                    
                    break;
                    
                case '查看':
                    if(tvKnowledge.get_selectedNode() == contextDataNode)
                    {
                        return false;
                    }
                    contextDataNode.select();
                    
                    break;
            
                case '新增下级':                      
                    var theBtn = document.getElementById(btnIds[0]);
                    imgBtns_onClick(theBtn);
                    
                    break;
                    
                case '新增同级':                      
                    var theBtn = document.getElementById(btnIds[7]);
                    imgBtns_onClick(theBtn);
                    
                    break;
            
                case '编辑':
                  
                    var theBtn = document.getElementById(btnIds[1]);
                    imgBtns_onClick(theBtn);
                    
                    break;
                    
                case '保存':
                    var theBtn;
                    if(theItem && theItem.value == "编辑")
                    {
                        theBtn = document.getElementById(btnIds[3]);
                    }
                    
                    if(theItem  && (theItem.value == "新增同级" || theItem.value == "新增下级") )
                    {
                        theBtn = document.getElementById(btnIds[5]);
                    }
                    
                    if(theForm && theBtn && theItem)
                    {
                        imgBtns_onClick(theBtn);
                    }

                    break;
                    
                case '取消':
                    var theBtn;
                    if(theItem && theItem.value == "编辑")
                    {
                        theBtn = document.getElementById(btnIds[4]);
                    }
                    if(theItem && (theItem.value == "新增同级" || theItem.value == "新增下级") )
                    {
                        theBtn = document.getElementById(btnIds[6]);
                    }
                    if(theForm && theBtn)
                    {
                        imgBtns_onClick(theBtn);
                    }  

                    break;
                    
                case '上移':
                    theItem.value = "上移";
                    if(tvKnowledgeNodeMove(contextDataNode.get_value(), "UP") == "False")
                    {
                        alert("不能上移该节点！");
                    }
                    else
                    {
                        tvKnowledgeChangeCallBack.callback("UP",contextDataNode.get_value());
                    }
                    
                    break;
                    
                case '下移':
                    theItem.value = "下移";
                    if(tvKnowledgeNodeMove(contextDataNode.get_value(), "DOWN") == "False")
                    {
                        alert("不能下移该节点！");
                    }
                    else
                    {
                        tvKnowledgeChangeCallBack.callback("DOWN",contextDataNode.get_value());
                    }
                    
                    break;
                
                case '删除':
                    var  node =tvKnowledge.get_selectedNode(); 
                    var theBtn = document.getElementById(btnIds[2]);
                    if(theForm && theBtn)
                    {                                                
                        imgBtns_onClick(theBtn);
                    }
                    
                    break;
            }
            
            return true;
        }
        
//        function tvKnowledge_onLoad(sender, eventArgs)
//        {
//            tvTrainType.expandAll();
//        }

//        function tvTrainType_onNodeRename(sender, eventArgs)
//        {
//            // 给节点重命名
//            tvTrainTypeRenameCallBack.callback('1', 'Rename', 'NewName', 'Result');
//        }
        
        function tvKnowledge_onNodeSelect(sender, eventArgs)
        {
            // 查看节点
            var node = eventArgs.get_node();
            var theFrame = window.frames["ifKnowledgeDetail"];
            var theItem = document.getElementById("hfSelectedMenuItem");
            if(theItem  && (theItem.value == "新增同级" || theItem.value == "新增下级") )
            {
                if(confirm("要保存数据吗？"))
                {
                    imgBtns_onClick(document.getElementById(btnIds[5]));
                    return;
                }
                else
                {
                    imgBtns_onClick(document.getElementById(btnIds[6]));
                }
            }
            else if(theItem && theItem.value == "编辑")
            {
                if(confirm("要保存数据吗？"))
                {
                    imgBtns_onClick(document.getElementById(btnIds[3]));
                    return;
                }
                else
                {
                    imgBtns_onClick(document.getElementById(btnIds[4]));
                }
             }
            
            theItem.value = "";
            theFrame.location = "KnowledgeDetail.aspx?id=" + node.get_id();
            
            node.expand();
        }
        
        function tvKnowledgeChangeCallBack_onCallbackComplete(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem");
            if(theItem && theItem.value != "")
            {
                theItem.value == "";
            }
        
            var id = document.getElementById("hfMaxID").value;
            if(id == "0")
            {
                 tvKnowledge.selectNodeById(sender.get_parameter());
            }
            else
            {
                 //tvKnowledge.expandAll();
                 tvKnowledge.selectNodeById(id);
                 tvKnowledge.findNodeById(id).select();
                document.getElementById("hfMaxID").value = "0"; 
            }
            
            if(!tvKnowledge.get_selectedNode())
            {
                tvKnowledge.get_nodes().getNode(0).select();
            } 
//            else
//            {
//                var node = tvKnowledge.get_selectedNode();
//                node.expand(); 
//                while(node.get_parentNode())
//                {
//                    node = node.get_parentNode();
//                    node.expand();
//                } 
//               tvKnowledge.render(); 
//            }
        }
         
        function tvKnowledgeChangeCallBack_onLoad(sender, eventArgs)
        {
            // 给树重排序
            if(tvKnowledge.get_nodes().get_length() > 0)
            {
//                tvKnowledge.expandAll();
            }
        }
        
        var btnIds = new Array("fvKnowledge_NewButton", "fvKnowledge_EditButton", "fvKnowledge_DeleteButton",
            "fvKnowledge_UpdateButton", "fvKnowledge_UpdateCancelButton",
            "fvKnowledge_InsertButton", "fvKnowledge_InsertCancelButton","fvKnowledge_NewButtonBrother");
            
        function imgBtns_onClick(btn)
        {
           var flagupdate=document.getElementById("HfUpdateRight").value;
           if(flagupdate=="False"&&(btn.action == "edit"|| btn.action == "newChild" || btn.action == "newBrother"))
            {
                alert("您没有权限使用该操作！");                       
                return;
            }
              
             var flagDelete=document.getElementById("HfDeleteRight").value;
             if(flagDelete=="False"&&btn.action == "delete")
              {
                alert("您没有权限使用该操作！");                       
                return;
              }
              
             if(btn.action == "delete")
             {	                
                  var node = tvKnowledge.get_selectedNode();       
                  if(node.get_nodes().get_length() > 0)
                     {
                        alert("节点有子节点，不能删除！");
                        return ;
                     }
              }        	 
         	var frame = window.frames["ifKnowledgeDetail"];
         	if (frame)
         	{
         	   if(btn.action == "newBrother")
         	   {
         	         btn1 =document.getElementById(btnIds[0]);
         	         theBtn = frame.document.getElementById(btn1.id);
         	   }
         	   else
         	   {
         	         theBtn = frame.document.getElementById(btn.id);
         	   }
         	    if(theBtn.onclick())
         	    {
     	              if(btn.action == "newBrother")
     	              {
          	                var node = tvKnowledge.get_selectedNode();        
          	                if(node)
          	                 {
     	                            if(node.get_parentNode())
     	                            {
     	                                frame.document.getElementById("hfInsert").value = node.get_parentNode().get_id();
     	                            } 
     	                            else
     	                            {
     	                               frame.document.getElementById("hfInsert").value = "0";
     	                            }
          	                  } 
          	                  else
          	                  {
     	                           frame.document.getElementById("hfInsert").value = "0";
          	                  }
     	              } 
     	             else if(btn.action == "newChild")
     	             {
     	                    frame.document.getElementById("hfInsert").value = "-1";
     	             } 
         	       frame.location = theBtn.href;
            	   setImageBtnVisiblity(btn);
        	    }
         	}
        }
        
        function setImageBtnVisiblity(btn)
        {             
        	switch(btn.type)
        	{
        	    case "0":
        	        {
        	            if (btn.action == "newChild")
        	            {
        	            
        	                document.getElementById(btnIds[0]).style.display = "none";
        	                document.getElementById(btnIds[1]).style.display = "none";
        	                document.getElementById(btnIds[2]).style.display = "none";
        	                document.getElementById(btnIds[3]).style.display = "none";
        	                document.getElementById(btnIds[4]).style.display = "none";
        	                document.getElementById(btnIds[5]).style.display = "";
        	                document.getElementById(btnIds[6]).style.display = "";
        	                document.getElementById(btnIds[7]).style.display = "none"; 
        	            } 
        	           else if (btn.action == "newBrother")
        	            {
        	            
        	                document.getElementById(btnIds[0]).style.display = "none";
        	                document.getElementById(btnIds[1]).style.display = "none";
        	                document.getElementById(btnIds[2]).style.display = "none";
        	                document.getElementById(btnIds[3]).style.display = "none";
        	                document.getElementById(btnIds[4]).style.display = "none";
        	                document.getElementById(btnIds[5]).style.display = "";
        	                document.getElementById(btnIds[6]).style.display = "";
        	                document.getElementById(btnIds[7]).style.display = "none"; 
        	            } 
        	            else if(btn.action == "edit")
        	            {
        	                document.getElementById(btnIds[0]).style.display = "none";
        	                document.getElementById(btnIds[1]).style.display = "none";
        	                document.getElementById(btnIds[2]).style.display = "none";
        	                document.getElementById(btnIds[3]).style.display = "";
        	                document.getElementById(btnIds[4]).style.display = "";
        	                document.getElementById(btnIds[5]).style.display = "none";
        	                document.getElementById(btnIds[6]).style.display = "none";
        	               document.getElementById(btnIds[7]).style.display = "none";  
        	            }
        	            else if(btn.action == "delete")
        	            {
        	            }
            	        
            	        
        	            break;
        	        }
        	    
        	        case "1":
        	        case "2":
        	        {
    	                document.getElementById(btnIds[0]).style.display = "";
    	                document.getElementById(btnIds[1]).style.display = "";
    	                document.getElementById(btnIds[2]).style.display = "";
    	                document.getElementById(btnIds[3]).style.display = "none";
    	                document.getElementById(btnIds[4]).style.display = "none";
    	                document.getElementById(btnIds[5]).style.display = "none";
    	                document.getElementById(btnIds[6]).style.display = "none";
    	                document.getElementById(btnIds[7]).style.display = ""; 

        	            break;
        	        }
        	    
        	    default:
        	        {
        	            alert("状态错误！");
            	        
        	            return;
        	        }
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
                        知识体系</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                   <img id="fvKnowledge_NewButtonBrother" onclick="imgBtns_onClick(this);"  src="../Common/Image/addBrother.gif"
                        type="0" action="newBrother" alt="" />
                    <img id="fvKnowledge_NewButton" onclick="imgBtns_onClick(this);" src="../Common/Image/addChild.gif"
                        type="0" action="newChild" alt="" />
                     <img id="fvKnowledge_EditButton" onclick="imgBtns_onClick(this);" src="../Common/Image/edit.gif"
                        type="0" action="edit" alt="" />
                    <img id="fvKnowledge_DeleteButton" onclick="imgBtns_onClick(this);" src="../Common/Image/delete.gif"
                        type="0" action="delete" alt="" />
                    <img id="fvKnowledge_UpdateButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="1" action="update" style="display: none;" alt="" />
                    <img id="fvKnowledge_UpdateCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="1" action="cancel" style="display: none;" alt="" />
                    <img id="fvKnowledge_InsertButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="2" action="insert" style="display: none;" alt="" />
                    <img id="fvKnowledge_InsertCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="2" action="cancel" style="display: none;" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        知识体系</div>
                    <div id="leftContent">
                        <ComponentArt:CallBack ID="tvKnowledgeChangeCallBack" runat="server" OnCallback="tvKnowledgeChangeCallBack_Callback">
                            <Content>
                                <ComponentArt:TreeView ID="tvKnowledge" runat="server" EnableViewState="true">
                                    <ClientEvents>
                                        <NodeSelect EventHandler="tvKnowledge_onNodeSelect" />
                                        <ContextMenu EventHandler="tvKnowledge_onContextMenu" />

                                    </ClientEvents>
                                </ComponentArt:TreeView>
                            </Content>
                            <LoadingPanelClientTemplate>
                                加载数据...
                            </LoadingPanelClientTemplate>
                            <ClientEvents>
                                <CallbackComplete EventHandler="tvKnowledgeChangeCallBack_onCallbackComplete" />
                                <Load EventHandler="tvKnowledgeChangeCallBack_onLoad" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        知识体系详细信息</div>
                    <div id="rightContent">
                        <iframe id="ifKnowledgeDetail" frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <ComponentArt:Menu ID="tvKnowledgeMenu" runat="server" EnableViewState="false">
            <ClientEvents>
                <ItemSelect EventHandler="tvKnowledgeMenu_onItemSelect" />
            </ClientEvents>
            <Items>
                <ComponentArt:MenuItem Text="新增同级" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" /> 
             　<ComponentArt:MenuItem Text="新增下级" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" />
                <ComponentArt:MenuItem Text="编辑" look-lefticonurl="edit.gif" disabledlook-lefticonurl="edit_disabled.gif" />
                <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" />
                <ComponentArt:MenuItem Text="删除" look-lefticonurl="delete.gif" disabledlook-lefticonurl="delete_disabled.gif" />
                <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" />
                <ComponentArt:MenuItem Text="保存" look-lefticonurl="save.gif" disabledlook-lefticonurl="save_disabled.gif" />
                <ComponentArt:MenuItem Text="取消" look-lefticonurl="cancel.gif" disabledlook-lefticonurl="cancel_disabled.gif" />
                <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" />
                <ComponentArt:MenuItem Text="上移" look-lefticonurl="move_up.gif" disabledlook-lefticonurl="move_up_disabled.gif" />
                <ComponentArt:MenuItem Text="下移" look-lefticonurl="move_down.gif" disabledlook-lefticonurl="move_down_disabled.gif" />
                <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" disabledlook-lefticonurl="new_disabled.gif" />
                <ComponentArt:MenuItem Text="展开" look-lefticonurl="expand.gif" disabledlook-lefticonurl="expand_disabled.gif" />
                <ComponentArt:MenuItem Text="折叠" look-lefticonurl="collapse.gif" disabledlook-lefticonurl="collapse_disabled.gif" />
                <ComponentArt:MenuItem Text="展开全部" look-lefticonurl="expand_all.gif" disabledlook-lefticonurl="expand_all_disabled.gif" />
                <ComponentArt:MenuItem Text="折叠全部" look-lefticonurl="collapse_all.gif" disabledlook-lefticonurl="collapse_all_disabled.gif" />
            </Items>
        </ComponentArt:Menu>
        <asp:HiddenField ID="hfSelectedMenuItem" runat="server" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
         <asp:HiddenField  ID="hfMaxID" runat="server" Value="0"/>
    </form>

    <script type="text/javascript">
        if(tvKnowledge && tvKnowledge.get_nodes().get_length() > 0)
        {
            tvKnowledge.get_nodes().getNode(0).select();
        }
    </script>

</body>
</html>
