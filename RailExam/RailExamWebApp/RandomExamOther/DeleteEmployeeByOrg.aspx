<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteEmployeeByOrg.aspx.cs" Inherits="RailExamWebApp.RandomExamOther.DeleteEmployeeByOrg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>删除站段员工</title>
    <script type="text/javascript" src="../Common/JS/Common.js"></script>
    <script type="text/javascript">
        function delConfirm()
        {
            if(document.getElementById("ddlOrg").value == "0")
            {
                alert("请选择站段！");
                return;
            }
            
            if(!window.confirm("请确定要删除所选站段的所有员工信息吗？"))
            {
                return;
            }
            
            var ret = showCommonDialog("/RailExamBao/RandomExamOther/DeleteEmployeeProgress.aspx?OrgID="+document.getElementById('ddlOrg').value,'dialogWidth:320px;dialogHeight:30px;');
            if(ret == "true")
            {
               alert("删除成功！");
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="page">
            <div id="head">
                <div id="location">

                    <div id="parent">
                        职员管理</div>

                    <div id="current"></div>
                </div>
                <div id="welcomeInfo">
                </div>
                <div id="button">
                    <input type="button"  value="删  除" class="button" onclick="delConfirm()" />
                </div>
            </div>
            <div id="content">
                <table id="contentTable">
                    <tr>
                    <td id="contentTd"> 选择站段 </td>
                    <td id="contentTd"> <asp:DropDownList ID="ddlOrg" runat="server"></asp:DropDownList></td></tr>
                </table>
            </div>
    </div>
    </form>
</body>
</html>
