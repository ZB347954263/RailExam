<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamMessage.aspx.cs" Inherits="RailExamWebApp.RandomExam.ExamMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
   <script type="text/javascript">
        function exit()
        {
              var  examtime=document.getElementById("hfExam").value;
              document.getElementById("hfExam").value=examtime-1;
              if(document.getElementById("hfExam").value == 0)
              {
                  window.close();
              }
              setTimeout("exit()", 1000);				
        }
   </script> 
</head>
<body onload="exit()">
    <form id="form1" runat="server">
 <div style="text-align: center;">
            <br />
            <br />
            <br />
            <br />
            <span style="font-family: 宋体; color: Red; font-size: 25pt">离考试结束还剩下5分钟！</span><br />
        </div>
        <asp:HiddenField ID="hfExam" runat="server" Value="5"/>
    </form>
</body>
</html>
