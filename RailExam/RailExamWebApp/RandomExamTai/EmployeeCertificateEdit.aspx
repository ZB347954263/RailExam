<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeeCertificateEdit.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.EmployeeCertificateEdit" %>

<%@ Register TagPrefix="uc1" TagName="Date" Src="~/Common/Control/Date/Date.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>新增其他证书</title> 
 <script type="text/javascript" src="../Common/JS/JSHelper.js"></script>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
      function checkInfo() {
      	var drop_certificate = $F("drop_certificate");
      	var drop_certificate_Level = $F("drop_certificate_Level");
      	var drop_train_unit = $F("drop_train_unit");
      	var drop_certificate_unit = $F("drop_certificate_unit");
      	var txtcertificate_num = $F("txtcertificate_num");
      

      	if (drop_certificate.value == "") {
      		alert("请选择证书名称！");
      		drop_certificate.focus();
      		return false;
      	}
      	if (drop_certificate_Level.value == "") {
      		alert("请选择证书级别！");
      		drop_certificate_Level.focus();
      		return false;
      	}
      	if (drop_train_unit.value == "") {
      		alert("请选择培训单位！");
      		drop_train_unit.focus();
      		return false;
      	}
      	if (drop_certificate_unit.value == "") {
      		alert("请选择发证单位！");
      		drop_certificate_unit.focus();
      		return false;
      	}
   	if (txtcertificate_num.value == "") {
      		alert("请输入证书编号！");
   		txtcertificate_num.focus();
      		return false;
      	}
      	var certificate_date = document.getElementById("certificate_date_DateBox");
    	var date0 = document.getElementById("certificate_date_compareValidator");
      	if(certificate_date.value=="") {
      		alert("发证时间不能为空！");
      		certificate_date.focus();
    		return false;
      	}
      	
      	var begin_date = document.getElementById("begin_date_DateBox");
    	var date1 = document.getElementById("begin_date_compareValidator");
      	if(begin_date.value=="") {
      		alert("从事本专业的起始时间不能为空！");
      		begin_date.focus();
    		return false;
      	}
      	var end_date = document.getElementById("end_date_DateBox");
    	var date2 = document.getElementById("end_date_compareValidator");
      	      	if(end_date.value=="") {
      		alert("有效期截止日期不能为空！");
      		end_date.focus();
    		return false;
      	}

      	
      	
      	return true;
      }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head" style="width: 680px">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current" runat="server">
                        新增其他证书</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 10%;">
                            证书名称<span class="require">*</span></td>
                        <td style="width: 39%;">
                            <asp:DropDownList ID="drop_certificate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drop_certificate_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 13%;">
                            证书级别<span class="require">*</span>
                        </td>
                        <td style="width: 41%;">
                            <asp:DropDownList ID="drop_certificate_Level" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            发证日期<span class="require">*</span>
                        </td>
                        <td>
                            <uc1:Date ID="certificate_date" runat="server" />
                        </td>
                        <td style="width: 13%;">
                            培训单位<span class="require">*</span>
                        </td>
                        <td style="width: 41%;">
                            <asp:DropDownList ID="drop_train_unit" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            发证单位<span class="require">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="drop_certificate_unit" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 13%;">
                            证书编号 <span class="require">*</span>
                        </td>
                        <td style="width: 41%;">
                            <asp:TextBox ID="txtcertificate_num" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            复审（年度鉴定）日期
                        </td>
                        <td>
                            <uc1:Date ID="check_date" runat="server" />
                        </td>
                        <td style="width: 13%;">
                            复审单位
                        </td>
                        <td style="width: 41%;">
                            <asp:TextBox ID="txtcheck_unit" runat="server" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            复审（年度鉴定）结果 
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="check_result" runat="server" Width="100%" Rows="3" MaxLength="100"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td  >
                           从事本专业的起始时间<span class="require">*</span>
                        </td>
                        <td>
                           <uc1:Date ID="begin_date" runat="server" />
                        </td>
                         <td  >
                          有效期截止日期<span class="require">*</span>
                        </td>
                        <td>
                           <uc1:Date ID="end_date" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            复审周期
                        </td>
                        <td>
                            <asp:TextBox ID="txtcheck_cycle" runat="server" MaxLength="30"></asp:TextBox>
                        </td>
                         
                    </tr>
                    <tr>
                        <td style="width: 10%;">
                            备注
                        </td>
                        <td style="width: 90%;" colspan="3">
                            <asp:TextBox ID="txtmemo" runat="server" Width="100%" Rows="3" MaxLength="100" TextMode="MultiLine"></asp:TextBox>
                            <br />
                            （100个字以内）
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
 
    </form>
</body>
</html>
