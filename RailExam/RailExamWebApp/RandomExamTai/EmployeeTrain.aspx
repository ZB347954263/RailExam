<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeTrain.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.EmployeeTrain" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">  <base target="_self" />
    <title></title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    function forbiddenAdd()
    {
        var search=location.search;
        var index=search.indexOf("Type");
        if(index!=-1)
        {
            var type=search.substr(index+5,1);
            if(type=="0")
            {
                var btn=document.getElementById("btnRef");
                btn.style.display="none";
            }
        }
    	
    	if(document.getElementById("hfIsServerCenter").value=="False") {
    		var btn=document.getElementById("btnRef");
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
                    <asp:Button ID="btnRef"  runat="server"  class="button" Text="更  新" OnClick="btnRef_Click" />
                    <asp:Button ID="btnDelete" runat="server" Style="display: none" OnClick="btnDelete_Click" />
                </div>
                <div style="text-align: center;">
                    <ComponentArt:Grid ID="grdEntity" runat="server" PageSize="15" Width="850px">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="employee_train_id">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="employee_train_id" Visible="false" />
                                    <ComponentArt:GridColumn DataField="train_class_name" HeadingText="培训班名称" />
                                    <ComponentArt:GridColumn DataField="trainplan_type_name" HeadingText="培训类别" />
                                    <ComponentArt:GridColumn DataField="train_subject" HeadingText="培训科目" />
                                    <ComponentArt:GridColumn DataField="begin_date1" HeadingText="开始时间" />
                                    <ComponentArt:GridColumn DataField="end_date1" HeadingText="结束时间" />
                                    <ComponentArt:GridColumn DataField="class_hour_count" HeadingText="培训时间" />
                                    <ComponentArt:GridColumn DataField="location" HeadingText="地点" />
                                    <ComponentArt:GridColumn DataField="create_date1" HeadingText="更新时间" />
                                    <ComponentArt:GridColumn DataField="create_person" HeadingText="更新人" />
                                    <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="CTEdit">
                     <a onclick="javascript:if(!confirm('您确定要删除此培训情况吗？')){return;}__doPostBack('btnDelete','## DataItem.getMember('employee_train_id').get_value() ##');"
                                        title="删除培训情况" href="#"><b>删除</b></a>
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

