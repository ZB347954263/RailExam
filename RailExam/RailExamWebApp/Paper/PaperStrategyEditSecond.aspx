<%@ Page Language="C#" AutoEventWireup="True" Codebehind="PaperStrategyEditSecond.aspx.cs"
    Inherits="RailExamWebApp.Paper.PaperStrategyEditSecond" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组卷策略大题</title>

    <script type="text/javascript">              
        function deleteItem(id)
        {
            if(document.getElementById('hfMode').value != "ReadOnly")
            {
                if(confirm('您确定要删除该大题和其下面的组卷策略吗 ? '))
                {
                    form1.DeleteID.value = id;
                    form1.submit();
                    form1.DeleteID.value = "";
                }
            }
        }
        
        function mouseIsInXButton()
        {
        return (event.clientY < 0)  &&  ((document.body.offsetWidth - event.clientX) < 30);
        }      
		
		function logout()
		{	
		    
		    if(mouseIsInXButton() || event.altKey || event.ctlKey )
		    {
		    var paperid=document.getElementById("Hfpaperid").value; 
		    var id=document.getElementById("HfstrategyID").value; 		    
		   
		    if(paperid!=null&&paperid!="")
		    {		    
		    window.opener.form1.strategyID.value=id;
		    window.opener.form1.submit();
		   // window.close();		    
		    }			   
		    }
		}			
		
    </script>

</head>
<body onbeforeunload="logout()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        组卷策略</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        组卷策略大题</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td colspan="3">
                            <b>第二步：组卷策略大题</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            组卷策略名称：
                            <asp:Label ID="txtPaperName" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="3">
                            请确定组卷策略大题信息：</td>
                    </tr>--%>
                    <tr>
                        <td colspan="2" valign="bottom">
                            可选题型：</td>
                        <td>
                            已选大题：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnSaveAs" runat="server" CssClass="buttonSearch" Text="保  存" OnClick="btnSaveAs_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 20%;">
                            <div style="width: 50%; float: left;">
                                <asp:ListBox ID="lbType" runat="server" Width="100px" Height="300px" DataSourceID="odsItemType"
                                    DataTextField="TypeName" DataValueField="ItemTypeId"></asp:ListBox>
                            </div>
                            <div style="width: 23%; float: right; vertical-align: bottom; line-height: 130px;">
                                <br />
                                <asp:Button ID="btnInput" runat="server" CausesValidation="false" CssClass="buttonGo"
                                    Text=">" ToolTip="加入左边选择的题型" OnClick="btnInput_Click" />
                            </div>
                        </td>
                        <td>
                            <div style="overflow: auto; height: 316px">
                                <asp:GridView ID="Grid1" runat="server" DataKeyNames="PaperStrategySubjectId" DataSourceID="odsPaperStrategySubject"
                                    HeaderStyle-BackColor="ActiveBorder" AutoGenerateColumns="False" Width="97%"
                                    ForeColor="#333333" OnRowDataBound="Grid1_RowDataBound" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderText="题号">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblID" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfPaperStrategySubjectId" runat="server" Value='<%# Bind("PaperStrategySubjectId") %>' />
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
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" Display="None" ErrorMessage="题数必须为整数！"
                                                    MaximumValue="99999999" MinimumValue="0" Type="Integer" ControlToValidate="txtItemCount"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="每题分数">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtUnitScore" Width="90px" runat="server" Text='<%# Bind("UnitScore") %>'></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator2" runat="server" Display="None" ErrorMessage="每题分数必须为整数！"
                                                    MaximumValue="9999" MinimumValue="0" Type="Integer" ControlToValidate="txtUnitScore"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>                                      
                                      
                                        
                                        
                                        <asp:TemplateField HeaderText="操作">
                                            <ItemTemplate>
                                                <a onclick='deleteItem("<%# Eval("PaperStrategySubjectId") %>");' href="#">
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
                        ImageUrl="../Common/Image/next.gif"  CausesValidation="true"/>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                        ImageUrl="../Common/Image/cancel.gif" />
                </div>
            </div>
        </div>
        <input type="hidden" name="DeleteID" />
        <asp:HiddenField ID="hfMode" runat="server" />
        <asp:HiddenField ID="Hfpaperid" runat="server" />
        <asp:HiddenField ID="HfstrategyID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
        <asp:ObjectDataSource ID="odsItemType" runat="server" SelectMethod="GetItemTypes"
            TypeName="RailExam.BLL.ItemTypeBLL"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsPaperStrategySubject" runat="server" SelectMethod="GetPaperStrategySubjectByPaperStrategyId"
            TypeName="RailExam.BLL.PaperStrategySubjectBLL">
            <SelectParameters>
                <asp:QueryStringParameter Name="PaperId" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
