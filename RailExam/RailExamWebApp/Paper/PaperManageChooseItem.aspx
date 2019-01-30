<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="PaperManageChooseItem.aspx.cs" Inherits="RailExamWebApp.Paper.PaperManageChooseItem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试卷试题</title>
     <script type="text/javascript" src="../Common/JS/Common.js"></script>
    <script type="text/javascript">   
        function selectItem()
        {
            var itemtype= document.getElementById('hfItemType').value ;
            var paperId=document.getElementById('hfPaperId').value ;
           
	        var selectedItem = window.showCommonDialog('/RailExamBao/common/SelectItems.aspx?paperId='+paperId+'&&ItemType=' +itemtype, 
                 'dialogWidth:900px;dialogHeight:660px;'); 
          
               
            if(! selectedItem)
            {
                return false;
            }
            else
            {
                document.getElementById('hfItemIds').value = selectedItem;
                return true;
            }
 
        }
        
        function ManagePaper(id)
        {
        var Grid1 = document.getElementById("Grid1");
        
          
         if(Grid1==null||Grid1.rows.length==0)
         {
           alert("请选择试题！");
           return;
         }
         
         
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5;              
          
           var re = window.open("PaperPreview.aspx?id="+id,"PaperPreview","Width=800px; Height=600px,status=false,resizable=yes,left="+cleft+",top="+ctop+",scrollbars=yes",true);
           re.focus();
        }
        
        
    
        function CheckAll(oCheckbox)
        {
            var Grid1 = document.getElementById("Grid1");
            for(i = 1;i < Grid1.rows.length; i ++)
            {
                Grid1.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
  
        function Delete()
        {
            return confirm('您确定要删除所选择的试题吗 ? ');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        试卷管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        试卷试题</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td colspan="2">
                            <b>第三步：试卷试题</b>
                        </td>
                    </tr>
                    <tr>
                        <td valign="bottom">
                            试卷大题：</td>
                        <td>
                            试题列表：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSelectItem" runat="server" CssClass="buttonSearch" Text="选择试题" OnClientClick="selectItem();" OnClick="btnSelectItem_Click" />
                            <asp:Button ID="btnDelete" runat="server" CssClass="buttonSearch" Text="删  除" OnClientClick="Delete();" OnClick="btnDelete_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            <div style="float:left;">
                                <asp:ListBox runat="server" ID="lbType" Height="400px" Width="180px" AutoPostBack="true"
                                    OnSelectedIndexChanged="lbType_SelectedIndexChanged"></asp:ListBox>
                            </div>
                        </td>
                        <td>
                            <div style="overflow: auto; height: 416px">
                                <asp:GridView ID="Grid1" runat="server" DataKeyNames="PaperItemId" DataSourceID="odsPaperItem"
                                    HeaderStyle-BackColor="ActiveBorder" AutoGenerateColumns="False" Width="97%"
                                    ForeColor="#333333" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <input id="Checkbox2" type="checkbox" onclick="CheckAll(this)" title="全选或反选" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="题型">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="50"/>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTypeName" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                                                <asp:HiddenField ID="hfTypeId" runat="server" Value='<%# Bind("TypeId") %>' />
                                                <asp:HiddenField ID="hfPaperItemId" runat="server" Value='<%# Bind("PaperItemId") %>' />
                                                <asp:HiddenField ID="hfItemId" runat="server" Value='<%# Bind("ItemId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="试题内容">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" Width="300"/>
                                            <ItemTemplate>
                                                <asp:Label ID="txtContent" runat="server" Text='<%# Bind("Content") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="难度">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="40"/>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDifficultyId" runat="server" Text='<%# Bind("DifficultyId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="分数">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="40"/>
                                            <ItemTemplate>
                                                <asp:Label ID="lblScore" runat="server" Text='<%# Bind("Score") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="操作" Visible="False">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="False" Width="40"/>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" Text="删除"></asp:LinkButton>
                                            </ItemTemplate>
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
                    <tr>
                        <td colspan="2">
                            本大题设定的总分为<asp:Label ID="lblTotalScore" runat="server"></asp:Label>&nbsp;&nbsp;题数为<asp:Label ID="LabelItemCount" runat="server"></asp:Label>&nbsp;&nbsp;实际总分数为<asp:Label ID="LabelCurrentTotalScore" runat="server"></asp:Label>&nbsp;&nbsp;实际题数为<asp:Label ID="LabelCurrentItemCount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="button" style="text-align:center;">
                    <br />
                    <asp:ImageButton ID="btnLast" runat="server" CausesValidation="False" OnClick="btnLast_Click"
                        ImageUrl="../Common/Image/last.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnSave" runat="server"   ImageUrl="../Common/Image/preview.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                        ImageUrl="../Common/Image/close.gif" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfItemIds" runat="server" />
        <%--<input type="hidden" name="ItemIds" />--%>
        <asp:ObjectDataSource ID="odsPaperItem" runat="server" TypeName="RailExam.BLL.PaperItemBLL" SelectMethod="GetItemsByPaperSubjectId">
            <SelectParameters>
                <asp:ControlParameter ControlID="lbType" Name="PaperSubjectId" PropertyName="SelectedValue"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
       <asp:HiddenField  ID="hfItemType" runat="server"  /> 
       <asp:HiddenField  ID="hfPaperId" runat="server"  /> 
    </form>
</body>
</html>
