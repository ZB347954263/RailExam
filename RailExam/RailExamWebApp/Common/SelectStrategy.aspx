<%@ Page Language="C#" AutoEventWireup="True" Codebehind="SelectStrategy.aspx.cs"
    Inherits="RailExamWebApp.Common.SelectStrategy" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ѡ�����</title>

    <script type="text/javascript"> 
        function paperSelect(id,id1)
        {	 
	        if(id != null)
	        {     
	            var ct = id + "|" + id1;	
	            window.returnValue = ct;
	            window.close();	    		
           }
        }

        function searchButton_onClick()
        {        
            cb1.callback();           
            
            return false;
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
                        ѡ�����</div>
                </div>
            </div>
            <div id="content">
                <div id="query">
                    &nbsp;&nbsp;����ģʽ��                  
                    <select id="ddlView" onchange="searchButton_onClick();" name="ddlView">
                        <option value="0">--��ѡ��--</option>
                        <option value="2">���̲��½�</option>
                        <option value="3">�����⸨������</option>
                    </select>                   
                </div>
                <div id="grid">
                    <ComponentArt:CallBack ID="cb1" runat="server" OnCallback="cb1_Callback" PostState="true">
                        <Content>
                            <ComponentArt:Grid ID="Grid1" runat="server" DataSourceID="odsPaperStrategy">
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="PaperStrategyId">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="PaperStrategyId" Visible="false" />
                                            <ComponentArt:GridColumn DataField="CategoryName" HeadingText="�Ծ����" />
                                            <ComponentArt:GridColumn DataField="PaperStrategyName" HeadingText="����������" />
                                            <ComponentArt:GridColumn DataField="PaperCategoryId" Visible="false" />
                                            <ComponentArt:GridColumn DataField="IsRandomOrder" Visible="false" />
                                            <ComponentArt:GridColumn DataField="SingleAsMultiple" Visible="false" />
                                            <ComponentArt:GridColumn DataField="StrategyMode" Visible="false" />
                                            <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="����ģʽ" />
                                            <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate1" HeadingText="����������ʾ˳��" />
                                            <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate2" HeadingText="�ѵ�ѡ��ʾ�ɶ�ѡ" />
                                            <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="����" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="CTEdit">
                                        <a onclick="paperSelect('##DataItem.getMember('PaperStrategyId').get_value()##','##DataItem.getMember('PaperStrategyName').get_value()##')"
                                            href="#"><b>ѡ��</b></a>
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="ClientTemplate1">
                                        ## DataItem.getMember("IsRandomOrder").get_value() == 1 ? "��" : "��" ##
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="ClientTemplate2">
                                        ## DataItem.getMember("SingleAsMultiple").get_value() == 1 ? "��":"��" ##
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="ClientTemplate3">
                                        ## DataItem.getMember("StrategyMode").get_value() == 2 ? "���̲��½�" : "�����⸨������" ##
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                            </ComponentArt:Grid>
                        </Content>
                    </ComponentArt:CallBack>
                </div>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsPaperStrategy" runat="server" SelectMethod="GetPaperStrategyByPaperCategoryId"
            TypeName="RailExam.BLL.PaperStrategyBLL">
            <SelectParameters>
                <asp:QueryStringParameter Name="PaperCategoryId" QueryStringField="id" Type="Int32" />
                <asp:FormParameter FormField="ddlView" Type="Int32" Name="StrategyMode" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
