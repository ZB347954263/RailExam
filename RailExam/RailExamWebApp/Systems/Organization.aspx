<%@ Page Language="C#" AutoEventWireup="True" Codebehind="Organization.aspx.cs" Inherits="RailExamWebApp.Systems.Organization" %>
<%@ Import namespace="RailExamWebApp.Common.Class"%>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>��֯����</title>
    <script type="text/javascript">
        //��֯�����������Ĳ˵� 
        function tvOrganization_onContextMenu(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem").value;
            var menuItemNewBrother = tvOrganizationMenu.findItemByProperty("Text", "����ͬ��");
             var menuItemNewChild = tvOrganizationMenu.findItemByProperty("Text", "�����¼�");
            var menuItemEdit = tvOrganizationMenu.findItemByProperty("Text", "�༭");
            var menuItemSave = tvOrganizationMenu.findItemByProperty("Text", "����");
            var menuItemCancel = tvOrganizationMenu.findItemByProperty("Text", "ȡ��");
            var menuItemDelete = tvOrganizationMenu.findItemByProperty("Text", "ɾ��");
            var menuItemUp = tvOrganizationMenu.findItemByProperty("Text", "����");
            var menuItemDown = tvOrganizationMenu.findItemByProperty("Text", "����");
            
            if(theItem == "����ͬ��" || theItem == "�༭"  || theItem=="�����¼�")
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
        
       //�˵������� 
        function tvOrganizationMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();
            var contextDataNode = menuItem.get_parentMenu().get_contextData();
            var theItem = document.getElementById("hfSelectedMenuItem");
            var theForm = window.frames["ifOrganizationDetail"].document.forms[0];
           
            switch(menuItem.get_text())
            {
                case 'չ��':
                    contextDataNode.expand();
                    break;
                    
                case '�۵�':
                    contextDataNode.collapse();
                    break;
                    
                case 'չ��ȫ��':
                    tvOrganization.expandAll();
                    break;
                    
                case '�۵�ȫ��':
                    tvOrganization.collapseAll();
                    break;
                    
                case '�鿴':
                    if(tvOrganization.get_selectedNode() == contextDataNode)
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
                    if(theItem  && (theItem.value == "����ͬ��" || theItem.value == "�����¼�") )
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
                    if(tvOrganizationNodeMove(contextDataNode.get_value(), "UP") == "False")
                    {
                        alert("�������Ƹýڵ㣡");
                    }
                    else
                    {
                    	tvOrganizationChangeCallBack.callback(contextDataNode.get_value(), "Up");
                    }
                    
                    break;
                    
                case '����':
                    theItem.value = "����";
                    if(tvOrganizationNodeMove(contextDataNode.get_value(), "DOWN") == "False")
                    {
                        alert("�������Ƹýڵ㣡");
                    }
                    else
                    {
                       tvOrganizationChangeCallBack.callback(contextDataNode.get_value(), "Down");
                    }
                    
                    break;
                
                case 'ɾ��':
                    var theBtn = document.getElementById(btnIds[2]);
                    if(theForm&&theBtn)
                    {
                        imgBtns_onClick(theBtn);
                    }
                    break;
            }
            
            return true;
        }
        
       //��֯�����ڵ�ѡ���¼������� 
        function tvOrganization_onNodeSelect(sender, eventArgs)
        {
            // �鿴�ڵ�
            var node = eventArgs.get_node();
            var theFrame = window.frames["ifOrganizationDetail"];
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
          
         //�ص���ɴ�����
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
        
       //��֯���������ش����� 
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
        
       //��ť����¼�������  
        function imgBtns_onClick(btn)
        {
            if(btn.action == "newBrother")
            {
                var node = tvOrganization.get_selectedNode();    	                
                
               if(node)
               {
                 if(node.get_id() == 0)
                 {
                        alert("����������[�񻪰�����·���Ź�˾]ͬ������֯������");
                       return; 
                 } 
                  
                 if(form1.hfSuitRange.value !=1 && node.get_depth == 2)
                 {
                        alert("վ�β���������վ��ͬ������֯������");
                       return;                  
                  }  
               } 
            } 
            
            
            if(btn.action == "newBrother" || btn.action == "newChild")
            {
                if(!tvOrganization.get_selectedNode())
                {
                    alert("��ѡ��һ����֯�����ڵ㣡");
                    return; 
                }
            }
            
            var flagupdate=document.getElementById("HfUpdateRight").value;
            if(flagupdate=="False"&&(btn.action == "edit"|| btn.action == "newChild" || btn.action == "newBrother"))
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
                var node = tvOrganization.get_selectedNode();    	                

                if(node.get_nodes().get_length() > 0)
                {
                alert("�ڵ����ӽڵ㣬����ɾ����");
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
<body >
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                     <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        ϵͳ����</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        ��֯����</div>
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
                        ��֯����</div>
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
                        ��֯������ϸ��Ϣ</div>
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
                <ComponentArt:MenuItem Text="����ͬ��" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" /> 
             ��<ComponentArt:MenuItem Text="�����¼�" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" />
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
