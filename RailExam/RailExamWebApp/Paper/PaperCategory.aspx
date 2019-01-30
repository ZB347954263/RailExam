<%@ Page Language="C#" AutoEventWireup="True" Codebehind="PaperCategory.aspx.cs"
    Inherits="RailExamWebApp.Paper.PaperCategory" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�Ծ����</title>

    <script type="text/javascript">
        var btnIds = new Array("fvPaperCategory_NewButton", "fvPaperCategory_EditButton", "fvPaperCategory_DeleteButton",
            "fvPaperCategory_UpdateButton", "fvPaperCategory_UpdateCancelButton",
            "fvPaperCategory_InsertButton", "fvPaperCategory_InsertCancelButton","fvPaperCategory_NewButtonBrother");
            
        function tvPaperCategory_onNodeSelect(sender, eventArgs)
        {
            // �鿴�ڵ�
            var node = eventArgs.get_node();
            var theFrame = window.frames["ifPaperCategoryDetail"];
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
            theFrame.location = "PaperCategoryDetail.aspx?id=" + node.get_id();
        }
       
        function tvPaperCategory_onContextMenu(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem").value;
            var menuItemNewBrother = treeContextMenu.findItemByProperty("Text", "����ͬ��");
             var menuItemNewChild = treeContextMenu.findItemByProperty("Text", "�����¼�");
            var menuItemEdit = treeContextMenu.findItemByProperty("Text", "�༭");
            var menuItemSave = treeContextMenu.findItemByProperty("Text", "����");
            var menuItemCancel = treeContextMenu.findItemByProperty("Text", "ȡ��");
            var menuItemDelete = treeContextMenu.findItemByProperty("Text", "ɾ��");
            
            if(theItem == "����ͬ��" || theItem == "�༭"  || theItem=="�����¼�")
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
                case 'չ��':
                    contextDataNode.expand();
                    
                    break;
                    
                case '�۵�':
                    contextDataNode.collapse();
                    
                    break;
                    
                case 'չ��ȫ��':
                    tvPaperCategory.expandAll();
                    
                    break;
                    
                case '�۵�ȫ��':
                    tvPaperCategory.collapseAll();
                    
                    break;
                    
                case '�鿴':
                    if(tvPaperCategory.get_selectedNode() == contextDataNode)
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
                    var theBtn = document.getElementById(btnIds[1]);
                    imgBtns_onClick(theBtn);
                    
                    break;
                    
                case '����':
                    var theBtn;
                    if(theItem && theItem.value == "�༭")
                    {
                        theBtn = document.getElementById(btnIds[3]);
                    }
                    
                    if(theItem && (theItem.value == "����ͬ��" || theItem.value == "�����¼�"))
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
                    if(theItem && (theItem.value == "����ͬ��" || theItem.value == "�����¼�"))
                    {
                        theBtn = document.getElementById(btnIds[6]);
                    }
                    if(theForm && theBtn)
                    {
                        imgBtns_onClick(theBtn);
                    }  

                    break;
                    
                case 'ɾ��':
                    

                    if(tvPaperCategory.get_selectedNode().get_nodes().get_length() > 0)
                    {
                        alert("�ڵ����ӽڵ㣬����ɾ����");
                        return false;
                    }
                     
                    var theBtn = document.getElementById(btnIds[2]);
                    if(theForm && theBtn)
                    {                        
                        if(contextDataNode.get_nodes().get_length() > 0)
                        {
                            alert("�ڵ����ӽڵ㣬����ɾ����");
                            return false;
                        }
                        
                        imgBtns_onClick(theBtn);
                        // ɾ���ýڵ㲢ѡ���һ���ڵ�
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
            // ����������

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
        	                var node = tvPaperCategory.get_selectedNode();
        	                
        	                  if(node.get_value() == 1)
                       {
                        alert("�ÿ������Ϊ�̶���𣬲���ɾ����");
                        return ;
                        }

                    if(node.get_nodes().get_length() > 0)
                       {
                        alert("�ڵ����ӽڵ㣬����ɾ����");
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
                        �Ծ����</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        �Ծ����</div>
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
                        �Ծ����</div>
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
                                ��������...
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
                        �Ծ������ϸ��Ϣ</div>
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
                <ComponentArt:MenuItem Text="����ͬ��" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" /> 
             ��<ComponentArt:MenuItem Text="�����¼�" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" />
                <ComponentArt:MenuItem Text="�༭" look-lefticonurl="edit.gif" disabledlook-lefticonurl="edit_disabled.gif" />
                <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" />
                <ComponentArt:MenuItem Text="ɾ��" look-lefticonurl="delete.gif" disabledlook-lefticonurl="delete_disabled.gif" />
                <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" />
                <ComponentArt:MenuItem Text="����" look-lefticonurl="save.gif" disabledlook-lefticonurl="save_disabled.gif" />
                <ComponentArt:MenuItem Text="ȡ��" look-lefticonurl="cancel.gif" disabledlook-lefticonurl="cancel_disabled.gif" />
                <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" />
                <ComponentArt:MenuItem Text="չ��" look-lefticonurl="expand.gif" disabledlook-lefticonurl="expand_disabled.gif" />
                <ComponentArt:MenuItem Text="�۵�" look-lefticonurl="collapse.gif" disabledlook-lefticonurl="collapse_disabled.gif" />
                <ComponentArt:MenuItem Text="չ��ȫ��" look-lefticonurl="expand_all.gif" disabledlook-lefticonurl="expand_all_disabled.gif" />
                <ComponentArt:MenuItem Text="�۵�ȫ��" look-lefticonurl="collapse_all.gif" disabledlook-lefticonurl="collapse_all_disabled.gif" />
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
