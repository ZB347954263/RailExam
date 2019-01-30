<%@ Import Namespace="RailExamWebApp.Common.Class" %>

<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Codebehind="ItemList.aspx.cs" Inherits="RailExamWebApp.Item.ItemList" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>������Ϣ</title>

    <script src="../Common/JS/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <!-- DO NOT DELETE THIS TAG -->

    <script type="text/javascript">
    </script>

    <script type="text/javascript">
        //�Զ���showProperties(obj)���� 
        $.extend({
            $F : function(objId){
                return document.getElementById(objId);                
            },            
            getProperties : function(obj){	
                var ps;
                if(typeof(obj) != "object" || obj == "undefined")
                {
                    ps = "[" + obj + "]<BR/>";
                    
                    return ps;
                }
                
                try
                {
                    $.each(obj, function(n, v){
                        if(typeof(p) != "object")
                        {					
	                        ps += "[" + n + "=" + v + "]<BR/>";
                        }
                        else
                        {
	                        ps += "[" + n + "=" + $.getProperties(p) + "]<BR/>";
                        }
                    });
                }
                catch(e)
                {
                    ps = "Not DOM Objects��" + $.getProperties(e);
                }
                
                return ps;			
            },
            showProperties : function (obj){
	            var win = window.open();
	            
	            win.title = "Properties of " + obj;
	            win.document.write($.getProperties(obj));
	            win.document.close();
            }
        });
        
       //�鿴��ʽ���ڵ�ѡ���¼������� 
        function tvView_onNodeSelect(sender, eventArgs)
        {
            document.getElementById("hfRefresh").value = "";
            var node = eventArgs.get_node();
            var hfKnowledgeIdPath = $.$F("hfKnowledgeIdPath");
            var hfBookId = $.$F("hfBookId");
            var hfChapterId = $.$F("hfChapterId");
            var hfTrainTypeIdPath = $.$F("hfTrainTypeIdPath");
            var hfCategoryIdPath = $.$F("hfCategoryIdPath");
            
            // Reset hidden fields
            hfKnowledgeIdPath.value = "null";
            hfBookId.value = "-1";
            hfChapterId.value = "-1";
            hfTrainTypeIdPath.value = "null";
            hfCategoryIdPath.value = "null";
            
            // VIEW_KNOWLEDGE
            if(node && node.getProperty("isKnowledge") == "true")
            {
                hfKnowledgeIdPath.value = node.get_value();
                treeNodeSelectedCallBack.callback();
            }
            
            if(node && node.getProperty("isBook") == "true")
            {
                hfBookId.value = node.get_id();
                treeNodeSelectedCallBack.callback();
            }
            if(node && node.getProperty("isChapter") == "true")
            {
                var tempNode = node;
                while(true)
                {
                    if(tempNode.getProperty("isBook") == "true")
                    {
                        break;
                    }
                    else
                    {
                        tempNode = tempNode.get_parentNode();
                    }
                }
                hfBookId.value = tempNode.get_id();
                hfChapterId.value = node.get_id();
                treeNodeSelectedCallBack.callback();
            }
            
            // VIEW_TRAINTYPE
            if (node && node.getProperty("isTrainType") == "true")
            {
                hfTrainTypeIdPath.value = node.get_value();
                treeNodeSelectedCallBack.callback();
            }
            
            // VIEW_CATEGORY
            if (node && node.getProperty("isCategory") == "true")
            {
                hfCategoryIdPath.value = node.get_value();
                treeNodeSelectedCallBack.callback();
            }
            
            var node = eventArgs.get_node();
            if(node)
            {            
                node.expand();  
            }
        }
        
       //��ʾGrid�����Ĳ˵� 
        function itemsGrid_onContextMenu(sender, eventArgs)
        {
            var e = (eventArgs.get_event() == null) ? window.event : eventArgs.get_event();
            var contextMenuX = e.clientX ? e.clientX : e.x;
            var contextMenuY = e.clientY ? e.clientY : e.y;

            // Single select
            if(itemsGrid.getSelectedItems().length == 0)
            {
                itemsGrid.select(eventArgs.get_item()); 
            }
            else
            {
                var items = itemsMenu.get_items();
                
                if(itemsGrid.getSelectedItems().length < 2)
                {
                    items.getItemByProperty("Text", "�鿴").set_enabled(true);
                    items.getItemByProperty("Text", "����").set_enabled(true);
                    items.getItemByProperty("Text", "�༭").set_enabled(true);
                    
                    itemsGrid.select(eventArgs.get_item());
                }
                else
                {
                    items.getItemByProperty("Text", "�鿴").set_enabled(false);
                    items.getItemByProperty("Text", "����").set_enabled(false);
                    items.getItemByProperty("Text", "�༭").set_enabled(false);
                }
            }
                       
             var orgid=eventArgs.get_item().getMember('OrganizationId').get_text();   
             
             var flagUpdate=document.getElementById("HfUpdateRight").value;   
             var flagDelete=document.getElementById("HfDeleteRight").value;   
             var flagOrgID=document.getElementById("HfOrgId").value;  
                  var item = itemsMenu.get_items();
                
        	        if(flagUpdate=="True")
                      {  
                         item.getItemByProperty("Text", "����").set_enabled(true);                    
                         item.getItemByProperty("Text", "�༭").set_enabled(true);
                      }  
                      else
                      {                
                        
                       item.getItemByProperty("Text", "����").set_enabled(false);
                        
                       item.getItemByProperty("Text", "�༭").set_enabled(false);
                      }                     
                      
                         
                       if(flagDelete=="True")
                      {                     
                     item.getItemByProperty("Text", "ɾ��").set_enabled(true);
                      }  
                      else
                      {
                      item.getItemByProperty("Text", "ɾ��").set_enabled(false);
                      }                        
                      
           
            itemsMenu.showContextMenu(contextMenuX, contextMenuY);
            itemsMenu.set_contextData(eventArgs.get_item());
            //showProperties(eventArgs.get_event());
        }
        
       //�˵������� 
        function itemsMenu_onItemSelect(sender, eventArgs)
        {
            var menuItemSelectecd = eventArgs.get_item();
            var gridItemSelected = menuItemSelectecd.get_parentMenu().get_contextData();
        	
            var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-570)*.5;    
          
            
            //showProperties(itemsMenu.get_contextData());
            switch(menuItemSelectecd.get_text())
            {
                case "�鿴":
                {
                    var viewWindow = window.open('ItemDetail.aspx?mode=readonly&id=' + gridItemSelected.GetProperty("Id"),
                        'ItemDetail','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
                    viewWindow.focus();
                    break;
                }
                case "�༭":
                {
                	 editItem(gridItemSelected.getMember("ItemId").get_value(),gridItemSelected.getMember("OrganizationId").get_value(),gridItemSelected.getMember("TypeId").get_value(),gridItemSelected.getMember("Authors").get_value()); 
                    /*var editWindow = window.open('ItemDetail.aspx?mode=edit&&id=' + gridItemSelected.GetProperty("Id"),
                        'ItemDetail','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
                    editWindow.focus();*/
                    break;
                }
                case "����":
                {
                    var bid = $.$F("hfBookId").value;
                    var cid = $.$F("hfChapterId").value;
                    var ctid = $.$F("hfCategoryIdPath").value;
                    
                    if(leftContent.style.display != "none")
                    {
                        if(!bid || !cid || bid < 1)
                        {
                            alert("��ѡ���½ڣ�");
                            return;
                        }
                        var addWindow = window.open('ChooseTheWay.aspx?mode=insert&bid=' + bid + "&cid=" + cid,
                            'ItemDetail',' top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
                        addWindow.focus(); 
                    }
                    else if(divCategory.style.display != "none")
                    {
                        if(ctid == -1 || !ctid)
                        {
                            alert("��ѡ�������࣡");
                            return;
                        }
                        alert("�ݲ�֧��������");
                    }
                    else
                    {
                        alert("δ֪����");
                    }
                    
                    break;
                }
                case "ɾ��":
                {                 
                    deleteItem(gridItemSelected.getMember('ItemId').get_value(),gridItemSelected.getMember("OrganizationId").get_value(),gridItemSelected.getMember("Authors").get_value()); 
//                    if(items.length == 0)
//                    {
//                        alert("��ѡ��Ҫɾ�������⣡");
//                    }
//                    else
//                    {
//                    
//                       if(! confirm("��ȷ��Ҫɾ����"))
//                    {
//                        return false;
//                    }
//                        var ids = "";
//                        
//                        for(var i = 0; i < items.length; i ++)
//                        {
//                            ids += items[i].GetProperty("Id") + "|";
//                        }
//                        if (ids.length > 0)
//                        {
//                            ids = ids.substring(0, ids.length-1);
//                        }
//                        
//                        // ComponentArt Callback Method
//                        if (DeleteItem(ids) > 0)
//                        {
//                            alert("ɾ���ɹ���");
//                            
//                            treeNodeSelectedCallBack.callback();
//                        }
//                        else
//                        {
//                            alert("ɾ��ʧ�ܣ�");
//                        }
//                    }
                    
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
        
       //�鿴��ʽ�����¼������� 
        function ddlView_onChange()
        {
            var ddlView = $.$F("ddlView");

            switch(ddlView.selectedIndex)
            {
                case 0:
                {
                    ddlViewChangeCallBack.callback("VIEW_KNOWLEDGE");
                    break;
                }
                case 1:
                {
                    ddlViewChangeCallBack.callback("VIEW_KNOWLEDGE_ALL");
                    break;
                }
                case 2:
                {
                    ddlViewChangeCallBack.callback("VIEW_TRAINTYPE");
                    break;
                }
                case 3:
                {
                    ddlViewChangeCallBack.callback("VIEW_CATEGORY");
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
        
       //�鿴���� 
        function viewItem(id)
        {  
            var viewWindow = window.open('ItemDetail.aspx?mode=readonly&&id=' + id,
                'ItemDetail',' top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
        }
        
       //�������� 
        function addItem()
        {
            var ddlView = $.$F("ddlView");
            var selectedNode = tvView.get_selectedNode();
            var hfBookId = $.$F("hfBookId");
            var hfChapterId = $.$F("hfChapterId");

            var flagUpdate=document.getElementById("HfUpdateRight").value;               
                  
	        if(flagUpdate=="False")
              {                     
              
                 alert("��û��Ȩ��ʹ�øò�����");
                 return;
              }                    

            if(ddlView.selectedIndex != 0)
            {
                alert("�ݲ�֧��������");
                return;
            }
            
            if(selectedNode == null || selectedNode.getProperty("isChapter") != "true" 
                || hfBookId.value < 1 || hfChapterId.value < 1)
            {
                alert("��ѡ���½ڣ�");
                return;
            }
            
            var scrW=window.screen.width;
            var scrH=window.screen.height;
            var left=scrW/2-150;
            var top=scrH/2-30;

            var addWindow = window.open('ChooseTheWay.aspx?mode=insert&bid=' + hfBookId.value + "&cid=" + hfChapterId.value,
                'ChooseTheWay','left='+left+'px,top='+top+'px, width=300px,height=150px,resizable=no,status=no,scrollbars=no');
            addWindow.focus(); 
            
//        	var ret = window.showModalDialog('ChooseTheWay.aspx?mode=insert&bid=' + hfBookId.value + "&cid=" + hfChapterId.value,'','help:no; status:no;dialogWidth:300px;dialogHeight:150px;');  

//            var addWindow = window.open('ItemDetail.aspx?mode=insert&bid=' + hfBookId.value + "&cid=" + hfChapterId.value,
//                'ItemDetail',' top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
//            addWindow.focus(); 
        }
        
       //�޸����� 
        function editItem(id,orgid,typeID,authors)
        {  
            var flagOrgID=document.getElementById("HfOrgId").value;   
          	var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("��û��Ȩ��ʹ�øò�����");
                return;
            }  
        	
        	var SuitRange = form1.SuitRange.value;
        	var employeeId = form1.loginerID.value;
			if(SuitRange == 0) 
			{
				if (authors == "-1" && flagOrgID != orgid)
				{
					alert("��û��Ȩ��ʹ�øò�����");
					return;
				}

				if (authors != "-1" && employeeId != authors)
				{
					alert("��û��Ȩ��ʹ�øò�����");
					return;
				}
			}
        	
            if(typeID<4 || typeID>5)
            {            
                var editWindow = window.open('ItemDetail.aspx?mode=edit&id=' + id,
                'ItemDetail',' top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
                editWindow.focus(); 
            }
            else
            {
                var editWindow = showCommonDialogFull('/RailExamBao/Item/ItemClozeDetail.aspx?mode=edit&id=' + id+'&typeID='+typeID,
                ' help:no; status:no; dialogWidth:1000px;dialogHeight:700px;scroll:no;');
                //editWindow.focus();
            	form1.refresh.value = "true";
            	form1.submit();
            }
        }
        
       //ɾ������ 
        function deleteItem(id,orgid,authors)
        {
//        	alert(id);
//        	alert(orgid);
//        	alert(authors);
            var flagOrgID=document.getElementById("HfOrgId").value;   
            var flagDelete=document.getElementById("HfDeleteRight").value;   
                  
        	if(flagDelete=="False")
            {                     
                alert("��û��Ȩ��ʹ�øò�����");
                return;
            }
        	
        	var SuitRange = form1.SuitRange.value;
        	var employeeId = form1.loginerID.value;
			if(SuitRange == 0) 
			{
				if (authors == "-1" && flagOrgID != orgid)
				{
					alert("��û��Ȩ��ʹ�øò�����");
					return;
				}

				if (authors != "-1" && employeeId != authors)
				{
					alert("��û��Ȩ��ʹ�øò�����");
					return;
				}
			}
            
            itemsGrid.select(itemsGrid.getItemFromClientId("0 " + id), false);
            if(! confirm("��ȷ��Ҫɾ���ü�¼��"))
            {
                return;
            }
            //itemsGrid.deleteSelected();
            //showProperties(itemsGrid.getSelectedItems()[0]);
            itemsGrid.deleteItem(itemsGrid.getSelectedItems()[0]);
            //treeNodeSelectedCallBack.callback();
        }
        
        function newButton_onClick()
        {
            addItem();
        }
        
       //��ʾ�����ز�ѯ����
        var _fHide = true;
        function toggleButton_onClick()
        {
            var h = $("#rightHead").height();
            var res = $("#rightHead").slideToggle("slow");
            if(_fHide)
            {
                $("#rightContent").height($("#rightContent").height() - h);
                _fHide = !_fHide;
            }
            else
            {
                $("#rightContent").height($("#rightContent").height() + h);
                _fHide = !_fHide;
            }
        }
        
       //��ѯ��ť����¼�������
        function searchButton_onClick()
        {
            var hfKnowledgeIdPath = $.$F("hfKnowledgeIdPath");
            var hfBookId = $.$F("hfBookId");
            var hfChapterId = $.$F("hfChapterId");
            var hfTrainTypeIdPath = $.$F("hfTrainTypeIdPath");
            var hfCategoryIdPath = $.$F("hfCategoryIdPath");    
            var hftxtitemScore = $.$F("hftxtitemScore");
            var hfContent = $.$F("hfContent");    
            var hfFlag = $.$F("hfFlag");  
          
            
            if(hfKnowledgeIdPath.value.length = 0)
            {
                hfKnowledgeIdPath.value = 'null';
            }         
            if(hfBookId.value.length = 0)
            {
                hfBookId.value = '-1';
            }         
            if(hfChapterId.value.length = 0)
            {
                hfChapterId.value = '-1';
            }         
            
            hfFlag.value='-1';
            
            if(hfTrainTypeIdPath.value.length = 0)
            {
                hfTrainTypeIdPath.value = 'null';
            }         
            if(hfCategoryIdPath.value.length = 0)
            {
                hfCategoryIdPath.value = 'null';
            }         
            
              if($.$F("txtitemScore").value.length > 0)
            {
                hftxtitemScore.value = $.$F("txtitemScore").value;
            }  
            

            hfContent.value = $.$F("txtContent").value;
            
            $.$F("hfIsSearchCommand").value = "true";
            //treeNodeSelectedCallBack.callback();
        }       
      
        
         function EnableButton_onClick()
        {
            var hfKnowledgeIdPath = $.$F("hfKnowledgeIdPath");
            var hfBookId = $.$F("hfBookId");
            var hfChapterId = $.$F("hfChapterId");
            var hfTrainTypeIdPath = $.$F("hfTrainTypeIdPath");
            var hfCategoryIdPath = $.$F("hfCategoryIdPath");    
            var hftxtitemScore = $.$F("hftxtitemScore");    
           var hfFlag = $.$F("hfFlag");  
          
            
            if(hfKnowledgeIdPath.value.length = 0)
            {
                hfKnowledgeIdPath.value = 'null';
            }         
            if(hfBookId.value.length = 0)
            {
                hfBookId.value = '-1';
            }         
            if(hfChapterId.value.length = 0)
            {
                hfChapterId.value = '-1';
            }         
            if(hfTrainTypeIdPath.value.length = 0)
            {
                hfTrainTypeIdPath.value = 'null';
            }         
            if(hfCategoryIdPath.value.length = 0)
            {
                hfCategoryIdPath.value = 'null';
            }         
            
            hfFlag.value='0';
            
              if($.$F("txtitemScore").value.length > 0)
            {
                hftxtitemScore.value = $.$F("txtitemScore").value;
            }             
            
            $.$F("hfIsSearchCommand").value = "true";
            //treeNodeSelectedCallBack.callback();
        }
        
        
        
       //��ʼ��ҳ�� 
        $(document.body).ready(function(){
            var h = $("#rightHead").height();
            $("#rightContent").height($("#rightContent").height() + h);
        });
        
        function ShowInput()
        {
          	var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("��û��Ȩ��ʹ�øò�����");
                return;
            }  
            
            var selectedNode = tvView.get_selectedNode();
            var hfBookId = $.$F("hfBookId");
            var hfChapterId = $.$F("hfChapterId");
//            alert(form1.IsWuhan.value);
//            alert(hfBookId.value);
//            alert(hfChapterId.value);

             var ret = showCommonDialog("/RailExamBao/Item/ItemInput.aspx?bid=" + hfBookId.value + "&cid=" + hfChapterId.value,'dialogWidth:850px;dialogHeight:650px;');
             treeNodeSelectedCallBack.callback();
        }
        
        function addBookItem()
        {
            var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("��û��Ȩ��ʹ�øò�����");
                return;
            }  
            
            var selectedNode = tvView.get_selectedNode();
            var hfBookId = $.$F("hfBookId");
            var hfChapterId = $.$F("hfChapterId");
            //alert(selectedNode.getProperty("isBook"));
            if(selectedNode != null && selectedNode.getProperty("isBook") == "true")
            {
                var ret = showCommonDialog("/RailExamBao/Item/ItemBookChapter.aspx?id="+hfBookId.value,'dialogWidth:800px;dialogHeight:620px;');
            }
            else
            {
                var ret = showCommonDialog("/RailExamBao/Item/ItemBookDetail.aspx",'dialogWidth:600px;dialogHeight:600px;');
            }
            form1.submit();
        }
        
        function exportItem()
        {
          	var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("��û��Ȩ��ʹ�øò�����");
                return;
            }  
            
            var selectedNode = tvView.get_selectedNode();
            var hfBookId = $.$F("hfBookId");
            var hfChapterId = $.$F("hfChapterId");

            if(selectedNode == null ||  selectedNode.getProperty("isKnowledge") == "true" 
                || hfBookId.value < 1 )
            {
                alert("�����������ѡ��̲Ļ�̲��½ڣ�");
                return;
            }    
        	        	
//        	 var   cleft;   
//          var   ctop;   
//          cleft=(screen.availWidth-260)*.5;   
//          ctop=(screen.availHeight-50)*.5; 
//        	var ret = window.open("/RailExamBao/Item/SelectExportType.aspx?bid=" + hfBookId.value + "&cid=" + hfChapterId.value,
//                     'SelectExportType', 'Width=260px; Height=50px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
        	showCommonDialog("/RailExamBao/Item/SelectExportType.aspx?bid=" + hfBookId.value + "&cid=" + hfChapterId.value, 'dialogWidth:260px;dialogHeight:100px;');
        }
        
        function openPage()
	    {
            var url="../Common/SelectPost.aspx?src=book";

            var selectedPost = window.showModalDialog(url, 
                '', 'help:no; status:no; dialogWidth:300px;dialogHeight:620px;scroll:no;');
            
            if(! selectedPost)
            {
                return;
            } 
            
            var id=selectedPost.split('|')[0];
            var text=selectedPost.split('|')[1]; 
            
            document.getElementById("txtPost").value=text;
            document.getElementById("hfPostID").value=id;

	    	__doPostBack("btnQuery");
	    }
        
        function init() {
//        	 var roleID = form1.roleID.value;
//             if(roleID != 1) {
//             	document.getElementById("btnOutputWord").style.display = "none";
//             } else {
//                document.getElementById("btnOutputWord").style.display = "";
//             }
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
                        ������</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        �������</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <asp:Button runat="server" ID="btnAddBookItem" CssClass="button" Text="�������" OnClientClick="addBookItem()" />
                    <input type="button" value="��������" class="button" onclick="ShowInput()" />
                    <input name="btnOutputWord" id="btnOutputWord" onclick="exportItem()" class="button"
                        value="��������" type="button"  />
                    <input type="button" id="newButton" value="��   ��" class="button" onclick="newButton_onClick();" />
                    <input type="button" id="toggleButton" value="��   ѯ" class="button" onclick="toggleButton_onClick();" />
                    <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="ȷ   ��" OnClick="btnQuery_Click" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        �鿴��ʽ
                        <select id="ddlView" onchange="ddlView_onChange();">
                            <option>��֪ʶ��ϵ</option>
                            <option>��֪ʶ��ϵ��ȫ����</option>
                        </select>
                    </div>
                    <div id="leftContent">
                        <ComponentArt:CallBack ID="ddlViewChangeCallBack" runat="server" PostState="true"
                            OnCallback="ddlViewChangeCallBack_Callback">
                            <Content>
                                <ComponentArt:TreeView ID="tvView" runat="server">
                                    <ClientEvents>
                                        <NodeSelect EventHandler="tvView_onNodeSelect" />
                                        <%--<ContextMenu EventHandler="tvView_onContextMenu" />--%>
                                    </ClientEvents>
                                </ComponentArt:TreeView>
                            </Content>
                        </ComponentArt:CallBack>
                    </div>
                </div>
                <div id="right">
                    <div class="queryPost">
                    &nbsp;ְ&nbsp;&nbsp;��
                    <asp:TextBox ID="txtPost" runat="server" Enabled="false"></asp:TextBox>&nbsp;
                    <input type="button" id="btnSelectPost" class="button" value="ѡ��ְ��" onclick="openPage()" /></div>
                    <div id="rightHead" style="display: none; white-space: nowrap; text-align: left;">
                        ����<asp:TextBox ID="txtContent"  runat="server" />
                        <asp:DropDownList ID="ddlItemType" runat="server" DataSourceID="odsItemType" DataValueField="ItemTypeId"
                            DataTextField="TypeName">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlItemDifficulty" runat="server" DataSourceID="odsItemDifficulty"
                            DataTextField="DifficultyName" DataValueField="ItemDifficultyId">
                        </asp:DropDownList>
                        ����<input id="txtitemScore" type="text" size="5" />
                        <asp:DropDownList ID="ddlStatus" runat="server" DataSourceID="odsItemStatus" DataTextField="StatusName"
                            DataValueField="ItemStatusId">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlUsage" runat="server">
                            <asp:ListItem Text="-������;-" Value="-1"></asp:ListItem>
                            <asp:ListItem Text="��ϰ�Ϳ���" Value="0"></asp:ListItem>
                            <asp:ListItem Text="������" Value="1"></asp:ListItem>
                        </asp:DropDownList>
                        <input type="submit" id="searchButton" class="buttonSearch" title="��ѯ��������������" value="ȷ  ��"
                            onclick="searchButton_onClick();" />
                        <input type="submit" id="Submit1" class="buttonEnableLong" title="��ѯȫ����������" value="��ѯȫ����������"
                            onclick="EnableButton_onClick();" />
                    </div>
                    <div id="rightContentWithNoHead">
                        <ComponentArt:CallBack ID="treeNodeSelectedCallBack" runat="server" PostState="true"
                            OnCallback="treeNodeSelectedCallBack_Callback" Debug="false">
                            <Content>
                                <ComponentArt:Grid ID="itemsGrid" runat="server" DataSourceID="odsItems" OnDataBinding="itemsGrid_DataBinding"
                                    OnDeleteCommand="itemsGrid_DeleteCommand" OnPageIndexChanged="itemsGrid_PageIndexChanged"
                                    OnSortCommand="itemsGrid_SortCommand" RunningMode="Server" ManualPaging="true"
                                    Debug="false" PageSize="18">
                                    <ClientEvents>
                                        <ContextMenu EventHandler="itemsGrid_onContextMenu" />
                                        <%--<PageIndexChange EventHandler="itemsGrid_onPageIndexChange" />--%>
                                    </ClientEvents>
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="ItemId">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="ItemId" Visible="false" />
                                                <%--<ComponentArt:GridColumn DataField="OrganizationName" HeadingText="��֯����" />--%>
                                                <ComponentArt:GridColumn DataField="TypeName" HeadingText="����" Width="40" />
                                                <ComponentArt:GridColumn DataField="DifficultyName" HeadingText="�Ѷ�" Width="40" />
                                                <%--<ComponentArt:GridColumn DataField="Score" HeadingText="��ֵ" />--%>
                                                <ComponentArt:GridColumn DataField="Content" HeadingText="����" Align="left" Width="410" />
                                                <ComponentArt:GridColumn DataField="Score" HeadingText="����" Width="40" Visible="false" />
                                                <ComponentArt:GridColumn DataField="StatusName" HeadingText="״̬" Width="40" />
                                                <ComponentArt:GridColumn DataField="OrganizationId" HeadingText="��֯����" Visible="false" />
                                                <ComponentArt:GridColumn DataField="TypeId" HeadingText="TypeId" Visible="false" />
                                                <ComponentArt:GridColumn DataField="Authors" HeadingText="authors" Visible="false"/>                                                
                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="����"
                                                    Width="60" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit">
                                            <a onclick="viewItem('##DataItem.getMember('ItemId').get_value()##');" href="#">
                                                <img alt="�鿴" border="0" src="../Common/Image/edit_col_see.gif" /></a> <a onclick="editItem(##DataItem.getMember('ItemId').get_value()##,##DataItem.getMember('OrganizationId').get_value()##,##DataItem.getMember('TypeId').get_value()##,##DataItem.getMember('Authors').get_value()##);"
                                                    href="#">
                                                    <img alt="�༭" border="0" src="../Common/Image/edit_col_edit.gif" /></a>
                                            <a onclick="deleteItem(##DataItem.getMember('ItemId').get_value()##,##DataItem.getMember('OrganizationId').get_value()##,##DataItem.getMember('Authors').get_value()##);"
                                                href="#">
                                                <img alt="ɾ��" border="0" src="../Common/Image/edit_col_delete.gif" /></a>
                                        </ComponentArt:ClientTemplate>
                                    </ClientTemplates>
                                </ComponentArt:Grid>
                            </Content>
                        </ComponentArt:CallBack>
                    </div>
                </div>
            </div>
        </div>
        <%--<asp:HiddenField ID="hfItemId" runat="server" Value="-1" />--%>
        <asp:HiddenField ID="hfContent" runat="server" />
        <asp:HiddenField ID="hfKnowledgeIdPath" runat="server" />
        <asp:HiddenField ID="hfBookId" runat="server" />
        <asp:HiddenField ID="hfFlag" runat="server" />
        <asp:HiddenField ID="hfChapterId" runat="server" />
        <asp:HiddenField ID="hfTrainTypeIdPath" runat="server" />
        <asp:HiddenField ID="hfCategoryIdPath" runat="server" />
        <asp:HiddenField ID="hftxtitemScore" runat="server" />
        <asp:HiddenField ID="hfIsSearchCommand" runat="server" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfOrgId" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="hfRailSystemId" runat="server" />
        <%--<asp:TextBox ID="hfKnowledgeIdPath" runat="server" style="display: none;" />--%>
        <asp:ObjectDataSource ID="odsItems" runat="server" DataObjectTypeName="RailExam.Model.Item"
            DeleteMethod="DeleteItem" InsertMethod="AddItem" SelectMethod="GetItems" TypeName="RailExam.BLL.ItemBLL"
            UpdateMethod="UpdateItem" OnObjectCreated="odsItems_ObjectCreated" >
            <SelectParameters>
                <asp:ControlParameter ControlID="hfContent" Name="content" PropertyName="Value" Type="String"
                    DefaultValue="null" />
                <asp:ControlParameter ControlID="hfKnowledgeIdPath" Name="knowledgeIdPath" PropertyName="Value"
                    Type="String" DefaultValue="null" />
                <asp:ControlParameter ControlID="hfBookId" DefaultValue="-1" Name="bookId" PropertyName="Value"
                    Type="Int32" />
                <asp:ControlParameter ControlID="hfChapterId" DefaultValue="-1" Name="chapterId"
                    PropertyName="Value" Type="Int32" />
                <asp:ControlParameter ControlID="hfTrainTypeIdPath" Name="trainTypeIdPath" PropertyName="Value"
                    Type="String" DefaultValue="null" />
                <asp:ControlParameter ControlID="hfCategoryIdPath" Name="categoryIdPath" PropertyName="Value"
                    Type="String" DefaultValue="null" />
                <asp:ControlParameter ControlID="ddlItemType" DefaultValue="-1" Name="itemTypeId"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="ddlItemDifficulty" DefaultValue="-1" Name="itemDifficultyId"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="hftxtitemScore" Name="itemScore" PropertyName="Value"
                    Type="Int32" DefaultValue="-1" />
                <asp:ControlParameter ControlID="ddlStatus" DefaultValue="-1" Name="StatusId" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="ddlUsage" DefaultValue="-1" Name="usageId" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="itemsGrid" DefaultValue="-1" Name="currentPageIndex"
                    PropertyName="CurrentPageIndex" Type="Int32" />
                <asp:ControlParameter ControlID="itemsGrid" DefaultValue="-1" Name="pageSize" PropertyName="PageSize"
                    Type="Int32" />
                <asp:ControlParameter ControlID="itemsGrid" DefaultValue="a.bookid,a.item_id" Name="orderBy"
                    PropertyName="Sort" Type="String" />
                <asp:ControlParameter ControlID="hfFlag" DefaultValue="-1" Name="flag" PropertyName="Value"
                    Type="Int32" />
                <asp:ControlParameter ControlID="HfOrgId" DefaultValue="-1" Name="orgID" PropertyName="Value"
                    Type="Int32" />
                 <asp:ControlParameter ControlID="hfRailSystemId" DefaultValue="0" Name="railSystemId" PropertyName="Value"
                    Type="Int32" /> 
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSourceOutput" runat="server" DataObjectTypeName="RailExam.Model.Item"
            SelectMethod="GetOutputItems" TypeName="RailExam.BLL.ItemBLL">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfKnowledgeIdPath" Name="knowledgeIdPath" PropertyName="Value"
                    Type="String" DefaultValue="null" />
                <asp:ControlParameter ControlID="hfBookId" DefaultValue="-1" Name="bookId" PropertyName="Value"
                    Type="Int32" />
                <asp:ControlParameter ControlID="hfChapterId" DefaultValue="-1" Name="chapterId"
                    PropertyName="Value" Type="Int32" />
                <asp:ControlParameter ControlID="hfTrainTypeIdPath" Name="trainTypeIdPath" PropertyName="Value"
                    Type="String" DefaultValue="null" />
                <asp:ControlParameter ControlID="hfCategoryIdPath" Name="categoryIdPath" PropertyName="Value"
                    Type="String" DefaultValue="null" />
                <asp:ControlParameter ControlID="ddlItemType" DefaultValue="-1" Name="itemTypeId"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="ddlItemDifficulty" DefaultValue="-1" Name="itemDifficultyId"
                    PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="hftxtitemScore" Name="itemScore" PropertyName="Value"
                    Type="Int32" DefaultValue="-1" />
                <asp:ControlParameter ControlID="ddlStatus" DefaultValue="-1" Name="StatusId" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="ddlUsage" DefaultValue="-1" Name="usageId" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:ControlParameter ControlID="hfFlag" DefaultValue="-1" Name="flag" PropertyName="Value"
                    Type="Int32" />
                <asp:ControlParameter ControlID="HfOrgId" DefaultValue="-1" Name="orgID" PropertyName="Value"
                    Type="Int32" />
                   <asp:ControlParameter ControlID="hfRailSystemId" DefaultValue="0" Name="railSystemId" PropertyName="Value"
                    Type="Int32" />  
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsItemType" runat="server" SelectMethod="GetItemTypes"
            TypeName="RailExam.BLL.ItemTypeBLL" DataObjectTypeName="RailExam.Model.ItemType">
            <SelectParameters>
                <asp:Parameter Name="bForSearchUse" Type="boolean" DefaultValue="true" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsItemStatus" runat="server" SelectMethod="GetItemStatuss"
            TypeName="RailExam.BLL.ItemStatusBLL" DataObjectTypeName="RailExam.Model.ItemStatus">
            <SelectParameters>
                <asp:Parameter Name="bForSearchUse" Type="boolean" DefaultValue="true" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsItemDifficulty" runat="server" SelectMethod="GetItemDifficulties"
            TypeName="RailExam.BLL.ItemDifficultyBLL" DataObjectTypeName="RailExam.Model.ItemDifficulty">
            <SelectParameters>
                <asp:Parameter Name="bForSearchUse" Type="boolean" DefaultValue="true" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <ComponentArt:Menu ID="itemsMenu" runat="server" EnableViewState="false">
            <ClientEvents>
                <ItemSelect EventHandler="itemsMenu_onItemSelect" />
            </ClientEvents>
            <Items>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="view_disabled.gif" look-lefticonurl="view.gif"
                    Text="�鿴">
                </ComponentArt:MenuItem>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="new_disabled.gif" look-lefticonurl="new.gif"
                    Text="����">
                </ComponentArt:MenuItem>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="edit_disabled.gif" look-lefticonurl="edit.gif"
                    Text="�༭">
                </ComponentArt:MenuItem>
                <ComponentArt:MenuItem LookId="BreakItem">
                </ComponentArt:MenuItem>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="delete_disabled.gif" look-lefticonurl="delete.gif"
                    Text="ɾ��">
                </ComponentArt:MenuItem>
            </Items>
        </ComponentArt:Menu>
        <input type="hidden" name="SuitRange" value='<%=PrjPub.CurrentLoginUser.SuitRange %>' />
        <input type="hidden" name="loginerID" value='<%=PrjPub.CurrentLoginUser.EmployeeID %>' />
        <input type="hidden" name="roleID" value='<%=PrjPub.CurrentLoginUser.RoleID %>' />
        <asp:HiddenField ID="hfRefresh" runat="server" />
        <input type="hidden" name="refresh"/>
         <asp:HiddenField ID="hfPostID" runat="server" />
    </form>
</body>
</html>
