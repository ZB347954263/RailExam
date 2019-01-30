<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamManageFourthEdit.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamManageFourthEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>批量添加考生</title>

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
       if(window.opener) {
       	   window.opener.form1.Refresh.value = "true";
           window.opener.form1.submit();
           window.close(); 
       }
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
                    <asp:DropDownList ID="ddlStation" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlStation_SelectedIndexChanged">
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
                <td style="width: 22%" align="left" colspan="2">
                    <asp:DropDownList ID="ddlGroupLeader" runat="server">
                        <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                        <asp:ListItem Value="1">是</asp:ListItem>
                        <asp:ListItem Value="0">否</asp:ListItem>
                    </asp:DropDownList></td>
                
            </tr>
           <tr>
                <td style="width: 10%" align="right">
                    安全等级
                </td>
                <td style="width: 23%" align="left">
                    <asp:DropDownList ID="ddlSafe" runat="server">
                    </asp:DropDownList>
                </td>
                <td colspan="4"></td>
               <td align="center">
                    <asp:ImageButton runat="server" ID="btnQuery" ImageUrl="~/Common/Image/find.gif"
                        OnClick="btnQuery_Click" />
                </td>
           </tr> 
        </table>
        <table border="0" style="width: 99%;">
            <tr>
                <td style="width: 100%; height: 380px;" valign="top" align="center">
                    <table style="width: 100%; height: 380px">
                        <tr>
                            <td style="vertical-align: top;">
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
                                                <asp:CheckBox ID="chkSelect" runat="server" Checked='<%#((","+ViewState["ChooseId"]+",").Contains(","+DataBinder.Eval(Container, "DataItem.EmployeeID")+",") ? true : false) %>'
                                                    Enabled='<%#((","+ViewState["HasExamId"]+",").Contains(","+DataBinder.Eval(Container, "DataItem.EmployeeID")+",") ?  false:true ) %>' />
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
                                        <asp:TemplateColumn HeaderText="员工编码<br>(身份证号码)" SortExpression="a.Work_No">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemTemplate>
                                                <asp:Label ID="LabelWorkNo" runat="server" Text='<%# Bind("StrWorkNo") %>'></asp:Label>
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
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="22px" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <EditItemStyle BackColor="#2461BF" />
                                    <PagerStyle Visible="false" />
                                </asp:DataGrid>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                每页15条记录&nbsp;&nbsp;共<asp:Label ID="lbRecordCount" runat="server"></asp:Label>条记录&nbsp;&nbsp;
                                <asp:LinkButton ID="Fistpage" runat="server" CommandArgument="First" OnClick="Page_OnClick">首页</asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="Prevpage" runat="server" CommandArgument="Prev" OnClick="Page_OnClick">上页</asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="Nextpage" runat="server" CommandArgument="Next" OnClick="Page_OnClick">下页</asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="Lastpage" runat="server" CommandArgument="Last" OnClick="Page_OnClick">尾页</asp:LinkButton>&nbsp;&nbsp;
                                <asp:TextBox ID="txtJumpPage" runat="server" Width="30"></asp:TextBox>/
                                <asp:Label ID="lbPageCount" runat="server"></asp:Label>
                                <asp:Button ID="btnJumpPage" runat="server" Text="转页" OnClick="btnJumpPage_Click" />&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 90%;" align="center">
                    <asp:Button runat="server" ID="btnInput" Text="添加所选职员" CssClass="buttonLong" OnClick="btnInput_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="ButtonInputAll" Text="添加全部职员" CssClass="buttonLong"
                        OnClick="ButtonInputAll_Click" />&nbsp;&nbsp;&nbsp;
                    <asp:Button runat="server" ID="btnClose" Text="关　闭" CssClass="button" OnClientClick="CloseBack()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
