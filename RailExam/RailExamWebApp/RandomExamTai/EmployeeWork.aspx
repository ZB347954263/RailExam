<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeeWork.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.EmployeeWork"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title></title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    function editWork(id,mode,oldStationID) {
    	var stationOrgID = document.getElementById("hfStationID").value;
    	if(oldStationID!=stationOrgID && mode=="edit") {
    		alert("您没有该操作的权限!");
    		return;
    	}
    	if(mode=="add")
    		id=document.getElementById("hfID").value;
    	var returnvalue = window.showModalDialog('EmployeeWorkEdit.aspx?id=' + id +"&mode="+mode+"&num=" + Math.random(),
    			'', 'help:no; status:no; dialogWidth:640px;dialogHeight:300px');
    	if(returnvalue)
    	{
    		__doPostBack("btnDelete", "ref");
    		//alert("数据保存成功！");
    	}
    }

    function deleteInfo(workID,oldStationID) {
    		var stationOrgID = document.getElementById("hfStationID").value;
//    		if(oldStationID!=stationOrgID) {
//    		alert("您没有该操作的权限!");
//    		return;
//    	}
    	if(!confirm('您确定要删除此工作动态吗？'))
    	{return;}
    	__doPostBack('btnDelete', workID);
    }
    function forbiddenAdd()
    {
        var search=location.search;
        var index=search.indexOf("Type");
        if(index!=-1)
        {
            var type=search.substr(index+5,1);
            if(type=="0")
            {
                var btn=document.getElementById("btnAdd");
                btn.style.display="none";
            }
        }
    	
    	if(document.getElementById("hfIsServerCenter").value=="False") {
    		var btn=document.getElementById("btnAdd");
            btn.style.display="none";
    	}
    }
    </script>

</head>
<body onload="forbiddenAdd()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div style="text-align: right;">
                    <input id="btnAdd" type="button" value="新  增" class="button" onclick="editWork('','add')" />
                    <asp:Button ID="btnDelete" runat="server" Style="display: none" OnClick="btnDelete_Click" />
                </div>
                <div style="text-align: center;">
                    <ComponentArt:Grid ID="grdEntity" runat="server" PageSize="15" Width="850px">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="employee_work_id">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="employee_work_id" Visible="false"/>
                                    <ComponentArt:GridColumn DataField="oldStationID" Visible="false"  />
                                    <ComponentArt:GridColumn DataField="oorg" HeadingText="原机构" />
                                    <ComponentArt:GridColumn DataField="norg" HeadingText="现机构"  />
                                    <ComponentArt:GridColumn DataField="opost_name" HeadingText="原职名"  />
                                    <ComponentArt:GridColumn DataField="npost_name" HeadingText="现职名"  />
                                    <ComponentArt:GridColumn DataField="mobilizationorderno" HeadingText="调令"  />
                                    <ComponentArt:GridColumn DataField="transfer_date" HeadingText="异动时间" />
                                    <ComponentArt:GridColumn DataField="create_date" HeadingText="修改时间" FormatString="yyyy-MM-dd"/>
                                    <ComponentArt:GridColumn DataField="create_person" HeadingText="修改人"  />
                                    <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="CTEdit">
                                <a onclick="editWork(##DataItem.getMember('employee_work_id').get_value()##,'edit',##DataItem.getMember('oldStationID').get_value()##)"
                                    title="修改工作动态" href="#" class="underline"><b>修改</b></a> 
                                    <a onclick="deleteInfo(##DataItem.getMember('employee_work_id').get_value()##,##DataItem.getMember('oldStationID').get_value()##)"
                                        title="删除工作动态" href="#"><b>删除</b></a>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfID" runat="server" />
          <asp:HiddenField ID="hfStationID" runat="server" />
         <asp:HiddenField runat="server" ID="hfIsServerCenter"/> 
    </form>
</body>
</html>
