<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainCourseBookDetail.aspx.cs" Inherits="RailExamWebApp.Train.TrainCourseBookDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�༭�γ���ؽ̲���Ϣ</title>
    <style id="textbox" type="text/css">
        .textbox{width:90%; border:1 1 1 1; border-style:groove;}
        .textboxMultiple{width:96%; border:1 1 1 1; border-style:groove;}    
        td{text-align:left; border-right:1; border-bottom:1; border-style:groove;}   
        table{border-width:1 1 1 1; border-style:groove;}     
    </style>

    <script type="text/javascript">
        // ��֤�û�����
        function editBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "�༭";
            
            return true;
        }
        
        function updateBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "";
            
            return true;
        }
        function cancelBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "";
            
            return true;
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <asp:FormView ID="fvTrainCourseBook" runat="server" BorderStyle="Solid" GridLines="Both"
            HorizontalAlign="Left" DataSourceID="dsTrainCourseBook" Width="320px" DataKeyNames="TrainCourseBookChapterID">
            <EditItemTemplate>
                <table style="width: 95%">
                    <tr>
                        <td style="width: 15%">
                            ѧϰҪ��
                        </td>
                        <td colspan="3" style="width: 35%">
                            <asp:TextBox ID="txtTime"  TextMode="MultiLine" Rows="3" runat="server" Text='<%# Bind("StudyDemand") %>'>
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTypeName" runat="server" EnableClientScript="true"
                                Display="None" ErrorMessage="ѧϰҪ����Ϊ�գ�" ControlToValidate="txtTime">
                            </asp:RequiredFieldValidator>
                        </td>
                     </tr>
                     <tr>
                        <td style="width: 15%">
                            ����ѧʱ
                        </td>
                        <td colspan="3" style="width: 35%">
                            <asp:TextBox ID="txtHour"  runat="server" Text='<%# Bind("StudyHours") %>'>
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" EnableClientScript="true"
                                Display="None" ErrorMessage="����ѧʱ����Ϊ�գ�" ControlToValidate="txtHour">
                            </asp:RequiredFieldValidator>
                             <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="����ѧʱӦΪ���֣�" Display="None" MaximumValue="9999" MinimumValue="0" Type="Double" ControlToValidate="txtHour"></asp:RangeValidator>
                       </td>
                     </tr>
                    <tr>
                        <td>
                            ��ע
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxMemo"  TextMode="MultiLine" Rows="3"  runat="server" Text='<%# Bind("Memo") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" />
                        </td>
                    </tr>
                </table>
                <asp:LinkButton ID="EditButton" runat="server" Visible="false" CausesValidation="False" CommandName="Edit"
                    Text="�༭"></asp:LinkButton>
                <asp:LinkButton ID="NewButton" runat="server" Visible="false" CausesValidation="False" CommandName="New"
                    Text="�½�">
                </asp:LinkButton>             
                  <asp:LinkButton ID="DeleteButton" runat="server" Visible="false" CausesValidation="False" CommandName="Delete"
                    Text="ɾ��" ></asp:LinkButton>
                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update"
                    Text="����" OnClientClick="return updateBtnClientClick();">
                </asp:LinkButton>
                <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                    Text="ȡ��" OnClientClick="return cancelBtnClientClick();">
                </asp:LinkButton>
            </EditItemTemplate>
          
            <ItemTemplate>
                <table style="width: 95%;">
                    <tr>
                        <td style="width: 15%">
                            ѧϰҪ��
                        </td>
                        <td colspan="3" style="width: 35%">
                            <asp:TextBox ID="txtTime1"  TextMode="MultiLine" Enabled="false" Rows="3" runat="server" Text='<%# Bind("StudyDemand") %>'>
                            </asp:TextBox>
                        </td>
                     </tr>
                     <tr>
                        <td style="width: 15%">
                            ����ѧʱ
                        </td>
                        <td colspan="3" style="width: 35%">
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("StudyHours") %>'></asp:Label>
                        </td>
                     </tr>
                    <tr>
                        <td>
                            ��ע
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxMemo1"  Enabled="false" TextMode="MultiLine" Rows="3"  runat="server" Text='<%# Bind("Memo") %>'>
                            </asp:TextBox>
                    </tr>
                </table>
               <asp:LinkButton ID="EditButton" runat="server"  CausesValidation="False" CommandName="Edit"
                    Text="�༭" OnClientClick="return editBtnClientClick();">
                </asp:LinkButton>
                <asp:LinkButton ID="NewButton" runat="server" Visible="false" CausesValidation="False" CommandName="New"
                    Text="�½�">
                </asp:LinkButton>
                <asp:LinkButton ID="DeleteButton" Visible="false" runat="server" CausesValidation="False" CommandName="Delete"
                    Text="ɾ��" >
                </asp:LinkButton>
                 <asp:LinkButton ID="UpdateButton" runat="server" Visible="false" CausesValidation="True" CommandName="Update"
                    Text="����">
                </asp:LinkButton>
                <asp:LinkButton ID="InsertCancelButton" runat="server" Visible="false"  CausesValidation="False" CommandName="Cancel"
                    Text="ȡ��">
                </asp:LinkButton>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="dsTrainCourseBook" runat="server" DeleteMethod="DeleteTrainCourseBook"
            SelectMethod="GetTrainCourseBookInfo" TypeName="RailExam.BLL.TrainCourseBookBLL"
            UpdateMethod="UpdateTrainCourseBook" DataObjectTypeName="RailExam.Model.TrainCourseBook"
            EnableViewState="False">
            <DeleteParameters>
                <asp:QueryStringParameter DefaultValue="1" Name="TrainCourseBookChapterID" QueryStringField="id"
                    Type="Int32" />
            </DeleteParameters>
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="1" Name="TrainCourseBookChapterID" QueryStringField="id"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
