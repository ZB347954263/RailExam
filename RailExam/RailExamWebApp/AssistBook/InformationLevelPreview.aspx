﻿<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InformationLevelPreview.aspx.cs"
    Inherits="RailExamWebApp.AssistBook.InformationLevelPreview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    .left
    {
        text-align: center;
        width: 100px;
        padding: 10px 10px 10px 0;
        font-family:微软雅黑;
        font-size:15px;
        font-weight:bold;
        background-color: #DDEEFF;
    }
    .right
    {
        text-align: left;
        
        padding: 10px 0px 10px 10px;
        font-size:14px;
        background-color: White;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table border="0px" cellpadding="0" cellspacing="1px" width="100%" style="background-color: #B1C1EF;">
            <tr>
                <td class="left">
                    级别名称
                </td>
                <td class="right">
                    <asp:Literal ID="ltrlLevelName" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="left">
                    描 述
                </td>
                <td class="right">
                    <asp:Literal ID="ltrlDescription" runat="server" Text="(暂无描述)"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="left">
                    备 注
                </td>
                <td class="right" style="height: 80px">
                    <asp:Literal ID="ltrlMemo" runat="server" Text="(暂无备注)"></asp:Literal>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
