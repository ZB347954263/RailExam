<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentLogin.aspx.cs" Inherits="RailExamWebApp.RandomExamOther.StudentLogin" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考生登录</title>

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">
        function ShowExamList()
       {
//        var   cleft;   
//        var   ctop;   
//        cleft=(screen.availWidth-800)*.5;   
//        ctop=(screen.availHeight-600)*.5;
//        var re= window.open("/RailExamBao/Online/Exam/OnlineExamList.aspx",'OnlineExamList'," Width=800; Height=600,status=false,left="+cleft+",top="+ctop+",resizable=yes",true);
//        re.focus();
//         window.close();   
         
         var ret = showCommonDialogFull("/RailExamBao/Online/Exam/OnlineExamList.aspx",'dialogWidth:'+window.screen.width+'px;dialogHeight:'+window.screen.height+'px;');
         if(ret == "false" )
         {
            top.returnValue = ret;
            top.close();
         }
      }
      
      function ShowStudy()
      {
        var search = window.location.search;
        var str1 = search.substring(search.indexOf('&')+1);
        var str =  str1.substring(search.indexOf('=')+1);
        if(str == "student")
        {
            var ret = showCommonDialog("/RailExamBao/Online/Study/SelectStudyHaErBin.aspx?type=student",'dialogWidth:480px;dialogHeight:320px;');
            if(ret == "true" )
            {
                
            }
        }  
        else
        {
            var ret = showCommonDialog("/RailExamBao/Online/Study/SelectStudyHaErBin.aspx?type=teacher",'dialogWidth:480px;dialogHeight:320px;');
            if(ret == "true" )
            {

            }
         }
      }
      
      function ShowResultList()
      {
          var ret = showCommonDialogFull("/RailExamBao/Online/Exam/OnlineExamResultList.aspx",'dialogWidth:'+window.screen.width+'px;dialogHeight:'+window.screen.height+'px;');
         if(ret == "false" )
         {
            top.returnValue = "false";
            top.close();
           }
      }
      
         function inputCallback_onCallbackComplete()
        {
            var type = document.getElementById("hfType").value;
            if(type == "middle")
            {
                ShowExamList();
            }
            else if(type == "right")
            {
               ShowResultList();
            }
        }
      
  
    function ChangePassword()
    {
         var ret = showCommonDialog("/RailExamBao/Online/ChangePassword.aspx",'dialogWidth:350px;dialogHeight:200px;');
         if(ret == "true" )
         {
            alert('修改密码成功！');
         }
    }
   
   function logout()
   {
       var employeeID = document.getElementById("hfEmployeeID").value;   
       top.returnValue = "false|"+employeeID;
   } 
    </script>

</head>
<body onbeforeunload="logout()">
    <form id="form1" runat="server">
        <div style="text-align: center; color: #2D67CF;">
            <table cellspacing="10" cellpadding="1" border="0">
                <tr>
                    <td nowrap align="right" valign="middle" style="width: 30%">
                        <asp:Label ID="Label1" runat="server">单&nbsp;&nbsp;&nbsp;&nbsp;位：</asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblOrgName" runat="server" Visible="False" CssClass="LoginLabel"></asp:Label>
                        <asp:DropDownList ID="ddlOrg" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td nowrap align="right" valign="middle" style="width: 30%">
                        <asp:Label ID="lblUserName" runat="server">工资编号：</asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblEmployeeName" runat="server" Visible="False" CssClass="LoginLabel"></asp:Label>
                        <asp:TextBox ID="txtUserName" TabIndex="1" runat="server" Columns="20"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserName" runat="server" ErrorMessage="“用户名”不能为空！"
                            ControlToValidate="txtUserName" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td nowrap align="right">
                        <asp:Label ID="lblPassword" runat="server">密&nbsp;&nbsp;&nbsp;&nbsp;码：</asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblOrg" runat="server" Visible="False" CssClass="LoginLabel">Label</asp:Label>
                        <asp:TextBox ID="txtPassword" TabIndex="2" runat="server" TextMode="Password" Columns="21"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="“密码”不能为空！"
                            ControlToValidate="txtPassword" Display="None"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td nowrap align="right">
                        <asp:Label ID="lbl" runat="server" Visible="false">职&nbsp;&nbsp;&nbsp;&nbsp;名：</asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblPost" runat="server" Visible="False" CssClass="LoginLabel">Label</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:Button ID="btnExam" runat="server" CssClass="button" OnClientClick="ShowExamList()"
                            Text="进入考试" Visible="false" /><asp:Button ID="btnStudy" runat="server" CssClass="button"
                                OnClientClick="ShowStudy()" Text="进入学习" Visible="false" /><asp:Button ID="btnResult"
                                    runat="server" CssClass="button" Text="查看成绩" OnClientClick="ShowResultList()"
                                    Visible="false" />&nbsp; &nbsp;<asp:Button ID="ImageButtonLogin" runat="server" CssClass="button"
                                        Text="登   录" OnClick="ImageButtonLogin_Click"></asp:Button><asp:Button ID="btnModifyPsw"
                                            runat="server" CssClass="button" OnClientClick="ChangePassword()" Text="修改密码"
                                            Visible="false" />&nbsp;&nbsp;<asp:Button ID="btnExit" runat="server" CssClass="button"
                                                Text="退    出" Visible="false" OnClick="btnExit_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:ValidationSummary ID="ValidationSummary" runat="server" ShowSummary="False"
            ShowMessageBox="True" DisplayMode="List"></asp:ValidationSummary>
        <asp:HiddenField ID="hfLoginTime" runat="server" Value="0" />
        <asp:HiddenField ID="hfEmployeeID" runat="server" />
        <componentart:callback id="inputCallback" runat="server" oncallback="inputCallback_Callback">
            <ClientEvents>
                <CallbackComplete EventHandler="inputCallback_onCallbackComplete" />
            </ClientEvents>
            <Content>
                <asp:HiddenField ID="hfType" runat="server" />
            </Content>
        </componentart:callback>
    </form>
</body>
</html>
