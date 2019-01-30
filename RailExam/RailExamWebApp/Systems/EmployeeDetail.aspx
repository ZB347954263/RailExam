<%@ Import Namespace="RailExamWebApp.Common.Class"%>
<%@ Page Language="C#" AutoEventWireup="True" Codebehind="EmployeeDetail.aspx.cs"
    Inherits="RailExamWebApp.Systems.EmployeeDetail" %>

<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>职员详细信息</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function deleteBtnClientClick()
        {
            return confirm("您确定要删除此记录吗？");
        }
        
        // 验证用户输入
        function validateUserInput()
        {
            if(document.getElementById("fvEmployee_txtOrgNameEdit")&&document.getElementById("fvEmployee_txtOrgNameEdit").value.length == 0)
            {
                alert("组织机构不能为空！");
                return false ;
            }  
            
            if(document.getElementById("fvEmployee_txtPostNameEdit")&&document.getElementById("fvEmployee_txtPostNameEdit").value.length == 0)
            {
                alert("工作岗位不能为空！");
                return  false;
            }
          return true;
        }    
        
        function validateUserInputInsert()
        {   
            if(document.getElementById("fvEmployee_txtOrgNameInsert")&&document.getElementById("fvEmployee_txtOrgNameInsert").value.length == 0)
            {
                alert("组织机构不能为空！");
                return  false;
            } 
            if(document.getElementById("fvEmployee_txtPostNameInsert")&&document.getElementById("fvEmployee_txtPostNameInsert").value.length == 0)
            {
                alert("工作岗位不能为空！");
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
	　 {
	       if(form1.txtOrgNameEdit.value == "")
          {
            alert('组织机构不能为空！');
            return false;
          } 
           if(form1.txtEmployeeNameEdit.value == "")
          {
            alert('姓名不能为空！');
            return false;
          } 
          
           if(form1.txtWorkNoEdit.value == "")
          {
            alert('员工编码不能为空！');
            return false;
          }
          
          if(form1.txtPostNameEdit.value == "")
          {
            alert('工作岗位不能为空！');
            return false;
          } 
          
          if(form1.txtMemoEdit.value.length > 50)
          {
            alert('备注不能为超过50个字！');
            return false;
          }
                    
          if(form1.ddlIsGroup.value == "2")
          {
            alert('请设置班组长选项！');
            return false;
          }
          
          if(form1.ddlTech.value == "0")
          {
            alert('请设置技能等级选项！');
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
                        职员详细信息</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 15%;">
                            组织机构<span class="require">*</span></td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtOrgNameEdit" runat="server" ReadOnly="true" Width="85%" Text='<%# Bind("OrgName") %>'></asp:TextBox>
                            <a onclick="selectOrg()" href="#">
                                <asp:Image runat="server" ID="img1" ImageUrl="../Common/Image/search.gif" AlternateText="选择组织机构">
                                </asp:Image>
                            </a>
                        </td>
                        <td style="width: 15%;">
                            姓名<span class="require">*</span></td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtEmployeeNameEdit" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            员工编码<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtWorkNoEdit" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                        <td>
                            工作岗位<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtPostNameEdit" runat="server" ReadOnly="true" Width="85%"></asp:TextBox>
                            <a onclick="selectPost()" href="#">
                                <asp:Image runat="server" ID="img2" ImageUrl="../Common/Image/search.gif" AlternateText="选择工作岗位">
                                </asp:Image>
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            工作证号
                        </td>
                        <td>
                            <asp:TextBox ID="txtPostNo" runat="server" MaxLength="13"></asp:TextBox>
                        </td>
                        <td>
                            角色权限
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRoleNameEdit" runat="server" DataSourceID="odsDdlRoleName"
                                DataTextField="RoleName" DataValueField="RoleID">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            性别</td>
                        <td>
                            <asp:DropDownList ID="ddlSex" runat="server">
                                <asp:ListItem Value="男">男</asp:ListItem>
                                <asp:ListItem Value="女">女</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            生日</td>
                        <td>
                            <uc1:Date ID="dateBirthday" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            籍贯</td>
                        <td>
                            <asp:TextBox ID="txtNativePlace" runat="server" MaxLength="20"></asp:TextBox></td>
                        <td>
                            民族</td>
                        <td>
                            <asp:TextBox ID="txtFolk" runat="server" MaxLength="20"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            婚否</td>
                        <td>
                            <asp:RadioButtonList ID="rblWedding" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="0" Selected="True" Text="未婚"></asp:ListItem>
                                <asp:ListItem Value="1" Text="已婚"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            入职日期</td>
                        <td>
                            <uc1:Date ID="dateBeginDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            办公电话</td>
                        <td>
                            <asp:TextBox ID="txtWorkPhoneEdit" runat="server" MaxLength="30"></asp:TextBox></td>
                        <td>
                            家庭电话</td>
                        <td>
                            <asp:TextBox ID="txtHomePhone" runat="server" MaxLength="30"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            移动电话</td>
                        <td>
                            <asp:TextBox ID="txtMobilePhone" runat="server" MaxLength="30"></asp:TextBox></td>
                        <td>
                            通讯地址</td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" MaxLength="100"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            邮政编码</td>
                        <td>
                            <asp:TextBox ID="txtPostCode" runat="server" MaxLength="6"></asp:TextBox></td>
                        <td>
                            已离职</td>
                        <td>
                            <asp:CheckBox ID="chDimission" runat="server"></asp:CheckBox></td>
                    </tr>
                    <tr>
                        <td>
                            是班组长<span class="require">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIsGroup" runat="server">
                                <asp:ListItem Value="2">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            技能等级<span class="require">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTech" DataSourceID="odsTechType" DataTextField="TypeName"
                                DataValueField="TechnicianTypeID" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注</td>
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
