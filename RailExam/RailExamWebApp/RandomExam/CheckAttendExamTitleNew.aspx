<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckAttendExamTitleNew.aspx.cs" Inherits="RailExamWebApp.RandomExam.CheckAttendExamTitleNew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
  <title>����ҳ��</title>
    <link href="../Online/style/ExamPaper.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script src="../Common/JS/JSHelper.js" type="text/javascript"></script>

    <script type="text/javascript">
        function document.oncontextmenu()
        {
           event.returnValue = false;
        }
        
        function window.onhelp()
        {
            return false
        }
        
		
		function Close()
		{
            top.window.close();
		}
		       
		function getFinishedItemsCount()
		{
		    var count = 0;
		    for(var i=0; i<answers.length; i++)
		    {
		        if(answers[i] && answers[i].length > 0)
		        {
		            count++;
		        }
		    }
		    
		    //alert(count);
		    return count;
		}
		
		function showTime()
		{
		    var  examtime=document.getElementById("HfExamTime").value;//Exam Time 	    
           
           document.getElementById("HfExamTime").value=examtime-1;
			/*var now = new Date();			
			var year = now.getYear();
			var month = now.getMonth() + 1;
			var date = now.getDate();
			var day = now.getDay();
			var hour = now.getHours();
			var minute = now.getMinutes();
			var second = now.getSeconds();	
         
			if(minute < 10)	minute = "0" + minute;
			if(second < 10)	second = "0" + second;
			
			switch(day)
			{
				case 0: day = "��";break;
				case 1: day = "һ";break;
				case 2: day = "��";break;
				case 3: day = "��";break;
				case 4: day = "��";break;
				case 5: day = "��";break;
				case 6: day = "��";break;
			}
			
			nowtime.innerHTML =  year + "��" + month + "��" + date + "�� ����" + day + " "+(hour<12?"����":"����") + hour + ":" + minute + ":" + second;
			*/	
			var difftime = Math.floor(examtime);
			 
			if(difftime < 0)
			{
    //			 hastime.innerHTML= "����ʱ���ѵ������ύ�Ծ�!";   
    //			 answers = [];
    //		     var ak = getAnswers();
    //		     form1.strreturnAnswer.value = ak;
    //          form1.submit();           
			}
			else			
			{			
                var diff = "����ʱ��"       
                var hours = Math.floor(difftime%(60*60*24)/3600);
                var minutes = Math.floor((difftime%(60*60*24)-3600*hours)/60);
                var second1 = difftime-3600*hours-60*minutes;
                diff += (hours>=10?"":"0") + hours + "Сʱ";
                diff += (minutes>=10?"":"0") + minutes + "��";
                diff += (second1>=10?"":"0") + second1 + "��";    
                hastime.innerHTML= diff;     
               
//               if(hours==0 && minutes==5 && second1==0)
//               {
//        	        var ret=showCommonDialog("/RailExamBao/RandomExam/ExamMessage.aspx",'dialogWidth:500px;dialogHeight:200px;');
//               }    
             }         				
			 setTimeout("showTime()", 1000);				
		}
	
    </script>

</head>
<body onload="showTime()">
    <form id="form1" runat="server">
        <div>
            <table width="100%">
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class='ExamTitle' colspan="2">
                        <%=RailExamWebApp.Common.Class.PrjPub.GetRailNameBao()%>ְ����ѵ�����Ծ�</td>
                </tr>
                <tr>
                    <td id="ExamName">
                        �������ƣ�
                        <asp:Label runat="server" ID="lblTitle"></asp:Label>
                    </td>
                    <td id="ExamInfo">
                        <asp:Label runat="server" ID="lblTitleRight"></asp:Label>
                    </td>
                </tr>
                <tr align="left">
                    <td colspan="2">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td class="ExamStyle" colspan="6">
                                </td>
                            </tr>
                            <tr class="StudentTable">
                                <td class="StudentTitleInfo">
                                    ��&nbsp;&nbsp;&nbsp;&nbsp;λ��</td>
                                <td class="StudentInfo" style="width: 30%; ">
                                    <asp:Label runat="server" ID="lblOrgName"></asp:Label></td>
                                <td class="StudentTitleInfo">
                                    ��&nbsp;&nbsp;&nbsp;&nbsp;����</td>
                                <td class="StudentInfo" style="width: 18%;">
                                    <asp:Label runat="server" ID="lblName"></asp:Label>
                                </td>
                                <td class="StudentTitleInfo">
                                    ��&nbsp;&nbsp;&nbsp;&nbsp;��</td>
                                <td class="StudentInfo" style="width: 18%;">
                                    <asp:Label runat="server" ID="lblSex"></asp:Label></td>
                            </tr>
                            <tr class="StudentTable">
                                <td class="StudentTitleInfo">
                                    ��λ֤�ţ�</td>
                                <td class="StudentInfo" style="width: 30%;">
                                    <asp:Label runat="server" ID="lblWorkNo"></asp:Label></td>
                                <td class="StudentTitleInfo">
                                    ���֤�ţ�</td>
                                <td class="StudentInfo" style="width: 18%;">
                                    <asp:Label runat="server" ID="lblIDCard"></asp:Label>
                                </td>
                                <td class="StudentTitleInfo">
                                    ְ&nbsp;&nbsp;&nbsp;&nbsp;����</td>
                                <td class="StudentInfo" style="width: 18%;">
                                    <asp:Label runat="server" ID="lblPost"></asp:Label></td>
                            </tr>
                            <tr class="StudentTable">
                                <td colspan="4" class="NowTimeInfo">
                                  <ComponentArt:CallBack OnCallback="CallBack1_Callback" ID="CallBack1" RefreshInterval="1000"
                                        runat="server">
                                        <Content>
                                            <asp:Label ID="lblServerDateTime" runat="server" />
                                        </Content>
                                    </ComponentArt:CallBack>
                                </td>
                                <td colspan="2">
                                    <div class="TimeInfo" id="hastime">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div id="rightContentWithNoHeight">
                            <iframe id="ifExamInfo" name="ifExamInfo" frameborder="0" scrolling="yes" width="100%">
                            </iframe>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hfPaperItemsCount" runat="server" />
        <asp:HiddenField ID="HiddenFieldMaxCount" runat="server" />
        <asp:HiddenField ID="HiddenFieldExamTime" runat="server" />
        <asp:HiddenField ID="HfExamTime" runat="server" />
        <input id="strreturnAnswer" type="hidden" name="strreturnAnswer" />
    </form>

    <script type="text/javascript">
    document.getElementById("rightContentWithNoHeight").height = 0.9*screen.availHeight-110;
    document.getElementById("ifExamInfo").height = 0.9*screen.availHeight-110; 
     var search=window.location.search;
     window.frames["ifExamInfo"].location = "CheckAttendExamNew.aspx"+search; 
    </script>

</body>
