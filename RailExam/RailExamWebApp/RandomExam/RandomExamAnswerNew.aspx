<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RandomExamAnswerNew.aspx.cs" Inherits="RailExamWebApp.RandomExam.RandomExamAnswerNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考试成绩</title>
    <link href="../Online/style/ExamPaperResult.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript">
    	var count = 0;
     function updateScore(resultId,itemId,score,answer,hasyear) 
     {
     	if(!confirm("您确定将该考生的当前题目的考生答案修改为正确答案吗？\r\n如果确定，将无法恢复原考生答案！")) {
     		return;
     	}
     	count = count + 1;
     	//alert(document.getElementById('span-' + itemId + '-1').innerHTML);
     	document.getElementById('span-' + itemId + '-1').innerHTML = document.getElementById('span-' + itemId + '-0').innerHTML;
     	document.getElementById('span-' + itemId + '-2').innerHTML = score;
     	document.getElementById("lblScore").innerHTML = parseFloat(document.getElementById("lblScore").innerHTML) +  parseFloat(score);
     	document.getElementById("span-" + itemId).className = "ExamAnswer";
     	document.getElementById("a-" + itemId).style.display = "none";
     	
     	updateCallback.Callback(resultId, itemId, answer,hasyear);
     }
     
     function logout() {
     	    if(document.getElementById("hfLoginEmployeeID").value == "0" && count>0)
     	    {
     	        window.opener.form1.RefreshList.value ="true";
                window.opener.form1.submit();
       	        window.opener.form1.RefreshList.value ="";
     	    }

     	var search = window.location.search;
     	if(search.indexOf("type=online")>=0) {
     		window.opener.exit();
     		window.close();
     	}
     }
    </script>
</head>
<body onbeforeunload="logout()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="parent">
                        随机考试
                    </div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        考试成绩
                    </div>
                </div>
            </div>
            <div id="content">
                <div id="Div1">
                    <table>
                         <tr>
                            <td style="width:120px; text-align: center;font-size:13pt; ">登录指纹</td>
                            <td style="width:120px; text-align: center;font-size:13pt; ">档案头像</td> 
                           <td style="width:170px; text-align: center;font-size:13pt; ">采集图像（1）</td> 
                           <td style="width:170px; text-align: center;font-size:13pt; ">采集图像（2）</td> 
                           <td style="width:170px; text-align: center;font-size:13pt; ">采集图像（3）</td>  
                        </tr>
                        <tr>
                            <td style="font-size:13pt; "><asp:Label runat="server" ID="lblFignerDate"></asp:Label></td>
                           <td></td>
                           <td style="font-size:13pt; "><asp:Label runat="server" ID="lblPhotoDate1"></asp:Label></td>
                           <td style="font-size:13pt; "><asp:Label runat="server" ID="lblPhotoDate2"></asp:Label></td>
                           <td style="font-size:13pt; "><asp:Label runat="server" ID="lblPhotoDate3"></asp:Label></td> 
                        </tr>
                        <tr>
                            <td><asp:Image ID="fignerImage" ImageUrl="" runat="server" Width="120px" Height="150px" /></td>
                           <td><asp:Image ID="picImage" ImageUrl="" runat="server" Width="120px" Height="150px" /></td>
                           <td><asp:Image ID="photoImage1" ImageUrl="" runat="server" Width="170px" Height="130px" /></td> 
                           <td><asp:Image ID="photoImage2" ImageUrl="" runat="server" Width="170px" Height="130px" /></td> 
                           <td><asp:Image ID="photoImage3" ImageUrl="" runat="server" Width="170px" Height="130px" /></td> 
                        </tr>
                    </table>
                    <table width="95%">
                        <tr>
                            <td id='ExamTitle' colspan="6">
                                <%=RailExamWebApp.Common.Class.PrjPub.GetRailNameBao()%>职工培训考试试卷</td>
                        </tr>
                        <tr>
                            <td id='ExamName' colspan="6">
                                考试名称：
                                <asp:Label runat="server" ID="lblTitle"></asp:Label></td>
                        </tr> 
                        <tr>       
                            <td id="ExamInfo" colspan="6">
                                <asp:Label runat="server" ID="lblTitleRight"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" class="ExamStyle">
                            </td>
                        </tr>
                        <tr align="center" id="ExamStudentInfo1">
                            <td style="width: 8%;">
                                单位：</td>
                            <td style="width: 28%;" align="left">
                                <asp:Label runat="server" ID="lblOrg"></asp:Label>
                            </td>
                            <td style="width: 8%;">
                                车间：</td>
                            <td style="width: 28%;" align="left">
                                <asp:Label runat="server" ID="lblWorkShop"></asp:Label>
                            </td>
                            <td style="width: 8%;">
                                职名：</td>
                            <td style="width: 20%;" align="left">
                                <asp:Label runat="server" ID="lblPost"></asp:Label>
                            </td>
                        </tr>
                        <tr id="ExamStudentInfo2">
                            <td style="width: 8%;">
                                姓名：</td>
                            <td style="width: 28%;" align="left">
                                <asp:Label runat="server" ID="lblName"></asp:Label>
                            </td>
                            <td style="width: 8%;">
                                时间：</td>
                            <td style="width: 28%;" align="left">
                                <asp:Label runat="server" ID="lblTime"></asp:Label>
                            </td>
                            <td style="width: 8%;">
                                成绩：</td>
                            <td style="width: 20%;" align="left">
                                <asp:Label runat="server" ID="lblScore"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" class="ExamStyle">
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="mainContent">
                    <% FillPaper(); %>
                </div>
            </div>
        </div>
        <ComponentArt:Callback ID="updateCallback" runat="server" OnCallback="updateCallback_Callback">
        </ComponentArt:Callback>
        <asp:HiddenField runat="server" ID="hfLoginEmployeeID"/>
    </form>
</body>
</html>
