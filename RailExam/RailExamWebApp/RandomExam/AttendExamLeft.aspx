<%@ Page Language="C#" AutoEventWireup="true" Codebehind="AttendExamLeft.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.AttendExamLeft" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考试页面</title>
    <link href="../Online/style/ExamPaper.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript">
		
		function SaveRecord()
		{
			try
			{
				window.frames["ifExamDetailInfo"].frames["ifExamInfo"].SaveRecord();
			}
			catch(e) 
			{
				document.getElementById("btnClose").disabled = false;
			} 
		}

		var size = 12;
		
		function showBig() {
			var inputs = window.frames["ifExamDetailInfo"].frames["ifExamInfo"].document.getElementsByTagName("td");
			
			for(var i=0; i<inputs.length; i++)
			{
				inputs[i].style.fontSize = (size+1)+"pt"; 
			}

			size = size + 1;
		}
		
		function showSmall() {
			var inputs = window.frames["ifExamDetailInfo"].frames["ifExamInfo"].document.getElementsByTagName("td");
			
			for(var i=0; i<inputs.length; i++)
			{
				inputs[i].style.fontSize = (size-1)+"pt"; 
			}

			size = size - 1;
		}
		
		function imageStart()
        {
			if(document.getElementById('cap1')) 
			{
				if(document.getElementById('cap1').camCount >0)
			    {
			        document.getElementById('cap1').start();
                    //setTimeout("paizhao()", 60000);		
			    }
			}
        }
        
        function paizhao()
        {
            var now = new Date();			
			var year = now.getYear();
			var month = now.getMonth();
			var date = now.getDate();
			var day = now.getDay();
			var hour = now.getHours();
			var minute = now.getMinutes();
			var second = now.getSeconds();	
			
			var name = 'test'+ year+month+day+hour+minute+second;
        	var strBase;
            document.getElementById('cap1').cap();
            //document.getElementById('cap1').saveToFile('c://'+name+'.jpg');
        	strBase = document.getElementById('cap1').jpegBase64Data;
            document.getElementById('cap1').clear();

        	//return strBase;

        	var search = window.location.search;
        	var examid = search.substring(search.indexOf('id=') + 3, search.indexOf("&employeeID="));
        	var employeeid = search.substring(search.indexOf("&employeeID=")+12);	        	
        	imageCallback.callback(examid,employeeid,strBase);
        	
        	 imageStart();
        }
    </script>

</head>
<body onload="imageStart()">
    <form id="form1" runat="server">
        <div id="page">
            <div align="left">
                <table>
                    <tr>
                        <td>
                            <iframe id="ifExamDetailInfo" src="" name="ifExamDetailInfo" frameborder="0" scrolling="no"
                                width="100%" height="100%"></iframe>
                        </td>
                        <td style="width: 170px; vertical-align: top; text-align: center;">
                            <table>
                                <tr>
                                    <td style="border: solid 1px #E0E0E0;padding: 5px;white-space:nowrap;text-align:left; width: 170px; text-align: center">
                                        <asp:Image ID="myImagePhoto" ImageUrl="" runat="server" Width="120px" Height="150px" /></td>
                                </tr>
                                <tr>
                                    <td>
                                        <object classid="clsid:34681DB3-58E6-4512-86F2-9477F1A9F3D8" id="cap1" codebase="../ImageCapOnWeb.cab#version=2,0,0,0"
                                            width="170px" height="130px" >
                                            <param name="Visible" value="0" />
                                            <param name="AutoScroll" value="1" />
                                            <param name="AutoSize" value="0" />
                                            <param name="AxBorderStyle" value="1" />
                                            <param name="Caption" value="WebVideoCap" />
                                            <param name="Color" value="4278190095" />
                                            <param name="Font" value="宋体" />
                                            <param name="KeyPreview" value="0" />
                                            <param name="PixelsPerInch" value="96" />
                                            <param name="PrintScale" value="1" />
                                            <param name="Scaled" value="-1" />
                                            <param name="DropTarget" value="0" />
                                            <param name="HelpFile" value="" />
                                            <param name="PopupMode" value="0" />
                                            <param name="ScreenSnap" value="0" />
                                            <param name="SnapBuffer" value="10" />
                                            <param name="DockSite" value="0" />
                                            <param name="DoubleBuffered" value="0" />
                                            <param name="ParentDoubleBuffered" value="0" />
                                            <param name="UseDockManager" value="0" />
                                            <param name="Enabled" value="-1" />
                                            <param name="AlignWithMargins" value="0" />
                                            <param name="ParentCustomHint" value="-1" />
                                            <param name="licenseMode" value="2"/>
                                            <param name="key1" value="puOGfx5IZA0dkRea17fs5PS7kFeRO46UFMXd30kDnF541UPvPG5j4kiAFOQ2FqGyTUUimZB/Hn2+L11NwRX5Ei7SAnU="/>
                                            <param name="key2" value="bqjAfWVIL3mB037XRiKCK5DTZyePjcbVSEVm+VwkbaCBE+7yllMgD0hOzgOVrPA/zlzIPYrdCJjH95gQVLj7SjIdD7vBUnnUi02rEZfF8YLbRuyP8EfsZWqJd+8TvkLBZTXoPTPe7YyY2uVXaRUdzEF/Z/5G2n3GCaQITg=="/> 
                                        </object>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div style="text-align: center; font-size: 14pt; color: #1182ea;">
                                字体大小：<a href="#" onclick="showSmall()" style="font-size: 14pt; cursor: hand; color: #1182ea;">小</a>&nbsp;&nbsp;<a
                                    href="#" onclick="showBig()" style="font-size: 14pt; cursor: hand; color: #1182ea;">大</a>
                            </div>
                            <br />
                            <iframe id="ifExamNavigation" src="" name="ifExamNavigation" frameborder="0" scrolling="auto"
                                width="100%" height="100%"></iframe>
                            <div class='ExamButton'>
                                <input id='btnClose' class='button' name='btnSave' type='button' value='提交答卷' onclick='SaveRecord()' />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
<%--   <div style="display: none">
            <object id="UserControl1" classid="CLSID:F09D6002-3AD0-42E6-8E3D-D02149CD8F07" codebase="../Hook2012.CAB#version=1,0,0,0">
            </object>
        </div>--%>
        <ComponentArt:CallBack ID="imageCallback" runat="server" PostState="true"
            OnCallback="imageCallback_Callback">   </ComponentArt:CallBack>
    </form>

    <script type="text/javascript">
           var search = window.location.search;
           window.frames["ifExamDetailInfo"].location = "AttendExamTitleNew.aspx"+search;
           document.getElementById("ifExamDetailInfo").style.height =(document.documentElement.clientHeight-30)+'px';
           document.getElementById("ifExamDetailInfo").style.width =(document.documentElement.clientWidth-190)+'px';
        
           window.frames["ifExamNavigation"].location = "AttendExamNavigation.aspx"+search;
           document.getElementById("ifExamNavigation").style.width ='170px';
           
           if(document.getElementById('cap1'))
           {
           	    document.all.cap1.SwitchWatchOnly(); //切换到只显示摄像头画面形式，隐藏编辑按钮等图标.
           	
           	   if(document.getElementById('cap1').camCount == 0)
			    {
					document.getElementById("cap1").style.display = "none";
           	   	   document.getElementById("ifExamNavigation").style.height =(document.documentElement.clientHeight-270)+'px';
			    }
           	   else {
           	   	   document.getElementById("ifExamNavigation").style.height =(document.documentElement.clientHeight-400)+'px';
           	   }
           }
    </script>

</body>
</html>
