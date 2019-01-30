<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamStudent.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamStudent" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    
    <script type="text/javascript">    
      
      function selectEmployee()
      {
      	    var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
      
            var ret = window.open("/RailExamBao/RandomExam/RandomExamManageFourthAdd.aspx?type=select",'RandomExamManageFourthAdd','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
            ret.focus();
      }
      
      function exportTemplate()
      {
      	var ret = window.showModalDialog("/RailExamBao/RandomExam/ExportExcel.aspx?Type=StudentExamInfo", '', 'help:no;status:no;dialogWidth:320px;dialogHeight:30px;');
      	if (ret != "")
      	{
      		document.getElementById("hfRefreshExcel").value = ret;
      		document.getElementById("btnExcels").click();
      	}
      }
      
     function checkConfirm() {
     	if(document.getElementById("txtEmployee").value =="") {
     		alert("请选择学员！");
     		return false;
     	}

     	return true;
     }
    </script>
    <link href="/RailExamWebApp/Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
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
                        考试统计</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="查   询" OnClick="btnSelect_Click" OnClientClick="return checkConfirm();" />
                    <input type="button" id="btnExcel" class="button"  onclick="exportTemplate();" value="导出Excel" />
                </div>
            </div>
            <div id="content">
                <table>
                    <tr>
                        <td style="color: #2D67CF; vertical-align: bottom;">
                            学&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;员：
                        </td>
                        <td style="text-align: left; vertical-align: bottom;">
                            <asp:TextBox ID="txtEmployee" runat="server" ReadOnly="true"></asp:TextBox>
                            <img id="ImgEmployee" style="cursor: hand;" onclick="selectEmployee();" src="../Common/Image/search.gif"
                                alt="选择学员" border="0" runat="server" />
                        </td>
                        <td style="color: #2D67CF; vertical-align: bottom;">
                            时间范围：</td>
                        <td style="color: #2D67CF; text-align: left; vertical-align: bottom;" colspan="2">
                            从<asp:DropDownList ID="ddlYearSmall" runat="server">
                            </asp:DropDownList>年
                            <asp:DropDownList ID="ddlMonthSmall" runat="server">
                            </asp:DropDownList>月&nbsp;&nbsp;到
                            <asp:DropDownList ID="ddlYearBig" runat="server">
                            </asp:DropDownList>年
                            <asp:DropDownList ID="ddlMonthBig" runat="server">
                            </asp:DropDownList>月
                        </td>
                        <td>
                            考试类型：
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlExamMode" runat="server">
                                <asp:ListItem Selected="True" Value="0">全部</asp:ListItem>
                                <asp:ListItem Value="2">存档考试</asp:ListItem>
                                <asp:ListItem Value="1">不存档考试</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <div style="text-align: center">
                    <ComponentArt:Grid id="examsGrid" runat="server" autoadjustpagesize="false" pagesize="25">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="RandomExamResultID">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="RandomExamResultID" HeadingText="编号" Visible="false" />
                                    <ComponentArt:GridColumn DataField="ExamineeName" HeadingText="考生" Width="50" />
                                    <ComponentArt:GridColumn DataField="WorkNo" HeadingText="员工编码" Width="100" />
                                    <ComponentArt:GridColumn DataField="PostName" HeadingText="职名" Width="80" />
                                    <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" Width="150" />
                                    <ComponentArt:GridColumn DataField="ExamStyleName" HeadingText="考试类型" Width="60" />
                                    <ComponentArt:GridColumn DataField="OrganizationId" Visible="false" />
                                    <ComponentArt:GridColumn DataField="OrganizationName" HeadingText="考生单位" Width="80" />
                                    <ComponentArt:GridColumn DataField="ExamOrgName" HeadingText="考试地点" Width="80" />
                                    <ComponentArt:GridColumn DataField="BeginDateTime" HeadingText="开始时间" Width="122"
                                        DataType="System.DateTime" />
                                    <ComponentArt:GridColumn DataField="EndDateTime" HeadingText="结束时间" Width="123" DataType="System.DateTime" />
                                    <ComponentArt:GridColumn DataField="ExamTimeName" HeadingText="考试时间" Width="122"
                                      　 FormatString="yyyy-MM-dd" />
                                    <ComponentArt:GridColumn DataField="Score" HeadingText="成绩" DataType="System.Decimal"
                                        Width="40" />
                                </Columns>
                            </ComponentArt:GridLevel>
                            </Levels>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfBegin" runat="server" />
         <asp:HiddenField ID="hfEmployeeID" runat="server" />
        <asp:HiddenField ID="hfEnd" runat="server" />
        <input name="employee" type="hidden" />
        <input name="Refresh" type="hidden" />
        <asp:HiddenField ID="hfRefreshExcel" runat="server" />
        <asp:HiddenField ID="hfIsRef" runat="server" />
        <asp:Button ID="btnExcels" runat="server" Text="" style="display: none" OnClick="btnExcels_Click" />
    </form>
</body>
</html>
