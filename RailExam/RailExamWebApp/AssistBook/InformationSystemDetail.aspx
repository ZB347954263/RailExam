<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InformationSystemDetail.aspx.cs"
    Inherits="RailExamWebApp.AssistBook.InformationSystemDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>����ڵ���Ϣ�༭</title>
    <style type="text/css">
    .label
    {
         font-family: ΢���ź�;
         font-size:14px; 
         font-weight: bold; 
         background-color: #C3DCF5;
         padding-left:10px;
         width:300px;
         border:solid 1px #C3DCF5;
    }
    .control
    {
        border:solid 1px #C3DCF5;
        width:290px;
        height:30px;
        padding: 15px 0px 10px 20px;
    }
    #bottom
    {
        text-align: center; 
        padding-top: 10px; 
        width: 310px
    }
    </style>

    <script language="javascript" type="text/javascript">
    // <!CDATA[

    function btnClose_onclick() 
    {
        window.close();
    }

    // ]]>
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="label">
                ��������</div>
            <div class="control">
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox></div>
            <div class="label">
                �� ��</div>
            <div class="control">
                <asp:TextBox ID="txtDescription" runat="server" Width="200px"></asp:TextBox></div>
            <div class="label">
                �� ע</div>
            <div class="control" style="height: 80px">
                <asp:TextBox ID="txtMemo" runat="server" Width="200px" Height="60px" TextMode="MultiLine"></asp:TextBox></div>
            <div id="bottom">
                <asp:Button ID="btnSave" runat="server" Text="�� ��" OnClick="btnSave_Click" CssClass="button" />
                <input id="btnClose" type="button" value="�� ��" onclick="return btnClose_onclick()" class="button"/>
            </div>
        </div>
    </form>
</body>
</html>
