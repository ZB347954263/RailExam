<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DeleteRandomExamApply.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.DeleteRandomExamApply" %>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录信息</title>

    <script type="text/javascript">
           function delApply(id)
        {
           	var deleteRight = document.getElementById("HfDeleteRight").value;
			
			if(deleteRight == "False" ) {
				alert("您没有该操作的权限！");
				return false;
			}
           	
           var suitRange = document.getElementById("hfSuitRange").value;
           var isServer = document.getElementById("hfIsServer").value;
           if(suitRange == "0")
           {
             if(isServer == "True")
             {
                  alert("您没有该操作的权限！请连接本地考试系统进行登录信息管理！");
                  return;
             }  
           }
           
            if(!confirm("确定要删除该登录信息吗？"))
           {
                return; 
           }  
           
           form1.deleteid.value = id;
           form1.submit();
           form1.deleteid.value = "";
        }
        
        function clearConfirm()
        {
        	var deleteRight = document.getElementById("HfDeleteRight").value;
			
			if(deleteRight == "False" ) {
				alert("您没有该操作的权限！");
				return false;
			}
        	
            var suitRange = document.getElementById("hfSuitRange").value;
           var isServer = document.getElementById("hfIsServer").value;
           if(suitRange == "0")
           {
             if(isServer == "True")
             {
                  alert("您没有该操作的权限！请连接本地考试系统进行登录信息管理！");
                  return false;
             }  
           }
           
           if(!confirm("确定要清理考生登录信息吗？"))
           {
                return false; 
           }  
           return true;
        }
        
        function deleteConfirm()
        {
        	var deleteRight = document.getElementById("HfDeleteRight").value;
			
			if(deleteRight == "False" ) {
				alert("您没有该操作的权限！");
				return false;
			}
        	
           var suitRange = document.getElementById("hfSuitRange").value;
           var isServer = document.getElementById("hfIsServer").value;
           if(suitRange == "0")
           {
             if(isServer == "True")
             {
                  alert("您没有该操作的权限！请连接本地考试系统进行登录信息管理！");
                  return false;
             }  
           }
           
           if(!confirm("确定要删除全部登录信息吗？"))
           {
                return false; 
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
                        考试管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        登录信息</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <asp:Button ID="btnClear" runat="server" CssClass="buttonEnableLong" Text="清理考生登录信息" OnClientClick="return clearConfirm()" OnClick="btnClear_Click" />
                    <asp:Button ID="btnDelete" runat="server" CssClass="buttonEnableLong" Text="删除全部登录信息" OnClientClick="return deleteConfirm()" OnClick="btnDelete_Click"/> 
                </div>
            </div>
            <div id="content">
                <div style="text-align: center">
                    <ComponentArt:CallBack ID="gridCallback" runat="server" OnCallback="gridCallback_callback">
                        <Content>
                            <ComponentArt:Grid ID="Grid1" runat="server" PageSize="20" Width="100%">
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="EmployeeID">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="EmployeeID" HeadingText="编号" Visible="false" />
                                            <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="姓名" Width="80" />
                                            <ComponentArt:GridColumn DataField="OrgName" HeadingText="考生单位" Width="150" />
                                            <ComponentArt:GridColumn DataField="PostName" HeadingText="职名" Width="150" />
                                            <ComponentArt:GridColumn DataField="IPAddress" HeadingText="IP地址" Width="100" />
                                            <ComponentArt:GridColumn DataCellClientTemplateId="CTedit" HeadingText="操作" Width="100"
                                                Align="Center" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="CTedit">
                                        <a onclick="delApply(##DataItem.getMember('EmployeeID').get_value()##)" href="#">
                                            <b>删除</b></a>
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                            </ComponentArt:Grid>
                        </Content>
                    </ComponentArt:CallBack>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfSuitRange" runat="server" />
        <asp:HiddenField ID="hfIsServer" runat="server" />
        <input name="OutPutRandom" type="hidden" />
        <input name="ChooseID" type="hidden" />
        <input name="ChooseOneID" type="hidden" />
        <input name="Refresh" type="hidden" />
        <input name="deleteid" type="hidden" />
        <asp:HiddenField ID="hfSelect" runat="server" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" /> 
    </form>
</body>
</html>
