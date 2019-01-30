<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ShowSaveExam.aspx.cs" Inherits="RailExamWebApp.Exam.ShowSaveExam" %>

<%@ Register TagPrefix="uc1" TagName="Date" Src="~/Common/Control/Date/Date.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>存档过期和不存档考试管理</title>
    <base target="_self"/>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        //按对象ID获取对象 
        function $F(objId)
        {
            return document.getElementById(objId);              
        }  
    
        //显示或隐藏查询区域
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

        //查询按钮点击事件处理函数
        function searchButton_onClick()
        {
            searchExamCallBack.callback();
        }

        //评卷按钮点击事件处理函数
        function judgePaper(eid,type)
        {       
            if(!eid || !parseInt(eid))
            {
                alert("不正确的数据！");
                
                return;
            }

            var search = window.location.search;
            var orgID= search.substring(search.indexOf("=")+1);
            
            var flagUpdate=document.getElementById("HfUpdateRight").value; 
            var flagIsAdmin = document.getElementById("hfIsAdmin").value;
            
            if(flagIsAdmin == "False" || flagUpdate=="False")
            {
                  alert("您没有该操作的权限！");
                  return;
            }
            
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-900)*.5;   
            ctop=(screen.availHeight-600)*.5;    
            var winGradeEdit = window.open("/RailExamBao/RandomExam/RandomExamResultList.aspx?eid=" + eid +"&OrgID=" +orgID,
                 '', "height=600, width=900,left="+cleft+",top="+ctop+",status=false,resizable=yes", true);
        }
        
        function DeleteData(name,id)
        {
            if(document.getElementById("hfDeleteRight").value =="False")
            {
                  alert("您没有该操作的权限！");
                  return;
            }
            
             if(! confirm("删除考试，将会删除该考试的所有成绩以及答卷等信息，您确定要删除“" +name + "”吗？"))
            {
                return false;
            }
            form1.DeleteID.value = id;
            form1.submit();
            form1.DeleteID.value = "";
        }
       
       function selectExamCategory()
        {
            var selectedExamCategory = window.showModalDialog('../Common/SelectExamCategory.aspx', 
                    '', 'help:no; status:no; dialogWidth:340px;dialogHeight:620px');

            if(! selectedExamCategory)
            {
                return;
            }
            document.getElementById("hfCategoryId").value = selectedExamCategory.split('|')[0];
            document.getElementById("txtCategoryName").value = selectedExamCategory.split('|')[1];
        } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="mainContent">
                    <ComponentArt:CallBack ID="searchExamCallBack" runat="server" Debug="false" PostState="true">
                        <Content>
                            <ComponentArt:Grid ID="examsGrid" runat="server" PageSize="15" DataSourceID="odsExams">
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="RandomExamId">
                                        <Columns>
                                            <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="操作"
                                                Width="40" />
                                            <ComponentArt:GridColumn DataField="RandomExamId" Visible="false" />
                                            <ComponentArt:GridColumn DataField="OrgId" Visible="false" />
                                            <ComponentArt:GridColumn DataField="ExamType" Visible="false" />
                                            <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" Width="160" />
                                            <ComponentArt:GridColumn DataField="ExamStyleName" HeadingText="考试类型" Width="80" />
                                            <ComponentArt:GridColumn DataField="SaveDate" HeadingText="存档时间" Width="120" FormatString="yyyy-MM-dd HH:mm" />
                                            <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="有效时间"
                                                Width="160" />
                                            <ComponentArt:GridColumn DataField="BeginTime" HeadingText="开始时间" FormatString="yyyy-MM-dd HH:mm"
                                                Visible="false" />
                                            <ComponentArt:GridColumn DataField="EndTime" HeadingText="结束时间" FormatString="yyyy-MM-dd HH:mm"
                                                Visible="false" />
                                            <ComponentArt:GridColumn DataField="ExamineeCount" HeadingText="参考人次" Width="50" />
                                            <ComponentArt:GridColumn DataField="ExamAverageScore" HeadingText="平均成绩" Width="50" />
                                            <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="制卷人" Width="50" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="CTEdit">
                                        <a onclick="DeleteData('## DataItem.getMember('ExamName').get_value() ##',## DataItem.getMember('RandomExamId').get_value() ##)"
                                            href="#">
                                            <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a>
                                        <img id="img_##DataItem.getMember('RandomExamId').get_value()##" name="img_##DataItem.getMember('RandomExamId').get_value()##"
                                            alt="查看成绩" style="cursor: hand; border: 0;" onclick='judgePaper("##DataItem.getMember("RandomExamId").get_value()##","##DataItem.getMember("ExamType").get_value()##");'
                                            src="../Common/Image/edit_col_edit.gif" />
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="ClientTemplate3">
                                        ## DataItem.getMember("BeginTime").get_value().getYear()##-##DataItem.getMember("BeginTime").get_value().getMonth()+1##-##DataItem.getMember("BeginTime").get_value().getDate()##
                                        <%--                            ##DataItem.getMember("BeginTime").get_value().getHours()##:##DataItem.getMember("BeginTime").get_value().getMinutes()##--%>
                                        / ## DataItem.getMember("EndTime").get_value().getYear()##-##DataItem.getMember("EndTime").get_value().getMonth()+1##-##DataItem.getMember("EndTime").get_value().getDate()##
                                        <%--                            ##DataItem.getMember("EndTime").get_value().getHours()##:##DataItem.getMember("EndTime").get_value().getMinutes()##
--%>
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                            </ComponentArt:Grid>
                        </Content>
                    </ComponentArt:CallBack>
                </div>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsExams" runat="server" SelectMethod="GetSaveExam" TypeName="RailExam.BLL.RandomExamBLL">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="1" Name="strWhereClause" QueryStringField="orgID"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="hfDeleteRight" runat="server" />
        <asp:HiddenField ID="hfIsAdmin" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <asp:HiddenField ID="hfCategoryId" runat="server" />
        <asp:HiddenField ID="hfWhereCluase" runat="server" />
        <asp:HiddenField ID="hfIsServer" runat="server" />
        <input type="hidden" name="DeleteID" />
    </form>
</body>
</html>
