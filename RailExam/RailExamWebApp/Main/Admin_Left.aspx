<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Admin_Left.aspx.cs" Inherits="RailExamWebApp.Main.Admin_Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script type="text/javascript" src="js/jquery.js"></script>

    <script type="text/javascript" src="js/chili-1.7.pack.js"></script>

    <script type="text/javascript" src="js/jquery.easing.js"></script>

    <script type="text/javascript" src="js/jquery.dimensions.js"></script>

    <script type="text/javascript" src="js/jquery.accordion.js"></script>

    <script language="javascript" type="text/javascript">
	jQuery().ready(function(){
		jQuery('#navigation').accordion({
			header: '.head',
			navigation1: true, 
			event: 'click',
			fillSpace: false,
			animated: 'bounceslide'
		});
	});
    </script>

    <style type="text/css">

body {
	margin:0px;
	padding:0px;
	font-size: 12px;
}
#navigation {
	margin:0px;
	padding:0px;
	width:165px;
}
#navigation a.head {
	cursor:pointer;
	background:url(images/main_34.gif) no-repeat scroll;
	display:block;
	font-weight:bold;
	margin:0px;
	padding:5px 0 5px;
	text-align:center;
	font-size:12px;
	text-decoration:none;
}
#navigation ul {
	border-width:0px;
	margin:0px;
	padding:0px;
	text-indent:0px;
}
#navigation li {
	list-style:none; display:inline;
}
#navigation li li a {
	display:block;
	font-size:12px;
	text-decoration: none;
	text-align:center;
	padding:3px;
}
#navigation li li a:hover {
	background:url(images/tab_bg.gif) repeat-x;
		border:solid 1px #adb9c2;
}

</style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="menu" runat="server">
        </div>
    </form>
</body>
</html>
