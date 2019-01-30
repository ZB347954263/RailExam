<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeeTransferInDetail.aspx.cs"
    Inherits="RailExamWebApp.Systems.EmployeeTransferInDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>填写调入信息</title>

    <script type="text/javascript">
         function showConfirm()
      {
         if(document.getElementById("txtName").value == "")
         {
            alert("姓名不能为空！");
            return false;
         }
         
         if(document.getElementById("ddlWorkshop").value == "0")
         {
            alert("请选择车间！");
            return false;
         }
         
          if(document.getElementById("txtWorkNo").value == "0")
         {
            alert("员工编码不能为空！");
            return false;
         }
         
         if(!window.confirm("您确定要将该员工调入本单位吗？"))
         {
            return false;
         }
         
         return true;
      }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <table class="contentTable">
                <tr>
                    <td class="contentTd">
                        姓名</td>
                    <td class="contentTd">
                        <asp:TextBox ID="txtName" runat="server" MaxLength="10"></asp:TextBox></td>
                    <td class="contentTd">
                        身份证号</td>
                    <td class="contentTd">
                        <asp:TextBox ID="txtPostNo" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="contentTd">
                        单位</td>
                    <td class="contentTd">
                        <asp:Label ID="lblOrg" runat="server"></asp:Label></td>
                    <td class="contentTd">
                        车间</td>
                    <td class="contentTd">
                        <asp:DropDownList ID="ddlWorkshop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlWorkshop_SelectedIndexChanged">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="contentTd">
                        班组</td>
                    <td class="contentTd">
                        <asp:DropDownList ID="ddlWorkgroup" runat="server">
                        </asp:DropDownList></td>
                    <td class="contentTd">
                        员工编码</td>
                    <td class="contentTd">
                        <asp:TextBox ID="txtWorkNo" runat="server" MaxLength="20"></asp:TextBox></td>
                </tr>
            </table>
            <br />
            <div style="text-align: center">
                <asp:Button ID="btnOK" Text="确定调入" CssClass="button" runat="server" OnClientClick="return showConfirm();"
                    OnClick="btnOk_Click" />
            </div>
        </div>
    </form>
</body>
</html>
