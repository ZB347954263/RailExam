<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeeTransferManage.aspx.cs"
    Inherits="RailExamWebApp.Systems.EmployeeTransferManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>职员调动</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">
    
     function selectArow(obj)
         {
         	var TBL = document.getElementById("grdEntity");
         	for (var i = 1; i < TBL.rows.length; i++)
         	{
         		TBL.rows[i].style.backgroundColor = "White";
         		if (i % 2 != 0)
         			TBL.rows[i].style.backgroundColor = "#EFF3FB";
         	}
         	obj.style.backgroundColor = "#FFEEC2";
         }
     
    function selectEmployee()
      {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
        
            var ret = window.open("SelectEmployee.aspx?math="+Math.random()+"&employeeID="+document.getElementById("hfEmployeeID").value,'','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
            ret.focus();
      }
   
   function deleteTransfer(id)
   {
   	var deleteRight = document.getElementById("HfDeleteRight").value;
			
		if(deleteRight == "False" ) {
			alert("您没有该操作的权限！");
			return;
		}
   	
       if(!window.confirm("您确定要取消调动该员工吗？"))
      {
            return;       
       }
   	document.getElementById("hfDeleteID").value = id;
    form1.submit();
   } 
    function chkAll(obj)
    {
    	var TBL = document.getElementById("grdEntity");
    	for (var i = 1; i < TBL.rows.length; i++)
    	{
    		var ob = TBL.rows[i].cells[0].childNodes[0];
    		if (ob.type == "checkbox")
    			ob.checked = obj.checked;
    	}
    }
    function Transfer() 
    {
    	var updateRight = document.getElementById("HfUpdateRight").value;
			
		if(updateRight == "False" ) {
			alert("您没有该操作的权限！");
			return false;
		}
    	
    		var TBL = document.getElementById("grdEntity");
         	if(TBL.rows.length<2)
         	{
         		alert("请先选择职员！");
         		return false;
         	}
             var  returnValue=  window.showModalDialog('EmployeeTransferManageDetail.aspx?num=' + Math.random(),
    			'', 'help:no; status:no; dialogWidth:400px;dialogHeight:200px');
    
    	if(returnValue!=undefined) {
    	
    		   var sql = "";
    			if(returnValue[0]!="" && returnValue[1]=="")
    				sql ="   org_id="+ returnValue[0];
    			if(returnValue[0]==""  && returnValue[1]!="" )
    				sql ="   post_id="+ returnValue[1];
    		if(returnValue[0]!="" && returnValue[1]!="")
    			sql="  org_id="+ returnValue[0]+", post_id="+returnValue[1];
    		document.getElementById("hfOrgIDAndPostID").value = sql;
    		
    		document.getElementById("hfOrgID").value=returnValue[0];
    		document.getElementById("hfPostID").value=returnValue[1];
    			return true;
    		 
    	}
    	return false;
    }

 
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        系统管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        职员调动</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <input type="button" value="添加职员" onclick="selectEmployee()" class="button" />
                    <asp:Button ID="btnOK" runat="server" Text="调  动" CssClass="button" OnClientClick="return Transfer()"
                        OnClick="btnOK_Click" />
                    <asp:Button ID="btnDeleteChecked" runat="server" Text="移除所选" CssClass="button" OnClick="btnDeleteChecked_Click" />
                </div>
            </div>
            <div id="content">
                <div>
                    <yyc:SmartGridView ID="grdEntity" runat="server" PageSize="15" AutoGenerateColumns="False"
                        DataKeyNames="EMPLOYEE_ID" DataSourceID="ObjectDataSource1" OnRowCreated="grdEntity_RowCreated"
                        AllowPaging="True">
                        <Columns>
                            <asp:TemplateField>
                                <headertemplate>
                                          <asp:CheckBox ID="chkAll" runat="server"/>
                                 
</headertemplate>
                                <itemtemplate>
                                  <asp:CheckBox ID="item" runat="server"/>
                                  <asp:Label runat="server" style="display: none;" ID="lblID" Text='<%# Eval("EMPLOYEE_ID")%>'></asp:Label>
                              
                                  
</itemtemplate>
                                <headerstyle width="50px" wrap="False" />
                                <itemstyle width="50px" wrap="False" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="EMPLOYEE_ID" HeaderText="EMPLOYEE_ID" Visible="False"
                                ReadOnly="True" SortExpression="EMPLOYEE_ID" />
                            <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="姓名" ReadOnly="True" SortExpression="EMPLOYEE_NAME">
                                <headerstyle width="80px" wrap="False" />
                                <itemstyle width="80px" wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WORK_NO" HeaderText="员工编码" ReadOnly="True" SortExpression="WORK_NO">
                                <headerstyle width="150px" wrap="False" />
                                <itemstyle width="150px" wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="getorgname" HeaderText="组织机构" ReadOnly="True" SortExpression="getorgname">
                                <headerstyle width="200px" wrap="False" />
                                <itemstyle width="200px" wrap="False" />
                            </asp:BoundField>
                            <asp:BoundField DataField="post_name" HeaderText="职名" ReadOnly="True" SortExpression="post_name">
                                <headerstyle width="200px" wrap="False" />
                                <itemstyle width="200px" wrap="False" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="操作">
                                <itemtemplate>
                            <a href="#" onclick='deleteTransfer(<%# Eval("EMPLOYEE_ID") %>)'>移除</a>
                            
</itemtemplate>
                                <headerstyle width="100px" />
                                <itemstyle width="100px" />
                            </asp:TemplateField>
                        </Columns>
                    </yyc:SmartGridView>
                </div>
            </div>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="Get" TypeName="OjbData" OnSelected="ObjectDataSource1_Selected">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfSql" Name="sql" PropertyName="Value" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:HiddenField ID="hfSql" runat="server" />
            <asp:HiddenField ID="hfEmployeeID" runat="server" />
            <asp:HiddenField ID="hfDeleteID" runat="server" />
            <asp:HiddenField ID="hfOrgIDAndPostID" runat="server" />
            <asp:HiddenField ID="hfOrgID" runat="server" />
            <asp:HiddenField ID="hfPostID" runat="server" />
            <asp:HiddenField ID="HfUpdateRight" runat="server" />
            <asp:HiddenField ID="HfDeleteRight" runat="server" />
        </div>
    </form>
</body>
</html>
