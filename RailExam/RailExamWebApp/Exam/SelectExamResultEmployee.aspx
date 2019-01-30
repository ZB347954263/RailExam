<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectExamResultEmployee.aspx.cs"
    Inherits="RailExamWebApp.Exam.SelectExamResultEmployee" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>成绩管理 - 考试成绩</title>

    <script type="text/javascript">
        //按对象ID获取对象 
        function $F(objId) 
       {
            return document.getElementById(objId);
       } 
    
       
        
        
       //删除按钮点击事件处理函数 
        function btnDelete_onClick()
        {
            var ids = getSelectedItems();
            if(!ids || ids.length == 0)
            {
                alert("您至少得选择一项！");
                return;
            }
             
            btnsClickCallBack.callback("delete", ids);
             
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
        
        function OutPutPaper(id,orgid)
        {   
            form1.OutPut.value=id;
            form1.OutPutOrgID.value=orgid;
	        form1.submit();
	        form1.OutPut.value = ""; 
	        form1.OutPutOrgID.value = "";
        }
       
       function GetResult(id,orgid)
       {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;   
            
            var winGradeEdit = window.open("ExamResult.aspx?id=" +id + "&orgID="+orgid,
                "ExamResult", "height=600, width=800,left="+cleft+",top="+ctop+",status=false,resizable=yes,scrollbars", true);
            winGradeEdit.focus(); 
       } 
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        成绩查询</div>
                </div>
                <div id="button">
                    <asp:ImageButton runat="server" ID="btnOutPut" ImageUrl="~/Common/Image/OutputExamResult.gif"
                        OnClick="btnOutPut_Click" />&nbsp;
                    <asp:ImageButton runat="server" ID="btnOutPutWord" ImageUrl="~/Common/Image/OutPutPapers.gif"
                        OnClick="btnOutPutWord_Click" />&nbsp;
                    <img alt="查询" onclick="QueryRecord();" src="../Common/Image/find.gif" />
                </div>
            </div>
            <div id="content">
                <div id="query" style="display: none;">
                    &nbsp;&nbsp;单位
                    <asp:TextBox ID="txtOrganizationName" runat="server" Width="80">
                    </asp:TextBox>
                    姓名
                    <asp:TextBox ID="txtExamineeName" runat="server" Width="80"></asp:TextBox>
                    <asp:Label ID="lblWorkNo" runat="server" Text="上岗证号"></asp:Label>
                    <asp:TextBox ID="txtWorkNo" runat="server" Width="80"></asp:TextBox>
                    分数 从
                    <asp:TextBox ID="txtScoreLower" runat="server" Width="80">
                    </asp:TextBox>
                    到
                    <asp:TextBox ID="txtScoreUpper" runat="server" Width="80">
                    </asp:TextBox>
                    <asp:DropDownList ID="ddlStatusId" runat="server" DataSourceID="odsStatuses" DataTextField="StatusName"
                        DataValueField="ExamResultStatusId">
                    </asp:DropDownList>
                    <asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Common/Image/confirm.gif"
                        OnClick="btnSearch_Click" />
                </div>
                <div id="mainContent">
                    <ComponentArt:Grid ID="gradesGrid" runat="server" AllowPaging="true" PageSize="15"
                        Width="100%">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="ExamResultIDStation">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="ExamResultIDStation" HeadingText="编号" Visible="false" />
                                    <ComponentArt:GridColumn DataField="ExamineeName" HeadingText="考生" Width="40" />
                                    <ComponentArt:GridColumn DataField="WorkNo" HeadingText="员工编码" Width="40" />
                                    <ComponentArt:GridColumn DataField="PostName" HeadingText="职名" Width="200" />
                                    <ComponentArt:GridColumn DataField="OrganizationId" Visible="false" />
                                    <ComponentArt:GridColumn DataField="OrganizationName" HeadingText="考生单位" Width="150" />
                                    <ComponentArt:GridColumn DataField="BeginDateTime" HeadingText="开始时间" Width="122"
                                        DataType="System.DateTime" />
                                    <ComponentArt:GridColumn DataField="EndDateTime" HeadingText="结束时间" Width="123" DataType="System.DateTime" />
                                    <ComponentArt:GridColumn DataField="ExamTimeString" HeadingText="答卷时间" Width="60" />
                                    <ComponentArt:GridColumn DataField="Score" HeadingText="成绩" DataType="System.Decimal"
                                        Width="40" />
                                    <ComponentArt:GridColumn AllowSorting="false" HeadingText="操作" DataCellClientTemplateId="EditTemplate"
                                        Width="70" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="EditTemplate">
                                <a onclick="GetResult(##DataItem.getMember('ExamResultIDStation').get_value()##,##DataItem.getMember('OrganizationId').get_value()##)"
                                    href="#"><b>查看</b></a> <a onclick="OutPutPaper(##DataItem.getMember('ExamResultIDStation').get_value()##,##DataItem.getMember('OrganizationId').get_value()##)"
                                        href="#"><b>导出</b></a>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <input type="hidden" name="OutPut" />
        <input type="hidden" name="OutPutOrgID" />
        <asp:HiddenField ID="hfOrganizationId" runat="server" />
        <asp:ObjectDataSource ID="odsStatuses" runat="server" SelectMethod="GetExamResultStatuses"
            TypeName="RailExam.BLL.ExamResultStatusBLL" DataObjectTypeName="RailExam.Model.ExamResultStatus">
            <SelectParameters>
                <asp:Parameter DefaultValue="true" Type="boolean" ConvertEmptyStringToNull="false"
                    Name="bForSearchUse" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
