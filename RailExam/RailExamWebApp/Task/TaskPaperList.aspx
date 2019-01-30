<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TaskPaperList.aspx.cs" Inherits="RailExamWebApp.Task.TaskPaperList" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>作业管理 - 作业列表</title>

    <script src="../Common/JS/JSHelper.js" type="text/javascript">
    </script>

    <script type="text/javascript">
        function selectOrganizationBtnClicked()
        {
            var selectedOrganization = window.showModalDialog('../Common/SelectOrganization.aspx', 
                '', 'help:no; status:no; dialogWidth:300px;dialogHeight:575px');
            
            if(!selectedOrganization)
            {
                return;
            }
            $F("hfOrganizationId").value = selectedOrganization.split('|')[0];
            $F("txtOrganization").value = selectedOrganization.split('|')[1];
        }
        
        function toggleButton_onClick()
        {
            if($F("contentHead").style.display == '')
            {
                $F("contentHead").style.display = 'none';
            }
            else
            {
                $F("contentHead").style.display = '';
            }
        }
        
        function searchBtnClicked()
        {
            if($F("txtPaperName").value)
            {
                $F("hfPaperName").value = $F("txtPaperName").value;
            }
            else
            {
                $F("hfPaperName").value = "null";
            }
            
            if($F("txtCreatePerson").value)
            {
                $F("hfCreatePerson").value = $F("txtPaperName").value;
            }
            else
            {
                $F("hfCreatePerson").value = "null";
            }
            
            searchPaperCallBack.callback();
        }      
        
        function judgePaper(id)
        {
            if(!id || !parseInt(id))
            {
                alert("不正确的数据！");
                
                return;
            }
            
            var winJudge = window.open("TaskResultList.aspx?id=" + id , 
                "JudgePaper", "height:600; width: 800,resizable=no,scrollbars=yes", false);
        }                  
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        作业管理
                    </div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        作业列表
                    </div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img id="toggleButton" alt="" onclick='toggleButton_onClick();' src="../Common/Image/find.gif" />
                </div>
            </div>
            <div id="content">
                <div id="contentHead" style="display: none;">
                    <div id="searchCondition">
                        单位
                        <input id="txtOrganization" maxlength="20" size="10" type="text" />
                        <img id="imgSelectOrganization" name="imgSelectOrganization" onclick='javascript:selectOrganizationBtnClicked();'
                            src="../Common/Image/search.gif" />&nbsp;&nbsp; 作业名

                        <input id="txtPaperName" maxlength="20" size="10" type="text" />&nbsp;&nbsp; 出题人

                        <input id="txtCreatePerson" maxlength="20" size="10" type="text" />&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlTaskCategory" runat="server" DataSourceID="odsTaskCategory"
                            DataTextField="CategoryName" DataValueField="PaperCategoryId">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddlPaperStatus" runat="server" DataSourceID="odsPaperStatus"
                            DataTextField="StatusName" DataValueField="PaperStatusId">
                        </asp:DropDownList>
                        <img id="btnSearch" alt="查询" onclick='javascript:searchBtnClicked();' src="../Common/Image/confirm.gif" />
                    </div>
                </div>
                <div id="mainContent">
                    <table style="width: 100%;">
                        <tr>
                            <td>
                                <ComponentArt:CallBack ID="searchPaperCallBack" runat="server" OnCallback="searchPaperCallBack_Callback"
                                    PostState="true">
                                    <Content>
                                        <ComponentArt:Grid ID="papersGrid" runat="server" AutoCallBackOnColumnReorder="true"
                                            DataSourceID="odsPapers" Debug="true" ManualPaging="true" OnPageIndexChanged="papersGrid_OnPageIndexChanged"
                                            OnSortCommand="papersGrid_OnSortCommand" PageSize="15" RunningMode="callback"
                                            OnDataBinding="papersGrid_OnDataBinding" Width="100%">
                                            <Levels>
                                                <ComponentArt:GridLevel DataKeyField="PaperId">
                                                    <Columns>
                                                        <ComponentArt:GridColumn DataField="PaperId" HeadingText="作业编号" Visible="false" />
                                                        <ComponentArt:GridColumn DataField="OrganizationName" HeadingText="机构" />
                                                        <ComponentArt:GridColumn DataField="TaskTrainTypeName" HeadingText="培训类别" />
                                                        <ComponentArt:GridColumn DataField="CategoryName" HeadingText="分类" />
                                                        <ComponentArt:GridColumn DataField="PaperName" HeadingText="名称" />
                                                        <ComponentArt:GridColumn DataField="ItemCount" HeadingText="题数" />
                                                        <ComponentArt:GridColumn DataField="TotalScore" HeadingText="总分数" />
                                                        <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="出卷人" />
                                                        <ComponentArt:GridColumn DataField="CreateTime" HeadingText="出卷日期" />
                                                        <ComponentArt:GridColumn DataField="StatusName" HeadingText="状态" />
                                                        <ComponentArt:GridColumn Width="80" AllowSorting="false" DataCellClientTemplateId="CTEdit"
                                                            HeadingText="操作" />
                                                    </Columns>
                                                </ComponentArt:GridLevel>
                                            </Levels>
                                            <ClientTemplates>
                                                <ComponentArt:ClientTemplate ID="CTEdit">
                                                    <img id="img_##DataItem.getMember('PaperId').get_value()##" alt="批阅作业" name="img_##DataItem.getMember('PaperId').get_value()##"
                                                        onclick='javascript:judgePaper("##DataItem.getMember("PaperId").get_value()##");'
                                                        src="../Common/Image/edit_col_edit.gif" />
                                                </ComponentArt:ClientTemplate>
                                            </ClientTemplates>
                                        </ComponentArt:Grid>
                                    </Content>
                                </ComponentArt:CallBack>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div>
            <asp:HiddenField ID="hfPaperName" runat="server" Value="null" />
            <asp:HiddenField ID="hfOrganizationId" runat="server" Value="-1" />
            <asp:HiddenField ID="hfCreatePerson" runat="server" Value="null" />
        </div>
        <div>
            <asp:ObjectDataSource ID="odsPaperStatus" runat="server" DataObjectTypeName="RailExam.Model.PaperStatus"
                SelectMethod="GetPaperStatuses" TypeName="RailExam.BLL.PaperStatusBLL">
                <SelectParameters>
                    <asp:Parameter DefaultValue="true" Name="bForSearchUse" Type="boolean" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsTaskCategory" runat="server" DataObjectTypeName="RailExam.Model.PaperCategory"
                SelectMethod="GetTaskCategories" TypeName="RailExam.BLL.PaperCategoryBLL">
                <SelectParameters>
                    <asp:Parameter DefaultValue="false" Name="bForSearchUse" Type="boolean" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsPapers" runat="server" DataObjectTypeName="RailExam.Model.Paper"
                OnObjectCreated="odsPapers_OnObjectCreated" SelectMethod="GetTaskPapers" TypeName="RailExam.BLL.PaperBLL">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfPaperName" DefaultValue="null" Name="paperName"
                        PropertyName="Value" Type="String" />
                    <asp:ControlParameter ControlID="hfOrganizationId" DefaultValue="-1" Name="organizationId"
                        PropertyName="Value" Type="Int32" />
                    <asp:ControlParameter ControlID="ddlTaskCategory" DefaultValue="-1" Name="categoryId"
                        PropertyName="SelectedValue" Type="Int32" />
                    <asp:ControlParameter ControlID="hfCreatePerson" DefaultValue="null" Name="createPerson"
                        PropertyName="Value" Type="String" />
                    <asp:ControlParameter ControlID="ddlPaperStatus" DefaultValue="-1" Name="statusId"
                        PropertyName="SelectedValue" Type="Int32" />
                    <asp:ControlParameter ControlID="papersGrid" ConvertEmptyStringToNull="False" DefaultValue="-1"
                        Name="currentPageIndex" PropertyName="CurrentPageIndex" Type="Int32" />
                    <asp:ControlParameter ControlID="papersGrid" ConvertEmptyStringToNull="False" DefaultValue="-1"
                        Name="pageSize" PropertyName="PageSize" Type="Int32" />
                    <asp:ControlParameter ControlID="papersGrid" ConvertEmptyStringToNull="False" DefaultValue="-1"
                        Name="orderBy" PropertyName="Sort" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
