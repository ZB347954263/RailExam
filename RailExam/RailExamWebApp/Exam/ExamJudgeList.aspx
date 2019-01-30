<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ExamJudgeList.aspx.cs"
    Inherits="RailExamWebApp.Exam.ExamJudgeList" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>手工评卷 - 考试列表</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function $F(objId)
        {
            return document.getElementById(objId);              
        }  
    
        function QueryRecord()
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

        function searchButton_onClick()
        {
            searchExamCallBack.callback();
        }

        function judgePaper(eid)
        {
        
         var flagupdate=document.getElementById("HfUpdateRight").value;
        if(flagupdate=="False")
                      {
                        alert("您没有权限使用该操作！");                       
                        return;
                      }
                      
            if(!eid || !parseInt(eid))
            {
                alert("不正确的数据！");
                
                return;
            }

            var search = window.location.search;           
            
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;               
            
            var winGradeEdit = window.open("ExamPaperList.aspx?eid=" + eid ,
                "ExamPaperList", "height=600,width=800,scrollbars=yes,left="+cleft+",top="+ctop+",status=false,resizable=no,scrollbars=auto", true);
            winGradeEdit.focus();
      }
      
      function ViewRelustUpdateRecord()
      {      
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;               
            
            var winGradeEdit = window.open("ExamResultUpdateList.aspx" ,
                "ExamResultUpdateList", "height=600,width=800,scrollbars=yes,left="+cleft+",top="+ctop+",status=false,resizable=no,scrollbars=auto", true);
            winGradeEdit.focus();
      }
      
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        考试管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        手工评卷</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img alt="" onclick="ViewRelustUpdateRecord();" src="../Common/Image/resultupdate.gif" />&nbsp;
                    <img alt="" onclick="QueryRecord();" src="../Common/Image/find.gif" />
                </div>
            </div>
            <div id="content">
                <div id="query" style="display: none;">
                    &nbsp;&nbsp;考试名称
                    <asp:TextBox ID="txtExamName" runat="server"></asp:TextBox>
                    有效时间 从
                    <uc1:Date ID="dateStartDateTime" runat="server" />
                    到
                    <uc1:Date ID="dateEndDateTime" runat="server" />
                    <input id="searchButton" type="button" class="buttonSearch" title="查询符合条件的考卷" value="确  定"
                        onclick="searchButton_onClick();" />
                </div>
                <div id="mainContent">
                    <ComponentArt:CallBack ID="searchExamCallBack" runat="server" Debug="false" PostState="true"
                        OnCallback="searchExamCallBack_Callback">
                        <Content>
                            <ComponentArt:Grid ID="examsGrid" runat="server" PageSize="19" DataSourceID="odsExams">
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="ExamId">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="ExamId" Visible="false" />
                                            <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" />
                                            <ComponentArt:GridColumn DataField="CreateModeString" HeadingText="出题方式" />
                                            <ComponentArt:GridColumn DataField="ValidExamTimeDurationString" HeadingText="有效时间" />
                                            <ComponentArt:GridColumn DataField="ExamineeCount" HeadingText="参考人次" />
                                            <ComponentArt:GridColumn DataField="ExamAverageScore" HeadingText="平均成绩" />
                                            <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="制卷人" />
                                            <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="CTEdit">
                                        <img id="img_##DataItem.getMember('ExamId').get_value()##" name="img_##DataItem.getMember('ExamId').get_value()##"
                                            alt="评卷" style="cursor: hand; border: 0;" onclick='javascript:judgePaper("##DataItem.getMember("ExamId").get_value()##");'
                                            src="../Common/Image/edit_col_edit.gif" />
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                            </ComponentArt:Grid>
                        </Content>
                    </ComponentArt:CallBack>
                </div>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsExams" runat="server" SelectMethod="GetExams" TypeName="RailExam.BLL.ExamBLL">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="1" Name="examTypeId" QueryStringField="type"
                    Type="Int32" />
                <asp:ControlParameter ControlID="txtExamName" Name="examName" PropertyName="Text"
                    Type="String" DefaultValue="null" />
                <asp:ControlParameter ControlID="dateStartDateTime" DefaultValue="0001-01-01" Name="beginDateTime"
                    PropertyName="DateValue" Type="DateTime" />
                <asp:ControlParameter ControlID="dateEndDateTime" DefaultValue="0001-01-01" Name="endDateTime"
                    PropertyName="DateValue" Type="DateTime" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
    </form>
</body>
</html>
