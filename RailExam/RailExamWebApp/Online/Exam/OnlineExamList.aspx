<%@ Page Language="C#" AutoEventWireup="true" Codebehind="OnlineExamList.aspx.cs"
    Inherits="RailExamWebApp.Online.Exam.OnlineExamList" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>参加考试</title>

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">
        function AttendExam(ExamId,PaperId,type)      
        {
            var employeeID = document.getElementById("hfEmployeeID").value;   
            //alert(employeeID);               
            if(type == 0)
            {
                var w=window.open("/RailExamBao/Exam/ExamKSTitle.aspx?ExamId="+ExamId+"&PaperId="+PaperId,"ExamKS","fullscreen=yes,toolbar=no,scrollbars=no");	
                w.focus();	
            } 
            else
            {               
                var ret = window.open("/RailExamBao/RandomExam/AttendExamStart.aspx?id="+ExamId+"&employeeID="+employeeID,"AttendExamStart","fullscreen=yes,toolbar=no,scrollbars=no");
                ret.focus();
           }
        }    
        
       function logout()
       {
           top.returnValue = "false";
       } 
    </script>

</head>
<body onbeforeunload="logout()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="parent">
                        在线考试</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        参加考试</div>
                </div>
                <div id="button">
                </div>
            </div>
            <div style="text-align: center;">
                <font style="font-size: large; color: #2D67CF; font-weight: bold">请点击列表中您要参加的考试的考试名称，开始考试</font>
            </div>
            <div style="text-align: left; width: 100%;">
                <table style="width: 100%; height: 25px; background-color: #EAF4FF; padding-left: 10px;
                    padding-right: 10px; line-height: 25px; color: #2D67CF; font-size: 12px">
                    <tr>
                        <td style="width: 8%;" align="right">
                            姓名：</td>
                        <td style="width: 20%;" align="left">
                            <asp:Label ID="lblName" runat="server"></asp:Label></td>
                        <td style="width: 8%;" align="right">
                            单位：</td>
                        <td style="width: 25%;" align="left">
                            <asp:Label ID="lblOrg" runat="server"></asp:Label></td>
                        <td style="width: 8%;" align="right">
                            职名：</td>
                        <td style="width: 25%;" align="left">
                            <asp:Label ID="lblPost" runat="server"></asp:Label></td>
                    </tr>
                </table>
                <div id="grid">
                    <ComponentArt:Grid ID="gvExam" runat="server" AllowPaging="true" PageSize="20" Width="100%">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="ExamId">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="ExamId" Visible="false" />
                                    <ComponentArt:GridColumn DataField="paperId" Visible="false" />
                                    <ComponentArt:GridColumn DataField="ExamType" Visible="false" />
                                    <ComponentArt:GridColumn DataField="BeginTime" HeadingText="开始时间" FormatString="yyyy-MM-dd HH:mm"
                                        Visible="false" />
                                    <ComponentArt:GridColumn DataField="EndTime" HeadingText="结束时间" FormatString="yyyy-MM-dd HH:mm"
                                        Visible="false" />
                                    <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" Width="300" Align="Center" />
                                    <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="有效时间"
                                        Width="220" Align="Center" />
                                    <ComponentArt:GridColumn DataField="StartModeName" HeadingText="开考模式" Width="100" />
                                    <ComponentArt:GridColumn DataField="ExamStyleName" HeadingText="考试类型" Width="100" />
                                    <%--                                    <ComponentArt:GridColumn DataCellClientTemplateId="ct1" HeadingText="操作" AllowSorting="False"
                                        Width="80" />--%>
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="ClientTemplate3">
                                ## DataItem.getMember("BeginTime").get_value().getYear()##-##DataItem.getMember("BeginTime").get_value().getMonth()+1##-##DataItem.getMember("BeginTime").get_value().getDate()##
                                ##DataItem.getMember("BeginTime").get_value().getHours()##:##DataItem.getMember("BeginTime").get_value().getMinutes()##
                                / ## DataItem.getMember("EndTime").get_value().getYear()##-##DataItem.getMember("EndTime").get_value().getMonth()+1##-##DataItem.getMember("EndTime").get_value().getDate()##
                                ##DataItem.getMember("EndTime").get_value().getHours()##:##DataItem.getMember("EndTime").get_value().getMinutes()##
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="ct1">
                                <table width="100%">
                                    <tr>
                                        <td align="center">
                                            <input type="button" class="button" value="参加考试" onclick="AttendExam(##DataItem.getMember('ExamId').get_value()##,##DataItem.getMember('paperId').get_value()##,##DataItem.getMember('ExamType').get_value()##)" />
                                        </td>
                                    </tr>
                                </table>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <input type="hidden" name="ApplyID" />
        <ComponentArt:CallBack ID="CallBack1" runat="server" RefreshInterval="30000" OnCallback="CallBack1_CallBack">
        </ComponentArt:CallBack>
        <asp:HiddenField ID="hfEmployeeID" runat="server" />
    </form>
</body>
</html>
