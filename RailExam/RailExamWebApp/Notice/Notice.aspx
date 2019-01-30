<%@ Page Language="C#" AutoEventWireup="True" Codebehind="Notice.aspx.cs" Inherits="RailExamWebApp.Notice.Notice" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>通知发布</title>

    <script type="text/javascript">
    	function AddRecord()
        {
         var flagUpdate=document.getElementById("HfUpdateRight").value;  
          if(flagUpdate=="False")
             {  
             alert("您没有权限使用该操作!");
             return;
                         
              }  
                 var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-580)*.5;   
          ctop=(screen.availHeight-460)*.5;  
                 
            var ret = window.open('NoticeDetail.aspx?mode=Insert','NoticeDetail',' Width=580px; Height=460px, left='+cleft+',top='+ctop+',status=false,resizable=no',true);
		    if(ret == "true")
		    {
			    Form1.Refresh.value = ret;
			    Form1.submit();
			    Form1.Refresh.value = "";
		    }
        }
        
        function QueryRecord()
        {
            if(document.getElementById("query").style.display == "none")
            {
                document.getElementById("query").style.display = "";
            }
            else
            {
                document.getElementById("query").style.display = "none";
            }
        }
        
        function Grid1_onContextMenu(sender, eventArgs)
        {             
                 
         var flagUpdate=document.getElementById("HfUpdateRight").value;   
             var flagDelete=document.getElementById("HfDeleteRight").value;  
             
              var item = ContextMenu.get_items();
                
        	        if(flagUpdate=="False")
                      {  
                         item.getItemByProperty("Text", "新增").set_enabled(false);                    
                         item.getItemByProperty("Text", "编辑").set_enabled(false);
                      }  
                      else
                      {                
                        
                       item.getItemByProperty("Text", "新增").set_enabled(true);
                        
                       item.getItemByProperty("Text", "编辑").set_enabled(true);
                      }                               
                                     
                       if(flagDelete=="False")
                      {                     
                     item.getItemByProperty("Text", "删除").set_enabled(false);
                      }  
                      else
                      {
                      item.getItemByProperty("Text", "删除").set_enabled(true);
                      }     
                      
                      
            switch(eventArgs.get_item().getMember('NoticeID').get_text())
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
          cleft=(screen.availWidth-580)*.5;   
          ctop=(screen.availHeight-460)*.5;    
                
                 
            var menuItem = eventArgs.get_item();
            var contextDataNode = menuItem.get_parentMenu().get_contextData();
            
            switch(menuItem.get_text())
            {
                case '查看':
                    var re = window.open('NoticeDetail.aspx?mode=ReadOnly&id='+contextDataNode.getMember('NoticeID').get_value(),'NoticeDetail',' Width=580px; Height=460px,status=false,left='+cleft+',top='+ctop+',resizable=no',true);
				    re.focus();
                    break;
                    
                case '编辑':
                    var ret = window.open('NoticeDetail.aspx?mode=Edit&id='+contextDataNode.getMember('NoticeID').get_value(),'NoticeDetail',' Width=580px; Height=460px,status=false,left='+cleft+',top='+ctop+',resizable=no',true);
				    if(ret == "true")
				    {
					    Form1.Refresh.value = ret;
					    Form1.submit();
					    Form1.Refresh.value = "";
				    }
                    break;
            
                case '新增':
                    AddRecord();
                    break;
                
                case '删除':
                    if(!confirm("您确定要删除“" + contextDataNode.getMember('Title').get_value() + "”吗？"))
                    {
                        return false;
                    }
                    
                    form1.DeleteID.value = contextDataNode.getMember('NoticeID').get_value();
					form1.submit();
					form1.DeleteID.value = "";

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
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        系统管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        通知发布</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img id="btnAdd" onclick="AddRecord();" src="../Common/Image/add.gif" alt="" />
                    <img id="btnFind" onclick="QueryRecord();" src="../Common/Image/find.gif" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="query" style="display: none;">
                    <span>标题 </span>
                    <asp:TextBox ID="txtTitle" runat="server" Width="10%">
                    </asp:TextBox>
                    <span>紧急程度</span>
                    <asp:DropDownList ID="ddlImportanceName" runat="server" Width="10%">
                    </asp:DropDownList>
                    <span>发布机构</span>
                    <asp:TextBox ID="txtOrgName" runat="server" Width="7%">
                    </asp:TextBox>
                    <span>发布人</span>
                    <asp:TextBox ID="txtEmployeeName" runat="server" Width="6%"></asp:TextBox>
                    <span>发布时间从</span>
                    <uc1:Date ID="dateBeginTime" runat="server">
                    </uc1:Date>
                    <span>到</span>
                    <uc1:Date ID="dateEndTime" runat="server">
                    </uc1:Date>
                    <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="确  定" OnClick="btnQuery_Click" />
                </div>
                <div id="mainContent">
                    <ComponentArt:Grid ID="Grid1" runat="server"   PageSize="19">
                        <ClientEvents>
                            <ContextMenu EventHandler="Grid1_onContextMenu" />
                        </ClientEvents>
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="NoticeID">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="NoticeID" Visible="false" />
                                    <ComponentArt:GridColumn DataField="Title" HeadingText="标题" />
                                    <ComponentArt:GridColumn DataField="ImportanceName" HeadingText="紧急程度" />
                                    <ComponentArt:GridColumn DataField="DayCount" HeadingText="有效天数" />
                                    <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="发布人" />
                                    <ComponentArt:GridColumn DataField="OrgName" HeadingText="发布机构" />
                                    <ComponentArt:GridColumn DataField="CreateTime" FormatString="yyyy-MM-dd" HeadingText="发布时间" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsNotice" runat="server" SelectMethod="GetNotices" TypeName="RailExam.BLL.NoticeBLL">
            <SelectParameters>
                <asp:Parameter DefaultValue="-1" Name="nNum" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <ComponentArt:Menu ID="ContextMenu" ContextMenu="Custom" runat="server" EnableViewState="false">
            <ClientEvents>
                <ItemSelect EventHandler="ContextMenu_onItemSelect" />
            </ClientEvents>
            <Items>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="view_disabled.gif" look-lefticonurl="view.gif"
                    Text="查看" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="new_disabled.gif" look-lefticonurl="new.gif"
                    Text="新增" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="edit_disabled.gif" look-lefticonurl="edit.gif"
                    Text="编辑" />
                <ComponentArt:MenuItem LookId="BreakItem" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="delete_disabled.gif" look-lefticonurl="delete.gif"
                    Text="删除" />
            </Items>
        </ComponentArt:Menu>
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
           <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>
</body>
</html>
