<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TJ_EmployeeWorkerGroupHeaderByOrgDetail.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.TJ_EmployeeWorkerGroupHeaderByOrgDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>各单位班组长人数统计详细信息</title>

    <script type="text/javascript">
    function SelectedRow(row)
    {
        var grid = document.getElementById("GridView1");
	    for (var i = 2; i < grid.rows.length; i++) 
	    {
	        if(i%2==0)
	        {
	            grid.rows(i).style.backgroundColor = "White";
	        }
	        else
	        {
	            grid.rows(i).style.backgroundColor = "#EFF3FB";
	        }
	    }
	    row.style.backgroundColor = "#FFEEC2"; 
    }
    
    function OpenEmployeeArchives(employeeID)
    {
        var url = "EmployeeArchives.aspx?ID=" + employeeID;
        var name = "档案";
        var features="width=1024px, height=768px";
        
        open(url,name,features);
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <asp:GridView ID="GridView1" runat="server" Width="99%" AllowPaging="True" PageSize="30"
                AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None"
                OnPageIndexChanging="GridView1_PageIndexChanging" OnRowDataBound="GridView1_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="职工ID" SortExpression="employee_id" Visible="False">
                        <EditItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("employee_id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("employee_id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="employee_name" HeaderText="职工姓名" ReadOnly="True" SortExpression="employee_name" />
                    <asp:BoundField DataField="sex" HeaderText="性别" ReadOnly="True" SortExpression="sex" />
                    <asp:BoundField DataField="post_name" HeaderText="职名" ReadOnly="True" SortExpression="职名" />
                    <asp:BoundField DataField="short_name" HeaderText="站段" ReadOnly="True" SortExpression="short_name" />
                </Columns>
                <RowStyle BackColor="#EFF3FB" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
