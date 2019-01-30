<%@ Page Language="C#" AutoEventWireup="true" Codebehind="OnlineExamResultList.aspx.cs"
    Inherits="RailExamWebApp.Online.Exam.OnlineExamResultList" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>参加考试</title>

    <script type="text/javascript" src="../../Common/JS/Common.js"></script>

    <script type="text/javascript">
       function btnViewExamResult(examResultId,type,orgid)
        {
            var isServer = document.getElementById("hfIsServer").value;
            var NowOrgID = document.getElementById("hfOrgID").value;
            if(isServer == "False")
            {
                if(NowOrgID !=orgid)
               {
                    alert("请连接路局查询您的试卷！");
                    return;
               } 
            }
            
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
            if(type == 0 )
            {
                var re= window.open("/RailExamBao/Online/Exam/ExamResult.aspx?id=" + examResultId+"&orgid="+orgid,
                    "ExamResult",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
                re.focus();
            }
            else
            {
                var re= window.open("/RailExamBao/RandomExam/RandomExamAnswerNew.aspx?id=" +examResultId +"&orgid="+orgid,
                    "ExamResult",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
                re.focus();
            }
        }
       
       function OutPutPaper(id,type,orgid )
        {
            var isServer = document.getElementById("hfIsServer").value;
            var NowOrgID = document.getElementById("hfOrgID").value;
            if(isServer == "False")
            {
                if(NowOrgID !=orgid)
               {
                    alert("请连接路局导出您的试卷！");
                    return;
               } 
            }
            
            if(type == 0)
            {
                form1.OutPut.value=id;
                form1.OutPutOrg.value=orgid; 
	            form1.submit();
	            form1.OutPut.value = "";        
	           form1.OutPutOrg.value=""; 
            } 
            else
            {
                var ret = showCommonDialog("/RailExamBao/RandomExam/OutputPaperAllNew.aspx?eid="+id+"&OrgID="+ orgid +"&Mode=one",'dialogWidth:320px;dialogHeight:30px;');
                if(ret != "" )
                {
                   form1.OutPutRandom.value =ret;
                   form1.submit();
                }
            }
        } 
        
       function ItemDetail(itemID,examID)
      {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
            
            var bookID ="0";
            var chapterID = "0";
            var rangeType = "0";
            var begin = "";
            var end ="";
            var employeeID = document.getElementById('hfEmployeeID').value;
            var ret = window.open("/RailExamBao/RandomExam/RandomExamStatisticDetail.aspx?BookID="+bookID+"&ChapterID="+chapterID+"&RangeType="+rangeType+"&RandomExamItemID="+itemID+"&BeginDate="+begin+"&EndDate="+end+"&ExamID="+examID+"&EmployeeID="+employeeID,'RandomExamStatisticDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
            ret.focus();
      }
     
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../../Main/EmployeeDesktop.aspx'">
                    </div>
                    <div id="parent">
                        我的工作台</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        成绩查询</div>
                </div>
                <div id="button">
                </div>
            </div>
            <div style="text-align: left">
                <div id="grid" style="height: 310px; overflow: auto;">
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
                    <ComponentArt:Grid ID="gvExam" runat="server" AllowPaging="true" PageSize="9" Width="100%">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="ExamResultId">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="ExamId" Visible="false" />
                                    <ComponentArt:GridColumn DataField="ExamResultId" Visible="false" />
                                    <ComponentArt:GridColumn DataField="ExamType" Visible="false" />
                                    <ComponentArt:GridColumn DataField="OrganizationId" Visible="false" />
                                    <ComponentArt:GridColumn DataField="BeginDateTime" HeadingText="开始时间" FormatString="yyyy-MM-dd HH:mm"
                                        Visible="false" />
                                    <ComponentArt:GridColumn DataField="EndDateTime" HeadingText="结束时间" FormatString="yyyy-MM-dd HH:mm"
                                        Visible="false" />
                                    <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" />
                                    <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="考试时间" />
                                    <ComponentArt:GridColumn DataField="OrganizationName" HeadingText="考试地点" />
                                    <ComponentArt:GridColumn DataField="Score" HeadingText="分数" />
                                    <ComponentArt:GridColumn DataCellClientTemplateId="ct1" HeadingText="操作" AllowSorting="False" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="ClientTemplate3">
                                ## DataItem.getMember("BeginDateTime").get_value().toLocaleString()## / ## DataItem.getMember("EndDateTime").get_value().toLocaleString()##
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="ct1">
                                <a onclick="btnViewExamResult(##DataItem.getMember('ExamResultId').get_value()##,##DataItem.getMember('ExamType').get_value()##,##DataItem.getMember('OrganizationId').get_value()##)"
                                    href="#"><b>查看试卷</b></a> <a onclick="OutPutPaper(##DataItem.getMember('ExamResultId').get_value()##,##DataItem.getMember('ExamType').get_value()##,##DataItem.getMember('OrganizationId').get_value()##)"
                                        href="#" style="display: none;"><b>导出答卷</b></a>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </div>
                <br />
                <br />
                <br />
                <div style="width: 100%; height: 25px; background-image: url(/RailExamBao/App_Themes/Default/Images/head.gif);
                    background-repeat: repeat-x; padding-left: 10px; padding-right: 10px; line-height: 25px;
                    text-align: center; color: #2D61BA; font-size: 12px; font-weight: bold;">
                    历次考试试题错误信息
                </div>
                <div style="text-align: center">
                    <ComponentArt:Grid ID="examsGrid" runat="server" AutoAdjustPageSize="false" PageSize="5">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="ItemID">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="RandomExamItemID" Visible="false" />
                                    <ComponentArt:GridColumn DataField="Content" HeadingText="试题内容" Align="Left" Width="290" />
                                    <ComponentArt:GridColumn DataField="BookName" HeadingText="所属教材" Width="200" Align="Left" />
                                    <ComponentArt:GridColumn DataField="ChapterName" HeadingText="所属章节" Width="280" Align="Left" />
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
                                    src="/RailExamBao/Common/Image/edit_col_edit.gif" />
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <ComponentArt:CallBack ID="CallBack1" OnCallback="CallBack1_CallBack" runat="server"
            RefreshInterval="30000">
        </ComponentArt:CallBack>
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <asp:HiddenField ID="hfIsServer" runat="server" />
        <asp:HiddenField ID="hfEmployeeID" runat="server" />
        <input type="hidden" name="OutPut" />
        <input type="hidden" name="OutPutRandom" />
        <input type="hidden" name="OutPutOrg" />
        <input type="hidden" name="OutPutRandomOrg" />
    </form>
</body>
</html>
