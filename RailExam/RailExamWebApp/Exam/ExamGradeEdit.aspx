<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ExamGradeEdit.aspx.cs"
    Inherits="RailExamWebApp.Exam.ExamGradeEdit" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>成绩管理 - 考试成绩</title>

    <script type="text/javascript">
        //按对象ID获取对象 
        function $F(objId) 
       {
            return document.getElementById(objId);
       } 
    
        function gradesGrid_onItemUpdate(s, e)
        {
        }
        
        //选择所有
        function btnSelectAllClicked()
        {
            if(gradesGrid.get_table().getRowCount() == 0)
            {
                alert("无试题可选！");
                return;
            }
            
            var theItem;
            var start = (gradesGrid.GetProperty("CurrentPageIndex")) * gradesGrid.get_pageSize();
            var end = (gradesGrid.GetProperty("CurrentPageIndex")+1) * gradesGrid.get_pageSize();
            if(end > gradesGrid.get_table().getRowCount())
            {
                end = gradesGrid.get_table().getRowCount();
            }
            for(var i=start; i<end; i++)
            {
                theItem = gradesGrid.getItemFromClientId(gradesGrid.get_table().getRow(i).get_clientId());
                $F("cbxSelectItem_" + theItem.GetProperty("Id")).checked = true;
            }
        }

        //反选择所有
        function btnUnselectAllClicked()
        {
            if(getSelectedItems() == "")
            {
                alert("无试题可反选！");
                return;
            }
            
            var theItem;
            var start = (gradesGrid.GetProperty("CurrentPageIndex")) * gradesGrid.get_pageSize();
            var end = (gradesGrid.GetProperty("CurrentPageIndex")+1) * gradesGrid.get_pageSize();
            if(end > gradesGrid.get_table().getRowCount())
            {
                end = gradesGrid.get_table().getRowCount();
            }
            for(var i=start; i<end; i++)
            {
                theItem = gradesGrid.getItemFromClientId(gradesGrid.get_table().getRow(i).get_clientId());
                $F("cbxSelectItem_" + theItem.GetProperty("Id")).checked = false;
            }
        }
        
        //获取选择项
        function getSelectedItems()
        {
            var ids = "";
            var theItem;
            var start = (gradesGrid.GetProperty("CurrentPageIndex")) * gradesGrid.get_pageSize();
            var end = (gradesGrid.GetProperty("CurrentPageIndex")+1) * gradesGrid.get_pageSize();
            if(end > gradesGrid.get_table().getRowCount())
            {
                end = gradesGrid.get_table().getRowCount();
            }
            for(var i=start; i<end; i++)
            {
                theItem = gradesGrid.getItemFromClientId(gradesGrid.get_table().getRow(i).get_clientId());
                if($F("cbxSelectItem_" + theItem.GetProperty("Id")).checked)
                {
                    ids += theItem.GetProperty("Id") + "|";
                }
            }
            
            if (ids.lastIndexOf('|') == ids.length - 1)
            {
                ids = ids.substring(0, ids.length - 1);
            }
            
            return ids;
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
        
       //查询按钮点击事件处理函数 
        function searchButton_onClick()
        {
            searchExamCallBack.callback();
        }
        
       //编辑按钮点击事件处理函数 
        function editItem(rowId)
        {
            gradesGrid.edit(gradesGrid.getItemFromClientId(rowId)); 
        }

       //客户端编辑按钮点击事件处理函数 
        function onUpdateItem(item)
        {
            if (confirm("您确定要更改此记录吗?"))
                return true; 
            else
                return false; 
        }

       //客户端更新按钮点击事件处理函数 
        function updateItem()
        {
            gradesGrid.editComplete();     
        }

       //客户端插入按钮点击事件处理函数 
        function onInsert(item)
        {
            if (confirm("您确定要插入此记录吗?"))
                return true; 
            else
                return false; 
        }
        
       //客户端插入按钮点击事件处理函数 
        function insertItem()
        {
            gradesGrid.editComplete(); 
        }

       //客户端删除按钮点击事件处理函数 
        function onDeleteItem(item)
        {
            if (confirm("您确定要删除此记录吗?"))
                return true; 
            else
                return false; 
        }

       //客户端删除按钮点击事件处理函数 
        function deleteItem(rowId)
        {
            gradesGrid.deleteItem(gradesGrid.getItemFromClientId(rowId)); 
        }

       //回调完成处理函数 
        function btnsClickCallBack_onCallbackComplete(s, e)
        {
//            var res = $F("hfBtnsClickCallBackResult").value;
//            if(!res || parseInt(res) == 0)
//            {
//                alert("删除失败！");
//            }
//            else
//            {
//                alert("成功删除！");
//            }
        }
        
       //回调错误处理函数 
        function btnsClickCallBack_onCallbackError(s, e)
        {
            alert("系统出错，稍后重试！");
            
            return true;
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
                        修改成绩</div>
                </div>
                <div id="button">
                    <img alt="查询" onclick="QueryRecord();" src="../Common/Image/find.gif" />
                    <img alt="删除答卷" onclick='javascript:btnDelete_onClick();' src="../Common/Image/deletepaper.gif" />
                </div>
            </div>
            <div id="content">
            <div  style="text-align:left">
                <span >试卷类别：</span>
                <asp:Label ID="TextBoxExamCategory" runat="server"  />&nbsp;
                <span>试卷名称：</span>
                <asp:Label ID="TextBoxExamName" runat="server"   />&nbsp;
                <span>考试时间：</span>
                <asp:Label ID="TextBoxExamTime" runat="server"   />
                </div>
                <div id="query" style="display: none;">
                    &nbsp;&nbsp;单位
                    <asp:TextBox ID="txtOrganizationName" runat="server" Width="80">
                    </asp:TextBox>
                    姓名
                    <asp:TextBox ID="txtExamineeName" runat="server" Width="80">
                    </asp:TextBox>
                    分数 从
                    <asp:TextBox ID="txtScoreLower" runat="server" Width="80">
                    </asp:TextBox>
                    到
                    <asp:TextBox ID="txtScoreUpper" runat="server" Width="80">
                    </asp:TextBox>
                    <asp:DropDownList ID="ddlStatusId" runat="server" DataSourceID="odsStatuses" DataTextField="StatusName"
                        DataValueField="ExamResultStatusId">
                    </asp:DropDownList>
                    <input id="searchButton" type="button" class="buttonSearch" title="查询符合条件的考卷" value="确  定"
                        onclick="searchButton_onClick();" />
                </div>
                <div id="mainContent">
                    <ComponentArt:CallBack ID="searchExamCallBack" runat="server" PostState="true" Debug="false"
                        OnCallback="searchExamCallBack_Callback" Width="825">
                        <Content>
                            <ComponentArt:Grid ID="gradesGrid" runat="server" AutoAdjustPageSize="false" ClientSideOnDelete="onDeleteItem"
                                ClientSideOnUpdate="onUpdateItem" Debug="false" EditOnClickSelectedItem="false"
                                LoadingPanelClientTemplateId="LoadingFeedbackTemplate" LoadingPanelPosition="MiddleCenter"
                                OnUpdateCommand="gradesGrid_UpdateCommand" OnDeleteCommand="gradesGrid_DeleteCommand"
                                PageSize="15" RunningMode="Callback" Width="825" AutoCallBackOnUpdate="true"
                                AutoCallBackOnDelete="true">
                                <ClientEvents>
                                    <ItemUpdate EventHandler="gradesGrid_onItemUpdate" />
                                </ClientEvents>
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="ExamResultId" EditCommandClientTemplateId="EditCommandTemplate">
                                        <Columns>
                                            <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="ctSelect"
                                                HeadingText="&lt;a href='#' onclick='btnSelectAllClicked()' &gt;全选&lt;/a&gt; &lt;a href='#' onclick='btnUnselectAllClicked()' &gt;反选&lt;/a&gt;" />
                                            <ComponentArt:GridColumn DataField="ExamResultId" HeadingText="编号" Visible="false" />
                                            <ComponentArt:GridColumn DataField="ExamineeName" HeadingText="考生" Width="40"/>
                                            <ComponentArt:GridColumn DataField="WorkNo" HeadingText="员工编码" Width="40"/>
                                            <ComponentArt:GridColumn DataField="PostName" HeadingText="职名" Width="90"/>
                                            <ComponentArt:GridColumn DataField="OrganizationName" HeadingText="考生单位" Width="70"/>
                                            <ComponentArt:GridColumn DataField="BeginDateTime" HeadingText="开始时间"     AllowEditing="True" Width="122" DataType="System.DateTime" />
                                            <ComponentArt:GridColumn DataField="EndDateTime" HeadingText="结束时间"     AllowEditing="True" Width="123"  DataType="System.DateTime" />
                                            <ComponentArt:GridColumn DataField="ExamTimeString" HeadingText="答卷时间" />
                                            <ComponentArt:GridColumn DataField="Score" HeadingText="成绩" AllowEditing="True" DataType="System.Decimal" Width="40"/>
                                            <ComponentArt:GridColumn DataField="StatusName" HeadingText="状态" AllowEditing="True" Width="70"
                                                ForeignDataKeyField="ExamResultStatusId" ForeignDisplayField="StatusName" ForeignTable="ExamResultStatus" />
                                            <ComponentArt:GridColumn DataField="JudgeName" HeadingText="评卷人" />
                                            <ComponentArt:GridColumn AllowSorting="false" HeadingText="操作" DataCellClientTemplateId="EditTemplate"
                                                EditControlType="EditCommand" Width="30" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="ctSelect">
                                        <input id="cbxSelectItem_##DataItem.getMember('ExamResultId').get_value()##" name="cbxSelectItem_##DataItem.getMember('ExamResultId').get_value()##"
                                            type="checkbox" value="##DataItem.getMember('ExamResultId').get_value()##" />
                                    </ComponentArt:ClientTemplate>
                                    <ComponentArt:ClientTemplate ID="EditTemplate">
                                        <a href="javascript:editItem('## DataItem.ClientId ##');">
                                            <img border="0" width="13" height="13" src="../Common/Image/edit_col_edit.gif" /></a>
                                        <a href="javascript:deleteItem('## DataItem.ClientId ##')">
                                            <img border="0" width="13" height="13" src="../Common/Image/edit_col_delete.gif" /></a>
                                    </ComponentArt:ClientTemplate>
                                    <ComponentArt:ClientTemplate ID="EditCommandTemplate">
                                        <a href="javascript:updateItem();">
                                            <img border="0" width="13" height="13" src="../Common/Image/edit_col_save.gif" /></a>
                                        <a href="javascript:gradesGrid.EditCancel();">
                                            <img border="0" width="13" height="13" src="../Common/Image/edit_col_cancel.gif" /></a>
                                    </ComponentArt:ClientTemplate>
                                    <%--<ComponentArt:ClientTemplate ID="InsertCommandTemplate">
                                        <a href="javascript:insertItem();">插入</a> | <a href="javascript:gradesGrid.EditCancel();">
                                            取消</a>
                                    </ComponentArt:ClientTemplate>--%>
                                </ClientTemplates>
                            </ComponentArt:Grid>
                        </Content>
                    </ComponentArt:CallBack>
                </div>
            </div>
        </div>
        <ComponentArt:CallBack ID="btnsClickCallBack" runat="server" PostState="true" OnCallback="btnsClickCallBack_Callback">
            <Content>
                <asp:HiddenField ID="hfBtnsClickCallBackResult" runat="server" />
            </Content>
            <ClientEvents>
                <CallbackComplete EventHandler="btnsClickCallBack_onCallbackComplete" />
                <CallbackError EventHandler="btnsClickCallBack_onCallbackError" />
            </ClientEvents>
        </ComponentArt:CallBack>
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
