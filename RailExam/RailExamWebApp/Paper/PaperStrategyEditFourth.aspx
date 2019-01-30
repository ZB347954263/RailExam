<%@ Page Language="C#" AutoEventWireup="True" Codebehind="PaperStrategyEditFourth.aspx.cs"
    Inherits="RailExamWebApp.Paper.PaperStrategyEditFourth" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组卷策略大题详细信息</title>

    <script type="text/javascript">
        function selectChapter()
        {
	        var selectedChapter = window.showModalDialog('../Common/SelectStrategyChapter.aspx', 
                '', 'help:no; status:no; dialogWidth:300px;dialogHeight:660px');
            
            if(! selectedChapter)
            {
                return;
            }
            
           // document.getElementById('HfBookId').value =selectedChapter.split('|')[0];
            document.getElementById('HfChapterId').value = selectedChapter.split('|')[1];
            document.getElementById('txtChapterName').value = selectedChapter.split('|')[2];
            document.getElementById('HfRangeType').value = selectedChapter.split('|')[3];
            document.getElementById('HfRangeName').value = selectedChapter.split('|')[2];
            
            
        }
   
        function selectChapterS()
        {
            var selectedChapter = window.showModalDialog('../Common/MultiSelectBookChapter.aspx', 
            '', 'help:no; status:no; dialogWidth:300px;dialogHeight:660px');

            if(! selectedChapter)
            {
                return;
            }

            document.getElementById('HfExCludeChaptersId').value = selectedChapter.split('|')[0];
            document.getElementById('txtExCludeChapters').value = selectedChapter.split('|')[1];            
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
                        组卷策略大题详细信息</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 10%">
                            大题名称：</td>
                        <td style="width: 80%">
                            <asp:Label ID="txtSubjectName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            教材章节</td>
                        <td>
                            <asp:TextBox ID="txtChapterName" runat="server" Width="270px" ReadOnly="true">
                            </asp:TextBox>
                            <img id="ImgSelectChapterName" style="cursor: hand;" onclick="selectChapter();" src="../Common/Image/search.gif"
                                alt="选择教材章节" border="0" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="“教材章节”不能为空！" ControlToValidate="txtChapterName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            屏蔽教材章节</td>
                        <td>
                            <asp:TextBox ID="txtExCludeChapters" runat="server" Width="270px" ReadOnly="true">
                            </asp:TextBox>
                            <img id="ImgSelectExCludeChapters" style="cursor: hand;" onclick="selectChapterS();"
                                src="../Common/Image/search.gif" alt="选择屏蔽教材章节" border="0" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            试题类型</td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" DataSourceID="odsItemType" DataTextField="TypeName"
                                DataValueField="ItemTypeId">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            随机题数</td>
                        <td>
                            <asp:TextBox ID="txtNDR" runat="server" Width="100px"></asp:TextBox>(本大题设定的总题数为：<asp:Label
                                runat="server" ID="labelTotalCount"></asp:Label>)
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="随机题数应为数字！"
                                Display="None" MaximumValue="9999" MinimumValue="0" Type="Integer" ControlToValidate="txtNDR"></asp:RangeValidator>
                        </td>
                    </tr>
                    <asp:Panel runat="server" Visible="false">
                        <tr visible="false">
                            <td>
                                难度1题数</td>
                            <td>
                                <asp:TextBox ID="txtNd1" runat="server" Width="100px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr visible="false">
                            <td>
                                难度2题数</td>
                            <td>
                                <asp:TextBox ID="txtNd2" runat="server" Width="100px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr visible="false">
                            <td>
                                难度3题数</td>
                            <td>
                                <asp:TextBox ID="txtNd3" runat="server" Width="100px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr visible="false">
                            <td>
                                难度4题数</td>
                            <td>
                                <asp:TextBox ID="txtNd4" runat="server" Width="100px">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr visible="false">
                            <td>
                                难度5题数</td>
                            <td>
                                <asp:TextBox ID="txtNd5" runat="server" Width="100px">
                                </asp:TextBox>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td>
                            每题分数</td>
                        <td>
                            <asp:TextBox ID="txtScore" runat="server" Width="100px" ReadOnly="true">
                            </asp:TextBox>(来自大题定义的每题分数)
                        </td>
                    </tr>
                    <tr>
                        <td>
                            每题限时</td>
                        <td>
                            <asp:TextBox ID="txtSeconds" Width="100px" runat="server">
                            </asp:TextBox>秒
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注</td>
                        <td>
                            <asp:TextBox ID="txtMemo" runat="server" TextMode="MultiLine">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div id="button" style="text-align: center;">
                    <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" ImageUrl="../Common/Image/save.gif" />
                    <asp:ImageButton ID="CancelButton" runat="server" Visible="false" OnClientClick="return window.close();"
                        ImageUrl="../Common/Image/confirm.gif" />
                </div>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsItemType" runat="server" SelectMethod="GetItemTypes"
            TypeName="RailExam.BLL.ItemTypeBLL"></asp:ObjectDataSource>
        <asp:HiddenField ID="hfSubjectId" runat="server" />
        <asp:HiddenField ID="HfChapterId" runat="server" />
        <asp:HiddenField ID="HfBookId" runat="server" />
        <asp:HiddenField ID="HfRangeType" runat="server" />
        <asp:HiddenField ID="HfRangeName" runat="server" />
        <asp:HiddenField ID="HfExCludeChaptersId" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
    </form>
</body>
</html>
