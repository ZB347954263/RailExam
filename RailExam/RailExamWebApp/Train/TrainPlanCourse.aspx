<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainPlanCourse.aspx.cs" Inherits="RailExamWebApp.Train.TrainPlanCourse" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ѡ����ѵ�γ�</title>
    <script type="text/javascript">
        function selectPost()
        {
            var selectedPost = window.showModalDialog('../Common/SelectPost.aspx', 
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:580px');

            if(! selectedPost)
            {
                return;
            }
            
            document.getElementById("txtPostID").value = selectedPost.split('|')[0];
            document.getElementById("txtPostName").value = selectedPost.split('|')[1];
        }
        
        function SelectType(id,name)
        {
            var re= window.open("SelectType.aspx?PostID="+id+"&PostName="+name ,'SelectType',' Width=300px; Height=600px,status=false,resizable=yes',true);		
            re.focus();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:90%; height:580; text-align: left;">
             <tr align="center">
                <td style=" width:15%">������λ</td>
                <td style=" width:35%" align="left">
                    <asp:TextBox ID="txtPost" runat="server" ReadOnly="true"></asp:TextBox>
                    <a onclick="selectPost()" href="#"><b>ѡ������λ</b></a>
                    </td>
                <td style=" width:15%">��ѵ���</td>
                <td style=" width:35%" align="left">
                    <asp:TextBox ID="txtType" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:LinkButton ID="btnSelectType" runat="server">ѡ�����</asp:LinkButton></td>
             </tr>
             <tr align="center">
                <td>�γ�����</td>
                <td align="left">
                 <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                <td>�����γ�</td>
                <td align="left"><asp:CheckBox ID="chk" runat="server" /></td>
             </tr>
             <tr align="center">
                <td colspan="4">
                    <asp:Button ID="btnQuery" runat="server" Text="��ѯ" OnClick="btnQuery_Click" />
                    <br />
                </td>
             </tr>
             <tr>
                <td colspan="4" style="text-align:center">
                    ��ѵ�γ�
                </td>          
             </tr>
             <tr>
                <td colspan="4" style="width:90%; text-align: center;">
                    <ComponentArt:Grid ID="Grid1" runat="server" AllowPaging="true" PageSize="5">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="TrainCourseID"  >
                                <Columns>
                                    <ComponentArt:GridColumn AllowEditing="True" AllowSorting="False" DataField="Flag" HeadingText="ѡ��" DataType="System.Boolean" ColumnType="CheckBox" />
                                    <ComponentArt:GridColumn DataField="TrainCourseID" Visible ="false" />
                                    <ComponentArt:GridColumn DataField="StandardID" Visible ="false" />
                                    <ComponentArt:GridColumn DataField="PostName" HeadingText="������λ" TextWrap="true"/>                     
                                    <ComponentArt:GridColumn DataField="TypeName" HeadingText="��ѵ���" />    
                                    <ComponentArt:GridColumn DataField="CourseNo" HeadingText="���" />                     
                                    <ComponentArt:GridColumn DataField="CourseName" HeadingText="�γ�����" TextWrap="true" />
                                    <ComponentArt:GridColumn DataField="StudyHours" HeadingText="�γ�ѧʱ"/>
                                    <ComponentArt:GridColumn DataField="StudyDemand" HeadingText="ѧϰҪ��" TextWrap="true"/>
                                    <ComponentArt:GridColumn DataField="HasExam" ColumnType="CheckBox" DataType="System.Boolean" HeadingText="�Ƿ���"/>
                                    <ComponentArt:GridColumn DataField="RequireCourseName" HeadingText="Լ����ϵ"/>
                                 </Columns>           
                            </ComponentArt:GridLevel>     
                        </Levels>
                     </ComponentArt:Grid>
                </td>
            </tr>
            <tr  align="center">
                <td colspan="4">
                    <asp:Button ID="btnAdd" runat="server" Text="��" OnClick="btnAdd_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDel" runat="server" Text="��" OnClick="btnDel_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:center">
                    ������ѵ�Ŀγ���Ϣ

                </td>
             </tr> 
            <tr>
                <td colspan="4" style="width:90%; text-align: center;">
                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetTrainPlanCourseInfoByPlanID" TypeName="RailExam.BLL.TrainPlanCourseBLL">
                  <SelectParameters ><asp:QueryStringParameter  Name="TrainPlanID" QueryStringField="PlanID"  Type="Int32"/></SelectParameters>
                </asp:ObjectDataSource>
                    <ComponentArt:Grid ID="Grid2" runat="server" DataSourceID="ObjectDataSource2" AllowPaging="true" PageSize="4">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="TrainCourseID"  >
                                <Columns>
                                    <ComponentArt:GridColumn AllowEditing="True" ColumnType="CheckBox" DataField="TrainCourseList.Flag" HeadingText="ѡ��" />
                                    <ComponentArt:GridColumn DataField="TrainCourseID" Visible ="false" />
                                    <ComponentArt:GridColumn DataField="TrainCourseList.StandardID" Visible ="false" />
                                    <ComponentArt:GridColumn DataField="TrainCourseList.PostName" HeadingText="������λ" TextWrap="true" />                     
                                    <ComponentArt:GridColumn DataField="TrainCourseList.TypeName" HeadingText="��ѵ���" />    
                                    <ComponentArt:GridColumn DataField="TrainCourseList.CourseNo" HeadingText="���" />                     
                                    <ComponentArt:GridColumn DataField="TrainCourseList.CourseName" HeadingText="�γ�����" TextWrap="true"/>
                                    <ComponentArt:GridColumn DataField="TrainCourseList.StudyHours" HeadingText="�γ�ѧʱ" />
                                    <ComponentArt:GridColumn DataField="TrainCourseList.StudyDemand" HeadingText="ѧϰҪ��" TextWrap="true"/>
                                    <ComponentArt:GridColumn DataField="TrainCourseList.HasExam" ColumnType="CheckBox" DataType="System.Boolean" HeadingText="�Ƿ���"/>
                                    <ComponentArt:GridColumn DataField="TrainCourseList.RequireCourseName" HeadingText="Լ����ϵ"/>
                                 </Columns>           
                            </ComponentArt:GridLevel>     
                        </Levels>
                     </ComponentArt:Grid>
                </td>
            </tr>
            <tr align="center">
                <td colspan="4">
                <asp:Button ID="btnUp" runat="server" Text="��һ��" OnClick="btnUp_Click"  /> &nbsp;&nbsp;&nbsp;&nbsp; 
                <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="��һ��" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="ȡ&nbsp; &nbsp; ��" OnClick="btnCancel_Click" /></td>
            </tr>
        </table>
    </div>
    <input id="txtPostID" type="hidden" name="txtPostID" />
    <input id="txtPostName" type="hidden" name="txtPostName" />
    <input id="txtTypeID" type="hidden" name="txtTypeID" />
    <input id="txtTypeName" type="hidden" name="txtTypeName" />
    </form>
</body>
</html>
