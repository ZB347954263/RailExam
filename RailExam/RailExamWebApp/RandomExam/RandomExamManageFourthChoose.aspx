<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RandomExamManageFourthChoose.aspx.cs" Inherits="RailExamWebApp.RandomExam.RandomExamManageFourthChoose" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>选择考试</title>
    <script type="text/javascript">       
        function tvExamCategory_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {            
                window.frames["ifExamManageInfo"].location = "RandomExamManageFourthChooseDetail.aspx?id="+ node.get_value(); 
            }
            else
            {
                window.frames["ifExamManageInfo"].location = "RandomExamManageFourthChooseDetail.aspx?id="+ tvExamCategory.get_nodes().getNode(0).get_value(); 
            }
        } 
       
       function GetChoose()
       {
          var search = window.location.search;
          var examid= search.substring(search.indexOf('id=')+3);
          var frame = window.frames["ifExamManageInfo"];
		  var inputs = frame.document.getElementsByTagName("input");
		    for(var i=0; i<inputs.length; i++)
		    {
		        switch(inputs[i].type)
		        {
		            case "radio":
		            {
		                if(inputs[i].checked)
		                {
//		                   alert(examid);
//		                   alert( inputs[i].id);
		                    if(examid== inputs[i].id)
		                   {
		                        alert("您正在编辑该考试，不能选择该考试！");
		                        return;
		                   } 
		                   else
		                   {
		                        window.opener.form1.ChooseExamID.value =  inputs[i].id;
		                        window.opener.form1.submit();
		                        window.close();
		                   }
		                }
		                break;
		            }
		            default:
		            {
		                break;
		            }
		        }
		    }
       } 
      
      function imgBtns_onClick(btn)
      {
            if(window.frames["ifExamManageInfo"].document.getElementById("query").style.display == "none")
                window.frames["ifExamManageInfo"].document.getElementById("query").style.display = "";
            else
                window.frames["ifExamManageInfo"].document.getElementById("query").style.display = "none";
      } 
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                  <div id="parent">
                        选择考试</div>
                </div>
               <div id="button">
                     <input type="button" class="button" value="查  询" onclick="imgBtns_onClick(this)" />
                    <input type="button" class="button" value="确  定" onclick="GetChoose()" />
                </div> 
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        考试分类</div>
                    <div id="leftContent">
                        <ComponentArt:TreeView ID="tvExamCategory" runat="server" EnableViewState="true">
                            <ClientEvents>
                                <NodeSelect EventHandler="tvExamCategory_onNodeSelect" />
                            </ClientEvents>
                        </ComponentArt:TreeView>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContentWithNoHead">
                        <iframe id="ifExamManageInfo" src="RandomExamManageFourthChooseDetail.aspx?id=0" frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div> 
    </form>
</body>
</html>