<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" Codebehind="EmployeeInfoDetail.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.EmployeeInfoDetail" %>

<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript"> 
        function selectOrg()
        {
            var selectedOrg = window.showModalDialog('../Common/SelectOrganization.aspx?Type=All&OrgID='+document.getElementById("hfStationOrgID").value, 
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:580px');

            if(! selectedOrg)
            {
                return;
            }
            
            document.getElementById("hfOrgID").value = selectedOrg.split('|')[0];
            document.getElementById("txtORG").value = selectedOrg.split('|')[1];
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
            document.getElementById("txtPOST").value = selectedPost.split('|')[1];
            
        }
        
        function selectNowPost()
        {
            var selectedPost = window.showModalDialog('../Common/SelectPost.aspx', 
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:580px');

            if(! selectedPost)
            {
                return;
            }
            
            document.getElementById("hfNowPostID").value = selectedPost.split('|')[0];
            document.getElementById("txtNowPost").value = selectedPost.split('|')[1];
            
        }
        
        function changeBanZuZhangLeiXing()
        {
            if(document.getElementById("ddlWORKGROUPLEADER_TYPE_ID").value=="-1")
            {
                document.getElementById("ddlIsGroup").value="0";
            }
            else
            {
                document.getElementById("ddlIsGroup").value="1";
            }
        }
        
        function changeZhiGongLeiXing()
        {
        	if(window.parent.document.getElementById("NewButton")) 
        	{
        		window.parent.document.getElementById("NewButton").style.display = "none";
        		window.parent.document.getElementById("FindButton").style.display = "none";
        	}
        	
            if(document.getElementById("ddlEMPLOYEE_TYPE_ID").value=="-1")
            {    
                document.getElementById("lblEMPLOYEE_TYPE_Txt").style.display="none";
                document.getElementById("lblEMPLOYEE_TYPE_Txt").innerText = "";
                
                document.getElementById("divTECHNICAL_TITLE_ID").style.display="none";
                document.getElementById("divTECHNICIAN_TYPE_ID").style.display="none";
            	
//            	document.getElementById("dateTechnicalTitle").disabled = true;
//            	document.getElementById("dateTechnicalDate").disabled = true;
            }
            else  if(document.getElementById("ddlEMPLOYEE_TYPE_ID").value=="0")
            {  
                document.getElementById("lblEMPLOYEE_TYPE_Txt").innerText = "工人技能等级";
                document.getElementById("lblEMPLOYEE_TYPE_Txt").style.display="";
                
                document.getElementById("divTECHNICAL_TITLE_ID").style.display="none";
                document.getElementById("divTECHNICIAN_TYPE_ID").style.display="";

//            	document.getElementById("dateTechnicalTitle").disabled = true;
//            	document.getElementById("dateTechnicalDate").disabled = false;
            }
            else
            {  
                document.getElementById("lblEMPLOYEE_TYPE_Txt").innerText = "干部技术职称";
                document.getElementById("lblEMPLOYEE_TYPE_Txt").style.display="";
                
                document.getElementById("divTECHNICAL_TITLE_ID").style.display="";
                document.getElementById("divTECHNICIAN_TYPE_ID").style.display="none";
            	
//            	document.getElementById("dateTechnicalTitle").disabled = false;
//            	document.getElementById("dateTechnicalDate").disabled = true;
            }
        }
        
        function changeJianZhi1()
        {
            if(document.getElementById("cmbJianZhi2").value!="-1")
            {
                if(document.getElementById("cmbJianZhi1").value==document.getElementById("cmbJianZhi2").value)
                {
                    alert("请选择不同的兼职！");
                    document.getElementById("cmbJianZhi1").value="-1";
                }
            }
            document.getElementById("hfSECOND_POST_ID").value=document.getElementById("cmbJianZhi1").value;
            document.getElementById("hfTHIRD_POST_ID").value=document.getElementById("cmbJianZhi2").value; 
        }
        
        function changeJianZhi2()
        {
            if(document.getElementById("cmbJianZhi1").value!="-1")
            {
                if(document.getElementById("cmbJianZhi1").value==document.getElementById("cmbJianZhi2").value)
                {
                    alert("请选择不同的兼职！");
                    document.getElementById("cmbJianZhi2").value="-1";
                }
            }
             document.getElementById("hfSECOND_POST_ID").value=document.getElementById("cmbJianZhi1").value;
             document.getElementById("hfTHIRD_POST_ID").value=document.getElementById("cmbJianZhi2").value; 
        } 
        
        //弹出选择允许兼职岗位窗体
        function selectYunXuJianZhi()
        {
            var ids=document.getElementById('hfCOULD_POST_ID').value; 
            var postId = document.getElementById("hfPostID").value;
            var postText = document.getElementById("txtCOULD_POST_Name").value;
 
            var url1="../Common/SelectJianZhi.aspx?ids="+ids+"&names="+postText+"&postId="+postId;

            var selectedPost = window.showModalDialog(url1, 
                '', 'help:no; status:no; dialogWidth:300px;dialogHeight:620px;scroll:no;');
            
            if(! selectedPost)
            {
                return;
            }
        	   
            document.getElementById('hfCOULD_POST_ID').value = selectedPost.split('|')[0];
            document.getElementById('txtCOULD_POST_Name').value = selectedPost.split('|')[1];
             
            var nodeIDs = new Array();
            nodeIDs = selectedPost.split('|')[0].split(',');
            var nodeTexts = new Array();
            nodeTexts = selectedPost.split('|')[1].split(',');
            
            var jzHTMLText ="兼职1 : <select id='cmbJianZhi1' onchange='changeJianZhi1();'><option value='-1' >--请选择--</option>";
            
            //alert(jzHTMLText);
            
            for(var i = 0; i < nodeIDs.length; i ++)
		    {
		        jzHTMLText=jzHTMLText+"<option value='"+nodeIDs[i]+"' >"+nodeTexts[i]+"</option>";
                //alert(jzHTMLText);
		    }
		    jzHTMLText = jzHTMLText+"</select>&nbsp;&nbsp;&nbsp;&nbsp;兼职2 : <select id='cmbJianZhi2' onchange='changeJianZhi2();'><option value='-1' >--请选择--</option>";
		    for(var i = 0; i < nodeIDs.length; i ++)
		    {
		        jzHTMLText=jzHTMLText+"<option value='"+nodeIDs[i]+"' >"+nodeTexts[i]+"</option>";
                //alert(jzHTMLText);
		    }
            jzHTMLText=jzHTMLText+"</select>";
            
            // document.getElementById("divJianZhi1").innerHTML="兼职1 : <select id='cmbJianZhi1' onchange='changeJianZhi();'><option value='-1' >无</option><option value='0' >职位1</option><option value='1' >职位2</option></select>"+
            //                                    "&nbsp;&nbsp;&nbsp;&nbsp;兼职2 : <select id='cmbJianZhi2' onchange='changeJianZhi();'><option value='-1' >无</option><option value='0' >职位1</option></select>";
           
            //alert(jzHTMLText);
            document.getElementById("divJianZhi1").innerHTML=jzHTMLText;
        } 
        
        function showSelectedImage()
        {
            var img=document.getElementById("myImagePhoto");
            var fileUp=document.getElementById("fileUpload1");
            img.src=fileUp.value;
        }
        
        function changeSchoolCategory()
        {
            
        }
        
    </script>

</head>
<body onload="changeZhiGongLeiXing()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 15%;">
                            组织机构<span class="require">*</span>
                        </td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtORG" runat="server" ReadOnly="true" Width="82%"></asp:TextBox>
                            <a onclick="selectOrg()" href="#">
                                <asp:Image runat="server" Visible="false" ID="imgORG" ImageUrl="../Common/Image/search.gif"
                                    AlternateText="选择组织机构"></asp:Image>
                            </a>
                        </td>
                        <td style="width: 15%;" rowspan="8">
                            照片 <span class="require">*</span>
                        </td>
                        <td style="width: 35%;" rowspan="8">
                            <asp:Image ID="myImagePhoto" runat="server" Width="120" Height="150" />
                            <br />
                            <asp:FileUpload ID="fileUpload1" runat="server" Width="193px" />&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            姓名<span class="require">*</span>
                        </td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtEMPLOYEE_NAME" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            拼音码
                        </td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtPinYin" runat="server" MaxLength="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            工作证号<span class="require">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPOST_NO" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            工作职名<span class="require">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPOST" runat="server" ReadOnly="true" Width="82%"></asp:TextBox>
                            <a onclick="selectPost()" href="#">
                                <asp:Image runat="server" Visible="false" ID="Image2" ImageUrl="../Common/Image/search.gif"
                                    AlternateText="选择工作职名"></asp:Image>
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            现从事岗位
                        </td>
                        <td>
                            <asp:TextBox ID="txtNowPost" runat="server" ReadOnly="true" Width="82%"></asp:TextBox>
                            <a onclick="selectNowPost()" href="#">
                                <asp:Image runat="server" ID="Image3" ImageUrl="../Common/Image/search.gif" AlternateText="选择现从事岗位">
                                </asp:Image>
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            任现职名时间<span class="require">*</span>
                        </td>
                        <td>
                            <uc1:Date ID="datePost" runat="server" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            性别
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSex" runat="server">
                                <asp:ListItem Value="男">男</asp:ListItem>
                                <asp:ListItem Value="女">女</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            出生日期
                        </td>
                        <td>
                            <uc1:Date ID="dateBIRTHDAY" runat="server" />
                        </td>
                        <td>
                            安全等级
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSafe" runat="server" Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="display: none;">
                        <td>
                            籍贯
                        </td>
                        <td>
                            <asp:TextBox ID="txtNATIVE_PLACE" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                        <td>
                            民族
                        </td>
                        <td>
                            <asp:TextBox ID="txtFOLK" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            身份证号
                        </td>
                        <td>
                            <asp:TextBox ID="txtIDENTITY_CARDNO" runat="server" MaxLength="18"></asp:TextBox>
                        </td>
                        <td>
                            政治面貌
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPOLITICAL_STATUS" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            办公电话
                        </td>
                        <td>
                            <asp:TextBox ID="txtWORK_PHONE" runat="server" MaxLength="30" Columns="30"></asp:TextBox>
                        </td>
                        <td>
                            参加工作日期
                        </td>
                        <td>
                            <uc1:Date ID="dateBEGIN_DATE" runat="server" />
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            婚否
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblWEDDING" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="0" Selected="True" Text="未婚"></asp:ListItem>
                                <asp:ListItem Value="1" Text="已婚"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            家庭电话
                        </td>
                        <td>
                            <asp:TextBox ID="txtHOME_PHONE" runat="server" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            移动电话
                        </td>
                        <td>
                            <asp:TextBox ID="txtMOBILE_PHONE" runat="server" MaxLength="30"></asp:TextBox>
                        </td>
                        <td>
                            电子邮件
                        </td>
                        <td>
                            <asp:TextBox ID="txtEMAIL" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            邮政编码
                        </td>
                        <td>
                            <asp:TextBox ID="txtPOST_CODE" runat="server" MaxLength="6"></asp:TextBox>
                        </td>
                        <td>
                            通讯地址
                        </td>
                        <td>
                            <asp:TextBox ID="txtADDRESS" runat="server" MaxLength="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            工作编码
                        </td>
                        <td>
                            <asp:TextBox ID="txtWORK_NO" runat="server" MaxLength="20"></asp:TextBox>
                        </td>
                        <td>
                            学历
                        </td>
                        <td>
                            <asp:DropDownList ID="DDLeducation_level_id" runat="server">
                            </asp:DropDownList>
                            <%--<asp:TextBox ID="txtEDUCATION_LEVEL_ID" runat="server" MaxLength="20"></asp:TextBox>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            进入神华系统时间
                        </td>
                        <td>
                            <uc1:Date ID="dateAWARD_DATE" runat="server" />
                        </td>
                        <td>
                            进入本公司时间
                        </td>
                        <td>
                            <uc1:Date ID="dateJOIN_RAIL_DATE" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            毕业学校
                        </td>
                        <td>
                            <asp:TextBox ID="txtFinishSchool" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            毕业时间
                        </td>
                        <td>
                            <uc1:Date ID="dateGraduate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            所学专业
                        </td>
                        <td>
                            <asp:TextBox ID="txtMajor" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>
                            学校类别
                        </td>
                        <td>
                            <asp:DropDownList ID="dropSchoolCategory" runat="server" onchange="changeSchoolCategory();">
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
                    </tr>
                    <tr>
                        <td>
                            职工类型<span class="require">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEMPLOYEE_TYPE_ID" runat="server" onchange="changeZhiGongLeiXing();">
                                <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                                <asp:ListItem Value="0">工人</asp:ListItem>
                                <asp:ListItem Value="1">技术干部</asp:ListItem>
                                <asp:ListItem Value="2">管理干部</asp:ListItem>
                                <asp:ListItem Value="3">其他干部</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <div id="lblEMPLOYEE_TYPE_Txt">
                                技术职称
                            </div>
                        </td>
                        <td>
                            <div id="divTECHNICAL_TITLE_ID">
                                <asp:DropDownList ID="ddlTECHNICAL_TITLE_ID" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div id="divTECHNICIAN_TYPE_ID">
                                <asp:DropDownList ID="ddlTECHNICIAN_TYPE_ID" runat="server" Enabled="false">
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            技术职称聘任时间
                        </td>
                        <td>
                            <uc1:Date ID="dateTechnicalTitle" runat="server" />
                        </td>
                        <td>
                            技能等级获得时间
                        </td>
                        <td>
                            <uc1:Date ID="dateTechnicalDate" runat="server" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            班组长类型
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlWORKGROUPLEADER_TYPE_ID" runat="server" onchange="changeBanZuZhangLeiXing();">
                            </asp:DropDownList>
                        </td>
                        <td>
                            班组长下令日期
                        </td>
                        <td>
                            <uc1:Date ID="dateWORKGROUPLEADER_ORDER_DATE" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            是班组长<span class="require">*</span>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIsGroup" runat="server" Enabled="False">
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            在岗
                        </td>
                        <td>
                            <asp:CheckBox ID="cbISONPOST" runat="server"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            职教人员类型
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEDUCATION_EMPLOYEE_TYPE_ID" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            职教委员会职务
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCOMMITTEE_HEAD_SHIP_ID" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            在册
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlISREGISTERED" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlISREGISTERED_SelectedIndexChanged">
                                <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            运输业的职工类型
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEMPLOYEE_TRANSPORT_TYPE_ID" runat="server">
                                <asp:ListItem Value="-1">--请选择--</asp:ListItem>
                                <asp:ListItem Value="0">生产人员</asp:ListItem>
                                <asp:ListItem Value="1">管理人员</asp:ListItem>
                                <asp:ListItem Value="2">服务人员</asp:ListItem>
                                <asp:ListItem Value="3">其他人员</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            允许兼职的职名
                        </td>
                        <td>
                            <asp:TextBox ID="txtCOULD_POST_Name" runat="server" ReadOnly="true" Width="82%"></asp:TextBox>
                            <a onclick="selectYunXuJianZhi()" href="#">
                                <asp:Image runat="server" ID="Image1" ImageUrl="../Common/Image/search.gif" AlternateText="选择允许兼职的职名">
                                </asp:Image>
                            </a>
                        </td>
                        <td colspan="2">
                            <div id="divJianZhi1">
                                兼职1 :
                                <select id='cmbJianZhi1' onchange='changeJianZhi1();'>
                                </select>
                                &nbsp;&nbsp;&nbsp;&nbsp;兼职2 :
                                <select id='cmbJianZhi2' onchange='changeJianZhi2();'>
                                </select>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            技术档案编号
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtTechnicalCode" runat="server" Width="98%" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtMEMO" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div align="center">
                    <asp:Button ID="btnSave1" runat="server" CssClass="button" Text="保  存" OnClick="btnSave_Click" />
                    <asp:Button ID="btnClose1" runat="server" CssClass="button" Text="关  闭" OnClick="btnClose_Click" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <asp:HiddenField ID="hfStationOrgID" runat="server" />
        <asp:HiddenField ID="hfPostID" runat="server" />
        <asp:HiddenField ID="hfNowPostID" runat="server" />
        <asp:HiddenField ID="hfCOULD_POST_ID" runat="server" />
        <asp:HiddenField ID="hfSECOND_POST_ID" runat="server" />
        <asp:HiddenField ID="hfTHIRD_POST_ID" runat="server" />
    </form>
</body>
</html>
