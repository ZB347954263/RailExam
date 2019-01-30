<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeeDetail.aspx.cs"
    Inherits="RailExamWebApp.RandomExamOther.EmployeeDetail" %>

<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>职员详细信息</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function deleteBtnClientClick()
        {
            return confirm("您确定要删除此记录吗？");
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
         
           if(form1.txtIdentifyCode.value == "")
          {
            alert('身份证号码不能为空！');
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
         
         if(form1.ddlEducationLevel.value == "0")
          {
            alert('请选择文化程度！');
            return false;
          } 
         
          if(form1.ddlEmployeeTypeID.value == "0")
          {
            alert('请选择职工类型！');
            return false;
          }  
          
          return true;
	 } 
	 
    </script>

    <link href="/RailExamWebApp/Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <link href="/RailExamWebApp/Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
</head>
<body>
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
                        <td style="width: 10%;">
                            组织机构<span class="require">*</span></td>
                        <td style="width: 23%;">
                            <asp:TextBox ID="txtOrgNameEdit" runat="server" ReadOnly="true" Width="85%" Columns="10"></asp:TextBox>
                            <a onclick="selectOrg()" href="#">
                                <asp:Image runat="server" ID="img1" ImageUrl="../Common/Image/search.gif" AlternateText="选择组织机构">
                                </asp:Image>
                            </a>
                        </td>
                        <td style="width: 10%;">
                            姓名<span class="require">*</span></td>
                        <td style="width: 23%;">
                            <asp:TextBox ID="txtEmployeeNameEdit" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                        <td style="width: 10%;">
                            身份证号码<span class="require">*</span></td>
                        <td style="width: 23%;">
                            <asp:TextBox ID="txtIdentifyCode" runat="server" MaxLength="18"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            工资编号<span class="require">*</span></td>
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
                        <td>
                            工作证号
                        </td>
                        <td>
                            <asp:TextBox ID="txtPostNo" runat="server" MaxLength="13"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            员工编码</td>
                        <td>
                            <asp:TextBox ID="txtHomePhone" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                        <td>
                            性别</td>
                        <td>
                            <asp:DropDownList ID="ddlSex" runat="server">
                                <asp:ListItem Value="男">男</asp:ListItem>
                                <asp:ListItem Value="女">女</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            文化程度<span class="require">*</span></td>
                        <td>
                            <asp:DropDownList ID="ddlEducationLevel" DataSourceID="odsEducationLevel" DataTextField="EducationLevelName"
                                DataValueField="EducationLevelID" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            生日</td>
                        <td>
                            <uc1:Date ID="dateBirthday" runat="server" />
                        </td>
                        <td>
                            入路日期</td>
                        <td>
                            <uc1:Date ID="dateBeginDate" runat="server" />
                        </td>
                        <td>
                            工作日期</td>
                        <td>
                            <uc1:Date ID="workDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            政治面貌</td>
                        <td>
                            <asp:DropDownList ID="ddlPolictical" DataSourceID="odsPoliticalStatus" DataTextField="PoliticalStatusName"
                                DataValueField="PoliticalStatusID" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            毕业院校</td>
                        <td>
                            <asp:TextBox ID="txtUniversity" runat="server" MaxLength="50"></asp:TextBox></td>
                        <td>
                            学习专业</td>
                        <td>
                            <asp:TextBox ID="txtStudy" runat="server" MaxLength="50"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            职工类型<span class="require">*</span></td>
                        <td>
                            <asp:DropDownList ID="ddlEmployeeTypeID" runat="server" OnSelectedIndexChanged="ddlEmployeeTypeID_SelectedIndexChanged"
                                AutoPostBack="True">
                                <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">工人</asp:ListItem>
                                <asp:ListItem Value="2">干部</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            工人技能等级<span class="require">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTech" DataSourceID="odsTechType" DataTextField="TypeName"
                                DataValueField="TechnicianTypeID" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            干部技术职称
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTechTitle" DataSourceID="odsTechTitleType" DataTextField="TypeName"
                                DataValueField="TechnicianTitleTypeID" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            工作地址</td>
                        <td>
                            <asp:TextBox ID="txtAddress" runat="server" MaxLength="100"></asp:TextBox></td>
                        <td>
                            班组长类别
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWorkGroup" DataSourceID="odsWorkGroup" DataTextField="LevelName"
                                AutoPostBack="True" OnSelectedIndexChanged="ddlWorkGroup_SelectedIndexChanged"
                                DataValueField="WorkGroupLeaderLevelID" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            是班组长<span class="require">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIsGroup" runat="server" Enabled="false">
                                <asp:ListItem Value="0">否</asp:ListItem>
                                <asp:ListItem Value="1">是</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            行政级别
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmployeeLevel" runat="server">
                                <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">正局</asp:ListItem>
                                <asp:ListItem Value="2">副局</asp:ListItem>
                                <asp:ListItem Value="3">正部</asp:ListItem>
                                <asp:ListItem Value="4">副部</asp:ListItem>
                                <asp:ListItem Value="5">正处</asp:ListItem>
                                <asp:ListItem Value="6">副处</asp:ListItem>
                                <asp:ListItem Value="7">正科</asp:ListItem>
                                <asp:ListItem Value="8">副科</asp:ListItem>
                                <asp:ListItem Value="9">科员</asp:ListItem>
                                <asp:ListItem Value="10">股级</asp:ListItem>
                                <asp:ListItem Value="11">干事</asp:ListItem>
                                <asp:ListItem Value="12">办事员</asp:ListItem>
                                <asp:ListItem Value="13">其他</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            职教人员类型
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEducationEmployeeType" runat="server">
                                <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">管理干部</asp:ListItem>
                                <asp:ListItem Value="2">专职教员</asp:ListItem>
                                <asp:ListItem Value="3">其他</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            职教委员会职务
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlHeadship" runat="server">
                                <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">主任</asp:ListItem>
                                <asp:ListItem Value="2">副主任</asp:ListItem>
                                <asp:ListItem Value="3">委员</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            运输业职工类型
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEmployeeTransportType" runat="server">
                                <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">生产人员</asp:ListItem>
                                <asp:ListItem Value="2">服务人员</asp:ListItem>
                                <asp:ListItem Value="3">其他人员</asp:ListItem>
                                <asp:ListItem Value="4">工程技术人员</asp:ListItem>
                                <asp:ListItem Value="5">行政管理人员</asp:ListItem>
                                <asp:ListItem Value="6">政工人员</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            教师类别
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlTeacherType" runat="server">
                                <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">兼职教师</asp:ListItem>
                                <asp:ListItem Value="2">专职教师</asp:ListItem>
                                <asp:ListItem Value="3">管理干部</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            职鉴</td>
                        <td>
                            <asp:CheckBox ID="chkApprove" runat="server" Enabled="false"></asp:CheckBox></td>
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
                        <td>
                            邮政编码</td>
                        <td>
                            <asp:TextBox ID="txtPostCode" runat="server" MaxLength="6"></asp:TextBox></td>
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
                            办公电话</td>
                        <td>
                            <asp:TextBox ID="txtWorkPhoneEdit" runat="server" MaxLength="30"></asp:TextBox></td>
                        <td>
                            移动电话</td>
                        <td>
                            <asp:TextBox ID="txtMobilePhone" runat="server" MaxLength="30"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            角色权限
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRoleNameEdit" runat="server" DataSourceID="odsDdlRoleName"
                                DataTextField="RoleName" DataValueField="RoleID">
                            </asp:DropDownList>
                        </td>
                        <td>
                            学习登录次数</td>
                        <td>
                            <asp:Label ID="lblCount" runat="server"></asp:Label></td>
                        <td>
                            学习累计时长</td>
                        <td>
                            <asp:Label ID="lblTime" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            已离职</td>
                        <td>
                            <asp:CheckBox ID="chDimission" runat="server"></asp:CheckBox></td>
                        <td>
                            备注</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtMemoEdit" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td colspan="6" align="center">
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
            <asp:ObjectDataSource ID="odsTechTitleType" runat="server" SelectMethod="GetTechnicianTitleType"
                TypeName="RailExam.BLL.TechnicianTitleTypeBLL" DataObjectTypeName="RailExam.Model.TechnicianTitleType">
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsEducationLevel" runat="server" SelectMethod="GetEducationLevel"
                TypeName="RailExam.BLL.EducationLevelBLL" DataObjectTypeName="RailExam.Model.EducationLevel">
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsPoliticalStatus" runat="server" SelectMethod="GetPoliticalStatus"
                TypeName="RailExam.BLL.PoliticalStatusBLL" DataObjectTypeName="RailExam.Model.PoliticalStatus">
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsWorkGroup" runat="server" SelectMethod="GetWorkGroupLeaderLevel"
                TypeName="RailExam.BLL.WorkGroupLeaderLevelBLL" DataObjectTypeName="RailExam.Model.WorkGroupLeaderLevel">
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
        <asp:HiddenField ID="hfIsWuhan" runat="server" />
    </form>
</body>
</html>
