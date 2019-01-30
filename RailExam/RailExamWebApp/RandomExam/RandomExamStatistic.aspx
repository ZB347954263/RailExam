<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamStatistic.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamStatistic" %>

<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考试监控</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">    
        function selectChapter()
        {
	        var selectedChapter = window.showModalDialog('../Common/SelectStrategyChapter.aspx', 
                '', 'help:no; status:no; dialogWidth:370px;dialogHeight:680px');
            if(! selectedChapter)
            {
                return;
            }
            if( selectedChapter.split('|')[3]<3)
            {
                alert('请选择教材或教材章节！');
                document.getElementById('HfBookId').value ="";
                document.getElementById('HfChapterId').value ="";
                document.getElementById('HfRangeType').value = "";
                document.getElementById('HfRangeName').value = "";
                document.getElementById('txtBookChapter').value = ""; 
               return; 
            }
            document.getElementById('HfBookId').value =selectedChapter.split('|')[0];
            document.getElementById('HfChapterId').value = selectedChapter.split('|')[1];
            document.getElementById('HfRangeType').value = selectedChapter.split('|')[3];
            document.getElementById('HfRangeName').value = selectedChapter.split('|')[2];
            document.getElementById('txtBookChapter').value = selectedChapter.split('|')[2];
      }
      
      function selectEmployee()
      {
      	    var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
      
            var ret = window.open("/RailExamBao/RandomExam/RandomExamManageFourthAdd.aspx?type=select",'RandomExamManageFourthAdd','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
            ret.focus();
      }
      
      
      function selectExam()
      {
      	    var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
      
            var ret = window.open("/RailExamBao/RandomExam/RandomExamManageFourthChoose.aspx",'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
            ret.focus();
      }
      
      function ItemDetail(itemID,examID)
      {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
            
            var bookID =document.getElementById('HfBookId').value;
            var chapterID = document.getElementById('HfChapterId').value;
            var rangeType = document.getElementById('HfRangeType').value;
            var begin = document.getElementById('hfBegin').value;
            var end = document.getElementById('hfEnd').value;
            var employeeID = document.getElementById('hfEmployeeID').value;
            var ret = window.open("/RailExamBao/RandomExam/RandomExamStatisticDetail.aspx?BookID="+bookID+"&ChapterID="+chapterID+"&RangeType="+rangeType+"&RandomExamItemID="+itemID+"&BeginDate="+begin+"&EndDate="+end+"&ExamID="+examID+"&EmployeeID="+employeeID,'RandomExamStatisticDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
            ret.focus();
      }
      
      function exportTemplate()
      {
      	var ret = window.showModalDialog("/RailExamBao/RandomExam/ExportExcel.aspx?Type=examStatistic", '', 'help:no;status:no;dialogWidth:320px;dialogHeight:30px;');
      	if (ret != "")
      	{
      		document.getElementById("hfRefreshExcel").value = ret;
      		document.getElementById("btnExcels").click();
      	}
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
                    <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="查   询" OnClick="btnSelect_Click" />
                    <input type="button" id="btnExcel" class="button"  onclick="exportTemplate();" value="导出Excel" />
                </div>
            </div>
            <div id="content">
                <table >
                    <tr>
                        <td style="color: #2D67CF;vertical-align:bottom;">
                            时间范围：</td>
                        <td style="color: #2D67CF;text-align: left;vertical-align:bottom;" colspan="2">
                            从<asp:DropDownList ID="ddlYearSmall" runat="server">
                            </asp:DropDownList>年
                            <asp:DropDownList ID="ddlMonthSmall" runat="server">
                            </asp:DropDownList>月&nbsp;&nbsp;到
                            <asp:DropDownList ID="ddlYearBig" runat="server">
                            </asp:DropDownList>年
                            <asp:DropDownList ID="ddlMonthBig" runat="server">
                            </asp:DropDownList>月
                        </td> 
                        <td style="color: #2D67CF; width: 10%; text-align: left;vertical-align:bottom;">
                            <asp:RadioButton ID="rbnBook" Checked="true" GroupName="rbnSelect" runat="server" AutoPostBack="True"
                                Text="按教材章节" OnCheckedChanged="rbnBook_CheckedChanged" /></td>
                        <td style="color: #2D67CF; vertical-align:bottom;">
                            教材章节：
                        </td>
                        <td style="text-align: left;vertical-align:bottom;">
                            <asp:TextBox ID="txtBookChapter" runat="server" ReadOnly="true"></asp:TextBox>
                            <img id="ImgSelectChapterName" style="cursor: hand;" onclick="selectChapter();" src="../Common/Image/search.gif"
                                alt="选择教材章节" border="0" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="color: #2D67CF;vertical-align:bottom;" colspan="3"></td> 
                        <td style="color: #2D67CF; width: 10%; text-align: left;vertical-align:bottom;">
                            <asp:RadioButton ID="rbnExam" GroupName="rbnSelect" runat="server" Text="按考试场次" AutoPostBack="True"
                                OnCheckedChanged="rbnExam_CheckedChanged" /></td>
                        <td style="color: #2D67CF;vertical-align:bottom;">
                            考&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;试：
                        </td>
                        <td style="text-align: left;vertical-align:bottom;">
                            <asp:TextBox ID="txtExam" runat="server" ReadOnly="true"></asp:TextBox>
                            <img id="ImgExam" style="cursor: hand;" onclick="selectExam();" src="../Common/Image/search.gif"
                                alt="选择考试" border="0" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="color: #2D67CF;vertical-align:bottom;" colspan="3"></td> 
                        <td style="color: #2D67CF; width: 10%; text-align: left;vertical-align:bottom;">
                            <asp:RadioButton ID="rbnEmployee" GroupName="rbnSelect" runat="server" Text="按学员个人"
                                AutoPostBack="True" OnCheckedChanged="rbnEmployee_CheckedChanged" /></td>
                        <td style="color: #2D67CF;vertical-align:bottom;">
                            学&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;员：
                        </td>
                        <td style="text-align: left;vertical-align:bottom;">
                            <asp:TextBox ID="txtEmployee" runat="server" ReadOnly="true"></asp:TextBox>
                            <img id="ImgEmployee" style="cursor: hand;" onclick="selectEmployee();" src="../Common/Image/search.gif"
                                alt="选择学员" border="0" runat="server" />
                        </td>
                    </tr>
                </table>
                <div style="text-align: center">
                    <ComponentArt:Grid ID="examsGrid" runat="server"  PageSize="15" >
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="ItemID">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="RandomExamItemID" Visible="false" />
                                    <ComponentArt:GridColumn DataField="Content" HeadingText="试题内容" Align="Left" />
                                    <ComponentArt:GridColumn DataField="BookName" HeadingText="所属教材" Width="200" Align="Left" />
                                    <ComponentArt:GridColumn DataField="ChapterName" HeadingText="所属章节" Width="220" Align="Left" />
                                    <ComponentArt:GridColumn DataField="ErrorNum" HeadingText="错误次数" Width="55" />
                                    <ComponentArt:GridColumn DataField="ExamCount" HeadingText="出题次数" Width="55" />
                                    <ComponentArt:GridColumn DataField="ErrorRate" HeadingText="错误率" Width="55" />
                                   <ComponentArt:GridColumn DataField="RandomExamID" Visible="False"/> 
                                    <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="操作"
                                        Width="40" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="CTEdit">
                                <img id="img_##DataItem.getMember('RandomExamItemID').get_value()##" name="img_##DataItem.getMember('RandomExamItemID').get_value()##"
                                    alt="详细信息" style="cursor: hand; border: 0;" onclick='javascript: ItemDetail("##DataItem.getMember("RandomExamItemID").get_value()##","##DataItem.getMember("RandomExamID").get_value()##");'
                                    src="../Common/Image/edit_col_edit.gif" />
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="hfIsAdmin" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <input name="ChooseExamID" type="hidden" />
        <input name="employee" type="hidden" />
        <asp:HiddenField ID="HfChapterId" runat="server" />
        <asp:HiddenField ID="HfBookId" runat="server" />
        <asp:HiddenField ID="HfRangeType" runat="server" />
        <asp:HiddenField ID="HfRangeName" runat="server" />
        <asp:HiddenField ID="hfBegin" runat="server" />
        <asp:HiddenField ID="hfEnd" runat="server" />
        <asp:HiddenField ID="hfExamID" runat="server" />
        <asp:HiddenField  ID="hfEmployeeID" runat="server"/>
        <asp:HiddenField ID="hfRefreshExcel" runat="server" />
         <asp:HiddenField ID="hfIsRef" runat="server" />
        <asp:Button ID="btnExcels" runat="server" Text="" style="display: none" OnClick="btnExcels_Click" />
    </form>
</body>
</html>
