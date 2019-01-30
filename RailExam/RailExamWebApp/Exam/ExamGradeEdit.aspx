<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ExamGradeEdit.aspx.cs"
    Inherits="RailExamWebApp.Exam.ExamGradeEdit" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�ɼ����� - ���Գɼ�</title>

    <script type="text/javascript">
        //������ID��ȡ���� 
        function $F(objId) 
       {
            return document.getElementById(objId);
       } 
    
        function gradesGrid_onItemUpdate(s, e)
        {
        }
        
        //ѡ������
        function btnSelectAllClicked()
        {
            if(gradesGrid.get_table().getRowCount() == 0)
            {
                alert("�������ѡ��");
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

        //��ѡ������
        function btnUnselectAllClicked()
        {
            if(getSelectedItems() == "")
            {
                alert("������ɷ�ѡ��");
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
        
        //��ȡѡ����
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
        
       //ɾ����ť����¼������� 
        function btnDelete_onClick()
        {
            var ids = getSelectedItems();
            if(!ids || ids.length == 0)
            {
                alert("�����ٵ�ѡ��һ�");
                return;
            }
             
            btnsClickCallBack.callback("delete", ids);
             
        }
        
       //��ѯ��ť����¼������� 
        function searchButton_onClick()
        {
            searchExamCallBack.callback();
        }
        
       //�༭��ť����¼������� 
        function editItem(rowId)
        {
            gradesGrid.edit(gradesGrid.getItemFromClientId(rowId)); 
        }

       //�ͻ��˱༭��ť����¼������� 
        function onUpdateItem(item)
        {
            if (confirm("��ȷ��Ҫ���Ĵ˼�¼��?"))
                return true; 
            else
                return false; 
        }

       //�ͻ��˸��°�ť����¼������� 
        function updateItem()
        {
            gradesGrid.editComplete();     
        }

       //�ͻ��˲��밴ť����¼������� 
        function onInsert(item)
        {
            if (confirm("��ȷ��Ҫ����˼�¼��?"))
                return true; 
            else
                return false; 
        }
        
       //�ͻ��˲��밴ť����¼������� 
        function insertItem()
        {
            gradesGrid.editComplete(); 
        }

       //�ͻ���ɾ����ť����¼������� 
        function onDeleteItem(item)
        {
            if (confirm("��ȷ��Ҫɾ���˼�¼��?"))
                return true; 
            else
                return false; 
        }

       //�ͻ���ɾ����ť����¼������� 
        function deleteItem(rowId)
        {
            gradesGrid.deleteItem(gradesGrid.getItemFromClientId(rowId)); 
        }

       //�ص���ɴ����� 
        function btnsClickCallBack_onCallbackComplete(s, e)
        {
//            var res = $F("hfBtnsClickCallBackResult").value;
//            if(!res || parseInt(res) == 0)
//            {
//                alert("ɾ��ʧ�ܣ�");
//            }
//            else
//            {
//                alert("�ɹ�ɾ����");
//            }
        }
        
       //�ص��������� 
        function btnsClickCallBack_onCallbackError(s, e)
        {
            alert("ϵͳ�����Ժ����ԣ�");
            
            return true;
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
                        �޸ĳɼ�</div>
                </div>
                <div id="button">
                    <img alt="��ѯ" onclick="QueryRecord();" src="../Common/Image/find.gif" />
                    <img alt="ɾ�����" onclick='javascript:btnDelete_onClick();' src="../Common/Image/deletepaper.gif" />
                </div>
            </div>
            <div id="content">
            <div  style="text-align:left">
                <span >�Ծ����</span>
                <asp:Label ID="TextBoxExamCategory" runat="server"  />&nbsp;
                <span>�Ծ����ƣ�</span>
                <asp:Label ID="TextBoxExamName" runat="server"   />&nbsp;
                <span>����ʱ�䣺</span>
                <asp:Label ID="TextBoxExamTime" runat="server"   />
                </div>
                <div id="query" style="display: none;">
                    &nbsp;&nbsp;��λ
                    <asp:TextBox ID="txtOrganizationName" runat="server" Width="80">
                    </asp:TextBox>
                    ����
                    <asp:TextBox ID="txtExamineeName" runat="server" Width="80">
                    </asp:TextBox>
                    ���� ��
                    <asp:TextBox ID="txtScoreLower" runat="server" Width="80">
                    </asp:TextBox>
                    ��
                    <asp:TextBox ID="txtScoreUpper" runat="server" Width="80">
                    </asp:TextBox>
                    <asp:DropDownList ID="ddlStatusId" runat="server" DataSourceID="odsStatuses" DataTextField="StatusName"
                        DataValueField="ExamResultStatusId">
                    </asp:DropDownList>
                    <input id="searchButton" type="button" class="buttonSearch" title="��ѯ���������Ŀ���" value="ȷ  ��"
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
                                                HeadingText="&lt;a href='#' onclick='btnSelectAllClicked()' &gt;ȫѡ&lt;/a&gt; &lt;a href='#' onclick='btnUnselectAllClicked()' &gt;��ѡ&lt;/a&gt;" />
                                            <ComponentArt:GridColumn DataField="ExamResultId" HeadingText="���" Visible="false" />
                                            <ComponentArt:GridColumn DataField="ExamineeName" HeadingText="����" Width="40"/>
                                            <ComponentArt:GridColumn DataField="WorkNo" HeadingText="Ա������" Width="40"/>
                                            <ComponentArt:GridColumn DataField="PostName" HeadingText="ְ��" Width="90"/>
                                            <ComponentArt:GridColumn DataField="OrganizationName" HeadingText="������λ" Width="70"/>
                                            <ComponentArt:GridColumn DataField="BeginDateTime" HeadingText="��ʼʱ��"     AllowEditing="True" Width="122" DataType="System.DateTime" />
                                            <ComponentArt:GridColumn DataField="EndDateTime" HeadingText="����ʱ��"     AllowEditing="True" Width="123"  DataType="System.DateTime" />
                                            <ComponentArt:GridColumn DataField="ExamTimeString" HeadingText="���ʱ��" />
                                            <ComponentArt:GridColumn DataField="Score" HeadingText="�ɼ�" AllowEditing="True" DataType="System.Decimal" Width="40"/>
                                            <ComponentArt:GridColumn DataField="StatusName" HeadingText="״̬" AllowEditing="True" Width="70"
                                                ForeignDataKeyField="ExamResultStatusId" ForeignDisplayField="StatusName" ForeignTable="ExamResultStatus" />
                                            <ComponentArt:GridColumn DataField="JudgeName" HeadingText="������" />
                                            <ComponentArt:GridColumn AllowSorting="false" HeadingText="����" DataCellClientTemplateId="EditTemplate"
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
                                        <a href="javascript:insertItem();">����</a> | <a href="javascript:gradesGrid.EditCancel();">
                                            ȡ��</a>
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
