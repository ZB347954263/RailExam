<%@ Page Language="C#" AutoEventWireup="True" Codebehind="Post.aspx.cs" Inherits="RailExamWebApp.Systems.Post" %>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>������λ</title>

    <script type="text/javascript">
        //��λ�������Ĳ˵� 
        function tvPost_onContextMenu(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem").value;
            var menuItemNewBrother = tvPostMenu.findItemByProperty("Text", "����ͬ��");
             var menuItemNewChild = tvPostMenu.findItemByProperty("Text", "�����¼�");
            var menuItemEdit = tvPostMenu.findItemByProperty("Text", "�༭");            
            var menuItemSave = tvPostMenu.findItemByProperty("Text", "����");
            var menuItemCancel = tvPostMenu.findItemByProperty("Text", "ȡ��");
            var menuItemDelete = tvPostMenu.findItemByProperty("Text", "ɾ��");
            var menuItemUp = tvPostMenu.findItemByProperty("Text", "����");
            var menuItemDown = tvPostMenu.findItemByProperty("Text", "����");           
            
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
            }            if(!tvPost.get_selectedNode() || 
                tvPost.get_selectedNode().get_id() != eventArgs.get_node().get_id())
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
                if(tvPost.get_selectedNode().get_id() == eventArgs.get_node().get_id()){
                    tvPostMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_node()); 
                }
                break; 
            }      
            
            return true;
        }
        
       //�˵������� 
        function tvPostMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();     
            var contextDataNode = menuItem.get_parentMenu().get_contextData(); 
            var theItem = document.getElementById("hfSelectedMenuItem");
            var theForm = window.frames["ifPostDetail"].document.forms[0];
 
            /*
            var feedback = '"' + menuItem.get_text() + '" command was issued on the "' 
                + contextDataNode.get_text() + '" node from "' + contextDataNode.get_value() + '".'; 
            alert(feedback);
            */
            
            switch(menuItem.get_text())
            {
                case 'չ��':
                    contextDataNode.expand();
                    
                    break;
                    
                case '�۵�':
                    contextDataNode.collapse();
                    
                    break;
                    
                case 'չ��ȫ��':
                    tvPost.expandAll();
                    
                    break;
                    
                case '�۵�ȫ��':
                    tvPost.collapseAll();
                    
                    break;
                    
                case '�鿴':
                    if(tvPost.get_selectedNode() == contextDataNode)
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
                    
                    if(theForm && theBtn && theItem)
                    {
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
                    if(theItem && (theItem.value == "����ͬ��" || theItem.value == "�����¼�") )
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
                    if(tvPostNodeMove(contextDataNode.get_value(), "UP") == "False")
                    {
                        alert("�������Ƹýڵ㣡");
                    }
                    else
                    {
                        tvPostChangeCallBack.callback()
                    }
                    
                    break;
                    
                case '����':
                    theItem.value = "����";
                    if(tvPostNodeMove(contextDataNode.get_value(), "DOWN") == "False")
                    {
                        alert("�������Ƹýڵ㣡");
                    }
                    else
                    {
                        tvPostChangeCallBack.callback()
                    }
                    
                    break;
                                    
                case 'ɾ��':
                    var theBtn = document.getElementById(btnIds[2]);
                    
                    if(theForm && theBtn)
                    {
                        imgBtns_onClick(theBtn);                        
                    }  
                                  
                    break;     
            } 
            
            return true; 
        }
        
       //չ����λ�����нڵ�
        function tvPost_onLoad(sender, eventArgs)
        {
            tvPost.expandAll();
        }
        
       //��λ��ѡ���¼�������
        function tvPost_onNodeSelect(sender, eventArgs)
        {
            // �鿴�ڵ�
            var node = eventArgs.get_node();            
            var theFrame = window.frames["ifPostDetail"];
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
            theFrame.location = "PostDetail.aspx?id=" + node.get_id()+"&depth="+node.get_depth();
            node.expand();
        }  
         
        //��λ�����ش����� 
        function tvPost_onLoad(sender, eventArgs)
        {
            if(tvPost.get_nodes().get_length() > 0)
            {
                //tvPost.expandAll();
            }            
        }
        
       //�ص���ɴ����� 
        function tvPost_onCallbackComplete(sender, eventArgs)
        {
        }
        
       //�ص���ɴ����� 
        function tvPostChangeCallBack_onCallbackComplete(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem");  
            if(theItem && theItem.value != "")
            {
                theItem.value == "";
            }  
                        
            //tvPost.expandAll();
            //tvPost.selectNodeById(sender.get_parameter());
            
            var id = document.getElementById("hfMaxID").value;
            if(id == "0")
            {
                 tvPost.selectNodeById(sender.get_parameter());
            }
            else
            {
                 //tvKnowledge.expandAll();
                 tvPost.selectNodeById(id);
                 tvPost.findNodeById(id).select();
                document.getElementById("hfMaxID").value = "0"; 
            }
            
            if(!tvPost.get_selectedNode())
            {
                tvPost.get_nodes().getNode(0).select();
            } 
        }
        
        var btnIds = new Array("fvPost_NewButton", "fvPost_EditButton", "fvPost_DeleteButton",
            "fvPost_UpdateButton", "fvPost_UpdateCancelButton",
            "fvPost_InsertButton", "fvPost_InsertCancelButton","fvPost_NewButtonBrother");
            
       //��ť����¼������� 
        function imgBtns_onClick(btn)
        {
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
                var node = tvPost.get_selectedNode();    	                

                if(node.get_nodes().get_length() > 0)
                {
                alert("�ڵ����ӽڵ㣬����ɾ����");
                return ;
                }
            }     
        	          
            var frame = window.frames["ifPostDetail"];
         	
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
              	        var node = tvPost.get_selectedNode();        
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
        
        function init()
        {
            if(form1.IsWuhanOnly.value == "True")
            {
                document.getElementById("fvPost_NewButtonBrother").style.display="none";
               document.getElementById("fvPost_NewButton").style.display="none";
               document.getElementById("fvPost_EditButton").style.display="none";
               document.getElementById("fvPost_DeleteButton").style.display="none"; 
            }
        }
    </script>

</head>
<body onload="init();">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        ϵͳ����</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        ������λ</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img id="fvPost_NewButtonBrother" onclick="imgBtns_onClick(this);" src="../Common/Image/addBrother.gif"
                        type="0" action="newBrother" alt="" />
                    <img id="fvPost_NewButton" onclick="imgBtns_onClick(this);" src="../Common/Image/addChild.gif"
                        type="0" action="newChild" alt="" />
                    <img id="fvPost_EditButton" onclick="imgBtns_onClick(this);" src="../Common/Image/edit.gif"
                        type="0" action="edit" alt="" />
                    <img id="fvPost_DeleteButton" onclick="imgBtns_onClick(this);" src="../Common/Image/delete.gif"
                        type="0" action="delete" alt="" />
                    <img id="fvPost_UpdateButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="1" action="update" style="display: none;" alt="" />
                    <img id="fvPost_UpdateCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="1" action="cancel" style="display: none;" alt="" />
                    <img id="fvPost_InsertButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="2" action="insert" style="display: none;" alt="" />
                    <img id="fvPost_InsertCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="2" action="cancel" style="display: none;" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        ������λ</div>
                    <div id="leftContent">
                        <ComponentArt:CallBack ID="tvPostChangeCallBack" runat="server" OnCallback="tvPostChangeCallBack_Callback">
                            <Content>
                                <ComponentArt:TreeView ID="tvPost" EnableViewState="true" runat="server">
                                    <ClientEvents>
                                        <ContextMenu EventHandler="tvPost_onContextMenu" />
                                        <NodeSelect EventHandler="tvPost_onNodeSelect" />
                                        <Load EventHandler="tvPost_onLoad" />
                                    </ClientEvents>
                                </ComponentArt:TreeView>
                            </Content>
                            <ClientEvents>
                                <CallbackComplete EventHandler="tvPostChangeCallBack_onCallbackComplete" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        ������λ��ϸ��Ϣ</div>
                    <div id="rightContent">
                        <iframe id="ifPostDetail" frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <ComponentArt:Menu ID="tvPostMenu" runat="server">
            <ClientEvents>
                <ItemSelect EventHandler="tvPostMenu_onItemSelect" />
            </ClientEvents>
            <Items>
                <ComponentArt:MenuItem Text="����ͬ��" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" />
                <ComponentArt:MenuItem Text="�����¼�" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" />
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
        <input type="hidden" name="IsWuhanOnly" value='<%=PrjPub.IsWuhanOnly()%>' />
    </form>

    <script type="text/javascript">
        if (tvPost && tvPost.get_nodes().get_length() > 0)
        {
            tvPost.get_nodes().getNode(0).select();
        }
        
        // Preload CSS-referenced images 
        (new Image()).src = '~/App_Themes/Default/images/Menu/group_background.gif';
        (new Image()).src = '~/App_Themes/Default/images/Menu/break_bg.gif';
    </script>

</body>
</html>
