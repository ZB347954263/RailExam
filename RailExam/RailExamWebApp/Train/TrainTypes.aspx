<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainTypes.aspx.cs" Inherits="RailExamWebApp.Train.TrainTypes" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.TrainType/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>培训类别</title>
    
    <script type="text/javascript">
        function tvTrainType_onContextMenu(sender, eventArgs)
        {
        
            var theItem = document.getElementById("hfSelectedMenuItem").value;
           
            var menuItemNewBrother = tvTrainTypeMenu.findItemByProperty("Text", "新增同级");
            var menuItemNewChild = tvTrainTypeMenu.findItemByProperty("Text", "新增下级");
             
            var menuItemEdit = tvTrainTypeMenu.findItemByProperty("Text", "编辑");
            var menuItemSave = tvTrainTypeMenu.findItemByProperty("Text", "保存");
            var menuItemCancel = tvTrainTypeMenu.findItemByProperty("Text", "取消");
            var menuItemDelete = tvTrainTypeMenu.findItemByProperty("Text", "删除");
               
           var menuItemUp = tvTrainTypeMenu.findItemByProperty("Text", "上移");
           var menuItemDown = tvTrainTypeMenu.findItemByProperty("Text", "下移");         
            
            
            if( theItem == "新增同级" || theItem == "新增下级" || theItem == "编辑")
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
            
            if(! tvTrainType.get_selectedNode()
              || tvTrainType.get_selectedNode().get_id() != eventArgs.get_node().get_id())
            {
                eventArgs.get_node().select();
            }
        
            switch(eventArgs.get_node().get_text())
            {
                default:
                    if(tvTrainType.get_selectedNode().get_id() == eventArgs.get_node().get_id())
                    {
                        tvTrainTypeMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_node());
                    }
                    break;
            }
            return true;
        }
        
        function tvTrainTypeMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();     
            var contextDataNode = menuItem.get_parentMenu().get_contextData(); 
            var theItem = document.getElementById("hfSelectedMenuItem");
            var theForm = window.frames["ifTrainTypeDetail"].document.forms[0];
     
            /*
            var feedback = '"' + menuItem.get_text() + '" command was issued on the "' 
                + contextDataNode.get_text() + '" node from "' + contextDataNode.get_value() + '".'; 
            alert(feedback);
            */
            
            switch(menuItem.get_text())
            {
                case '展开':
                    contextDataNode.expand();
                    
                    break;
                    
                case '折叠':
                    contextDataNode.collapse();
                    
                    break;
                    
                case '展开全部':
                    tvTrainType.expandAll();
                    
                    break;
                    
                case '折叠全部':
                    tvTrainType.collapseAll();
                    
                    break;
                    
                case '查看':
                    if(tvTrainType.get_selectedNode() == contextDataNode)
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
                    if(theItem  && (theItem.value == "新增同级" || theItem.value== "新增下级") )
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
                    tvTrainTypeMoveCallBack.callback(contextDataNode.get_value(), "CanMoveUp");
                    break;
                    
                case '下移':
                    theItem.value = "下移";
                    tvTrainTypeMoveCallBack.callback(contextDataNode.get_value(), "CanMoveDown");
                    break;
                
                case '删除':
                    var theBtn = window.frames["ifTrainTypeDetail"].document.getElementById("fvTrainType_DeleteButton");
                    if(theForm&&theBtn)
                    {
                        if(!confirm("您确定要删除“" + contextDataNode.get_text() + "”吗？"))
                        {
                            return false; 
                        }
                        if(contextDataNode.get_nodes().get_length() > 0)
                        {
                            alert("节点有子节点，不能删除！");
                            return false;
                        }
                        theForm.__EVENTTARGET.value = theBtn.id.replace("_", "$");                
                        theForm.submit();
                        
                        // 删除该节点并选择第一个节点


                        tvTrainType.get_selectedNode().remove();
                        if(tvTrainType.get_nodes().get_length() > 0)
                        {
                            tvTrainType.get_nodes().getNode(0).select();
                        }
                    }                
                    break;     
            } 
            
            return true; 
        }
        function tvTrainType_onLoad(sender, eventArgs)
        {
            tvTrainType.expandAll();
        }
        function tvTrainType_onNodeSelect(sender, eventArgs)
        {
            // 查看节点
            var node = eventArgs.get_node();            
            var theFrame = window.frames["ifTrainTypeDetail"];
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
            theFrame.location = "TrainTypeDetail.aspx?id=" + node.get_id();
            node.expand();
        }   
        function tvTrainType_onNodeRename(sender, eventArgs)
        {
            // 给节点重命名
            tvTrainTypeRenameCallBack.callback('1', 'Rename', 'NewName', 'Result');
        } 
        function tvTrainTypeChangeCallBack_onLoad(sender, eventArgs)
        {
            // 给树重排序

            if(tvTrainType.get_nodes().get_length() > 0)
            {
//                tvTrainType.expandAll();
            }
        }
        function tvTrainType_onCallbackComplete(sender, eventArgs)
        {
        }
        function tvTrainTypeRenameCallBack_onCallbackComplete(sender, eventArgs)
        {
        }
        function tvTrainTypeChangeCallBack_onCallbackComplete(sender, eventArgs)
        {
//          tvTrainType.expandAll();
            var id = document.getElementById("hfMaxID").value;
            if(id == "0")
            {
                 tvTrainType.selectNodeById(sender.get_parameter());
            }
            else
            {
                // tvTrainType.expandAll();
                 tvTrainType.selectNodeById(id);
                 tvTrainType.findNodeById(id).select();
                document.getElementById("hfMaxID").value = "0"; 
            }
            
            if(!tvTrainType.get_selectedNode())
            {
                tvTrainType.get_nodes().getNode(0).select();
            } 
        }
        
        function tvTrainTypeMoveCallBack_onCallbackComplete(sender, eventArgs)
        {
        
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
                tvTrainTypeChangeCallBack.callback(tvTrainType.get_selectedNode().get_id(), "MoveUp");
            }
            if(theItem && theItem.value == "下移")
            {
                tvTrainTypeChangeCallBack.callback(tvTrainType.get_selectedNode().get_id(), "MoveDown");
            }
            
            theResult.value == "";
        }
        
        var btnIds = new Array("fvTrainType_NewButton", "fvTrainType_EditButton", "fvTrainType_DeleteButton",
            "fvTrainType_UpdateButton", "fvTrainType_UpdateCancelButton",
            "fvTrainType_InsertButton", "fvTrainType_InsertCancelButton","fvTrainType_NewButtonBrother");
            
        function imgBtns_onClick(btn)
        {
        	  var flagupdate=document.getElementById("HfUpdateRight").value;
        	                 if(flagupdate=="False"&&(btn.action == "edit"||btn.action == "newChild" || btn.action == "newBrother"))
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
        	                var node = tvTrainType.get_selectedNode();    	                
        	                 
                    if(node.get_nodes().get_length() > 0)
                       {
                        alert("节点有子节点，不能删除！");
                        return ;
                       }
        	                
        	          }      
                      
         	var frame = window.frames["ifTrainTypeDetail"];
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
              	        var node = tvTrainType.get_selectedNode();        
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
         	}        }
        
        function setImageBtnVisiblity(btn)
        {
            var theItem = document.getElementById("hfSelectedMenuItem");
        	switch(btn.type)
        	{
        	    case "0":
        	    {
        	        if (btn.action == "newBrother")
        	        {
        	            document.getElementById(btnIds[0]).style.display = "none";
        	            document.getElementById(btnIds[1]).style.display = "none";
        	            document.getElementById(btnIds[2]).style.display = "none";
        	            document.getElementById(btnIds[3]).style.display = "none";
        	            document.getElementById(btnIds[4]).style.display = "none";
        	            document.getElementById(btnIds[5]).style.display = "";
        	            document.getElementById(btnIds[6]).style.display = "";
        	           document.getElementById(btnIds[7]).style.display = "none"; 
        	            theItem.value = "新增同级";
        	        }
        	        else  if (btn.action == "newChild")
        	        {
        	            document.getElementById(btnIds[0]).style.display = "none";
        	            document.getElementById(btnIds[1]).style.display = "none";
        	            document.getElementById(btnIds[2]).style.display = "none";
        	            document.getElementById(btnIds[3]).style.display = "none";
        	            document.getElementById(btnIds[4]).style.display = "none";
        	            document.getElementById(btnIds[5]).style.display = "";
        	            document.getElementById(btnIds[6]).style.display = "";
        	           document.getElementById(btnIds[7]).style.display = "none"; 
        	            theItem.value = "新增下级";
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
        	            theItem.value = "编辑";
        	        }
        	        else if(btn.action == "delete")
        	        {
        	            theItem.value = "";
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
    	            theItem.value = "";

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

    <script src="../Common/JS/JSHelper.js" type="text/javascript" />
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
                        培训类别</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo %>
                </div>
                <div id="button">
                    <img id="fvTrainType_NewButtonBrother" onclick="imgBtns_onClick(this);" src="../Common/Image/addBrother.gif"
                        type="0" action="newBrother" alt="" />
                    <img id="fvTrainType_NewButton" onclick="imgBtns_onClick(this);" src="../Common/Image/addChild.gif"
                        type="0" action="newChild" alt="" />
                    <img id="fvTrainType_EditButton" onclick="imgBtns_onClick(this);" src="../Common/Image/edit.gif"
                        type="0" action="edit" alt="" />
                    <img id="fvTrainType_DeleteButton" onclick="imgBtns_onClick(this);" src="../Common/Image/delete.gif"
                        type="0" action="delete" alt="" />
                    <img id="fvTrainType_UpdateButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="1" action="update" style="display: none;" alt="" />
                    <img id="fvTrainType_UpdateCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="1" action="cancel" style="display: none;" alt="" />
                    <img id="fvTrainType_InsertButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="2" action="insert" style="display: none;" alt="" />
                    <img id="fvTrainType_InsertCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="2" action="cancel" style="display: none;" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">培训类别</div>
                    <div id="leftContent">
                          <ComponentArt:CallBack ID="tvTrainTypeChangeCallBack" runat="server" OnCallback="tvTrainTypeChangeCallBack_Callback">
                                <Content>
                                    <ComponentArt:TreeView ID="tvTrainType" EnableViewState="true" DragAndDropEnabled="false"
                                        NodeEditingEnabled="false" KeyboardEnabled="true" MultipleSelectEnabled="false"
                                        KeyboardCutCopyPasteEnabled="false" runat="server" DefaultTarget="ifTrainTypeDetail"
                                        AutoAssignNodeIDs="false" ExpandNodeOnSelect="false" CollapseNodeOnSelect="false">
                                        <ClientEvents>
                                            <ContextMenu EventHandler="tvTrainType_onContextMenu" />
                                            <NodeSelect EventHandler="tvTrainType_onNodeSelect" />
                                            <%--<Load EventHandler="tvTrainType_onLoad" />--%>
                                        </ClientEvents>
                                    </ComponentArt:TreeView>
                                </Content>
                               <%--< <LoadingPanelClientTemplate>
                                    Loading...
                                </LoadingPanelClientTemplate>--%>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="tvTrainTypeChangeCallBack_onCallbackComplete" />
                                    <Load EventHandler="tvTrainTypeChangeCallBack_onLoad" />
                                </ClientEvents>
                            </ComponentArt:CallBack>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        培训类别详细信息
                    </div>
                    <div id="rightContent">
                          <iframe id="ifTrainTypeDetail" frameborder="0" scrolling="auto" class="iframe">
                         </iframe>
                   </div>
                </div>
           </div>
        </div>
        <div>
             <ComponentArt:Menu ID="tvTrainTypeMenu" runat="server" EnableViewState="false">
                    <ClientEvents>
                        <ItemSelect EventHandler="tvTrainTypeMenu_onItemSelect" />
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
                <ComponentArt:CallBack ID="tvTrainTypeMoveCallBack" runat="server" OnCallback="tvTrainTypeMoveCallBack_Callback">
                        <Content>
                            <asp:HiddenField ID="hfCanMove" runat="server" />
                        </Content>
                        <ClientEvents>
                            <CallbackComplete EventHandler="tvTrainTypeMoveCallBack_onCallbackComplete" />
                        </ClientEvents>
               </ComponentArt:CallBack>
        </div>
        <div>
            <asp:HiddenField ID="hfSelectedMenuItem" runat="server" />
             <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
          <asp:HiddenField  ID="hfMaxID" runat="server" Value="0"/>
        </div>
    </form>
    <script type="text/javascript">
        var theFrame = window.frames["ifTrainTypeDetail"];
        theFrame.location = "TrainTypeDetail.aspx?id="+tvTrainType.get_nodes().getNode(0).get_id()+"&ParentID=0";
        
        // Preload CSS-referenced images 
        (new Image()).src = '~/App_Themes/Default/images/Menu/group_background.gif';
        (new Image()).src = '~/App_Themes/Default/images/Menu/break_bg.gif';
    </script>
</body>
</html>
