<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="EmployeeInfo.aspx.cs" Inherits="RailExamWebApp.Systems.EmployeeInfo" %>
<%@ Import namespace="RailExamWebApp.Common.Class"%>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>员工信息</title>
    <script type="text/javascript">
        function AssignAccount(id)
        {
           var flagUpdate=document.getElementById("HfUpdateRight").value; 
	        if(flagUpdate=="False" || document.getElementById("hfAdmin").value  == "False")
             {
                alert("您没有权限使用该操作！");
                return;
             } 
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-500)*.5;   
            ctop=(screen.availHeight-240)*.5;  
             
            var re = window.open("AssignAccount.aspx?id="+id,'AssignAccount','Width=500px; Height=240px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
	        re.focus();
        }
        
        function AddRecord()
        {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;  
            
           var pageUrl = "";
            if(document.getElementById("IsWuhan").value == "True")
            {
                pageUrl = "EmployeeDetail.aspx";
            }
            else
            {
                pageUrl = "/RailExamBao/RandomExamOther/EmployeeDetail.aspx";
            }
            
           var ret = window.open(pageUrl+'?mode=Insert','EmployeeDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
		   ret.focus();
        }
        
        function Grid_onContextMenu(sender, eventArgs)
        {
            var menuItemEdit = ContextMenu.findItemByProperty("Text", "编辑");
            var menuItemDelete = ContextMenu.findItemByProperty("Text", "删除");
            var flagUpdate=document.getElementById("HfUpdateRight").value; 
                          
	        if(flagUpdate=="False")
             {
                menuItemEdit.set_enabled(false); 
             }               
                      
            var flagDelete=document.getElementById("HfDeleteRight").value;
            if(flagDelete=="False")
            {                       
               menuItemDelete.set_enabled(false); 
            }      
            
             if(document.getElementById("IsWuhanOnly").value == "True")
            {
                menuItemEdit.set_enabled(false);
                menuItemDelete.set_enabled(false);   
            }
            
            switch(eventArgs.get_item().getMember('EmployeeID').get_text())
            {
                default:
                    ContextMenu.showContextMenu(eventArgs.get_event(), eventArgs.get_item());
                    return false;
                break;
            }
        }
        
        function ContextMenu_onItemSelect(sender, eventArgs)
        {
            var search = window.location.search;
            var str= search.substring(search.indexOf("&")+1);
            str = str.substring(str.indexOf("=")+1);
            var menuItem = eventArgs.get_item();
            var contextDataNode = menuItem.get_parentMenu().get_contextData();
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;  
            
            var pageUrl = "";
            if(document.getElementById("IsWuhan").value == "True")
            {
                pageUrl = "EmployeeDetail.aspx";
            }
            else
            {
                pageUrl = "/RailExamBao/RandomExamOther/EmployeeDetail.aspx";
            }
            
            switch(menuItem.get_text())
            {
                case '查看':
                    var ret = window.open(pageUrl+'?mode=ReadOnly&id='+contextDataNode.getMember('EmployeeID').get_value(),'EmployeeDetail',' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    ret.focus();
                    break;
                    
                case '编辑': 
                    var ret = window.open(pageUrl+'?mode=Edit&type='+ str +'&id='+contextDataNode.getMember('EmployeeID').get_value(),'EmployeeDetail',' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    ret.focus();
                    break;
            
                case '新增':
                    AddRecord();
                    break;
                
                case '删除':
                    if( contextDataNode.getMember('EmployeeID').get_value() == document.getElementById("NowEmployeeID").value)
                    {
                        alert('登录用户不能删除本人信息！');
                       return false; 
                    }
                    if(! confirm("您确定要删除“" + contextDataNode.getMember('EmployeeName').get_value() + "”吗？"))
                    {
                        return false;
                    }
                    
                    form1.DeleteID.value = contextDataNode.getMember('EmployeeID').get_value();
					form1.submit();
					form1.DeleteID.value = "";

                    break;
            }
            
            return true;
        }
       function Update(id)
       {
           var flagUpdate=document.getElementById("HfUpdateRight").value; 
	        if(document.getElementById("hfAdmin").value == "False")
             {
                alert("您没有权限使用该操作！");
                return;
             } 
             if(! confirm("您确定要为该员工初始化密码吗？"))
            {
                return false;
            }
            
          form1.UpdatePsw.value = id;
			form1.submit();
			form1.UpdatePsw.value = "";
      } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="gridPage">
            <div id="query" style="display: none;">
                &nbsp;&nbsp;姓名
                <asp:TextBox ID="txtName" runat="server" Width="10%"></asp:TextBox>
                <asp:DropDownList ID="ddlSex" runat="server">
                    <asp:ListItem Value="" Text="-性别-"></asp:ListItem>
                    <asp:ListItem Value="男" Text="男"></asp:ListItem>
                    <asp:ListItem Value="女" Text="女"></asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                <asp:TextBox ID="txtWorkNo" runat="server" Width="10%"></asp:TextBox>
                <%--<asp:ImageButton ID="btnQuery" runat="server" CausesValidation="False" OnClick="btnQuery_Click"
                    ImageUrl="../Common/Image/confirm.gif" />--%>
                是否离职
               <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="0">未离职</asp:ListItem>
                    <asp:ListItem Value="1">已离职</asp:ListItem>
               </asp:DropDownList> 
                <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="确  定" OnClick="btnQuery_Click" />
            </div>
            <div id="gird">
                <ComponentArt:Grid ID="Grid1" runat="server" PageSize="20" Width="97%">
                    <ClientEvents>
                        <ContextMenu EventHandler="Grid_onContextMenu" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="EmployeeID">
                            <Columns>
                                <ComponentArt:GridColumn DataField="EmployeeID" HeadingText = "员工编码" Width="50"/>
                                <ComponentArt:GridColumn DataField="WorkNo" HeadingText="员工编码"  Width="120"/>
                                <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="姓名"  Width="80"/>
                                <ComponentArt:GridColumn DataField="OrgName" HeadingText="组织机构"  Width="150"/>
                                <ComponentArt:GridColumn DataField="PostName" HeadingText="工作岗位" Width="120" />
                                <ComponentArt:GridColumn DataField="WorkPhone" HeadingText="办公电话" Visible="false" />
                                <ComponentArt:GridColumn AllowSorting="False" DataCellClientTemplateId="CTEdit" Width="150" HeadingText="操作" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTEdit" runat="server">
                            <a onclick="AssignAccount(##DataItem.getMember('EmployeeID').get_value()##)" href="#">
                                <b>登录帐户</b></a>
                             <a onclick="Update(##DataItem.getMember('EmployeeID').get_value()##)" href="#"><b>初始化密码</b></a>
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
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="view_disabled.gif" look-lefticonurl="view.gif"
                    Text="查看" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="edit_disabled.gif" look-lefticonurl="edit.gif"
                    Text="编辑" />
                <ComponentArt:MenuItem LookId="BreakItem" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="delete_disabled.gif" look-lefticonurl="delete.gif"
                    Text="删除" />
            </Items>
        </ComponentArt:Menu>
        <input type="hidden" id="NewButton" onclick="return AddRecord();" />
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <input type="hidden" name="RefreshAdd" />
       <input type="hidden" name="UpdatePsw" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="IsWuhan" runat="server" />
        <asp:HiddenField ID="IsWuhanOnly" runat="server"/>
        <asp:HiddenField ID="hfAdmin" runat="server" />
        <asp:HiddenField ID="NowEmployeeID" runat="server" />
    </form>
</body>
</html>
