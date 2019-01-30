<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainPlanProjectDetail.aspx.cs" Inherits="RailExamWebApp.TrainManage.TrainPlanProjectDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>培训项目编辑</title>
    <script type="text/javascript">
    function checkData()
    {
        var txt=document.getElementById("txtName");
        if(txt.value.length==0)
        {
            alert("请输入培训类别名称");
            return false;
        }
    }
    function cancel()
    {
        close();
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; padding-top: 10px;">
            培训项目名称:
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </div>
        <div style="text-align: center; padding-top: 5px">
            <asp:Button ID="btnOK" runat="server" CssClass="button" Text="确定" OnClientClick="return checkData()"
                OnClick="btnOK_Click" />
            <input id="btnCancel" type="button" class="button" value="取消" onclick="cancel()" />
        </div>
    </form>
</body>
</html>
