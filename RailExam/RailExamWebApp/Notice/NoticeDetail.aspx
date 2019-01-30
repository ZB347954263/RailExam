<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="NoticeDetail.aspx.cs" Inherits="RailExamWebApp.Notice.NoticeDetail" %>

<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>֪ͨ������ϸ��Ϣ</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    
        function $F(objId)
       {
            return document.getElementById(objId);
       }  
       
        function deleteBtnClientClick()
        {
            return confirm("��ȷ��Ҫɾ���˼�¼��");
        }
        
          function insertBtnClientClick()
        {
        
         if($F("FormView1_hfReceiveOrgIDS") && $F("FormView1_hfReceiveOrgIDS").value.length == 0&&$F("FormView1_hfReceiveEmployeeIDS") && $F("FormView1_hfReceiveEmployeeIDS").value.length == 0)
            {
                alert("���յ�λ�ͽ����˲���ͬʱΪ�գ�");
               
               return false; 
            } 
           
         }
        
        
        function SelectOrganizations()
        {   
              var   cleft;   
              var   ctop;   
              cleft=(screen.availWidth-320)*.5;   
              ctop=(screen.availHeight-610)*.5; 
            window.open('SelectOrganizations.aspx?id='+document.getElementById("FormView1_hfReceiveOrgIDS").value,
                'SelectOrganizations',' Width=320px; Height=610px, left='+cleft+',top='+ctop+',status=false,resizable=no',true);
	    }
	    
	    function SelectEmployees()
        {
           var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-860)*.5;   
            ctop=(screen.availHeight-600)*.5;   
            window.open('../Exam/SelectEmployeeDetail.aspx?op=3&id='+document.getElementById("FormView1_hfReceiveEmployeeIDS").value, 
                'SelectEmployees', 'Width=860px; Height=600px, left='+cleft+',top='+ctop+',status=false,resizable=no',true);
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
                        ֪ͨ������ϸ��Ϣ
                    </div>
                </div>
            </div>
            <div id="content">
                <asp:FormView ID="FormView1" runat="server" DataSourceID="odsNoticeDetail" DataKeyNames="NoticeID"
                    OnItemUpdated="FormView1_ItemUpdated" OnItemDeleted="FormView1_ItemDeleted" OnItemInserted="FormView1_ItemInserted"
                    OnDataBound="FormView1_DataBound">
                    <EditItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 5%">��������</td>
                                <td style="width: 15%"><asp:Label ID="lblOrgNameEdit" runat="server"></asp:Label></td>
                                <td style="width: 5%">������</td>
                                <td style="width: 15%"><asp:Label ID="lblEmployeeNameEdit" runat="server"></asp:Label></td>
                            </tr>        
                            <tr>
                                <td>����</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtTitleEdit" runat="server" Width="70%" Text='<%# Bind("Title") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="�����⡱����Ϊ�գ�"
                                        ToolTip="�����⡱����Ϊ�գ�" ControlToValidate="txtTitleEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>�����̶�</td>
                                <td>
                                    <asp:DropDownList ID="ddlImportanceNameEdit" runat="server" DataSourceID="odsDdlImportanceName" 
                                        DataTextField="ImportanceName" DataValueField="ImportanceID" SelectedValue='<%# Bind("ImportanceID") %>'>
                                    </asp:DropDownList>
                                </td>
                                  <td>����ʱ��</td>
                                <td>
                                    <uc1:Date ID="dateCreateTimeEdit" runat="server" DateValue='<%# Bind("CreateTime","{0:yyyy-MM-dd}") %>' />
                                </td>
                                
                              
                            </tr>
                            <tr>
						        <td>���յ�λ</td>
						        <td><a onclick="SelectOrganizations()" href="#"><b>ѡ����յ�λ</b></a></td>
						        <td>������</td>
						        <td><a onclick="SelectEmployees()" href="#"><b>ѡ�������</b></a></td>
					        </tr>
                            <tr>
                                <td>��Ч����</td>
                                <td>
                                    <asp:TextBox ID="txtDayCountEdit" runat="server" Text='<%# Bind("DayCount") %>'></asp:TextBox>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="����Ч��������Ϊ�գ�"
                                        ToolTip="����Ч����������Ϊ�գ�" ControlToValidate="txtDayCountEdit" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="����Ч������ӦΪ1��9999��������"
                                        Display="Dynamic" MaximumValue="9999" MinimumValue="1" Type="Integer" ControlToValidate="txtDayCountEdit">
                                        <img alt="����Ч������ӦΪ1��9999��������" src="../Common/Image/error.gif"></asp:RangeValidator>
						        </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>����</td>
                                <td colspan="3"><asp:TextBox ID="txtContentEdit" runat="server" Height="160px" Width="98%" TextMode="MultiLine" Text='<%# Bind("Content") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>��ע</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtMemoEdit" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                        <asp:HiddenField ID="hfReceiveOrgIDS" runat="server" Value='<%# Bind("ReceiveOrgIDS") %>' />
                        <asp:HiddenField ID="hfReceiveEmployeeIDS" runat="server" Value='<%# Bind("ReceiveEmployeeIDS") %>' />
                        <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" OnClientClick="return insertBtnClientClick();"
                            Text='&lt;img border=0 src="../Common/Image/save.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text='&lt;img border=0 src="../Common/Image/cancel.gif" alt="" /&gt;' OnClientClick="return window.close();">
                        </asp:LinkButton>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 5%">��������</td>
                                <td style="width: 15%"><asp:Label ID="lblOrgNameInsert" runat="server"></asp:Label></td>
                                <td style="width: 5%">������</td>
                                <td style="width: 15%"><asp:Label ID="lblEmployeeNameInsert" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>����</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtTitleInsert" runat="server" Width="70%" Text='<%# Bind("Title") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="�����⡱����Ϊ�գ�"
                                        ToolTip="�����⡱����Ϊ�գ�" ControlToValidate="txtTitleInsert" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>�����̶�</td>
                                <td>
                                    <asp:DropDownList ID="ddlImportanceNameInsert" runat="server" DataSourceID="odsDdlImportanceName" 
                                        DataTextField="ImportanceName" DataValueField="ImportanceID" SelectedValue='<%# Bind("ImportanceID") %>'>
                                    </asp:DropDownList>
                                </td>
                                  <td>����ʱ��</td>
                                <td>
                                    <uc1:Date ID="dateCreateTimeInsert" runat="server" DateValue='<%# Bind("CreateTime","{0:yyyy-MM-dd}") %>' />
                                </td>
                                
                              
                            </tr>                      
					        
					         <tr>
						        <td>���յ�λ</td>
						        <td><a onclick="SelectOrganizations()" href="#"><b>ѡ����յ�λ</b></a>						        
						        </td>
						        <td>������</td>
						        <td><a onclick="SelectEmployees()" href="#"><b>ѡ�������</b></a></td>
					        </tr>
					        
					        
                            <tr>
                                <td>��Ч����</td>
                                <td>
                                    <asp:TextBox ID="txtDayCountInsert" runat="server" Text='<%# Bind("DayCount") %>'></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="����Ч��������Ϊ�գ�"
                                        ToolTip="����Ч����������Ϊ�գ�" ControlToValidate="txtDayCountInsert" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="����Ч������ӦΪ1��9999��������"
                                        Display="Dynamic" MaximumValue="9999" MinimumValue="1" Type="Integer" ControlToValidate="txtDayCountInsert">
                                        <img alt="����Ч������ӦΪ1��9999��������" src="../Common/Image/error.gif"></asp:RangeValidator>
                                </td>
                                
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>����</td>
                                <td colspan="3"><asp:TextBox ID="txtContentInsert" runat="server" Height="160px" Width="98%" TextMode="MultiLine" Text='<%# Bind("Content") %>'></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>��ע</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtMemoInsert" runat="server" Width="98%" TextMode="MultiLine" Text='<%# Bind("Memo") %>'></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                        <asp:HiddenField ID="hfReceiveOrgIDS" runat="server" Value='<%# Bind("ReceiveOrgIDS") %>' />
                        <asp:HiddenField ID="hfReceiveEmployeeIDS" runat="server" Value='<%# Bind("ReceiveEmployeeIDS") %>' />
                        <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" OnClientClick="return insertBtnClientClick();"
                            Text='&lt;img border=0 src="../Common/Image/save.gif" alt="" /&gt;'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel"
                            Text='&lt;img border=0 src="../Common/Image/cancel.gif" alt="" /&gt;' OnClientClick="return window.close();">
                        </asp:LinkButton>
                    </InsertItemTemplate>
                    <ItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 5%">��������</td>
                                <td style="width: 15%">
                                    <asp:Label ID="lblOrgName" runat="server" Text='<%# Eval("OrgName") %>'></asp:Label></td>
                                <td style="width: 5%">������</td>
                                <td style="width: 15%">
                                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label></td>
                            </tr>
                            <tr>
                                <td>����</td>
                                <td colspan="3"><asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>�����̶�</td>
                                <td><asp:Label ID="lblImportanceName" runat="server" Text='<%# Eval("ImportanceName") %>'></asp:Label></td>
                                    <td>����ʱ��</td>
                                <td><asp:Label ID="lblCreateTime" runat="server" Text='<%# Eval("CreateTime") %>'></asp:Label></td> </tr>
                            <tr>
						        <td>���յ�λ</td>
						        <td><b>ѡ����յ�λ</b></td>
						        <td>������</td>
						        <td><b>ѡ�������</b></td>
					        </tr>
                            <tr>
                            
                              
                                
                                <td>��Ч����</td>
                                <td><asp:Label ID="lblDayCount" runat="server" Text='<%# Eval("DayCount") %>'></asp:Label></td>
                         
                                
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>����</td>
                                <td colspan="3" style="white-space: normal;">
                                    <%# Eval("Content") %></td>
                            </tr>
                            <tr>
                                <td>��ע</td>
                                <td colspan="3" style="white-space: normal;">
                                    <%# Eval("Memo") %></td>
                            </tr>
                        </table>
                        <br />
                        <div>
                            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit"
                                Text='&lt;img border=0 src="../Common/Image/edit.gif" alt="" /&gt;'>
                            </asp:LinkButton>
                            <asp:LinkButton ID="DeleteButton" runat="server" CausesValidation="False" CommandName="Delete"
                                Text='&lt;img border=0 src="../Common/Image/delete.gif" alt="" /&gt;' OnClientClick="return deleteBtnClientClick();">
                            </asp:LinkButton>
                            <asp:LinkButton ID="NewButton" runat="server" CausesValidation="False" CommandName="New"
                                Text='&lt;img border=0 src="../Common/Image/add.gif" alt="" /&gt;'>
                            </asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:FormView>
                <asp:ObjectDataSource ID="odsNoticeDetail" runat="server" DataObjectTypeName="RailExam.Model.Notice"
                    DeleteMethod="DeleteNotice" InsertMethod="AddNotice" SelectMethod="GetNotice"
                    TypeName="RailExam.BLL.NoticeBLL" UpdateMethod="UpdateNotice" EnableViewState="false">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="NoticeID" QueryStringField="id" Type="Int32"/>
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ObjectDataSource ID="odsDdlImportanceName" runat="server" SelectMethod="GetImportances" TypeName="RailExam.BLL.ImportanceBLL">
                </asp:ObjectDataSource>
            </div>
        </div>
    </form>
</body>
</html>
