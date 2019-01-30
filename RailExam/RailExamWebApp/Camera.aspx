<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Camera.aspx.cs" Inherits="RailExamWebApp.Camera" %>
<html >
<head runat="server">
    <title>无标题页</title>

    <script type="text/javascript">
        function showImage() {
        	var ret = window.showModalDialog("Hook2012.aspx", "", 'help:no; status:no; dialogWidth:200px;dialogHeight:300px;scroll:no;');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <object id="UserControl1" classid="CLSID:2ED10C34-7B31-4793-9446-9CF21C8673C9" codebase="Camera.CAB#version=1,0,0,0">
            </object>
        </div>
        <input type="button" onclick="showImage();" value="click" />
    </form>
</body>
</html>
