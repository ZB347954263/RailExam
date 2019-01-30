<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowComputerServer.aspx.cs" Inherits="RailExamWebApp.RandomExam.ShowComputerServer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>服务器状态</title>
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
                        各服务器状态</div>
                </div>
            </div>
            <div id="content">
                <div style="overflow: auto; height: 550px; width: 780px;">
                <table width="100%">
                    <tr>
                        <td colspan="4">
                            <div style="overflow: auto; height: 450px; width: 780px;">
                                <asp:GridView ID="gvChoose" runat="server" HeaderStyle-BackColor="ActiveBorder" Width="99%"
                                     AllowPaging="False" AutoGenerateColumns="False" ForeColor="#333333"
                                    GridLines="None" AllowSorting="false" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="站段名称">
                                            <HeaderStyle HorizontalAlign="Center"  Wrap="false" Width="15%"/>
                                            <ItemStyle HorizontalAlign="Center"  Wrap="false" Width="15%"/>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOrgName" runat="server" Text='<%# Bind("OrganizationName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="服务器编号" >
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="10%"/>
                                            <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="false"/>
                                            <ItemTemplate>
                                                <asp:Label ID="lblServerNo" runat="server" Text='<%# Bind("ComputerServerNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="服务器名称">
                                            <HeaderStyle HorizontalAlign="Center" Width="15%" Wrap="false"/>
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblServerName" runat="server" Text='<%# Bind("ComputerServerName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="已下载">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="10%"/>
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" Width="10%" />
                                            <ItemTemplate>
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Common/Image/charge.gif" ToolTip="已下载！"
                                                    Visible='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.DownloadedStatus")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="已生成试卷">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="10%"/>
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" Width="10%" />
                                            <ItemTemplate>
                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Common/Image/charge.gif" ToolTip="已生成试卷！"
                                                   Visible='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.HasPaperStatus")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="考试状态" >
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" Wrap="false"/>
                                            <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="false" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatusName" runat="server" Text='<%# Bind("StatusName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <asp:TemplateField HeaderText="最近一次上传时间" >
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="20%" />
                                            <ItemStyle HorizontalAlign="Center" Width="20%" Wrap="false" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblLastUploadDate" runat="server" Text='<%# Bind("LastUploadDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                        <asp:TemplateField HeaderText="已完全上传">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="10%"/>
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" Width="10%" />
                                            <ItemTemplate>
                                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Common/Image/charge.gif" ToolTip="已完全上传！"
                                                   Visible='<%# Convert.ToBoolean(DataBinder.Eval(Container, "DataItem.IsUploadStatus")) %>' />
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
                </table>
              </div> 
            </div>
        </div>
        <input type="hidden" name="Refresh" />
        <textarea rows="10" cols="30" name="ChooseID" style="display: none"></textarea>
        <input type="hidden" name="ChooseExamID" />
        <input type="hidden" name="RefreshArrange" />
    </form>
</body>
</html>
