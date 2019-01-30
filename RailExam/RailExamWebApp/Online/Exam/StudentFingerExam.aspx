<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentFingerExam.aspx.cs" Inherits="RailExamWebApp.Online.Exam.StudentFingerExam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>考试信息确认页面</title>
    <script type="text/javascript">
    function StartExam() 
    {
    	var employeeID = document.getElementById("hfEmployeeID").value;
    	var ExamId = document.getElementById("hfExamID").value;
        var ret = window.open("/RailExamBao/RandomExam/CheckAttendExamInfo.aspx?id="+ExamId+"&employeeID="+employeeID,"CheckAttendExamInfo","fullscreen=yes,toolbar=no,scrollbars=no,status=no");
        ret.focus();
    }
    
    function logout()
    {
       top.returnValue = "false";
    } 
    
    </script>
</head>
<body onbeforeunload="logout()">
    <form id="form1" runat="server">
    <div align="center" style="height: 500px; overflow-y: auto;">
        <table style="font-size: 10pt; font-weight: bold;">
            <tr>
                <td style="text-align: right;height:20px"></td>
                <td style="text-align: left;height:20px" colspan="2"></td>
            </tr>
            <tr>
                <td style="text-align: left;height:20px">姓名：</td>
                <td style="text-align: left;height:20px"><asp:Label runat="server" ID="lblName"></asp:Label></td>
                <td rowspan="5">&nbsp;&nbsp;<asp:Image ID="myImagePhoto" ImageUrl="" runat="server" Width="118px" Height="177px" /></td>
            </tr>
             <tr>
                <td style="text-align: left;height:20px">性别：</td>
                <td style="text-align: left;height:20px"><asp:Label runat="server" ID="lblSex"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: left;height:20px">单位：</td>
                <td style="text-align: left;height:20px"><asp:Label runat="server" ID="lblOrgName"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: left;height:20px">车间：</td>
                <td style="text-align: left;height:20px"><asp:Label runat="server" ID="lblWorkShop"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: left;height:20px">职名：</td>
                <td style="text-align: left;height:20px"><asp:Label runat="server" ID="lblPost"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: left;height:20px">身份证号码：</td>
                <td style="text-align: left;height:20px" colspan="2"><asp:Label runat="server" ID="lblIdentityCard"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: left;height:20px">岗位合格证：</td>
                <td style="text-align: left;height:20px" colspan="2"><asp:Label runat="server" ID="lblPostNo"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: right;height:20px"></td>
                <td style="text-align: left;height:20px" colspan="2"></td>
            </tr>
            <tr>
                <td style="text-align: right;height:20px;color:red; font-size:14pt;">您参加的考试是：</td>
                <td style="text-align: left;height:20px;color:red; font-size:14pt;" colspan="2"><asp:Label runat="server" ID="lblExam"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: right;height:20px"></td>
                <td style="text-align: left;height:20px" colspan="2"></td>
            </tr>
            <tr>
                <td colspan="3">
                    <input type="button" class="button" value="开始考试" onclick="StartExam()"/>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField runat="server" ID="hfEmployeeID"/>
     <asp:HiddenField runat="server" ID="hfExamID"/>
    </form>
</body>
</html>
