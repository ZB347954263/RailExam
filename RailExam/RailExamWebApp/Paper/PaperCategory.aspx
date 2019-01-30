<%@ Page Language="C#" AutoEventWireup="True" Codebehind="PaperCategory.aspx.cs"
    Inherits="RailExamWebApp.Paper.PaperCategory" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试卷类别</title>

    <script type="text/javascript">
        var btnIds = new Array("fvPaperCategory_NewButton", "fvPaperCategory_EditButton", "fvPaperCategory_DeleteButton",
            "fvPaperCategory_UpdateButton", "fvPaperCategory_UpdateCancelButton",
            "fvPaperCategory_InsertButton", "fvPaperCategory_InsertCancelButton","fvPaperCategory_NewButtonBrother");
            
        function tvPaperCategory_onNodeSelect(sender, eventArgs)
        {
            // 查看节点
            var node = eventArgs.get_node();
            var theFrame = window.frames["ifPaperCategoryDetail"];
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
            theFrame.location = "PaperCategoryDetail.aspx?id=" + node.get_id();
        }
       
        function tvPaperCategory_onContextMenu(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem").value;
            var menuItemNewBrother = treeContextMenu.findItemByProperty("Text", "新增同级");
             var menuItemNewChild = treeContextMenu.findItemByProperty("Text", "新增下级");
            var menuItemEdit = treeContextMenu.findItemByProperty("Text", "编辑");
            var menuItemSave = treeContextMenu.findItemByProperty("Text", "保存");
            var menuItemCancel = treeContextMenu.findItemByProperty("Text", "取消");
            var menuItemDelete = treeContextMenu.findItemByProperty("Text", "删除");
            
            if(theItem == "新增同级" || theItem == "编辑"  || theItem=="新增下级")
            {
                menuItemNewBrother.set_enabled(false);
               menuItemNewChild.set_enabled(false); 
                menuItemEdit.set_enabled(false);
                menuItemSave.set_enabled(true);
                menuItemCancel.set_enabled(true);
                menuItemDelete.set_enabled(false); 
            }
            else
            {
                menuItemNewBrother.set_enabled(true);
               menuItemNewChild.set_enabled(true); 
                menuItemEdit.set_enabled(true);
                menuItemSave.set_enabled(false);
                menuItemCancel.set_enabled(false);
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
                                                      
                }                                              
                                     
                  if(flagDelete=="False")
                 {                     
                    menuItemDelete.set_enabled(false);   
                   }                
                   
            
            if(! tvPaperCategory.get_selectedNode()
              || tvPaperCategory.get_selectedNode().get_id() != eventArgs.get_node().get_id())
            {
                eventArgs.get_node().select();
            }

            switch(eventArgs.get_node().get_text())
            {
                default:
                    if(tvPaperCategory.get_selectedNode().get_id() == eventArgs.get_node().get_id())
                    {
                        treeContextMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_node());
                    }
                    
                    break;
            }
            return true;
        }
        
        function treeContextMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();
            var contextDataNode = menuItem.get_parentMenu().get_contextData();
            var theItem = document.getElementById("hfSelectedMenuItem");
            var theForm = window.frames["ifPaperCategoryDetail"].document.forms[0];
            
            switch(menuItem.get_text())
            {
                case '展开':
                    contextDataNode.expand();
                    
                    break;
                    
                case '折叠':
                    contextDataNode.collapse();
                    
                    break;
                    
                case '展开全部':
                    tvPaperCategory.expandAll();
                    
                    break;
                    
                case '折叠全部':
                    tvPaperCategory.collapseAll();
                    
                    break;
                    
                case '查看':
                    if(tvPaperCategory.get_selectedNode() == contextDataNode)
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
                    
                    if(theItem && (theItem.value == "新增同级" || theItem.value == "新增下级"))
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
                    if(theItem && (theItem.value == "新增同级" || theItem.value == "新增下级"))
                    {
                        theBtn = document.getElementById(btnIds[6]);
                    }
                    if(theForm && theBtn)
                    {
                        imgBtns_onClick(theBtn);
                    }  

                    break;
                    
                case '删除':
                    

                    if(tvPaperCategory.get_selectedNode().get_nodes().get_length() > 0)
                    {
                        alert("节点有子节点，不能删除！");
                        return false;
                    }
                     
                    var theBtn = document.getElementById(btnIds[2]);
                    if(theForm && theBtn)
                    {                        
                        if(contextDataNode.get_nodes().get_length() > 0)
                        {
                            alert("节点有子节点，不能删除！");
                            return false;
                        }
                        
                        imgBtns_onClick(theBtn);
                        // 删除该节点并选择第一个节点
                        //tvPaperCategory.get_selectedNode().remove();
                        //if(tvPaperCategory.get_nodes().get_length() > 0)
                        //{
                        //    tvPaperCategory.get_nodes().getNode(0).select();
                        //}
                    }
                    
                    break;
            }
            
            return true;
        }
        
        function tvPaperCategoryChangeCallBack_onCallbackComplete(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem");
            if(theItem && theItem.value != "")
            {
                theItem.value == "";
            }
        
//            tvPaperCategory.expandAll();
            tvPaperCategory.selectNodeById(sender.get_parameter());
        }
         
        function tvPaperCategoryChangeCallBack_onLoad(sender, eventArgs)
        {
            // 给树重排序

            if(tvPaperCategory.get_nodes().get_length() > 0)
            {
//                tvPaperCategory.expandAll();
            }
        }
            
        function imgBtns_onClick(btn)
        {   
                    var flagupdate=document.getElementById("HfUpdateRight").value;
        	         if(flagupdate=="False"&&(btn.action == "edit"||btn.action == "newBrother" || btn.action=="newChild"  ))
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
        	                var node = tvPaperCategory.get_selectedNode();
        	                
        	                  if(node.get_value() == 1)
                       {
                        alert("该考试类别为固定类别，不能删除！");
                        return ;
                        }

                    if(node.get_nodes().get_length() > 0)
                       {
                        alert("节点有子节点，不能删除！");
                        return ;
                       }
        	                
        	          }        	                   	            
              
         	var frame = window.frames["ifPaperCategoryDetail"];
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
              	        var node = tvPaperCategory.get_selectedNode();        
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
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        试卷管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        试卷类别</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
<%--                <div id="button">
                   <img id="fvPaperCategory_NewButtonBrother" onclick="imgBtns_onClick(this);"  src="../Common/Image/addBrother.gif"
                        type="0" action="newBrother" alt="" />
                    <img id="fvPaperCategory_NewButton" onclick="imgBtns_onClick(this);" src="../Common/Image/addChild.gif"
                        type="0" action="newChild" alt="" />
                     <img id="fvPaperCategory_EditButton" onclick="imgBtns_onClick(this);" src="../Common/Image/edit.gif"
                        type="0" action="edit" alt="" />
                    <img id="fvPaperCategory_DeleteButton" onclick="imgBtns_onClick(this);" src="../Common/Image/delete.gif"
                        type="0" action="delete" alt="" />
                    <img id="fvPaperCategory_UpdateButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="1" action="update" style="display: none;" alt="" />
                    <img id="fvPaperCategory_UpdateCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="1" action="cancel" style="display: none;" alt="" />
                    <img id="fvPaperCategory_InsertButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="2" action="insert" style="display: none;" alt="" />
                    <img id="fvPaperCategory_InsertCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="2" action="cancel" style="display: none;" alt="" />
                </div>--%>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        试卷类别</div>
                    <div id="leftContent">
                        <ComponentArt:CallBack ID="tvPaperCategoryChangeCallBack" runat="server" OnCallback="tvPaperCategoryChangeCallBack_Callback">
                            <Content>
                                <ComponentArt:TreeView ID="tvPaperCategory" runat="server" EnableViewState="true">
                                    <ClientEvents>
                                        <NodeSelect EventHandler="tvPaperCategory_onNodeSelect" />
<%--                                        <ContextMenu EventHandler="tvPaperCategory_onContextMenu" />
--%>                                    </ClientEvents>
                                </ComponentArt:TreeView>
                            </Content>
                            <%--<LoadingPanelClientTemplate>
                                加载数据...
                            </LoadingPanelClientTemplate>--%>
                            <ClientEvents>
                                <CallbackComplete EventHandler="tvPaperCategoryChangeCallBack_onCallbackComplete" />
                                <Load EventHandler="tvPaperCategoryChangeCallBack_onLoad" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        试卷类别详细信息</div>
                    <div id="rightContent">
                        <iframe id="ifPaperCategoryDetail" frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <ComponentArt:Menu ID="treeContextMenu" ContextMenu="Custom" runat="server">
            <ClientEvents>
                <ItemSelect EventHandler="treeContextMenu_onItemSelect" />
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
                <ComponentArt:MenuItem Text="展开" look-lefticonurl="expand.gif" disabledlook-lefticonurl="expand_disabled.gif" />
                <ComponentArt:MenuItem Text="折叠" look-lefticonurl="collapse.gif" disabledlook-lefticonurl="collapse_disabled.gif" />
                <ComponentArt:MenuItem Text="展开全部" look-lefticonurl="expand_all.gif" disabledlook-lefticonurl="expand_all_disabled.gif" />
                <ComponentArt:MenuItem Text="折叠全部" look-lefticonurl="collapse_all.gif" disabledlook-lefticonurl="collapse_all_disabled.gif" />
            </Items>
        </ComponentArt:Menu>
        <asp:HiddenField ID="hfSelectedMenuItem" runat="server" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>

    <script type="text/javascript">
        if(tvPaperCategory && tvPaperCategory.get_nodes().get_length() > 0)
        {
            tvPaperCategory.get_nodes().getNode(0).select();
        }
    </script>

</body>
</html>
