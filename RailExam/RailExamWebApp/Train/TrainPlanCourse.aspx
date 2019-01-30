<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainPlanCourse.aspx.cs" Inherits="RailExamWebApp.Train.TrainPlanCourse" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择培训课程</title>
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
                <td style=" width:15%">工作岗位</td>
                <td style=" width:35%" align="left">
                    <asp:TextBox ID="txtPost" runat="server" ReadOnly="true"></asp:TextBox>
                    <a onclick="selectPost()" href="#"><b>选择工作岗位</b></a>
                    </td>
                <td style=" width:15%">培训类别</td>
                <td style=" width:35%" align="left">
                    <asp:TextBox ID="txtType" runat="server" ReadOnly="true" ></asp:TextBox>
                    <asp:LinkButton ID="btnSelectType" runat="server">选择类别</asp:LinkButton></td>
             </tr>
             <tr align="center">
                <td>课程名称</td>
                <td align="left">
                 <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                <td>公共课程</td>
                <td align="left"><asp:CheckBox ID="chk" runat="server" /></td>
             </tr>
             <tr align="center">
                <td colspan="4">
                    <asp:Button ID="btnQuery" runat="server" Text="查询" OnClick="btnQuery_Click" />
                    <br />
                </td>
             </tr>
             <tr>
                <td colspan="4" style="text-align:center">
                    培训课程
                </td>          
             </tr>
             <tr>
                <td colspan="4" style="width:90%; text-align: center;">
                    <ComponentArt:Grid ID="Grid1" runat="server" AllowPaging="true" PageSize="5">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="TrainCourseID"  >
                                <Columns>
                                    <ComponentArt:GridColumn AllowEditing="True" AllowSorting="False" DataField="Flag" HeadingText="选择" DataType="System.Boolean" ColumnType="CheckBox" />
                                    <ComponentArt:GridColumn DataField="TrainCourseID" Visible ="false" />
                                    <ComponentArt:GridColumn DataField="StandardID" Visible ="false" />
                                    <ComponentArt:GridColumn DataField="PostName" HeadingText="工作岗位" TextWrap="true"/>                     
                                    <ComponentArt:GridColumn DataField="TypeName" HeadingText="培训类别" />    
                                    <ComponentArt:GridColumn DataField="CourseNo" HeadingText="序号" />                     
                                    <ComponentArt:GridColumn DataField="CourseName" HeadingText="课程名称" TextWrap="true" />
                                    <ComponentArt:GridColumn DataField="StudyHours" HeadingText="课程学时"/>
                                    <ComponentArt:GridColumn DataField="StudyDemand" HeadingText="学习要求" TextWrap="true"/>
                                    <ComponentArt:GridColumn DataField="HasExam" ColumnType="CheckBox" DataType="System.Boolean" HeadingText="是否考试"/>
                                    <ComponentArt:GridColumn DataField="RequireCourseName" HeadingText="约束关系"/>
                                 </Columns>           
                            </ComponentArt:GridLevel>     
                        </Levels>
                     </ComponentArt:Grid>
                </td>
            </tr>
            <tr  align="center">
                <td colspan="4">
                    <asp:Button ID="btnAdd" runat="server" Text="↓" OnClick="btnAdd_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDel" runat="server" Text="↑" OnClick="btnDel_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:center">
                    本次培训的课程信息

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
                                    <ComponentArt:GridColumn AllowEditing="True" ColumnType="CheckBox" DataField="TrainCourseList.Flag" HeadingText="选择" />
                                    <ComponentArt:GridColumn DataField="TrainCourseID" Visible ="false" />
                                    <ComponentArt:GridColumn DataField="TrainCourseList.StandardID" Visible ="false" />
                                    <ComponentArt:GridColumn DataField="TrainCourseList.PostName" HeadingText="工作岗位" TextWrap="true" />                     
                                    <ComponentArt:GridColumn DataField="TrainCourseList.TypeName" HeadingText="培训类别" />    
                                    <ComponentArt:GridColumn DataField="TrainCourseList.CourseNo" HeadingText="序号" />                     
                                    <ComponentArt:GridColumn DataField="TrainCourseList.CourseName" HeadingText="课程名称" TextWrap="true"/>
                                    <ComponentArt:GridColumn DataField="TrainCourseList.StudyHours" HeadingText="课程学时" />
                                    <ComponentArt:GridColumn DataField="TrainCourseList.StudyDemand" HeadingText="学习要求" TextWrap="true"/>
                                    <ComponentArt:GridColumn DataField="TrainCourseList.HasExam" ColumnType="CheckBox" DataType="System.Boolean" HeadingText="是否考试"/>
                                    <ComponentArt:GridColumn DataField="TrainCourseList.RequireCourseName" HeadingText="约束关系"/>
                                 </Columns>           
                            </ComponentArt:GridLevel>     
                        </Levels>
                     </ComponentArt:Grid>
                </td>
            </tr>
            <tr align="center">
                <td colspan="4">
                <asp:Button ID="btnUp" runat="server" Text="上一步" OnClick="btnUp_Click"  /> &nbsp;&nbsp;&nbsp;&nbsp; 
                <asp:Button ID="btnNext" runat="server" OnClick="btnNext_Click" Text="下一步" />&nbsp; &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="取&nbsp; &nbsp; 消" OnClick="btnCancel_Click" /></td>
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
