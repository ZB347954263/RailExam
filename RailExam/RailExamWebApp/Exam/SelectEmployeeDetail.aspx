<%@ Page Language="C#" AutoEventWireup="True" Codebehind="SelectEmployeeDetail.aspx.cs"
    Inherits="RailExamWebApp.Exam.SelectEmployeeDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择考生</title>

    <script type="text/javascript">
    
        function tvPost_onNodeSelect(sender, eventArgs)
        {
            // 查看节点
            var node = eventArgs.get_node(); 
            
        }
        
        function tvOrg_onNodeSelect(sender, eventArgs)
        {
         var node = eventArgs.get_node();   
        }
        
           function CheckAll(oCheckbox)
        {
            var Grid1 = document.getElementById("Grid1");
            for(i = 1;i < Grid1.rows.length; i ++)
            {               
            if(Grid1.rows[i].cells[0].getElementsByTagName("INPUT")[0] !=null)
            {
                Grid1.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;     
            }
            }
        }
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="0" style="width: 99%; height: 80%;">
                <tr>
                    <td style="width: 14%; height: 551px;" valign="top">
                        <table>
                            <tr>
                                <td style="height: 24px;">
                                    <asp:RadioButtonList runat="server" ID="rdo1" RepeatDirection="Horizontal" AutoPostBack="true"
                                        RepeatLayout="Flow" OnSelectedIndexChanged="rdo1_SelectedIndexChanged">
                                        <asp:ListItem Value="0" Text="组织机构" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="工作岗位"></asp:ListItem>
                                    </asp:RadioButtonList></td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <ComponentArt:TreeView ID="tvOrg" EnableViewState="true" runat="server" Width="140px"
                                        Height="550px" AutoPostBackOnSelect="true" OnNodeSelected="tvOrg_NodeSelected">
                                        <ClientEvents>
                                            <NodeSelect EventHandler="tvPost_onNodeSelect" />
                                        </ClientEvents>
                                    </ComponentArt:TreeView>
                                    <ComponentArt:TreeView ID="tvPost" EnableViewState="true" runat="server" Width="140px"
                                        Height="550px" Visible="false" AutoPostBackOnSelect="true" OnNodeSelected="tvPost_NodeSelected">
                                        <ClientEvents>
                                            <NodeSelect EventHandler="tvPost_onNodeSelect" />
                                        </ClientEvents>
                                    </ComponentArt:TreeView>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="left" valign="top" style="width: 86%; height: 541px;">
                        <table>
                            <tr>
                                <td align="left" style="width: 100%">
                                    姓名&nbsp;
                                    <asp:TextBox runat="server" ID="TextBoxPapername" Width="80px"></asp:TextBox>
                                    &nbsp;职名&nbsp;
                                    <asp:TextBox runat="server" ID="txtPostName" Width="80px"></asp:TextBox>
                                    &nbsp; 员工编码&nbsp;<asp:TextBox runat="server" ID="textgh" Width="80px"></asp:TextBox>
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:ImageButton runat="server" ID="btnQuery" OnClick="btnQuery_Click" ImageUrl="~/Common/Image/find.gif" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 90%; height: 250px;" valign="top">
                                    <div style="overflow: auto; width: 630px; height: 250px">
                                        <asp:DataGrid ID="Grid1" runat="server" HeaderStyle-BackColor="ActiveBorder" Width="98%"
                                            PageSize="9" DataKeyField="EmployeeID" AllowPaging="true" AutoGenerateColumns="False"
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
                                                <asp:TemplateColumn HeaderText="工作岗位" SortExpression="b.Post_Name">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" Wrap="false" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelPostName" runat="server" Text='<%# Bind("PostName") %>'></asp:Label>
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
                                    <asp:Button runat="server" ID="ButtonInputAll" Text=" ↓↓ " ToolTip="加入全部职员" OnClick="ButtonInputAll_Click" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="btnInput" Text="  ↓  " ToolTip="加入所选的职员" OnClick="btnInput_Click" />&nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="ButtonOutPut" Text="  ↑  " ToolTip="移除所选的考生" OnClick="ButtonOutPut_Click" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button runat="server" ID="ButtonOutPutAll" Text=" ↑↑ " ToolTip="移除全部考生" OnClick="ButtonOutPutAll_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 90%; height: 250px;" valign="top">
                                    <div style="overflow: auto; width: 630px; height: 250px">
                                        <asp:GridView ID="Grid2" runat="server" HeaderStyle-BackColor="ActiveBorder" Width="99%"
                                            DataKeyNames="EmployeeID" AllowPaging="False" AutoGenerateColumns="False" ForeColor="#333333"
                                            GridLines="None" AllowSorting="true" OnSorting="Grid2_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="选择">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect2" runat="server" Visible='<%#((","+ViewState["HasExamId"]+",").Contains(","+DataBinder.Eval(Container, "DataItem.EmployeeID")+",") ?  false:true ) %>' />
                                                        <asp:Image runat="server" ImageUrl="~/Common/Image/charge.gif" ToolTip="已参加考试！" Visible='<%#((","+ViewState["HasExamId"]+",").Contains(","+DataBinder.Eval(Container, "DataItem.EmployeeID")+",") ? true : false) %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="组织机构" SortExpression="OrgName">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" Wrap="false" Width="150px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Labelorgid" runat="server" Text='<%# Bind("OrgName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="职员id" Visible="false">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelEmployeeID" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="姓名" SortExpression="EmployeeName">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" Width="130px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelName" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="员工编码" SortExpression="WorkNo">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" Width="60px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelWorkNo" runat="server" Text='<%# Bind("WorkNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="工作岗位" SortExpression="PostName">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="left" Width="160" Wrap="false" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="LabelPostName" runat="server" Text='<%# Bind("PostName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="22px" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:ImageButton ID="btnLast" runat="server" CausesValidation="False" OnClick="btnLast_Click"
                                        ImageUrl="../Common/Image/last.gif" />
                                    <asp:ImageButton runat="server" ID="ButtonSave" CausesValidation="False" ImageUrl="~/Common/Image/save.gif"
                                        OnClick="ButtonSave_Click" />
                                    <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                                        ImageUrl="../Common/Image/close.gif" />
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
