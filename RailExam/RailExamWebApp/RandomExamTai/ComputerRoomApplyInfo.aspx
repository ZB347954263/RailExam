<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ComputerRoomApplyInfo.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.ComputerRoomApplyInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312">
    <title>微机教室信息</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script language="javascript" type="text/javascript">
	
         function UpdApply(orgID)
         {
		        var   cleft;   
                var   ctop;   
                cleft=(screen.availWidth-580)*.5;   
                ctop=(screen.availHeight-250)*.5;        
                var flagUpdate=document.getElementById("HfUpdateRight").value; 
                              
	            if(flagUpdate=="False")
                 {
                    alert("您没有权限使用该操作！");                       
                    return;
                 }            
                
                var ret = window.open('ComputerApplyDetail.aspx?mode=Edit&&id='+orgID,'ComputerApplyDetail',' Width=580px; Height=250px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
		        if(ret == "true")
		        {
			        Form1.Refresh.value = ret;
			        Form1.submit();
			        Form1.Refresh.value = "";
		        }
         }
         function SelApply(orgID)
         {
		        var   cleft;   
                var   ctop;   
                cleft=(screen.availWidth-580)*.5;   
                ctop=(screen.availHeight-250)*.5;        
                var flagUpdate=document.getElementById("HfUpdateRight").value; 
                              
	            if(flagUpdate=="False")
                 {
                    alert("您没有权限使用该操作！");                       
                    return;
                 }            
                
                var ret = window.open('ComputerApplyDetail.aspx?mode=ReadOnly&&id='+orgID,'ComputerApplyDetail',' Width=580px; Height=250px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
		        if(ret == "true")
		        {
			        Form1.Refresh.value = ret;
			        Form1.submit();
			        Form1.Refresh.value = "";
		        }
         }
        function DelApply()
        {
            if(!confirm('您确定要删除此记录吗？'))
            {
                alter("NO");
            }
            
        }


        /*第一种形式 第二种形式 更换显示样式*/
        function setTab(name,cursel,n) {
	        for (i = 1; i <= n; i++) {
		        var menu = document.getElementById(name + i);
		        var con = document.getElementById("con_" + name + "_" + i);
		        menu.className = i == cursel ? "hover" : "";
		        con.style.display = i == cursel ? "block" : "none";
	        }
        }
        

        function SelFind_one()
        {
                window.frames["iframeInfo"].location = "ComputerApplyTab_one.aspx?";
        }
        function SelFind_two()
        {
                window.frames["iframeInfo"].location="ComputerApplyTab_two.aspx?";
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        微机教室管理-微机教室预订</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
            </div>
            <div id="content">
                <%--                <div id="Tab1">
                    <div class="Menubox">
                        <ul>
                            <li id="one1" onclick="setTab('one',1,2)" class="hover">申请信息</li>
                            <li id="one2" onclick="setTab('one',2,2)">联系信息</li>
                        </ul>
                    </div>
                    <div class="Contentbox">
                        <div id="con_one_1" class="hover">
                            <div id="btn1">
                                <img id="Img1" onclick="SelFind_one()" src="../Common/Image/find.gif" alt="" />
                                <img id="Img2" onclick="AddApply();" src="../Common/Image/add.gif" alt="" />
                            </div>
                        </div>
                        <div id="con_one_2" style="display: none">
                            <div id="btn2">
                                <img id="Img3" onclick="SelFind_two();" runat="server" src="../Common/Image/find.gif"
                                    alt="" />
                            </div>
                        </div>
                        <iframe id="iframeInfo" style="width: 100%; height: 98%" frameborder="0" src="ComputerApplyTab_one.aspx"></iframe>
                    </div>
                </div>--%>
                <div style="text-align: left;">
                    <ComponentArt:TabStrip ID="TabStrip1" runat="server" MultiPageId="EmployeeDetailMultiPage">
                        <Tabs>
                            <ComponentArt:TabStripTab Value="0" Text="申请信息" PageViewId="FirstPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="1" Text="待回复信息" PageViewId="SecondPage">
                            </ComponentArt:TabStripTab>
                        </Tabs>
                    </ComponentArt:TabStrip>
                    <ComponentArt:MultiPage ID="EmployeeDetailMultiPage" Width="100%" runat="server"
                        RunningMode="Callback">
                        <ComponentArt:PageView ID="FirstPage">
                            <iframe id="iframeFirst" style="width: 100%;" height="500px" frameborder="0" src="ComputerApplyTab_one.aspx">
                            </iframe>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="SecondPage">
                            <iframe id="iframeSecond" style="width: 100%;" height="500px"  frameborder="0" src="ComputerApplyTab_two.aspx">
                            </iframe>
                        </ComponentArt:PageView>
                    </ComponentArt:MultiPage>
                </div>
            </div>
        </div>
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>
</body>
</html>
