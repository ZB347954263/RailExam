<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ItemClozeDetail.aspx.cs"
    Inherits="RailExamWebApp.Item.ItemClozeDetail" %>

<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>完型填空</title>
    <link href="/RailExamWebApp/Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <link href="/RailExamWebApp/Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    td
    {
        background-color:White;
    }
    </style>

    <script type="text/javascript">
    function checkData()
    {
        var creater=document.getElementById("txtCreater").value;
        var testCount=document.getElementById("txtTestCount").value;
        var dropTestNo=document.getElementById("dropTestNo").value;
        var content=document.getElementById("txtContent").value;

        if(creater.length==0){alert("请输入出题人");return false;}
        if(testCount.length==0)
        {
            alert("请输入题目数");
            return false;
        }
        else if(isNaN(testCount))
        {
            alert("请输入合法的题目数");
            return false;
        }
        if(content.length==0){alert("请输入内容");return false;}
    }
    </script>

</head>
<body>
    <form id="frmCloze" runat="server">
        <table style="width: 99%; text-align: center; background-color: Silver; margin: 10px"
            cellpadding="0" cellspacing="1">
            <tr>
                <td>
                    教材章节
                </td>
                <td style="text-align: left; padding-left: 2px;">
                    <asp:Label ID="lblchapter" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    试题类型
                </td>
                <td style="text-align: left; padding-left: 2px;">
                    单 选
                </td>
                <td>
                    试题种类
                </td>
                <td style="text-align: left; padding-left: 2px; width: 300px;">
                    文 本
                </td>
            </tr>
            <tr>
                <td>
                    试题状态
                </td>
                <td style="text-align: left; padding-left: 2px;">
                    <asp:DropDownList ID="dropStatus" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    试题用途
                </td>
                <td style="text-align: left; padding-left: 2px;">
                    <asp:DropDownList ID="dropPurpose" runat="server">
                        <asp:ListItem Selected="True" Value="0">用作练习与考试</asp:ListItem>
                        <asp:ListItem Value="1">仅作考试</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    出题日期
                </td>
                <td style="text-align: left; padding-left: 2px; width: 300px;">
                    <uc1:Date ID="dateCreate" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    过期日期
                </td>
                <td style="text-align: left; padding-left: 2px;">
                    <uc1:Date ID="dateOut" runat="server" />
                </td>
                <td>
                    出 题 人
                </td>
                <td style="text-align: left; padding-left: 2px;">
                    <asp:TextBox ID="txtCreater" runat="server"></asp:TextBox>
                </td>
                <td>
                    题 目 数
                </td>
                <td style="text-align: left; padding-left: 2px; width: 300px;">
                    <asp:TextBox ID="txtTestCount" runat="server" MaxLength="2" OnTextChanged="txtTestCount_TextChanged"
                        Width="50px" AutoPostBack="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    试题内容
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Width="99%" Height="70px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    题 号
                </td>
                <td style="text-align: left; padding-left: 2px;">
                    <asp:DropDownList ID="dropTestNo" runat="server" OnSelectedIndexChanged="dropTestNo_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td>
                    选 项 数
                </td>
                <td colspan="3" style="text-align: left; padding-left: 2px;">
                    <asp:DropDownList ID="dropItemCount" runat="server" Width="60px" AutoPostBack="True"
                        OnSelectedIndexChanged="dropItemCount_SelectedIndexChanged">
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem Selected="True">3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    子题内容
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtSubContent" runat="server" TextMode="MultiLine" Width="99%" Height="50px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    候 选 项
                </td>
                <td colspan="5">
                    <table id="tblItems" runat="server" style="width: 99%; background-color: Silver"
                        cellpadding="0" cellspacing="1">
                        <tr id="trA" runat="server">
                            <td style="width: 9%">
                                <asp:RadioButton ID="radioA" runat="server" GroupName="TheKey" Checked="true" />A
                            </td>
                            <td style="width: 93%">
                                <asp:TextBox ID="txtA" runat="server" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trB" runat="server">
                            <td style="width: 9%; height: 24px;">
                                <asp:RadioButton ID="radioB" runat="server" GroupName="TheKey" />B
                            </td>
                            <td style="width: 93%; height: 24px;">
                                <asp:TextBox ID="txtB" runat="server" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trC" runat="server">
                            <td style="width: 9%">
                                <asp:RadioButton ID="radioC" runat="server" GroupName="TheKey" />C
                            </td>
                            <td style="width: 93%">
                                <asp:TextBox ID="txtC" runat="server" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trD" runat="server">
                            <td style="width: 9%; height: 24px;">
                                <asp:RadioButton ID="radioD" runat="server" GroupName="TheKey" />D
                            </td>
                            <td style="width: 93%; height: 24px;">
                                <asp:TextBox ID="txtD" runat="server" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trE" runat="server">
                            <td style="width: 9%">
                                <asp:RadioButton ID="radioE" runat="server" GroupName="TheKey" />E
                            </td>
                            <td style="width: 93%">
                                <asp:TextBox ID="txtE" runat="server" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trF" runat="server">
                            <td style="width: 9%">
                                <asp:RadioButton ID="radioF" runat="server" GroupName="TheKey" />F
                            </td>
                            <td style="width: 93%">
                                <asp:TextBox ID="txtF" runat="server" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trG" runat="server">
                            <td style="width: 9%">
                                <asp:RadioButton ID="radioG" runat="server" GroupName="TheKey" />G
                            </td>
                            <td style="width: 93%">
                                <asp:TextBox ID="txtG" runat="server" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trH" runat="server">
                            <td style="width: 9%">
                                <asp:RadioButton ID="radioH" runat="server" GroupName="TheKey" />H
                            </td>
                            <td style="width: 93%">
                                <asp:TextBox ID="txtH" runat="server" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trI" runat="server">
                            <td style="width: 9%">
                                <asp:RadioButton ID="radioI" runat="server" GroupName="TheKey" />I
                            </td>
                            <td style="width: 93%">
                                <asp:TextBox ID="txtI" runat="server" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trJ" runat="server">
                            <td style="width: 9%">
                                <asp:RadioButton ID="radioJ" runat="server" GroupName="TheKey" />J
                            </td>
                            <td style="width: 93%">
                                <asp:TextBox ID="txtJ" runat="server" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trK" runat="server">
                            <td style="width: 9%">
                                <asp:RadioButton ID="radioK" runat="server" GroupName="TheKey" />K
                            </td>
                            <td style="width: 93%">
                                <asp:TextBox ID="txtK" runat="server" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trL" runat="server">
                            <td style="width: 9%">
                                <asp:RadioButton ID="radioL" runat="server" GroupName="TheKey" />L
                            </td>
                            <td style="width: 93%">
                                <asp:TextBox ID="txtL" runat="server" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    关键字
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtKeyWord" runat="server" Width="99%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    备注
                </td>
                <td colspan="5">
                    <asp:TextBox ID="txtMemo" runat="server" Width="99%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="button" OnClientClick="return checkData()"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="取 消" CssClass="button" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfMainItemID" runat="server" />
        <asp:HiddenField ID="hfBookID" runat="server" />
        <asp:HiddenField ID="hfChapterID" runat="server" />
        <asp:HiddenField ID="hfMode" runat="server" />
        <asp:HiddenField ID="hfSavedTestNo" runat="server" />
        <asp:HiddenField ID="hfTestNoBefore" runat="server" />        
    </form>
</body>
</html>
