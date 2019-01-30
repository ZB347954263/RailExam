<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectItems.aspx.cs" Inherits="RailExamWebApp.Common.SelectItems" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择试题</title>

    <script src="../Common/JS/jquery.js" type="text/javascript">
    </script>

    <script type="text/javascript">
    </script>

    <script type="text/javascript">        
        
        function tvView_onNodeSelect(sender, eventArgs)
        {   
           var node = eventArgs.get_node(); 
            if(node && node.getProperty("isKnowledge") == "true")
            {
               document.getElementById('hftype').value=1;
            }
            
            if(node && node.getProperty("isBook") == "true")
            {
                document.getElementById('hftype').value=2;
            }
            
            if(node && node.getProperty("isChapter") == "true")
            {
               document.getElementById('hftype').value=3;
            }            
          
            if (node && node.getProperty("isTrainType") == "true")
            {
                 document.getElementById('hftype').value=4;
            }            
            
            if (node && node.getProperty("isCategory") == "true")
            {
               document.getElementById('hftype').value=5;
            }        
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
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        选择试题</div>
                </div>
                <div id="button">
                    <asp:ImageButton runat="server" ID="ImageButton1" CausesValidation="False" ImageUrl="~/Common/Image/query.gif"
                        OnClick="ImageButton1_Click" />
                    <asp:ImageButton runat="server" ID="ButtonSave" CausesValidation="False" ImageUrl="~/Common/Image/confirm.gif"
                        OnClick="ButtonSave_Click" />
                    <img alt="" onclick="return window.close();" src="../Common/Image/cancel.gif" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        查看方式
                        <asp:DropDownList ID="ddlTreeType" runat="server" OnSelectedIndexChanged="ddlTreeType_SelectedIndexChanged"
                            AutoPostBack="true">
                            <asp:ListItem Text="按知识体系" Value="1"></asp:ListItem>
                            <asp:ListItem Text="按培训类别" Value="2"></asp:ListItem>
                            <asp:ListItem Text="按辅助分类" Value="3"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div id="leftContent">
                        <ComponentArt:TreeView ID="tvView" runat="server" EnableViewState="true" AutoPostBackOnSelect="true"
                            OnNodeSelected="tvView_NodeSelected">
                            <ClientEvents>
                                <NodeSelect EventHandler="tvView_onNodeSelect" />
                            </ClientEvents>
                        </ComponentArt:TreeView>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContentWithNoHead">
                        <div id="divquery" style="text-align: left;" runat="server" visible="false">
                            <asp:DropDownList ID="ddlItemDifficulty" runat="server" DataSourceID="odsItemDifficulty"
                                DataTextField="DifficultyName" DataValueField="ItemDifficultyId">
                            </asp:DropDownList>
                            分数<asp:TextBox runat="server" ID="txtScore" MaxLength="3"></asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator2" runat="server" Display="None" ErrorMessage="分数必须为整数！"
                                MaximumValue="999" MinimumValue="0" Type="Integer" ControlToValidate="txtScore"></asp:RangeValidator>
                            <asp:DropDownList ID="ddlUsage" runat="server">
                                <asp:ListItem Text="-试题用途-" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="练习和考试" Value="0"></asp:ListItem>
                                <asp:ListItem Text="仅考试" Value="1"></asp:ListItem>
                            </asp:DropDownList>&nbsp;
                            <asp:ImageButton runat="server" ID="btnQuery" CausesValidation="true" ImageUrl="~/Common/Image/find.gif"
                                OnClick="btnQuery_Click" />
                        </div>
                        <div style="overflow: auto; width: 630px; height: 550px">
                            <asp:DataGrid ID="Grid1" runat="server" HeaderStyle-BackColor="ActiveBorder" Width="98%"
                                PageSize="20" DataKeyField="ItemId" AllowPaging="true" AutoGenerateColumns="False"
                                GridLines="None" AllowCustomPaging="true" OnPageIndexChanged="Grid1_PageIndexChanged">
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            <input id="CheckboxAll" type="checkbox" onclick="CheckAll(this)" title="全选或反选" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Checked='<%#((","+ViewState["ChooseId"]+",").Contains(","+DataBinder.Eval(Container, "DataItem.ItemId")+",") ? true : false) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="试题id" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="LabelItemID" runat="server" Text='<%# Bind("ItemId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="组织机构">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemTemplate>
                                            <asp:Label ID="LabelOrganizationName" runat="server" Text='<%# Bind("OrganizationName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="题型">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemTemplate>
                                            <asp:Label ID="LabelTypeName" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="难度">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemTemplate>
                                            <asp:Label ID="LabelDifficultyName" runat="server" Text='<%# Bind("DifficultyName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="分值">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemTemplate>
                                            <asp:Label ID="LabelScore" runat="server" Text='<%# Bind("Score") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="内容">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="left" Wrap="false" />
                                        <ItemTemplate>
                                            <asp:Label ID="LabelContent" runat="server" Text='<%# Bind("Content") %>'></asp:Label>
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
                    </div>
                </div>
            </div>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
        <asp:HiddenField ID="hftype" runat="server" />
        <asp:ObjectDataSource ID="odsItemDifficulty" runat="server" SelectMethod="GetItemDifficulties"
            TypeName="RailExam.BLL.ItemDifficultyBLL" DataObjectTypeName="RailExam.BLL.ItemDifficulty">
            <SelectParameters>
                <asp:Parameter Name="bForSearchUse" Type="boolean" DefaultValue="true" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
