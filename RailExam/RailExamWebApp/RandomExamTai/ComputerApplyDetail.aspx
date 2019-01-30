<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ComputerApplyDetail.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.ComputerApplyDetail" %>


<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>微机教室预订信息</title>
    <base target="_self" />
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    function selectOrg()
        {
            var selectedOrg = window.showModalDialog('../Common/SelectOrganization.aspx?Type=Station', 
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:580px');

            if(! selectedOrg)
            {
                return;
            }
            
            document.getElementById("hfOrgID").value = selectedOrg.split('|')[0];
            document.getElementById("txtORG").value = selectedOrg.split('|')[1];
            document.getElementById("hfOrgName").value = selectedOrg.split('|')[1];
    	    __doPostBack('LinkButton1', '');
        }
        
        function CloseDetail() {
            var selectOrgId = document.getElementById("hfOrgID").value;
            location.href = "ComputerManageInfo.aspx?ID=" + selectOrgId +"&type=Org";
        }
function __doPostBack(eventTarget, eventArgument) {
        var theform;
        if (window.navigator.appName.toLowerCase().indexOf("netscape") > -1) {
            theform = document.forms["form1"];
        }
        else {
            theform = document.Form1;
        }
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        theform.submit();
    }
    function CloseApplyDetail()
    {
        var selectOrgId = document.getElementById("hfOrgID").value;
        window.frames["iframeFirst"].location="ComputerRoomApplyInfo.aspx?ID="+selectOrgId;
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="content">
            <table class="contentTable">
                <tr>
                    <td style="width: 15%;">
                        被申请单位<span class="require">*</span>
                    </td>
                    <td style="width: 35%;">
                        <asp:DropDownList ID="DDLOrg" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDLOrg_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 15%;">
                        微机教室<span class="require">*</span>
                    </td>
                    <td style="width: 35%;">
                        <asp:DropDownList ID="DDLComputerRoom" AutoPostBack="true" runat="server" OnSelectedIndexChanged="DDLComputerRoom_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="display: none">
                    <td>
                        机位数
                    </td>
                    <td>
                        <label id="lblCOMPUTER_NUMBER" runat="server">
                        </label>
                    </td>
                    <td>
                        损坏机位数
                    </td>
                    <td>
                        <label id="lblBAD_SEAT" runat="server">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        有效机位数
                    </td>
                    <td colspan="3">
                        <asp:Label ID="lblEffectNum" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        申请时间
                    </td>
                    <td colspan="3">
                        <uc1:Date ID="dateBeginTime" runat="server" /><asp:DropDownList  id="ddlBeginHour" runat="server"></asp:DropDownList>时
                            至<uc1:Date ID="dateEndTime" runat="server" /><asp:DropDownList  id="ddlEndHour" runat="server"></asp:DropDownList>时
                    </td>
                </tr>
                <tr>
                    <td>
                        申请机位
                    </td>
                    <td>
                        <asp:TextBox ID="txtAPPLY_COMPUTER_NUMBER" runat="server" Width="85%" onkeypress="if (event.keyCode<48 || event.keyCode>57||event.keyCode==13) event.returnValue=false;"
                            MaxLength="3"></asp:TextBox>
                    </td>
                    <td>
                        回复状态
                    </td>
                    <td>
                        <asp:DropDownList ID="DDLREPLY_STATUS" Enabled="false" runat="server">
                            <asp:ListItem>申请未回复</asp:ListItem>
                            <asp:ListItem>申请已通过</asp:ListItem>
                            <asp:ListItem>申请未通过</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        原因
                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtREJECT_REASON" runat="server" Enabled="false" TextMode="MultiLine"
                            Height="64px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td colspan="4" align="center">
                        <asp:ImageButton ID="btnSave" runat="server" ImageUrl="../Common/Image/save.gif"
                            CausesValidation="false" OnClick="btnSave_Click" />&nbsp;&nbsp;
                        <%--<asp:ImageButton ID="btnSaveNew" runat="server" ImageUrl="../Common/Image/SaveAdd.gif" CausesValidation="false"
							 OnClick="btnSaveAdd_Click" />&nbsp;&nbsp;
						<img id="btnClose" src="../Common/Image/close.gif" alt="" onclick="CloseApplyDetail();"  />  --%>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <asp:HiddenField ID="hfOrgName" runat="server" />
    </form>
</body>
</html>
