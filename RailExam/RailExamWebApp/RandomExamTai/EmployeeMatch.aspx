<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeMatch.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.EmployeeMatch" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <base target="_self" />
    <title></title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    function editMatch(id,mode) {
    	if(mode=="add")
    		id=document.getElementById("hfID").value;
    	var returnvalue = window.showModalDialog('EmployeeMatchEdit.aspx?id=' + id +"&mode="+mode+"&num=" + Math.random(),
    			'', 'help:no; status:no; dialogWidth:640px;dialogHeight:330px');
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
                    <input id="btnAdd" type="button" value="新  增" class="button" onclick="editMatch('','add')" />
                    <asp:Button ID="btnDelete" runat="server" Style="display: none" OnClick="btnDelete_Click" />
                </div>
                <div style="text-align: center;">
                    <ComponentArt:Grid ID="grdEntity" runat="server" PageSize="15" Width="850px">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="employee_match_id">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="employee_match_id" Visible="false" />
                                    <ComponentArt:GridColumn DataField="match_date1" HeadingText="竞赛时间" />
                                    <ComponentArt:GridColumn DataField="unit" HeadingText="举办单位" />
                                    <ComponentArt:GridColumn DataField="match_project" HeadingText="竞赛项目" />
                                    <ComponentArt:GridColumn DataField="match_type" HeadingText="竞赛类别" />
                                    <ComponentArt:GridColumn DataField="total_score" HeadingText="总成绩" />
                                    <ComponentArt:GridColumn DataField="lilun_score" HeadingText="理论成绩" />
                                    <ComponentArt:GridColumn DataField="shizuo_score" HeadingText="实作成绩" />
                                    <ComponentArt:GridColumn DataField="match_rank" HeadingText="竞赛名次" />
                                    <ComponentArt:GridColumn DataField="memo" HeadingText="备注" Visible="False" />
                                    <ComponentArt:GridColumn DataField="create_date1" HeadingText="修改时间" />
                                    <ComponentArt:GridColumn DataField="create_person" HeadingText="修改人" />
                                    <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="CTEdit">
                                <a onclick="editMatch(##DataItem.getMember('employee_match_id').get_value()##,'edit')"
                                    title="修改技能竞赛情况" href="#" class="underline"><b>修改</b></a> <a onclick="javascript:if(!confirm('您确定要删除此技能竞赛情况吗？')){return;}__doPostBack('btnDelete','## DataItem.getMember('employee_match_id').get_value() ##');"
                                        title="删除技能竞赛情况" href="#"><b>删除</b></a>
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
