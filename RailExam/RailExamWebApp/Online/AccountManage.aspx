<%@ Import Namespace="RailExamWebApp.Common.Class"%>
<%@ Page Language="C#" AutoEventWireup="True" Codebehind="AccountManage.aspx.cs"
    Inherits="RailExamWebApp.Online.AccountManage" %>

<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�ʻ�����</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function ChangePassword()
        {
        
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-350)*.5;   
          ctop=(screen.availHeight-200)*.5;    
          var ret = window.open('ChangePassword.aspx','ChangePassword','Width=350px; Height=200px,status=false,left='+cleft+',top='+ctop+',resizable=no',true);
          ret.focus();
        }
        
        function init()
        {
             var isWuhan = document.form1.isWuhan.value;
             if(isWuhan == "False")
             {
	             document.getElementById("studyRecord").style.display = "";   
	         }
	         else
	         {
	             document.getElementById("studyRecord").style.display = "none"; 
	         }
        }
    </script>

</head>
<body style="text-align: center;" onload="init();">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        �ʻ�����</div>
                </div>
            </div>
            <div id="content">
                <asp:FormView ID="fvEmployee" runat="server" DataSourceID="odsEmployeeDetail" DataKeyNames="EmployeeID"
                    OnItemUpdated="fvEmployee_ItemUpdated" DefaultMode="Edit" OnItemCommand="fvEmployee_ItemCommand" OnDataBound="fvEmployee_DataBound">
                    <EditItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 3%">
                                    ��֯����</td>
                                <td style="width: 10%">
                                    <asp:Label ID="lblOrgName" runat="server" Text='<%# Eval("OrgName") %>'></asp:Label>
                                </td>
                                <td style="width: 3%">
                                    ����</td>
                                <td style="width: 10%">
                                    <asp:Label ID="txtEmployeeNameEdit" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 3%">
                                    Ա������</td>
                                <td style="width: 10%">
                                    <asp:Label ID="lblWorkNo" runat="server" Text='<%# Bind("WorkNo") %>'></asp:Label></td>
                                <td>
                                    ������λ</td>
                                <td>
                                    <asp:Label ID="lblPostName" runat="server" Text='<%# Eval("PostName") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    ����֤��
                                </td>
                                <td>
                                    <asp:Label ID="lblPostNo" runat="server" Text='<%# Eval("PostNo") %>'></asp:Label></td>
                                </td>
                                <td>
                                    �Ա�</td>
                                <td>
                                    <asp:DropDownList ID="ddlSexEdit" runat="server" Enabled="false" SelectedValue='<%# Bind("Sex") %>'>
                                        <asp:ListItem Value="��">��</asp:ListItem>
                                        <asp:ListItem Value="Ů">Ů</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ����</td>
                                <td>
                                    <uc1:Date ID="dateBirthdayeEdit" runat="server" DateValue='<%# Bind("Birthday","{0:yyyy-MM-dd}") %>' />
                                </td>
                                <td>
                                    ����</td>
                                <td>
                                    <asp:TextBox ID="txtNativePlaceEdit" runat="server" ReadOnly="true" Text='<%# Bind("NativePlace") %>'></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    ����</td>
                                <td>
                                    <asp:TextBox ID="txtFolkEidt" runat="server" ReadOnly="true" Text='<%# Bind("Folk") %>'></asp:TextBox></td>
                                <td>
                                    ���</td>
                                <td>
                                    <asp:DropDownList ID="ddlWeddingEdit" runat="server" Enabled="false" SelectedValue='<%# Bind("Wedding") %>'>
                                        <asp:ListItem Text="δ��" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="�ѻ�" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ��ְ����</td>
                                <td>
                                    <uc1:Date ID="dateBeginDateEdit" runat="server" Enabled="false" DateValue='<%# Bind("BeginDate","{0:yyyy-MM-dd}") %>' />
                                </td>
                                <td>
                                    �칫�绰</td>
                                <td>
                                    <asp:TextBox ID="txtWorkPhoneEdit" runat="server" ReadOnly="true" Text='<%# Bind("WorkPhone") %>'></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    ��ͥ�绰</td>
                                <td>
                                    <asp:TextBox ID="txtHomePhoneEdit" runat="server" ReadOnly="true" Text='<%# Bind("HomePhone") %>'></asp:TextBox></td>
                                <td>
                                    �ƶ��绰</td>
                                <td>
                                    <asp:TextBox ID="txtMobilePhoneEdit" runat="server" ReadOnly="true" Text='<%# Bind("MobilePhone") %>'></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    ͨѶ��ַ</td>
                                <td>
                                    <asp:TextBox ID="txtAddressEdit" runat="server" ReadOnly="true" Text='<%# Bind("Address") %>'></asp:TextBox></td>
                                <td>
                                    ��������</td>
                                <td>
                                    <asp:TextBox ID="txtPostCodeEdit" runat="server" ReadOnly="true" Text='<%# Bind("PostCode") %>'></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    �ǰ��鳤
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlIsGroup" runat="server" Enabled="false" SelectedValue='<%# Bind("IsGroupLeader") %>'>
                                        <asp:ListItem Value="2">--��ѡ��--</asp:ListItem>
                                        <asp:ListItem Value="1">��</asp:ListItem>
                                        <asp:ListItem Value="0">��</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    ���ܵȼ�
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlTech" DataSourceID="odsTechType" Enabled="false" DataTextField="TypeName"
                                        DataValueField="TechnicianTypeID" runat="server" SelectedValue='<%# Bind("TechnicianTypeID") %>'>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr id="studyRecord">
                                <td >
                                    ѧϰ��¼����</td>
                                <td>
                                    <asp:Label ID="lblCount" runat="server"></asp:Label></td>
                                <td>
                                    ѧϰ�ۼ�ʱ��</td>
                                <td>
                                    <asp:Label ID="lblTime" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    �ڸ�</td>
                                <td>
                                    <asp:CheckBox ID="chDimission" runat="server" Enabled="false" Checked='<%# Bind("IsOnPost") %>'>
                                    </asp:CheckBox></td>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    ��ע</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtMemoEdit" runat="server" ReadOnly="true" Width="98%" TextMode="MultiLine"
                                        Text='<%# Bind("Memo") %>'></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                        <asp:HiddenField ID="hfOrgID" runat="server" Value='<%# Bind("OrgID") %>' />
                        <asp:HiddenField ID="hfPostID" runat="server" Value='<%# Bind("PostID") %>' />
                        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" Visible="false"
                            CommandName="Update" Text='&lt;img border=0 src="../Common/Image/save.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="ChangePasswordButton" runat="server" CausesValidation="False"
                            Text='&lt;img border=0 src="../Common/Image/changepassword.gif" alt="" /&gt;'
                            OnClientClick="return ChangePassword();">
                        </asp:LinkButton>
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text='&lt;img border=0 src="../Common/Image/cancel.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                    </EditItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="odsEmployeeDetail" runat="server" DataObjectTypeName="RailExam.Model.Employee"
                    SelectMethod="GetEmployee" TypeName="RailExam.BLL.EmployeeBLL" UpdateMethod="UpdateEmployee1"
                    EnableViewState="False">
                    <SelectParameters>
                        <asp:SessionParameter Name="employeeID" SessionField="StudentID" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="odsTechType" runat="server" SelectMethod="GetTechnicianType"
                    TypeName="RailExam.BLL.TechnicianTypeBLL" DataObjectTypeName="RailExam.Model.TechnicianType">
                </asp:ObjectDataSource>
               <input type="hidden" name="isWuhan" value='<%=PrjPub.IsWuhan() %>' /> 
            </div>
        </div>
    </form>
</body>
</html>
