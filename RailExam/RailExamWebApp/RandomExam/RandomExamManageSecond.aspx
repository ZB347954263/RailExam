<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamManageSecond.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamManageSecond" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>组卷策略大题</title>

    <script type="text/javascript">              
        function deleteItem(id)
        {
            if(document.getElementById('hfMode').value != "ReadOnly")
            {
                if(confirm('您确定要删除该大题和其下面的取题范围吗 ? '))
                {
                    form1.DeleteID.value = id;
                    form1.submit();
                    form1.DeleteID.value = "";
                }
            }
        } 		
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="parent">
                        新增考试</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        考试大题</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td colspan="3">
                            <b>第二步：设置考试大题</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            考试名称：
                            <asp:Label ID="txtPaperName" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="bottom">
                            可选题型：</td>
                        <td>
                            已选大题：
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 20%;">
                            <div style="width: 50%; float: left;">
                                <asp:ListBox ID="lbType" runat="server" Width="100px" Height="300px" ></asp:ListBox>
                            </div>
                            <div style="width: 23%; float: right; vertical-align: bottom; line-height: 130px;">
                                <br />
                                <asp:Button ID="btnInput" runat="server" CausesValidation="false" CssClass="buttonGo"
                                    Text=">" ToolTip="加入左边选择的题型" OnClick="btnInput_Click" />
                            </div>
                        </td>
                        <td>
                            <div style="overflow: auto; height: 316px">
                                <asp:GridView ID="Grid1" runat="server" DataKeyNames="RandomExamSubjectId" DataSourceID="odsPaperStrategySubject"
                                    HeaderStyle-BackColor="ActiveBorder" AutoGenerateColumns="False" Width="97%"
                                    ForeColor="#333333" OnRowDataBound="Grid1_RowDataBound" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderText="题号">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfPaperStrategySubjectId" runat="server" Value='<%# Bind("RandomExamSubjectId") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="题型">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTypeName" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                                                <asp:HiddenField ID="hfItemTypeId" runat="server" Value='<%# Bind("ItemTypeId") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="标题">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtSubjectName" Width="130px" runat="server" Text='<%# Bind("SubjectName") %>'></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvSubjectName" runat="server" Display="None" ControlToValidate="txtSubjectName"
                                                    ErrorMessage="“标题”不能为空！"></asp:RequiredFieldValidator>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="题数">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtItemCount" Width="90px" runat="server" Text='<%# Bind("ItemCount") %>'></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" Display="None" ErrorMessage="题数必须为大于0的整数！"
                                                    MaximumValue="99999999" MinimumValue="1" Type="Integer" ControlToValidate="txtItemCount"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="每题分数">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtUnitScore" Width="90px" runat="server" Text='<%# Bind("UnitScore") %>'></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" Display="None" ErrorMessage="每题分数必须为整数！"
                                                    MaximumValue="9999" MinimumValue="0" Type="Double" ControlToValidate="txtUnitScore"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="操作">
                                            <ItemTemplate>
                                                <a onclick='deleteItem("<%# Eval("RandomExamSubjectId") %>");' href="#">
                                                    <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
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
                        </td>
                    </tr>
                </table>
                <div id="button" style="text-align: center;">
                    <br />
                    <asp:ImageButton ID="btnLast" runat="server" CausesValidation="False" OnClick="btnLast_Click"
                        ImageUrl="../Common/Image/last.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnSaveAndNext" runat="server" OnClick="btnSaveAndNext_Click"
                        ImageUrl="../Common/Image/next.gif" CausesValidation="true" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                        ImageUrl="../Common/Image/cancel.gif" />
                </div>
            </div>
        </div>
        <input type="hidden" name="DeleteID" />
        <asp:HiddenField ID="hfMode" runat="server" />
        <asp:HiddenField ID="HfRandomExamid" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
        <asp:ObjectDataSource ID="odsPaperStrategySubject" runat="server" SelectMethod="GetRandomExamSubjectByRandomExamId"
            TypeName="RailExam.BLL.RandomExamSubjectBLL">
            <SelectParameters>
                <asp:QueryStringParameter Name="RandomExamId" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
