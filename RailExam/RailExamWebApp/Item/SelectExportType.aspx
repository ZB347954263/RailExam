<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectExportType.aspx.cs"
    Inherits="RailExamWebApp.Item.SelectExportType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择导出方式</title>

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">
        function exportItem()
        {
            var search = window.location.search;
            var type;
            //alert(document.getElementById("rbnType1").checked);
            if(document.getElementById("rbnType1").checked)
            {
                type="word";
            }
            else
            {
                type="excel";
            }
            var ret = showCommonDialog("/RailExamBao/Item/ExportItem.aspx" + search + "&type=" + type,'dialogWidth:350px;dialogHeight:30px;');  
            if(ret != "")
           {
               form1.refresh.value = ret;
               form1.submit();
               form1.refresh.value ="";
           }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center;">
            <asp:RadioButton ID="rbnType1" runat="server" Text="导出Word" Checked="true" GroupName="rbnType" />&nbsp;&nbsp;&nbsp;
            <asp:RadioButton ID="rbnType2" runat="server" Text="导出Excel" GroupName="rbnType" />
            <br />
            <br />
            <input type="button" name="export" onclick="exportItem()" class="button" value="确  定" />
            <input type="hidden" name="refresh"/>
        </div>
    </form>
</body>
</html>
