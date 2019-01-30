<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="BookChapterUpdate.aspx.cs" Inherits="RailExamWebApp.Book.BookChapterUpdate" %>

<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�̲��½ڸ��¼�¼</title>
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
                    ���¼�¼--%>
                    <div id="separator">
                    </div>
                    <div id="current">
                        �̲ĸ��¼�¼</div>
                </div>
                <div id="button">
                   <asp:Button ID="btnSave" runat="server" Text="��  ��" CssClass="button"  CausesValidation="True"   OnClick="SaveButton_Click"/>&nbsp;&nbsp;
                   <asp:Button ID="CancelButton" runat="server" Text="��  ��" CssClass="button" CausesValidation="false" OnClientClick="window.close();"  />
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 10%">�̲�����</td>
                        <td style="width: 90%">
                            <asp:Label ID="lblBookName"  runat="server" Width="60px" >
                            </asp:Label>
                        </td>                   
                        </tr> 
                    <tr>
                        <td style="width: 10%"> ���Ķ���</td>
                        <td style="width: 90%">
                            <asp:Label ID="lblChapterName"  runat="server" Width="60px" >
                            </asp:Label>
                        </td>                   
                        </tr> 
                    <tr>
                        <td style="width: 10%">������</td>
                        <td style="width: 90%">
                            <asp:Label ID="lblPerson"  runat="server" Width="60px" >
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>����ʱ��</td>
                        <td> <asp:Label ID="lblDate"  runat="server" Width="60px" >
                            </asp:Label></td>
                    </tr>
                    <tr>
                        <td>����ԭ��<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtCause" runat="server" TextMode="MultiLine" Width="98%" Height="50px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>��������<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" Width="98%" Height="50px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>��ע</td>
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
