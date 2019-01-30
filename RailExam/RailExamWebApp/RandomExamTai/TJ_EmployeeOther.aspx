<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TJ_EmployeeOther.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.TJ_EmployeeOther" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>各单位其他持证情况统计</title>
    <style type="text/css">
 #Grid th{  height:100%;
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
     #Grid tr{  background-color: #FFFFFF; height:16px;}
     #Grid td{  padding:1px 2px 1px 2px;
                border-right: 1px solid #EAE9E1;
                border-bottom: 1px solid #EAE9E1;
                font-family: 宋体;
                font-size: 12px;
                text-align:center;
             }
</style>

    <script type="text/javascript">
  function selectCol(obj) {
	    	 var t = document.getElementById("Grid");
	    	 for (var i = 2; i < t.rows.length; i++) {
	    	 	t.rows(i).style.backgroundColor = "White";
	    	 }
	    	obj.style.backgroundColor = "#FFEEC2"; 
	    }
	    
        function ShowDetail(stationName, colIndex, cellValue)
        {
//            if(cellValue>0)
//            {
//                // stationName已转码
//                var url="TJ_EmployeeWorkerGroupHeaderByOrgDetail.aspx?StationName="+stationName+"&ColIndex="+colIndex;
//                var name="各单位班组长人数统计详细信息";
//                var features="width=1024px,height=768px";
//                
//                open(url,name,features);
//            }
        }
        
          function exportTemplate()
	      {
	       
	       	document.getElementById("btnExcel").click();
	      	var ret = window.showModalDialog("TJ_ExportTemplate.aspx?type=EmployeeOther&math="+Math.random(), '', 'help:no;status:no;dialogWidth:320px;dialogHeight:30px;');
	     
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
            <asp:GridView ID="Grid" runat="server" Width="100%" CssClass="Grid" OnRowCreated="Grid_RowCreated"
                AllowPaging="True" OnPageIndexChanging="Grid_PageIndexChanging" PageSize="18"  AutoGenerateColumns="True"
                OnRowDataBound="Grid_RowDataBound">
            </asp:GridView>
        </div>
        <asp:HiddenField ID="hfRefreshExcel" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
    </form>
</body>
</html>