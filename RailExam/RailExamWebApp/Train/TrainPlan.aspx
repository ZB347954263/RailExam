<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainPlan.aspx.cs" Inherits="RailExamWebApp.Train.TrainPlan" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>培训计划</title>
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
                case '修改' :   
                   var re= window.open('TrainPlanEdit.aspx?id='+contextDataNode.getMember('TrainPlanID').get_value()+'&name='+contextDataNode.getMember('TrainName').get_text(),'TrainPlanEdit',' Width=700px; Height=600px,status=false,resizable=yes',true);		
				    re.focus(); 
                  break;
               case '新建':
                    var re= window.open('TrainPlanEdit.aspx','TrainPlanEdit',' Width=700px; Height=600px,status=false,resizable=yes',true);		
				    re.focus(); 
                    break;
                case '删除': 
                    if(!confirm("您确定要删除“" + contextDataNode.getMember('TrainName').get_text() + "”吗？"))
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
            <ComponentArt:MenuItem Text="修改" ClientSideCommand=" " />
            <ComponentArt:MenuItem Text="新建" ClientSideCommand=" " />
            <ComponentArt:MenuItem Text="删除"  ClientSideCommand="" />
        </Items>
        <ClientEvents>
        <ItemSelect EventHandler="ContextMenu_onItemSelect" />
        </ClientEvents>
    </ComponentArt:Menu>
    <div>
         <table style="width:90%; text-align: left;">
             <tr>
                <td style="text-align:left;">
                    培训计划
                </td>
                <td style="text-align:right; ">
                    <asp:Button ID="btnAddTrainPlan" runat="server" Text="新增" />
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
                                <ComponentArt:GridColumn DataField="TrainName" HeadingText="计划名称" />                     
                                <ComponentArt:GridColumn DataField="TrainContent" HeadingText="计划内容" />
                                <ComponentArt:GridColumn DataField="BeginDate" HeadingText="开始时间" FormatString="yyyy-MM-dd"/>
                                <ComponentArt:GridColumn DataField="EndDate" HeadingText="结束时间" FormatString="yyyy-MM-dd"/>
                                <ComponentArt:GridColumn DataField="HasExam" ColumnType="CheckBox" DataType="System.Boolean" HeadingText="是否考试"/>
                                <ComponentArt:GridColumn DataField="StatusName" HeadingText="当前状态"/>
                                <ComponentArt:GridColumn  DataCellClientTemplateId="CTedit"  HeadingText="操作"/>
                             </Columns>           
                        </ComponentArt:GridLevel>     
                    </Levels>
                    <ClientTemplates >
                        <ComponentArt:ClientTemplate ID="CTedit" >
                            <A  onclick="ManageChapter(##DataItem.getMember('TrainPlanID').get_value()## )"  href="#"><b>培训计划</b></A>&nbsp;
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
