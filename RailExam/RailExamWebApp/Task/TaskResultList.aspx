<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TaskResultList.aspx.cs" Inherits="RailExamWebApp.Task.TaskResultList" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>作业列表 - 作业信息</title>
    <script src="../Common/JS/JSHelper.js" type="text/javascript">
    </script>
    <script type="text/javascript">
        function selectOrganizationBtnClicked()
        {
            var selectedOrganization = window.showModalDialog('../Common/SelectOrganization.aspx', 
                '', 'help:no; status:no; dialogWidth:300px;dialogHeight:575px');
            
            if(!selectedOrganization)
            {
                return;
            }
            $F("hfOrganizationId").value = selectedOrganization.split('|')[0];
            $F("txtOrganization").value = selectedOrganization.split('|')[1];
        }
        
        function toggleButton_onClick()
        {
            if($F("query").style.display == '')
            {
                $F("query").style.display = 'none';
            }
            else
            {
                $F("query").style.display = '';
            }
        }
        
        function searchBtnClicked()
        {
            $F("hfEmployeeName").value = $F("txtUserName").value;
            $F("hfScoreLower").value = $F("txtScoreLower").value;
            $F("hfScoreUpper").value = $F("txtScoreUpper").value;
            
            searchTaskResultCallBack.callback();
        }      
        
        function judgeTaskResult(id, judgeStatusId)
        {
            if(!id || !parseInt(id) || !judgeStatusId || !parseInt(judgeStatusId))
            {
                alert("不正确的数据！");
                
                return;
            }
            
            //alert(id + ", " + judgeStatusId);
            var winJudge;
            switch(parseInt(judgeStatusId))
            {
                case 1:
                case 2:
                {
                    winJudge = window.open("TaskJudge.aspx?id=" + id , 
                        "JudgeTaskResult", "height:600; width: 800,resizable=no,scrollbars=yes", false);
                        
                    break;
                }
                case 3:
                {
                    if(confirm("该作业已批阅，要重新批阅吗？"))
                    {
                        winJudge = window.open("TaskJudge.aspx?id=" + id , 
                            "JudgeTaskResult", "height:600; width: 800,resizable=no,scrollbars=yes", false);
                    }
                    
                    break;
                }
                default:
                {
                    alert("数据错误！");
                    break;
                }
            }
        }      
        
        function viewTaskResult(id)
        {
            var winView = window.open("TaskResult.aspx?id=" + id , 
                "JudgeTaskResult", "height:600; width: 800,resizable=no,scrollbars=yes", false);
        }  
        
        function changeTaskGrade(id)
        {
            var winGrade = window.open("TaskGrade.aspx?id=" + id , 
                "JudgeTaskResult", "height:600; width: 800,resizable=no,scrollbars=yes", false);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        作业列表
                    </div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        作业信息
                    </div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img id="toggleButton" onclick='toggleButton_onClick();' src="../Common/Image/find.gif"
                        alt="" />
                </div>
            </div>
            <div id="content">
                <div id="query" style="display: none;">
                    单位
                    <input id="txtOrganization" maxlength="20" size="10" type="text" />
                    <img id="imgSelectOrganization" name="imgSelectOrganization" src="../Common/Image/search.gif"
                        onclick='javascript:selectOrganizationBtnClicked();' />&nbsp;&nbsp; 姓名
                    <input id="txtUserName" type="text" maxlength="20" size="10" />&nbsp;&nbsp; 得分从
                    <input id="txtScoreLower" type="text" maxlength="5" size="5" />
                    到
                    <input id="txtScoreUpper" type="text" maxlength="5" size="5" />&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlTaskResultStatus" runat="server" DataSourceID="odsTaskResultStatus"
                        DataTextField="StatusName" DataValueField="TaskResultStatusId">
                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                    <img id="btnSearch" alt="查询" onclick='javascript:searchBtnClicked();' src="../Common/Image/confirm.gif" />
                </div>
                <ComponentArt:CallBack ID="searchTaskResultCallBack" runat="server" PostState="true"
                    OnCallback="searchTaskResultCallBack_Callback">
                    <Content>
                        <ComponentArt:Grid ID="taskResultGrid" runat="server" AutoAdjustPageSize="false"
                            AutoCallBackOnDelete="true" AutoCallBackOnUpdate="true" DataSourceID="odsTaskResults"
                            Debug="false" ManualPaging="true" OnDataBinding="taskResultGrid_OnDataBinding"
                            OnPageIndexChanged="taskResultGrid_OnPageIndexChanged" OnSortCommand="taskResultGrid_OnSortCommand"
                            PageSize="15" RunningMode="callback">
                            <Levels>
                                <ComponentArt:GridLevel DataKeyField="ExamResultId">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="姓名" />
                                        <ComponentArt:GridColumn DataField="OrganizationName" HeadingText="单位" />
                                        <ComponentArt:GridColumn DataField="TaskResultId" HeadingText="编号" />
                                        <ComponentArt:GridColumn DataField="Score" HeadingText="成绩" />
                                        <ComponentArt:GridColumn DataField="BeginTime" HeadingText="开始时间" />
                                        <ComponentArt:GridColumn DataField="EndTime" HeadingText="结束时间" />
                                        <ComponentArt:GridColumn DataField="UsedTimeString" HeadingText="答题时长" />
                                        <ComponentArt:GridColumn DataField="JudgeName" HeadingText="评卷人" />
                                        <ComponentArt:GridColumn DataField="StatusName" HeadingText="状态" />
                                        <ComponentArt:GridColumn DataField="StatusId" HeadingText="状态" Visible="false" />
                                        <ComponentArt:GridColumn AllowSorting="false" Width="70" DataCellClientTemplateId="CTEdit"
                                            HeadingText="操作" />
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </Levels>
                            <ClientTemplates>
                                <ComponentArt:ClientTemplate ID="CTEdit">
                                    <img id="img_##DataItem.getMember('TaskResultId').get_value()##" alt="批阅" name="img_##DataItem.getMember('TaskResultId').get_value()##"
                                        onclick='javascript:judgeTaskResult("##DataItem.getMember("TaskResultId").get_value()##", "##DataItem.getMember("StatusId").get_value()##");'
                                        src="../Common/Image/edit_col_edit.gif" />
                                    <img id="img_##DataItem.getMember('TaskResultId').get_value()##_view" alt="查阅" name="img_##DataItem.getMember('TaskResultId').get_value()##"
                                        onclick='javascript:viewTaskResult("##DataItem.getMember("TaskResultId").get_value()##");'
                                        src="../Common/Image/edit_col_see.gif" />
                                    <img id="img1" alt="更改成绩" name="img_##DataItem.getMember('TaskResultId').get_value()##"
                                        onclick='javascript:changeTaskGrade("##DataItem.getMember("TaskResultId").get_value()##", "##DataItem.getMember("StatusId").get_value()##");'
                                        src="../Common/Image/edit_col_edit.gif" />
                                </ComponentArt:ClientTemplate>
                            </ClientTemplates>
                        </ComponentArt:Grid>
                    </Content>
                </ComponentArt:CallBack>
            </div>
        </div>
        <div>
            <asp:HiddenField ID="hfTaskResultId" runat="server" Value="-1" />
            <asp:HiddenField ID="hfOrganizationId" runat="server" Value="-1" />
            <asp:HiddenField ID="hfTrainTypeId" runat="server" Value="-1" />
            <asp:HiddenField ID="hfEmployeeName" runat="server" Value="null" />
            <asp:HiddenField ID="hfScoreLower" runat="server" Value="-1" />
            <asp:HiddenField ID="hfScoreUpper" runat="server" Value="-1" />
            <asp:HiddenField ID="hfJudgeId" runat="server" Value="-1" />
            <asp:HiddenField ID="hfIsPass" runat="server" Value="-1" />
        </div>
        <div>
            <asp:ObjectDataSource ID="odsTaskResultStatus" runat="server" SelectMethod="GetTaskResultStatuses"
                TypeName="RailExam.BLL.TaskResultStatusBLL" DataObjectTypeName="RailExam.Model.TaskResultStatus">
                <SelectParameters>
                    <asp:Parameter Name="bForSearchUse" DefaultValue="true" Type="boolean" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsTaskResults" runat="server" SelectMethod="GetTaskResults"
                OnObjectCreated="odsTaskResults_OnDataObjectCreated" TypeName="RailExam.BLL.TaskResultBLL"
                DataObjectTypeName="RailExam.Model.TaskResult">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfOrganizationId" PropertyName="Value" DefaultValue="-1"
                        Name="organizationId" Type="Int32" />
                    <asp:QueryStringParameter ConvertEmptyStringToNull="False" DefaultValue="-1" Name="paperId"
                        QueryStringField="id" Type="Int32" />
                    <asp:ControlParameter ControlID="hfTrainTypeId" DefaultValue="-1" Name="trainTypeId"
                        PropertyName="Value" Type="Int32" />
                    <asp:ControlParameter ControlID="hfEmployeeName" DefaultValue="-1" Name="employeeName"
                        PropertyName="Value" Type="String" />
                    <asp:ControlParameter ControlID="hfScoreLower" ConvertEmptyStringToNull="False" DefaultValue="-1"
                        Name="scoreLower" PropertyName="Value" Type="Decimal" />
                    <asp:ControlParameter ControlID="hfScoreUpper" ConvertEmptyStringToNull="False" DefaultValue="-1"
                        Name="scoreUpper" PropertyName="Value" Type="Decimal" />
                    <asp:ControlParameter ControlID="ddlTaskResultStatus" DefaultValue="-1" Name="statusId"
                        PropertyName="SelectedValue" Type="Int32" />
                    <asp:ControlParameter ControlID="taskResultGrid" ConvertEmptyStringToNull="False"
                        DefaultValue="-1" Name="currentPageIndex" PropertyName="CurrentPageIndex" Type="Int32" />
                    <asp:ControlParameter ControlID="taskResultGrid" ConvertEmptyStringToNull="False"
                        DefaultValue="-1" Name="pageSize" PropertyName="PageSize" Type="Int32" />
                    <asp:ControlParameter ControlID="taskResultGrid" ConvertEmptyStringToNull="False"
                        DefaultValue="-1" Name="orderBy" PropertyName="Sort" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
