<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamCountStatistic.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamCountStatistic" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>站段汇总</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">
   //查看站段考试详情
   function showItem(orgid)
   {  
            var beginTime=document.getElementById("beginTime").value;
            var endTime=document.getElementById("endTime").value;
        	var style=document.getElementById("ddlStyle").value;
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;  
            var showWindow = window.open('RandomExamCountStatisticDetail.aspx?OrgID=' + orgid+'&beginTime='+beginTime+'&endTime='+endTime+'&style='+style,
                'ItemDetail',' top=0,left=0,width=800,height=600,left='+cleft+',top='+ctop+',status=false,resizable=no');
            showWindow.focus(); 
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
                        考试管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        站段汇总</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <asp:Button ID="btnSelect" runat="server" CssClass="buttonSearch" Text="查询" ToolTip="查询符合条件的内容"
                        OnClick="btnSelect_Click" />
                    <asp:Button runat="server" ID="btnOutPut" Text="导出Excel" CssClass="button" OnClick="btnOutPut_Click" />&nbsp;
                </div>
            </div>
            <div id="content">
                <div style="text-align: left;">
                    时间范围 从
                    <uc1:Date ID="dateStartDateTime" runat="server" />
                    到
                    <uc1:Date ID="dateEndDateTime" runat="server" />
                    &nbsp;&nbsp; 考试类型
                    <asp:DropDownList ID="ddlStyle" runat="server">
                        <asp:ListItem Text="--请选择--" Selected="true" Value="0"></asp:ListItem>
                        <asp:ListItem Text="不存档考试" Value="1"></asp:ListItem>
                        <asp:ListItem Text="存档考试" Value="2"></asp:ListItem>
                    </asp:DropDownList></div>
                <div>
                    <ComponentArt:Grid ID="examsGrid" runat="server" AutoAdjustPageSize="false" PageSize="19">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="OrgID">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="OrgID" Visible="false" />
                                    <ComponentArt:GridColumn DataField="OrgName" HeadingText="站段单位" />
                                    <ComponentArt:GridColumn DataField="ExamCount" HeadingText="考试次数" />
                                    <ComponentArt:GridColumn DataField="EmployeeCount" HeadingText="参考人次" />
                                    <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="CTEdit">
                                <a onclick="showItem('##DataItem.getMember('OrgID').get_value()##');" href="#">
                                    <img alt="查看" border="0" src="../Common/Image/edit_col_see.gif" /></a>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </div>
            </div>
            <asp:HiddenField ID="beginTime" runat="server" />
            <asp:HiddenField ID="endTime" runat="server" />
            <asp:HiddenField runat="server" ID="hfStyle" />
        </div>
    </form>
</body>
</html>
