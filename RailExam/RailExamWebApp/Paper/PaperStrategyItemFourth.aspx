<%@ Page Language="C#" AutoEventWireup="True" Codebehind="PaperStrategyItemFourth.aspx.cs"
    Inherits="RailExamWebApp.Paper.PaperStrategyItemFourth" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�����Դ�����ϸ��Ϣ</title>

    <script type="text/javascript">
        function selectItemCategory()   
        { 
	        var selectedChapter = window.showModalDialog('../Common/SelectItemCategory.aspx', 
                '', 'help:no; status:no; dialogWidth:300px;dialogHeight:640px');
            
            if(! selectedChapter)
            {
                return;
            }
            
            document.getElementById('hfItemCategoryID').value = selectedChapter.split('|')[0];
            document.getElementById('txtItemCategoryName').value = selectedChapter.split('|')[1];  
        }
   
        function selectItemCategoryS()  
        {
            var selectedChapter = window.showModalDialog('../Common/MultiSelectItemCategory.aspx', 
                '', 'help:no; status:no; dialogWidth:300px;dialogHeight:640px');
            
            if(! selectedChapter)
            {
                return;
            }
            
            document.getElementById('hfExCludeItemCategoryIDS').value = selectedChapter.split('|')[0];
            document.getElementById('txtExCludeItemCategorys').value = selectedChapter.split('|')[1];  
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
                        �����Դ�����ϸ��Ϣ</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 10%">
                            �������ƣ�</td>
                        <td style="width: 80%">
                            <asp:Label ID="txtSubjectName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ���⸨������</td>
                        <td>
                            <asp:TextBox ID="txtItemCategoryName" runat="server" Width="230px" ReadOnly="true">
                            </asp:TextBox>
                            <img id="ImgSelectItemCategory" style="cursor: hand;" onclick="selectItemCategory();"
                                src="../Common/Image/search.gif" alt="ѡ�����⸨������" border="0" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="�����⸨�����ࡱ����Ϊ�գ�" ControlToValidate="txtItemCategoryName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ���θ�������</td>
                        <td>
                            <asp:TextBox ID="txtExCludeItemCategorys" runat="server" Width="230px" ReadOnly="true">
                            </asp:TextBox>
                            <img id="ImgSelectExCludeItemCategorys" style="cursor: hand;" onclick="selectItemCategoryS();"
                                src="../Common/Image/search.gif" alt="ѡ�����θ�������" border="0" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��������</td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" DataSourceID="odsItemType" DataTextField="TypeName"
                                DataValueField="ItemTypeId">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            �������</td>
                        <td>
                            <asp:TextBox ID="txtNDR" runat="server" Width="100px">
                            </asp:TextBox>(�������趨��������Ϊ��<asp:Label runat="server" ID="labelTotalCount"></asp:Label>)
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="�Ѷ��������ӦΪ���֣�"
                                Display="None" MaximumValue="9999" MinimumValue="0" Type="Integer" ControlToValidate="txtNDR"></asp:RangeValidator>
                        </td>
                    </tr>
                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                        <tr visible="false">
                            <td>
                                �Ѷ�1����</td>
                            <td>
                                <asp:TextBox ID="txtNd1" runat="server" Width="100px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr visible="false">
                            <td>
                                �Ѷ�2����</td>
                            <td>
                                <asp:TextBox ID="txtNd2" runat="server" Width="100px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr visible="false">
                            <td>
                                �Ѷ�3����</td>
                            <td>
                                <asp:TextBox ID="txtNd3" runat="server" Width="100px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr visible="false">
                            <td>
                                �Ѷ�4����</td>
                            <td>
                                <asp:TextBox ID="txtNd4" runat="server" Width="100px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr visible="false">
                            <td>
                                �Ѷ�5����</td>
                            <td>
                                <asp:TextBox ID="txtNd5" runat="server" Width="100px">
                                </asp:TextBox>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td>
                            ÿ�����</td>
                        <td>
                            <asp:TextBox ID="txtScore" runat="server" Width="100px" ReadOnly="true">
                            </asp:TextBox>(���Դ��ⶨ���ÿ�����)
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ÿ����ʱ</td>
                        <td>
                            <asp:TextBox ID="txtSeconds" Width="100px" runat="server">
                            </asp:TextBox>��
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��ע</td>
                        <td>
                            <asp:TextBox ID="txtMemo" runat="server" TextMode="MultiLine">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div id="button" style="text-align: center;">
                    <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" ImageUrl="../Common/Image/complete.gif" />
                    <asp:ImageButton ID="CancelButton" runat="server" Visible="false" OnClientClick="return window.close();"
                        ImageUrl="../Common/Image/confirm.gif" />
                </div>
            </div>
            <asp:ObjectDataSource ID="odsItemType" runat="server" SelectMethod="GetItemTypes"
                TypeName="RailExam.BLL.ItemTypeBLL"></asp:ObjectDataSource>
            <asp:HiddenField ID="hfSubjectId" runat="server" />
            <asp:HiddenField ID="hfItemCategoryID" runat="server" />
            <asp:HiddenField ID="hfExCludeItemCategoryIDS" runat="server" />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                ShowSummary="false" />
        </div>
    </form>
</body>
</html>
