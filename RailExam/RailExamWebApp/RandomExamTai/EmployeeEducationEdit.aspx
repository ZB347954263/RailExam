<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeeEducationEdit.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.EmployeeEducationEdit" %>

<%@ Register TagPrefix="uc1" TagName="Date" Src="~/Common/Control/Date/Date.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>新增学习动态</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
 
    function  checkInfo() {
    	var oname = document.getElementById("droponame"); 
    	if(oname.value=="0") {
    		alert("原学历不能为空！");
    		//oname.focus();
    		return false;
    	}
       var nname = document.getElementById("dropnname");
    	if(nname.value=="0") {
    		alert("请选择学历！");
    		nname.focus();
    		return false;
    	}
    	 var graduate_date = document.getElementById("graduate_date_DateBox");
    	 var date = document.getElementById("graduate_date_compareValidator");
    	if(date.style.display!="none") {
    		alert("毕业时间时间格式不正确！");
    		graduate_date.focus();
    		return false;
    	}
    	
    	if(graduate_date.value.indexOf("-")<0 && graduate_date.value.indexOf("/")<0) {
    		alert("毕业时间不能为空！");
    		graduate_date.focus();
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
                        新增学习动态</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 13%;">
                            原学历<span class="require">*</span>
                            
                        </td>
                        <td style="width: 35%;">
                           <asp:DropDownList ID="droponame" runat="server" Width="120px" Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 13%;">
                            新学历<span class="require">*</span>
                        </td>
                        <td style="width: 40%;">
                               <asp:DropDownList ID="dropnname" runat="server" Width="120px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 13%;">
                            学习专业
                        </td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtsubject" runat="server" Width="120px" MaxLength="20"></asp:TextBox>
                        </td>
                        <td style="width: 13%;">
                            学习形式 
                        </td>
                        <td style="width: 40%;">
                            <asp:TextBox ID="txtstyle" runat="server" Width="120px" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 13%;">
                            毕业证号
                        </td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtdiplomano" runat="server" Width="120px" MaxLength="20"></asp:TextBox>
                        </td>
                        <td style="width: 13%;">
                            毕业学校 
                        </td>
                        <td style="width: 40%;">
                            <asp:TextBox ID="txtgraducateschool" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 13%;">
                            学校类别
                        </td>
                        <td style="width: 35%;">
                            <asp:DropDownList ID="dropSchoolCategory" runat="server" >
                                <asp:ListItem Value="0">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">全日制</asp:ListItem>
                                <asp:ListItem Value="2">网络教育</asp:ListItem>
                                <asp:ListItem Value="3">自学考试</asp:ListItem>
                                <asp:ListItem Value="4">党校函授</asp:ListItem>
                                <asp:ListItem Value="5">函授学习</asp:ListItem>
                                <asp:ListItem Value="6">电大学习</asp:ListItem>
                                <asp:ListItem Value="7">职校学习</asp:ListItem>
                                <asp:ListItem Value="8">业校学习</asp:ListItem>
                                <asp:ListItem Value="9">夜校学习</asp:ListItem>
                                <asp:ListItem Value="10">成人教育</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 13%;">
                            毕业时间<span class="require">*</span>
                        </td>
                        <td style="width: 40%;">
                            <uc1:Date ID="graduate_date" runat="server" />
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
        <asp:HiddenField ID="hfPostID" runat="server" />
        <asp:HiddenField ID="empID" runat="server" /> 
         
    </form>
</body>
</html>
