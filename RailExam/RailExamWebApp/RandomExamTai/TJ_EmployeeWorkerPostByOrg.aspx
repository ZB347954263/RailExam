<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TJ_EmployeeWorkerPostByOrg.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.TJ_EmployeeWorkerPostByOrg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>各单位工种人数统计</title>
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
	    	 for (var i = 1; i < t.rows.length; i++) {
	    	 	t.rows(i).style.backgroundColor = "White";
	    	 }
	    	obj.style.backgroundColor = "#FFEEC2";
 
	    }
	    
	    function SelectPost() {

	    	var selectedIDs = document.getElementById("hfPostIDs").value ;
	    	 var selectedNames=document.getElementById("hfPostNames").value ;
	    	var url1 = "../Common/MultiSelectPostOne.aspx?ids=" + selectedIDs;
	    	var selectedPost = window.showModalDialog(url1,
	    		'', 'help:no; status:no; dialogWidth:300px;dialogHeight:620px;scroll:no;');
	     //	alert(selectedPost);
	    	if (selectedPost != "" && selectedPost != undefined)
	    	{ 
	    		var arrIDS = [];
	    		var arr = selectedPost.split("|")[0].split(",");
	    		for (var i = 0; i < arr.length; i++) {
	    			if(arr[i].indexOf("'")==-1)
	    			arrIDS.push("'"+arr[i]+"'");
	    			else {
	    				arrIDS.push(arr[i]);
	    			}
	    		}
	    	 
	    		  document.getElementById("hfPostIDs").value =arrIDS.join(",");   
	    		if(document.getElementById("hfPostIDs").value.length<=2)
	    		{
	    			document.getElementById("hfPostIDs").value = "";
	    			document.getElementById("txtPostName").value = "";
	    			document.getElementById("hfPostNames").value = "";
	    			return;
	    		}
	    		document.getElementById("btnPost").click();
	    	}
	    }
	    
	      function exportTemplate()
	      {
	       
	       	document.getElementById("btnExcel").click();
	      	var ret = window.showModalDialog("TJ_ExportTemplate.aspx?type=EmployeeWorkerPostByOrg&math="+Math.random(), '', 'help:no;status:no;dialogWidth:320px;dialogHeight:30px;');
	      	 
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
        <div id="content">
            <div style="text-align: right">
                <asp:Button ID="btnOK" runat="server" Text="查  询" CssClass="button" OnClick="btnOK_Click" OnClientClick="return getOrg();" />
                <input type="button" class="button" onclick="exportTemplate()" value="导出Excel" />
                <asp:Button ID="btnExcel" runat="server" Text="" OnClick="btnExcel_Click" Style="display: none;" />
        
            </div>
            <table class="contentTable" width="400px">
                <tr>
                    <td style="width: 5%">
                        工种</td>
                    <td>
                        <asp:TextBox ID="txtPostName" runat="server" Enabled="False" Width="95%" Rows="3"
                            TextMode="MultiLine"></asp:TextBox>
                        <a href="#" title="选择工种" onclick="SelectPost()">
                            <asp:Image runat="server" ID="Image" ImageUrl="../Common/Image/search.gif" AlternateText="选择工种">
                            </asp:Image>
                        </a>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="Grid" runat="server" Width="100%" AllowPaging="true" PageSize="15"
                OnRowCreated="Grid_RowCreated" OnPageIndexChanging="Grid_PageIndexChanging">
            </asp:GridView>
        </div>
        <asp:HiddenField ID="hfPostIDs" runat="server" />
        <asp:HiddenField ID="hfPostNames" runat="server" />
        <asp:HiddenField ID="hfRefreshExcel" runat="server" />
        <asp:Button ID="btnPost" runat="server" Style="display: none" OnClick="btnPost_Click" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
    </form>
</body>
</html>
