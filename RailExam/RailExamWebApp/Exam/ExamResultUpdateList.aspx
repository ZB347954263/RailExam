<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ExamResultUpdateList.aspx.cs"
    Inherits="RailExamWebApp.Exam.ExamResultUpdateList" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>成绩修改记录</title>

    <script type="text/javascript">  
      function Grid_onContextMenu(sender, eventArgs)
        {
          var flagUpdate=document.getElementById("HfUpdateRight").value;              
             
                  var item = ContextMenu1.get_items();
                
        	        if(flagUpdate=="False" )
                      {                                          
                         item.getItemByProperty("Text", "编辑").set_enabled(false);
                      }  
                      else
                      {                                             
                       item.getItemByProperty("Text", "编辑").set_enabled(true);
                      }                        
        
            switch(eventArgs.get_item().getMember('ExamName').get_text())
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
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;   
               
            switch(menuItem.get_text())
            {
                case '查看':
                    var ret = window.open('ExamResultUpdateFirst.aspx?mode=ReadOnly&id='+contextDataNode.getMember('examResultUpdateId').get_value(),'ExamManageFirst','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    ret.focus();
                    break;

                case '编辑':              
                 
         
                    var ret = window.open('ExamResultUpdateFirst.aspx?mode=Edit&id='+contextDataNode.getMember('examResultUpdateId').get_value(),'ExamManageFirst','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
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
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="parent">
                        手工评卷</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        成绩修改记录</div>
                </div>
            </div>
            <div id="content">
                <div id="mainContent">
                    <ComponentArt:Grid ID="examsGrid" runat="server" PageSize="19" DataSourceID="odsExams"
                        AllowPaging="true">
                        <ClientEvents>
                            <ContextMenu EventHandler="Grid_onContextMenu" />
                        </ClientEvents>
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="examResultUpdateId">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="examResultUpdateId" Visible="false" />
                                    <ComponentArt:GridColumn DataField="OrgName" HeadingText="机构名称" />
                                    <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" />
                                    <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="考生姓名" />
                                    <ComponentArt:GridColumn DataField="oldScore" HeadingText="修改前分数" />
                                    <ComponentArt:GridColumn DataField="newScore" HeadingText="修改后分数" />
                                    <ComponentArt:GridColumn DataField="updateCause" HeadingText="修改原因" />
                                    <ComponentArt:GridColumn DataField="updatePerson" HeadingText="修改人" />
                                    <ComponentArt:GridColumn DataField="updateDate" HeadingText="修改时间" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <ComponentArt:Menu ID="ContextMenu1" ContextMenu="Custom" runat="server" EnableViewState="true">
            <ClientEvents>
                <ItemSelect EventHandler="ContextMenu_onItemSelect" />
            </ClientEvents>
            <Items>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="view_disabled.gif" look-lefticonurl="view.gif"
                    Text="查看" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="edit_disabled.gif" look-lefticonurl="edit.gif"
                    Text="编辑" />
            </Items>
        </ComponentArt:Menu>
        <asp:ObjectDataSource ID="odsExams" runat="server" SelectMethod="GetExamResultUpdates"
            TypeName="RailExam.BLL.ExamResultUpdateBLL">
            <SelectParameters>
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <input type="hidden" name="Refresh" />
    </form>
</body>
</html>
