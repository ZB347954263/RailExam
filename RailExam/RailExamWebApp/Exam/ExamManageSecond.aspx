<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ExamManageSecond.aspx.cs"
    Inherits="RailExamWebApp.Exam.ExamManageSecond" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择试卷</title>

    <script type="text/javascript">
        function ChoosePaper(id)
        {
            if(document.getElementById('hfMode').value != "ReadOnly")
            {
                if(id > 0)
                {
                    if(! confirm("试卷已经存在，您要选择另外一份试卷替换原试卷吗？"))
                    {
                        return false;
                    }
                }

	            var selectedPaper = window.showModalDialog('../Paper/SelectPaper.aspx', 
                    '', 'help:no; status:no; dialogWidth:820px;dialogHeight:620px');
                
                if(! selectedPaper)
                {
                    return;
                }
                
                document.getElementById('hfPaperId').value = selectedPaper;
                form1.submit();
                document.getElementById('hfPaperId').value = "";
            }
        }
   
        function ManagePaper(id)
        {
           var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5;    
         
            var re = window.open("../Paper/PaperPreview.aspx?id="+id,"PaperPreview","Width=800px; Height=600px,scrollbars=yes,left="+cleft+",top="+ctop+",status=false,resizable=no",true);
			re.focus();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="parent">
                        考试设计</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        选择试卷</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td>
                            <b>第二步：选择试卷</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            考试名称：
                            <asp:Label ID="txtPaperName" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            试卷信息： <a onclick="ChoosePaper(<%=Grid1.Items.Count%>)" href="#"><b>选择试卷</b></a>
                        </td>
                    </tr>
                </table>
                <div id="grid">
                    <ComponentArt:Grid ID="Grid1" runat="server" Width="97%">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="PaperId">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="PaperId" HeadingText="编号" Visible="false" />
                                    <ComponentArt:GridColumn DataField="PaperName" HeadingText="试卷名称" />
                                    <ComponentArt:GridColumn DataField="CategoryName" HeadingText="试卷分类" />
                                    <ComponentArt:GridColumn DataField="CategoryId" Visible="false" />
                                    <ComponentArt:GridColumn DataField="CreateMode" HeadingText="出题方式" Visible="false" />
                                    <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate1" HeadingText="出题方式" />
                                    <ComponentArt:GridColumn DataField="ItemCount" HeadingText="设定题数" />
                                    <ComponentArt:GridColumn DataField="TotalScore" HeadingText="设定总分" />
                                    <ComponentArt:GridColumn DataField="CurrentItemCount" HeadingText="实际题数" />
                                    <ComponentArt:GridColumn DataField="CurrentTotalScore" HeadingText="实际总分" />
                                    <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="出卷人" />
                                    <ComponentArt:GridColumn DataCellClientTemplateId="CTedit" HeadingText="操作" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="CTedit">
                                <a onclick="ManagePaper(##DataItem.getMember('PaperId').get_value()##)" href="#"><b>
                                    预览试卷</b></a>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="ClientTemplate1">
                                ## DataItem.getMember("CreateMode").get_value() == 1 ? "手工出题" : "随机出题" ##
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </div>
                <div id="button" style="text-align: center;">
                    <br />
                    <asp:ImageButton ID="btnLast" runat="server" CausesValidation="False" OnClick="btnLast_Click"
                        ImageUrl="../Common/Image/last.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnSaveAndNext" runat="server" OnClick="btnSaveAndNext_Click"
                        ImageUrl="../Common/Image/next.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                        ImageUrl="../Common/Image/cancel.gif" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfMode" runat="server" />
        <asp:HiddenField ID="hfPaperId" runat="server" />
    </form>
</body>
</html>
