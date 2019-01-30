<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ComputerServerInfoDetail.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.ComputerServerInfoDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript"> 
    
    function Back()
    {
        var orgID=document.getElementById("hfOrgID").value;
        this.location.href="ComputerServerInfo.aspx?OrgID="+orgID;
    }
    
    function checkData()
    {
        var txtNo=document.getElementById("txtNo");
        var txtName=document.getElementById("txtName");
        var txtAddress=document.getElementById("txtAddress");
        var txtIP=document.getElementById("txtIP");
        if(txtNo.value.length==0)
        {
            alert("请输入编号");
            return false;
        }
		else if(txtNo.value.length>30)
		{
			alert("编号不合法");
			return false;
		}
        if(txtName.value.length==0)
        {
            alert("请输入服务器名称");
            return false;
        }
		else if(txtName.value.length>50)
		{
			alert("服务器名称不合法 ");
			return false;
		}
        if(txtIP.value.length>0)
        {
            var re = /\b((\d|[1-9]\d|1\d{2}|2([0-4]\d|5[0-5]))\.){3}(\d|[1-9]\d|1\d{2}|2([0-4]\d|5[0-5]))\b/;
            var m = txtIP.value.match(re);
            if(m==null)
            {
                alert("IP地址不合法");
                return false;
            }
        }
        return true;
    }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table id="tblCSID" class="contentTable">
            <tr>
                <td style="width: 20%">
                    服务器编号:
                </td>
                <td style="width: 79%">
                    <asp:TextBox ID="txtNo" runat="server" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    服务器名称:
                </td>
                <td style="width: 79%">
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    服务器地址:
                </td>
                <td style="width: 79%">
                    <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    IP地址:
                </td>
                <td style="width: 79%">
                    <asp:TextBox ID="txtIP" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">
                    备注:
                </td>
                <td style="width: 79%">
                    <asp:TextBox ID="txtMemo" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <asp:Button ID="btnConfirm" runat="server" CssClass="buttonSearch" Text="确 认" OnClientClick="return checkData()"
                        OnClick="btnConfirm_Click" />
                    <input type="button" id="btnCancel" class="buttonSearch" value="取 消" onclick="Back()" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <asp:HiddenField ID="hfCSID" runat="server" />
    </form>
</body>
</html>
