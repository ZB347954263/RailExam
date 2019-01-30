<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeeEducation.aspx.cs" EnableEventValidation="false"
    Inherits="RailExamWebApp.RandomExamTai.EmployeeEducation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title></title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    function editEducation(id,mode) {
    	if(mode=="add")
    		id=document.getElementById("hfID").value;
    	var returnvalue = window.showModalDialog('EmployeeEducationEdit.aspx?id=' + id +"&mode="+mode+"&num=" + Math.random(),
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
                    <input id="btnAdd" type="button" value="新  增" class="button" onclick="editEducation('','add')" />
                    <asp:Button ID="btnDelete" runat="server" Style="display: none" OnClick="btnDelete_Click" />
                </div>
                <div style="text-align: center;">
                    <ComponentArt:Grid ID="grdEntity" runat="server" PageSize="15" Width="850px">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="employee_education_id">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="employee_education_id" Visible="false" />
                                    <ComponentArt:GridColumn DataField="oname" HeadingText="原学历" />
                                    <ComponentArt:GridColumn DataField="nname" HeadingText="新学历" />
                                    <ComponentArt:GridColumn DataField="school_subject" HeadingText="学习专业" />
                                    <ComponentArt:GridColumn DataField="study_style" HeadingText="学习形式" />
                                    <ComponentArt:GridColumn DataField="diploma_no" HeadingText="毕业证号" />
                                    <ComponentArt:GridColumn DataField="graducate_school" HeadingText="毕业学校" />
                                    <ComponentArt:GridColumn DataField="school_type1" HeadingText="学校类别" />
                                      <ComponentArt:GridColumn DataField="graduate_date1" HeadingText="毕业时间" />
                                    <ComponentArt:GridColumn DataField="create_date1" HeadingText="修改时间" />
                                    <ComponentArt:GridColumn DataField="create_person" HeadingText="修改人" />
                                    <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="CTEdit">
                                <a onclick="editEducation(##DataItem.getMember('employee_education_id').get_value()##,'edit')"
                                    title="修改学习动态" href="#" class="underline"><b>修改</b></a> <a onclick="javascript:if(!confirm('您确定要删除此学习动态吗？')){return;}__doPostBack('btnDelete','## DataItem.getMember('employee_education_id').get_value() ##');"
                                        title="删除学习动态" href="#"><b>删除</b></a>
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
