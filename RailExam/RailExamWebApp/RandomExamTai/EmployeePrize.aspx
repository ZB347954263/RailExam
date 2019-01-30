<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeePrize.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.EmployeePrize" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <base target="_self" />
    <title></title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    function editPrize(id,mode) {
    	if(mode=="add")
    		id=document.getElementById("hfID").value;
    	var returnvalue = window.showModalDialog('EmployeePrizeEdit.aspx?id=' + id +"&mode="+mode+"&num=" + Math.random(),
    			'', 'help:no; status:no; dialogWidth:640px;dialogHeight:380px');
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
                    <input id="btnAdd" type="button" value="新  增" class="button" onclick="editPrize('','add')" />
                    <asp:Button ID="btnDelete" runat="server" Style="display: none" OnClick="btnDelete_Click" />
                </div>
                <div style="text-align: center;">
                    <ComponentArt:Grid ID="grdEntity" runat="server" PageSize="15" Width="850px">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="employee_prize_id">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="employee_prize_id" Visible="false" />
                                    <ComponentArt:GridColumn DataField="prize_date1" HeadingText="奖惩日期" Width="100"/>
                                    <ComponentArt:GridColumn DataField="prize_type1" HeadingText="奖惩类别" Width="60"/>
                                    <ComponentArt:GridColumn DataField="prize_no" HeadingText="奖惩文号" Width="60"/>
                                    <ComponentArt:GridColumn DataField="content_brief" HeadingText="事迹概况" Width="200" />
                                    <ComponentArt:GridColumn DataField="prize_result" HeadingText="奖惩结果" Visible="False"/>
                                    <ComponentArt:GridColumn DataField="memo" HeadingText="备注" Visible="False" />
                                    <ComponentArt:GridColumn DataField="create_date1" HeadingText="修改时间" Width="100"/>
                                    <ComponentArt:GridColumn DataField="create_person" HeadingText="修改人" Width="80"/>
                                    <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" Width="80" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="CTEdit">
                                <a onclick="editPrize(##DataItem.getMember('employee_prize_id').get_value()##,'edit')"
                                    title="修改奖惩情况" href="#" class="underline"><b>修改</b></a> <a onclick="javascript:if(!confirm('您确定要删除此奖惩情况吗？')){return;}__doPostBack('btnDelete','## DataItem.getMember('employee_prize_id').get_value() ##');"
                                        title="删除奖惩情况" href="#"><b>删除</b></a>
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
