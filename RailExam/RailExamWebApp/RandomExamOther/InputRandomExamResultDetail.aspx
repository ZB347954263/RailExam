<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InputRandomExamResultDetail.aspx.cs"
    Inherits="RailExamWebApp.RandomExamOther.InputRandomExamResultDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>登记成绩</title>

    <script type="text/javascript">
       function unload()
       {
//            window.opener.form1.Refresh.value = "true";
//            window.opener.form1.submit();
//            window.opener.form1.Refresh.value = "";
            top.returnValue = "true";
            top.close();
       }
        
    </script>

</head>
<body onunload="unload()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div style="width: 15%; float: left;">
                    <div style="color: #2D67CF; float: left;">
                        登记成绩</div>
                </div>
                <div id="button">
                    <asp:Button ID="btnEnd" runat="server" Text="登记完毕" CssClass="button" OnClick="btnEnd_Click"
                        OnClientClick="return confirm('你确定登记完毕吗？登记完毕后不能再修改成绩！');" />
                </div>
            </div>
            <div id="content">
                <table class="contentTableCenter">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="Grid1" runat="server" HeaderStyle-BackColor="ActiveBorder" AutoGenerateColumns="False"
                                Width="97%" ForeColor="#333333" GridLines="None" OnRowCancelingEdit="Grid1_RowCancelingEdit"
                                DataKeyNames="RandomExamResultID" OnRowEditing="Grid1_RowEditing" OnRowUpdating="Grid1_RowUpdating"
                                OnRowDeleting="Grid1_RowDeleting" AllowPaging="True" PageSize="15" OnPageIndexChanging="Grid1_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="考生姓名">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfID" runat="server" Value='<%# Bind("RandomExamResultID") %>' />
                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("ExamineeName") %>'></asp:Label>
                                            <asp:HiddenField ID="hfExamineeID" runat="server" Value='<%# Bind("ExamineeID") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="工资编号">
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblWorkNo" runat="server" Text='<%# Bind("WorkNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="职名">
                                        <ItemStyle HorizontalAlign="Center" Width="15%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblPostName" runat="server" Text='<%# Bind("PostName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="考生单位">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrganizationName" runat="server" Text='<%# Bind("OrganizationName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="考试地点">
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblExamOrgName" runat="server" Text='<%# Bind("ExamOrgName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="成绩">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblScore" runat="server" Text='<%# Bind("Score") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtScore" Width="90px" runat="server" Text='<%# Bind("Score") %>'></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" Display="None" ErrorMessage="分数必须为数字！"
                                                MaximumValue="100" MinimumValue="0" Type="Double" ControlToValidate="txtScore"></asp:RangeValidator>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnModify" runat="server" ImageUrl="../Common/Image/edit_col_edit.gif"
                                                AlternateText="修改" CommandName="Edit"></asp:ImageButton>&nbsp;&nbsp;
                                            <asp:ImageButton ID="btnDel" runat="server" ImageUrl="../Common/Image/edit_col_Delete.gif"
                                                AlternateText="删除成绩" CommandName="Delete" OnClientClick="return confirm('您确定要删除该学员的成绩吗？');">
                                            </asp:ImageButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="../Common/Image/edit_col_save.gif"
                                                AlternateText="保存" CommandName="Update"></asp:ImageButton>&nbsp;&nbsp;
                                            <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="../Common/Image/edit_col_cancel.gif"
                                                AlternateText="取消" CommandName="Cancel" CausesValidation="False"></asp:ImageButton>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="left" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
    </form>
</body>
</html>
