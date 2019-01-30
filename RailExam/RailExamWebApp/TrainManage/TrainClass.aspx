<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TrainClass.aspx.cs" Inherits="RailExamWebApp.TrainManage.TrainClass" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <link href="../Style/css.css" rel="stylesheet" type="text/css" />
 <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script src="../Common/JS/jquery.js" type="text/javascript"></script>

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
	    function CallbackUpdateData_Complete(sender, eventArgs) {
	      //  if (document.getElementById("hfIsSFZJ").value == "0") {
	        //    document.getElementById("drpIsComplete").value = document.getElementById("hfIsComplete").value;
	           // document.getElementById("txtUpdateDate").value = document.getElementById("hfUpdateData").value;
	       // }
	    }
	    function Validate()
	    {
	    	var TRAIN_PLAN_ID = document.getElementById("TRAIN_PLAN_ID");
	    	if(TRAIN_PLAN_ID) 
	    	{
	    		if (TRAIN_PLAN_ID.value == "0") 
	    		{
	    		    alert("请选择培训计划！");
	    		    TRAIN_PLAN_ID.focus();
	    		    return false;
	    	    }
	    	}

	    	var dropPlanClass = document.getElementById("dropPlanClass");
	    	if(dropPlanClass) 
	    	{
		    	if (dropPlanClass.value == "" || dropPlanClass.value == "0") 
		    	{
	    		    alert("请选择计划班级！");
	    		    dropPlanClass.focus();
	    		    return false;
	    	    }
	    	}

	    	if (document.getElementById("TRAIN_CLASS_CODE").value == "")
	    	{
	    		alert("培训班编号不能为空！");
	    		return false;
	    	}
	    	 if (document.getElementById("TRAIN_CLASS_CODE").value.length>30)
	    	{
	    	    alert("输入长度超过30个字符！！");
	    		return false;
	    	}

	    	if (document.getElementById("TRAIN_CLASS_NAME").value == "")
	    	{
	    		alert("培训班名称不能为空！");
	    		return false;
	    	}
	    	
	    	if (document.getElementById("TRAIN_CLASS_NAME").value.length>100)
	    	{
	    	    alert("输入长度超过30个字符！！");
	    		return false;
	    	}
	    	
//	    	if (document.getElementById("TRAIN_PLAN_ID").value == "")
//	    	{
//	    		alert("培训计划不能为空！");
//	    		return false;
//	    	}

	    	var beginDate = document.getElementById("dateBegin_DateBox");
	    	var beginDateObj = document.getElementById("dateBegin_compareValidator");
	    	if (beginDateObj.style.display != "none") {
	    		alert("开班日期格式不正确！");
	    		beginDate.focus();
	    		return false;
	    	}
	    	if (beginDate.value.indexOf("-") < 0 && beginDate.value.indexOf("/") < 0) {
	    		alert("开班日期不能为空！");
	    		beginDate.focus();
	    		return false;
	    	}

	    	var dateEnd = document.getElementById("dateEnd_DateBox");
	    	var dateEndObj = document.getElementById("dateEnd_compareValidator");
	    	if (dateEndObj.style.display != "none") {
	    		alert("结束日期格式不正确！");
	    		dateEnd.focus();
	    		return false;
	    	}
	    	if (dateEnd.value.indexOf("-") < 0 && dateEnd.value.indexOf("/") < 0) {
	    		alert("结束日期不能为空！");
	    		dateEnd.focus();
	    		return false;
	    	}
	    	return true;
	    }

	    function Subject(subjectID,mod)
	    {
	    var classid=document.getElementById("hidClassID").value;
	    if(classid!="")
	    {
	        var returnValue="";
	        if(mod=="edit")
	             returnValue = window.showModalDialog('SubjectAdd.aspx?mod=edit&subjectid='+subjectID+"&classID="+classid+"&num="+Math.random(), 
                    '', 'help:no; status:no; dialogWidth:640px;dialogHeight:310px');
            if(mod=="add")
                returnValue = window.showModalDialog('SubjectAdd.aspx?mod=add'+"&classid="+classid+"&num="+Math.random(), 
                    '', 'help:no; status:no; dialogWidth:640px;dialogHeight:310px');
            if(mod=="view")
                returnValue = window.showModalDialog('SubjectAdd.aspx?mod=view&subjectid='+subjectID+"&num="+Math.random(), 
                    '', 'help:no; status:no; dialogWidth:640px;dialogHeight:310px');
             if(returnValue=="ref")
             {   
               if(mod=="add")
                  alert("数据保存成功！");
               else 
                  alert("数据修改成功！");
               document.getElementById("btnPostBack").click();
              }
          }
          else
          {
             alert("请先保存培训班的基本信息！");
          }
	    }
	    function DelSubject(subjectID)
	    {
	       if(confirm("确定要删除吗？"))
	       {
	            document.getElementById("hidSubject").value=subjectID;
	            alert("数据删除成功！");
	            document.getElementById("btnPostBack").click();
	        }
	    }
	    
	    function studentPage(mod)
	    {
	     var classid=document.getElementById("hidClassID").value;
	     var returnValue="";
	     if(classid!="")
	     {
             if(mod=="add")
                returnValue = window.showModalDialog("StudentAdd.aspx?mod=add&classID="+classid+"&num="+Math.random(),
                              "", "help:no; status:no; dialogWidth:710px;dialogHeight:650px;");
             if(returnValue=="ref")
             {   
                document.getElementById("hidStudent").value="add";
               if(mod=="add")
                  alert("数据保存成功！");
               document.getElementById("btnPostBack").click();
              }
          }
          else
          {
             alert("请先保存培训班的基本信息！");
          }
	    }
	    
	    function delStudent(stuID)
	    {
	       document.getElementById("hidStudent").value="del|"+stuID;
	       if(confirm("确定要删除吗？"))
	       {
	           alert("数据删除成功！");
	           document.getElementById("btnPostBack").click();
	       }
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
	        	for (var i = 0; i < grdStudent.get_table().getRowCount(); i++)
	        	{
	        		theItem = grdStudent.getItemFromClientId(grdStudent.get_table().getRow(i).get_clientId());
	        		if($.$F("cbxSelectItem_" + theItem.GetProperty("Id"))!=null)
	        		$.$F("cbxSelectItem_" + theItem.GetProperty("Id")).checked = true;
	        	}
	        }
	         function btnUnselectAllClicked()
	         {
	         	var theItem;
	         	for (var i = 0; i < grdStudent.get_table().getRowCount(); i++)
	         	{
	         		theItem = grdStudent.getItemFromClientId(grdStudent.get_table().getRow(i).get_clientId());
	         		if($.$F("cbxSelectItem_" + theItem.GetProperty("Id"))!=null)
	         		$.$F("cbxSelectItem_" + theItem.GetProperty("Id")).checked = false;
	         	}
	         }
	          function getSelectedItems()
	          {
	          	var ids = [];
	          	var theItem;
	          	for (var i = 0; i < grdStudent.get_table().getRowCount(); i++)
	          	{
	          		theItem = grdStudent.getItemFromClientId(grdStudent.get_table().getRow(i).get_clientId());
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
	          			document.getElementById("btnPostBack").click();
	          	}
	          }
  
  
      function init() {
      	var edit = document.getElementById("hfEdit").value;
      	var search = window.location.search;
      	if(edit =="False" || search.indexOf("type=view")>0) 
      	{
      		document.getElementById("deleteSelected").style.display ="none";
      		document.getElementById("Button1").style.display ="none";
      	}
      }
    </script>

    <title>培训计划</title>
</head>
<body onload="init()">
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
            <div id="content" style="text-align: left">
                <ComponentArt:TabStrip ID="TabStrip1" runat="server" MultiPageId="EmployeeDetailMultiPage">
                    <Tabs>
                        <ComponentArt:TabStripTab Value="0" Text="基本信息" PageViewId="FirstPage">
                        </ComponentArt:TabStripTab>
                        <ComponentArt:TabStripTab Value="1" Text="学员来源" PageViewId="SecondPage">
                        </ComponentArt:TabStripTab>
                        <ComponentArt:TabStripTab Value="2" Text="考试科目" PageViewId="ThirdPage">
                        </ComponentArt:TabStripTab>
                        <ComponentArt:TabStripTab Value="3" Text="考试成绩" PageViewId="FourthPage">
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
                                        <asp:Label ID="Label2" runat="server">培训计划</asp:Label><span class="require">*</span>
                                    </td>
                                    <td colspan="3">
                                        <asp:Label ID="lblPlan" runat="server" ></asp:Label> 
                                        <asp:DropDownList ID="TRAIN_PLAN_ID" runat="server" DataTextField="TRAIN_PLAN_Name"
                                          DataValueField="TRAIN_PLAN_ID" AutoPostBack="true" OnSelectedIndexChanged="TRAIN_PLAN_ID_OnSelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                   </tr>
                                <tr> 
                                    <td style="width: 15%">
                                        <asp:Label ID="Label3" runat="server">计划班级</asp:Label><span class="require">*</span>
                                    </td>
                                    <td colspan="3">
                                       <asp:Label ID="lblPlanClass" runat="server" ></asp:Label>  
                                        <asp:DropDownList ID="dropPlanClass" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropPlanClass_OnSelectedIndexChanged"
                                         >
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        <asp:Label ID="Label1" runat="server">培训班编号</asp:Label><span class="require">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="TRAIN_CLASS_CODE"  Width="200px" MaxLength="30"></asp:TextBox>
                                    </td>
                                
                                    <td style="width: 15%">
                                        <asp:Label ID="Label12" runat="server">培训班名称</asp:Label><span class="require">*</span>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="TRAIN_CLASS_NAME"  Width="200px" MaxLength="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        <asp:Label ID="Label8" runat="server">开班日期</asp:Label>
                                    </td>
                                    <td>
                                        <uc1:Date ID="dateBegin" runat="server" Enabled="false" />
                                    </td>
                                 
                                    <td style="width: 15%">
                                        <asp:Label ID="Label9" runat="server">结束日期</asp:Label>
                                    </td>
                                    <td>
                                        <uc1:Date ID="dateEnd" runat="server" Enabled="false"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 15%">
                                        <asp:Label ID="Label10" runat="server">制定人</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="lblPerson"></asp:Label>
                                    </td>
                                 
                                    <td style="width: 15%">
                                        <asp:Label ID="Label11" runat="server">制定日期</asp:Label>
                                    </td>
                                    <td>
                                        <uc1:Date ID="dateMake" runat="server" Enabled="false" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center" colspan="4">
                                        <asp:Button ID="btnSave" runat="server" Text="保  存" CssClass="button" OnClientClick="return Validate();"
                                            OnClick="btnSave_Click" />
                                        <input type="button" class="button" value="返  回" onclick="javascript:location.href='TrainClassList.aspx'" />
                                        <%-- <asp:Button ID="btnBack" runat="server" Text="返  回" CssClass="button" />--%>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ComponentArt:PageView>
                    <ComponentArt:PageView ID="SecondPage">
                        <div style="text-align: right;">
                            <input id="btnAdd" type="button" value="添加学员" class="button" onclick="studentPage('add')" style="display: none" />
                            <input id="deleteSelected" type="button" class="button" value="删除所选" onclick="getSelectedItems()" />
                        </div>
                        <ComponentArt:Grid ID="grdStudent" runat="server" PageSize="20" Width="98%">
                            <levels>
                                        <ComponentArt:GridLevel DataKeyField="TRAIN_PLAN_EMPLOYEE_ID">
                                            <Columns>
                                            <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" Width="40"
                                    HeadingText="&lt;input  type='checkbox' onclick='btnAllClicked()' name='btnAll' style='border: none' /&gt;" />
                                                <ComponentArt:GridColumn DataField="TRAIN_PLAN_EMPLOYEE_ID" Visible="false" />
                                                 <ComponentArt:GridColumn DataField="employee_name" HeadingText="姓名"  />
                                                <ComponentArt:GridColumn DataField="unit" HeadingText="单位" />
                                                <ComponentArt:GridColumn DataField="workshop" HeadingText="车间"  />
                                                <ComponentArt:GridColumn DataField="group" HeadingText="班组" />
                                                <ComponentArt:GridColumn DataField="post_name" HeadingText="工作岗位"  />
                                                <ComponentArt:GridColumn DataField="work_no" HeadingText="员工编码" />
                                                <ComponentArt:GridColumn DataField="identity_cardno" HeadingText="身份证号"  />
                                                <ComponentArt:GridColumn DataCellClientTemplateId="CTEditStu" HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </levels>
                            <clienttemplates>
                                        <ComponentArt:ClientTemplate ID="CTEditStu">
                                            <a onclick="delStudent(##DataItem.getMember('TRAIN_PLAN_EMPLOYEE_ID').get_value()## )"
                                                title="删除学员" href="#"><b>删除</b></a>
                                        </ComponentArt:ClientTemplate>
                                         <ComponentArt:ClientTemplate ID="CTEdit">
                            <input style="border: none" id="cbxSelectItem_##DataItem.getMember('TRAIN_PLAN_EMPLOYEE_ID').get_value()##" name="cbxSelectItem_##DataItem.getMember('TRAIN_PLAN_EMPLOYEE_ID').get_value()##"
                                type="checkbox" value="##DataItem.getMember('TRAIN_PLAN_EMPLOYEE_ID').get_value()##" />
                        </ComponentArt:ClientTemplate>
                                    </clienttemplates>
                        </ComponentArt:Grid>
                    </ComponentArt:PageView>
                    <ComponentArt:PageView ID="ThirdPage">
                        <div style="text-align: right;">
                            <input id="Button1" type="button" value="新  增" class="button" onclick="Subject('','add')"/>
                        </div>
                        <ComponentArt:Grid ID="GridSubject" runat="server" PageSize="20" Width="98%">
                            <levels>
                                <ComponentArt:GridLevel DataKeyField="train_class_subject_id">
                                    <Columns>
                                        <ComponentArt:GridColumn DataField="train_class_subject_id" Visible="false"/>
                                         <ComponentArt:GridColumn DataField="train_class_name" HeadingText="培训班名称"  />
                                        <ComponentArt:GridColumn DataField="subject_name" HeadingText="科目名称"  />
                                        <ComponentArt:GridColumn DataField="class_hour" HeadingText="培训时间"  />
                                        <ComponentArt:GridColumn DataField="isComputer" HeadingText="是否机考"  />
                                        <ComponentArt:GridColumn DataField="pass_result" HeadingText="及格分数"  />
                                        <ComponentArt:GridColumn DataField="book_name" HeadingText="教材" Visible="false"  />
                                        <ComponentArt:GridColumn DataField="memo" HeadingText="备注" />
                                        <ComponentArt:GridColumn DataCellClientTemplateId="CTEditSub" HeadingText="操作" />
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </levels>
                            <clienttemplates>
                                <ComponentArt:ClientTemplate ID="CTEditSub">
                                <a onclick="Subject(##DataItem.getMember('train_class_subject_id').get_value()##,'view')"
                                        title="预览科目" href="#" class="underline" style="display:none">预览</a> &nbsp;
                                    <a onclick="Subject(##DataItem.getMember('train_class_subject_id').get_value()##,'edit')"
                                        title="修改科目" href="#" class="underline"><b>修改</b></a> &nbsp; <a onclick="DelSubject(##DataItem.getMember('train_class_subject_id').get_value()## )"
                                            title="删除科目" href="#" class="underline"><b>删除</b></a> &nbsp;
                                </ComponentArt:ClientTemplate>
                            </clienttemplates>
                        </ComponentArt:Grid>
                    </ComponentArt:PageView>
                    <ComponentArt:PageView ID="FourthPage">
                        <div style="text-align: right; height: 20px">
                        </div>
                        <ComponentArt:Grid ID="GridResult" runat="server" PageSize="20" Width="98%">
                        </ComponentArt:Grid>
                    </ComponentArt:PageView>
                </ComponentArt:MultiPage>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="OjbData"
                    OnSelected="ObjectDataSource1_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hfSelect" Type="String" PropertyName="Value" Name="sql" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:HiddenField ID="hfSelect" runat="server" />
            </div>
        </div>
        <asp:HiddenField ID="hfID" runat="server" />
        <asp:HiddenField ID="hidSubject" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hidStudent" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hidClassID" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hfSelectedIDs" runat="server"></asp:HiddenField>
        <asp:HiddenField runat="server" ID="hfEdit"/>
        <asp:Button ID="btnPostBack" runat="server" Style="display: none" OnClick="btnPostBack_Click" />
    </form>
</body>
</html>
