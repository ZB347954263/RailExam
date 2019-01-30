<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssistBookChapterUpdate.aspx.cs" Inherits="RailExamWebApp.AssistBook.AssistBookChapterUpdate" %>

<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>辅导教材章节更新记录</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
   <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />  
   <script type="text/javascript">
       //var search = window.location.search;
       //alert(search);  
   </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <%--<asp:Label runat="server" ID="lblBookName" Font-Bold="true"></asp:Label>
                    -
                    <asp:Label runat="server" ID="lblChapterName" Font-Bold="true"></asp:Label>
                    更新记录--%>
                    <div id="separator">
                    </div>
                    <div id="current">
                        辅导教材更新记录</div>
                </div>
                <div id="button">
                   <asp:Button ID="btnSave" runat="server" Text="保  存" CssClass="button"  CausesValidation="True"   OnClick="SaveButton_Click"/>&nbsp;&nbsp;
                   <asp:Button ID="CancelButton" runat="server" Text="关  闭" CssClass="button" CausesValidation="false" OnClientClick="window.close();"  />
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 10%">辅导教材名称</td>
                        <td style="width: 90%">
                            <asp:Label ID="lblBookName"  runat="server" Width="60px" >
                            </asp:Label>
                        </td>                   
                        </tr> 
                    <tr>
                        <td style="width: 10%"> 更改对象</td>
                        <td style="width: 90%">
                            <asp:Label ID="lblChapterName"  runat="server" Width="60px" >
                            </asp:Label>
                        </td>                   
                        </tr> 
                    <tr>
                        <td style="width: 10%">更改人</td>
                        <td style="width: 90%">
                            <asp:Label ID="lblPerson"  runat="server" Width="60px" >
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>更改时间</td>
                        <td> <asp:Label ID="lblDate"  runat="server" Width="60px" >
                            </asp:Label></td>
                    </tr>
                    <tr>
                        <td>更改原因<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtCause" runat="server" TextMode="MultiLine" Width="98%" Height="50px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>更改内容<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Width="98%" Height="50px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>备注</td>
                        <td>
                            <asp:TextBox ID="txtMemo" runat="server" TextMode="MultiLine" Width="98%" Height="50px">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="hfBookID" runat="server" />
        </div>
    <asp:HiddenField ID="hfBookNewName" runat="server" /> 
    </form>
</body>
</html>
