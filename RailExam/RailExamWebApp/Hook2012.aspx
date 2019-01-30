<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Hook2012.aspx.cs" Inherits="RailExamWebApp.Hook2012" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script type="text/javascript">
        function imageStart()
        {
			document.getElementById('cap1').start();
        	//show();
        }

        var i = 1;
        function show() {
        	i = i + 1;
           setTimeout("show()", 1000);		

        	alert(i);
        	if(i==5) {
        		window.close();
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
			//alert(name);

            document.getElementById('cap1').cap();
            document.getElementById('cap1').saveToFile('c://'+name+'.jpg');
        	//alert(document.getElementById('cap1').jpegBase64Data);
        	imageCallback.callback(document.getElementById('cap1').jpegBase64Data);
            document.getElementById('cap1').clear();
                   	
            imageStart();
        }
        
    </script>

</head>
<body onload="imageStart();">
    <form id="form1" runat="server">
        <div>
            <object classid="clsid:34681DB3-58E6-4512-86F2-9477F1A9F3D8" id="cap1" codebase="ImageCapOnWeb.cab#version=2,0,0,0"
                width="170px" height="130px">
                <param name="Visible" value="0" />
                <param name="AutoScroll" value="0" />
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
                <param name="licenseMode" value="2" />
                <param name="key1" value="puOGfx5IZA0dkRea17fs5PS7kFeRO46UFMXd30kDnF541UPvPG5j4kiAFOQ2FqGyTUUimZB/Hn2+L11NwRX5Ei7SAnU=" />
                <param name="key2" value="bqjAfWVIL3mB037XRiKCK5DTZyePjcbVSEVm+VwkbaCBE+7yllMgD0hOzgOVrPA/zlzIPYrdCJjH95gQVLj7SjIdD7vBUnnUi02rEZfF8YLbRuyP8EfsZWqJd+8TvkLBZTXoPTPe7YyY2uVXaRUdzEF/Z/5G2n3GCaQITg==" />
            </object>
            <object id="UserControl1" classid="CLSID:F09D6002-3AD0-42E6-8E3D-D02149CD8F07" codebase="Hook2012.CAB#version=1,0,0,0">
            </object>
            <input id='Button1' class='button' type='button' value='照相' onclick='paizhao()' />
        </div>
        <ComponentArt:CallBack ID="imageCallback" runat="server" PostState="true" OnCallback="imageCallback_Callback">
        </ComponentArt:CallBack>
    </form>

    <script type="text/javascript">
        document.all.cap1.SwitchWatchOnly();  //切换到只显示摄像头画面形式，隐藏编辑按钮等图标.
    </script>

</body>
</html>
