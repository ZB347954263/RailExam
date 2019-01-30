<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TrainPlanType.aspx.cs"
    Inherits="RailExamWebApp.TrainManage.TrainPlanType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>培训类别</title>
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
        
        function addType()
        {
        	if(document.getElementById("hfUpdate").value == "False") {
        		 alert("您没有权限使用该操作！");                       
                        return;
        	}
            var scrH=screen.height;
            var scrW=screen.width;
            var top=scrH/2-20;
            var left=scrW/2-150;
            var features="width=300px,height=1px,top="+top+",left="+left+",menubar=no,toolbar=no,location=no,scrollbar=no,status=no";
            open("TrainPlanTypeDetail.aspx","TrainPlanTypeDetail",features);
        }
        
        function editType(id)
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
                open("TrainPlanTypeDetail.aspx?id="+id,"TrainPlanTypeDetail",features);
            }
        }
        
        function deleteType() {
        	if(document.getElementById("hfDelete").value == "False") {
        		 alert("您没有权限使用该操作！");                       
                        return false;
        	}
        	
        	return confirm('您确定要删除此培训类别吗？');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="Div4">
                    <div id="location">
                        <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                        </div>
                        <div id="parent">
                            培训管理</div>
                        <div id="separator">
                        </div>
                        <div id="current">
                            培训类别</div>
                    </div>
                    <div id="welcomeInfo">
                        <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                    </div>
                    <div id="button">
                        <img id="add" onclick="addType();" src="../Common/Image/add.gif" alt="新增培训类别" />
                    </div>
                </div>
            </div>
            <div id="content">
                <div id="rightContentWithNoHead">
                    <yyc:SmartGridView ID="grdEntity" runat="server" DataKeyNames="trainplan_type_id"
                        PageSize="15" AllowSorting="True" OnRowCreated="grdEntity_RowCreated" DataSourceID="ObjectDataSource1">
                        <Columns>
                            <asp:BoundField DataField="trainplan_type_id" Visible="False" />
                            <asp:BoundField DataField="trainplan_type_name" HeaderText="培训类别名称" SortExpression="trainplan_type_name">
                            </asp:BoundField>
                            <asp:TemplateField>
                                <headertemplate>
                                    操作                               
                                </headertemplate>
                                <itemtemplate>
                                    <a class="underline" onclick='editType(<%# Eval("trainplan_type_id")%>)' href="#"><b>编辑</b></a> 
                                    <asp:LinkButton id="btnDelete" onclick="btnDelete_Click" runat="server" CssClass="underline" CommandName="del" OnClientClick="return deleteType();" CommandArgument='<%#Eval("trainplan_type_id")%>'><b>删除</b></asp:LinkButton> 
                                </itemtemplate>
                                <headerstyle width="20%" horizontalalign="Center" wrap="False" />
                                <itemstyle width="20%" horizontalalign="Center" wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False">
                                <itemtemplate>
                                    <asp:Label id="lblID" runat="server" Text='<%# Eval("trainplan_type_id") %>'></asp:Label> 
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
                </div>
            </div>
        </div>
        <asp:HiddenField runat="server" ID="hfUpdate" />
        <asp:HiddenField ID="hfDelete" runat="server" />
    </form>
</body>
</html>
