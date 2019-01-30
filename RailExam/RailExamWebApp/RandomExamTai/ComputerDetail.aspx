<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ComputerDetail.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.ComputerDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
        }
        
        function CloseDetail() {
            var selectOrgId = document.getElementById("hfOrgID").value;
            location.href = "ComputerManageInfo.aspx?ID=" + selectOrgId +"&type=Org";
        }
        function selectSeat()
        {  
            var seatCount=document.getElementById("txtCOMPUTER_NUMBER").value;
            var txtDadSeat=document.getElementById("txtBAD_SEAT").value;
            if(seatCount!="")
            {
            var url="";
            	var ret = "";
                if(txtDadSeat!="")
                    ret= window.showModalDialog("../Train/TrainBadSeat.aspx?badCount="+seatCount+"&mode=Edit&txtDadSeat="+txtDadSeat+"&math="+Math.random(),"","status:false;dialogWidth:200px;dialogHeight:650px");
               else
                  ret= window.showModalDialog("../Train/TrainBadSeat.aspx?badCount="+seatCount+"&mode=Insert&math="+Math.random(),"","status:false;dialogWidth:200px;dialogHeight:650px");
               if( ret!=undefined)
               {
                  document.getElementById("txtBAD_SEAT").value=ret;
                 document.getElementById("hfBadSeatIDs").value=ret;
               }
            	 
            }
            else
            {
                alert('请填写机位数！');
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        微机教室详细信息</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 15%;">
                            所属单位<span class="require">*</span>
                        </td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtORG" runat="server" ReadOnly="true" Width="85%"></asp:TextBox>
                            <a onclick="selectOrg()" href="#">
                                <asp:Image runat="server" Visible="false" ID="Image1" ImageUrl="../Common/Image/search.gif" AlternateText="选择组织机构">
                                </asp:Image>
                            </a>
                        </td>
                        <td style="width: 15%;">
                            微机教室名称<span class="require">*</span>
                        </td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtCOMPUTER_ROOM_NAME" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            地址
                        </td>
                        <td>
                            <asp:TextBox ID="txtADDRESS" runat="server" Width="85%"></asp:TextBox>
                        </td>
                        <td>
                            机位数<span class="require">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCOMPUTER_NUMBER" runat="server" Width="85%" onkeypress="if (event.keyCode<48 || event.keyCode>57||event.keyCode==13) event.returnValue=false;"
                                MaxLength="3"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            损坏机位
                        </td>
                        <td colspan="3">
                            <%--<asp:TextBox ID="txtBAD_SEAT" runat="server" Width="85%" onkeypress="if (event.keyCode<48 || event.keyCode>57||event.keyCode==13) event.returnValue=false;"
                                MaxLength="3"></asp:TextBox>--%>
                            <asp:TextBox ID="txtBAD_SEAT" ReadOnly="true" runat="server" Width="85%"></asp:TextBox>
                            <a href="#" onclick="selectSeat()">
                                <asp:Image runat="server" ID="Image2" ImageUrl="../Common/Image/search.gif" AlternateText="选择机位">
                                </asp:Image>
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            联系人
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractPerson" runat="server" Width="85%"  MaxLength="30"></asp:TextBox>
                        </td>
                        <td>
                            联系电话
                        </td>
                        <td>
                            <asp:TextBox ID="txtContractPhone" runat="server" Width="85%"  MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            服务器所属单位<span class="require">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSelectOrg" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSelectOrg_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            服务器<span class="require">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="dropServer" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            是否有效<span class="require">*</span>
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="ddlIS_EFFECT" runat="server">
                                <asp:ListItem Value="2">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td colspan="4" align="center">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="../Common/Image/save.gif"
                                CausesValidation="false" OnClick="btnSave_Click" />&nbsp;&nbsp;
                            &nbsp;&nbsp;
                            <img id="btnClose" src="../Common/Image/close.gif" alt="" onclick="CloseDetail();" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <input type="hidden" name="Refresh" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <asp:HiddenField ID="hfOrgName" runat="server" />
        <asp:HiddenField ID="hfBadSeatIDs" runat="server" />
    </form>
</body>
</html>
