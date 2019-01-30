<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ExamGradeList.aspx.cs"
    Inherits="RailExamWebApp.Exam.ExamGradeList" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>���Գɼ� - �����б�</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        //������ID��ȡ���� 
        function $F(objId)
        {
            return document.getElementById(objId);              
        }  
    
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

        //��ѯ��ť����¼�������
        function searchButton_onClick()
        {
            searchExamCallBack.callback();
        }

        //����ť����¼�������
        function judgePaper(eid)
        {
        
         var flagupdate=document.getElementById("HfUpdateRight").value;
        	                 if(flagupdate=="False")
                      {
                        alert("��û��Ȩ��ʹ�øò�����");                       
                        return;
                      }
                      
            if(!eid || !parseInt(eid))
            {
                alert("����ȷ�����ݣ�");
                
                return;
            }

            var search = window.location.search;
            var type = search.substring(search.indexOf("type") + 5);
            if(!type || !parseInt(type))
            {
                alert("����ȷ�����ݣ�");
                
                return;
            }
            
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-900)*.5;   
            ctop=(screen.availHeight-600)*.5;   
            
            var winGradeEdit = window.open("ExamGradeEdit.aspx?type=" + type + "&eid=" + eid ,
                "ExamGradeEdit", "height=600, width=900,left="+cleft+",top="+ctop+",status=false,resizable=no", true);
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
                        ���Թ���</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        ���Գɼ�</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img alt="" onclick="QueryRecord();" src="../Common/Image/find.gif" />
                </div>
            </div>
            <div id="content">
                <div id="query" style="display: none;">
                    &nbsp;&nbsp;��������
                    <asp:TextBox ID="txtExamName" runat="server"></asp:TextBox>
                    ��Чʱ�� ��
                    <uc1:Date ID="dateStartDateTime" runat="server" />
                    ��
                    <uc1:Date ID="dateEndDateTime" runat="server" />
                    <input id="searchButton" type="button" class="buttonSearch" title="��ѯ���������Ŀ���" value="ȷ  ��"
                        onclick="searchButton_onClick();" />
                </div>
                <div id="mainContent">
                    <ComponentArt:CallBack ID="searchExamCallBack" runat="server" Debug="false" PostState="true"
                        OnCallback="searchExamCallBack_Callback">
                        <Content>
                            <ComponentArt:Grid ID="examsGrid" runat="server" AutoAdjustPageSize="false" PageSize="19"
                                DataSourceID="odsExams">
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="ExamId">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="ExamId" Visible="false" />
                                            <ComponentArt:GridColumn DataField="ExamName" HeadingText="��������" />
                                            <ComponentArt:GridColumn DataField="CreateModeString" HeadingText="���ⷽʽ" />
                                            <ComponentArt:GridColumn DataField="ValidExamTimeDurationString" HeadingText="��Чʱ��" />
                                            <ComponentArt:GridColumn DataField="ExamineeCount" HeadingText="�ο��˴�" />
                                            <ComponentArt:GridColumn DataField="ExamAverageScore" HeadingText="ƽ���ɼ�" />
                                            <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="�ƾ���" />
                                            <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="����" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="CTEdit">
                                        <img id="img_##DataItem.getMember('ExamId').get_value()##" name="img_##DataItem.getMember('ExamId').get_value()##"
                                            alt="�޸ĳɼ�" style="cursor: hand; border: 0;" onclick='javascript:judgePaper("##DataItem.getMember("ExamId").get_value()##");'
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
