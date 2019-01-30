<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ExamList.aspx.cs" Inherits="RailExamWebApp.Online.ExamList" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
       <script type="text/JavaScript">
         function AttendExam(ExamId,PaperId)      
        {                  
            var w=window.open("../exam/ExamKS.aspx?ExamId="+ExamId+"&PaperId="+PaperId,"ExamKS","fullscreen=yes,toolbar=no,scrollbars=yes");	
            w.focus();	
        }   
        
          function btnViewExamResult(examResultId)
        {
            var re= window.open("Exam/ExamResult.aspx?id=" + examResultId,
                "ExamResult"," Width=800; Height=600,status=false,resizable=yes,scrollbars",true);
            re.focus();
        }
        
         </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        在线考试</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        考试信息</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">                         
                     <asp:ImageButton runat="server" ID="ImageButton0"  ImageUrl="~/Online/Image/currentexam01.gif" OnClick="ImageButton0_Click" />
                     <asp:ImageButton runat="server" ID="ImageButton1"  ImageUrl="~/Online/Image/examcoming.gif" OnClick="ImageButton1_Click" />
                     <asp:ImageButton runat="server" ID="ImageButton2"  ImageUrl="~/Online/Image/examhis.gif" OnClick="ImageButton2_Click" />
                     <asp:ImageButton runat="server" ID="ImageButton3"  ImageUrl="~/Online/Image/examresult.gif" OnClick="ImageButton3_Click" /> 
                </div>
            </div>
            <div>&nbsp;
            </div>
            <div   >
              <asp:Label runat="server" ID="labelTitle" Font-Bold="true"  Font-Size="Larger" Text="当前考试"></asp:Label>
            </div>
            <div id="content">
             <ComponentArt:Grid ID="Grid1" runat="server"   AllowPaging="true"  PageSize="20"  Width="98%">                   
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="ExamId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="ExamId" HeadingText="编号" Visible="false" />
                                 <ComponentArt:GridColumn DataField="paperId" HeadingText="编号" Visible="false" />
                                 <ComponentArt:GridColumn DataField="EndTime" HeadingText="编号" Visible="false" />
                                <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" />                                                                
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="有效时间" />
                                 <ComponentArt:GridColumn DataField="ConvertTotalScore" HeadingText="总分数" />
                                <ComponentArt:GridColumn DataField="BeginTime" HeadingText="答题时间" FormatString="yyyy-MM-dd HH:mm"                                    />                                  
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate1" HeadingText="操作"  />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>             
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate1">
                            <a onclick="AttendExam('## DataItem.getMember('ExamId').get_value() ##' ,'## DataItem.getMember('paperId').get_value() ##')"
                                href="#"><b>参加考试</b></a>
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
                
                <ComponentArt:Grid ID="Grid2" runat="server"  AllowPaging="true"  PageSize="20"  Width="98%">                   
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="ExamId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="ExamId" HeadingText="编号" Visible="false" />
                                 <ComponentArt:GridColumn DataField="paperId" HeadingText="编号" Visible="false" />
                                 <ComponentArt:GridColumn DataField="EndTime" HeadingText="编号" Visible="false" />
                                <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" />                                                                
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate4" HeadingText="有效时间" />
                                 <ComponentArt:GridColumn DataField="ConvertTotalScore" HeadingText="总分数" />
                                <ComponentArt:GridColumn DataField="BeginTime" HeadingText="答题时间" FormatString="yyyy-MM-dd HH:mm"                                    />                                  
                                
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>             
                 
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate4">
                          ## DataItem.getMember("BeginTime").get_value().getYear()##-##DataItem.getMember("BeginTime").get_value().getMonth()+1##-##DataItem.getMember("BeginTime").get_value().getDate()##
                            ##DataItem.getMember("BeginTime").get_value().getHours()##:##DataItem.getMember("BeginTime").get_value().getMinutes()##
                            / ## DataItem.getMember("EndTime").get_value().getYear()##-##DataItem.getMember("EndTime").get_value().getMonth()+1##-##DataItem.getMember("EndTime").get_value().getDate()##
                            ##DataItem.getMember("EndTime").get_value().getHours()##:##DataItem.getMember("EndTime").get_value().getMinutes()##
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>                   
                </ComponentArt:Grid>    
                
                    <ComponentArt:Grid ID="Grid3" runat="server"  AllowPaging="true"  PageSize="20" Width="98%">                   
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="ExamId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="ExamId" HeadingText="编号" Visible="false" />
                                 <ComponentArt:GridColumn DataField="paperId" HeadingText="编号" Visible="false" />
                                 <ComponentArt:GridColumn DataField="EndTime" HeadingText="编号" Visible="false" />
                                <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" />                                                                
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate5" HeadingText="有效时间" />
                                 <ComponentArt:GridColumn DataField="ConvertTotalScore" HeadingText="总分数" />
                                <ComponentArt:GridColumn DataField="BeginTime" HeadingText="答题时间" FormatString="yyyy-MM-dd HH:mm"                                    />                                  
                                
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>             
                 
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate5">
                          ## DataItem.getMember("BeginTime").get_value().getYear()##-##DataItem.getMember("BeginTime").get_value().getMonth()+1##-##DataItem.getMember("BeginTime").get_value().getDate()##
                            ##DataItem.getMember("BeginTime").get_value().getHours()##:##DataItem.getMember("BeginTime").get_value().getMinutes()##
                            / ## DataItem.getMember("EndTime").get_value().getYear()##-##DataItem.getMember("EndTime").get_value().getMonth()+1##-##DataItem.getMember("EndTime").get_value().getDate()##
                            ##DataItem.getMember("EndTime").get_value().getHours()##:##DataItem.getMember("EndTime").get_value().getMinutes()##
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>                   
                </ComponentArt:Grid>   
                
                
                <ComponentArt:Grid ID="Grid4" runat="server"  AllowPaging="true"  PageSize="20" Width="98%">                   
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="ExamResultId">
                            <Columns>
                                
                                <ComponentArt:GridColumn DataField="ExamResultId" HeadingText="编号" Visible="false" />
                                
                                 <ComponentArt:GridColumn DataField="ExamEndTime" HeadingText="编号" Visible="false" />
                                <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" />                                                                
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate6" HeadingText="考试时间" />
                                 <ComponentArt:GridColumn DataField="Score" HeadingText="分数" />
                                 <ComponentArt:GridColumn DataField="CorrectRate" HeadingText="正确率(%)" />                                 
                                <ComponentArt:GridColumn DataField="ExamBeginTime" HeadingText="答题时间" FormatString="yyyy-MM-dd HH:mm"  Visible="false"/>   
                                 <ComponentArt:GridColumn DataField="IsPass" HeadingText="是否通过考试" Visible="false" />
                                 <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate9" HeadingText="是否通过考试" />                                                                             
                                 <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate8" HeadingText="操作"  />                                 
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>           
                     <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate9">
                            ## DataItem.getMember("IsPass").get_value() == 1 ? "是" : "否" ##
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>                      
                 <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate8">
                            <a onclick="btnViewExamResult('## DataItem.getMember('ExamResultId').get_value() ##')"
                                href="#"><b>查看试卷</b></a>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate6">
                           ## DataItem.getMember("BeginTime").get_value().getYear()##-##DataItem.getMember("BeginTime").get_value().getMonth()+1##-##DataItem.getMember("BeginTime").get_value().getDate()##
                            ##DataItem.getMember("BeginTime").get_value().getHours()##:##DataItem.getMember("BeginTime").get_value().getMinutes()##
                            / ## DataItem.getMember("EndTime").get_value().getYear()##-##DataItem.getMember("EndTime").get_value().getMonth()+1##-##DataItem.getMember("EndTime").get_value().getDate()##
                            ##DataItem.getMember("EndTime").get_value().getHours()##:##DataItem.getMember("EndTime").get_value().getMinutes()##
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>                   
                </ComponentArt:Grid>                    
                                            
                
            </div>
          
        </div>
    </form>
</body>
</html>
