<%@ Page Language="C#" AutoEventWireup="True" Codebehind="SelectEmployees.aspx.cs"
    Inherits="RailExamWebApp.Common.SelectEmployees" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ѡ��Ա��</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" style="width: 99%; height: 80%;">
                <tr>
                    <td style="width: 14%; height: 551px;" valign="top">
                        <table>
                            <tr>
                                <td style="width: 100%; height: 24px;">
                                    ��֯����</td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:ListBox runat="server" ID="listTType" Height="550px" Width="120px" AutoPostBack="true"
                                        OnSelectedIndexChanged="listTType_SelectedIndexChanged"></asp:ListBox></td>
                            </tr>
                        </table>
                    </td>
                    <td align="left" valign="top" style="width: 90%; height: 541px;">
                        <table>
                            <tr>
                                <td align="left" colspan="3" style="width: 100%">
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;����
                                    <asp:TextBox runat="server" ID="TextBoxPapername" Width="60px"></asp:TextBox>
                                    <asp:DropDownList runat="server" ID="ddlType">
                                        <asp:ListItem Value="" Text="-�Ա�-"></asp:ListItem>
                                        <asp:ListItem Value="��" Text="��"></asp:ListItem>
                                        <asp:ListItem Value="Ů" Text="Ů"></asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;Ա������<asp:TextBox runat="server" ID="textgh" Width="60px"></asp:TextBox>
                                    &nbsp;&nbsp; �Ƿ���ְ
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem Value="0">δ��ְ</asp:ListItem>
                                        <asp:ListItem Value="1">����ְ</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;<asp:Button runat="server" ID="btnQuery" Text="�� ѯ" OnClick="btnQuery_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="ButtonSave" Text="ȷ��" BackColor="#63B8FF" OnClick="ButtonSave_Click" />&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="Button1" Text="ȡ��" OnClick="Button1_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 47%">
                                    <div style="overflow: auto; height: 495px; width: 300px;">
                                        <asp:GridView ID="Grid1" runat="server" BorderColor="ActiveBorder" HeaderStyle-BackColor="ActiveBorder"
                                            Width="98%" DataKeyNames="EmployeeID" AllowPaging="False" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ְԱid" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelEmployeeID" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ա������">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelWorkNo" runat="server" Text='<%# Bind("WorkNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="����">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelName" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="������λ">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelPostName" runat="server" Text='<%# Bind("PostName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                                <td style="width: 6%">
                                    <asp:Button runat="server" ID="ButtonInputAll" BackColor="#63B8FF" Text=" >> " ToolTip="�������ȫ���û�"
                                        OnClick="ButtonInputAll_Click" />
                                    <br />
                                    <br />
                                    <asp:Button runat="server" ID="btnInput" BackColor="#63B8FF" Text="  >  " ToolTip="�������ѡ����û�"
                                        OnClick="btnInput_Click" />
                                    <br />
                                    <br />
                                    <asp:Button runat="server" ID="ButtonOutPut" Text="  <  " ToolTip="��ȥ�ұ�ѡ����û�" OnClick="ButtonOutPut_Click" />
                                    <br />
                                    <br />
                                    <asp:Button runat="server" ID="ButtonOutPutAll" Text=" << " ToolTip="��ȥ�ұ�ȫ���û�" OnClick="ButtonOutPutAll_Click" />
                                </td>
                                <td style="width: 47%; height: 495px;">
                                    <div style="overflow: auto; height: 495px">
                                        <asp:GridView ID="Grid2" runat="server" BorderColor="ActiveBorder" HeaderStyle-BackColor="ActiveBorder"
                                            Width="98%" DataKeyNames="EmployeeID" AllowPaging="False" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect2" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ְԱid" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelEmployeeID" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ա������">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelWorkNo" runat="server" Text='<%# Bind("WorkNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="����">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelName" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="������λ">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelPostName" runat="server" Text='<%# Bind("PostName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
