<%@ Page Language="C#" AutoEventWireup="True" Codebehind="BookChapter.aspx.cs" Inherits="RailExamWebApp.Book.BookChapter" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>教材章节</title>

    <script type="text/javascript">
        function tvBookChapter_onContextMenu(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem").value;
            var menuItemNewBrother = tvBookChapterMenu.findItemByProperty("Text", "新增同级");
             var menuItemNewChild = tvBookChapterMenu.findItemByProperty("Text", "新增下级");
            var menuItemEdit = tvBookChapterMenu.findItemByProperty("Text", "编辑");            
            var menuItemSave = tvBookChapterMenu.findItemByProperty("Text", "保存");
            var menuItemCancel = tvBookChapterMenu.findItemByProperty("Text", "取消");
            var menuItemDelete = tvBookChapterMenu.findItemByProperty("Text", "删除");                   
            var menuItemUp = tvBookChapterMenu.findItemByProperty("Text", "上移");
            var menuItemDown = tvBookChapterMenu.findItemByProperty("Text", "下移");
            
            
                      
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
          
           var node = tvBookChapter.get_selectedNode();
          
          if(node && node.get_id()==0)
          {
                menuItemNewBrother.set_enabled(false);
                 menuItemEdit.set_enabled(false);
                menuItemDelete.set_enabled(false); 
               menuItemUp.set_enabled(false);
                menuItemDown.set_enabled(false);
         }  
        else
       {
       
       }  
            
            if(!tvBookChapter.get_selectedNode() || 
                tvBookChapter.get_selectedNode().get_id() != eventArgs.get_node().get_id())
            {
                eventArgs.get_node().select();
            }
        
            switch(eventArgs.get_node().get_text())
            {                
                default:
                if(tvBookChapter.get_selectedNode().get_id() == eventArgs.get_node().get_id()){
                    tvBookChapterMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_node()); 
                }
                break; 
            }      
            return true;
        }        
        function tvBookChapterMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item(); 
            var contextDataNode = menuItem.get_parentMenu().get_contextData(); 
            var theItem = document.getElementById("hfSelectedMenuItem");
            var theForm = window.frames["ifBookChapterDetail"].document.forms[0];
 
            switch(menuItem.get_text())
            {
                case '展开':
                    contextDataNode.expand();
                    break;
                    
                case '折叠':
                    contextDataNode.collapse();
                    break;
                    
                case '展开全部':
                    tvBookChapter.expandAll();
                    break;
                    
                case '折叠全部':
                    tvBookChapter.collapseAll();
                    break;
                    
                case '查看':
                    if(tvBookChapter.get_selectedNode() == contextDataNode)
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
                    tvBookChapterMoveCallBack.callback(contextDataNode.get_id(), "CanMoveUp");
                    break;
                    
                case '下移':
                    theItem.value = "下移";
                    tvBookChapterMoveCallBack.callback(contextDataNode.get_id(), "CanMoveDown");
                    break;
                
                case '删除':
                    var theBtn = window.frames["ifBookChapterDetail"].document.getElementById("fvBookChapter_DeleteButton");
                    if(theForm&&theBtn)
                    {
                        if(contextDataNode.get_nodes().get_length() > 0)
                        {
                            alert("节点有子节点，不能删除！");
                            return false;
                        }
                        if(!confirm("您确定要删除“" + contextDataNode.get_text() + "”吗？如果删除则该章节所有试题也将被禁用！"))
                        {
                            return false; 
                        }
                        
                        theForm.__EVENTTARGET.value = theBtn.id.replace("_", "$");                
                        theForm.submit();
                        
                        // 删除该节点并选择第一个节点


                        tvBookChapter.get_selectedNode().remove();
                        if(tvBookChapter.get_nodes().get_length() > 0)
                        {
                            tvBookChapter.get_nodes().getNode(0).select();
                        }
                    }                
                    break;     
            } 
            
            return true; 
        }
        function tvBookChapter_onLoad(sender, eventArgs)
        {
            tvBookChapter.expandAll();
        }
        function tvBookChapter_onNodeSelect(sender, eventArgs)
        {
            // 查看节点
            var node = eventArgs.get_node();            
            
            if(node.get_id() == 0)
            {
                document.getElementById(btnIds[1]).style.display = "none"; 
               document.getElementById(btnIds[2]).style.display = "none"; 
               document.getElementById(btnIds[8]).style.display = "none";  
                 document.getElementById(btnIds[7]).style.display = "none"; 
 
            }
            else
            {
                document.getElementById(btnIds[1]).style.display = ""; 
               document.getElementById(btnIds[2]).style.display = ""; 
               document.getElementById(btnIds[8]).style.display = "";  
                 document.getElementById(btnIds[7]).style.display = "";             
            }
            
            var theFrame = window.frames["ifBookChapterDetail"];
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
           // theItem.value = "";
            var search = window.location.search;
             var str= search.substring(search.indexOf("Mode=")+5);
            //alert("BookChapterDetail.aspx?id=" + node.get_id()+"&BookID=" + node.get_value() + "&ParentID="+ node.get_id());
            theFrame.location = "BookChapterDetail.aspx?id=" + node.get_id()+"&BookID=" + node.get_value() + "&ParentID="+ node.get_id()+"&Mode="+str;
        }   
        function tvBookChapter_onNodeRename(sender, eventArgs)
        {
            // 给节点重命名
            tvBookChapterRenameCallBack.callback('1', 'Rename', 'NewName', 'Result');
        } 
        function tvBookChapterChangeCallBack_onLoad(sender, eventArgs)
        {
            // 给树重排序

            if(tvBookChapter.get_nodes().get_length() > 0)
            {
                tvBookChapter.expandAll();
            }
        }
        function tvBookChapter_onCallbackComplete(sender, eventArgs)
        {
        }
        function tvBookChapterRenameCallBack_onCallbackComplete(sender, eventArgs)
        {
        }
        function tvBookChapterChangeCallBack_onCallbackComplete(sender, eventArgs)
        {
            tvBookChapter.expandAll();
            var id = document.getElementById("hfMaxID").value;
            if(id == "0")
            {
                 tvBookChapter.selectNodeById(sender.get_parameter());
            }
            else
            {
                tvBookChapter.selectNodeById(id);
               document.getElementById("hfMaxID").value = "0"; 
            }
            
            if(!tvBookChapter.get_selectedNode())
            {
                tvBookChapter.get_nodes().getNode(0).select();
            } 
        }
        function tvBookChapterMoveCallBack_onCallbackComplete(sender, eventArgs)
        {
            var search = window.location.search;
            var str= search.substring(search.indexOf("Mode=")+5);
             
            var theResult = document.getElementById("hfCanMove");        
            var theItem = document.getElementById("hfSelectedMenuItem");    
            
            if(!(theResult && theResult.value == "true"))
            {
                alert("不能移动该节点！");
                theItem.value == "";
                theResult.value == "";
                return;
            }
            
            if(theItem && theItem.value == "上移")
            {
                tvBookChapterChangeCallBack.callback(tvBookChapter.get_selectedNode().get_id(), "MoveUp",str);
            }
            if(theItem && theItem.value == "下移")
            {
                tvBookChapterChangeCallBack.callback(tvBookChapter.get_selectedNode().get_id(), "MoveDown",str);
            }
            
            theResult.value == "";
        }

        var btnIds = new Array("fvBookChapter_NewButton", "fvBookChapter_EditButton", "fvBookChapter_DeleteButton",
            "fvBookChapter_UpdateButton", "fvBookChapter_UpdateCancelButton",
            "fvBookChapter_InsertButton", "fvBookChapter_InsertCancelButton","fvBookChapter_NewButtonBrother","fvBookChapter_btnAdd");
            
        function imgBtns_onClick(btn)
        {              
             if(btn.action == "delete")
             {	                
                  var node = tvBookChapter.get_selectedNode();       
                  if(node.get_nodes().get_length() > 0)
                     {
                        alert("节点有子节点，不能删除！");
                        return ;
                     }
              }        	 
         	var frame = window.frames["ifBookChapterDetail"];
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
              	        var node = tvBookChapter.get_selectedNode();        
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
        	               document.getElementById(btnIds[8]).style.display = "none";   
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
        	               document.getElementById(btnIds[8]).style.display = "none";   
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
        	               document.getElementById(btnIds[8]).style.display = "none";  
        	            }
        	            else if(btn.action == "delete")
        	            {
        	            }
            	        
            	        
        	            break;
        	        }
        	    
        	        case "1":
                           document.getElementById(btnIds[0]).style.display = "";
    	                    document.getElementById(btnIds[1]).style.display = "";
    	                    document.getElementById(btnIds[2]).style.display = "";
    	                    document.getElementById(btnIds[3]).style.display = "none";
    	                    document.getElementById(btnIds[4]).style.display = "none";
    	                    document.getElementById(btnIds[5]).style.display = "none";
    	                    document.getElementById(btnIds[6]).style.display = "none";
    	                    document.getElementById(btnIds[7]).style.display = ""; 
                            document.getElementById(btnIds[8]).style.display = "";
                           break;        	       
                  case "2":
        	              var node = tvBookChapter.get_selectedNode();    
        	             if (node && node.get_id()==0)
        	             {
    	                    document.getElementById(btnIds[0]).style.display = "";
    	                    document.getElementById(btnIds[1]).style.display = "none";
    	                    document.getElementById(btnIds[2]).style.display = "none";
    	                    document.getElementById(btnIds[3]).style.display = "none";
    	                    document.getElementById(btnIds[4]).style.display = "none";
    	                    document.getElementById(btnIds[5]).style.display = "none";
    	                    document.getElementById(btnIds[6]).style.display = "none";
    	                    document.getElementById(btnIds[7]).style.display = "none"; 
                            document.getElementById(btnIds[8]).style.display = "none";         	             
                        }
                       else
                      {
                         document.getElementById(btnIds[0]).style.display = "";
    	                document.getElementById(btnIds[1]).style.display = "";
    	                document.getElementById(btnIds[2]).style.display = "";
    	                document.getElementById(btnIds[3]).style.display = "none";
    	                document.getElementById(btnIds[4]).style.display = "none";
    	                document.getElementById(btnIds[5]).style.display = "none";
    	                document.getElementById(btnIds[6]).style.display = "none";
    	                document.getElementById(btnIds[7]).style.display = ""; 
                        document.getElementById(btnIds[8]).style.display = ""; 
                      }   
                      break;
        	    
        	    default:
        	        {
        	            alert("状态错误！");
            	        
        	            return;
        	        }
        	}
        }
       
       function logout()
       {   
            var search = window.location.search;
            if(search.indexOf("Type") >=0)
            {
               top.returnValue = "true";
               top.close();
            }
       } 
    </script>

</head>
<body onbeforeunload="logout();">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="parent">
                        教材管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        教材章节</div>
                </div>
                <div id="button">
                    <img id="fvBookChapter_NewButtonBrother" onclick="imgBtns_onClick(this);" src="../Common/Image/addBrother.gif"
                        type="0" action="newBrother" alt="" style="display: none;" />
                    <img id="fvBookChapter_NewButton" onclick="imgBtns_onClick(this);" src="../Common/Image/addChild.gif"
                        type="0" action="newChild" alt="" />
                    <img id="fvBookChapter_EditButton" onclick="imgBtns_onClick(this);" src="../Common/Image/edit.gif"
                        type="0" action="edit" alt="" style="display: none;" />
                    <img id="fvBookChapter_DeleteButton" onclick="imgBtns_onClick(this);" src="../Common/Image/delete.gif"
                        type="0" action="delete" alt="" style="display: none;" />
                    <img id="fvBookChapter_btnAdd" onclick="imgBtns_onClick(this);" src="../Common/Image/chat.gif"
                        type="0" action="add" alt="" style="display: none;" />
                    <img id="fvBookChapter_UpdateButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="1" action="update" style="display: none;" alt="" />
                    <img id="fvBookChapter_UpdateCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="1" action="cancel" style="display: none;" alt="" />
                    <img id="fvBookChapter_InsertButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="2" action="insert" style="display: none;" alt="" />
                    <img id="fvBookChapter_InsertCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="2" action="cancel" style="display: none;" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        教材章节</div>
                    <div id="leftContent">
                        <ComponentArt:CallBack ID="tvBookChapterChangeCallBack" runat="server" OnCallback="tvBookChapterChangeCallBack_Callback">
                            <Content>
                                <ComponentArt:TreeView ID="tvBookChapter" EnableViewState="true" DragAndDropEnabled="false"
                                    NodeEditingEnabled="false" KeyboardEnabled="true" MultipleSelectEnabled="false"
                                    KeyboardCutCopyPasteEnabled="false" runat="server" DefaultTarget="ifBookChapterDetail"
                                    AutoAssignNodeIDs="false" ExpandNodeOnSelect="false" CollapseNodeOnSelect="false">
                                    <ClientEvents>
                                        <ContextMenu EventHandler="tvBookChapter_onContextMenu" />
                                        <NodeSelect EventHandler="tvBookChapter_onNodeSelect" />
                                        <NodeRename EventHandler="tvBookChapter_onNodeRename" />
                                        <Load EventHandler="tvBookChapter_onLoad" />
                                    </ClientEvents>
                                </ComponentArt:TreeView>
                            </Content>
                            <LoadingPanelClientTemplate>
                                Loading...
                            </LoadingPanelClientTemplate>
                            <ClientEvents>
                                <CallbackComplete EventHandler="tvBookChapterChangeCallBack_onCallbackComplete" />
                                <Load EventHandler="tvBookChapterChangeCallBack_onLoad" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                        <ComponentArt:Menu ID="tvBookChapterMenu" runat="server">
                            <ClientEvents>
                                <ItemSelect EventHandler="tvBookChapterMenu_onItemSelect" />
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
                        <ComponentArt:CallBack ID="tvBookChapterRenameCallBack" runat="server">
                            <ClientEvents>
                                <CallbackComplete EventHandler="tvBookChapterRenameCallBack_onCallbackComplete" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                        <ComponentArt:CallBack ID="tvBookChapterMoveCallBack" runat="server" OnCallback="tvBookChapterMoveCallBack_Callback">
                            <Content>
                                <asp:HiddenField ID="hfCanMove" runat="server" />
                            </Content>
                            <ClientEvents>
                                <CallbackComplete EventHandler="tvBookChapterMoveCallBack_onCallbackComplete" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        详细信息</div>
                    <div id="rightContent">
                        <iframe id="ifBookChapterDetail" frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField  ID="hfMaxID" runat="server" Value="0"/>
        <asp:HiddenField ID="hfBookID" runat="server" />
        <asp:HiddenField ID="hfSelectedMenuItem" runat="server" />
    </form>

    <script type="text/javascript">
        var theFrame = window.frames["ifBookChapterDetail"]; 
        var search = window.location.search;
        var str= search.substring(search.indexOf("Mode=")+5);  
        theFrame.location = "BookChapterDetail.aspx?id="+tvBookChapter.get_nodes().getNode(0).get_id()+"&BookID="+ +tvBookChapter.get_nodes().getNode(0).get_value() + "&ParentID=0&Mode="+str;
        // Preload CSS-referenced images 
        (new Image()).src = '~/App_Themes/Default/images/Menu/group_background.gif';
        (new Image()).src = '~/App_Themes/Default/images/Menu/break_bg.gif';
       
        if(tvBookChapter  && tvBookChapter.get_nodes().get_length() > 0)
        {
            tvBookChapter.get_nodes().getNode(0).select();
        } 
    </script>

</body>
</html>
