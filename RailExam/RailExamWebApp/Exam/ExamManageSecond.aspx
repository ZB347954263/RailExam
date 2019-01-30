<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ExamManageSecond.aspx.cs"
    Inherits="RailExamWebApp.Exam.ExamManageSecond" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ѡ���Ծ�</title>

    <script type="text/javascript">
        function ChoosePaper(id)
        {
            if(document.getElementById('hfMode').value != "ReadOnly")
            {
                if(id > 0)
                {
                    if(! confirm("�Ծ��Ѿ����ڣ���Ҫѡ������һ���Ծ��滻ԭ�Ծ���"))
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
                        �������</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        ѡ���Ծ�</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td>
                            <b>�ڶ�����ѡ���Ծ�</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            �������ƣ�
                            <asp:Label ID="txtPaperName" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            �Ծ���Ϣ�� <a onclick="ChoosePaper(<%=Grid1.Items.Count%>)" href="#"><b>ѡ���Ծ�</b></a>
                        </td>
                    </tr>
                </table>
                <div id="grid">
                    <ComponentArt:Grid ID="Grid1" runat="server" Width="97%">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="PaperId">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="PaperId" HeadingText="���" Visible="false" />
                                    <ComponentArt:GridColumn DataField="PaperName" HeadingText="�Ծ�����" />
                                    <ComponentArt:GridColumn DataField="CategoryName" HeadingText="�Ծ����" />
                                    <ComponentArt:GridColumn DataField="CategoryId" Visible="false" />
                                    <ComponentArt:GridColumn DataField="CreateMode" HeadingText="���ⷽʽ" Visible="false" />
                                    <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate1" HeadingText="���ⷽʽ" />
                                    <ComponentArt:GridColumn DataField="ItemCount" HeadingText="�趨����" />
                                    <ComponentArt:GridColumn DataField="TotalScore" HeadingText="�趨�ܷ�" />
                                    <ComponentArt:GridColumn DataField="CurrentItemCount" HeadingText="ʵ������" />
                                    <ComponentArt:GridColumn DataField="CurrentTotalScore" HeadingText="ʵ���ܷ�" />
                                    <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="������" />
                                    <ComponentArt:GridColumn DataCellClientTemplateId="CTedit" HeadingText="����" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="CTedit">
                                <a onclick="ManagePaper(##DataItem.getMember('PaperId').get_value()##)" href="#"><b>
                                    Ԥ���Ծ�</b></a>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="ClientTemplate1">
                                ## DataItem.getMember("CreateMode").get_value() == 1 ? "�ֹ�����" : "�������" ##
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
