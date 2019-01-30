<%@ Page Language="C#" AutoEventWireup="True" Codebehind="TrainAimDetail.aspx.cs"
    Inherits="RailExamWebApp.Train.TrainAimDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>��ѵ�����ϸ��Ϣ</title>

    <script type="text/javascript">
//        function ManagePaper(id)
//        {
//            var re = window.open("../Paper/PaperPreview.aspx?id="+id,"PaperPreview"," Width=800px; Height=600px,status=false,resizable=yes,scrollbars=yes",true);		
//	        re.focus(); 
//        }
        
        function GetExercise(id)
        {
         var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5;    
    
      
            var re = window.open("../Exercise/ExerciseManage.aspx?id="+id,"ExerciseManage","Width=800px,left="+cleft+",top="+ctop+", Height=600px,status=false,resizable=no,scrollbars=yes",true);		
	        re.focus(); 
        }
        
        function EditBook(id)
        {
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5; 
      
            var re = window.open('../Book/BookDetail.aspx?mode=ReadOnly&id='+id,'BookDetail','Width=800px,left='+cleft+',top='+ctop+', Height=600px,status=false,resizable=no',true);
		    re.focus();
        }
        
        function EditCourseware(id)
        {
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5; 
          
            var re = window.open('../Courseware/CoursewareDetail.aspx?mode=ReadOnly&id='+id,'CoursewareEdit','Width=800px, Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
		    re.focus();
        }
        
//        function AddExercise()
//        {
//            var search = window.location.search;
//            var str = search.substring(search.indexOf("=")+1);
//            
//            if(str == '1')
//            {
//                alert("�ýڵ����¼��ڵ㣬����ѡ����ϰ��");
//                return false;
//            }
//            
//            var theNode = window.parent.tvType.get_selectedNode();
//            if(theNode.get_nodes().get_length() > 0)
//            {
//                alert("�ýڵ����¼��ڵ㣬����ѡ����ϰ��");
//                return false;
//            }
//            
//        	var re = window.open('TrainAimExercise.aspx?TypeID='+str,'TrainAimExercise',' Width=900px; Height=650px,status=false,resizable=no',true);
//		    re.focus();
//        }
        
//        function AddTask()
//        {
//            var search = window.location.search;
//            var str = search.substring(search.indexOf("=")+1);
//            
//            if(str == '1')
//            {
//                alert("�ýڵ����¼��ڵ㣬����ѡ����ҵ��");
//                return false;
//            }
//            
//            var theNode = window.parent.tvType.get_selectedNode();
//            if(theNode.get_nodes().get_length() > 0)
//            {
//                alert("�ýڵ����¼��ڵ㣬����ѡ����ҵ��");
//                return false;
//            }
//            
//        	var re = window.open('TrainAimTask.aspx?TypeID='+str,'TrainAimTask',' Width=900px; Height=650px,status=false,resizable=no',true);
//		    re.focus();
//        }
        
//        function ShowContextMenu1(sender, eventArgs)
//        {
//            switch(eventArgs.get_item().getMember('ObjPaper.PaperName').get_text())
//            {
//                default:
//                ContextMenu1.showContextMenu(eventArgs.get_event(), eventArgs.get_item());
//                return false;
//                break;
//            }
//        }      
        
//        function ContextMenu_onItemSelect(sender, eventArgs)
//        {
//            var menuItem = eventArgs.get_item();
//            var contextDataNode = menuItem.get_parentMenu().get_contextData();
//            
//            switch(menuItem.get_text())
//            {
//                case 'ɾ��':
//                    if(! confirm("��ȷ��Ҫɾ����" + contextDataNode.getMember('ObjPaper.PaperName').get_text() + "����"))
//                    {
//                        return false;
//                    }
//                    form1.DeleteID.value = contextDataNode.getMember('ObjPaper.PaperId').get_value();
//                    form1.submit();
//                    form1.DeleteID.value = "";
//                   
//                    break;
//            }
//            return true;
//        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="Page">
            <div>
                <table class="contentTable">
                    <tr>
                        <td>
                            ������
                            <asp:Label ID="lblDescription" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            ��ע��
                            <asp:Label ID="lblMemo" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </div>
            <div>
                <%--<table style="width: 650px; height: 160px">
            <tr>
                <td width="650px" height="35" align="left">--%>
                <img src="../Online/image/jclb01.jpg" alt="" />
                <%--</td>
            </tr>--%>
            </div>
            <%--<tr style="height: 125px">
                    <td valign="top">--%>
            <div id="grid1">
                <ComponentArt:Grid ID="gvBook" runat="server" AllowPaging="true" PageSize="5" Width="97%">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="bookId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="bookId" Visible="false" />
                                <ComponentArt:GridColumn DataField="bookName" HeadingText="�̲�����" />
                                <ComponentArt:GridColumn DataField="knowledgeId" Visible="false" />
                                <ComponentArt:GridColumn DataField="trainTypeId" Visible="false" />
                                <ComponentArt:GridColumn DataField="bookNo" HeadingText="�̲ı��" />
                                <ComponentArt:GridColumn DataField="publishOrgName" HeadingText="���Ƶ�λ" />
                                <ComponentArt:GridColumn DataField="authors" HeadingText="������" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="����" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTEdit">
                            <a onclick="GetExercise(##DataItem.getMember('bookId').get_value()## )" href="#"><b>
                                ��ϰ</b></a>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
                <%--</td>
                </tr>
            </table>--%>
            </div>
            <div>
                <%--<table style="width: 650px; height: 160px">
                <tr>
                    <td width="650px" height="35" align="left">--%>
                <img src="../Online/image/kjlb1.gif" alt="" />
                <%--</td>
                </tr>--%>
            </div>
            <%--<tr style="height: 125px">
                    <td valign="top">--%>
            <div id="grid2">
                <ComponentArt:Grid ID="gvCourse" runat="server" AllowPaging="true" PageSize="5" Width="97%">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="CoursewareID">
                            <Columns>
                                <ComponentArt:GridColumn DataField="CoursewareID" Visible="false" />
                                <ComponentArt:GridColumn DataField="CoursewareName" HeadingText="�μ�����" />
                                <ComponentArt:GridColumn DataField="CoursewareTypeID" Visible="false" />
                                <ComponentArt:GridColumn DataField="TrainTypeId" Visible="false" />
                                <ComponentArt:GridColumn DataField="ProvideOrgName" HeadingText="������λ" />
                                <ComponentArt:GridColumn DataField="Authors" HeadingText="������" />
                                <ComponentArt:GridColumn DataField="keyWord" HeadingText="�ؼ���" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                </ComponentArt:Grid>
                <%--</td>
                </tr>
            </table>--%>
            </div>
            <!--
            <table style="width: 650px; height: 160px">
                <tr>
                    <td width="650px" height="35" align="left" valign="middle">
                        <img alt="" src="../Online/image/exercise.gif" width="541" height="34" />
                        &nbsp;&nbsp;&nbsp;&nbsp; <a href="#" onclick="AddExercise()">
                            <img style="border: 0; cursor: hand;" alt="" src="../Common/Image/add.gif" /></a>
                    </td>
                </tr>
                <tr style="height: 125px">
                    <td valign="top">
                        <ComponentArt:Grid ID="gvExercise" runat="server" AllowPaging="true" PageSize="4"
                            Width="650px">
                            <Levels>
                                <ComponentArt:GridLevel DataKeyField="ObjPaper.PaperId">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="ObjPaper.PaperId" Visible="false" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.PaperName" HeadingText="��ϰ����" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.CategoryId" HeadingText="categoryId"
                                            Visible="false" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.CreateMode" HeadingText="���ⷽʽ" Visible="false" />
                                        <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate1" HeadingText="���ⷽʽ" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.ItemCount" HeadingText="��������" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.TotalScore" HeadingText="�ܷ�" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.CreatePerson" HeadingText="������" />
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </Levels>
                            <ClientTemplates>
                                <ComponentArt:ClientTemplate ID="ClientTemplate1">
                                    ## DataItem.getMember("ObjPaper.CreateMode").get_value() == 1 ? "�ֹ�����":"�������" ##
                                </ComponentArt:ClientTemplate>
                            </ClientTemplates>
                            <ClientEvents>
                                <ContextMenu EventHandler="ShowContextMenu1" />
                            </ClientEvents>
                        </ComponentArt:Grid>
                    </td>
                </tr>
            </table>
            <table style="width: 650px; height: 160px">
                <tr>
                    <td width="650px" align="left">
                        <img src="../Online/image/task.gif" width="541" height="34" />
                         &nbsp;&nbsp;&nbsp;&nbsp; <a href="#" onclick="AddTask()">
                            <img style="border: 0; cursor: hand;" alt="" src="../Common/Image/add.gif" /></a>
                    </td>
                </tr>
                <tr style="height: 125px">
                    <td valign="top">
                        <ComponentArt:Grid ID="gvTask" runat="server" AllowPaging="true" PageSize="4" Width="650px">
                             <Levels>
                                <ComponentArt:GridLevel DataKeyField="ObjPaper.PaperId">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="ObjPaper.PaperId" Visible="false" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.PaperName" HeadingText="��ϰ����" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.CategoryId" HeadingText="categoryId"
                                            Visible="false" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.CreateMode" HeadingText="���ⷽʽ" Visible="false" />
                                        <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="���ⷽʽ" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.ItemCount" HeadingText="��������" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.TotalScore" HeadingText="�ܷ�" />
                                        <ComponentArt:GridColumn DataField="ObjPaper.CreatePerson" HeadingText="������" />
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </Levels>
                            <ClientTemplates>
                                <ComponentArt:ClientTemplate ID="ClientTemplate3">
                                    ## DataItem.getMember("ObjPaper.CreateMode").get_value() == 1 ? "�ֹ�����":"�������" ##
                                </ComponentArt:ClientTemplate>
                            </ClientTemplates>
                            <ClientEvents>
                                <ContextMenu EventHandler="ShowContextMenu1" />
                            </ClientEvents>
                        </ComponentArt:Grid>
                    </td>
                </tr>
            </table>
            -->
        </div>
        <%--<ComponentArt:Menu ID="ContextMenu1" ContextMenu="Custom" runat="server">
            <Items>
                <ComponentArt:MenuItem Text="ɾ��" look-lefticonurl="delete.gif" disabledlook-lefticonurl="delete_disabled.gif" />
            </Items>
            <ClientEvents>
                <ItemSelect EventHandler="ContextMenu_onItemSelect" />
            </ClientEvents>
        </ComponentArt:Menu>--%>
        <input type="hidden" name="Refresh" />
        <%--<input type="hidden" name="DeleteID" />--%>
    </form>
</body>
</html>
