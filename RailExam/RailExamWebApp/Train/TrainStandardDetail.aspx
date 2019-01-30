<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainStandardDetail.aspx.cs" Inherits="RailExamWebApp.Train.TrainStandardDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>��ѵ�淶��ϸ��Ϣ</title>
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
        function deleteBtnClientClick()
        {
            var theNode = window.parent.tvTrainType.get_selectedNode();
            if(theNode.get_nodes().get_length() > 0)
            {
                alert("�ýڵ����¼��ڵ㣬����ɾ����");
                return false;
            }
            return confirm("��ȷ��Ҫɾ���˼�¼��");
        }
        function newBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            var theNode = window.parent.tvTrainType.get_selectedNode();
            
            if(theNode.get_depth() >= 2)
            {
                alert("����Ϊ�ýڵ������¼��ڵ㣡");
                return false;
            }
            theItem.value = "����";
            
            return true;
        }
        function updateBtnClientClick()
        {
            var theItem = window.parent.document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "";
            
            return true;
        }
        function insertBtnClientClick()
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
<body style="text-align: center; width:500; ">
    <form id="form1" runat="server">
        <asp:FormView ID="fvTrainStandard" runat="server" BorderStyle="Solid" GridLines="Both"
            HorizontalAlign="Left" DataSourceID="dsTrainStandard" Width="455px" DataKeyNames="TrainStandardID">
            <EditItemTemplate>
                <table style="width: 95%">
                    <tr>
                        <td style="width: 15%">
                            ְ��
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="lblPost" runat="server" Text='<%# Bind("PostName") %>'></asp:Label>
                        </td> 
                        <td style="width: 15%">
                            ��ѵ���
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="lblType" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                        </td>                    
                        </tr>
                    <tr>
                        <td style="width: 15%">
                            ��ѵʱ��
                        </td>
                        <td colspan="3" style="width: 35%">
                            <asp:TextBox ID="txtTime" MaxLength="100" Columns="50" runat="server" Text='<%# Bind("TrainTime") %>'>
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTypeName" runat="server" EnableClientScript="true"
                                Display="Dynamic" ErrorMessage="��ѵʱ�䲻��Ϊ�գ�" ControlToValidate="txtTime">
                            </asp:RequiredFieldValidator>
                        </td>
                     </tr>
                     <tr>
                        <td style="width: 15%">
                            ��ѵ����
                        </td>
                        <td colspan="3" style="width: 35%">
                            <asp:TextBox ID="txtContent" MaxLength="100" Columns="50" runat="server" Text='<%# Bind("TrainContent") %>'>
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" EnableClientScript="true"
                                Display="Dynamic" ErrorMessage="��ѵ���ݲ���Ϊ�գ�" ControlToValidate="txtContent">
                            </asp:RequiredFieldValidator>
                        </td>
                     </tr>
                     <tr>
                        <td style="width: 15%">
                            ��֯��ʽ
                        </td>
                        <td colspan="3" style="width: 35%">
                            <asp:TextBox ID="txtTrain" MaxLength="100" Columns="50" runat="server" Text='<%# Bind("TrainForm") %>'>
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" EnableClientScript="true"
                                Display="Dynamic" ErrorMessage="��֯��ʽ����Ϊ�գ�" ControlToValidate="txtTrain">
                            </asp:RequiredFieldValidator>
                        </td>
                     </tr>
                     <tr>
                        <td style="width: 15%">
                            ������ʽ
                        </td>
                        <td colspan="3" style="width: 35%">
                            <asp:TextBox ID="txtExam" MaxLength="100"  Columns="50" runat="server" Text='<%# Bind("ExamForm") %>'>
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" EnableClientScript="true"
                                Display="Dynamic" ErrorMessage="������ʽ����Ϊ�գ�" ControlToValidate="txtExam">
                            </asp:RequiredFieldValidator>
                        </td>
                     </tr>
                     <tr>
                        <td style="width: 15%">
                            ����
                        </td>
                        <td colspan="3" >
                            <asp:TextBox ID="tbxDescription" MaxLength="100"  Columns="50" runat="server" Text='<%# Bind("Description") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            ��ע
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="tbxMemo" MaxLength="50"  Columns="50" runat="server" Text='<%# Bind("Memo") %>'>
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <asp:LinkButton ID="EditButton" runat="server" Visible="false" CausesValidation="False" CommandName="Edit"
                    Text="�༭" ></asp:LinkButton>
                <asp:LinkButton ID="NewButton" runat="server" Visible="false" CausesValidation="False" CommandName="New"
                    Text="�½�">
                </asp:LinkButton>             
                 <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                    Text="ɾ��" OnClientClick="return deleteBtnClientClick();"></asp:LinkButton>
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
                            ְ��
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="lblPost1" runat="server" Text='<%# Bind("PostName") %>'></asp:Label>
                        </td> 
                        <td style="width: 15%">
                            ��ѵ���
                        </td>
                        <td style="width: 35%">
                            <asp:Label ID="lblType1" runat="server" Text='<%# Bind("TypeName") %>'></asp:Label>
                        </td>                    
                        </tr>
                    <tr>
                        <td style="width: 15%">
                            ��ѵʱ��
                        </td>
                        <td colspan="3" style="width: 35%">
                           <asp:Label ID="lblTime1" runat="server" Text='<%# Bind("TrainTime") %>'></asp:Label>
                        </td>
                     </tr>
                     <tr>
                        <td style="width: 15%">
                            ��ѵ����
                        </td>
                        <td colspan="3" style="width: 35%">
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("TrainContent") %>'></asp:Label>
                        </td>
                     </tr>
                     <tr>
                        <td style="width: 15%">
                            ��֯��ʽ
                        </td>
                        <td colspan="3" style="width: 35%">
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("TrainForm") %>'></asp:Label>
                        </td>
                     </tr>
                     <tr>
                        <td style="width: 15%">
                            ������ʽ
                        </td>
                        <td colspan="3" style="width: 35%">
                           <asp:Label ID="Label3" runat="server" Text='<%# Bind("ExamForm") %>'></asp:Label>
                        </td>
                     </tr>
                    <tr>                       
                     <td style="width: 15%;">
                            ����
                        </td>
                        <td  colspan="3" style="width: 35%;">
                            <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Bind("Description") %>'></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            ��ע
                        </td>
                        <td colspan="3">
                            <asp:Label ID="MemoLabel" runat="server" Text='<%# Bind("Memo") %>'></asp:Label></td>
                    </tr>
                </table>
                <asp:LinkButton ID="EditButton" runat="server"  CausesValidation="False" CommandName="Edit"
                    Text="�༭" OnClientClick="return editBtnClientClick();">
                </asp:LinkButton>
                <asp:LinkButton ID="NewButton" runat="server" Visible="false" CausesValidation="False" CommandName="New"
                    Text="�½�">
                </asp:LinkButton>
                <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                    Text="ɾ��" OnClientClick="return deleteBtnClientClick();">
                </asp:LinkButton>
                 <asp:LinkButton ID="UpdateButton" runat="server" Visible="false" CausesValidation="True" CommandName="Update"
                    Text="����">
                </asp:LinkButton>
                <asp:LinkButton ID="InsertCancelButton" runat="server" Visible="false"  CausesValidation="False" CommandName="Cancel"
                    Text="ȡ��">
                </asp:LinkButton>
            </ItemTemplate>
        </asp:FormView>
        <asp:ObjectDataSource ID="dsTrainStandard" runat="server" DeleteMethod="DeleteTrainStandard"
            SelectMethod="GetTrainStandardInfo" TypeName="RailExam.BLL.TrainStandardBLL"
            UpdateMethod="UpdateTrainStandard" DataObjectTypeName="RailExam.Model.TrainStandard"
            EnableViewState="False">
            <DeleteParameters>
                <asp:QueryStringParameter DefaultValue="1" Name="TrainStandardID" QueryStringField="id"
                    Type="Int32" />
            </DeleteParameters>
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="1" Name="TrainStandardID" QueryStringField="id"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
