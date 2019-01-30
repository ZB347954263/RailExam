<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ChooseTheWay.aspx.cs" Inherits="RailExamWebApp.Item.ChooseTheWay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择题型</title>

    <script type="text/javascript">
    function gotoTheWeb(way)
    {
        var bid=document.getElementById("hfbid").value;
        var cid=document.getElementById("hfcid").value;
        if(bid.length > 0 && cid.length > 0)
        {            
            switch(way)
            {
                case "Others":
                	var editWindow = window.open("ItemDetail.aspx?mode=insert&bid="+bid+"&cid="+cid,
                        'ItemDetail','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
                	editWindow.focus();
                	break;
                case "Cloze":
                	var editWindow = window.open("ItemClozeDetail.aspx?mode=insert&bid="+bid+"&cid="+cid,
                        'ItemClozeDetail','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
                	editWindow.focus();
                    break;
            }
        }
        else
        {
            alert("教材章节信息缺失，无法进入新增题型页面.");
        }
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 99%; height: 80px;">
            <tr style="height: 50px">
                <td colspan="2" style="text-align: center; font-size: 14px; font-weight: bold; background-color: White;">
                    请选项编辑题型种类
                </td>
            </tr>
            <tr>
                <td style="text-align: center; width: 50%; background-color: White;">
                    <input id="btnOthers" class="buttonLong" type="button" value="选择, 判断" onclick="gotoTheWeb('Others')" />
                </td>
                <td style="text-align: center; width: 50%; background-color: White;">
                    <input id="btnCloze" class="buttonLong" type="button" value=" 综合选择题 " onclick="gotoTheWeb('Cloze')" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfbid" runat="server" />
        <asp:HiddenField ID="hfcid" runat="server" />
    </form>
</body>
</html>
