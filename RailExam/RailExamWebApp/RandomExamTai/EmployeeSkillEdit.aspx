<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeSkillEdit.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.EmployeeSkillEdit" %>
<%@ Register TagPrefix="uc1" TagName="Date" Src="~/Common/Control/Date/Date.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
      <base target="_self" />
    <title>新增技能动态</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
 
    function  checkInfo() {
    	var oname = document.getElementById("droponame");
    	if (oname.value == "0") {
    		alert("请选择等级！");
    		oname.focus();
    		return false;
    	}
    	var nname = document.getElementById("dropnname");
    	if (nname.value == "0") {
    		alert("请选择等级！");
    		nname.focus();
    		return false;
    	}
    	var txtappoint_time = document.getElementById("txtappoint_time_DateBox");
    	var date = document.getElementById("txtappoint_time_compareValidator");
    	if (date.style.display != "none") {
    		alert("聘任时间格式不正确！");
    		txtappoint_time.focus();
    		return false;
    	}
    	var qualification_time = document.getElementById("qualification_timee_DateBox");
    	var date = document.getElementById("qualification_time_compareValidator");
    	if (date.style.display != "none") {
    		alert("取得资格时间格式不正确！");
    		qualification_time.focus();
    		return false;
    	}
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head" style="width: 610px">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current" runat="server">
                        新增技能动态</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 10%;">
                            原等级<span class="require">*</span>
                            
                        </td>
                        <td style="width: 39%;">
                           <asp:DropDownList ID="droponame" runat="server" Width="120px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 13%;">
                            现等级<span class="require">*</span>
                        </td>
                        <td style="width: 41%;">
                               <asp:DropDownList ID="dropnname" runat="server" Width="120px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 10%;">
                            原安全等级 
                            
                        </td>
                        <td style="width: 39%;">
                           <asp:DropDownList ID="dropOldSafe" runat="server" Width="120px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 13%;">
                            现安全等级 
                        </td>
                        <td style="width: 41%;">
                               <asp:DropDownList ID="dropNewSafe" runat="server" Width="120px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                            证书编号
                        </td>
                        <td style="width: 39%;">
                            <asp:TextBox ID="txtcertificate_no" runat="server" Width="120px" MaxLength="20"></asp:TextBox>
                        </td>
                        <td style="width: 13%;">
                            聘任令号 
                        </td>
                        <td style="width: 41%;">
                            <asp:TextBox ID="txtappoint_order_no" runat="server" Width="120px" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                            聘任时间
                        </td>
                        <td style="width: 39%;">
                         <uc1:Date ID="txtappoint_time" runat="server" />
                        </td>
                        <td style="width: 13%;">
                            取得资格时间 
                        </td>
                        <td style="width: 41%;">
                             <uc1:Date ID="qualification_time" runat="server" />
                        </td>
                    </tr>
                     
                   
                </table>
                <div align="center" style="margin-top: 20px">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="保  存" OnClientClick="return checkInfo()"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnClose" runat="server" CssClass="button" Text="关  闭" OnClientClick="javascript:window.close()" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="empID" runat="server" />
       
    </form>
</body>
</html>
