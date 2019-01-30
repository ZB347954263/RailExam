<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainPlanEmployeeSelect.aspx.cs" Inherits="RailExamWebApp.Train.TrainPlanEmployeeSelect" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>选择培训课程</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width:90%; height:580; text-align: left;">
             <tr>
                <td colspan="3" style="text-align:center">
                    员工信息
                </td>          
             </tr>
             <tr>
                <td  style="width:90%; text-align: center;">
                    <ComponentArt:Grid ID="Grid1" runat="server" AllowPaging="true"  PageSize="8">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="EmployeeID"  >
                                <Columns>
                                    <ComponentArt:GridColumn AllowEditing="True" AllowSorting="False" DataField="Flag" HeadingText="选择" DataType="System.Boolean" ColumnType="CheckBox" />
                                    <ComponentArt:GridColumn DataField="EmployeeID" HeadingText="编号"/>                     
                                    <ComponentArt:GridColumn DataField="WorkNo" HeadingText="员工编码"  />    
                                    <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="姓名" />                     
                                    <ComponentArt:GridColumn DataField="PostName" HeadingText="工作岗位" />
                                </Columns>           
                            </ComponentArt:GridLevel>     
                        </Levels>
                     </ComponentArt:Grid>
                </td>
             </tr>
             <tr>
                <td  valign="middle" align="center">
                    <asp:Button ID="btnAdd" runat="server" Text="↓" OnClick="btnAdd_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnDel" runat="server" Text="↑" OnClick="btnDel_Click" />
                </td>
             </tr>
             <tr>
                <td colspan="3" style="text-align:center">
                    参加本次培训的员工信息

                </td>
             </tr> 
             <tr>
                <td style="width:90%; text-align: center;">
                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetTrainPlanEmployeeInfoByPlanID" TypeName="RailExam.BLL.TrainPlanEmployeeBLL">
                  <SelectParameters ><asp:QueryStringParameter  Name="TrainPlanID" QueryStringField="PlanID"  Type="Int32"/></SelectParameters>
                </asp:ObjectDataSource>
                    <ComponentArt:Grid ID="Grid2" runat="server" DataSourceID="ObjectDataSource2" AllowPaging="true" PageSize="8" >
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="TrainPlanEmployeeID" >
                                <Columns>
                                    <ComponentArt:GridColumn AllowEditing="True" DataField="TrainEmployeeList.Flag" ColumnType="CheckBox" HeadingText="选择" />
                                    <ComponentArt:GridColumn DataField="TrainPlanEmployeeID" Visible ="false" />
                                    <ComponentArt:GridColumn DataField="TrainEmployeeList.EmployeeID" HeadingText="编号" />                     
                                    <ComponentArt:GridColumn DataField="TrainEmployeeList.WorkNo" HeadingText="员工编码" />    
                                    <ComponentArt:GridColumn DataField="TrainEmployeeList.EmployeeName" HeadingText="姓名" />                     
                                    <ComponentArt:GridColumn DataField="TrainEmployeeList.PostName" HeadingText="工作岗位" />
                                 </Columns>           
                            </ComponentArt:GridLevel>     
                        </Levels>
                     </ComponentArt:Grid>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
