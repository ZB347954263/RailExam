<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ExamResultUpdateDetail.aspx.cs"
    Inherits="RailExamWebApp.Exam.ExamResultUpdateDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>成绩修改记录</title>

    <script type="text/javascript">
       function selectBtnClicked()
        {         
            var cause=document.getElementById("TextBoxCause").value;  
            var remark=document.getElementById("TextBoxRemark").value;  
                           
            if(cause=="")
            {
                alert("请输入修改原因！");
                return;
            }         
          
            var ret=cause+"|"+remark;
            window.returnValue = escape(ret);
            window.close();
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
          <div id="page">
            <div id="head">
                <div id="location">
                    <div id="parent">手工评卷</div>
                    <div id="separator"></div>
                    <div id="current">
                        成绩修改记录</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                        <tr>
                            <td style="text-align: center">
                                修改人
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="LabelWorkMan" runat="server"></asp:Label>
                            </td>
                            <td style="text-align: center">
                                修改时间
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="LabelTime" runat="server"></asp:Label>
                            </td>
                        </tr> 
                        <tr>
                            <td style="text-align: center">
                                考生姓名
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="lblExamineeName" runat="server"></asp:Label>
                            </td>
                             <td style="text-align: center">
                                考生原始分数
                            </td>
                            <td style="text-align: left">
                                <asp:Label ID="LabelOldScore" runat="server"></asp:Label>
                            </td>
                            
                        </tr>
                        <tr>
                            <td style="text-align: center">
                                修改原因
                            </td>
                            <td style="text-align: left" colspan="3">
                                <asp:TextBox runat="server" ID="TextBoxCause" TextMode="MultiLine" Height="60px"></asp:TextBox>
                            </td>
                        </tr>
                             <tr>
                            <td style="text-align: center">
                                备注
                            </td>
                            <td style="text-align: left" colspan="3">
                                <asp:TextBox runat="server" ID="TextBoxRemark" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                       
                    </table>
                </div>
                <div>
                    <br />
                    <img onclick="selectBtnClicked();" src="../Common/Image/confirm.gif" alt="" />
                </div>
            </div>        
    </form>
</body>
</html>
