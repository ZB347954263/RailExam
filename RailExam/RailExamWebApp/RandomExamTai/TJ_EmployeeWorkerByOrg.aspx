<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TJ_EmployeeWorkerByOrg.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.TJ_EmployeeWorkerByOrg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>各单位工人总数统计</title>

    <script type="text/javascript">
      function exportTemplate()
      {
      	document.getElementById("btnExcel").click();
      	var ret = window.showModalDialog("TJ_ExportTemplate.aspx?type=EmployeeWorkerByOrg&math="+Math.random(), '', 'help:no;status:no;dialogWidth:320px;dialogHeight:30px;');
      	if (ret != "")
      	{
      		document.getElementById("hfRefreshExcel").value = ret;
      			document.getElementById("btnExcel").click();
      	}
      }
      
     function  getOrg() {
        document.getElementById("hfOrgID").value=window.parent.document.getElementById("ddlOrg").value;
     	return true;
     }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: right">
            <asp:Button ID="btnQuery" runat="server" Text="查  询" CssClass="button" OnClick="btnQuery_Click"  OnClientClick="return getOrg();" />
            <input type="button" class="button" onclick="exportTemplate()" value="导出Excel" />
            <asp:Button ID="btnExcel" runat="server" Text="" OnClick="btnExcel_Click" Style="display: none;" />
        </div>
        <div>
            <ComponentArt:Grid ID="grid1" runat="server" AutoAdjustPageSize="false" PageSize="18">
                <Levels>
                    <ComponentArt:GridLevel>
                        <Columns>
                            <ComponentArt:GridColumn DataField="row_index" HeadingText="序号" />
                            <ComponentArt:GridColumn DataField="full_name" HeadingText="单位" />
                            <ComponentArt:GridColumn DataField="amount" HeadingText="总数" />
                            <ComponentArt:GridColumn DataField="is_on_post" HeadingText="在岗人数" />
                            <ComponentArt:GridColumn DataField="is_not_on_post" HeadingText="不在岗人数" />
                            <ComponentArt:GridColumn DataField="ISREGISTERED" HeadingText="在册人数" />
                            <ComponentArt:GridColumn DataField="IS_not_REGISTERED" HeadingText="不在册人数" />
                            <ComponentArt:GridColumn DataField="Is_on_post_worker" HeadingText="在岗工人人数" />
                            <ComponentArt:GridColumn DataField="Is_on_post_worker_no_photo" HeadingText="在岗工人无照片人数" />
                            <ComponentArt:GridColumn DataField="Is_on_post_worker_no_finger" HeadingText="在岗工人无指纹人数" />
                        </Columns>
                    </ComponentArt:GridLevel>
                </Levels>
            </ComponentArt:Grid>
        </div>
        <asp:HiddenField ID="hfRefreshExcel" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
    </form>
</body>
</html>
