<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ItemCategory.aspx.cs" Inherits="RailExamWebApp.Item.ItemCategory" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>��������</title>

    <script src="../Common/JS/JSHelper.js" type="text/javascript">
    </script>

    <script type="text/javascript">
        var contextMenuX = 0; 
        var contextMenuY = 0; 

        //��ʾ�����Ĳ˵�
        function showContextMenu(e)
        {
            e = (e == null) ? window.event : e;
            contextMenuX = e.pageX ? e.pageX : e.x;
            contextMenuY = e.pageY ? e.pageY : e.y;
            
            var theItem = document.getElementById("hfSelectedMenuItem").value;
            var menuItemExpand = tvItemCategoryMenu.findItemByProperty("Text", "չ��");
            var menuItemCollapse = tvItemCategoryMenu.findItemByProperty("Text", "�۵�");
            var menuItemMoveUp = tvItemCategoryMenu.findItemByProperty("Text", "����");
            var menuItemMoveDown = tvItemCategoryMenu.findItemByProperty("Text", "����");            
            var menuItemNewBrother = tvItemCategoryMenu.findItemByProperty("Text", "����ͬ��");
             var menuItemNewChild = tvItemCategoryMenu.findItemByProperty("Text", "�����¼�");
            var menuItemEdit = tvItemCategoryMenu.findItemByProperty("Text", "�༭");  
            var menuItemDelete = tvItemCategoryMenu.findItemByProperty("Text", "ɾ��");  
            var menuItemSave = tvItemCategoryMenu.findItemByProperty("Text", "����");
            var menuItemCancel = tvItemCategoryMenu.findItemByProperty("Text", "ȡ��");
            
            if(theItem == "����ͬ��"��|| theItem == "�����¼�"��|| theItem == "�༭")
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
        
       //�˵�������
        function tvItemCategoryMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();     
            var contextDataNode = tvItemCategory.get_selectedNode();
            var theItem = document.getElementById("hfSelectedMenuItem");
            var theFrame = window.frames["ifItemCategoryDetail"];
            var theForm = window.frames["ifItemCategoryDetail"].document.forms[0];
           
            switch(menuItem.get_text())
            {
                case 'չ��':
                    contextDataNode.expand();
                    break;
                    
                case '�۵�':
                    contextDataNode.collapse();
                    break;
                    
                case 'չ��ȫ��':
                    tvItemCategory.expandAll();
                    break;
                    
                case '�۵�ȫ��':
                    tvItemCategory.collapseAll();
                    break;
                    
                case '�鿴':
                    if(tvItemCategory.get_selectedNode() == contextDataNode)
                    {
                        return false;
                    }
                    contextDataNode.select();
                    break;
            
                case '�����¼�':                      
                    var theBtn = document.getElementById(btnIds[0]);
                    imgBtns_onClick(theBtn);
                    
                    break;
                    
                case '����ͬ��':                      
                    var theBtn = document.getElementById(btnIds[7]);
                    imgBtns_onClick(theBtn);
                    
                    break;
            
                case '�༭':
                    contextDataNode.select();
                    if(theForm && theItem)
                    {
                        var theBtn = document.getElementById(btnIds[1]);
                        imgBtns_onClick(theBtn);
                    }
                    
                    break;
                    
                case '����':
                    var theBtn;
                    if(theItem && theItem.value == "�༭")
                    {
                        theBtn = document.getElementById(btnIds[3]);
                    }
                    if(theItem && (theItem.value == "����ͬ��" || theItem.value == "�����¼�") )
                    {
                        theBtn = document.getElementById(btnIds[5]);
                    }
                    if(theForm && theBtn && theItem)
                    {
                        imgBtns_onClick(theBtn);
                    }
                    break;     
                    
                case 'ȡ��':
                    var theBtn;
                    if(theItem && theItem.value == "�༭")
                    {
                        theBtn = document.getElementById(btnIds[4]);
                    }
                    if(theItem  && (theItem.value == "����ͬ��" || theItem.value == "�����¼�") )
                    {
                        theBtn = document.getElementById(btnIds[6]);
                    }
                    if(theForm && theBtn)
                    {
                        imgBtns_onClick(theBtn);
                    }  

                    break;
                    
                case '����':                    
                    theItem.value = "����";
                    if(tvItemCategoryNodeCanMove(contextDataNode.get_value(), "UP") == "False")
                    {
                        alert("�������Ƹýڵ㣡");
                    }
                    else
                    {
                        tvItemCategoryChangeCallBack.callback()
                    }
                    
                    break;
                    
                case '����':
                    theItem.value = "����";
                    if(tvItemCategoryNodeCanMove(contextDataNode.get_value(), "DOWN") == "False")
                    {
                        alert("�������Ƹýڵ㣡");
                    }
                    else
                    {
                        tvItemCategoryChangeCallBack.callback()
                    }
                    
                    break;
                
                case 'ɾ��':
                    var theBtn = document.getElementById(btnIds[2]);
                    if(theForm&&theBtn)
                    {
                        if(contextDataNode.get_nodes().get_length() > 0)
                        {
                            alert("�ڵ����ӽڵ㣬����ɾ����");
                            return false;
                        }
                        imgBtns_onClick(theBtn);
                        
                        // ɾ���ýڵ㲢ѡ���һ���ڵ�                        
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
        
       //�������ڵ�ѡ���¼������� 
        function tvItemCategory_onNodeSelect(sender, eventArgs)
        {
            // �鿴�ڵ�
            var node = eventArgs.get_node();
            var theFrame = window.frames["ifItemCategoryDetail"];
            var theItem = document.getElementById("hfSelectedMenuItem");

            if(theItem  && (theItem.value == "����ͬ��" || theItem.value == "�����¼�") )
            {
                if(confirm("Ҫ����������"))
                {
                    imgBtns_onClick(document.getElementById(btnIds[5]));
                    return;
                }
                else
                {
                    imgBtns_onClick(document.getElementById(btnIds[6]));
                }
            }
            else if(theItem && theItem.value == "�༭")
            {
                if(confirm("Ҫ����������"))
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
       
       //�ص���������¼������� 
        function tvItemCategory_onCallbackComplete(sender, eventArgs)
        {
        }
        
       //�ص���������¼������� 
        function tvItemCategoryChangeCallBack_onCallbackComplete(sender, eventArgs)
        {
            //tvItemCategory.expandAll();
            tvItemCategory.selectNodeById(sender.get_parameter());
        }
        
        var btnIds = new Array("fvItemCategoryDetail_NewButton", 
            "fvItemCategoryDetail_EditButton", "fvItemCategoryDetail_DeleteButton",
            "fvItemCategoryDetail_UpdateButton", "fvItemCategoryDetail_UpdateCancelButton",
            "fvItemCategoryDetail_InsertButton", "fvItemCategoryDetail_InsertCancelButton","fvItemCategoryDetail_NewButtonBrother");
            
        //����ť����¼�
        function imgBtns_onClick(btn)
        {
        	 var flagupdate=document.getElementById("HfUpdateRight").value;
        	  if(flagupdate=="False"&&(btn.action == "edit"||btn.action == "newChild" || btn.action == "newBrother"))
             {
                        alert("��û��Ȩ��ʹ�øò�����");                       
                        return;
              }
              var flagDelete=document.getElementById("HfDeleteRight").value;
        	   if(flagDelete=="False"&&btn.action == "delete")
                {
                        alert("��û��Ȩ��ʹ�øò�����");                       
                        return;
                 }
                      
                      
        	    if(btn.action == "delete")
        	     {
        	                var node = tvItemCategory.get_selectedNode();        	                 

                            if(node.get_nodes().get_length() > 0)
                               {
                                alert("�ڵ����ӽڵ㣬����ɾ����");
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
        
       //��ʾ�����ذ�ť 
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
        	        alert("״̬����");
        	        
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
                        ������</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        ��������</div>
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
                        ��������</div>
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
                        ����������ϸ��Ϣ</div>
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
                    <ComponentArt:MenuItem Text="����ͬ��" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" /> 
             ��    <ComponentArt:MenuItem Text="�����¼�" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" />
                    <ComponentArt:MenuItem Text="�༭" look-lefticonurl="edit.gif" disabledlook-lefticonurl="edit_disabled.gif" />
                    <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" />
                    <ComponentArt:MenuItem Text="ɾ��" look-lefticonurl="delete.gif" disabledlook-lefticonurl="delete_disabled.gif" />
                    <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" />
                    <ComponentArt:MenuItem Text="����" look-lefticonurl="save.gif" disabledlook-lefticonurl="save_disabled.gif" />
                    <ComponentArt:MenuItem Text="ȡ��" look-lefticonurl="cancel.gif" disabledlook-lefticonurl="cancel_disabled.gif" />
                    <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" />
                    <ComponentArt:MenuItem Text="����" look-lefticonurl="move_up.gif" disabledlook-lefticonurl="move_up_disabled.gif" />
                    <ComponentArt:MenuItem Text="����" look-lefticonurl="move_down.gif" disabledlook-lefticonurl="move_down_disabled.gif" />
                    <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" disabledlook-lefticonurl="new_disabled.gif" />
                    <ComponentArt:MenuItem Text="չ��" look-lefticonurl="expand.gif" disabledlook-lefticonurl="expand_disabled.gif" />
                    <ComponentArt:MenuItem Text="�۵�" look-lefticonurl="collapse.gif" disabledlook-lefticonurl="collapse_disabled.gif" />
                    <ComponentArt:MenuItem Text="չ��ȫ��" look-lefticonurl="expand_all.gif" disabledlook-lefticonurl="expand_all_disabled.gif" />
                    <ComponentArt:MenuItem Text="�۵�ȫ��" look-lefticonurl="collapse_all.gif" disabledlook-lefticonurl="collapse_all_disabled.gif" />
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
