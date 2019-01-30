<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="SelectEmployees.aspx.cs" Inherits="RailExamWebApp.Notice.SelectEmployees" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ѡ�������</title>
    <script type="text/javascript">
        function QueryRecord()
        {
            if(document.getElementById("query").style.display == "none")
            {
                document.getElementById("query").style.display = "";
            }
            else
            {
                document.getElementById("query").style.display = "none";
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="gridViewPage">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        ѡ�������</div>
                </div>
                <div id="button">
                    <img id="FindButton" onclick="QueryRecord();" src="../Common/Image/query.gif" alt="" />
                    <%--<asp:Button ID="FindButton" runat="server" CssClass="buttonSearch" Text="��  ��" OnClientClick="QueryRecord();" />--%>
                    <asp:ImageButton ID="btnSave" runat="server" CausesValidation="False" OnClick="btnSave_Click"
                        ImageUrl="../Common/Image/confirm.gif" />
                    <img id="CancelButton" onclick="window.close();" src="../Common/Image/cancel.gif" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                         ��֯����</div>
                    <div id="leftContent">
                        <asp:ListBox runat="server" ID="lbOrgType" AutoPostBack="true" Width="99%" Height="100%"
                            OnSelectedIndexChanged="lbOrgType_SelectedIndexChanged"></asp:ListBox>
                    </div>
                </div>
                <div id="right">
                    <div id="query" style="display: none;">
                        &nbsp;&nbsp;����
                        <asp:TextBox runat="server" ID="txtName" Width="10%"></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:DropDownList runat="server" ID="ddlSex">
                            <asp:ListItem Value="" Text="-�Ա�-"></asp:ListItem>
                            <asp:ListItem Value="��" Text="��"></asp:ListItem>
                            <asp:ListItem Value="Ů" Text="Ů"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;Ա������
                        <asp:TextBox runat="server" ID="txtWorkNo" Width="10%"></asp:TextBox>
                        &nbsp;&nbsp;
                        <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="��  ѯ" OnClick="btnQuery_Click" />
                        <%--<asp:ImageButton ID="btnQuery" runat="server" CausesValidation="False" OnClick="btnQuery_Click"
                            ImageUrl="../Common/Image/find.gif" />--%>
                    </div>
                    <div id="rightContentOfLeft" style="overflow: auto;">
                        <asp:GridView ID="Grid1" runat="server" DataKeyNames="EmployeeID" AutoGenerateColumns="False" Width="98%" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ְԱID" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ա������">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkNo" runat="server" Text='<%# Bind("WorkNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="����">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="������λ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPostName" runat="server" Text='<%# Bind("PostName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                    <div id="rightContentOfMiddle">
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="btnInputAll" runat="server" CausesValidation="false" CssClass="buttonGo"
                            Text=">>" ToolTip="�������ȫ���û�" OnClick="btnInputAll_Click" />
                        <br />
                        <br />
                        <asp:Button ID="btnInput" runat="server" CausesValidation="false" CssClass="buttonGo"
                            Text=">" ToolTip="�������ѡ����û�" OnClick="btnInput_Click" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="btnOutPut" runat="server" CausesValidation="false" CssClass="buttonGo"
                            Text="<" ToolTip="��ȥ�ұ�ѡ����û�" OnClick="btnOutPut_Click" />
                        <br />
                        <br />
                        <asp:Button ID="btnOutPutAll" runat="server" CausesValidation="false" CssClass="buttonGo"
                            Text="<<" ToolTip="��ȥ�ұ�ȫ���û�" OnClick="btnOutPutAll_Click" />
                    </div>
                    <div id="rightContentOfRight" style="overflow: auto;">
                        <asp:GridView ID="Grid2" runat="server" DataKeyNames="EmployeeID" AutoGenerateColumns="False" Width="98%" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ְԱID" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmployeeID" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ա������">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorkNo" runat="server" Text='<%# Bind("WorkNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="����">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="������λ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPostName" runat="server" Text='<%# Bind("PostName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EditRowStyle BackColor="#2461BF" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
