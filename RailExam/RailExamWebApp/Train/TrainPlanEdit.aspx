<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainPlanEdit.aspx.cs" Inherits="RailExamWebApp.Train.TrainPlanEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>编辑培训计划信息</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table  border="1" style=" border-color:WindowText;  width: 100%; height:80%;">                
                    <tr>
                        <td style="width: 15%">
                            计划名称：
                        </td>
                        <td  colspan="3" style="width: 80%" align="left">
                            <asp:TextBox ID="txtPlanName" MaxLength="20" runat="server">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvKnowledgeName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="计划名称不能为空!" ControlToValidate="txtPlanName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            内容简介：
                        </td>
                        <td  colspan="3" align="left">
                            <asp:TextBox ID="txtContent" TextMode="MultiLine" Rows="3" runat="server"  Columns="50">
                            </asp:TextBox>
                        </td>
                    </tr>
                       <tr>
                        <td>
                            开始时间：
                        </td>
                        <td colspan="3"  align="left">
                            <asp:TextBox ID="txtBegin" runat="server" >
                            </asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td>
                            结束时间：
                        </td>
                        <td   align="left">
                            <asp:TextBox ID="txtEnd"  runat="server">
                            </asp:TextBox>
                        </td>
                        </tr>
                       <tr>
                       <td>
                            是否考试：
                        </td>
                        <td   align="left">
                            <asp:CheckBox ID="chkExam" runat="server" />
                        </td>                   
                       </tr>
                     <tr>
                        
                    </tr>
                       <tr>
                        <td style="width: 15%">
                            考试形式：

                        </td>
                        <td style="width:35%"  align="left">
                            <asp:TextBox ID="txtExam" runat="server" MaxLength="50">
                            </asp:TextBox>
                        </td>
                        </tr>
                       <tr>
                        <td style="width: 15%">
                            当前状态：
                        </td>
                        <td  style="width:35%" align="left">
                            <asp:DropDownList ID="ddlStatus" runat="server">
                           </asp:DropDownList>
                         </td>      
                                      
                     </tr>
                      <tr>
                        <td style="width: 15%">
                            备注：

                        </td>
                        <td  colspan="3" align="left">
                            <asp:TextBox ID="txtMemo" TextMode="MultiLine" runat="server" Columns="50" Rows="3">
                            </asp:TextBox>
                        </td>                
                     </tr>
                     <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="btnUp" runat="server" Text="上一步" Enabled="False" /> &nbsp;&nbsp;&nbsp;&nbsp; 
                            <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="下一步" />&nbsp; &nbsp; &nbsp; &nbsp;
                            <asp:Button ID="btnCancel" runat="server" Text="取&nbsp; &nbsp; 消" OnClick="btnCancel_Click" /></td>
                    </tr>
                </table>       
         <asp:ValidationSummary ID="ValidationSummary1" runat="server"  ShowMessageBox="true" ShowSummary="false"/>
   </div>
    </form>
</body>
</html>

