<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TJ_ExamResultExport.aspx.cs"  validateRequest="false" 
    Inherits="RailExamWebApp.RandomExam.TJ_ExamResultExport" %>

<%@ Register Src="../Common/Control/Date/DateTimeUC.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <base target="_self" />
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
    
        function selectExam() 
        {
        	var strSql = "1=1";
        	
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

        	document.getElementById("hfSql").value = strSql;

        	var examId = document.getElementById("hfExamId").value;
        	var ret = window.showModalDialog("/RailExamBao/RandomExam/TJ_SelectExam.aspx",window,'help:no;status:no;dialogWidth:800px;dialogHeight:600px;');
        	//alert(ret);
	 	    if(ret != null && ret != "") {
	 	        document.getElementById("hfExamId").value = ret.split('$')[0];
	 		    document.getElementById("txtSelectExam").value = ret.split('$')[1];
	 	    	document.getElementById("hfSelect").value = "0";
	 	    }
        }
        
      function ShowProgressBarExcel()
      {

      	var search = document.getElementById("hfExamId").value;
      	
      	if(search == "") {
      		alert("请至少选择一个考试");
      		return false;
      	}
      	
      	if(document.getElementById("hfSelect").value!="1") {
      		alert("请先点击查询按钮查询数据");
      		return false;
      	}
      	
     	var ret = window.showModalDialog("/RailExamBao/RandomExam/ExportExcel.aspx?eid="+search+"&Type=newExam",
                        '', 'help:no; status:no; dialogWidth:320px;dialogHeight:50px;scroll:no;');
     	if(ret != "")
        {
           form1.RefreshExcel.value =ret;
           form1.submit();
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
                        考试成绩统计</div>
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
                                考试时间：</td>
                            <td colspan="3">
                                <uc1:Date ID="dateBeginTime" runat="server" />
                                至<uc1:Date ID="dateEndTime" runat="server" />
                            </td>
                            <td style="height: 24px">
                                考试名称：</td>
                            <td>
                                <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                            </td>
                            <td style="text-align: right; height: 24px;" colspan="2">
                                <asp:Button ID="btnQuery" runat="server" Text="查  询" CssClass="button" OnClick="btnQuery_Click" />
                                <asp:Button ID="btnExcel" runat="server" Text="导出Excel" CssClass="button"  OnClientClick="ShowProgressBarExcel()"  />
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
                    </table>
                </div>
                <div style="text-align: center; overflow-x: auto;overflow-y:no-display">
                    <yyc:SmartGridView ID="grdEntity" runat="server" AllowPaging="true" PageSize="10"
                        OnPageIndexChanging="grdEntity_PageIndexChanging" AllowSorting="true" AutoGenerateColumns="false"
                        OnRowCreated="grdEntity_RowCreated">
                        <Columns>
                              <asp:BoundField DataField="RANDOM_EXAM_RESULT_ID" HeaderText="RANDOM_EXAM_RESULT_ID" Visible="False"
                                ReadOnly="True"/>
                                <asp:BoundField DataField="EXAMINEE_NAME" HeaderText="考生姓名" 
                                ReadOnly="True"/>
                                <asp:BoundField DataField="Work_No" HeaderText="员工编码" 
                                ReadOnly="True"/>
                                <asp:BoundField DataField="Post_Name" HeaderText="职名"
                                ReadOnly="True"/>
                                <asp:BoundField DataField="ORG_NAME" HeaderText="考生单位" 
                                ReadOnly="True"/>
                                <asp:BoundField DataField="Station_Org_ID" HeaderText="考生单位1"  Visible="False"
                                ReadOnly="True"/>
                               <asp:BoundField DataField="EXAM_NAME" HeaderText="考试名称" 
                                ReadOnly="True"/>
                                <asp:BoundField DataField="Exam_Org_Name" HeaderText="考试地点" 
                                ReadOnly="True"/>
                                <asp:BoundField DataField="Exam_Time_Str" HeaderText="考试时间" 
                                ReadOnly="True"/>
                                <asp:BoundField DataField="SCORE" HeaderText="成绩" 
                                ReadOnly="True"/>
                        </Columns>
                    </yyc:SmartGridView>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfSql" runat="server" />
        <asp:HiddenField ID="hfSelect" runat="server" />
        <input type="hidden" name="RefreshExcel" />
</form>
</body>
</html>
