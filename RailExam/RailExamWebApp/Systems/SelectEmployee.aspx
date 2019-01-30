<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectEmployee.aspx.cs"
    Inherits="RailExamWebApp.Systems.SelectEmployee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择员工</title>

    <script type="text/javascript">
         function CheckAll(oCheckbox)
        {
            var Grid1 = document.getElementById("Grid1");
            for(i = 1;i < Grid1.rows.length; i ++)
            {               
            if(Grid1.rows[i].cells[0].getElementsByTagName("INPUT")[0] !=null && !Grid1.rows[i].cells[0].getElementsByTagName("INPUT")[0].disabled)
            {
                Grid1.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;     
            }
            }
        }  
       
   function CloseBack()
  {
       window.close(); 
  } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table class="contentTable">
            <tr>
                <td style="width: 10%" align="right">
                    站段
                </td>
                <td style="width: 23%" align="left">
                    <asp:DropDownList ID="ddlStation" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="ddlStation_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 10%" align="right">
                    车间
                </td>
                <td style="width: 22%" align="left">
                    <asp:DropDownList ID="ddlWorkShop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlWorkShop_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td style="width: 10%" align="right">
                    班组
                </td>
                <td style="width: 25%" align="left" colspan="2">
                    <asp:DropDownList ID="ddlOrg" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 10%" align="right">
                    系统
                </td>
                <td style="width: 23%" align="left">
                    <asp:DropDownList ID="ddlSystem" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSystem_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 10%" align="right">
                    工种
                </td>
                <td style="width: 22%" align="left">
                    <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td style="width: 10%" align="right">
                    职名
                </td>
                <td style="width: 25%" align="left" colspan="2">
                    <asp:DropDownList ID="ddlPost" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 10%" align="right">
                    姓名</td>
                <td style="width: 23%" align="left">
                    <asp:TextBox runat="server" ID="txtEmployeeName"></asp:TextBox>
                </td>
                <td style="width: 10%" align="right">
                    拼音码</td>
                <td style="width: 22%" align="left">
                    <asp:TextBox runat="server" ID="txtPinyinCode"></asp:TextBox></td>
                <td style="width: 10%" align="right">
                    是否班组长</td>
                <td style="width: 22%" align="left">
                    <asp:DropDownList ID="ddlGroupLeader" runat="server">
                        <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Value="0">否</asp:ListItem>
                    </asp:DropDownList></td>
                <td align="center">
                    <asp:ImageButton runat="server" ID="btnQuery" ImageUrl="~/Common/Image/find.gif"
                        OnClick="btnQuery_Click" />
                </td>
            </tr>
        </table>
        <table border="0" style="width: 99%;">
            <tr>
                <td style="width: 100%; height: 380px;" valign="top" align="center">
                    <div style="overflow: auto; width: 100%; height: 380px">
                        <asp:DataGrid ID="Grid1" runat="server" HeaderStyle-BackColor="ActiveBorder" Width="98%"
                            PageSize="15" DataKeyField="EmployeeID" AllowPaging="true" AutoGenerateColumns="False"
                            OnPageIndexChanged="Grid1_PageIndexChanging" GridLines="None" AllowSorting="true"
                            AllowCustomPaging="true" OnSortCommand="Grid1_Sorting">
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderTemplate>
                                        <input id="CheckboxAll" type="checkbox" onclick="CheckAll(this)" title="全选或反选" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" Checked='<%#((","+ViewState["ChooseId"]+",").Contains(","+DataBinder.Eval(Container, "DataItem.EmployeeID")+",") ? true : false) %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="职员id" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="LabelEmployeeID" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="姓名" SortExpression="a.employee_name">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="LabelName" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="员工编码" SortExpression="a.WORK_NO">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="LabelWorkNo" runat="server" Text='<%# Bind("WorkNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="性别" SortExpression="a.Sex">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSex" runat="server" Text='<%# Bind("Sex") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="组织机构" SortExpression="GetOrgName(a.org_id)">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" Wrap="false" Width="30%" />
                                    <ItemTemplate>
                                        <asp:Label ID="LabelOrgName" runat="server" Text='<%# Bind("OrgName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="职名" SortExpression="b.Post_Name">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPostName" runat="server" Text='<%# Bind("PostName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="班组长">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="center" Wrap="false" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsGroupLeader" Enabled="false" runat="server" Checked='<%# Convert.ToInt32(Eval("IsGroupLeader"))==1 %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <ItemStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle HorizontalAlign="left" Mode="NumericPages" PageButtonCount="30" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="22px" />
                            <AlternatingItemStyle BackColor="White" />
                            <EditItemStyle BackColor="#2461BF" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 90%;" align="center">
                    <asp:Button runat="server" ID="btnInput" Text="添加所选职员" CssClass="buttonLong" OnClick="btnInput_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnClose" Text="关　闭" CssClass="button" OnClientClick="CloseBack()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
