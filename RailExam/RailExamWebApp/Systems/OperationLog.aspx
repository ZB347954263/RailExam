<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="OperationLog.aspx.cs" Inherits="RailExamWebApp.Systems.OperationLog" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>操作日志</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function FindRecord()
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
            var menuItemDelete = ContextMenu.findItemByProperty("Text", "删除");
                                                
            var flagDelete=document.getElementById("HfDeleteRight").value;
            if(flagDelete=="False")
            {                       
               menuItemDelete.set_enabled(false); 
            } 
            switch(eventArgs.get_item().getMember('LogID').get_text())
            {
                default:
                    ContextMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_item());
                    return false;
                break;
            }
        }
        
        function ContextMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();
            var contextDataNode = menuItem.get_parentMenu().get_contextData();
            
            switch(menuItem.get_text())
            {
                case '删除':
                	 var flagDelete=document.getElementById("HfDeleteRight").value;
                    if(flagDelete=="False") 
                    {
                    	alert("您没有该操作的权限！");
                    	return false;
                    }
                	if(! confirm("您确定要删除该条记录吗？"))
                    {
                        return false;
                    }
                    
                    form1.DeleteID.value = contextDataNode.getMember('LogID').get_value();
					form1.submit();
					form1.DeleteID.value = "";

                    break;
            }
            
            return true;
        }
        
         function btnAllClicked()
	      { 
	      	if (form1.btnAll.checked)
	      	{
	      		btnSelectAllClicked();
	      	}
	      	else
	      	{
	      		btnUnselectAllClicked();
	      	}
	      } 
	        function btnSelectAllClicked()
	        {    
	        	var theItem;
	        	for (var i = 0; i < Grid1.get_table().getRowCount(); i++)
	        	{
	        		theItem = Grid1.getItemFromClientId(Grid1.get_table().getRow(i).get_clientId());
	        		if(document.getElementById("cbxSelectItem_" + theItem.GetProperty("Id"))!=null)
	        		document.getElementById("cbxSelectItem_" + theItem.GetProperty("Id")).checked = true;
	        		
	        	}
	        }
	         function btnUnselectAllClicked()
	         {
	         	var theItem;
	         	for (var i = 0; i < Grid1.get_table().getRowCount(); i++)
	         	{
	         		theItem = Grid1.getItemFromClientId(Grid1.get_table().getRow(i).get_clientId());
	         		if(document.getElementById("cbxSelectItem_" + theItem.GetProperty("Id"))!=null)
	         		document.getElementById("cbxSelectItem_" + theItem.GetProperty("Id")).checked = false;
	         	}
	         }
	          function getSelectedItems()
	          {
	          	 var flagDelete=document.getElementById("HfDeleteRight").value;
                if(flagDelete=="False") 
                {
                	alert("您没有该操作的权限！");
                	return false;
                }
	          	
	          	var ids = [];
	          	var theItem;
	          	for (var i = 0; i < Grid1.get_table().getRowCount(); i++)
	          	{
	          		theItem = Grid1.getItemFromClientId(Grid1.get_table().getRow(i).get_clientId());
	          		if(document.getElementById("cbxSelectItem_" + theItem.GetProperty("Id"))!=null)
	          		{
	          			if (document.getElementById("cbxSelectItem_" + theItem.GetProperty("Id")).checked)
	          				ids.push(theItem.GetProperty("Id"));
	          		}
	          	}
	          	document.getElementById("hfSelectedIDs").value = ids.join(',');
	          	if(document.getElementById("hfSelectedIDs").value.length>0)
	          	{
	          		if (!confirm("您确定要删除吗？"))
	          		{
	          			return false;
	          	     }
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
                        操作日志</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img id="btnFind" onclick="FindRecord();" src="../Common/Image/find.gif" alt="" />
                    <asp:ImageButton ID="btnDeleteQuery" runat="server" CausesValidation="False" Visible="False"
                        OnClick="btnDeleteQuery_Click" ImageUrl="../Common/Image/delete_query.gif" />
                    <asp:Button ID="btnDelSelected" style="top: -4px; position: relative;" runat="server" CssClass="button" Text="删除所选" OnClientClick="return getSelectedItems()" OnClick="btnDelSelected_Click" />
                </div>
            </div>
            <div id="content">
                <div id="query" style="display: none;">
                    单位<asp:TextBox ID="txtOrgName" runat="server" Width="11%"></asp:TextBox>
                    登录名<asp:TextBox ID="txtUserID" runat="server" Width="7%"></asp:TextBox>
                    姓名<asp:TextBox ID="txtEmployeeName" runat="server" Width="7%"></asp:TextBox>
                    时间
                    从<uc1:Date ID="dateBeginTime" runat="server" />
                    到<uc1:Date ID="dateEndTime" runat="server" />
                    内容<asp:TextBox ID="txtActionContent" runat="server" Width="10%"></asp:TextBox>
                    备注<asp:TextBox ID="txtMemo" runat="server" Width="10%"></asp:TextBox>
                    <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="确  定" OnClick="btnQuery_Click" />
                </div>
                <div id="mainContent">
                    <asp:ObjectDataSource ID="odsLog" runat="server" SelectMethod="GetLogs"
                        TypeName="RailExam.BLL.SystemLogBLL">
                    </asp:ObjectDataSource>
                    <ComponentArt:Menu ID="ContextMenu" ContextMenu="Custom" runat="server" EnableViewState="true">
                        <ClientEvents>
                            <ItemSelect EventHandler="ContextMenu_onItemSelect" />
                        </ClientEvents>
                        <Items>
                            <ComponentArt:MenuItem DisabledLook-LeftIconUrl="delete_disabled.gif" look-lefticonurl="delete.gif"
                                Text="删除">
                            </ComponentArt:MenuItem>
                        </Items>
                    </ComponentArt:Menu>
                    <ComponentArt:Grid ID="Grid1" RunningMode="Client" runat="server" EnableViewState="False"   PageSize="19">
                        <ClientEvents>
                            <ContextMenu EventHandler="Grid1_onContextMenu" />
                        </ClientEvents>
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="LogID">
                                <Columns>
                                 <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" Width="40"
                                    HeadingText="&lt;input  type='checkbox' onclick='btnAllClicked()' name='btnAll' style='border: none' /&gt;" />
                                    <ComponentArt:GridColumn DataField="LogID" Visible="False" />
                                    <ComponentArt:GridColumn DataField="ActionOrgName" HeadingText="单位" />
                                    <ComponentArt:GridColumn DataField="ActionUserID" HeadingText="登录名" />
                                    <ComponentArt:GridColumn DataField="ActionEmployeeName" HeadingText="职员姓名" />
                                    <ComponentArt:GridColumn DataField="ActionTime" FormatString="yyyy-MM-dd HH:mm" HeadingText="时间" />
                                    <ComponentArt:GridColumn DataField="ActionContent" HeadingText="内容" />
                                    <ComponentArt:GridColumn DataField="Memo" HeadingText="备注" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                         <clienttemplates>
                               <ComponentArt:ClientTemplate ID="CTEdit">
                            <input style="border: none" id="cbxSelectItem_##DataItem.getMember('LogID').get_value()##" name="cbxSelectItem_##DataItem.getMember('LogID').get_value()##"
                                type="checkbox" value="##DataItem.getMember('LogID').get_value()##" />
                        </ComponentArt:ClientTemplate>
                                    </clienttemplates>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <input type="hidden" name="DeleteID" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="hfSelectedIDs" runat="server" />
    </form>
</body>
</html>