<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TJ_RandomExamResult.aspx.cs" EnableEventValidation="false"
    Inherits="RailExamWebApp.RandomExam.TJ_RandomExamResult"  ValidateRequest="false"  %>

<%@ Register Src="../Common/Control/Date/DateTimeUC.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <base target="_self"/>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function selectArow(obj)
		{
			var TBL = document.getElementById("grdEntity");
			for (var i = 1; i < TBL.rows.length; i++)
			{
				TBL.rows[i].style.backgroundColor = "White";
				if (i % 2 != 0)
					TBL.rows[i].style.backgroundColor = "#EFF3FB";
			}
			obj.style.backgroundColor = "#FFEEC2";
		}
        
        function selectTrainPlan() {
        	var id = document.getElementById("hfTrainPlan").value;
        	var name = document.getElementById("txtTrainPlan").value;
        	var ret = window.showModalDialog("/RailExamBao/RandomExam/TJ_SelectTrainPlan.aspx?planID="+id+"&planName="+name,window,'help:no;status:no;dialogWidth:850px;dialogHeight:600px;');
	 	    if(ret != null) {
	 	    	//alert(ret);
	 	        document.getElementById("hfTrainPlan").value = ret[0];
	 		    document.getElementById("txtTrainPlan").value = ret[1];
                
	 	    	if(document.getElementById("hfTrainPlan").value!="") {
	 	    	    __doPostBack("btnClass");
	 	    	}
	 	    }
        }
        
        function selectExam() 
        {
        	var strSql = "1=1";
        	
        	var orgid = document.getElementById("ddlOrg").value;
        	if(orgid != "0") {
        		strSql = strSql + " and a.Org_ID=" + orgid;
        	}
        	var name = document.getElementById("txtName").value;
        	if(name != "") {
        		strSql = strSql + " and Exam_Name like '%" + name + "%'";
        	}
        	var begin = document.getElementById("dateBeginTime_DateBox").value;
        	var end = document.getElementById("dateEndTime_DateBox").value;
        	if(begin != "" && end == "") {
        		strSql = strSql + " and Begin_Time>=to_date('" + begin + "','YYYY-MM-DD HH24:MI:SS')";
        	}
        	if(begin == "" && end != "") {
        		strSql = strSql + " and Begin_Time-1<to_date('" + end + "','YYYY-MM-DD HH24:MI:SS')";
        	}
        	if(begin != "" && end != "") {
        		strSql = strSql + " and Begin_Time>=to_date('" + begin + "','YYYY-MM-DD HH24:MI:SS')"
	        		+ " and Begin_Time-1<to_date('" + end + "','YYYY-MM-DD HH24:MI:SS')";
        	}
        	var planId = document.getElementById("hfTrainPlan").value;
        	var classId =document.getElementById("ddlTrainClass").value;
        	if(planId != "" && classId == "0") {
        		strSql = strSql + " and Random_Exam_ID in (select Random_Exam_ID from Random_Exam_Train_Class a "
	        		+ " inner join ZJ_Train_Class b on a.Train_Class_ID=b.Train_Class_ID"
		        	+ " where b.Train_Plan_ID = " + planId+ ")";
        	}
        	
        	if(classId != "0") {
        		strSql = strSql + " and Random_Exam_ID in (select Random_Exam_ID from Random_Exam_Train_Class "
		        	+ " where Train_Class_ID = " + classId+ ")";
        	}

        	//alert(strSql);
        	document.getElementById("hfSql").value = strSql;

        	var examId = document.getElementById("hfExamId").value;
        	var ret = window.showModalDialog("/RailExamBao/RandomExam/TJ_SelectExam.aspx",window,'help:no;status:no;dialogWidth:800px;dialogHeight:600px;');
        	//alert(ret);
	 	    if(ret != null && ret != "") {
	 	        document.getElementById("hfExamId").value = ret.split('$')[0];
	 		    document.getElementById("txtSelectExam").value = ret.split('$')[1];
	 	    }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        考试统计</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        考试结果分析统计</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
            </div>
            <div id="content">
                <div style="text-align: left;">
                    <table width="100%">
                        <tr>
                            <td style="height: 24px">
                                考试组织单位：</td>
                            <td>
                                <asp:DropDownList ID="ddlOrg" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td style="height: 24px">
                                考试名称：</td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right; height: 24px;" colspan="2">
                                <asp:Button ID="btnQuery" runat="server" Text="查  询" CssClass="button" OnClick="btnQuery_Click" />
                                 <asp:Button ID="btnClass" runat="server" Visible="false" OnClick="btnClass_Click" />
                                <asp:Button ID="btnExcel" runat="server" Text="导出Excel" CssClass="button" OnClick="btnExcel_Click"  />
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 24px">
                                考试时间：</td>
                            <td colspan="3">
                                <uc1:Date ID="dateBeginTime" runat="server" />
                                至<uc1:Date ID="dateEndTime" runat="server" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                培训计划：</td>
                            <td>
                                <asp:TextBox ID="txtTrainPlan" runat="server"></asp:TextBox><img id="Img1"
                                    style="cursor: hand;" onclick="selectTrainPlan();" src="../Common/Image/search.gif"
                                    alt="选择培训计划" border="0" />
                                <asp:HiddenField runat="server" ID="hfTrainPlan" />
                            </td>
                            <td>
                                培训班：</td>
                            <td>
                                <asp:DropDownList ID="ddlTrainClass" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                选择考试：</td>
                            <td colspan="3">
                                <asp:HiddenField runat="server" ID="hfExamId" />
                                <asp:TextBox ID="txtSelectExam" runat="server" Width="90%"></asp:TextBox><img id="ImgSelectTrainType"
                                    style="cursor: hand;" onclick="selectExam();" src="../Common/Image/search.gif"
                                    alt="选择考试" border="0" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                统计方式：</td>
                            <td>
                                <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                    <asp:ListItem Value="1">汇总</asp:ListItem>
                                    <asp:ListItem Value="2">不汇总</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                汇总方式：
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlMode" runat="server">
                                    <asp:ListItem Value="1">站段</asp:ListItem>
                                    <asp:ListItem Value="2">车间</asp:ListItem>
                                    <asp:ListItem Value="3">职名</asp:ListItem>
                                    <asp:ListItem Value="4">工种(考试名称)</asp:ListItem>
                                     <asp:ListItem Value="5">文化程度</asp:ListItem>
                                      <asp:ListItem Value="6">年龄结构</asp:ListItem>
                                      <asp:ListItem Value="7">站段工种</asp:ListItem> 
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="text-align: center;overflow-x: auto;">
                    <yyc:SmartGridView ID="grdEntity" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="grdEntity_PageIndexChanging"
                        AllowSorting="true" AutoGenerateColumns="true" OnRowCreated="grdEntity_RowCreated">
                        <Columns>
                            <asp:BoundField DataField="" Visible="false"  />
                        </Columns>
                    </yyc:SmartGridView>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfSql" runat="server" />
    </form> 
</body>
</html>
