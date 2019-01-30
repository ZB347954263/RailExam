<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeeInfo.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.EmployeeInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>员工信息</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
        function selectArow(rowIndex) {
            var t = document.getElementById("grdEntity");
            for (var i = 1; i < t.rows.length; i++) {
                if (i - 1 == rowIndex) {
                    t.rows(i).style.backgroundColor = "#FFEEC2";
                }
                else {
                    if ((i - 1) % 2 == 0) {
                        t.rows(i).style.backgroundColor = "#EFF3FB";
                    }
                    else {
                        t.rows(i).style.backgroundColor = "White";
                    }
                }
            }
        }

        function AssignAccount(id)
        {
           var flagUpdate=document.getElementById("HfUpdateRight").value; 
	        if(flagUpdate=="False")
             {
                alert("您没有权限使用该操作！");
                return;
             } 
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-500)*.5;   
            ctop=(screen.availHeight-240)*.5;  
             
            var re = window.open("../Systems/AssignAccount.aspx?id="+id,'AssignAccount','Width=500px; Height=240px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
	        re.focus();
        }
        
        function addEmployee() {
        	var flagUpdate=document.getElementById("HfUpdateRight").value; 
	        if(flagUpdate=="False")
             {
                alert("您没有权限使用该操作！");
                return;
             } 
            var selectOrgId = document.getElementById("hfSelectOrg").value;
        	if(selectOrgId == "") {
        		alert("请选择一个车间或班组！");
        		return;
        	}
        	
//        	var ret = window.showModalDialog('EmployeeInfoDetail.aspx?OrgID=' + selectOrgId,"EmployeeInfoDetail","status:false;dialogWidth:800px;dialogHeight:600px");
//    	    if(ret != "") {
//    		    form1.Refresh.value = ret;
//    		    form1.submit();
//    		    form1.Refresh.value = "";
//    	    }
//        	location.href = 'EmployeeInfoDetail.aspx?OrgID=' + selectOrgId;

        	
//        	var   cleft;   
//            var   ctop;   
//            cleft=(screen.availWidth-800)*.5;   
//            ctop=(screen.availHeight-600)*.5;  
//            var re = window.open("EmployeeInfoDetail.aspx?OrgID="+selectOrgId,'EmployeeInfoDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//	        re.focus();
            
        }
        
        function showEmployee(id) {
        	
        	var flagUpdate=document.getElementById("HfUpdateRight").value; 
	        if(flagUpdate=="False")
             {
	        	//查看
                var ret = window.showModalDialog('EmployeeArchives.aspx?Type=0&ID='+id,"EmployeeArchives","status:false;dialogWidth:900px;dialogHeight:650px");
    	        if(ret != "") 
    	        {
    		        form1.Refresh.value = ret;
    		        form1.submit();
    		        form1.Refresh.value = "";
    	        }	              
	        }
	        else 
	        {
	        	//编辑删除
        	    var ret = window.showModalDialog('EmployeeArchives.aspx?ID='+id,"EmployeeArchives","status:false;dialogWidth:900px;dialogHeight:650px");
    	        if(ret != "") 
    	        {
    		        form1.Refresh.value = ret;
    		        form1.submit();
    		        form1.Refresh.value = "";
    	        }	        
	        }

        	
//        	var   cleft;   
//            var   ctop;   
//            cleft=(screen.availWidth-800)*.5;   
//            ctop=(screen.availHeight-650)*.5;  
//            var re = window.open("EmployeeArchives.aspx?id="+id,'EmployeeArchives','Width=800px; Height=650px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//	        re.focus();
        }
        
        function updateEmployee(id) {
//        	var ret = window.showModalDialog('EmployeeInfoDetail.aspx?ID='+id+'&Type=1',"EmployeeInfoDetail","status:false;dialogWidth:800px;dialogHeight:600px");
//    	    if(ret != "") {
//    		    form1.Refresh.value = ret;
//    		    form1.submit();
//    		    form1.Refresh.value = "";
//    	    }
        	
//        	var   cleft;   
//            var   ctop;   
//            cleft=(screen.availWidth-800)*.5;   
//            ctop=(screen.availHeight-650)*.5;  
//            var re = window.open("EmployeeInfoDetail.aspx?id="+id+"&Type=1",'EmployeeInfoDetail','Width=800px; Height=650px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//	        re.focus();
        	
        	var flagUpdate=document.getElementById("HfUpdateRight").value; 
           if(flagUpdate=="False")
            {
               alert("您没有权限使用该操作！");
               return;
           } 
        	
        	var name = document.getElementById("txtName").value;
		  	var sex = document.getElementById("ddlSex").value;
		  	var status = document.getElementById("ddlStatus").value;
        	var pinyin = document.getElementById("txtPinYin").value;
        	var code = document.getElementById("txtTechnicalCode").value;
        	var postId = document.getElementById("hfPostID").value;

		  	var strQuery = name + "|" + sex + "|" + status+ "|" + pinyin+ "|" + code+ "|" + postId;

        	var search = window.location.search;
        	var idpath = search.substring(search.indexOf("idpath=") + 7, search.indexOf("&type"));
        	location.href = "EmployeeInfoDetail.aspx?id=" + id + "&Type=1&idpath="+ idpath +"&strQuery="+strQuery;
        }
        
       function Update(id)
       {  
       	   var flagUpdate=document.getElementById("HfUpdateRight").value; 
           if(flagUpdate=="False")
           {
               alert("您没有权限使用该操作！");
               return;
           }
       	
          if(! confirm("您确定要为该员工初始化密码吗？"))
          {
               return;
          }
        
          form1.UpdatePsw.value = id;
		  form1.submit();
		  form1.UpdatePsw.value = "";
      }
       
      function deleteEmployee(id) 
      {
      	     var flagUpdate=document.getElementById("HfDeleteRight").value; 
           if(flagUpdate=="False")
            {
               alert("您没有权限使用该操作！");
               return;
           }
      	var loginID = document.getElementById("hfLoginID").value;
      	
      	if(loginID != "0") 
      	{
      		 if(!confirm('您确定要删除此员工吗？')) {
      	 	return;
      	 }
      	} 
      	else 
      	{
           if(!confirm('系统管理员删除员工，将会删除此员工考试信息和基本信息，您确定要删除此员工吗？')) {
      	 	        return;
      	     }
      	}
      	
      	
      	form1.Delete.value = id;
		form1.submit();
		form1.Delete.value = "";
      }
     
     	    function openPage()
	    {
            var url="../Common/SelectPost.aspx?src=book";

            var selectedPost = window.showModalDialog(url, 
                '', 'help:no; status:no; dialogWidth:300px;dialogHeight:620px;scroll:no;');
            
            if(! selectedPost)
            {
                return;
            } 
	    	
            var id=selectedPost.split('|')[0];
            var text=selectedPost.split('|')[1]; 
            
            document.getElementById("txtPost").value=text;
            document.getElementById("hfPostID").value=id;
	    } 
      
     	function clearPost() 
        {
    		document.getElementById("txtPost").value = "";
    		document.getElementById("hfPostID").value = "";
        } 
     	
     	function init() 
     	{
     	    if(window.parent.document.getElementById("NewButton")) 
        	{
        		window.parent.document.getElementById("NewButton").style.display = "";
        		window.parent.document.getElementById("FindButton").style.display = "";
        	}	
     	}
     	
     	function showManagerEmployee() 
     	{
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;  
             var orgId=document.getElementById("hfOrgID").value;  
            var re = window.open("/RailExamBao/RandomExamTai/EmployeeInfoOther.aspx?id="+orgId,'AssignAccount','Width=800; Height=600,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
	        re.focus();
      }
    </script>

</head>
<body onload="init()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="highquery" style="display: none;">
                &nbsp;&nbsp;姓名
                <asp:TextBox ID="txtName" runat="server" Width="10%"></asp:TextBox>
                &nbsp;拼音码
                <asp:TextBox ID="txtPinYin" runat="server" Width="10%"></asp:TextBox>
                &nbsp;职&nbsp;&nbsp;名
                <asp:TextBox ID="txtPost" runat="server" Enabled="false"></asp:TextBox>&nbsp;
                <input type="button" id="btnSelectPost" class="button" value="选择职名" onclick="openPage()" />&nbsp;&nbsp;<input
                    type="button" class="button" value="清  空" onclick="clearPost();" />
                <asp:HiddenField ID="hfPostID" runat="server" />
                <asp:DropDownList ID="ddlSex" runat="server">
                    <asp:ListItem Value="" Text="-性别-"></asp:ListItem>
                    <asp:ListItem Value="男" Text="男"></asp:ListItem>
                    <asp:ListItem Value="女" Text="女"></asp:ListItem>
                </asp:DropDownList><br />
                <%--<asp:ImageButton ID="btnQuery" runat="server" CausesValidation="False" OnClick="btnQuery_Click"
                    ImageUrl="../Common/Image/confirm.gif" />--%>
                技术档案编号
                <asp:TextBox ID="txtTechnicalCode" runat="server" Width="10%"></asp:TextBox>
                是否在岗
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Value="1" Selected="True">在岗</asp:ListItem>
                    <asp:ListItem Value="0">未在岗</asp:ListItem>
                    <asp:ListItem Value="-1">全部</asp:ListItem>
                </asp:DropDownList>
                是否采集指纹
                <asp:DropDownList ID="ddlFinger" runat="server">
                    <asp:ListItem Value="" Text="全部" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="1">已采集</asp:ListItem>
                    <asp:ListItem Value="0">未采集</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="确  定" OnClick="btnQuery_Click" />
                <input type="button" class="buttonLong" value="查看权限人员" onclick="showManagerEmployee()" />
            </div>
            <div id="left" style="width: 100%">
                <div id="leftContent">
                    <yyc:smartgridview id="grdEntity" runat="server" datakeynames="EMPLOYEE_ID" pagesize="15"
                        onrowcommand="grdEntity_RowCommand" allowsorting="True" onrowcreated="grdEntity_RowCreated"
                        onrowdatabounddatarow="grdEntity_RowDataBoundDataRow" datasourceid="ObjectDataSource1">
                        <Columns>
                            <asp:BoundField DataField="EMPLOYEE_ID" Visible="false" />
                            <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="员工姓名" ItemStyle-Wrap="false"
                                HeaderStyle-Wrap="false" SortExpression="EMPLOYEE_NAME">
                                <headerstyle wrap="False"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:TemplateField Visible="false">
                                <itemtemplate>
                                 <asp:Label runat="server" ID="lblIsPhoto" Text='<%#Eval("isPhoto") %>'></asp:Label>
                              </itemtemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="SEX" HeaderText="性别" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                SortExpression="SEX">
                                <headerstyle wrap="False"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Post_Name" HeaderText="职务" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"
                                SortExpression="Post_Name">
                                <headerstyle wrap="False"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="是否在岗" DataField="IsOnPostName">
                                <headerstyle wrap="False"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="FingerNum" HeaderText="指纹采集个数" ItemStyle-Wrap="false"
                                HeaderStyle-Wrap="false" SortExpression="FingerNum">
                                <headerstyle wrap="False"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:TemplateField>
                                <headertemplate> 操作</headertemplate>
                                <itemstyle width="20%" horizontalalign="Center" wrap="false" />
                                <headerstyle width="20%" horizontalalign="Center" wrap="false" />
                                <itemtemplate>
                                    <a href="#" onclick="updateEmployee(<%#Eval("EMPLOYEE_ID")%>)" class="underline"><b>编辑</b></a>
                                    <a href="#" onclick="deleteEmployee(<%#Eval("EMPLOYEE_ID")%>)" class="underline"><b>删除</b></a>
                                    <a href="#" onclick="showEmployee(<%#Eval("EMPLOYEE_ID")%>)" class="underline"><b>档案</b></a>
                                    <a href='EmployeeInfoDetail.aspx?ID=<%#Eval("EMPLOYEE_ID")%>&Type=0' class="underline" style="display: none;">查看</a>
                                    <a onclick="AssignAccount(<%#Eval("EMPLOYEE_ID")%>)" href="#">
                                <b>登录帐户</b></a>
                                     <a onclick="Update(<%#Eval("EMPLOYEE_ID")%>)" href="#"><b>初始化密码</b></a>
                                </itemtemplate>
                            </asp:TemplateField>
                        </Columns>
                    </yyc:smartgridview>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="OjbData"
                        OnSelected="ObjectDataSource1_Selected">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hfSelect" Type="String" PropertyName="Value" Name="sql" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:HiddenField ID="hfSelect" runat="server" />
                </div>
            </div>
        </div>
        <input type="hidden" name="Refresh" />
        <input type="hidden" name="Delete" />
        <asp:HiddenField runat="server" ID="hfSelectOrg" />
        <asp:HiddenField runat="server" ID="hfLevelNum" />
        <input type="hidden" name="UpdatePsw" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="hfLoginID" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <!--href='EmployeeInfoDetail.aspx?ID=<%#Eval("EMPLOYEE_ID")%>&Type=1' -->
    </form>
</body>
</html>
