<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" Codebehind="EmployeeInfo.aspx.cs"
    Inherits="RailExamWebApp.TrainManage.EmployeeInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>学员信息</title>
    <base target="_self"/>
   <script src="../Common/JS/jquery.js" type="text/javascript"></script>
<script src="../Common/JS/jquery1.3.2.js" type="text/javascript"></script>
    <script type="text/javascript">
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
	          	/*var isHasExam = document.getElementById("HasExam").value;
	          	if(isHasExam=="1") {
    		            alert("已存在该培训班的考试，不能删除！");
    		            return false;
    	       }*/
	          	var ids = [];
	          	var theItem;
	          	for (var i = 0; i < grdEntity.get_table().getRowCount(); i++)
	          	{
	          		theItem = grdEntity.getItemFromClientId(grdEntity.get_table().getRow(i).get_clientId());
	          		if ($.$F("cbxSelectItem_" + theItem.GetProperty("Id")) != null)
	          		{
	          			if ($.$F("cbxSelectItem_" + theItem.GetProperty("Id")).checked)
	          				ids.push(theItem.GetProperty("Id"));
	          		}
	          	}
	          	document.getElementById("hfSelectedIDs").value = ids.join(',');
	          	if (document.getElementById("hfSelectedIDs").value.length > 0)
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

	          function clo() {
	          	var isRef = document.getElementById("hfIsRef");
	          	window.returnValue = isRef.value;
	          	window.close();
	          }
	          function DeleteEmp(planEmpID) 
	          {
	          	/*var isHasExam = document.getElementById("HasExam").value;
	          	if(isHasExam=="1") 
	          	{
    		            alert("已存在该培训班的考试，不能删除！");
    		            return;
    	        }*/
	          	
	          	if(!confirm('您确定要删除此学员吗？')) 
	          	{
	          		return;
	          	}
	          	
	          	document.getElementById("hfplanEmpID").value = planEmpID;
	          	document.getElementById("btnDelete").click();
	          }

	          function isRef() {
	           	var isRef = document.getElementById("hfIsRef");
	          	window.returnValue = isRef.value;
	          }
	          
	    function exportExcel()
        {
	    	var id = document.getElementById("hfID").value;
            var ret =  window.showModalDialog("/RailExamBao/TrainManage/ExportExcel.aspx?classOrgID="+id+"&Type=upEmployee",'','help:no;status:no;dialogWidth:320px;dialogHeight:30px;');
            if(ret != "")
            {
             form1.hfRefresh.value = ret;
             form1.submit();
           }
        }
    </script>

</head>
<body onunload="isRef()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="current" runat="server">
                        学员信息</div>
                </div>
            </div>
            <div id="content">
                <div style="text-align: right;">
                    <input type="button" class="buttonLong" value="导出上报学员" onclick="exportExcel()"/>
                    <asp:Button ID="btnDelete" runat="server" Text="删除学员" CssClass="displayNone" OnClick="btnDelete_Click" />
                    <asp:Button ID="btnDelAll" runat="server" Text="删除所选" CssClass="button" OnClientClick="return getSelectedItems()"
                        OnClick="btnDelAll_Click" />
                </div>
                <div style="text-align: center; height: 580px">
                    <ComponentArt:Grid ID="grdEntity" runat="server" PageSize="10" Width="98%" >
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="TRAIN_PLAN_EMPLOYEE_ID">
                                <Columns>
                                    <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="Delete" Width="40"
                                        HeadingText="&lt;input  type='checkbox' onclick='btnAllClicked()' name='btnAll' style='border: none' /&gt;" />
                                    <ComponentArt:GridColumn DataField="TRAIN_PLAN_EMPLOYEE_ID" Visible="false" />
                                    <ComponentArt:GridColumn DataField="train_plan_post_class_id" Visible="false" />
                                    <ComponentArt:GridColumn DataField="unit" HeadingText="单位" Align="Center" />
                                    <ComponentArt:GridColumn DataField="workshop" HeadingText="车间" Align="Center" />
                                    <ComponentArt:GridColumn DataField="group" HeadingText="班组" Align="Center" />
                                    <ComponentArt:GridColumn DataField="post_name" HeadingText="职名" Align="Center" />
                                    <ComponentArt:GridColumn DataField="employee_name" HeadingText="姓名" Align="Center" />
                                    <ComponentArt:GridColumn DataField="work_no" HeadingText="员工编码" Align="Center" />
                                    <ComponentArt:GridColumn DataField="identity_cardno" HeadingText="身份证号" Align="Center" />
                                    <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="CTEdit">
                                <a onclick="DeleteEmp('## DataItem.getMember('TRAIN_PLAN_EMPLOYEE_ID').get_value() ##');"
                                    title="删除学员" href="#"><b>删除</b></a>
                            </ComponentArt:ClientTemplate>
                            <ComponentArt:ClientTemplate ID="Delete">
                                <input style="border: none" id="cbxSelectItem_##DataItem.getMember('TRAIN_PLAN_EMPLOYEE_ID').get_value()##"
                                    name="cbxSelectItem_##DataItem.getMember('TRAIN_PLAN_EMPLOYEE_ID').get_value()##"
                                    type="checkbox" value="##DataItem.getMember('TRAIN_PLAN_EMPLOYEE_ID').get_value()##" />
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                    <div>
                     <input type="button" class="button" value="确  定" onclick="clo()"/>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfIsRef" runat="server" />
        <asp:HiddenField ID="hfplanEmpID" runat="server" />
        <asp:HiddenField ID="hfSelectedIDs" runat="server" />
      <input type="hidden" name="hfRefresh"/>
      <asp:HiddenField runat="server" ID="hfID"/> 
      <asp:HiddenField runat="server" ID="HasExam"/>
    </form>
</body>
</html>
