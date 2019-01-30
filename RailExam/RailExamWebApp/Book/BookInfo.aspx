<%@ Page Language="C#" AutoEventWireup="True" EnableEventValidation="false" Codebehind="BookInfo.aspx.cs"
    Inherits="RailExamWebApp.Book.BookInfo" %>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�̲���Ϣ</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
        function GetIndex(id)
        {
       		var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("��û��Ȩ��ʹ�øò�����");
                return;
            }   
            form1.Index.value = id;
            form1.submit();
        }
       
       function DelBook(id,orgid)
       {
            var flagDelete=document.getElementById("HfDeleteRight").value;
           
       	     if(flagDelete=="False")
            {                     
                alert("��û��Ȩ��ʹ�øò�����");
                return;
            }   
               
            var SuitRange= form1.SuitRange.value;
            var noworgid = document.getElementById("HfOrgId").value;
            if(SuitRange == 0 && noworgid != orgid)
            {
                alert("��û��Ȩ��ɾ���ñ��̲ģ�");
                return;
            } 
         
            if(! confirm("��ȷ��Ҫɾ���ý̲������ɾ����ý̲ĵ��������⽫�����ã�"))
            {
                return false; 
            }
       	 
           if(form1.UpdateRecord.value =="1")
           {
                 var ret = showCommonDialog("/RailExamBao/Book/BookChapterUpdate.aspx?BookID="+ id + "&ChapterID=0&Object=delbook",'dialogWidth:600px;dialogHeight:400px;');
                if(ret == "true")
                {
                    form1.DeleteID.value = id;
                    form1.submit();
                    form1.DeleteID.value = "";
                } 
            }
            else
            {
                form1.DeleteID.value = id;
                form1.submit();
                form1.DeleteID.value = "";
            }  
       } 
        
        function AddRecord()
		{
		    var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("��û��Ȩ��ʹ�øò�����");
                return;
            }   
                      
            var ret = window.open("BookDetail.aspx?Mode=Add",'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
		    ret.focus();
		    if(ret == "true")
		    {
			    form1.Refresh.value = ret;
			    form1.submit();
			    form1.Refresh.value = "";
		    }
		}
		
		function EditBook(id,orgid,authors)
		{
       		var flagUpdate=document.getElementById("HfUpdateRight").value;  
			var employeeId=document.getElementById("hfEmployeeID").value;  
                  
	        if(flagUpdate=="False")
            {                     
                alert("��û��Ȩ��ʹ�øò�����");
                return;
            }

			var SuitRange = form1.SuitRange.value;
            var noworgid = document.getElementById("HfOrgId").value;

			if(SuitRange == 0) 
			{
				if (authors == "-1" && noworgid != orgid)
				{
					alert("��û��Ȩ���޸ĸñ��̲ģ�");
					return;
				}

				if (authors != "-1" && employeeId != authors)
				{
					alert("��û��Ȩ���޸ĸñ��̲ģ�");
					return;
				}
			}

			var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 

		    var ret = window.open('BookDetail.aspx?mode=Edit&id='+id,'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
		    ret.focus();
		    if(ret == "true")
		    {
			    form1.Refresh.value = ret;
			    form1.submit();
			    form1.Refresh.value = "";
		    } 
	    }
		
		function OpenIndex(id)
		{
//		    var re = window.open('../Online/Book/' + id + '/index.html','index','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
//		    re.focus();
		   	var re = window.open('BookCount.aspx?id='+id,'index','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
		    re.focus();
 
		}
		
		function lookbook(id)
		{
		    var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
            
		        var ret = window.open('BookDetail.aspx?mode=ReadOnly&id='+id,'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
			ret.focus();
		}
        
        function ManageChapter(id,orgid)
        {
         
            var flagUpdate=document.getElementById("HfUpdateRight").value;   
             var flagOrgID=document.getElementById("HfOrgId").value;  
                  
	        if(flagUpdate=="False")
              {                     
                alert("��û��Ȩ��ʹ�øò�����");
                return;
              }                          
                      
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;
            var ret = window.open("BookChapter.aspx?id="+id+"&Mode=Edit","BookChapter",'Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
            ret.focus();
        }

        function DownloadBook(bookId,orgid,authors) {
       		var flagUpdate=document.getElementById("HfUpdateRight").value;  
			var employeeId=document.getElementById("hfEmployeeID").value;  
                  
	        if(flagUpdate=="False")
            {                     
                alert("��û��Ȩ��ʹ�øò�����");
                return;
            }

			var SuitRange = form1.SuitRange.value;
            var noworgid = document.getElementById("HfOrgId").value;

			if(SuitRange == 0) 
			{
//				alert(noworgid);
//				alert(orgid);
				if (authors == "-1" && noworgid != orgid)
				{
					alert("��û��Ȩ�����ظñ��̲ģ�");
					return;
				}

				if (authors != "-1" && employeeId != authors)
				{
					alert("��û��Ȩ�����ظñ��̲ģ�");
					return;
				}
			}                      
                      
            form1.RefreshDown.value = bookId;
            form1.submit();
            form1.RefreshDown.value = "";
        }
	
        function Grid1_onContextMenu(sender, eventArgs)
        {
            var menuItemEdit = ContextMenu.findItemByProperty("Text", "�޸�");
             var menuItemAdd = ContextMenu.findItemByProperty("Text", "����");
            var menuItemUp = ContextMenu.findItemByProperty("Text", "����");
            var menuItemDown = ContextMenu.findItemByProperty("Text", "����");
            var menuItemDelete = ContextMenu.findItemByProperty("Text", "ɾ��");
            var menuItemOrder = ContextMenu.findItemByProperty("Text", "�������");
            
            var orgid=eventArgs.get_item().getMember('publishOrg').get_text();          
          
            var flagUpdate=document.getElementById("HfUpdateRight").value;
            var flagDelete=document.getElementById("HfDeleteRight").value;   
            var flagOrgID=document.getElementById("HfOrgId").value;  
                  
	        if(flagUpdate=="True")
            {                     
               menuItemEdit.set_enabled(true); 
               menuItemAdd.set_enabled(true); 
               menuItemUp.set_enabled(true);
               menuItemDown.set_enabled(true);
               menuItemOrder.set_enabled(true);
            }
            else
            {
                menuItemEdit.set_enabled(false); 
                menuItemAdd.set_enabled(false); 
                menuItemUp.set_enabled(false);
                menuItemDown.set_enabled(false);
               menuItemOrder.set_enabled(false); 
           }
            
            if(flagDelete=="True")
            {                     
               menuItemDelete.set_enabled(true); 
            }
            else
            {
                
               menuItemDelete.set_enabled(false);
            }

        	var depth = 1;
        	
        	if( window.parent.tvKnowledge.get_selectedNode())
        	{
        		depth = window.parent.tvKnowledge.get_selectedNode().get_nodes().get_length();
        	}
        	//alert(depth);
            var search= window.location.search;
            if(form1.SuitRange.value == 0 || search.substring(search.indexOf('id=')+3) =='0' || search.substring(search.indexOf('id1=')+4) =='0' || depth>0)
            {
                menuItemUp.set_enabled(false);
                menuItemDown.set_enabled(false);
                menuItemOrder.set_enabled(false); 
           }
           
            switch(eventArgs.get_item().getMember('bookName').get_text())
            {
                default:
                    ContextMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_item());
                    return false;
                    break;
            }
        }
      
        function ContextMenu_onItemSelect(sender, eventArgs)
        {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
            
            var menuItem = eventArgs.get_item();
            var contextDataNode = menuItem.get_parentMenu().get_contextData();
            var search= window.location.search;
            
            switch(menuItem.get_text())
            {
                case '�鿴':
                    var ret = window.open('BookDetail.aspx?mode=ReadOnly&id='+contextDataNode.getMember('bookId').get_value(),'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    ret.focus();
				    
				    break;
				
				case '����':
                    AddRecord();
                    break;
				    
                case '�޸�':
                	var orgid = contextDataNode.getMember('publishOrg').get_value();
                	var authors = contextDataNode.getMember('authors').get_value();
                	var bookid = contextDataNode.getMember('bookId').get_value();
                	EditBook(bookid, orgid, authors);
//                    var SuitRange= form1.SuitRange.value;
//                    var noworgid = document.getElementById("HfOrgId").value;
//                    if(SuitRange == 0 && noworgid != contextDataNode.getMember('publishOrg').get_value())
//                    {
//                        alert("��û��Ȩ���޸ĸñ��̲ģ�");
//                        return;
//                    } 
//                    var ret = window.open('BookDetail.aspx?mode=Edit&id='+contextDataNode.getMember('bookId').get_value(),'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//				    ret.focus();
//				    if(ret == "true")
//				    {
//					    form1.Refresh.value = ret;
//					    form1.submit();
//					    form1.Refresh.value = "";
//				    }
				    
                    break;
                case 'ɾ��':
                    DelBook(contextDataNode.getMember('bookId').get_value(),contextDataNode.getMember('publishOrg').get_value());
                    break;
               case '����':
                    if(contextDataNode.getMember('OrderIndex').get_value() == 1)
                    {
                        alert('�ý̲��Ѿ����ڵ�һλ��');
                       return false; 
                    }
                    form1.UpID.value =contextDataNode.getMember('bookId').get_value();
					form1.submit();
					form1.UpID.value = "";
					break;
               case '����':
                    if(contextDataNode.getMember('OrderIndex').get_value() == Grid1.get_recordCount())
                    {
                        alert('�ý̲��Ѿ��������һλ��');
                       return false; 
                    }
                    form1.DownID.value =contextDataNode.getMember('bookId').get_value();
					form1.submit();
				    form1.DownID.value = "";
                    break;
                case '�������':
                    var ret ; 
                    if(search.indexOf('id=') >0)
                    {
                          ret = showCommonDialog("/RailExamBao/Book/SetOrderIndex.aspx?BookID="+ contextDataNode.getMember('bookId').get_value() + "&NowOrder="+contextDataNode.getMember('OrderIndex').get_value() +"&MaxOrder="+Grid1.get_recordCount(),'dialogWidth:180px;dialogHeight:100px;');
                    }
                    
                    if(search.indexOf('id1=') >0)
                    {
                          var str = search.substring(search.indexOf('id1=')+4);
                          ret = showCommonDialog("/RailExamBao/Book/SetOrderIndex.aspx?BookID="+ contextDataNode.getMember('bookId').get_value() + "&TrainTypeID="+str + "&NowOrder="+contextDataNode.getMember('OrderIndex').get_value() +"&MaxOrder="+Grid1.get_recordCount(),'dialogWidth:180px;dialogHeight:100px;');
                    }
                    if(ret == "true")
                    {
                        form1.Refresh.value = ret;
                        form1.submit();
                        form1.Refresh.value = "";
                    } 
                    break; 
            }
            
            return true; 
        }
       
        function eWebEditorPopUp(id)
        {
            var re = window.open("../ewebeditor/asp/ShowEditor.asp?BookID="+id+"&Type=Edit" ,'ShowEditor','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
            re.focus();
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

	    	window.parent.document.getElementById("hfPostID").value=id;

	    	__doPostBack("btnQuery");
	    }
	    
	     function clearPost() 
        {
    	    window.parent.document.getElementById("hfPostID").value = "";
    		document.getElementById("txtPost").value = "";
    		document.getElementById("hfPostID").value = "";
    		__doPostBack("btnQuery");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="gridPage">
            <div class="queryPost">
                &nbsp;ְ&nbsp;&nbsp;��
                <asp:TextBox ID="txtPost" runat="server" Enabled="false"></asp:TextBox>&nbsp;
                <input type="button" id="btnSelectPost" class="button" value="ѡ��ְ��" onclick="openPage()" />&nbsp;&nbsp;<input
                    type="button" class="button" value="��  ��" onclick="clearPost();" />
                <div id="mainContent">
                </div>
            <div id="query" style="display: none">
                �̲�����
                <asp:TextBox ID="txtBookName" runat="server" Width="10%"></asp:TextBox>
                ����
                <asp:TextBox ID="txtAuthors" runat="server" Width="10%"></asp:TextBox>
                �ؼ���
                <asp:TextBox ID="txtKeyWords" runat="server" Width="10%"></asp:TextBox>
                <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="ȷ   ��" OnClick="btnQuery_Click" />
            </div>
            <div id="grid">
                <ComponentArt:Grid ID="Grid1" runat="server" PageSize="20" Width="98%" OnLoad="Grid1_Load">
                    <ClientEvents>
                        <ContextMenu EventHandler="Grid1_onContextMenu" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="bookId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="bookId" Visible="false" />
                                <ComponentArt:GridColumn DataField="OrderIndex" HeadingText="���" />
                                <ComponentArt:GridColumn DataField="bookName" HeadingText="�̲�����" Align="Left" />
                                <ComponentArt:GridColumn DataField="knowledgeId" Visible="false" />
                                <ComponentArt:GridColumn DataField="trainTypeId" Visible="false" />
                                <ComponentArt:GridColumn DataField="bookNo" HeadingText="���" Visible="false" />
                                <ComponentArt:GridColumn DataField="publishOrgName" HeadingText="���Ƶ�λ" />
                                <ComponentArt:GridColumn DataField="AuthorsName" HeadingText="������" />
                                <ComponentArt:GridColumn DataField="authors" HeadingText="" Visible="false" />
                                <ComponentArt:GridColumn DataField="publishOrg" HeadingText="���Ƶ�λ" Visible="false" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="����" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTEdit">
                            <a onclick="DownloadBook(##DataItem.getMember('bookId').get_value()##,##DataItem.getMember('publishOrg').get_value()##,##DataItem.getMember('authors').get_value()##)"
                                title="���ؽ̲�" href="#"><b>����</b></a> &nbsp; <a onclick="EditBook(##DataItem.getMember('bookId').get_value()##,##DataItem.getMember('publishOrg').get_value()##,##DataItem.getMember('authors').get_value()##)"
                                    title="�޸Ľ̲�" href="#"><b>�޸�</b></a> &nbsp; <a onclick="DelBook(##DataItem.getMember('bookId').get_value()##,##DataItem.getMember('publishOrg').get_value()##)"
                                        title="ɾ���̲�" href="#"><b>ɾ��</b></a>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
            </div>
        </div>
        <ComponentArt:Menu ID="ContextMenu" ContextMenu="Custom" runat="server" EnableViewState="true">
            <ClientEvents>
                <ItemSelect EventHandler="ContextMenu_onItemSelect" />
            </ClientEvents>
            <Items>
                <ComponentArt:MenuItem Text="�鿴" look-lefticonurl="view.gif" disabledLook-LeftIconUrl="view_disabled.gif" />
                <ComponentArt:MenuItem Text="�޸�" look-lefticonurl="edit.gif" disabledlook-lefticonurl="edit_disabled.gif" />
                <ComponentArt:MenuItem Text="����" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" />
                <ComponentArt:MenuItem Text="ɾ��" look-lefticonurl="delete.gif" disabledlook-lefticonurl="delete_disabled.gif" />
                <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" />
                <ComponentArt:MenuItem Text="����" look-lefticonurl="move_up.gif" disabledlook-lefticonurl="move_up_disabled.gif" />
                <ComponentArt:MenuItem Text="����" look-lefticonurl="move_down.gif" disabledlook-lefticonurl="move_down_disabled.gif" />
                <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" />
                <ComponentArt:MenuItem Text="�������" look-lefticonurl="edit.gif" disabledlook-lefticonurl="edit_disabled.gif" />
            </Items>
        </ComponentArt:Menu>
        <input type="hidden" name="Refresh" />
        <input type="hidden" name="RefreshDown" />
        <input type="hidden" name="RefreshCover" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfOrgId" runat="server" />
        <input type="hidden" name="Index" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="UpID" />
        <input type="hidden" name="DownID" />
        <input type="hidden" name="UpdateRecord" value='<%=PrjPub.FillUpdateRecord %>' />
        <input type="hidden" name="SuitRange" value='<%=PrjPub.CurrentLoginUser.SuitRange %>' />
        <asp:HiddenField ID="hfPostID" runat="server" />
        <asp:HiddenField ID="hfEmployeeID" runat="server" />
    </form>
</body>
</html>
