<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainCourseEdit.aspx.cs" Inherits="RailExamWebApp.Train.TrainCourseEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�༭�γ���Ϣ</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table  border="1" style=" border-color:WindowText;  width: 100%; height:80%;">                
                    <tr>
                        <td style="width: 15%">
                            �γ����ƣ�
                        </td>
                        <td  colspan="3" style="width: 80%" align="left">
                            <asp:TextBox ID="txtCourseName" MaxLength="20" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvKnowledgeName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="�γ����Ʋ���Ϊ��!" ControlToValidate="txtCourseName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ���ݼ�飺
                        </td>
                        <td  colspan="3" align="left">
                            <asp:TextBox ID="txtDescription" MaxLength="200" runat="server"  Columns="50">
                            </asp:TextBox>
                        </td>
                    </tr>
                       <tr>
                        <td>
                            ѧϰҪ��

                        </td>
                        <td colspan="3"  align="left">
                            <asp:TextBox ID="txtStudyDemand" runat="server"  Columns="50" >
                            </asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            ����ѧʱ��
                        </td>
                        <td   align="left">
                            <asp:TextBox ID="txtHour"  runat="server">
                            </asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="����ѧʱӦΪ���֣�" Display="None" MaximumValue="9999" MinimumValue="0" Type="Double" ControlToValidate="txtHour"></asp:RangeValidator>
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
                            Լ����ϵ��
                        </td>
                        <td  style="width:35%" align="left">
                          <asp:DropDownList runat="server"   ID="ddlCourse"></asp:DropDownList>
                         </td>                 
                     </tr>
                      <tr>
                        <td style="width: 15%">
                            ��ע��

                        </td>
                        <td  colspan="3" align="left">
                            <asp:TextBox ID="txtMemo" runat="server" Columns="50" MaxLength="50">
                            </asp:TextBox>
                        </td>                
                     </tr>
                     <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnUp" runat="server" Text="��һ��" Enabled="False" />
                             &nbsp;&nbsp;&nbsp;&nbsp; 
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="��һ��" />&nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="ȡ&nbsp; &nbsp; ��" OnClick="Button2_Click" /></td>
                    </tr>
                </table>       
         <asp:ValidationSummary ID="ValidationSummary1" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
   </div>
    </form>
</body>
</html>
