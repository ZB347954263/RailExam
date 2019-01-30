<%@ Import Namespace="RailExamWebApp.Common.Class"%>
<%@ Page Language="C#" AutoEventWireup="True" Codebehind="EmployeeDetail.aspx.cs"
    Inherits="RailExamWebApp.Systems.EmployeeDetail" %>

<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ְԱ��ϸ��Ϣ</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function deleteBtnClientClick()
        {
            return confirm("��ȷ��Ҫɾ���˼�¼��");
        }
        
        // ��֤�û�����
        function validateUserInput()
        {
            if(document.getElementById("fvEmployee_txtOrgNameEdit")&&document.getElementById("fvEmployee_txtOrgNameEdit").value.length == 0)
            {
                alert("��֯��������Ϊ�գ�");
                return false ;
            }  
            
            if(document.getElementById("fvEmployee_txtPostNameEdit")&&document.getElementById("fvEmployee_txtPostNameEdit").value.length == 0)
            {
                alert("������λ����Ϊ�գ�");
                return  false;
            }
          return true;
        }    
        
        function validateUserInputInsert()
        {   
            if(document.getElementById("fvEmployee_txtOrgNameInsert")&&document.getElementById("fvEmployee_txtOrgNameInsert").value.length == 0)
            {
                alert("��֯��������Ϊ�գ�");
                return  false;
            } 
            if(document.getElementById("fvEmployee_txtPostNameInsert")&&document.getElementById("fvEmployee_txtPostNameInsert").value.length == 0)
            {
                alert("������λ����Ϊ�գ�");
                return  false;
            }     
             return true;                
        }      
        
        function validatePostCode()
        {
            var postCode = document.getElementById("txtPostCode").value;
            var RegExp = /[1-9]\d{5}/;
            
            return (RegExp.test(postCode));
        }
        
        function validatePhoneNo()
        {
            var phoneNo = document.getElementById("txtPhoneNo").value;
            var RegExp = /(\d{3})-?[1-9]\d{7}/;
            
            return (RegExp.test(phoneNo));
        }   

        function validateEmail()
        {
            var email = document.getElementById("txtEmail").value;
            var RegExp = /\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/;
            
            return (RegExp.test(email));
        }
        
        function selectOrg()
        {
            var selectedOrg = window.showModalDialog('../Common/SelectOrganization.aspx?Type=All', 
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:580px');

            if(! selectedOrg)
            {
                return;
            }
            
            document.getElementById("hfOrgID").value = selectedOrg.split('|')[0];
            document.getElementById("txtOrgNameEdit").value = selectedOrg.split('|')[1];
        }
        
        function selectPost()
        {
            var selectedPost = window.showModalDialog('../Common/SelectPost.aspx', 
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:580px');

            if(! selectedPost)
            {
                return;
            }
            
            document.getElementById("hfPostID").value = selectedPost.split('|')[0];
            document.getElementById("txtPostNameEdit").value = selectedPost.split('|')[1];
        }
       
       	 function CheckNew()
	�� {
	       if(form1.txtOrgNameEdit.value == "")
          {
            alert('��֯��������Ϊ�գ�');
            return false;
          } 
           if(form1.txtEmployeeNameEdit.value == "")
          {
            alert('��������Ϊ�գ�');
            return false;
          } 
          
           if(form1.txtWorkNoEdit.value == "")
          {
            alert('Ա�����벻��Ϊ�գ�');
            return false;
          }
          
          if(form1.txtPostNameEdit.value == "")
          {
            alert('������λ����Ϊ�գ�');
            return false;
          } 
          
          if(form1.txtMemoEdit.value.length > 50)
          {
            alert('��ע����Ϊ����50���֣�');
            return false;
          }
                    
          if(form1.ddlIsGroup.value == "2")
          {
            alert('�����ð��鳤ѡ�');
            return false;
          }
          
          if(form1.ddlTech.value == "0")
          {
            alert('�����ü��ܵȼ�ѡ�');
            return false;
          }
          
          return true;
	 } 
	 
    </script>

    <link href="/RailExamWebApp/Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <link href="/RailExamWebApp/Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
</head>
<body onload="init();">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        ְԱ��ϸ��Ϣ</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 15%;">
                            ��֯����<span class="require">*</span></td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtOrgNameEdit" runat="server" ReadOnly="true" Width="85%" Text='<%# Bind("OrgName") %>'></asp:TextBox>
                            <a onclick="selectOrg()" href="#">
                                <asp:Image runat="server" ID="img1" ImageUrl="../Common/Image/search.gif" AlternateText="ѡ����֯����">
                                </asp:Image>
                            </a>
                        </td>
                        <td style="width: 15%;">
                            ����<span class="require">*</span></td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtEmployeeNameEdit" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Ա������<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtWorkNoEdit" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                        <td>
                            ������λ<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtPostNameEdit" runat="server" ReadOnly="true" Width="85%"></asp:TextBox>
                            <a onclick="selectPost()" href="#">
                                <asp:Image runat="server" ID="img2" ImageUrl="../Common/Image/search.gif" AlternateText="ѡ������λ">
                                </asp:Image>
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ����֤��
                        </td>
                        <td>
                            <asp:TextBox ID="txtPostNo" runat="server" MaxLength="13"></asp:TextBox>
                        </td>
                        <td>
                            ��ɫȨ��
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRoleNameEdit" runat="server" DataSourceID="odsDdlRoleName"
                                DataTextField="RoleName" DataValueField="RoleID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            �Ա�</td>
                        <td>
                            <asp:DropDownList ID="ddlSex" runat="server">
                                <asp:ListItem Value="��">��</asp:ListItem>
                                <asp:ListItem Value="Ů">Ů</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            ����</td>
                        <td>
                            <uc1:Date ID="dateBirthday" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ����</td>
                        <td>
                            <asp:TextBox ID="txtNativePlace" runat="server" MaxLength="20"></asp:TextBox></td>
                        <td>
                            ����</td>
                        <td>
                            <asp:TextBox ID="txtFolk" runat="server" MaxLength="20"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            ���</td>
                        <td>
                            <asp:RadioButtonList ID="rblWedding" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="0" Selected="True" Text="δ��"></asp:ListItem>
                                <asp:ListItem Value="1" Text="�ѻ�"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            ��ְ����</td>
                        <td>
                            <uc1:Date ID="dateBeginDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            �칫�绰</td>
                        <td>
                            <asp:TextBox ID="txtWorkPhoneEdit" runat="server" MaxLength="30"></asp:TextBox></td>
                        <td>
                            ��ͥ�绰</td>
                        <td>
                            <asp:TextBox ID="txtHomePhone" runat="server" MaxLength="30"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            �ƶ��绰</td>
                        <td>
                            <asp:TextBox ID="txtMobilePhone" runat="server" MaxLength="30"></asp:TextBox></td>
                        <td>
                            ͨѶ��ַ</td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" MaxLength="100"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            ��������</td>
                        <td>
                            <asp:TextBox ID="txtPostCode" runat="server" MaxLength="6"></asp:TextBox></td>
                        <td>
                            ����ְ</td>
                        <td>
                            <asp:CheckBox ID="chDimission" runat="server"></asp:CheckBox></td>
                    </tr>
                    <tr>
                        <td>
                            �ǰ��鳤<span class="require">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIsGroup" runat="server">
                                <asp:ListItem Value="2">--��ѡ��--</asp:ListItem>
                                <asp:ListItem Value="1">��</asp:ListItem>
                                <asp:ListItem Value="0">��</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            ���ܵȼ�<span class="require">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTech" DataSourceID="odsTechType" DataTextField="TypeName"
                                DataValueField="TechnicianTypeID" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��ע</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtMemoEdit" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Common/Image/save.gif" CausesValidation="true"
                                OnClick="btnSave_Click" OnClientClick="return CheckNew();" />&nbsp;&nbsp;
                            <asp:ImageButton ID="btnSaveNew" runat="server" ImageUrl="~/Common/Image/SaveAdd.gif"
                                CausesValidation="true" OnClientClick="return CheckNew();" OnClick="btnSaveAdd_Click" />&nbsp;&nbsp;
                            <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/Common/Image/close.gif"
                                OnClientClick="window.close();" CausesValidation="false" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="hfOrgID" runat="server" />
            <asp:HiddenField ID="hfPostID" runat="server" />
            <asp:HiddenField ID="hfType" runat="server" />
            <asp:ObjectDataSource ID="odsTechType" runat="server" SelectMethod="GetTechnicianType"
                TypeName="RailExam.BLL.TechnicianTypeBLL" DataObjectTypeName="RailExam.Model.TechnicianType">
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsDdlRoleName" runat="server" SelectMethod="GetRoles"
                TypeName="RailExam.BLL.SystemRoleBLL">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfSuitRange" DefaultValue="0" PropertyName="Value"
                        Type="int32" Size="4" Name="suitRange" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <asp:HiddenField ID="hfSuitRange" runat="server" />
    </form>
</body>
</html>
