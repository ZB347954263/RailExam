<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InputRandomExamResult.aspx.cs"
    Inherits="RailExamWebApp.RandomExamOther.InputRandomExamResult" %>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>登记成绩</title>

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">              
        //评卷按钮点击事件处理函数
        function judgePaper(eid,orgID)
        { 
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5; 
          
            var flagUpdate=document.getElementById("HfUpdateRight").value; 
            var flagIsAdmin = document.getElementById("hfIsAdmin").value;
            var mode="Edit";
            
            if(flagIsAdmin == "False" || flagUpdate=="False")
            {
                  alert("您没有该操作的权限！请连接路局考试系统登记成绩！");
                  return;
            }
             
//          var ret = window.open('InputRandomExamResultDetail.aspx?RandomExamID='+eid+'&OrgID='+orgID,'RandomExamControlDetail','Width=850px; Height=650px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//		    ret.focus();
		    
		    var ret = showCommonDialog('/RailExamBao/RandomExamOther/InputRandomExamResultDetail.aspx?RandomExamID='+eid+'&OrgID='+orgID,'dialogWidth:850px;dialogHeight:700px;');
            if(ret == "true")
            {
		        form1.Refresh.value = ret ;
                form1.submit();
                form1.Refresh.value = "";
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
                        考试管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        登记成绩</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                </div>
            </div>
            <div id="content">
                <div style="text-align: center">
                    <ComponentArt:CallBack ID="searchExamCallBack" runat="server" Debug="false" PostState="true"
                        OnCallback="searchExamCallBack_Callback">
                        <Content>
                            <ComponentArt:Grid ID="examsGrid" runat="server" AutoAdjustPageSize="false" PageSize="19"
                                DataSourceID="odsExams">
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="ExamId">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="ExamId" Visible="false" />
                                            <ComponentArt:GridColumn DataField="OrgId" Visible="false" />
                                            <ComponentArt:GridColumn DataField="ExamType" Visible="false" />
                                            <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" />
                                            <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="有效时间" />
                                            <ComponentArt:GridColumn DataField="BeginTime" HeadingText="开始时间" FormatString="yyyy-MM-dd HH:mm"
                                                Visible="false" />
                                            <ComponentArt:GridColumn DataField="EndTime" HeadingText="结束时间" FormatString="yyyy-MM-dd HH:mm"
                                                Visible="false" />
                                            <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="制卷人" />
                                            <ComponentArt:GridColumn DataField="ExamineeCount" HeadingText="参考人次" Width="50" />
                                            <ComponentArt:GridColumn DataField="ExamAverageScore" HeadingText="平均成绩" Width="50" />
                                            <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="CTEdit">
                                        <img id="img_##DataItem.getMember('ExamId').get_value()##" name="img_##DataItem.getMember('ExamId').get_value()##"
                                            alt="登记成绩" style="cursor: hand; border: 0;" onclick='javascript:judgePaper("##DataItem.getMember("ExamId").get_value()##",##DataItem.getMember("OrgId").get_value()##);'
                                            src="../Common/Image/edit_col_edit.gif" />
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="ClientTemplate3">
                                        ## DataItem.getMember("BeginTime").get_value().getYear()##-##DataItem.getMember("BeginTime").get_value().getMonth()+1##-##DataItem.getMember("BeginTime").get_value().getDate()##
                                        ##DataItem.getMember("BeginTime").get_value().getHours()##:##DataItem.getMember("BeginTime").get_value().getMinutes()##
                                        / ## DataItem.getMember("EndTime").get_value().getYear()##-##DataItem.getMember("EndTime").get_value().getMonth()+1##-##DataItem.getMember("EndTime").get_value().getDate()##
                                        ##DataItem.getMember("EndTime").get_value().getHours()##:##DataItem.getMember("EndTime").get_value().getMinutes()##
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                            </ComponentArt:Grid>
                        </Content>
                    </ComponentArt:CallBack>
                </div>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsExams" runat="server" SelectMethod="GetIsNotComputerExamsInfo"
            TypeName="RailExam.BLL.ExamBLL">
            <SelectParameters>
                <asp:ControlParameter DefaultValue="1" Name="orgID" ControlID="hfOrgID" Type="int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="hfIsAdmin" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <input name="Refresh" type="hidden" />
    </form>
</body>
</html>
