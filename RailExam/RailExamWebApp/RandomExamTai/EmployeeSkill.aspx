<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeSkill.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.EmployeeSkill" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <base target="_self" />
    <title></title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    function editSkill(id,mode) {
    	if(mode=="add")
    		id=document.getElementById("hfID").value;
    	var returnvalue = window.showModalDialog('EmployeeSkillEdit.aspx?id=' + id +"&mode="+mode+"&num=" + Math.random(),
    			'', 'help:no; status:no; dialogWidth:640px;dialogHeight:300px');
    	if(returnvalue)
    	{
    		__doPostBack("btnDelete", "ref");
    		alert("数据保存成功！");
    	}
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
                    <input id="btnAdd" type="button" value="新  增" class="button" onclick="editSkill('','add')" />
                    <asp:Button ID="btnDelete" runat="server" Style="display: none" OnClick="btnDelete_Click" />
                </div>
                <div style="text-align: center;">
                    <ComponentArt:Grid ID="grdEntity" runat="server" PageSize="15" Width="850px">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="employee_skill_id">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="employee_skill_id" Visible="false" />
                                    <ComponentArt:GridColumn DataField="appoint_time1" HeadingText="聘任时间" />
                                    <ComponentArt:GridColumn DataField="qualification_time1" HeadingText="取得资格时间" />
                                    <ComponentArt:GridColumn DataField="oname" HeadingText="原等级" />
                                    <ComponentArt:GridColumn DataField="nname" HeadingText="现等级" />
                                    <ComponentArt:GridColumn DataField="oldSafe" HeadingText="原安全等级" />
                                    <ComponentArt:GridColumn DataField="newSafe" HeadingText="现安全等级" />
                                    <ComponentArt:GridColumn DataField="certificate_no" HeadingText="证书编号" />
                                    <ComponentArt:GridColumn DataField="appoint_order_no" HeadingText="聘任令号" />
                                    <ComponentArt:GridColumn DataField="create_date1" HeadingText="修改时间" />
                                    <ComponentArt:GridColumn DataField="create_person" HeadingText="修改人" />
                                    <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="CTEdit">
                                <a onclick="editSkill(##DataItem.getMember('employee_skill_id').get_value()##,'edit')"
                                    title="修改技能动态" href="#" class="underline"><b>修改</b></a> <a onclick="javascript:if(!confirm('您确定要删除此技能动态吗？')){return;}__doPostBack('btnDelete','## DataItem.getMember('employee_skill_id').get_value() ##');"
                                        title="删除技能动态" href="#"><b>删除</b></a>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfID" runat="server" />
        <asp:HiddenField runat="server" ID="hfIsServerCenter"/>
        </form>
</body>
</html>
