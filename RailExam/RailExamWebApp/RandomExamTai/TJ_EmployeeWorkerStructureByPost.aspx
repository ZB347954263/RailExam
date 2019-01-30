<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TJ_EmployeeWorkerStructureByPost.aspx.cs" Inherits="RailExamWebApp.TJ_EmployeeWorkerStructureByPost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>各工种工人结构统计</title>
    <style type="text/css">
 #GridInfo th{  height:100%;
                    border: 1px solid #FFFFFF; 
                    border-right-color: #92C3F2; 
                    border-bottom-color: #92C3F2;
                    padding:0;
                    vertical-align:middle;
                    text-align:center;
                    color: #2D61BA;
	                font-family: 宋体; 
	                font-size: 12px; 
	                font-weight: bold;
	                text-align:center;
                    cursor:pointer;
                    padding:2px;
                 }
     #GridInfo tr{  background-color: #FFFFFF; height:16px;}
     #GridInfo td{  padding:1px 2px 1px 2px;
                border-right: 1px solid #EAE9E1;
                border-bottom: 1px solid #EAE9E1;
                font-family: 宋体;
                font-size: 12px;
                text-align:center;
             }
</style>
  <script type="text/javascript">
  function selectCol(obj) {
	    	 var t = document.getElementById("GridInfo");
	    	 for (var i = 2; i < t.rows.length; i++) {
	    	 	t.rows(i).style.backgroundColor = "White";
	    	 }
	    	obj.style.backgroundColor = "#FFEEC2";
 
	    }
   function exportTemplate()
	      {

   	var deuCount = document.getElementById("hfEduCount").value;
   	var techCount = document.getElementById("hfTechCount").value;
	       	document.getElementById("btnExcel").click();
	      	var ret = window.showModalDialog("TJ_ExportTemplate.aspx?type=EmployeeWorkerStructureByPost&eduCount="+deuCount+"&techCount="+techCount+"&math="+Math.random(),
	      		'', 'help:no;status:no;dialogWidth:320px;dialogHeight:30px;');
	     
	      	if (ret != "")
	      	{
	      		document.getElementById("hfRefreshExcel").value = ret;
	      		document.getElementById("btnExcel").click();
	      		 document.getElementById("hfRefreshExcel").value = "";
	      	}
	      }
 function  getOrg() {
        document.getElementById("hfOrgID").value=window.parent.document.getElementById("ddlOrg").value;
     	return true;
     } 
  </script>
</head>
<body>
    <form id="form1" runat="server">
      <div style="text-align: right;">
           <asp:Button ID="btnQuery" runat="server" Text="查  询" CssClass="button" OnClick="btnQuery_Click"  OnClientClick="return getOrg();" />
            <input type="button" class="button" onclick="exportTemplate()" value="导出Excel" />
            <asp:Button ID="btnExcel" runat="server" Text="" OnClick="btnExcel_Click" Style="display: none;" />
        </div>
    <div>
        <asp:GridView ID="GridInfo" runat="server" OnRowCreated="GridInfo_RowCreated" AllowPaging="True" OnPageIndexChanging="GridInfo_PageIndexChanging" PageSize="18">
        </asp:GridView>
    </div>
     <asp:HiddenField ID="hfRefreshExcel" runat="server" />
        <asp:HiddenField ID="hfEduCount" runat="server" />
        <asp:HiddenField ID="hfTechCount" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
    </form>
</body>
</html>
