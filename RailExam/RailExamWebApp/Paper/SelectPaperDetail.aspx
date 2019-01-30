<%@ Page Language="C#" AutoEventWireup="True" Codebehind="SelectPaperDetail.aspx.cs"
    Inherits="RailExamWebApp.Paper.SelectPaperDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�Ծ��б�</title>

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
                    <asp:ListItem Value="0" Text="-���ⷽʽ-"></asp:ListItem>
                    <asp:ListItem Value="1" Text="�ֹ�����"></asp:ListItem>
                    <asp:ListItem Value="2" Text="�������"></asp:ListItem>
                </asp:DropDownList>
                �Ծ�����
                <asp:TextBox ID="txtPaperName" runat="server" Width="10%"></asp:TextBox>
                ������
                <asp:TextBox ID="txtCreatePerson" runat="server" Width="10%"></asp:TextBox>
                <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="��  ѯ" OnClick="btnQuery_Click" />
            </div>
            <div id="grid">
                <ComponentArt:Grid ID="Grid1" runat="server" DataSourceID="odsPaper" PageSize="20"
                    Width="96%">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="PaperId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="PaperId" Visible="false" />
                                <ComponentArt:GridColumn DataField="PaperName" HeadingText="�Ծ�����" Align="Left" />
                                <ComponentArt:GridColumn DataField="CategoryId" Visible="false" />
                                <ComponentArt:GridColumn DataField="CreateMode" HeadingText="���ⷽʽ" Visible="false" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate1" HeadingText="���ⷽʽ"
                                    Visible="false" />
                                <ComponentArt:GridColumn DataField="ItemCount" HeadingText="�趨����" />
                                <ComponentArt:GridColumn DataField="TotalScore" HeadingText="�趨�ܷ�" />
                                <ComponentArt:GridColumn DataField="CurrentItemCount" HeadingText="ʵ������" />
                                <ComponentArt:GridColumn DataField="CurrentTotalScore" HeadingText="ʵ���ܷ�" />
                                <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="������" />
                                <ComponentArt:GridColumn DataField="CreateTime" HeadingText="����ʱ��" FormatString="yyyy-MM-dd" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="CTedit" HeadingText="����" Width="60" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTedit">
                            <a onclick="paperSelect(##DataItem.getMember('PaperId').get_value()## )" href="#"><b>
                                ѡ��</b></a>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate1">
                            ## DataItem.getMember("CreateMode").get_value() == 1 ? "�ֹ�����" : "�������" ##
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
