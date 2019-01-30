<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" Codebehind="TrainPlanAdd.aspx.cs"
    Inherits="RailExamWebApp.TrainManage.TrainPlanAdd" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <link href="../Style/css.css" rel="stylesheet" type="text/css" />
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script src="../Common/JS/jquery.js" type="text/javascript"></script>
<script src="../Common/JS/jquery1.3.2.js" type="text/javascript"></script>
   
    <style type="text/css">
        .displayNone
        {
            display:none;
        }
         #TBLP{   border-collapse:collapse; width: 98%;}
         #TBLP th{  height:100%;
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
     #TBLP tr{  background-color: #FFFFFF; height:16px;}
     #TBLP td{  padding:1px 2px 1px 2px;
                border-right: 1px solid #EAE9E1;
                border-bottom: 1px solid #EAE9E1;
                font-family: 宋体;
                font-size: 12px;
                text-align:center;
             }

    </style>

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

	    function selectCol(obj) {
	    	 var t = document.getElementById("TBLP");
	    	 for (var i = 1; i < t.rows.length; i++) {
	    	 	t.rows(i).style.backgroundColor = "White";
	    	 }
	    	obj.style.backgroundColor = "#FFEEC2";
	    }
	    //切换Tab
	    function TabLog_onTabSelect(sender, eventArgs) {
//	        //等待所有控件加载完毕
//	        if (!allControlsLoaded()) {
//	            setTimeout(TabLog_onTabSelect, 100, sender, eventArgs);
//	            return;
//	        }

//	        if (eventArgs.get_tab().get_value() == "0") {
//	           // CallbackUpdateData.callback(document.getElementById("hfEmployeeID").value);
//	        }
	    }
	    
	    function Validate()
	    {
	    	if(document.getElementById("dropProject").value=="") {
	        	alert("培训项目不能为空！");
	        	return false;
	        }
	        if (document.getElementById("txtTrainPlanName").value == "") 
	        {
	            alert("培训计划名称不能为空！");
	            return false;
	        }
	        if (document.getElementById("txtTrainPlanName").value.length>100) 
	        {
	            alert("输入长度超过100个字符！！");
	            return false;
	        }
	    	if (document.getElementById("txtLoation").value == "") 
	        {
	            alert("培训地点不能为空！");
	            return false;
	        }
	    	 if (document.getElementById("txtLoation").value.length>100)
	    	 {
	    	 	alert("输入长度超过100个字符！！");
	    	 	return false;
	    	 }
	    	if (document.getElementById("txtXieBan").value.length>100) 
	        {
	            alert("输入长度超过100个字符！！");
	            return false;
	        }
	    	
	         if (document.getElementById("dateBegin_DateBox").value == "") 
	        {
	         	alert("开班日期不能为空！");
	            return false;
	        }
	         if (document.getElementById("dateEnd_DateBox").value == "") 
	        {
	            alert("结束日期不能为空！");
	            return false;
	        }
	    	
	        return true;
	    }
	    
	   
	    function getEmployee()
	    {  
	    	var EmpIDs = document.getElementById("hfAllEmpID").value;
	    	var arryEmpID = new Array();
	    	arryEmpID = EmpIDs.split(',');
	    	
	       //判断当前的培训计划ID是否位空
	       if(document.getElementById("hfID").value == "")
	       {
	            alert("请先保存培训计划的基本信息！");
	            return;
	       }
	      var employeeIDs =  window.showModalDialog('../Common/SelectMoreEmployee.aspx?EmpID='+arryEmpID, 
                    '', 'help:no; status:no; dialogWidth:'+window.screen.width+'px;dialogHeight:'+ window.screen.height+'px;scroll:no;');
	      if(!employeeIDs)
	      {
	        return;
	      }
	       if(employeeIDs == "")
	       {
	         return;
	       }
	        __doPostBack("btnUpDate",employeeIDs);
	    }
	   function selectMoreOrg()
	   {
	   	 var orgIDs =  window.showModalDialog('../Common/SelectMoreOrg.aspx', 
                    '', 'help:no; status:no; dialogWidth:310px;dialogHeight:600px;scroll:no;');
         if(!orgIDs)
	      {
	        return;
	      }
	       if(orgIDs == "")
	       {
	         return;
	       }
	        __doPostBack("btnSend",orgIDs);
	   }
	   function addPost(id,mode){
	   	if (document.getElementById("hfID").value == "")
	   	{
	   		alert("请先保存培训计划的基本信息！");
	   		return;
	   	}
	   	if(mode=="add")
	   		id = document.getElementById("hfID").value;
	   	var returnvalue = window.showModalDialog('TrainPlanPostEdit.aspx?id=' + id +"&mode="+mode+"&num=" + Math.random(),
    			'', 'help:no; status:no; dialogWidth:450px;dialogHeight:230px');
        if(returnvalue)
        {
        	__doPostBack("btnUpPost", "ref");
        	 
        }
	   	return false;
	   }
	   function addPostClass(id,mode) {
	   	if (document.getElementById("hfID").value == "")
	   	{
	   		alert("请先保存培训计划的基本信息！");
	   		return;
	   	}
	   	if (mode == "add")
	   		id = document.getElementById("hfID").value;
	   	var returnvalue = window.showModalDialog('TrainPlanPostClassEdit.aspx?id=' + id + "&mode=" + mode + "&num=" + Math.random(),
	   		'', 'help:no; status:no; dialogWidth:550px;dialogHeight:230px');
	   	if (returnvalue)
	   	{
	   		__doPostBack("btnUpPostClass", "ref");

	   	}
	   		return false;
	   }
	    function addPostClassOrg(id,mode) {
	   	if (document.getElementById("hfID").value == "")
	   	{
	   		alert("请先保存培训计划的基本信息！");
	   		return;
	   	}

	    	var height;
	   	if (mode == "add") {
	   		id = document.getElementById("hfID").value;
	   		height = 550;
	   	}
	   	else {
	   		height = 280;
	   	}
	   	var returnvalue = window.showModalDialog('TrainPlanPostClassOrg.aspx?id=' + id + "&mode=" + mode + "&num=" + Math.random(),
	   		'', 'help:no; status:no; dialogWidth:550px;dialogHeight:'+height+'px');
	   	if (returnvalue)
	   	{
	   		__doPostBack("btnUpPostClassOrg", "ref");

	   	}
	    return false;
	   }
	   
	    
	    	     $.extend({
            $F : function(objId){
                return document.getElementById(objId);                
            },            
            getProperties : function(obj){	
                var ps;
                if(typeof(obj) != "object" || obj == "undefined")
                {
                    ps = "[" + obj + "]<BR/>";
                    
                    return ps;
                }
                
                try
                {
                    $.each(obj, function(n, v){
                        if(typeof(p) != "object")
                        {					
	                        ps += "[" + n + "=" + v + "]<BR/>";
                        }
                        else
                        {
	                        ps += "[" + n + "=" + $.getProperties(p) + "]<BR/>";
                        }
                    });
                }
                catch(e)
                {
                    ps = "Not DOM Objects！" + $.getProperties(e);
                }
                
                return ps;			
            },
            showProperties : function (obj){
	            var win = window.open();
	            
	            win.title = "Properties of " + obj;
	            win.document.write($.getProperties(obj));
	            win.document.close();
            }
        });
	     
	      function btnAllClicked()
	      { 
	      	if (form1.btnAll.checked)
	      	{
	      		btnSelectAllClicked();
	      	}
	      	else
	      	{
	      		btnUnselectAllClicked();
	      	}
	      } 
	        function btnSelectAllClicked()
	        {   
	        	var theItem;
	        	for (var i = 0; i < grdEntity.get_table().getRowCount(); i++)
	        	{
	        		theItem = grdEntity.getItemFromClientId(grdEntity.get_table().getRow(i).get_clientId());
	        		if($.$F("cbxSelectItem_" + theItem.GetProperty("Id"))!=null)
	        		$.$F("cbxSelectItem_" + theItem.GetProperty("Id")).checked = true;
	        	}
	        }
	         function btnUnselectAllClicked()
	         {
	         	var theItem;
	         	for (var i = 0; i < grdEntity.get_table().getRowCount(); i++)
	         	{
	         		theItem = grdEntity.getItemFromClientId(grdEntity.get_table().getRow(i).get_clientId());
	         		if($.$F("cbxSelectItem_" + theItem.GetProperty("Id"))!=null)
	         		$.$F("cbxSelectItem_" + theItem.GetProperty("Id")).checked = false;
	         	}
	         }
	          function getSelectedItems()
	          {
	          	var ids = [];
	          	var theItem;
	          	for (var i = 0; i < grdEntity.get_table().getRowCount(); i++)
	          	{
	          		theItem = grdEntity.getItemFromClientId(grdEntity.get_table().getRow(i).get_clientId());
	          		if($.$F("cbxSelectItem_" + theItem.GetProperty("Id"))!=null)
	          		{
	          			if ($.$F("cbxSelectItem_" + theItem.GetProperty("Id")).checked)
	          				ids.push(theItem.GetProperty("Id"));
	          		}
	          	}
	          	document.getElementById("hfSelectedIDs").value = ids.join(',');  
	          	if(document.getElementById("hfSelectedIDs").value.length>0)
	          	{  
	          		if (confirm("您确定要删除吗？"))
	          			return true;
	          		else {
	          			return false;
	          		}
	          	}
	          	else {
	          		return false;
	          	}
	          }
	          
	     function LinkEmp(obj) {
	     	var isRef = showCommonDialog('/RailExamBao/TrainManage/EmployeeInfo.aspx?classOrgID=' + obj + "&num=" + Math.random());
	     	if (isRef == "true") {
	     		//刷新学员，站段，职名，表格
	     		document.getElementById("btnRef").click();
	     	}      
	     }
	   
    </script>

    <title>培训计划</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        培训管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        培训计划详细信息</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
            </div>
            <div id="content">
                <div style="text-align: left;">
                    <ComponentArt:TabStrip ID="TabStrip1" runat="server" MultiPageId="EmployeeDetailMultiPage">
                        <Tabs>
                            <ComponentArt:TabStripTab Value="0" Text="基本信息" PageViewId="FirstPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="1" Text="计划职名" PageViewId="SecondPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="2" Text="计划培训班" PageViewId="ThirdPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="3" Text="计划站段" PageViewId="FourthPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="4" Text="学员来源" PageViewId="FifthPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="5" Text="计划报表" PageViewId="SixPage">
                            </ComponentArt:TabStripTab>
                        </Tabs>
                        <ClientEvents>
                            <TabSelect EventHandler="TabLog_onTabSelect" />
                        </ClientEvents>
                    </ComponentArt:TabStrip>
                    <ComponentArt:MultiPage ID="EmployeeDetailMultiPage" Width="100%" runat="server"
                        RunningMode="Callback">
                        <ComponentArt:PageView ID="FirstPage">
                            <div style="text-align: center;">
                                <table class="contentTable">
                                    <tr>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label1" runat="server">年度</asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblYear" runat="server">2011</asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label2" runat="server">培训计划类别</asp:Label><span class="require">*</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTrainPlanType" runat="server" DataTextField="TRAINPLAN_TYPE_NAME"
                                                AutoPostBack="true" DataValueField="TRAINPLAN_TYPE_ID" OnSelectedIndexChanged="ddlTrainPlanType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label13" runat="server">培训项目</asp:Label><span class="require">*</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dropProject" runat="server" >
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label15" runat="server">是否选职名</asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chkHasPost" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label3" runat="server">计划名称</asp:Label><span class="require">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtTrainPlanName" MaxLength="100"></asp:TextBox>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label4" runat="server">培训地点</asp:Label><span class="require">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtLoation"  MaxLength="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label5" runat="server">主办单位</asp:Label><span class="require">*</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlZhuBan" runat="server" DataTextField="TRAINPLAN_TYPE_NAME"
                                                DataValueField="TRAINPLAN_TYPE_ID">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label6" runat="server">承办单位</asp:Label><span class="require">*</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlChengBan" runat="server" DataTextField="TRAINPLAN_TYPE_NAME"
                                                DataValueField="TRAINPLAN_TYPE_ID">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label7" runat="server">协办单位</asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <asp:TextBox runat="server" ID="txtXieBan" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label8" runat="server">开始日期</asp:Label><span class="require">*</span>
                                        </td>
                                        <td>
                                            <uc1:Date ID="dateBegin" runat="server" />
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label9" runat="server">结束日期</asp:Label><span class="require">*</span>
                                        </td>
                                        <td>
                                            <uc1:Date ID="dateEnd" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                    </tr>
                                    <tr>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label10" runat="server">制定人</asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label runat="server" ID="lblPerson"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:Label ID="Label11" runat="server">制定日期</asp:Label><span class="require">*</span>
                                        </td>
                                        <td>
                                            <uc1:Date ID="dateMake" runat="server" Enabled="false" />
                                        </td>
                                    </tr>
                                    <tr style="display: none">
                                        <!--屏蔽上报单位-->
                                        <td style="width: 15%">
                                            <asp:Label ID="Label12" runat="server">需要上报单位</asp:Label>
                                        </td>
                                        <td colspan="3">
                                            <input type="button" class="button" value="选  择" onclick="selectMoreOrg();" />
                                            <br />
                                            <asp:TextBox runat="server" Width="80%" ID="txtOrgIDs" ReadOnly="true" TextMode="MultiLine"
                                                Height="100">
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center" colspan="4">
                                            <asp:Button ID="btnSend" runat="server" Text="更新GRID" CssClass="displayNone" OnClick="btnSend_Click" />
                                            <asp:Button ID="btnSave" runat="server" Text="保  存" CssClass="button" OnClientClick="return Validate();"
                                                OnClick="btnSave_Click" />
                                            <input type="button" class="button" value="返  回" onclick="javascript:location.href='TrainPlanList.aspx'" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="SecondPage">
                            <div style="text-align: right;">
                                <asp:Button ID="btnUpPost" runat="server" CssClass="displayNone" OnClick="btnUpPost_Click" />
                                <asp:Button ID="btnAddPost" runat="server" Text="新  增" CssClass="button" OnClientClick="return addPost('','add');" />
                            </div>
                            <div style="text-align: center;">
                                <ComponentArt:Grid ID="grdPost" runat="server" PageSize="20" Width="98%">
                                    <levels>
                                        <ComponentArt:GridLevel DataKeyField="train_plan_post_id">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="train_plan_post_id" Visible="false" />
                                               <ComponentArt:GridColumn DataField="postName" HeadingText="职名" Align="Center"/>
                                               <ComponentArt:GridColumn DataField="isleader" HeadingText="是否班组长" Align="Center"/>
                                               <ComponentArt:GridColumn DataField="employee_number" HeadingText="计划上报人数" Align="Center"/>
                                               <ComponentArt:GridColumn DataField="last_employee_number" HeadingText="实际上报人数" Align="Center"/>
                                                <ComponentArt:GridColumn DataCellClientTemplateId="EditPost" HeadingText="操作" AllowSorting="False" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </levels>
                                    <clienttemplates>
                                        <ComponentArt:ClientTemplate ID="EditPost">
                                         <span id="spanView" >
                                            <a onclick="addPost('##DataItem.getMember('train_plan_post_id').get_value()##','edit')"
                                        title="修改职名" href="#" class="underline"><b>修改</b></a> &nbsp;
                                            <a onclick="javascript:if(!confirm('您确定要删除此职名吗？')){return;}__doPostBack('btnUpPost','## DataItem.getMember('train_plan_post_id').get_value() ##');"
                                                title="删除职名" href="#"><b>删除</b></a> </span>
                                        </ComponentArt:ClientTemplate>
                                    </clienttemplates>
                                </ComponentArt:Grid>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="ThirdPage">
                            <div style="text-align: right;">
                                <asp:Button ID="btnUpPostClass" runat="server" CssClass="displayNone" OnClick="btnUpPostClass_Click" />
                                <asp:Button ID="btnAddPostClass" runat="server" Text="新  增" CssClass="button" OnClientClick="return addPostClass('','add')" />
                            </div>
                            <div style="text-align: center;">
                                <ComponentArt:Grid ID="grdClass" runat="server" PageSize="20" Width="98%">
                                    <levels>
                                        <ComponentArt:GridLevel DataKeyField="train_plan_post_class_id">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="train_plan_post_class_id" Visible="false" />
                                                <ComponentArt:GridColumn DataField="postName" HeadingText="职名" Align="Center"/>
                                                <ComponentArt:GridColumn DataField="class_name" HeadingText="培训班" Align="Center"/>
                                                <ComponentArt:GridColumn DataField="begin_date1" HeadingText="开始时间" Align="Center"/>
                                                <ComponentArt:GridColumn DataField="end_date1" HeadingText="结束时间" Align="Center"/>
                                                <ComponentArt:GridColumn DataField="total_employee_number" HeadingText="计划上报人数" Align="Center"/>
                                                <ComponentArt:GridColumn DataField="last_employee_number" HeadingText="实际上报人数" Align="Center"/>
                                                <ComponentArt:GridColumn DataCellClientTemplateId="EditClass" HeadingText="操作" AllowSorting="False" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </levels>
                                    <clienttemplates>
                                        <ComponentArt:ClientTemplate ID="EditClass">
                                            <a onclick="addPostClass('##DataItem.getMember('train_plan_post_class_id').get_value()##','edit')"
                                        title="修改培训班" href="#" class="underline"><b>修改</b></a> &nbsp;
                                            <a onclick="javascript:if(!confirm('您确定要删除此培训班吗？')){return;}__doPostBack('btnUpPostClass','## DataItem.getMember('train_plan_post_class_id').get_value() ##');"
                                                title="删除培训班" href="#"><b>删除</b></a>
                                        </ComponentArt:ClientTemplate>
                                    </clienttemplates>
                                </ComponentArt:Grid>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="FourthPage">
                            <div style="text-align: right;">
                                <asp:Button ID="btnUpPostClassOrg" runat="server" CssClass="displayNone" OnClick="btnUpPostClassOrg_Click" />
                                <asp:Button ID="btnAddClassOrg" runat="server" Text="新  增" CssClass="button" OnClientClick="return addPostClassOrg('','add');" />
                            </div>
                            <div style="text-align: center;">
                                <ComponentArt:Grid ID="grdClassOrg" runat="server" PageSize="20" Width="98%">
                                    <levels>
                                        <ComponentArt:GridLevel DataKeyField="train_plan_post_class_org_id">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="train_plan_post_class_org_id" Visible="false" />
                                                <ComponentArt:GridColumn DataField="postName" HeadingText="职名" Align="Center"/>
                                                <ComponentArt:GridColumn DataField="class_name" HeadingText="培训班" Align="Center"/>
                                                 <ComponentArt:GridColumn DataField="full_name" HeadingText="单位" Align="Center"/>
                                                <ComponentArt:GridColumn DataField="begin_date1" HeadingText="开始时间" Align="Center"/>
                                                <ComponentArt:GridColumn DataField="end_date1" HeadingText="结束时间" Align="Center"/>
                                                <ComponentArt:GridColumn DataField="employee_number" HeadingText="计划上报人数" Align="Center"/>
                                                <ComponentArt:GridColumn DataField="link" HeadingText="实际上报人数" Align="Center"/>
                                                <ComponentArt:GridColumn DataCellClientTemplateId="EditClassOrg" HeadingText="操作" AllowSorting="False"/>
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </levels>
                                    <clienttemplates>
                                        <ComponentArt:ClientTemplate ID="EditClassOrg">
                                            <a onclick="addPostClassOrg('##DataItem.getMember('train_plan_post_class_org_id').get_value()##','edit')"
                                        title="修改培训班站段" href="#" class="underline"><b>修改</b></a> &nbsp;
                                            <a onclick="javascript:if(!confirm('您确定要删除此培训班站段吗？')){return;}__doPostBack('btnUpPostClassOrg','## DataItem.getMember('train_plan_post_class_org_id').get_value() ##');"
                                                title="删除培训班站段" href="#"><b>删除</b></a>
                                        </ComponentArt:ClientTemplate>
                                    </clienttemplates>
                                </ComponentArt:Grid>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="FifthPage">
                            <div style="text-align: right;">
                                <asp:Button ID="btnUpDate" runat="server" Text="更新GRID" CssClass="displayNone" OnClick="btnUpDate_Click" />
                                <asp:Button ID="btnDelete" runat="server" Text="删除学员" CssClass="displayNone" OnClick="btnDelete_Click" />
                                <input id="btnAdd" type="button" value="添加学员" class="button" onclick="getEmployee();"
                                    style="display: none" />
                                <asp:Button ID="btnDelStu" runat="server" Text="删除所选" CssClass="button" OnClientClick="return getSelectedItems()"
                                    OnClick="btnDelStu_Click" />
                            </div>
                            <div style="text-align: center;">
                                <ComponentArt:Grid ID="grdEntity" runat="server" PageSize="20" Width="98%">
                                    <levels>
                                        <ComponentArt:GridLevel DataKeyField="TRAIN_PLAN_EMPLOYEE_ID">
                                            <Columns>
                                            <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="Delete" 
                                    HeadingText="&lt;input  type='checkbox' onclick='btnAllClicked()' name='btnAll' style='border: none' /&gt;" />
                                                <ComponentArt:GridColumn DataField="TRAIN_PLAN_EMPLOYEE_ID" Visible="false" />
                                                <ComponentArt:GridColumn DataField="train_plan_post_class_id" Visible="false" />
                                                <ComponentArt:GridColumn DataField="unit" HeadingText="单位" Align="Center"/>
                                                <ComponentArt:GridColumn DataField="workshop" HeadingText="车间" Align="Center" />
                                                <ComponentArt:GridColumn DataField="group" HeadingText="班组" Align="Center" />
                                                <ComponentArt:GridColumn DataField="post_name" HeadingText="职名" Align="Center" />
                                                <ComponentArt:GridColumn DataField="postnames" HeadingText="计划职名" Align="Center" />
                                                <ComponentArt:GridColumn DataField="class_name" HeadingText="计划培训班" Align="Center" />
                                                <ComponentArt:GridColumn DataField="zgorg" HeadingText="计划站段" Align="Center" />
                                                <ComponentArt:GridColumn DataField="employee_name" HeadingText="姓名" Align="Center" />
                                                <ComponentArt:GridColumn DataField="work_no" HeadingText="员工编码" Align="Center" />
                                                <ComponentArt:GridColumn DataField="identity_cardno" HeadingText="身份证号" Align="Center" />
                                                <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </levels>
                                    <clienttemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit">
                                            <a onclick="javascript:if(!confirm('您确定要删除此学员吗？')){return;}__doPostBack('btnDelete','## DataItem.getMember('TRAIN_PLAN_EMPLOYEE_ID').get_value() ##');"
                                                title="删除学员" href="#"><b>删除</b></a>
                                        </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="Delete">
                            <input style="border: none" id="cbxSelectItem_##DataItem.getMember('TRAIN_PLAN_EMPLOYEE_ID').get_value()##" name="cbxSelectItem_##DataItem.getMember('TRAIN_PLAN_EMPLOYEE_ID').get_value()##"
                                type="checkbox" value="##DataItem.getMember('TRAIN_PLAN_EMPLOYEE_ID').get_value()##" />
                        </ComponentArt:ClientTemplate>
                                    </clienttemplates>
                                </ComponentArt:Grid>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="SixPage">
                            <div style="text-align: center;">
                                <div runat="server" id="divTab" style="margin-top: 20px; height: 485px; overflow-y: auto;">
                                </div>
                            </div>
                        </ComponentArt:PageView>
                    </ComponentArt:MultiPage>
                </div>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="OjbData"
                    OnSelected="ObjectDataSource1_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hfSelect" Type="String" PropertyName="Value" Name="sql" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:HiddenField ID="hfSelect" runat="server" />
                <asp:HiddenField ID="hfOrgIDs" runat="server" />
            </div>
        </div>
        <asp:HiddenField ID="hfID" runat="server" />
        <asp:HiddenField ID="hfAllEmpID" runat="server" />
        <asp:HiddenField ID="hfPostIDs" runat="server" />
        <asp:HiddenField ID="hfselectedPostIDs" runat="server" />
        <asp:HiddenField ID="hfIsView" runat="server" />
        <asp:HiddenField ID="hfSelectedIDs" runat="server"></asp:HiddenField>
        <asp:Button ID="btnRef" runat="server" Text="" CssClass="displayNone" OnClick="btnRef_Click" />
    </form>
   
</body>
 <script type="text/javascript">
 var tbl = document.getElementById("TBLP"); 
 var newhtml = "<tr> <td colspan='2'>小 &nbsp; &nbsp; &nbsp;  计</td> ";
 var n = 1;
 if(tbl!=null)
 {
 	if (tbl.rows.length > 3)
 		n = 3;

 	for (var i = 2; i < tbl.rows(n).cells.length; i++) {
 		var num = 0;
 		for (var j = 3; j < tbl.rows.length; j++) {
 			if (tbl.rows(j).cells(i).innerText == "")
 				num += 0;
 			else
 				num += Number(tbl.rows(j).cells(i).innerText);
 		}
 		newhtml += "<td>" + num + "</td>";
 	}
 	newhtml += "</tr>";

 	$("#TBLP").append(newhtml);
 }
    </script>
</html>
