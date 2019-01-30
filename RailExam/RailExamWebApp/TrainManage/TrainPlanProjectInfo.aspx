<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TrainPlanProjectInfo.aspx.cs"
    Inherits="RailExamWebApp.TrainManage.TrainPlanProjectInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>培训项目信息</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
        function selectArow(rowIndex) {
            var t = document.getElementById("grdEntity");
            for (var i = 1; i < t.rows.length; i++) {
                if (i - 1 == rowIndex) {
                    t.rows(i).style.backgroundColor = "#FFEEC2";
                }
                else {
                    if ((i - 1) % 2 == 0) {
                        t.rows(i).style.backgroundColor = "#EFF3FB";
                    }
                    else {
                        t.rows(i).style.backgroundColor = "White";
                    }
                }
            }
        }
        
        function editProject(id)
        {
        	if(document.getElementById("hfUpdate").value == "False") {
        		 alert("您没有权限使用该操作！");                       
                        return;
        	}
            if(id!=null)
            {
                var scrH=screen.height;
                var scrW=screen.width;
                var top=scrH/2-20;
                var left=scrW/2-150;
                var features="width=300px,height=1px,top="+top+",left="+left+",menubar=no,toolbar=no,location=no,scrollbar=no,resizable=no,status=no";
            	var typeID = document.getElementById("hfTypeID").value;
                
                open("TrainPlanProjectDetail.aspx?id="+id+"&typeID="+typeID,"TrainPlanProjectDetail",features);
            }
        }
        
       function deleteProject() {
        	if(document.getElementById("hfDelete").value == "False") {
        		 alert("您没有权限使用该操作！");                       
                        return false;
        	}
        	
        	return confirm('您确定要删除此培训项目吗？');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <yyc:SmartGridView ID="grdEntity" runat="server" DataKeyNames="trainplan_project_id"
                PageSize="15" AllowSorting="True" OnRowCreated="grdEntity_RowCreated" DataSourceID="ObjectDataSource1">
                <Columns>
                    <asp:BoundField DataField="trainplan_project_id" Visible="False" />
                    <asp:BoundField DataField="trainplan_type_id" Visible="False" />
                    <asp:BoundField DataField="trainplan_project_name" HeaderText="培训项目名称" SortExpression="trainplan_type_name">
                    </asp:BoundField>
                    <asp:TemplateField>
                        <headertemplate>
                            操作                               
                        </headertemplate>
                        <itemtemplate>
                            <a class="underline" onclick='editProject(<%# Eval("trainplan_project_id")%>)' href="#"><b>编辑</b></a> 
                            <asp:LinkButton id="btnDelete" onclick="btnDelete_Click" runat="server" CssClass="underline" CommandName="del" OnClientClick="return deleteProject();" CommandArgument='<%#Eval("trainplan_project_id")%>'><b>删除</b></asp:LinkButton> 
                        </itemtemplate>
                        <headerstyle width="20%" horizontalalign="Center" wrap="False" />
                        <itemstyle width="20%" horizontalalign="Center" wrap="False" />
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <itemtemplate>
                            <asp:Label id="lblID" runat="server" Text='<%# Eval("trainplan_project_id") %>'></asp:Label> 
                        </itemtemplate>
                    </asp:TemplateField>
                </Columns>
            </yyc:SmartGridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="OjbData"
                OnSelected="ObjectDataSource1_Selected">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfSelect" Type="String" PropertyName="Value" Name="sql" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:HiddenField ID="hfSelect" runat="server" />
           <asp:HiddenField ID="hfTypeID" runat="server" /> 
            <asp:HiddenField runat="server" ID="hfUpdate" />
            <asp:HiddenField ID="hfDelete" runat="server" />
        </div>
    </form>
</body>
</html>
