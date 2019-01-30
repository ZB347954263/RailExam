<%@ Page Language="C#" AutoEventWireup="True" Codebehind="SelectPaperDetail.aspx.cs"
    Inherits="RailExamWebApp.Paper.SelectPaperDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试卷列表</title>

    <script type="text/javascript"> 
        function paperSelect(id)
        {
//            top.window.opener.form1.PaperId.value = selectValue;
//            top.window.opener.form1.submit();
//            top.window.close()
            window.returnValue = id;
            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="gridPage">
            <div id="query">
                <asp:DropDownList ID="ddlType" runat="server" Width="15%">
                    <asp:ListItem Value="0" Text="-出题方式-"></asp:ListItem>
                    <asp:ListItem Value="1" Text="手工出题"></asp:ListItem>
                    <asp:ListItem Value="2" Text="随机出题"></asp:ListItem>
                </asp:DropDownList>
                试卷名称
                <asp:TextBox ID="txtPaperName" runat="server" Width="10%"></asp:TextBox>
                出卷人
                <asp:TextBox ID="txtCreatePerson" runat="server" Width="10%"></asp:TextBox>
                <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="查  询" OnClick="btnQuery_Click" />
            </div>
            <div id="grid">
                <ComponentArt:Grid ID="Grid1" runat="server" DataSourceID="odsPaper" PageSize="20"
                    Width="96%">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="PaperId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="PaperId" Visible="false" />
                                <ComponentArt:GridColumn DataField="PaperName" HeadingText="试卷名称" Align="Left" />
                                <ComponentArt:GridColumn DataField="CategoryId" Visible="false" />
                                <ComponentArt:GridColumn DataField="CreateMode" HeadingText="出题方式" Visible="false" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate1" HeadingText="出题方式"
                                    Visible="false" />
                                <ComponentArt:GridColumn DataField="ItemCount" HeadingText="设定题数" />
                                <ComponentArt:GridColumn DataField="TotalScore" HeadingText="设定总分" />
                                <ComponentArt:GridColumn DataField="CurrentItemCount" HeadingText="实际题数" />
                                <ComponentArt:GridColumn DataField="CurrentTotalScore" HeadingText="实际总分" />
                                <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="出卷人" />
                                <ComponentArt:GridColumn DataField="CreateTime" HeadingText="出卷时间" FormatString="yyyy-MM-dd" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="CTedit" HeadingText="操作" Width="60" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTedit">
                            <a onclick="paperSelect(##DataItem.getMember('PaperId').get_value()## )" href="#"><b>
                                选择</b></a>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate1">
                            ## DataItem.getMember("CreateMode").get_value() == 1 ? "手工出题" : "随机出题" ##
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsPaper" runat="server" SelectMethod="GetPaperByCategoryIDPath"
            TypeName="RailExam.BLL.PaperBLL">
            <SelectParameters>
                <asp:QueryStringParameter Name="idPath" QueryStringField="id" Type="String" />
                <asp:ControlParameter ControlID="ddlType" Type="Int32" PropertyName="SelectedValue"
                    Name="CreateMode" />
                <asp:ControlParameter ControlID="txtPaperName" Type="String" PropertyName="Text"
                    Name="PaperName" />
                <asp:ControlParameter ControlID="txtCreatePerson" Type="String" PropertyName="Text"
                    Name="CreatePerson" />
                <asp:SessionParameter SessionField="StationOrgID" Name="OrgId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
