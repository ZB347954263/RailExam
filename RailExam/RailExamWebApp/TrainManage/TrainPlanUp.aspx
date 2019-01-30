<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TrainPlanUp.aspx.cs" Inherits="RailExamWebApp.TrainManage.TrainPlanUp"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
       <script type="text/javascript" src="../Common/JS/Common.js"></script>
 
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <title>上报培训计划</title>

    <script type="text/javascript">
    function SelectEmp(classID,orgID,canUp,isHasExam) { 
    	var EmpIDs = document.getElementById("hfAllEmpID").value;
    	var arryEmpID = [];
	    	arryEmpID = EmpIDs.split(',');
	    	
    	/*if(isHasExam=="1") {
    		alert("已存在该培训班的考试，不能上报！");
    		return;
    	}*/
    	
    	if(canUp=="0")
    	{
    		alert("该培训班结束时间已经超过上报期限，不能上报！");
    		return;
    	}    
    	document.getElementById("hfClassID").value = classID;
    	document.getElementById("hfOrgID").value = orgID;

	      var employeeIDs =  window.showModalDialog('../Common/SelectMoreEmployee.aspx?EmpID=0'+"&planClassID="+classID+"&planClassOrgID="+orgID+"&num=" + Math.random(), 
                    window, 'help:no; status:no; dialogWidth:'+window.screen.width+'px;dialogHeight:'+ window.screen.height+'px;scroll:no;');
	     
//	       if(employeeIDs == "")
//	       {
//	         return;
//	       }

    	//document.getElementById("hfSelectEmpID").value = employeeIDs;
//    	if(employeeIDs!=undefined)
//	        __doPostBack("btnUpdateEmp");
    }
    function DropEmp(classID,orgID) {
    	
    		document.getElementById("hfClassID").value = classID;
         	document.getElementById("hfOrgID").value = orgID;
    	 __doPostBack("btnDropEmp");
    }

    function LinkEmp(obj) {
	     	var isRef = showCommonDialog('/RailExamBao/TrainManage/EmployeeInfo.aspx?classOrgID=' + obj + "&num=" + Math.random());
	     if(isRef=="true") {
	     	//刷新学员，站段，职名，表格
	     	document.getElementById("btnRef").click();
	     }
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head" style="width: 830px">
                <div id="location">
                    <div id="current" runat="server">
                        上报培训计划</div>
                </div>
            </div>
            <div style="text-align: right">
                <asp:Button ID="btnUpdateEmp" runat="server" CssClass="displayNone" OnClick="btnUpdateEmp_Click" />
                <asp:Button ID="btnDropEmp" runat="server" CssClass="displayNone" OnClick="btnDropEmp_Click" />
                <input type="button" onclick="javascript:window.close();" class="button" value="关  闭" />
            </div>
            <div style="text-align: center;">
                <ComponentArt:Grid ID="grdClassOrg" runat="server" PageSize="20" Width="98%">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="train_plan_post_class_org_id">
                            <Columns>
                                <ComponentArt:GridColumn DataField="train_plan_post_class_org_id" Visible="false" />
                                <ComponentArt:GridColumn DataField="train_plan_post_class_id" Visible="false" />
                                <ComponentArt:GridColumn DataField="isHasExam" Visible="false" />
                                <ComponentArt:GridColumn DataField="canUp" Visible="false" />
                                <ComponentArt:GridColumn DataField="postName" HeadingText="职名" Align="Center" />
                                <ComponentArt:GridColumn DataField="class_name" HeadingText="培训班" Align="Center" />
                                <ComponentArt:GridColumn DataField="full_name" HeadingText="单位" Align="Center" />
                                <ComponentArt:GridColumn DataField="begin_date1" HeadingText="开始时间" Align="Center" />
                                <ComponentArt:GridColumn DataField="end_date1" HeadingText="结束时间" Align="Center" />
                                <ComponentArt:GridColumn DataField="employee_number" HeadingText="计划上报人数" Align="Center" />
                                <ComponentArt:GridColumn DataField="link" HeadingText="实际上报人数" Align="Center"  />
                                <ComponentArt:GridColumn DataCellClientTemplateId="EditClassOrg" HeadingText="操作"
                                    AllowSorting="False" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="EditClassOrg">
                            <a onclick="SelectEmp('##DataItem.getMember('train_plan_post_class_id').get_value()##','##DataItem.getMember('train_plan_post_class_org_id').get_value()##','##DataItem.getMember('canUp').get_value()##','##DataItem.getMember('isHasExam').get_value()##')"
                                title="添加学员" href="#" class="underline"><b>添加</b></a> &nbsp; <a onclick="javascript:if(!confirm('您确定要删除此培训班所有学员吗？')){return;}DropEmp('## DataItem.getMember('train_plan_post_class_id').get_value() ##','##DataItem.getMember('train_plan_post_class_org_id').get_value()##');"
                                    title="清空学员" href="#"><b>清空</b></a>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
            </div>
        </div>
        <asp:HiddenField ID="hfAllEmpID" runat="server" />
        <asp:HiddenField ID="hfClassID" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
         <asp:HiddenField ID="hfSelectEmpID" runat="server" />
        <asp:Button ID="btnRef" runat="server" Text="" CssClass="displayNone" OnClick="btnRef_Click"/>
    </form>
</body>
</html>
