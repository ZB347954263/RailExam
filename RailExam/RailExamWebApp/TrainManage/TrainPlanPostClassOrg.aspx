<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TrainPlanPostClassOrg.aspx.cs"
    Inherits="RailExamWebApp.TrainManage.TrainPlanPostClassOrg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>新增计划培训班站段</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
     function SelectEmployee() {
     	var EmpIDs = document.getElementById("hfAllSeletedEmpID").value;
	    	var arryEmpID = new Array();
	    	arryEmpID = EmpIDs.split(',');
	        var employeeIDs =  window.showModalDialog('../Common/SelectMoreEmployee.aspx?EmpID='+arryEmpID, 
                    '', 'help:no; status:no; dialogWidth:'+window.screen.width+'px;dialogHeight:'+ window.screen.height+'px;scroll:no;');
     	document.getElementById("hfEmpID").value = employeeIDs;
     }

     function SelectOrg() {
     	var orgIDs =  window.showModalDialog('../Common/SelectOrganization.aspx?Type=Station', 
                    '', 'help:no; status:no; dialogWidth:310px;dialogHeight:600px;scroll:no;');
     	if(orgIDs!=undefined)
     	{
     		document.getElementById("hfOrg").value = orgIDs.split("|")[0];
     		document.getElementById("txtOrg").innerText=orgIDs.split("|")[1];
     	}

     }
     function check() {
     	var spanPost = document.getElementById("spanPost");
      	if( spanPost!=null)
      		{
      	if(spanPost.className=="require") {
      		var dropPost = document.getElementById("dropPlanPost");
      		if (dropPost.value == "0") {
      			alert("请选择职名！");
      			return false;
      		}
      	}
      	}
     	
     	var dropClass=document.getElementById("dropPlanClass");
     	if(dropClass.value=="0") {
     		alert("请选择培训班！");
     		return false;
     	}
     	var org = document.getElementById("hfOrg");
     	if(org.value=="") {
     		alert("请选择单位！");
     		return false;
     	}
     	var num = document.getElementById("txtNum");
     	if(num!=undefined)
     	{
     		if (num.value == "") {
     			alert("请输入上报人数！");
     			num.focus();
     			return false;
     		}
     	}
     }
     function checkNum(obj) {
     	var reg = /^\d+()?$/ ;
      	if (obj.value != "")
      	{
      		if (!reg.test(obj.value))
      		{
      			alert("请输入整数！");
      			obj.value = "";
      			obj.focus();
      			return;
      		}
      		 
      	}
     }
 
      
       function SelectMoreOrg() {

       	var classId = document.getElementById("dropPlanClass").value;
       	if(classId == "0") {
       		alert("请选择培训班！");
     		return;
       	}
       	 
     	 var orgIDs =  window.showModalDialog('../Common/SelectMoreOrgGroupBySystem.aspx?classId='+classId+'&selectOrgID='+document.getElementById("hfOrgIDs").value+'&math='+Math.random(), 
                    '', 'help:no; status:no; dialogWidth:310px;dialogHeight:600px;scroll:no;');
      
      	if(orgIDs!=undefined)
      	{
      		document.getElementById("hfOrgIDs").value = orgIDs;
      		 document.getElementById("hfOrg").value = orgIDs;
      		form1.submit();
      	}
      }
      function deleteInfo (obj) {
      	document.getElementById("hfdelOrgID").value = obj;
      	if(confirm("确定要删除？"))
      		document.getElementById("btnDelete").click();
      }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head" style="width: 530px">
                <div id="location">
                    <div id="current" runat="server">
                        新增计划培训班站段</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable" width="400px">
                    <tr>
                        <td style="width: 15%">
                            计划职名<span runat="server" id="spanPost" class="">*</span></td>
                        <td>
                            <asp:DropDownList ID="dropPlanPost" AutoPostBack="true" runat="server" OnSelectedIndexChanged="dropPlanPost_SelectedIndexChanged">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            培训班名称<span class="require">*</span></td>
                        <td>
                            <asp:DropDownList ID="dropPlanClass" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr runat="server" id="trUpdateOrg">
                        <td style="width: 15%">
                            单位<span class="require">*</span>
                        </td>
                        <td>
                            <asp:TextBox ID="txtOrg" runat="server" Width="150px" Enabled="false"></asp:TextBox>
                            <a href="#" title="选择单位" onclick="SelectOrg()" style="display: none;">
                                <asp:Image runat="server" ID="Image1" ImageUrl="../Common/Image/search.gif" AlternateText="选择单位">
                                </asp:Image>
                            </a>
                        </td>
                    </tr>
                    <tr runat="server" id="trUpdate">
                        <td style="width: 15%; height: 35px;">
                            计划上报人数<span class="require">*</span>
                        </td>
                        <td style="height: 35px">
                            <asp:TextBox ID="txtNum" runat="server" onblur="checkNum(this)" MaxLength="8"></asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="trNew">
                        <td style="width: 15%">
                            单位<span class="require">*</span>
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtMprOrgName" runat="server" Width="420px" Enabled="false"></asp:TextBox>
                            <a href="#" title="选择单位" onclick="SelectMoreOrg()">
                                <asp:Image runat="server" ID="Image4" ImageUrl="../Common/Image/search.gif" AlternateText="选择单位">
                                </asp:Image>
                            </a>
                        </td>
                    </tr>
                </table>
                <div runat="server" id="divAdd">
                    <asp:Button ID="btnDelete" runat="server" class="displayNone" OnClick="btnDelete_Click" />
                    <div id="Div1" style="overflow-y: auto;">
                        <yyc:SmartGridView runat="server" ID="gridInfo" AutoGenerateColumns="False" OnRowDataBoundDataRow="gridInfo_RowDataBoundDataRow"
                            DataKeyNames="orgID" AllowPaging="false">
                            <Columns>
                                <asp:TemplateField HeaderText="上报单位">
                                    <itemtemplate>
                                  <asp:Label id="lblOrgID" runat="server" Visible="false" Text='<%# Eval("orgID") %>'></asp:Label>
                                   <asp:Label id="lblOrgName" runat="server" Text='<%# Eval("orgName") %>'></asp:Label>
                               </itemtemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="上报人数">
                                    <itemtemplate>
                                        <asp:TextBox id="txtUpNum"   onblur="checkNum(this)" style="width: 95%;"  runat="server" Text='<%# Eval("upNum") %>'/>
                                </itemtemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="操作">
                                    <itemtemplate>
                             <a href="#" class="underline" onclick='deleteInfo(<%# Eval("orgID") %>)'>删除</a>
                            </itemtemplate>
                                </asp:TemplateField>
                            </Columns>
                        </yyc:SmartGridView>
                    </div>
                </div>
                <div align="center" style="margin-top: 20px">
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="保  存" OnClientClick="return check()"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnClose" runat="server" CssClass="button" Text="关  闭" OnClientClick="javascript:window.close()" />
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfOrg" runat="server" />
        <asp:HiddenField ID="hfOrgIDs" runat="server" />
        <asp:HiddenField ID="hfdelOrgID" runat="server" />
        <asp:HiddenField ID="oldClassName" runat="server" />
    </form>
</body>
</html>
