<%@ Page Language="C#" AutoEventWireup="True" Codebehind="Organization.aspx.cs" Inherits="RailExamWebApp.Systems.Organization" %>
<%@ Import namespace="RailExamWebApp.Common.Class"%>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组织机构</title>
    <script type="text/javascript">
        //组织机构树上下文菜单 
        function tvOrganization_onContextMenu(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem").value;
            var menuItemNewBrother = tvOrganizationMenu.findItemByProperty("Text", "新增同级");
             var menuItemNewChild = tvOrganizationMenu.findItemByProperty("Text", "新增下级");
            var menuItemEdit = tvOrganizationMenu.findItemByProperty("Text", "编辑");
            var menuItemSave = tvOrganizationMenu.findItemByProperty("Text", "保存");
            var menuItemCancel = tvOrganizationMenu.findItemByProperty("Text", "取消");
            var menuItemDelete = tvOrganizationMenu.findItemByProperty("Text", "删除");
            var menuItemUp = tvOrganizationMenu.findItemByProperty("Text", "上移");
            var menuItemDown = tvOrganizationMenu.findItemByProperty("Text", "下移");
            
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
                                
            if(!tvOrganization.get_selectedNode() || 
                tvOrganization.get_selectedNode().get_id() != eventArgs.get_node().get_id())
            {
                eventArgs.get_node().select();
            }
            
            if(form1.IsWuhanOnly.value == "True")
            {
                 menuItemNewBrother.set_enabled(false);
                menuItemNewChild.set_enabled(false); 
                menuItemEdit.set_enabled(false);
                menuItemDelete.set_enabled(false);   
            }

            switch(eventArgs.get_node().get_text())
            {
                default:
                if(tvOrganization.get_selectedNode().get_id() == eventArgs.get_node().get_id()){
                    tvOrganizationMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_node());
                }
                break;
            }
            return true;
        }
        
       //菜单处理函数 
        function tvOrganizationMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();
            var contextDataNode = menuItem.get_parentMenu().get_contextData();
            var theItem = document.getElementById("hfSelectedMenuItem");
            var theForm = window.frames["ifOrganizationDetail"].document.forms[0];
           
            switch(menuItem.get_text())
            {
                case '展开':
                    contextDataNode.expand();
                    break;
                    
                case '折叠':
                    contextDataNode.collapse();
                    break;
                    
                case '展开全部':
                    tvOrganization.expandAll();
                    break;
                    
                case '折叠全部':
                    tvOrganization.collapseAll();
                    break;
                    
                case '查看':
                    if(tvOrganization.get_selectedNode() == contextDataNode)
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
                    if(tvOrganizationNodeMove(contextDataNode.get_value(), "UP") == "False")
                    {
                        alert("不能上移该节点！");
                    }
                    else
                    {
                    	tvOrganizationChangeCallBack.callback(contextDataNode.get_value(), "Up");
                    }
                    
                    break;
                    
                case '下移':
                    theItem.value = "下移";
                    if(tvOrganizationNodeMove(contextDataNode.get_value(), "DOWN") == "False")
                    {
                        alert("不能下移该节点！");
                    }
                    else
                    {
                       tvOrganizationChangeCallBack.callback(contextDataNode.get_value(), "Down");
                    }
                    
                    break;
                
                case '删除':
                    var theBtn = document.getElementById(btnIds[2]);
                    if(theForm&&theBtn)
                    {
                        imgBtns_onClick(theBtn);
                    }
                    break;
            }
            
            return true;
        }
        
       //组织机构节点选择事件处理函数 
        function tvOrganization_onNodeSelect(sender, eventArgs)
        {
            // 查看节点
            var node = eventArgs.get_node();
            var theFrame = window.frames["ifOrganizationDetail"];
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
        	
        	if(node.get_id()==0) {
        		document.getElementById(btnIds[0]).style.display = "none";
                document.getElementById(btnIds[1]).style.display = "none";
                document.getElementById(btnIds[2]).style.display = "none";
                document.getElementById(btnIds[7]).style.display = "none";
        	}
        	else {
        		document.getElementById(btnIds[0]).style.display = "";
                document.getElementById(btnIds[1]).style.display = "";
                document.getElementById(btnIds[2]).style.display = "";
                document.getElementById(btnIds[7]).style.display = "";
        	}
        	
            theFrame.location = "OrganizationDetail.aspx?id=" + node.get_id();
            node.expand();
        } 
          
         //回调完成处理函数
        function tvOrganizationChangeCallBack_onCallbackComplete(sender, eventArgs)
        {
        	var theItem = document.getElementById("hfSelectedMenuItem");  
            if(theItem && theItem.value != "")
            {
                theItem.value == "";
            }  
                                    
            var id = document.getElementById("hfMaxID").value;
            if(id == "0")
            {
                 tvOrganization.selectNodeById(sender.get_parameter());
            }
            else
            {
                 tvOrganization.selectNodeById(id);
                 tvOrganization.findNodeById(id).select();
                 document.getElementById("hfMaxID").value = "0"; 
            }
            
            if(!tvOrganization.get_selectedNode())
            {
                tvOrganization.get_nodes().getNode(0).select();
            } 
        }
        
       //组织机构树加载处理函数 
        function tvOrganizationChangeCallBack_onLoad(sender, eventArgs)
        {
            if(tvOrganization.get_nodes().get_length() > 0)
            {
                //tvOrganization.expandAll();
            }
        }
       
        var btnIds = new Array("fvOrganization_NewButton", "fvOrganization_EditButton", "fvOrganization_DeleteButton",
            "fvOrganization_UpdateButton", "fvOrganization_UpdateCancelButton",
            "fvOrganization_InsertButton", "fvOrganization_InsertCancelButton","fvOrganization_NewButtonBrother");
        
       //按钮点击事件处理函数  
        function imgBtns_onClick(btn)
        {
            if(btn.action == "newBrother")
            {
                var node = tvOrganization.get_selectedNode();    	                
                
               if(node)
               {
                 if(node.get_id() == 0)
                 {
                        alert("不能新增与[神华包神铁路集团公司]同级的组织机构！");
                       return; 
                 } 
                  
                 if(form1.hfSuitRange.value !=1 && node.get_depth == 2)
                 {
                        alert("站段不能新增本站段同级的组织机构！");
                       return;                  
                  }  
               } 
            } 
            
            
            if(btn.action == "newBrother" || btn.action == "newChild")
            {
                if(!tvOrganization.get_selectedNode())
                {
                    alert("请选择一个组织机构节点！");
                    return; 
                }
            }
            
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
                var node = tvOrganization.get_selectedNode();    	                

                if(node.get_nodes().get_length() > 0)
                {
                alert("节点有子节点，不能删除！");
                return ;
                }
            }      
        	          
            var frame = window.frames["ifOrganizationDetail"];
         	
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
              	        var node = tvOrganization.get_selectedNode();        
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
<body >
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                     <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        系统管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        组织机构</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                   <img id="fvOrganization_NewButtonBrother" onclick="imgBtns_onClick(this);"  src="../Common/Image/addBrother.gif"
                        type="0" action="newBrother" style="display: none;" alt="" />
                    <img id="fvOrganization_NewButton" onclick="imgBtns_onClick(this);" src="../Common/Image/addChild.gif"
                        type="0" action="newChild" style="display: none;" alt="" />
                    <img id="fvOrganization_EditButton" onclick="imgBtns_onClick(this);" src="../Common/Image/edit.gif"
                        type="0" action="edit" style="display: none;" alt="" />
                    <img id="fvOrganization_DeleteButton" onclick="imgBtns_onClick(this);" src="../Common/Image/delete.gif"
                        type="0" action="delete" style="display: none;" alt="" />
                    <img id="fvOrganization_UpdateButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="1" action="update" style="display: none;" alt="" />
                    <img id="fvOrganization_UpdateCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="1" action="cancel" style="display: none;" alt="" />
                    <img id="fvOrganization_InsertButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="2" action="insert" style="display: none;" alt="" />
                    <img id="fvOrganization_InsertCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="2" action="cancel" style="display: none;" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        组织机构</div>
                    <div id="leftContent">
                        <ComponentArt:CallBack ID="tvOrganizationChangeCallBack" runat="server" OnCallback="tvOrganizationChangeCallBack_Callback">
                            <Content>
                                <ComponentArt:TreeView ID="tvOrganization" runat="server" EnableViewState="false">
                                    <ClientEvents>
                                        <ContextMenu EventHandler="tvOrganization_onContextMenu" />
                                        <NodeSelect EventHandler="tvOrganization_onNodeSelect" />
                                    </ClientEvents>
                                </ComponentArt:TreeView>
                            </Content>
                            <ClientEvents>
                                <CallbackComplete EventHandler="tvOrganizationChangeCallBack_onCallbackComplete" />
                                <Load EventHandler="tvOrganizationChangeCallBack_onLoad" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        组织机构详细信息</div>
                    <div id="rightContent">
                        <iframe id="ifOrganizationDetail" frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <ComponentArt:Menu ID="tvOrganizationMenu" runat="server" EnableViewState="false">
            <ClientEvents>
                <ItemSelect EventHandler="tvOrganizationMenu_onItemSelect" />
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
       <input type="hidden" name="hfSuitRange" value='<%=PrjPub.CurrentLoginUser.SuitRange %>' />
       <input type="hidden" name="IsWuhanOnly" value='<%=PrjPub.IsWuhanOnly()%>' /> 
    </form>

    <script type="text/javascript">
        if(tvOrganization && tvOrganization.get_nodes().get_length() > 0)
        {
            tvOrganization.get_nodes().getNode(0).select();
        }
        
        (new Image()).src = '~/App_Themes/Default/images/Menu/group_background.gif';
        (new Image()).src = '~/App_Themes/Default/images/Menu/break_bg.gif';
    </script>

</body>
</html>
