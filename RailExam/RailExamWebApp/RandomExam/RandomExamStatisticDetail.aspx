<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamStatisticDetail.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamStatisticDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考试统计详情</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">
      function judgePaper(examResultId,orgid)
       {
                var re= window.open("/RailExamBao/RandomExam/RandomExamAnswerNew.aspx?id=" +examResultId +"&orgid="+orgid,
                    "ExamResult",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
                re.focus();
       }  
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div style="width: 750px; height: 25px; background-image: url(/RailExamBao/App_Themes/Default/Images/head.gif);
                background-repeat: repeat-x; padding-left: 10px; padding-right: 10px; line-height: 25px;
                text-align: center; color: #2D61BA; font-size: 12px; font-weight: bold;">
                试题信息</div>
            <table class="contentTable">
                <tr>
                    <td style="width: 10%;">
                        教材章节</td>
                    <td>
                        <asp:Label ID="lblBookChapter" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        试题内容
                    </td>
                    <td>
                        <%FillContent(); %>
                    </td>
                </tr>
                <tr>
                    <td>
                        候选项</td>
                    <td style="padding: 0; border-width: 0;">
                        <%FillAnswer(); %>
                    </td>
                </tr>
                <tr>
                    <td>
                        原试题正确选项</td>
                    <td>
                        <asp:Label ID="lblAnswer" runat="server"></asp:Label></td>
                </tr>
            </table>
            <br />
            <br />
            <div style="width: 750px; height: 25px; background-image: url(/RailExamBao/App_Themes/Default/Images/head.gif);
                background-repeat: repeat-x; padding-left: 10px; padding-right: 10px; line-height: 25px;
                text-align: center; color: #2D61BA; font-size: 12px; font-weight: bold;">
                错误试题详细信息</div>
            <ComponentArt:Grid ID="examsGrid" runat="server" AutoAdjustPageSize="false" PageSize="12" Width="750px">
                <Levels>
                    <ComponentArt:GridLevel>
                        <Columns>
                            <ComponentArt:GridColumn DataField="RandomExamResultID" Visible="false" />
                            <ComponentArt:GridColumn DataField="OrgID" Visible="false" />
                            <ComponentArt:GridColumn DataField="EmployeeID" Visible="false" />
                            <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="考生姓名" Width="60" />
                            <ComponentArt:GridColumn DataField="WorkNo" HeadingText="员工编码" Width="95" />
                            <ComponentArt:GridColumn DataField="OrgName" HeadingText="考生单位" Width="100" />
                            <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" Width="180"/>
                          <ComponentArt:GridColumn DataField="SelectAnswer" HeadingText="试题选项" Width="200" />  
                           <ComponentArt:GridColumn DataField="StandardAnswer" HeadingText="正确选项" Width="55" /> 
                            <ComponentArt:GridColumn DataField="Answer" HeadingText="错误选项" Width="55" />
                            <ComponentArt:GridColumn DataField="Score" HeadingText="分数" Width="55" />
                            <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="操作"
                                Width="80" />
                        </Columns>
                    </ComponentArt:GridLevel>
                </Levels>
                <ClientTemplates>
                    <ComponentArt:ClientTemplate ID="CTEdit">
                        <a onclick="judgePaper(##DataItem.getMember('RandomExamResultID').get_value()##,##DataItem.getMember('OrgID').get_value()##)"
                            href="#"><b>查看考卷</b></a>
                    </ComponentArt:ClientTemplate>
                </ClientTemplates>
            </ComponentArt:Grid>
        </div>
    </form>
</body>
</html>
