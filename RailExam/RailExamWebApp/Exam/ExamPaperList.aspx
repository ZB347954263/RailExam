<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ExamPaperList.aspx.cs"
    Inherits="RailExamWebApp.Exam.ExamPaperList" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�ֹ����� - �����б�</title>

    <script src="../Common/JS/JSHelper.js" type="text/javascript"></script>

    <script type="text/javascript">
 
        
       //��ʾ�����ز�ѯ���� 
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
        
      
        
       //����ť����¼�������
        function judgePaper(id)
        {
          var flagupdate=document.getElementById("HfUpdateRight").value;
        	                 if(flagupdate=="False")
                      {
                        alert("��û��Ȩ��ʹ�øò�����");                       
                        return;
                      }
                      
                      
            if(!id || !parseInt(id))
            {
                alert("����ȷ�����ݣ�");
                
                return;
            }
            
              var   cleft;   
              var   ctop;   
            cleft=(screen.availWidth-900)*.5;   
            ctop=(screen.availHeight-700)*.5;  
            
          
            var winJudge = window.open("ExamJudge.aspx?id=" + id , "ExamJudge", "Width=900px,Height=700px,left="+cleft+",top="+ctop+",scrollbars=yes,resizable=yes", true);          
		  
            winJudge.focus();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        ���Թ���</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        �ֹ�����</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img alt="" onclick="QueryRecord();" src="../Common/Image/find.gif" />
                </div>
            </div>
            <div id="content">
                <div   style="text-align:left">
                <span >�Ծ����</span>
                <asp:Label ID="TextBoxExamCategory" runat="server"  />&nbsp;
                <span>�Ծ����ƣ�</span>
                <asp:Label ID="TextBoxExamName" runat="server"   />&nbsp;
                <span>����ʱ�䣺</span>
                <asp:Label ID="TextBoxExamTime" runat="server"   />
                </div>
                <div id="query" style="display: none;">
                    <span>��λ</span>
                    <asp:TextBox ID="txtOrganizationName" runat="server" Width="10%" />
                    <span>����</span>
                    <asp:TextBox ID="txtUserName" runat="server" Width="10%" />
                    <span>�÷ִ�</span>
                    <asp:TextBox ID="txtScoreLower" runat="server" Width="10%" />
                    <span>��</span>
                    <asp:TextBox ID="txtScoreUpper" runat="server" Width="10%" />
                    <asp:DropDownList ID="ddlPaperStatus" runat="server" DataSourceID="odsExamResultStatus"
                        DataTextField="StatusName" DataValueField="ExamResultStatusId">
                    </asp:DropDownList>
                    
                    <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" 
                        ImageUrl="../Common/Image/confirm.gif" OnClick="btnCancel_Click" />
                        
              
                </div>
                <div id="grid">                    
                            <ComponentArt:Grid ID="papersGrid" AutoAdjustPageSize="false" runat="server" PageSize="19"
                                DataSourceID="odsPapers">
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="ExamResultId">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="ExamineeName" HeadingText="��������" />
                                            <ComponentArt:GridColumn DataField="OrganizationName" HeadingText="������λ" />
                                            <ComponentArt:GridColumn DataField="WorkNo" HeadingText="Ա������" />
                                            <ComponentArt:GridColumn DataField="PostName" HeadingText="ְ��" />
                                            <ComponentArt:GridColumn DataField="ExamResultId" Visible="false" />
                                            <ComponentArt:GridColumn DataField="Score" HeadingText="�ɼ�" />
                                            <ComponentArt:GridColumn DataField="ExamTimeString" HeadingText="����ʱ��" />
                                            <ComponentArt:GridColumn DataField="BeginDateTime" HeadingText="��ʼʱ��" />
                                            <ComponentArt:GridColumn DataField="EndDateTime" HeadingText="����ʱ��" />
                                            <ComponentArt:GridColumn DataField="JudgeName" HeadingText="������" />
                                            <ComponentArt:GridColumn DataField="StatusName" HeadingText="״̬" />
                                            <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="����" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="CTEdit">
                                        <img id="img_##DataItem.getMember('ExamResultId').get_value()##" alt="����" 
                                            name="img_##DataItem.getMember('ExamResultId').get_value()##" onclick='javascript:judgePaper("##DataItem.getMember("ExamResultId").get_value()##");'
                                            src="../Common/Image/edit_col_edit.gif" style="cursor: hand; border: 0;" />
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                            </ComponentArt:Grid>
                       
                </div>
            </div>
        </div>
        <%--<asp:HiddenField ID="hfExamId" runat="server" />
        <asp:HiddenField ID="hfOrganizationId" runat="server" />--%>
        <asp:HiddenField ID="hfOrganizationName" runat="server" />
        <asp:HiddenField ID="hfExamineeName" runat="server" />
        <asp:HiddenField ID="hfScoreLower" runat="server" />
        <asp:HiddenField ID="hfScoreUpper" runat="server" />
        <asp:HiddenField ID="hfJudgeId" runat="server" />
        <%--<asp:HiddenField ID="hfIsPass" runat="server" />--%>
        <asp:ObjectDataSource ID="odsExamResultStatus" runat="server" SelectMethod="GetExamResultStatuses"
            TypeName="RailExam.BLL.ExamResultStatusBLL">
            <SelectParameters>
                <asp:Parameter Name="bForSearchUse" DefaultValue="true" Type="Boolean" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsPapers" runat="server" SelectMethod="GetExamResults"
            TypeName="RailExam.BLL.ExamResultBLL">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfExamID" DefaultValue="null" Name="examId"
                    PropertyName="Value" Type="Int32" />
                <asp:ControlParameter ControlID="hfOrganizationName" DefaultValue="null" Name="organizationName"
                    PropertyName="Value" Type="String" />
                <asp:ControlParameter ControlID="hfExamineeName" DefaultValue="null" Name="examineeName"
                    PropertyName="Value" Type="String" />
                <asp:ControlParameter ControlID="hfScoreLower" DefaultValue="-1" Name="scoreLower"
                    PropertyName="Value" Type="Decimal" />
                <asp:ControlParameter ControlID="hfScoreUpper" DefaultValue="-1" Name="scoreUpper"
                    PropertyName="Value" Type="Decimal" />
                <asp:ControlParameter ControlID="ddlPaperStatus" DefaultValue="-1" Name="statusId"
                    PropertyName="SelectedValue" Type="Int32" />
                <%--<asp:ControlParameter ControlID="hfJudgeId" DefaultValue="-1" Name="judgeId" PropertyName="Value"
                    Type="Int32" />--%>
                <asp:ControlParameter ControlID="papersGrid" DefaultValue="-1" Name="currentPageIndex"
                    PropertyName="CurrentPageIndex" Type="Int32" />
                <asp:ControlParameter ControlID="papersGrid" DefaultValue="-1" Name="pageSize" PropertyName="PageSize"
                    Type="Int32" />
                <asp:ControlParameter ControlID="papersGrid" DefaultValue="null" Name="orderBy" PropertyName="Sort"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
       <asp:HiddenField ID="hfExamID" runat ="server" />
       <input type="hidden" name="Refresh" />
    </form>
</body>
</html>
