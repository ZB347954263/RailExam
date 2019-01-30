<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ExamSuccess.aspx.cs" Inherits="RailExamWebApp.Online.Exam.ExamSuccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script type="text/javascript">
     function Next()
     {
         var orgid = document.getElementById("hfOrgID").value;
         var search = window.location.search;
         var examResultId = search.substring(search.indexOf("ExamResultID=")+13);
         var type = search.substring(search.indexOf("ExamType=")+9,search.indexOf("&ExamResultID"));   
         
         if(document.getElementById("hfIsShowResult").value != "1")
         {
            alert("您没有查看当前考试试卷的权限！");
            return;
         }
     	
     	
     	if(type== 1)
         {   
                var re= window.open("/RailExamBao/RandomExam/RandomExamAnswerNew.aspx?type=online&id=" +examResultId +"&orgid="+orgid,
                    "ExamResult",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
                re.focus();
         }
     }

     function init() 
     {
     	if(document.getElementById("hfIsShowAnswer").value != "1")
     	{
     		document.getElementById("btnnext").style.display = "none";
     	}
     }
     
     function exit()
     {
        top.close();
        window.parent.parent.opener.returnValue = "false";
        window.parent.parent.opener.close();          
     }
    </script>

</head>
<body onload="init();">
    <form id="form1" runat="server">
        <div style="text-align: center;">
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <span style="font-family: 宋体; color: Red; font-size: 50pt">考试已提交成功！</span><br />
            <span style="font-family: 宋体; color: Red; font-size: 50pt">
                <asp:Label ID="lblScore" runat="server"></asp:Label></span>
            <br />
            <br />
            <span style="font-family: 宋体; color: Red; font-size: 50pt">
                <asp:Label ID="lblPass" runat="server"></asp:Label></span>
            <br />
            <br />
            <input id="btnnext" name="next" type="button" class="button" onclick="Next()" value="查看答卷" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <input type="button" class="button" onclick="exit()" value="退   出" />
        </div>
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <asp:HiddenField ID="hfIsShowResult" runat="server" />
        <asp:HiddenField ID="hfIsShowAnswer" runat="server" />
    </form>
</body>
</html>
