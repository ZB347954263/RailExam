<%@ Page Language="C#" AutoEventWireup="true" Codebehind="MultiSelectItem.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.MultiSelectItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择试题</title>
    <base target="_self" />

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
        

//      	var search = window.dialogArguments;
//      	alert(search.document.getElementById("HfExCludeChaptersId").value);
//      	document.getElementById("hfExcludes").value = search.document.getElementById("HfExCludeChaptersId").value;
         
      function showItemDetail(id) 
      {
      	  var viewWindow = window.open('../Item/ItemDetail.aspx?mode=readonly&id=' + id,
                        'ItemDetail','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
          viewWindow.focus();
      }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table class="contentTable">
            <tr>
                <td style="width: 10%" align="right">
                    教材章节
                </td>
                <td style="width: 23%" align="left" colspan="6">
                    <asp:Label runat="server" ID="lblChapterName"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10%" align="right">
                    试题内容
                </td>
                <td style="width: 23%" align="left" colspan="3">
                    <asp:TextBox runat="server" ID="txtContent"></asp:TextBox>
                </td>
                <td style="width: 10%" align="right">
                    试题难度</td>
                <td style="width: 22%" align="left">
                    <asp:DropDownList ID="ddlDiff" runat="server">
                        <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
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
                    <table style="width: 100%; height: 380px">
                        <tr>
                            <td style="vertical-align: top;">
                                <asp:DataGrid ID="Grid1" runat="server" HeaderStyle-BackColor="ActiveBorder" Width="98%"
                                    PageSize="15" DataKeyField="Item_ID" AllowPaging="true" AutoGenerateColumns="False"
                                    OnPageIndexChanged="Grid1_PageIndexChanging" GridLines="None" AllowSorting="true"
                                    AllowCustomPaging="true" OnSortCommand="Grid1_Sorting">
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <HeaderTemplate>
                                                <input id="CheckboxAll" type="checkbox" onclick="CheckAll(this)" title="全选或反选" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" Checked='<%#((","+ViewState["ChooseId"]+",").Contains(","+DataBinder.Eval(Container, "DataItem.Item_ID")+",") ? true : false) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="ItemID" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="LabelItemID" runat="server" Text='<%# Bind("Item_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="试题内容" SortExpression="a.content">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="true" />
                                            <ItemTemplate>
                                                <asp:Label ID="LabelName" runat="server" Text='<%# Bind("content") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="题型" SortExpression="a.TypeName">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemTemplate>
                                                <asp:Label ID="LabelWorkNo" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="试题难度" SortExpression="a.DIFFICULTY_ID">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSex" runat="server" Text='<%# Bind("DIFFICULTY_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="操作">
                                            <itemtemplate>
                                            <a href="#" onclick="showItemDetail('<%#DataBinder.Eval(Container, "DataItem.Item_ID") %>')" style="cursor: hand" title="查看试题">
                                                <img src="../Common/Image/edit_col_edit.gif" border="0"/>
                                            </a>
                                        </itemtemplate>
                                            <headerstyle horizontalalign="Center" />
                                            <itemstyle horizontalalign="Center" width="10%" />
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
                                共<asp:Label ID="lbRecordCount" runat="server"></asp:Label>条记录&nbsp;&nbsp;
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
                    <asp:Button runat="server" ID="btnInput" Text="确   定" CssClass="buttonLong" OnClick="btnInput_Click" />&nbsp;&nbsp;&nbsp;
                    <input id="btnClose" value="关　闭" class="button" onclick="window.close();" type="button" />
                </td>
            </tr>
        </table>
        <asp:HiddenField runat="server" ID="hfExcludes" />
    </form>
</body>
</html>
