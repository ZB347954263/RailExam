<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeInfoOther.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.EmployeeInfoOther" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>查看权限人员</title>
       <script type="text/javascript">
        function AssignAccount(id)
        {
           var flagUpdate=document.getElementById("HfUpdateRight").value; 
	        if(flagUpdate=="False")
             {
                alert("您没有权限使用该操作！");
                return;
             } 
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-500)*.5;   
            ctop=(screen.availHeight-240)*.5;  
             
            var re = window.open("/RailExamBao/Systems/AssignAccount.aspx?id="+id,'AssignAccount','Width=500px; Height=240px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
	        re.focus();
        }
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="gridPage">
            <div id="gird">
                <br/>
                <componentart:grid id="Grid1" runat="server" pagesize="20" width="97%">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="EmployeeID">
                            <Columns>
                                <ComponentArt:GridColumn DataField="EmployeeID" HeadingText = "员工编码" Width="50"/>
                                <ComponentArt:GridColumn DataField="WorkNo" HeadingText="员工编码"  Width="120"/>
                                <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="姓名"  Width="80"/>
                                <ComponentArt:GridColumn DataField="OrgName" HeadingText="组织机构"  Width="150"/>
                                <ComponentArt:GridColumn DataField="PostName" HeadingText="工作岗位" Width="120" />
                                <ComponentArt:GridColumn DataField="RoleName" HeadingText="角色权限" Width="80"/>
                                <ComponentArt:GridColumn AllowSorting="False" DataCellClientTemplateId="CTEdit" Width="150" HeadingText="操作" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTEdit" runat="server">
                            <a onclick="AssignAccount(##DataItem.getMember('EmployeeID').get_value()##)" href="#">
                                <b>登录帐户</b></a>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </componentart:grid>
            </div>
        </div> 
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>
</body>
</html>
