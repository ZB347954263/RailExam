<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Index.aspx.cs" Inherits="RailExamWebApp.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <title>武汉铁路局在线培训考试系统</title>
    <link href="Online/style/css.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function MM_swapImgRestore() { //v3.0
          var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
        }

        function MM_preloadImages() { //v3.0
          var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
            var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
            if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
        }

        function MM_findObj(n, d) { //v4.01
          var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
            d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
          if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
          for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
          if(!x && d.getElementById) x=d.getElementById(n); return x;
        }

        function MM_swapImage() { //v3.0
          var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
           if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
        }

        function AttendExam(ExamId,PaperId)      
        {                  
            var w=window.open("exam/ExamKS.aspx?ExamId="+ExamId+"&PaperId="+PaperId,"ExamKS","fullscreen=yes,toolbar=no,scrollbars=yes");	
            w.focus();	
        }   
        
        function OpenBook()
        {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-900)*.5;   
            ctop=(screen.availHeight-600)*.5;              
            var re= window.open("Online/Study/ReadBook.aspx","ReadBook"," Width=900px; Height=600px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);		
            re.focus();  
        }
        
        function OpenCourse()
        {
             var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-900)*.5;   
            ctop=(screen.availHeight-600)*.5;             
            var re= window.open("Online/Study/ReadCourse.aspx","ReadCourse"," Width=900px; Height=600px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);		
            re.focus();  
        }
        
        function ExamNow()
        {             
            document.getElementById("divExamNow").style.display = ""; 
            document.getElementById("divExamComing").style.display = "none"; 
            document.getElementById("divExamHistory").style.display = "none"; 
            document.getElementById("divExamResult").style.display = "none";
            document.getElementById("Image50").style.display = "";
            document.getElementById("Image60").style.display = "none";
            document.getElementById("Image51").style.display = "";
            document.getElementById("Image61").style.display = "none";
            document.getElementById("Image52").style.display = "";
            document.getElementById("Image62").style.display = "none";
            document.getElementById("Image53").style.display = "";
            document.getElementById("Image63").style.display = "none";
        }
        
        function ExamComing()
        {         
            document.getElementById("divExamNow").style.display = "none"; 
            document.getElementById("divExamComing").style.display = ""; 
            document.getElementById("divExamHistory").style.display = "none"; 
            document.getElementById("divExamResult").style.display = "none"; 
            document.getElementById("Image50").style.display = "none";
            document.getElementById("Image60").style.display = "";
            document.getElementById("Image51").style.display = "none";
            document.getElementById("Image61").style.display = "";
            document.getElementById("Image52").style.display = "";
            document.getElementById("Image62").style.display = "none";
            document.getElementById("Image53").style.display = "";
            document.getElementById("Image63").style.display = "none";
        }
        
        function ExamHistory()
        {    
            document.getElementById("divExamNow").style.display = "none"; 
            document.getElementById("divExamComing").style.display = "none"; 
            document.getElementById("divExamHistory").style.display = ""; 
            document.getElementById("divExamResult").style.display = "none"; 
            document.getElementById("Image50").style.display = "none";
            document.getElementById("Image60").style.display = "";
            document.getElementById("Image51").style.display = "";
            document.getElementById("Image61").style.display = "none";
            document.getElementById("Image52").style.display = "none";
            document.getElementById("Image62").style.display = "";
            document.getElementById("Image53").style.display = "";
            document.getElementById("Image63").style.display = "none";
        }   

        function ExamResult()
        {    
            document.getElementById("divExamNow").style.display = "none"; 
            document.getElementById("divExamComing").style.display = "none"; 
            document.getElementById("divExamHistory").style.display = "none"; 
            document.getElementById("divExamResult").style.display = ""; 
            document.getElementById("Image50").style.display = "none";
            document.getElementById("Image60").style.display = "";
            document.getElementById("Image50").style.display = "none";
            document.getElementById("Image60").style.display = "";
            document.getElementById("Image51").style.display = "";
            document.getElementById("Image61").style.display = "none";
            document.getElementById("Image52").style.display = "";
            document.getElementById("Image62").style.display = "none";
            document.getElementById("Image53").style.display = "none";
            document.getElementById("Image63").style.display = "";
        }   
   
        
        function btnViewExamResult(examResultId)
        {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;             
            var re= window.open("Online/Exam/ExamResult.aspx?id=" + examResultId,
                "ExamResult"," Width=800; Height=600,status=false,left="+cleft+",top="+ctop+",resizable=yes,scrollbars",true);
            re.focus();
        }
       
        function StudyByKnowledge()
        {
            var   cleft;   
            var   ctop;   
           cleft=(screen.availWidth-800)*.5;   
           ctop=(screen.availHeight-600)*.5;   
            var re = window.open("Online/Study/StudyByKnowledge.aspx","StudyByKnowledge",' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);		
            re.focus();
        } 
       
        function SelectTrainType()
        {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;           
            var re = window.open("Train/TrainTypeEmployeeSelect.aspx","TrainTypeEmployeeSelect",' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);		
            re.focus();
        } 
    </script>
</head>
<body onload="MM_preloadImages('Online/image/dqkc01.jpg','Online/image/kscj01.jpg')">
    <form id="form1" runat="server">
        <table width="1004" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <object codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"
                        width="1004" height="140" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000">
                        <param name="_cx" value="17595">
                        <param name="_cy" value="1720">
                        <param name="FlashVars" value="">
                        <param name="Movie" value="Online/Image/head.swf">
                        <param name="Src" value="Online/Image/head.swf">
                        <param name="WMode" value="Transparent">
                        <param name="Play" value="-1">
                        <param name="Loop" value="-1">
                        <param name="Quality" value="High">
                        <param name="SAlign" value="">
                        <param name="Menu" value="-1">
                        <param name="Base" value="">
                        <param name="AllowScriptAccess" value="always">
                        <param name="Scale" value="ShowAll">
                        <param name="DeviceFont" value="0">
                        <param name="EmbedMovie" value="0">
                        <param name="BGColor" value="">
                        <param name="SWRemote" value="">
                        <param name="MovieData" value="">
                        <param name="SeamlessTabbing" value="1">
                        <param name="Profile" value="0">
                        <param name="ProfileAddress" value="">
                        <param name="ProfilePort" value="0">
                        <embed src="Online/Image/head.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer"
                            type="application/x-shockwave-flash" width="1004" height="140"> </embed>
                    </object>
                </td>
            </tr>
        </table>
        <table width="1004" height="30" border="0" cellpadding="0" cellspacing="0" align="center">
            <tr>
                <td align="center" bgcolor="#1082E9" class="style16">
                    <a href="#" target="_blank" class="white16">首&nbsp;&nbsp;页</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="Online/Notice/BulletinList.aspx" target="_blank" class="white16">信息公告</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="Online/Notice/NoticeList.aspx" target="_blank" class="white16">通&nbsp;&nbsp;知</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href='Online/AccountManage.aspx' target="_blank" class="white16">帐户管理</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="Online/Study/StudyList.aspx" target="_blank" class="white16">在线学习</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="Online/ExamList.aspx" target="_blank" class="white16">在线考试</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="<%=BBSUrl %>" target="_blank" class="white16">学习论坛</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="#" class="white16" onclick="window.open('Default.aspx','','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no')">后台管理</a>
                </td>
            </tr>
        </table>
        <table width="1004" border="0" cellspacing="0" cellpadding="0" align="center">
            <tr>
                <td height="6">
                </td>
            </tr>
        </table>
        <table width="1004" border="0" cellspacing="0" cellpadding="0" align="center">
            <tr>
                <td valign="top" style="width: 210px">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="12%" bgcolor="#1082E9">
                                <img src="Online/image/dot02.jpg" width="26" height="21" /></td>
                            <td align="left" bgcolor="#1082E9" class="style18">
                                用户登录</td>
                        </tr>
                        <tr>
                            <td height="200" colspan="2" align="center" class="kuang">
                                <table width="90%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td height="6">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="30%" height="32" align="left">
                                            <asp:Label ID="lblUserName" runat="server">用户名：</asp:Label></td>
                                        <td width="70%" align="left">
                                            <asp:TextBox ID="txtUserID" runat="server" CssClass="textbox" Width="60%"></asp:TextBox>
                                            <asp:Label ID="lblOrg" runat="server" Visible="False">Label</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="32" align="left">
                                            <asp:Label ID="lblPassword" runat="server">密&nbsp;&nbsp;码：</asp:Label></td>
                                        <td height="30" align="left">
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="textbox" TextMode="Password"
                                                Width="60%"></asp:TextBox>
                                            <asp:Label ID="lblEmployeeName" runat="server" Visible="False">Label</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="32" align="left">
                                            <asp:Label ID="lblWorkNo1" runat="server" Visible="false">工&nbsp;&nbsp;号：</asp:Label></td>
                                        <td height="30" align="left">
                                            <asp:Label ID="lblWorkNo2" runat="server" Visible="False">Label</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="32" align="left">
                                            <asp:Label ID="lblPostName" runat="server" Visible="False">职&nbsp;&nbsp;名：</asp:Label></td>
                                        <td height="30" align="left">
                                            <asp:Label ID="lblFullName" runat="server" Visible="False">Label</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="38" colspan="2" align="center" valign="bottom">
                                            <table width="100%" height="30" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="left">
                                                        <asp:ImageButton ID="ImageButtonLogin" runat="server" ImageUrl="Online/image/login.gif"
                                                            OnClick="ImageButtonLogin_Click"></asp:ImageButton>
                                                        <asp:ImageButton ID="ImageButtonLogout" runat="server" ImageUrl="Online/image/logout.gif"
                                                            OnClick="ImageButtonLogout_Click" Visible="false"></asp:ImageButton></td>
                                                    <td align="right">
                                                        <a href='Online/AccountManage.aspx' target="_blank">
                                                            <img src="Online/image/account.gif" width="81" height="23" border="0" /></a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="73" colspan="2" background="Online/image/login_bg.gif">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="5">
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="12%" bgcolor="#1082E9">
                                <img src="Online/image/dot02.jpg" width="26" height="21" /></td>
                            <td width="62%" align="left" valign="baseline" bgcolor="#1082E9">
                                <span class="style18">信息公告</span></td>
                            <td width="26%" align="center" valign="baseline" bgcolor="#1082E9">
                                <a href="Online/Notice/BulletinList.aspx" target="_blank" class="white12">更多&gt;&gt;</a></td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left" valign="top" class="kuang" style="height: 380px">
                                <div>
                                    <asp:ObjectDataSource ID="odsBulletin" runat="server" SelectMethod="GetBulletins"
                                        TypeName="RailExam.BLL.BulletinBLL">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="20" Name="nNum" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="BulletinID" AutoGenerateColumns="False"
                                        DataSourceID="odsBulletin" ShowHeader="False" PageSize="20" BorderWidth="0">
                                        <Columns>
                                            <asp:TemplateField HeaderText="标题">
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    ·
                                                    <asp:HyperLink ID="hlTitle" Text='<%# Eval("Title").ToString().Length <= 14 ? Eval("Title") : Eval("Title").ToString().Substring(0, 14)+"..." %>'
                                                        ToolTip='<%# Eval("Title") %>' runat="server" NavigateUrl='<%# "Online/Notice/BulletinDetail.aspx?id=" + Eval("BulletinID") %>'
                                                        Target="_blank"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="center" valign="top">
                    <table width="567" border="0" cellpadding="0" cellspacing="0" class="kuang">
                        <tr>
                            <td height="28" background="Online/image/hui.gif">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="34">
                                            <img src="Online/image/d0t01.jpg" width="34" height="28" /></td>
                                        <td width="318" align="left" class="style17">
                                            <span class="blue14">在线学习</span></td>
                                        <td width="106" align="left">
                                               <asp:ImageButton ID="btnStudyBook"  ImageUrl="Online/image/xxjc.gif" runat="server"  OnClientClick="OpenBook()" 
                                                width="87" height="20" /></td>
                                        <td width="107" align="left">
                                               <asp:ImageButton ID="btnStudyCourse" ImageUrl="Online/image/xxkj.gif" runat="server" OnClientClick="OpenCourse()" 
                                                width="87" height="20" /></td>
                                         <td >
                                             <asp:ImageButton ID="btnStudy" Visible="false" ImageUrl="Online/image/StudyByKnowledge.gif"  runat="server" OnClientClick="StudyByKnowledge()" 
                                        width="124" height="20" />&nbsp;&nbsp;
                                         </td  >  
                                        <td >
                                             <asp:ImageButton ID="btnSelectTrainType" Visible="false" ImageUrl="Online/image/StudyByTrainType.gif"  runat="server" OnClientClick="SelectTrainType()" 
                                        width="124" height="20" />&nbsp;&nbsp;
                                         </td>  
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <img src="Online/image/l01.jpg" width="563" height="5" /></td>
                        </tr>
                        <tr>
                            <td height="365" align="center" valign="top">
                                <iframe src="Online/Study/StudyCourse.aspx" id="StudyCourse" frameborder="0" width="557"
                                    height="100%" scrolling="no"></iframe>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="6">
                            </td>
                        </tr>
                    </table>
                    <table width="567" border="0" cellpadding="0" cellspacing="0" class="kuang">
                        <tr>
                            <td height="28" background="Online/image/hui.jpg">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="34">
                                            <img src="Online/image/d0t01.jpg" width="34" height="28" /></td>
                                        <td width="68" align="left" class="style17">
                                            <span class="blue14">在线考试</span></td>
                                        <td width="463" align="right" valign="bottom">
                                            <table width="56%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="25%" align="left">
                                                        <img src="Online/image/currentexam.gif" name="Image50" width="62" height="22" border="0"
                                                            id="Image50" />
                                                        <img src="Online/image/currentexam01.gif" name="Image60" width="62" height="22" border="0"
                                                            onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image50','','Online/image/currentexam.gif',1)"
                                                            id="Image60" onclick="ExamNow()" style="display: none; cursor: hand;" />
                                                    </td>
                                                    <td width="25%" align="left">
                                                        <img src="Online/image/examcoming.gif" name="Image51" width="62" height="22" border="0"
                                                            onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image51','','Online/image/examcoming01.gif',1)"
                                                            id="Image51" onclick="ExamComing()" style="cursor: hand;" />
                                                        <img src="Online/image/examcoming01.gif" name="Image61" width="62" height="22" border="0"
                                                            id="Image61" style="display: none;" />
                                                    </td>
                                                    <td width="25%" align="left">
                                                        <img src="Online/image/examhis.gif" name="Image52" width="62" height="22" border="0"
                                                            onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image52','','Online/image/examhis01.gif',1)"
                                                            id="Image52" onclick="ExamHistory()" style="cursor: hand;" />
                                                        <img src="Online/image/examhis01.gif" name="Image62" width="62" height="22" border="0"
                                                            id="Image62" style="display: none;" />
                                                    </td>
                                                    <td width="25%" align="left">
                                                        <img src="Online/image/examresult.gif" name="Image53" width="62" height="22" border="0"
                                                            onmouseout="MM_swapImgRestore()" onmouseover="MM_swapImage('Image53','','Online/image/examresult01.gif',1)"
                                                            id="Image53" onclick="ExamResult()" style="cursor: hand;" />
                                                        <img src="Online/image/examresult01.gif" name="Image63" width="62" height="22" border="0"
                                                            id="Image63" style="display: none;" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="238" background="Online/image/tbg02.jpg" align="center" valign="top"
                                style="padding: 4px;">
                                <div id="divExamNow" style="text-align: left; height: 238px; width: 100%;">
                                    <asp:GridView ID="GridView4" runat="server" Width="100%" DataKeyNames="ExamId" AutoGenerateColumns="False"
                                        ShowHeader="true" BorderWidth="0px" AlternatingRowStyle-Wrap="False" AlternatingRowStyle-Width="30%"
                                        OnRowDataBound="GridView4_RowDataBound" HeaderStyle-CssClass="gridhead">
                                        <Columns>
                                            <asp:TemplateField HeaderText="考试名称">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBoxSubjectName" runat="server" Text='<%# Bind("ExamName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="有效时间" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="labelT1" runat="server" Text='<%# Bind("BeginTime","{0:yyyy-MM-dd HH:mm}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="有效时间" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="labelT2" runat="server" Text='<%# Bind("EndTime","{0:yyyy-MM-dd HH:mm}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="有效时间">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="labelT3" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="总分数">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBoxTotalScore" runat="server" Text='<%# Bind("ConvertTotalScore") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ExamId" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="labExamId" runat="server" Text='<%# Bind("ExamId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PaperId" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="labPaperId" runat="server" Text='<%# Bind("PaperId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="答题时间">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBox2" runat="server" Text='<%# Bind("BeginTime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="操作">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="link1" Text="参加考试"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle Width="30%" Wrap="False" />
                                    </asp:GridView>
                                </div>
                                <div id="divExamComing" style="text-align: left; height: 238px; width: 100%; display: none;">
                                    <asp:GridView ID="GridView3" runat="server" Width="100%" DataKeyNames="ExamId" AutoGenerateColumns="False"
                                        ShowHeader="true" BorderWidth="0px" AlternatingRowStyle-Wrap="False" HeaderStyle-CssClass="gridhead"
                                        AlternatingRowStyle-Width="30%" OnRowDataBound="GridView3_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="考试名称">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBoxSubjectName" runat="server" Text='<%# Bind("ExamName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="有效时间" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="labelT1" runat="server" Text='<%# Bind("BeginTime","{0:yyyy-MM-dd HH:mm}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="有效时间" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="labelT2" runat="server" Text='<%# Bind("EndTime","{0:yyyy-MM-dd HH:mm}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="有效时间">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="labelT3" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="总分数">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBoxTotalScore" runat="server" Text='<%# Bind("ConvertTotalScore") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ExamId" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="labExamId" runat="server" Text='<%# Bind("ExamId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PaperId" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="labPaperId" runat="server" Text='<%# Bind("PaperId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="答题时间">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBox2" runat="server" Text='<%# Bind("BeginTime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle Width="30%" Wrap="False" />
                                    </asp:GridView>
                                </div>
                                <div id="divExamHistory" style="text-align: left; height: 238px; width: 100%; display: none;">
                                    <asp:GridView ID="GridView5" runat="server" Width="100%" DataKeyNames="ExamId" AutoGenerateColumns="False"
                                        ShowHeader="true" BorderWidth="0px" AlternatingRowStyle-Wrap="False" HeaderStyle-CssClass="gridhead"
                                        AlternatingRowStyle-Width="30%" OnRowDataBound="GridView5_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="考试名称">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBoxSubjectName" runat="server" Text='<%# Bind("ExamName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="有效时间" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="labelT1" runat="server" Text='<%# Bind("BeginTime","{0:yyyy-MM-dd HH:mm}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="有效时间" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="labelT2" runat="server" Text='<%# Bind("EndTime","{0:yyyy-MM-dd HH:mm}")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="有效时间">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="labelT3" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="总分数">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBoxTotalScore" runat="server" Text='<%# Bind("ConvertTotalScore") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ExamId" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="labExamId" runat="server" Text='<%# Bind("ExamId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PaperId" Visible="False">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="labPaperId" runat="server" Text='<%# Bind("PaperId") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="答题时间">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="TextBox2" runat="server" Text='<%# Bind("BeginTime") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle Width="30%" Wrap="False" />
                                    </asp:GridView>
                                </div>
                                <div id="divExamResult" style="text-align: left; height: 238px; width: 100%; display: none;">
                                    <asp:GridView ID="gvExamResult" runat="server" DataSourceID="odsExamResult" Width="100%"
                                        DataKeyNames="ExamId" AutoGenerateColumns="False" ShowHeader="true" BorderWidth="0px"
                                        AlternatingRowStyle-Wrap="False" AlternatingRowStyle-Width="30%" HeaderStyle-CssClass="gridhead">
                                        <Columns>
                                            <asp:TemplateField HeaderText="考试名称">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExamName" runat="server" Text='<%# Eval("ExamName") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="考试时间">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblExamBeginEndTime" runat="server" Text='<%# Eval("ExamBeginTime") + "/" + Eval("ExamEndTime") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="分数">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblScore" runat="server" Text='<%# Eval("Score")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="正确率(%)">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCorrectRate" runat="server" Text='<%# Eval("CorrectRate") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="是否通过考试">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIsPass" runat="server" Text='<%# (bool)Eval("IsPass") ? "是" : "否" %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="操作">
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="false" />
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <img onclick="btnViewExamResult('<%# Eval("ExamResultId") %>');" alt="查看试卷" src="Common/Image/edit_col_see.gif"
                                                        border="0" width="13" height="13" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <AlternatingRowStyle Width="30%" Wrap="False" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="210" valign="top">
                    <table width="100%" border="0" cellpadding="4" cellspacing="0" class="kuang">
                        <tr>
                            <td height="30" align="left" valign="top">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" style="margin-bottom: 4px;">
                                    <tr>
                                        <td width="12%" bgcolor="#1082E9">
                                            <img src="Online/image/dot02.jpg" width="26" height="21" /></td>
                                        <td width="62%" align="left" valign="baseline" bgcolor="#1082E9">
                                            <span class="style18">通 知</span></td>
                                        <td width="26%" align="center" valign="baseline" bgcolor="#1082E9">
                                            <a href="Online/Notice/NoticeList.aspx" target="_blank" class="white12">更多&gt;&gt;</a></td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                    <tr>
                                        <td align="center" bgcolor="#F6F6F6" style="height: 241px">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td height="217" align="left" valign="top">
                                                        <div>
                                                            <asp:ObjectDataSource ID="odsNotice" runat="server" SelectMethod="GetNotices" TypeName="RailExam.BLL.NoticeBLL">
                                                                <SelectParameters>
                                                                    <asp:Parameter DefaultValue="14" Name="nNum" Type="Int32" />
                                                                     <asp:SessionParameter ConvertEmptyStringToNull="False" DefaultValue="-1" Name="examineeId"
                        SessionField="StudentID" Type="String" />
                         <asp:SessionParameter ConvertEmptyStringToNull="False" DefaultValue="-1" Name="OrgId" SessionField="StudentOrdID" Type="String" />
                                                                </SelectParameters>
                                                            </asp:ObjectDataSource>
                                                            <asp:GridView ID="GridView2" runat="server" DataKeyNames="NoticeID" AutoGenerateColumns="False"
                                                                DataSourceID="odsNotice" ShowHeader="False" PageSize="14" BorderWidth="0px">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="标题">
                                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                        <ItemTemplate>
                                                                            ·
                                                                            <asp:HyperLink ID="hlTitle" Text='<%# Eval("Title").ToString().Length <= 12 ? Eval("Title") : Eval("Title").ToString().Substring(0, 12)+"..." %>'
                                                                                ToolTip='<%# Eval("Title") %>' runat="server" NavigateUrl='<%# "Online/Notice/NoticeDetail.aspx?id=" + Eval("NoticeID") %>'
                                                                                Target="_blank"></asp:HyperLink>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="5">
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellpadding="4" cellspacing="0" class="kuang">
                        <tr>
                            <td align="left" valign="top" style="height: 30px">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" style="margin-bottom: 4px;">
                                    <tr>
                                        <td width="3" align="left">
                                            <img src="Online/image/left.gif" width="3" height="24" /></td>
                                        <td width="30" align="center" background="Online/image/midlle.gif">
                                            <img src="Online/image/tb.gif" width="16" height="16" /></td>
                                        <td width="121" align="left" valign="baseline" background="Online/image/midlle.gif">
                                            <span class="style18">论坛热贴</span></td>
                                        <td width="43" align="center" valign="baseline" background="Online/image/midlle.gif">
                                            <a href="Online/main.html" target="_blank" class="white12">更多&gt;&gt;</a></td>
                                        <td width="3" align="right">
                                            <img src="Online/image/right.gif" width="3" height="24" /></td>
                                    </tr>
                                </table>
                                <table width="96%" border="0" cellspacing="0" cellpadding="5">
                                    <tr>
                                        <td align="center" bgcolor="#F6F6F6" style="height: 341px; vertical-align:top;">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td align="left" valign="top">
                                                        <script type="text/javascript" src="/dxBBs/Bbs_Quote.aspx?BoardID=0&Num=20&Length=13&Type=2&IsUser=0&IsTime=0&Target=_blank&BbsPath=/StudyBBS&List=·">
                                                        </script>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
            <tr>
                <td height="8">
                </td>
            </tr>
            <tr>
                <td height="8" bgcolor="#E4E4E4">
                </td>
            </tr>
            <tr>
                <td height="50" align="center">
                    Powered by：MyPower Ver1.00 站长：DSunSoft 页面执行时间：625.00毫秒<br />
                    版权所有 Copyright &copy 2006 武汉铁路局在线培训考试系统</td>
            </tr>
        </table>
        <div>
            <asp:ObjectDataSource ID="odsExamResult" runat="server" SelectMethod="GetExamResults"
                TypeName="RailExam.BLL.ExamResultBLL" DataObjectTypeName="RailExam.BLL.ExamResult">
                <SelectParameters>
                    <asp:SessionParameter ConvertEmptyStringToNull="False" DefaultValue="-1" Name="examineeId"
                        SessionField="StudentID" Type="String" />
                    <asp:Parameter ConvertEmptyStringToNull="false" DefaultValue="30" Name="count" Type="int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </form>
</body>
</html>
