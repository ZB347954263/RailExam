<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainPlan.aspx.cs" Inherits="RailExamWebApp.Train.TrainPlan" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>��ѵ�ƻ�</title>
    <script type="text/javascript">              
      
        function ManageChapter(id)
        {
            var re= window.open("TrainPlanEdit.aspx?id="+id,"TrainPlanEdit"," Width=700px; Height=600px,status=false,resizable=no",true);		
            re.focus();  
        }
                   
        function AddRecord()
        {			
            var re= window.open("TrainPlanEdit.aspx",'TrainPlanEdit',' Width=700px; Height=600px,status=false,resizable=yes',true);		
            re.focus();  				    
        }
			
        function ShowContextMenu1(sender, eventArgs)
        {
            switch(eventArgs.get_item().getMember('TrainName').get_text())
            {                
                default:
                    ContextMenu1.showContextMenu(eventArgs.get_event(), eventArgs.get_item()); 
                    return false; 
                    break; 
            }      
        }    
 
               
       function ContextMenu_onItemSelect(sender, eventArgs)
       {           
            var menuItem = eventArgs.get_item(); 
            var contextDataNode = menuItem.get_parentMenu().get_contextData();
            var theItem = document.getElementById("hfSelectedMenuItem"); 
            
            switch(menuItem.get_text()){
                case '�޸�' :   
                   var re= window.open('TrainPlanEdit.aspx?id='+contextDataNode.getMember('TrainPlanID').get_value()+'&name='+contextDataNode.getMember('TrainName').get_text(),'TrainPlanEdit',' Width=700px; Height=600px,status=false,resizable=yes',true);		
				    re.focus(); 
                  break;
               case '�½�':
                    var re= window.open('TrainPlanEdit.aspx','TrainPlanEdit',' Width=700px; Height=600px,status=false,resizable=yes',true);		
				    re.focus(); 
                    break;
                case 'ɾ��': 
                    if(!confirm("��ȷ��Ҫɾ����" + contextDataNode.getMember('TrainName').get_text() + "����"))
                    {
                        return false; 
                    }
                    form1.DeleteID.value=contextDataNode.getMember('TrainPlanID').get_value();                      
                    form1.submit();
                    break; 
            } 
            return true; 
        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ComponentArt:Menu  ID="ContextMenu1"  ContextMenu="Custom"   runat="server"  Orientation="Vertical">
        <Items>
            <ComponentArt:MenuItem Text="�޸�" ClientSideCommand=" " />
            <ComponentArt:MenuItem Text="�½�" ClientSideCommand=" " />
            <ComponentArt:MenuItem Text="ɾ��"  ClientSideCommand="" />
        </Items>
        <ClientEvents>
        <ItemSelect EventHandler="ContextMenu_onItemSelect" />
        </ClientEvents>
    </ComponentArt:Menu>
    <div>
         <table style="width:90%; text-align: left;">
             <tr>
                <td style="text-align:left;">
                    ��ѵ�ƻ�
                </td>
                <td style="text-align:right; ">
                    <asp:Button ID="btnAddTrainPlan" runat="server" Text="����" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>            
             </tr>
             <tr>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAllTrainPlanInfo" TypeName="RailExam.BLL.TrainPlanBLL">
                </asp:ObjectDataSource>
                <td colspan="2" style="width:90%; text-align: left;">
                 <ComponentArt:Grid ID="Grid1" runat="server" DataSourceID="ObjectDataSource1" >
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="TrainPlanID"  >
                            <Columns>
                                <ComponentArt:GridColumn DataField="TrainPlanID" Visible ="false" />
                                <ComponentArt:GridColumn DataField="TrainName" HeadingText="�ƻ�����" />                     
                                <ComponentArt:GridColumn DataField="TrainContent" HeadingText="�ƻ�����" />
                                <ComponentArt:GridColumn DataField="BeginDate" HeadingText="��ʼʱ��" FormatString="yyyy-MM-dd"/>
                                <ComponentArt:GridColumn DataField="EndDate" HeadingText="����ʱ��" FormatString="yyyy-MM-dd"/>
                                <ComponentArt:GridColumn DataField="HasExam" ColumnType="CheckBox" DataType="System.Boolean" HeadingText="�Ƿ���"/>
                                <ComponentArt:GridColumn DataField="StatusName" HeadingText="��ǰ״̬"/>
                                <ComponentArt:GridColumn  DataCellClientTemplateId="CTedit"  HeadingText="����"/>
                             </Columns>           
                        </ComponentArt:GridLevel>     
                    </Levels>
                    <ClientTemplates >
                        <ComponentArt:ClientTemplate ID="CTedit" >
                            <A  onclick="ManageChapter(##DataItem.getMember('TrainPlanID').get_value()## )"  href="#"><b>��ѵ�ƻ�</b></A>&nbsp;
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <ClientEvents>
                        <ContextMenu EventHandler="ShowContextMenu1"  />
                    </ClientEvents>
                </ComponentArt:Grid>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfSelectedMenuItem" runat="server" />
        <asp:HiddenField ID="TrainPlanID" runat="server" />
    </div>
    <input id="DeleteID" type="hidden" name="DeleteID" />
    <input id="Refresh" type="hidden" name="Refresh" />
    </form>
</body>
</html>
