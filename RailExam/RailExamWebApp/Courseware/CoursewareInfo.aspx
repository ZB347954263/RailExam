<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="CoursewareInfo.aspx.cs" Inherits="RailExamWebApp.Courseware.CoursewareInfo" %>
<%@ Import Namespace="RailExamWebApp.Common.Class"%>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�μ���Ϣ</title>
      <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>  
    <script type="text/javascript">              
        function AddRecord()
		{
		    var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
            
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
            var flagOrgID=document.getElementById("HfOrgId").value;  
                  
	        if(flagUpdate=="False")
            {                     
                alert("��û��Ȩ��ʹ�øò�����");
                return;
            }    
            var node =window.parent.tvCourseware.get_selectedNode();    
            var ret = window.open("CoursewareDetail.aspx?coursewareTypeID="+node.get_id(),'CoursewareDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
		    if(ret == "true")
		    {
			    form1.Refresh.value = ret;
			    form1.submit();
			    form1.Refresh.value = "";
		    }
		}
         
        function Grid1_onContextMenu(sender, eventArgs)
        {
            var menuItemEdit = ContextMenu.findItemByProperty("Text", "�޸�");
            var menuItemAdd = ContextMenu.findItemByProperty("Text", "�½�");
            var menuItemUp = ContextMenu.findItemByProperty("Text", "����");
            var menuItemDown = ContextMenu.findItemByProperty("Text", "����");
            var menuItemDelete = ContextMenu.findItemByProperty("Text", "ɾ��");
            var menuItemOrder = ContextMenu.findItemByProperty("Text", "�������");
            
            var orgid=eventArgs.get_item().getMember('ProvideOrg').get_text();          
          
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
            
            var search= window.location.search;
            if(form1.SuitRange.value=="0" || search.substring(search.indexOf('id=')+3) =='0')
            {
                menuItemUp.set_enabled(false);
                menuItemDown.set_enabled(false);
                menuItemOrder.set_enabled(false); 
           }
            switch(eventArgs.get_item().getMember('CoursewareName').get_text()){                
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
                    var ret = window.open('CoursewareDetail.aspx?mode=ReadOnly&id='+contextDataNode.getMember('CoursewareID').get_value(),'CoursewareDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    ret.focus();
                    break;
                    
                case '�޸�':
                    EditCourse(contextDataNode.getMember('CoursewareID').get_value(),contextDataNode.getMember('ProvideOrg').get_value());
                    break;
            
                case '�½�':
                    AddRecord();
                    break;
                
                case 'ɾ��':
                   DelCourse(contextDataNode.getMember('CoursewareID').get_value(),contextDataNode.getMember('ProvideOrg').get_value());
                    
                    break;
                case '����':
                    if(contextDataNode.getMember('OrderIndex').get_value() == 1)
                    {
                        alert('�ý̲��Ѿ����ڵ�һλ��');
                       return false; 
                    }
                    form1.UpID.value =contextDataNode.getMember('CoursewareID').get_value();
					form1.submit();
					form1.UpID.value = "";
					break;
               case '����':
                    if(contextDataNode.getMember('OrderIndex').get_value() == Grid1.get_recordCount())
                    {
                        alert('�ý̲��Ѿ��������һλ��');
                       return false; 
                    }
                    form1.DownID.value =contextDataNode.getMember('CoursewareID').get_value();
					form1.submit();
				    form1.DownID.value = "";
                    break;
                case '�������':
                    var ret ; 
                    if(search.indexOf('type=Courseware') >0)
                    {
                          ret = showCommonDialog("/RailExamBao/Courseware/SetOrderIndex.aspx?CoursewareID="+ contextDataNode.getMember('CoursewareID').get_value() + "&NowOrder="+contextDataNode.getMember('OrderIndex').get_value() +"&MaxOrder="+Grid1.get_recordCount(),'dialogWidth:180px;dialogHeight:100px;');
                    }
                    
                    if(search.indexOf('type=TrainType') >0)
                    {
                          var str = search.substring(search.indexOf('id=')+3);
                          ret = showCommonDialog("/RailExamBao/Courseware/SetOrderIndex.aspx?CoursewareID="+ contextDataNode.getMember('CoursewareID').get_value() + "&TrainTypeID="+str + "&NowOrder="+contextDataNode.getMember('OrderIndex').get_value() +"&MaxOrder="+Grid1.get_recordCount(),'dialogWidth:180px;dialogHeight:100px;');
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
       
       function EditCourse(id,orgID)
       {
       		 var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("��û��Ȩ��ʹ�øò�����");
                return;
            } 
            
            var SuitRange= form1.SuitRange.value;
            var noworgid = document.getElementById("HfOrgId").value;
            if(SuitRange == 0 && noworgid !=orgID)
            {
                alert("��û��Ȩ���޸ĸÿμ���");
                return;
            } 
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;
            
             var ret = window.open('CoursewareDetail.aspx?id='+id,'CoursewareDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
			    if(ret == "true")
			    {
				    form1.Refresh.value = ret;
				    form1.submit();
				    form1.Refresh.value = "";
			    }
       } 
       
      function DelCourse(id,orgID)
     {
            var flagDelete=document.getElementById("HfDeleteRight").value;   
                  
	        if(flagDelete=="False")
            {                     
                alert("��û��Ȩ��ʹ�øò�����");
                return;
            }   
            
            var SuitRange= form1.SuitRange.value;
            var noworgid = document.getElementById("HfOrgId").value;
            if(SuitRange == 0 && noworgid !=orgID)
            {
                alert("��û��Ȩ��ɾ���ÿμ���");
                return;
            } 
                        
           if(! confirm("��ȷ��Ҫɾ���ÿμ���")){
                return false; 
            }
            
            form1.DeleteID.value = id;
            form1.submit();
            form1.DeleteID.value = "";
       }  
     
        function OpenIndex(id)
		{
		    var re = window.open("CoursewareCount.aspx?id="+id,'ViewCourseware','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
		    re.focus();
		}
		
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="gridPage">
            <div id="query" style="display: none;">
                &nbsp;�μ�����
                <asp:TextBox ID="txtCoursewareName" runat="server" Width="10%"></asp:TextBox>
                ������
                <asp:TextBox ID="txtAuthors" runat="server" Width="10%"></asp:TextBox>
                �ؼ���
                <asp:TextBox ID="txtKeyWords" runat="server" Width="10%"></asp:TextBox>
                <%--<asp:ImageButton runat="server" ID="btnQuery" OnClick="btnQuery_Click" ImageUrl="~/Common/Image/confirm.gif" />--%>
                <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="ȷ  ��" OnClick="btnQuery_Click" />
            </div>
            <div id="grid">
                <ComponentArt:Grid ID="Grid1" runat="server" PageSize="20" Width="97%" OnLoad="Grid1_Load">
                    <ClientEvents>
                        <ContextMenu EventHandler="Grid1_onContextMenu" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="CoursewareID">
                            <Columns>
                                <ComponentArt:GridColumn DataField="CoursewareID" Visible="false" />
                               <ComponentArt:GridColumn DataField="OrderIndex" HeadingText="���"  AllowSorting="False"/>
                                <ComponentArt:GridColumn DataField="CoursewareName" HeadingText="�μ�����" Align="Left" />
                                <ComponentArt:GridColumn DataField="CoursewareTypeID" Visible="false" />
                                <ComponentArt:GridColumn DataField="TrainTypeID" Visible="false" />
                                <ComponentArt:GridColumn DataField="ProvideOrgName" HeadingText="�����ṩ��λ" />
                                <ComponentArt:GridColumn DataField="ProvideOrg" HeadingText="�����ṩ��λID" Visible="false" />
                                <ComponentArt:GridColumn DataField="PublishDate" FormatString="yyyy-MM-dd" HeadingText="�������" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="����" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTEdit">
                            <a  onclick="EditCourse(##DataItem.getMember('CoursewareID').get_value()## ,##DataItem.getMember('ProvideOrg').get_value()## )" title="�޸Ŀμ�������Ϣ"  href="#"><b>�޸�</b></a> &nbsp;
                           <a onclick="DelCourse(##DataItem.getMember('CoursewareID').get_value()## ,##DataItem.getMember('ProvideOrg').get_value()## )" title="ɾ���μ�" href="#"><b>ɾ��</b></a>
                            </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
            </div>
        </div>
        <ComponentArt:Menu ID="ContextMenu" ContextMenu="Custom" runat="server" Orientation="Vertical">
            <ClientEvents>
                <ItemSelect EventHandler="ContextMenu_onItemSelect" />
            </ClientEvents>
            <Items>
                <ComponentArt:MenuItem Text="�鿴" look-lefticonurl="view.gif" disabledLook-LeftIconUrl="view_disabled.gif" />
                <ComponentArt:MenuItem Text="�޸�" look-lefticonurl="edit.gif" disabledlook-lefticonurl="edit_disabled.gif" />
                <ComponentArt:MenuItem Text="�½�" look-lefticonurl="new.gif" disabledlook-lefticonurl="new_disabled.gif" />
                <ComponentArt:MenuItem Text="ɾ��" look-lefticonurl="delete.gif" disabledlook-lefticonurl="delete_disabled.gif" />
                 <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" />
                <ComponentArt:MenuItem Text="����" look-lefticonurl="move_up.gif" disabledlook-lefticonurl="move_up_disabled.gif" />
                <ComponentArt:MenuItem Text="����" look-lefticonurl="move_down.gif" disabledlook-lefticonurl="move_down_disabled.gif" />
                <ComponentArt:MenuItem LookId="BreakItem" AutoPostBackOnSelect="false" />
                 <ComponentArt:MenuItem Text="�������" look-lefticonurl="edit.gif" disabledlook-lefticonurl="edit_disabled.gif" />
          </Items>
        </ComponentArt:Menu>
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <input type="hidden" id="NewButton" onclick="return AddRecord();" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
         <input type="hidden" name="UpID" />
          <input type="hidden" name="DownID" />        
          <asp:HiddenField ID="HfOrgId" runat="server" />
           <input type="hidden" name="SuitRange" value ='<%=PrjPub.CurrentLoginUser.SuitRange %>' /> 
    </form>
</body>
</html>
