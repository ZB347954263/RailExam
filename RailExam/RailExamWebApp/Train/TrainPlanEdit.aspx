<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainPlanEdit.aspx.cs" Inherits="RailExamWebApp.Train.TrainPlanEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�༭��ѵ�ƻ���Ϣ</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table  border="1" style=" border-color:WindowText;  width: 100%; height:80%;">                
                    <tr>
                        <td style="width: 15%">
                            �ƻ����ƣ�
                        </td>
                        <td  colspan="3" style="width: 80%" align="left">
                            <asp:TextBox ID="txtPlanName" MaxLength="20" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvKnowledgeName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="�ƻ����Ʋ���Ϊ��!" ControlToValidate="txtPlanName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ���ݼ�飺
                        </td>
                        <td  colspan="3" align="left">
                            <asp:TextBox ID="txtContent" TextMode="MultiLine" Rows="3" runat="server"  Columns="50">
                            </asp:TextBox>
                        </td>
                    </tr>
                       <tr>
                        <td>
                            ��ʼʱ�䣺
                        </td>
                        <td colspan="3"  align="left">
                            <asp:TextBox ID="txtBegin" runat="server" >
                            </asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            ����ʱ�䣺
                        </td>
                        <td   align="left">
                            <asp:TextBox ID="txtEnd"  runat="server">
                            </asp:TextBox>
                        </td>
                        </tr>
                       <tr>
                       <td>
                            �Ƿ��ԣ�
                        </td>
                        <td   align="left">
                            <asp:CheckBox ID="chkExam" runat="server" />
                        </td>                   
                       </tr>
                     <tr>
                        
                    </tr>
                       <tr>
                        <td style="width: 15%">
                            ������ʽ��

                        </td>
                        <td style="width:35%"  align="left">
                            <asp:TextBox ID="txtExam" runat="server" MaxLength="50">
                            </asp:TextBox>
                        </td>
                        </tr>
                       <tr>
                        <td style="width: 15%">
                            ��ǰ״̬��
                        </td>
                        <td  style="width:35%" align="left">
                            <asp:DropDownList ID="ddlStatus" runat="server">
                           </asp:DropDownList>
                         </td>      
                                      
                     </tr>
                      <tr>
                        <td style="width: 15%">
                            ��ע��

                        </td>
                        <td  colspan="3" align="left">
                            <asp:TextBox ID="txtMemo" TextMode="MultiLine" runat="server" Columns="50" Rows="3">
                            </asp:TextBox>
                        </td>                
                     </tr>
                     <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnUp" runat="server" Text="��һ��" Enabled="False" /> &nbsp;&nbsp;&nbsp;&nbsp; 
                            <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="��һ��" />&nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="ȡ&nbsp; &nbsp; ��" OnClick="btnCancel_Click" /></td>
                    </tr>
                </table>       
         <asp:ValidationSummary ID="ValidationSummary1" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
   </div>
    </form>
</body>
</html>

