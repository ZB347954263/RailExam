<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ExamManageThird.aspx.cs"
    Inherits="RailExamWebApp.Exam.ExamManageThird" %>

<%@ Register Src="../Common/Control/Date/DateTimeUC.ascx" TagName="DateTimeUC" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�������š�ָ��������</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function ChooseUserIds(id, id1)
        {
            form1.ExamArrangeId.value = id;
           var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-900)*.5;   
            ctop=(screen.availHeight-600)*.5;   
               
            var re = window.open("SelectEmployeeDetail.aspx?op=1&id="+id1,"SelectEmployeeDetail"," Width=900px; Height=600px,status=false,left="+cleft+",top="+ctop+",resizable=no",true);
            re.focus();
        }
        
        function ChooseJudgeIds(id, id1)
        {
           var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-900)*.5;   
            ctop=(screen.availHeight-600)*.5;   
              
              
            form1.ExamArrangeId.value = id;

            var re = window.open("SelectEmployeeDetail.aspx?op=2&id="+id1,"SelectEmployeeDetail"," Width=900px; Height=600px,status=false,left="+cleft+",top="+ctop+",resizable=no",true);
            re.focus();
        }
        
        function deleteItem(id)
        {
            if(document.getElementById('hfMode').value != "ReadOnly")
            {
                if(confirm('��ȷ��Ҫɾ���ÿ��������� ? '))
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
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        �������</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        �������š�ָ��������</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td>
                            <b>���������������š�ָ��������</b> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAdd" runat="server" CssClass="buttonSearch" Text="��  ��" OnClick="btnAdd_Click"
                                CausesValidation="False" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="overflow: auto; height: 460px; width: 776px;">
                                <asp:GridView ID="Grid1" runat="server" DataSourceID="odsExamArrange" HeaderStyle-BackColor="ActiveBorder"
                                    Width="97.8%" DataKeyNames="ExamArrangeId" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderText="����">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtUserIds" runat="server" Text='<%# Bind("UserIds")  %>' ReadOnly="true"
                                                    Visible="false"></asp:TextBox>
                                                <a onclick="ChooseUserIds('<%# DataBinder.Eval(Container.DataItem,"ExamArrangeId") %>','<%# DataBinder.Eval(Container.DataItem,"UserIds") %>')"
                                                    href="#"><b>ѡ����</b></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="��ʼʱ��">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="170px" />
                                            <ItemTemplate>
                                                <uc1:DateTimeUC ID="dateBeginTime" runat="server" DateValue='<%# Bind("BeginTime","{0:yyyy-MM-dd HH:mm:ss}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="����ʱ��">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="170px" />
                                            <ItemTemplate>
                                                <uc1:DateTimeUC ID="dateEndTime" runat="server" DateValue='<%# Bind("EndTime","{0:yyyy-MM-dd HH:mm:ss}") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="������">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" Width="90px" />
                                            <ItemTemplate>
                                                <a onclick="ChooseJudgeIds('<%# DataBinder.Eval(Container.DataItem,"ExamArrangeId") %>','<%# DataBinder.Eval(Container.DataItem,"JudgeIds") %>')"
                                                    href="#"><b>ѡ��������</b></a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="����">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtJudgeIds" runat="server" Text='<%# Bind("JudgeIds") %>' ReadOnly="true"
                                                    Visible="false"></asp:TextBox>
                                                <asp:HiddenField ID="hfExamArrangeId" runat="server" Value='<%# Bind("ExamArrangeId") %>' />
                                                <a onclick='deleteItem("<%# Eval("ExamArrangeId") %>");' href="#">
                                                    <img alt="ɾ��" border="0" src="../Common/Image/edit_col_delete.gif" /></a>
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
                </table>
                <div id="button" style="text-align: center;">
                    <br />
                    <asp:ImageButton ID="btnLast" runat="server" CausesValidation="False" OnClick="btnLast_Click"
                        ImageUrl="../Common/Image/last.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnSave" runat="server" OnClick="btnSave_Click" ImageUrl="../Common/Image/complete.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                        ImageUrl="../Common/Image/cancel.gif" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfMode" runat="server" />
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="ExamArrangeId" />
        <input type="hidden" name="UserIds" />
        <input type="hidden" name="JudgeIds" />
        <asp:ObjectDataSource ID="odsExamArrange" runat="server" SelectMethod="GetExamArrangesByExamId"
            TypeName="RailExam.BLL.ExamArrangeBLL">
            <SelectParameters>
                <asp:QueryStringParameter Name="ExamId" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
