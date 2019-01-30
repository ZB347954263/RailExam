<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ItemCategory.aspx.cs" Inherits="RailExamWebApp.Item.ItemCategory" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>辅助分类</title>

    <script src="../Common/JS/JSHelper.js" type="text/javascript">
    </script>

    <script type="text/javascript">
        var contextMenuX = 0; 
        var contextMenuY = 0; 

        //显示上下文菜单
        function showContextMenu(e)
        {
            e = (e == null) ? window.event : e;
            contextMenuX = e.pageX ? e.pageX : e.x;
            contextMenuY = e.pageY ? e.pageY : e.y;
            
            var theItem = document.getElementById("hfSelectedMenuItem").value;
            var menuItemExpand = tvItemCategoryMenu.findItemByProperty("Text", "展开");
            var menuItemCollapse = tvItemCategoryMenu.findItemByProperty("Text", "折叠");
            var menuItemMoveUp = tvItemCategoryMenu.findItemByProperty("Text", "上移");
            var menuItemMoveDown = tvItemCategoryMenu.findItemByProperty("Text", "下移");            
            var menuItemNewBrother = tvItemCategoryMenu.findItemByProperty("Text", "新增同级");
             var menuItemNewChild = tvItemCategoryMenu.findItemByProperty("Text", "新增下级");
            var menuItemEdit = tvItemCategoryMenu.findItemByProperty("Text", "编辑");  
            var menuItemDelete = tvItemCategoryMenu.findItemByProperty("Text", "删除");  
            var menuItemSave = tvItemCategoryMenu.findItemByProperty("Text", "保存");
            var menuItemCancel = tvItemCategoryMenu.findItemByProperty("Text", "取消");
            
            if(theItem == "新增同级"　|| theItem == "新增下级"　|| theItem == "编辑")
            {
                menuItemNewBrother.set_enabled(false);
               menuItemNewChild.set_enabled(false); 
                menuItemEdit.set_enabled(false);
                menuItemSave.set_enabled(true);
                menuItemCancel.set_enabled(true);
               menuItemMoveUp.set_enabled(false);
               menuItemMoveDown.set_enabled(false); 
               menuItemDelete.set_enabled(false);
            }
            else
            {
                menuItemNewBrother.set_enabled(true);
               menuItemNewChild.set_enabled(true); 
                if(tvItemCategory.get_selectedNode())
                {
                    menuItemEdit.set_enabled(true);            
                }
                else
                {
                    menuItemEdit.set_enabled(false);            
                }
                menuItemSave.set_enabled(false);
                menuItemCancel.set_enabled(false); 
                menuItemMoveUp.set_enabled(true);
               menuItemMoveDown.set_enabled(true);  
               menuItemDelete.set_enabled(true);      
            }


             var flagUpdate=document.getElementById("HfUpdateRight").value;   
             var flagDelete=document.getElementById("HfDeleteRight").value;               
           
                
        	 if(flagUpdate=="False")
             {  
                menuItemNewBrother.set_enabled(false);
               menuItemNewChild.set_enabled(false); 
                menuItemEdit.set_enabled(false);
                menuItemSave.set_enabled(false);                
                menuItemMoveUp.set_enabled(false);
                menuItemMoveDown.set_enabled(false);                                         
              }                 
                                     
           if(flagDelete=="False")
           {                     
                 menuItemDelete.set_enabled(false);   
           }  
              
            tvItemCategoryMenu.showContextMenu(contextMenuX, contextMenuY);
            e.cancelBubble = true;
            e.returnValue = false;

            return false;
        }
        
       //菜单处理函数
        function tvItemCategoryMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();     
            var contextDataNode = tvItemCategory.get_selectedNode();
            var theItem = document.getElementById("hfSelectedMenuItem");
            var theFrame = window.frames["ifItemCategoryDetail"];
            var theForm = window.frames["ifItemCategoryDetail"].document.forms[0];
           
            switch(menuItem.get_text())
            {
                case '展开':
                    contextDataNode.expand();
                    break;
                    
                case '折叠':
                    contextDataNode.collapse();
                    break;
                    
                case '展开全部':
                    tvItemCategory.expandAll();
                    break;
                    
                case '折叠全部':
                    tvItemCategory.collapseAll();
                    break;
                    
                case '查看':
                    if(tvItemCategory.get_selectedNode() == contextDataNode)
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
                    contextDataNode.select();
                    if(theForm && theItem)
                    {
                        var theBtn = document.getElementById(btnIds[1]);
                        imgBtns_onClick(theBtn);
                    }
                    
                    break;
                    
                case '保存':
                    var theBtn;
                    if(theItem && theItem.value == "编辑")
                    {
                        theBtn = document.getElementById(btnIds[3]);
                    }
                    if(theItem && (theItem.value == "新增同级" || theItem.value == "新增下级") )
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
                    if(theItem  && (theItem.value == "新增同级" || theItem.value == "新增下级") )
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
                    if(tvItemCategoryNodeCanMove(contextDataNode.get_value(), "UP") == "False")
                    {
                        alert("不能上移该节点！");
                    }
                    else
                    {
                        tvItemCategoryChangeCallBack.callback()
                    }
                    
                    break;
                    
                case '下移':
                    theItem.value = "下移";
                    if(tvItemCategoryNodeCanMove(contextDataNode.get_value(), "DOWN") == "False")
                    {
                        alert("不能下移该节点！");
                    }
                    else
                    {
                        tvItemCategoryChangeCallBack.callback()
                    }
                    
                    break;
                
                case '删除':
                    var theBtn = document.getElementById(btnIds[2]);
                    if(theForm&&theBtn)
                    {
                        if(contextDataNode.get_nodes().get_length() > 0)
                        {
                            alert("节点有子节点，不能删除！");
                            return false;
                        }
                        imgBtns_onClick(theBtn);
                        
                        // 删除该节点并选择第一个节点                        
                        //var parentNode = tvItemCategory.get_selectedNode().get_parentNode();
                        //tvItemCategory.get_selectedNode().remove();                        
                        //if(parentNode)
                        //{
                        //    parentNode.select();
                        //}
                        //else if(tvItemCategory.get_nodes().get_length() > 0)
                        //{
                        //    tvItemCategory.get_nodes().getNode(0).select();
                        //}
                        //else
                        //{
                        //    theForm.document.parentWindow.location = "ItemCategoryDetail.aspx";
                        //}
                    }                
                    break;     
            } 
            
            return true; 
        }
        
       //试题树节点选择事件处理函数 
        function tvItemCategory_onNodeSelect(sender, eventArgs)
        {
            // 查看节点
            var node = eventArgs.get_node();
            var theFrame = window.frames["ifItemCategoryDetail"];
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
            theFrame.location = "ItemCategoryDetail.aspx?id=" + node.get_id();
            node.expand();
        }
       
       //回调处理完成事件处理函数 
        function tvItemCategory_onCallbackComplete(sender, eventArgs)
        {
        }
        
       //回调处理完成事件处理函数 
        function tvItemCategoryChangeCallBack_onCallbackComplete(sender, eventArgs)
        {
            //tvItemCategory.expandAll();
            tvItemCategory.selectNodeById(sender.get_parameter());
        }
        
        var btnIds = new Array("fvItemCategoryDetail_NewButton", 
            "fvItemCategoryDetail_EditButton", "fvItemCategoryDetail_DeleteButton",
            "fvItemCategoryDetail_UpdateButton", "fvItemCategoryDetail_UpdateCancelButton",
            "fvItemCategoryDetail_InsertButton", "fvItemCategoryDetail_InsertCancelButton","fvItemCategoryDetail_NewButtonBrother");
            
        //处理按钮点击事件
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
        	                var node = tvItemCategory.get_selectedNode();        	                 

                            if(node.get_nodes().get_length() > 0)
                               {
                                alert("节点有子节点，不能删除！");
                                return ;
                               }
        	                
        	        }        	 
              
              
              
         	var frame = window.frames["ifItemCategoryDetail"];
         	
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
              	        var node = tvItemCategory.get_selectedNode();        
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
        
       //显示或隐藏按钮 
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
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        题库管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        辅助分类</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                   <img id="fvItemCategoryDetail_NewButtonBrother" onclick="imgBtns_onClick(this);"  src="../Common/Image/addBrother.gif"
                        type="0" action="newBrother" alt="" />
                    <img id="fvItemCategoryDetail_NewButton" onclick="imgBtns_onClick(this);" src="../Common/Image/addChild.gif"
                        type="0" action="newChild" alt="" />
                    <img id="fvItemCategoryDetail_EditButton" onclick="imgBtns_onClick(this);" src="../Common/Image/edit.gif"
                        type="0" action="edit" alt="" />
                    <img id="fvItemCategoryDetail_DeleteButton" onclick="imgBtns_onClick(this);" src="../Common/Image/delete.gif"
                        type="0" action="delete" alt="" />
                    <img id="fvItemCategoryDetail_UpdateButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="1" action="update" style="display: none;" alt="" />
                    <img id="fvItemCategoryDetail_UpdateCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="1" action="cancel" style="display: none;" alt="" />
                    <img id="fvItemCategoryDetail_InsertButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="2" action="insert" style="display: none;" alt="" />
                    <img id="fvItemCategoryDetail_InsertCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="2" action="cancel" style="display: none;" alt="" />
                </div> 
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        辅助分类</div>
                    <div id="leftContent" oncontextmenu="showContextMenu(event);return false;">
                        <ComponentArt:CallBack ID="tvItemCategoryChangeCallBack" runat="server" Debug="false"
                            OnCallback="tvItemCategoryChangeCallBack_Callback">
                            <Content>
                                <ComponentArt:TreeView ID="tvItemCategory" runat="server" EnableViewState="false">
                                    <ClientEvents>
                                        <NodeSelect EventHandler="tvItemCategory_onNodeSelect" />
                                    </ClientEvents>
                                </ComponentArt:TreeView>
                            </Content>
                            <ClientEvents>
                                <CallbackComplete EventHandler="tvItemCategoryChangeCallBack_onCallbackComplete" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        辅助分类详细信息</div>
                    <div id="rightContent">
                        <iframe id="ifItemCategoryDetail" frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
            <ComponentArt:Menu ID="tvItemCategoryMenu" runat="server" EnableViewState="true"
                ShadowEnabled="true">
                <ClientEvents>
                    <ItemSelect EventHandler="tvItemCategoryMenu_onItemSelect" />
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
        </div>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>

    <script type="text/javascript">
        if (tvItemCategory && tvItemCategory.get_nodes().get_length() > 0)
        {
            tvItemCategory.get_nodes().getNode(0).select();
        }
        
        // Preload CSS-referenced images 
        (new Image()).src = '~/App_Themes/Default/images/Menu/group_background.gif';
        (new Image()).src = '~/App_Themes/Default/images/Menu/break_bg.gif';
    </script>

</body>
</html>
