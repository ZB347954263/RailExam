<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamCountStatisticDetail.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamCountStatisticDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>站段考卷列表</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">
      function ShowProgressBar()
      {
         var search = window.location.search; 
         var ret = showCommonDialog("/RailExamBao/RandomExam/ExportExcel.aspx"+search+"&Type=ExamInfo",'dialogWidth:320px;dialogHeight:30px;');
         if(ret == "true")
         {
           form1.ExamInfo.value = "true";
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
                        站段汇总</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        站段考试列表</div>
                </div>
                <div id="button">
                    <asp:Button runat="server" ID="btnOutPut" Text="导出Excel" CssClass="button" OnClientClick="ShowProgressBar()" />&nbsp;
                </div>
            </div>
            <div id="content">
                <ComponentArt:Grid ID="examsGrid" runat="server" PageSize="18" Width="720">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="ExamId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="ExamId" Visible="false" />
                                <ComponentArt:GridColumn DataField="OrgId" Visible="false" />
                                <ComponentArt:GridColumn DataField="ExamType" Visible="false" />
                                <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" Width="258" />
                                <ComponentArt:GridColumn DataField="ValidExamTimeDurationString" HeadingText="有效时间"
                                    Width="130" />
                                <ComponentArt:GridColumn DataField="ExamStyleName" HeadingText="考试类型" />
                                <ComponentArt:GridColumn DataField="ExamineeCount" HeadingText="参考人次" Width="50" />
                                <ComponentArt:GridColumn DataField="ExamAverageScore" HeadingText="平均成绩" Width="50" />
                                <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="制卷人" Width="50" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                </ComponentArt:Grid>
            </div>
        </div>
        <input name="ExamInfo" type="hidden" />
    </form>
</body>
</html>
