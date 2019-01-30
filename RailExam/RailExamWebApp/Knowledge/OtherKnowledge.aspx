<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="OtherKnowledge.aspx.cs" Inherits="RailExamWebApp.Knowledge.OtherKnowledge" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>����֪ʶ</title>
    <script type="text/javascript">
        function tvOtherKnowledge_onContextMenu(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem").value;
            var menuItemNewBrother = tvOtherKnowledgeMenu.findItemByProperty("Text", "����ͬ��");
             var menuItemNewChild = tvOtherKnowledgeMenu.findItemByProperty("Text", "�����¼�");
            var menuItemEdit = tvOtherKnowledgeMenu.findItemByProperty("Text", "�༭");
            var menuItemSave = tvOtherKnowledgeMenu.findItemByProperty("Text", "����");
            var menuItemCancel = tvOtherKnowledgeMenu.findItemByProperty("Text", "ȡ��");
            var menuItemDelete = tvOtherKnowledgeMenu.findItemByProperty("Text", "ɾ��");
            var menuItemUp = tvOtherKnowledgeMenu.findItemByProperty("Text", "����");
            var menuItemDown = tvOtherKnowledgeMenu.findItemByProperty("Text", "����");
            
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

            if(! tvOtherKnowledge.get_selectedNode()
              || tvOtherKnowledge.get_selectedNode().get_id() != eventArgs.get_node().get_id())
            {
                eventArgs.get_node().select();
            }

            switch(eventArgs.get_node().get_text())
            {
                default:
                    if(tvOtherKnowledge.get_selectedNode().get_id() == eventArgs.get_node().get_id())
                    {
                        tvOtherKnowledgeMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_node());
                    }
                    
                    break;
            }
            return true;
        }
        
        function tvOtherKnowledgeMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();
            var contextDataNode = menuItem.get_parentMenu().get_contextData();
            var theItem = document.getElementById("hfSelectedMenuItem");
            var theForm = window.frames["ifOtherKnowledgeDetail"].document.forms[0];
           
            switch(menuItem.get_text())
            {
                case 'չ��':
                    contextDataNode.expand();
                    
                    break;
                    
                case '�۵�':
                    contextDataNode.collapse();
                    
                    break;
                    
                case 'չ��ȫ��':
                    tvOtherKnowledge.expandAll();
                    
                    break;
                    
                case '�۵�ȫ��':
                    tvOtherKnowledge.collapseAll();
                    
                    break;
                    
                case '�鿴':
                    if(tvOtherKnowledge.get_selectedNode() == contextDataNode)
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
                    if(tvOtherKnowledgeNodeMove(contextDataNode.get_value(), "UP") == "False")
                    {
                        alert("�������Ƹýڵ㣡");
                    }
                    else
                    {
                        tvOtherKnowledgeChangeCallBack.callback()
                    }
                    
                    break;
                    
                case '����':
                    theItem.value = "����";
                    if(tvOtherKnowledgeNodeMove(contextDataNode.get_value(), "DOWN") == "False")
                    {
                        alert("�������Ƹýڵ㣡");
                    }
                    else
                    {
                        tvOtherKnowledgeChangeCallBack.callback()
                    }
                    
                    break;
                
                case 'ɾ��':
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
                        //tvOtherKnowledge.get_selectedNode().remove();
                        //if(tvOtherKnowledge.get_nodes().get_length() > 0)
                        //{
                        //    tvOtherKnowledge.get_nodes().getNode(0).select();
                        //}
                    }
                    
                    break;
            }
            
            return true;
        }
        
//        function tvOtherKnowledge_onLoad(sender, eventArgs)
//        {
//            tvTrainType.expandAll();
//        }

//        function tvTrainType_onNodeRename(sender, eventArgs)
//        {
//            // ���ڵ�������
//            tvTrainTypeRenameCallBack.callback('1', 'Rename', 'NewName', 'Result');
//        }
        
        function tvOtherKnowledge_onNodeSelect(sender, eventArgs)
        {
            // �鿴�ڵ�
            var node = eventArgs.get_node();
            var theFrame = window.frames["ifOtherKnowledgeDetail"];
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
            theFrame.location = "OtherKnowledgeDetail.aspx?id=" + node.get_id();
        }
        
        function tvOtherKnowledgeChangeCallBack_onCallbackComplete(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem");
            if(theItem && theItem.value != "")
            {
                theItem.value == "";
            }
        
//            tvOtherKnowledge.expandAll();
            tvOtherKnowledge.selectNodeById(sender.get_parameter());
        }
         
        function tvOtherKnowledgeChangeCallBack_onLoad(sender, eventArgs)
        {
            // ����������

            if(tvOtherKnowledge.get_nodes().get_length() > 0)
            {
//                tvOtherKnowledge.expandAll();
            }
        }
        
        var btnIds = new Array("fvOtherKnowledge_NewButton", "fvOtherKnowledge_EditButton", "fvOtherKnowledge_DeleteButton",
            "fvOtherKnowledge_UpdateButton", "fvOtherKnowledge_UpdateCancelButton",
            "fvOtherKnowledge_InsertButton", "fvOtherKnowledge_InsertCancelButton","fvOtherKnowledge_NewButtonBrother");
            
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
        	                var node = tvOtherKnowledge.get_selectedNode();    	                
        	                 
                    if(node.get_nodes().get_length() > 0)
                       {
                        alert("�ڵ����ӽڵ㣬����ɾ����");
                        return ;
                       }
        	                
        	          }       
        	          
         	var frame = window.frames["ifOtherKnowledgeDetail"];
         	
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
              	        var node = tvOtherKnowledge.get_selectedNode();        
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
                        ֪ʶ����</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        ����֪ʶ</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                   <img id="fvOtherKnowledge_NewButtonBrother" onclick="imgBtns_onClick(this);"  src="../Common/Image/addBrother.gif"
                        type="0" action="newBrother" alt="" />
                    <img id="fvOtherKnowledge_NewButton" onclick="imgBtns_onClick(this);" src="../Common/Image/addChild.gif"
                        type="0" action="newChild" alt="" />
                    <img id="fvOtherKnowledge_EditButton" onclick="imgBtns_onClick(this);" src="../Common/Image/edit.gif"
                        type="0" action="edit" alt="" />
                    <img id="fvOtherKnowledge_DeleteButton" onclick="imgBtns_onClick(this);" src="../Common/Image/delete.gif"
                        type="0" action="delete" alt="" />
                    <img id="fvOtherKnowledge_UpdateButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="1" action="update" style="display: none;" alt="" />
                    <img id="fvOtherKnowledge_UpdateCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="1" action="cancel" style="display: none;" alt="" />
                    <img id="fvOtherKnowledge_InsertButton" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="2" action="insert" style="display: none;" alt="" />
                    <img id="fvOtherKnowledge_InsertCancelButton" onclick="imgBtns_onClick(this);" src="../Common/Image/cancel.gif"
                        type="2" action="cancel" style="display: none;" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        ����֪ʶ</div>
                    <div id="leftContent">
                        <ComponentArt:CallBack ID="tvOtherKnowledgeChangeCallBack" runat="server" OnCallback="tvOtherKnowledgeChangeCallBack_Callback">
                            <Content>
                                <ComponentArt:TreeView ID="tvOtherKnowledge" runat="server" EnableViewState="true" DefaultTarget="iframe1">
                                    <ClientEvents>
                                        <NodeSelect EventHandler="tvOtherKnowledge_onNodeSelect" />
                                        <ContextMenu EventHandler="tvOtherKnowledge_onContextMenu" />
                                        <%--<NodeRename EventHandler="tvOtherKnowledge_onNodeRename" />
                                        <Load EventHandler="tvOtherKnowledge_onLoad" />--%>
                                    </ClientEvents>
                                </ComponentArt:TreeView>
                            </Content>
                            <%--<LoadingPanelClientTemplate>
                                ��������...
                            </LoadingPanelClientTemplate>--%>
                            <ClientEvents>
                                <CallbackComplete EventHandler="tvOtherKnowledgeChangeCallBack_onCallbackComplete" />
                                <Load EventHandler="tvOtherKnowledgeChangeCallBack_onLoad" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        ����֪ʶ��ϸ��Ϣ</div>
                    <div id="rightContent">
                        <iframe id="ifOtherKnowledgeDetail" frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <ComponentArt:Menu ID="tvOtherKnowledgeMenu" runat="server" EnableViewState="false">
            <ClientEvents>
                <ItemSelect EventHandler="tvOtherKnowledgeMenu_onItemSelect" />
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
    </form>
    <script type="text/javascript">
        if(tvOtherKnowledge && tvOtherKnowledge.get_nodes().get_length() > 0)
        {
            tvOtherKnowledge.get_nodes().getNode(0).select();
        }
    </script>
</body>
</html>
