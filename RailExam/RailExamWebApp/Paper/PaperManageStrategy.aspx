<%@ Page Language="C#" AutoEventWireup="True" Codebehind="PaperManageStrategy.aspx.cs"
    Inherits="RailExamWebApp.Paper.PaperManageStrategy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>�������ѡ��������</title>

    <script type="text/javascript">
        function selectPaperStrategy()   
        {
            if(document.getElementById('hfMode').value != "ReadOnly")
            {
            
                var id = document.getElementById('hfCategoryId').value; 
                var   cleft;   
                var   ctop;   
                cleft=(screen.availWidth-800)*.5;   
                ctop=(screen.availHeight-600)*.5; 
           	  
	            var selectedPaperStrategy = window.showModalDialog('../Common/SelectStrategy.aspx?id='+id, 
                    '', 'help:no; status:no; dialogWidth:800px;dialogHeight:600px;');
                
                if(! selectedPaperStrategy)
                {
                    return;
                }                
                document.getElementById('txtStrategyName').value = selectedPaperStrategy.split('|')[1];
                document.getElementById('hfStrategyId').value = selectedPaperStrategy.split('|')[0];               
                
            }  
        }
        
         function ManagePaper(id)
        {      
         
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5;              
          
           var re = window.open("PaperPreview.aspx?id="+id,"PaperPreview","Width=800px; Height=600px,status=false,resizable=yes,left="+cleft+",top="+ctop+",scrollbars=yes",true);
           re.focus();
        }        
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        �Ծ����</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        �������ѡ��������</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td colspan="2">
                            <b>�ڶ������������ѡ��������</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            �Ծ����ƣ�
                            <asp:Label ID="lblPaperName" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            �Ծ���ࣺ
                            <asp:Label ID="lblPaperCategoryName" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20%">
                            ѡ����Ծ�����µ������ԣ�</td>
                        <td>
                            <asp:TextBox ID="txtStrategyName" runat="server" ReadOnly="true" Width="30%"></asp:TextBox>
                            <img style="cursor: hand;" onclick="selectPaperStrategy();" src="../Common/Image/search.gif"
                                alt="ѡ��������" border="0" />
                            <asp:RequiredFieldValidator ID="rfvStrategyName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="��ѡ�������ԣ�" ControlToValidate="txtStrategyName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <div id="button" style="text-align: center;">
                    <br />
                    <asp:ImageButton ID="btnLast" runat="server" CausesValidation="False" OnClick="btnLast_Click"
                        ImageUrl="../Common/Image/last.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnSave" runat="server" OnClick="btnSave_Click" ImageUrl="../Common/Image/complete.gif" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnPreview" runat="server"  CausesValidation="false" ImageUrl="../Common/Image/preview.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnEdit" runat="server"  CausesValidation="false" ImageUrl="../Common/Image/edit.gif" OnClick="btnEdit_Click" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfMode" runat="server" />
        <asp:HiddenField ID="hfCategoryId" runat="server" />
        <asp:HiddenField ID="hfStrategyId" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
    </form>
</body>
</html>
