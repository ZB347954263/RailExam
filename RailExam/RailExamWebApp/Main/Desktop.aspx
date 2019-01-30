<%@ Page Language="C#" AutoEventWireup="True" Codebehind="Desktop.aspx.cs" Inherits="RailExamWebApp.Main.Desktop" %>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考试信息</title>

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">              
        //评卷按钮点击事件处理函数
    	function judgePaper(eid,type,orgID)
    	{ 
    		var flagUpdate=document.getElementById("HfUpdateRight").value;   
    		var flagIsAdmin = document.getElementById("hfIsAdmin").value;
    		if(flagIsAdmin == "False" || flagUpdate=="False")
    		{
    			alert("您没有该操作的权限！");
    			return;
    		}
             
    		if(!eid || !parseInt(eid))
    		{
    			alert("不正确的数据！");
                
    			return;
    		}

    		if(form1.hfSuitRange.value != "1")
    		{
    			orgID=form1.hfOrgID.value;
    		}
            
    		var   cleft;   
    		var   ctop;   
    		cleft=(screen.availWidth-900)*.5;   
    		ctop=(screen.availHeight-600)*.5;   
            
    		if(type == 0)
    		{
    			var winGradeEdit = window.open("/RailExamBao/Exam/SelectExamResultEmployee.aspx?type=1&eid=" + eid +"&OrgID=" +orgID,
    				"SelectExamResultEmployee", "height=600, width=900,left="+cleft+",top="+ctop+",status=false,resizable=yes", true);
    			winGradeEdit.focus();
    		}
    		else
    		{
    			var winGradeEdit = window.open("/RailExamBao/RandomExam/RandomExamResultList.aspx?eid=" + eid +"&OrgID=" +orgID,
    				"RandomExamResultList", "height=600, width=900,left="+cleft+",top="+ctop+",status=false,resizable=yes", true);
    			winGradeEdit.focus();
    		}
    	}
        
    	function ControlExam(eid,orgID)
    	{ 
    		var   cleft;   
    		var   ctop;   
    		cleft=(screen.availWidth-800)*.5;   
    		ctop=(screen.availHeight-600)*.5; 
          
    		var flagUpdate=document.getElementById("HfUpdateRightControl").value; 
    		var flagIsAdmin = document.getElementById("hfIsAdminControl").value;
    		var mode="Edit";
            
    		if(flagIsAdmin == "False" || flagUpdate=="False")
    		{
    			alert("您没有该操作的权限！");
    			return;
    		}
             
    		var ret = window.open('/RailExamBao/RandomExam/RandomExamControlDetail.aspx?RandomExamID='+eid+'&OrgID='+orgID,'RandomExamControlDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
    		ret.focus();             
    	}
		
    	function Edit(id)
    	{
    		var ret = window.showModalDialog("/RailExamBao/RandomExamTai/ComputerApplyDetail.aspx?ID="+id+"&&mode=EditTwo"+"&num=" + Math.random(),"ComputerApplyDetail","status:false;dialogWidth:645px;dialogHeight:400px");
    		if(ret != "" && ret!=undefined) {
    			form1.RefreshComp.value = ret;
    			form1.submit();
    			form1.Refresh.value = "";
    		}
    	}
    
    	function PlanUp(planID) { 
    		var flagUpdate=document.getElementById("HfUpdateRightPlan").value; 
            
    		if(flagUpdate=="False")
    		{
    			alert("您没有该操作的权限！");
    			return;
    		}
    	
    		var returnvalue = window.showModalDialog('/RailExamBao/TrainManage/TrainPlanUp.aspx?planID=' + planID + "&num=" + Math.random(),
    			'', 'help:no; status:no; dialogWidth:850px;dialogHeight:730px');
    	}
    	function completeTransfer(id)
    	{
    		var ret = showCommonDialog('/RailExamBao/Systems/EmployeeTransferInDetail.aspx?transferID=' + id,'dialogWidth:600px;dialogHeight:250px;');
    		if(ret == "true")
    		{
    			form1.RefreshEmp.value =ret;
    			form1.submit();
    		}       
    	} 
	
	    
    	function showVersion() 
    	{
    		window.showModalDialog("ShowVersionInfo.aspx", "", "help:no; status:no;dialogHeight:300px;dialogWidth:400px;scroll:no;");
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
                    <!--<div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        考试系统</div>
                    <div id="separator">
                    </div>-->
                    <div id="current">
                        桌面</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button" style="color: #023895">
                    您的IP地址为：<asp:Label ID="lblIP" runat="server" />
                </div>
            </div>
            <div id="content">
                当前系统版本号为：<asp:Label ID="lblVersion" runat="server"></asp:Label><br />
                <div style="text-align: left;">
                    <ComponentArt:TabStrip ID="TabStrip1" runat="server" MultiPageId="desktopPage">
                        <Tabs>
                            <ComponentArt:TabStripTab Value="0" Text="正在进行中的考试" PageViewId="FirstPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="1" Text="已过期还未结束的考试" PageViewId="SecondPage">
                            </ComponentArt:TabStripTab>
                        </Tabs>
                    </ComponentArt:TabStrip>
                    <ComponentArt:MultiPage ID="desktopPage" Width="100%" runat="server" RunningMode="Callback">
                        <ComponentArt:PageView ID="FirstPage">
                            <div style="text-align: center; height: 220px;">
                                <ComponentArt:CallBack ID="searchExamCallBack" runat="server" Debug="false" PostState="true"
                                    OnCallback="searchExamCallBack_Callback">
                                    <content>
                            <ComponentArt:Grid ID="examsGrid" runat="server" AutoAdjustPageSize="false" PageSize="5"
                                DataSourceID="odsExams">
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="ExamId">
                                        <Columns>
                                             <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                                           <ComponentArt:GridColumn DataField="ExamId" Visible="false" />
                                            <ComponentArt:GridColumn DataField="OrgId" Visible="false" />
                                            <ComponentArt:GridColumn DataField="ExamType" Visible="false" />
                                            <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" />
                                            <ComponentArt:GridColumn DataField="ValidExamTimeDurationString" HeadingText="有效时间" />
                                            <ComponentArt:GridColumn DataField="StationName" HeadingText="单位" />
                                            <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="制卷人" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="CTEdit">
                                        <img id="img_##DataItem.getMember('ExamId').get_value()##" name="img_##DataItem.getMember('ExamId').get_value()##"
                                            alt="查看成绩" style="cursor: hand; border: 0;" onclick='javascript:judgePaper("##DataItem.getMember("ExamId").get_value()##","##DataItem.getMember("ExamType").get_value()##",##DataItem.getMember("OrgId").get_value()##);'
                                            src="../Common/Image/edit_col_edit.gif" />
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                            </ComponentArt:Grid>
                        </content>
                                </ComponentArt:CallBack>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="SecondPage">
                            <div style="text-align: center; height: 220px;">
                                <ComponentArt:Grid ID="Grid1" runat="server" AutoAdjustPageSize="false" PageSize="5"
                                    DataSourceID="odsOverExam">
                                    <levels>
                            <ComponentArt:GridLevel DataKeyField="RandomExamId">
                                <Columns>
                                    <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="ClientTemplate1"
                                        HeadingText="操作" />
                                    <ComponentArt:GridColumn DataField="RandomExamId" Visible="false" />
                                    <ComponentArt:GridColumn DataField="OrgId" Visible="false" />
                                    <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" />
                                    <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="有效时间" />
                                    <ComponentArt:GridColumn DataField="BeginTime" HeadingText="开始时间" FormatString="yyyy-MM-dd HH:mm"
                                        Visible="false" />
                                    <ComponentArt:GridColumn DataField="EndTime" HeadingText="结束时间" FormatString="yyyy-MM-dd HH:mm"
                                        Visible="false" />
                                    <ComponentArt:GridColumn DataField="StationName" HeadingText="单位" />
                                    <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="制卷人" />
                                    <ComponentArt:GridColumn DataField="StartModeName" HeadingText="开考模式" />
                                    <ComponentArt:GridColumn DataField="IsStartName" HeadingText="考试状态" />
                                    <ComponentArt:GridColumn ColumnType="CheckBox" DataField="HasPaper" HeadingText="生成试卷" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </levels>
                                    <clienttemplates>
                            <ComponentArt:ClientTemplate ID="ClientTemplate1">
                                <img id="img_##DataItem.getMember('RandomExamId').get_value()##" name="img_##DataItem.getMember('RandomExamId').get_value()##"
                                    alt="考试监控" style="cursor: hand; border: 0;" onclick='javascript:ControlExam("##DataItem.getMember("RandomExamId").get_value()##",##DataItem.getMember("OrgId").get_value()##);'
                                    src="../Common/Image/edit_col_edit.gif" />
                            </ComponentArt:ClientTemplate>
                        </clienttemplates>
                                    <clienttemplates>
                            <ComponentArt:ClientTemplate ID="ClientTemplate3">
                                ## DataItem.getMember("BeginTime").get_value().getYear()##-##DataItem.getMember("BeginTime").get_value().getMonth()+1##-##DataItem.getMember("BeginTime").get_value().getDate()##
                                ##DataItem.getMember("BeginTime").get_value().getHours()##:##DataItem.getMember("BeginTime").get_value().getMinutes()##
                                / ## DataItem.getMember("EndTime").get_value().getYear()##-##DataItem.getMember("EndTime").get_value().getMonth()+1##-##DataItem.getMember("EndTime").get_value().getDate()##
                                ##DataItem.getMember("EndTime").get_value().getHours()##:##DataItem.getMember("EndTime").get_value().getMinutes()##
                            </ComponentArt:ClientTemplate>
                        </clienttemplates>
                                </ComponentArt:Grid>
                            </div>
                        </ComponentArt:PageView>
                    </ComponentArt:MultiPage>
                </div>
                <div style="text-align: left;">
                    <ComponentArt:TabStrip ID="TabStrip2" runat="server" MultiPageId="desktopPage1">
                        <Tabs>
                            <ComponentArt:TabStripTab Value="0" Text="微机教室预订提醒" PageViewId="FirstPage1">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="1" Text="上报培训计划提醒" PageViewId="SecondPage1">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="2" Text="职员调入提醒" PageViewId="ThirdPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="3" Text="教材更新记录提醒" PageViewId="FourthPage">
                            </ComponentArt:TabStripTab>
                        </Tabs>
                    </ComponentArt:TabStrip>
                    <ComponentArt:MultiPage ID="desktopPage1" Width="100%" runat="server">
                        <ComponentArt:PageView ID="FirstPage1">
                            <div>
                                <div>
                                    <asp:Button ID="btnDelete" runat="server" Text="删除信息" CssClass="displayNone" OnClick="btnDelete_Click" />
                                </div>
                                <ComponentArt:Grid ID="grdEntity2" runat="server" PageSize="5" RunningMode="Callback">
                                    <levels>
                    <ComponentArt:GridLevel DataKeyField="COMPUTER_ROOM_APPLY_ID">
                        <Columns>
                            <ComponentArt:GridColumn DataCellClientTemplateId="CTEditComputer" HeadingText="操作" />
                            <ComponentArt:GridColumn DataField="COMPUTER_ROOM_APPLY_ID" HeadingText="微机教室申请"
                                Visible="false" />
                            <ComponentArt:GridColumn DataField="short_name" HeadingText="站段名" />
                            <ComponentArt:GridColumn DataField="apply_short_name" HeadingText="被申请站段名" />
                            <ComponentArt:GridColumn DataField="COMPUTER_ROOM_NAME" HeadingText="被申请教室名" />
                            <ComponentArt:GridColumn DataField="COMPUTER_ROOM_NAME" HeadingText="教室名" />
                            <ComponentArt:GridColumn DataField="APPLY_START_TIME" HeadingText="开始时间" Align="Left" />
                            <ComponentArt:GridColumn DataField="APPLY_END_TIME" HeadingText="结束时间职名" Align="Left" />
                            <ComponentArt:GridColumn DataField="APPLY_COMPUTER_NUMBER" HeadingText="申请机位" Align="Left" />
                            <ComponentArt:GridColumn DataField="Reply_Status_Name" HeadingText="申请状态" Align="Left" />
                            <ComponentArt:GridColumn DataField="REPLY_DATE" HeadingText="回复时间" Align="Left" />
                        </Columns>
                    </ComponentArt:GridLevel>
                </levels>
                                    <clienttemplates>
                    <ComponentArt:ClientTemplate ID="CTEditComputer">
                        <a href="#"
                          onclick="Edit(## DataItem.getMember('COMPUTER_ROOM_APPLY_ID').get_value()##)"  class="underline">
                            <img src="../Common/Image/edit_col_edit.gif" style="border: 0" alt="回复" /></a> 
                    </ComponentArt:ClientTemplate>
                </clienttemplates>
                                </ComponentArt:Grid>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="SecondPage1">
                            <div style="text-align: center">
                                <ComponentArt:Grid ID="grdPlan" runat="server" PageSize="5">
                                    <levels>
                     <ComponentArt:GridLevel DataKeyField="TRAIN_PLAN_ID" >
                        <Columns>
                       
                        <ComponentArt:GridColumn DataCellClientTemplateId="CTEditPlan" HeadingText="操作" />
                           <ComponentArt:GridColumn DataField="TRAIN_PLAN_ID" Visible="false" />
                         <ComponentArt:GridColumn DataField="YEAR" HeadingText="年度" />
                         <ComponentArt:GridColumn DataField="TRAIN_PLAN_NAME" HeadingText="计划名称" />
                         <ComponentArt:GridColumn DataField="LOCATION" HeadingText="培训地点" />
                         <ComponentArt:GridColumn DataField="BEGINDATE" HeadingText="开班日期" FormatString="yyyy-MM-dd" />
                         <ComponentArt:GridColumn DataField="ENDDATE" HeadingText="结束日期" FormatString="yyyy-MM-dd"/>
                         <ComponentArt:GridColumn DataField="MAKEDATE" HeadingText="制定日期" FormatString="yyyy-MM-dd"/>
                         <ComponentArt:GridColumn DataField="MEMO" HeadingText="备注"  Visible="false"/>
                         <ComponentArt:GridColumn DataField="TRAINPLAN_TYPE_NAME" HeadingText="培训计划类别" />
                         <ComponentArt:GridColumn DataField="EMPLOYEE_NAME" HeadingText="制定人" />
                         <ComponentArt:GridColumn DataField="SPONSOR_UNIT_NAME" HeadingText="主办单位" />
                         <ComponentArt:GridColumn DataField="UNDERTAKE_UNIT_NAME" HeadingText="承办单位" />
                         <ComponentArt:GridColumn DataField="TRAIN_PLAN_PHASE_NAME" HeadingText="计划阶段" Visible="false"/>
                         <ComponentArt:GridColumn DataField="ASSIST_UNIT" HeadingText="协办单位" />
      
                        </Columns>
                        </ComponentArt:GridLevel>
                                  </levels>
                                    <clienttemplates>
                    <ComponentArt:ClientTemplate ID="CTEditPlan">
                        <a href="javascript:PlanUp(## DataItem.getMember('TRAIN_PLAN_ID').get_value()##)">
                       <img src="../App_Themes/Default/Images/Menu/move_up.gif" style="border:0" alt="上报计划" />
                      </a> 
                    </ComponentArt:ClientTemplate>
                </clienttemplates>
                                </ComponentArt:Grid>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="ThirdPage">
                            <div style="text-align: center">
                                <ComponentArt:Grid ID="grdEmployee" runat="server" AutoAdjustPageSize="false" PageSize="5">
                                    <levels>
                            <ComponentArt:GridLevel DataKeyField="EmployeeTransferID">
                                <Columns>
                                     <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEditEmp" HeadingText="操作"  Width="50"/>
                                    <ComponentArt:GridColumn DataField="EmployeeTransferID" Visible="false" />
                                    <ComponentArt:GridColumn DataField="EmployeeID" Visible="false" />
                                    <ComponentArt:GridColumn DataField="TransferToOrgID" Visible="false" />
                                    <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="姓名"  Width="80"/>
                                    <ComponentArt:GridColumn DataField="TransferOutOrgName" HeadingText="调出单位" Width="120"/>
                                    <ComponentArt:GridColumn DataField="TransferToOrgName" HeadingText="调入单位" Width="120"/>
                                    <ComponentArt:GridColumn DataField="WorkNo" HeadingText="员工编码" Width="120"/>
                                    <ComponentArt:GridColumn DataField="PostNo" HeadingText="工作证号" Width="100" Visible="false"/>
                                    <ComponentArt:GridColumn DataField="PostName" HeadingText="职名" Width="120"/>
                                    <ComponentArt:GridColumn DataField="TransferOutDate" HeadingText="调出时间"   FormatString="yyyy-MM-dd" Width="100"/>
                                </Columns>
                            </ComponentArt:GridLevel>
                        </levels>
                                    <clienttemplates>
                            <ComponentArt:ClientTemplate ID="CTEditEmp">
                                <a onclick="completeTransfer('##DataItem.getMember('EmployeeTransferID').get_value()##');" href="#">
                                    <img alt="调入员工" border="0" src="../Common/Image/edit_col_see.gif" /></a>
                            </ComponentArt:ClientTemplate>
                        </clienttemplates>
                                </ComponentArt:Grid>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="FourthPage">
                            <div style="text-align: center">
                                <ComponentArt:Grid ID="dgUpdate" runat="server"  AllowPaging="true" PageSize="5">
                                    <levels>
                        <ComponentArt:GridLevel DataKeyField="bookChapterUpdateId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="bookChapterUpdateId" Visible="false" />
                                <ComponentArt:GridColumn DataField="BookId" Visible="false" />
                                <ComponentArt:GridColumn DataField="ChapterId" Visible="false" />
                                <ComponentArt:GridColumn DataField="UpdateObject" Visible="false" />
                                <ComponentArt:GridColumn DataField="BookNameBak" HeadingText="教材名称" />
                                <ComponentArt:GridColumn DataField="ChapterName" HeadingText="更改对象" />
                                <ComponentArt:GridColumn DataField="updatePerson" HeadingText="更改人" />
                                <ComponentArt:GridColumn DataField="updateDate" FormatString="yyyy-MM-dd" HeadingText="更改日期" />
                                <ComponentArt:GridColumn DataField="updateCause" HeadingText="更改原因" />
                                <ComponentArt:GridColumn DataField="updateContent" HeadingText="更改内容" />
                                <%-- <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" />
--%>
                            </Columns>
                        </ComponentArt:GridLevel>
                    </levels>
                                    <%-- <ClientTemplates>
                                <ComponentArt:ClientTemplate ID="CTEdit">
                                    <a onclick="ManageChapterUpdate('##DataItem.getMember('BookId').get_value()##','##DataItem.getMember('bookUpdateId').get_value()##','##DataItem.getMember('ChapterId').get_value()##','##DataItem.getMember('UpdateObject').get_value()##')"
                                        href="#"><b>修改</b></a>
                                </ComponentArt:ClientTemplate>
                            </ClientTemplates>--%>
                                </ComponentArt:Grid>
                            </div>
                        </ComponentArt:PageView>
                    </ComponentArt:MultiPage>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfNowOrgID" runat="server" />
        <asp:ObjectDataSource ID="odsExams" runat="server" SelectMethod="GetExamsInfoDesktop"
            TypeName="RailExam.BLL.ExamBLL">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfNowOrgID" Name="orgID" Type="int32" DefaultValue="1" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsOverExam" runat="server" SelectMethod="GetOverdueNotEndRandomExam"
            TypeName="RailExam.BLL.RandomExamBLL">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfNowOrgID" Name="orgID" Type="int32" DefaultValue="1" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfUpdateRightControl" runat="server" />
        <asp:HiddenField ID="HfUpdateRightPlan" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="hfIsAdmin" runat="server" />
        <asp:HiddenField ID="hfIsAdminControl" runat="server" />
        <asp:HiddenField ID="HfExamCategoryId" runat="server" />
        <asp:HiddenField ID="HfPaperCategoryId" runat="server" />
        <input type="hidden" name="hfOrgID" value='<%=PrjPub.CurrentLoginUser.StationOrgID %>' />
        <input type="hidden" name="hfSuitRange" value='<%=PrjPub.CurrentLoginUser.SuitRange %>' />
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <input name="RefreshEmp" type="hidden" />
        <input name="RefreshComp" type="hidden" />
    </form>
</body>
</html>
