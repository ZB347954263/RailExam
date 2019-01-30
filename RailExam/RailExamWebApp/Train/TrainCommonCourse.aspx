<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainCommonCourse.aspx.cs" Inherits="RailExamWebApp.Train.TrainCommonCourse" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�����γ�</title>
    <script type="text/javascript">              
      
        function ManageChapter(id)
        {
            var re= window.open("TrainCommonCourseEdit.aspx?id="+id,"TrainCommonCourseEdit"," Width=700px; Height=600px,status=false,resizable=no",true);		
            re.focus();  
        }
                   
        function AddRecord()
        {			
            var re= window.open("TrainCommonCourseEdit.aspx",'TrainCommonCourseEdit',' Width=700px; Height=600px,status=false,resizable=yes',true);		
            re.focus();  				    
        }
			
        function ShowContextMenu1(sender, eventArgs)
        {
            switch(eventArgs.get_item().getMember('CourseName').get_text())
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
            var theItemID = document.getElementById("CourseID"); 
            
            switch(menuItem.get_text()){
                case '�޸�' :   
                   var re= window.open('TrainCommonCourseEdit.aspx?id='+contextDataNode.getMember('TrainCourseID').get_value()+'&name='+contextDataNode.getMember('CourseName').get_text(),'TrainCommonCourseEdit',' Width=700px; Height=600px,status=false,resizable=yes',true);		
				    re.focus(); 
                  break;
               case '�½�':
                    var re= window.open('TrainCommonCourseEdit.aspx','TrainCommonCourseEdit',' Width=700px; Height=600px,status=false,resizable=yes',true);		
				    re.focus(); 
                    break;
                case 'ɾ��': 
                    if(!confirm("��ȷ��Ҫɾ����" + contextDataNode.getMember('CourseName').get_text() + "����"))
                    {
                        return false; 
                    }
                    form1.DeleteID.value=contextDataNode.getMember('TrainCourseID').get_value();                      
                    form1.submit();
                    break;
               case '����':
                    theItem.value = "����";
                    theItemID.value = contextDataNode.getMember('TrainCourseID').get_value();               
                    tvTrainCourseMoveCallBack.callback(contextDataNode.getMember('TrainCourseID').get_value(), "CanMoveUp");
                    break;
                    
                case '����':
                    theItem.value = "����";
                    theItemID.value = contextDataNode.getMember('TrainCourseID').get_value();
                    tvTrainCourseMoveCallBack.callback(contextDataNode.getMember('TrainCourseID').get_value(), "CanMoveDown");
                    break;  
            } 
            return true; 
        }
        
        function tvTrainCourseMoveCallBack_onCallbackComplete(sender, eventArgs)
        {
            var theResult = document.getElementById("hfCanMove"); 
            var theItem = document.getElementById("hfSelectedMenuItem"); 
            var theItemID = document.getElementById("CourseID");
           
            if(!(theResult && theResult.value == "true"))
            {
                alert("�����ƶ��ýڵ㣡");
                theItem.value == "";
                theResult.value == "";
                return;
            }
            
            if(theItem && theItem.value == "����")
            {
                tvTrainCourseChangeCallBack.callback(theItemID.value,"MoveUp");
                form1.Refresh.value="true";
                form1.submit();
                form1.Refresh.value="";
            }
            if(theItem && theItem.value == "����")
            {
                tvTrainCourseChangeCallBack.callback(theItemID.value,"MoveDown");
                form1.Refresh.value="true";
                form1.submit();
                form1.Refresh.value="";
            }
            
            theResult.value == "";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <ComponentArt:Menu  ID="ContextMenu1"  ContextMenu="Custom"   runat="server"  Orientation="Vertical">
        <Items>
            <ComponentArt:MenuItem Text="����"  ClientSideCommand="" />
            <ComponentArt:MenuItem Text="����"  ClientSideCommand="" />
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
                <td style="text-align:left">
                    �����γ̿γ���ϵ
                </td>
                <td style="text-align:right">
                    <asp:Button ID="btnAddTrainCourse" runat="server" Text="����" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </td>            
             </tr>
             <tr>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetTrainCommondCourseInfo" TypeName="RailExam.BLL.TrainCourseBLL">
                </asp:ObjectDataSource>
                <td colspan="2" style="width:90%; text-align: left;">
                 <ComponentArt:CallBack ID="tvTrainCourseChangeCallBack" runat="server" OnCallback="tvTrainCourseChangeCallBack_Callback">
                    <Content>
                        <ComponentArt:Grid ID="Grid1" runat="server" AllowPaging="true" PageSize="15" DataSourceID="ObjectDataSource1" >
                            <Levels>
                                <ComponentArt:GridLevel DataKeyField="TrainCourseID"  >
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="TrainCourseID" Visible ="false" />
                                        <ComponentArt:GridColumn DataField="StandardID" Visible ="false" />
                                        <ComponentArt:GridColumn DataField="CourseNo" HeadingText="���" />                     
                                        <ComponentArt:GridColumn DataField="CourseName" HeadingText="�γ�����" />
                                        <ComponentArt:GridColumn DataField="StudyHours" HeadingText="�γ�ѧʱ"/>
                                        <ComponentArt:GridColumn DataField="StudyDemand" HeadingText="ѧϰҪ��"/>
                                        <ComponentArt:GridColumn DataField="HasExam" ColumnType="CheckBox" DataType="System.Boolean" HeadingText="�Ƿ���"/>
                                        <ComponentArt:GridColumn DataField="RequireCourseName" HeadingText="Լ����ϵ"/>
                                        <ComponentArt:GridColumn  DataCellClientTemplateId="CTedit"  HeadingText="����"/>
                                     </Columns>           
                                </ComponentArt:GridLevel>     
                            </Levels>
                            <ClientTemplates ><ComponentArt:ClientTemplate ID="CTedit" ><A  onclick="ManageChapter(##DataItem.getMember('TrainCourseID').get_value()## )"  href="#"><b>�γ����</b></A>&nbsp;</ComponentArt:ClientTemplate></ClientTemplates>
                            <ClientEvents>
                                <ContextMenu EventHandler="ShowContextMenu1"  />
                            </ClientEvents>
                        </ComponentArt:Grid>
                    </Content>
                </ComponentArt:CallBack>
                <ComponentArt:CallBack ID="tvTrainCourseMoveCallBack" runat="server" OnCallback="tvTrainCourseMoveCallBack_Callback">
                    <Content>
                        <asp:HiddenField ID="hfCanMove" runat="server" />
                    </Content>
                    <ClientEvents>
                        <CallbackComplete EventHandler="tvTrainCourseMoveCallBack_onCallbackComplete" />
                    </ClientEvents>
                 </ComponentArt:CallBack>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfSelectedMenuItem" runat="server" />
        <asp:HiddenField ID="CourseID" runat="server" />
    </div>
    <input id="DeleteID" type="hidden" name="DeleteID" />
    <input id="Refresh" type="hidden" name="Refresh" />
    </form>
</body>
</html>
