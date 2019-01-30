<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ItemCount.aspx.cs" Inherits="RailExamWebApp.Item.ItemCount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试题统计</title>
    <link href="../Style/css.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
      function openPage()
      {
      	var url = "../Common/SelectPost.aspx?src=book&num="+Math.random();

      	var selectedPost = window.showModalDialog(url,
      		'', 'help:no; status:no; dialogWidth:300px;dialogHeight:620px;scroll:no;');

      	if (!selectedPost)
      	{
      		return;
      	}

      	var id = selectedPost.split('|')[0];
      	var text = selectedPost.split('|')[1];

      	document.getElementById("txtPost").value = text;
      		document.getElementById("hfPost").value = text;
      	document.getElementById("hfPostID").value = id;
      	document.getElementById("bstnQuery").click();
      }
      
      function selectArow(obj)
      {
      	var TBL = document.getElementById("grdBook");
      	for (var i = 1; i < TBL.rows.length; i++)
      	{
      		TBL.rows[i].style.backgroundColor = "White";
      		if (i % 2 != 0)
      			TBL.rows[i].style.backgroundColor = "#EFF3FB";
      	}
      	obj.style.backgroundColor = "#FFEEC2";

      }
      
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        题库管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        参数配置</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
            </div>
            <div id="gridPage">
                <div class="queryPost" style="text-align: left; width: 98%">
                    &nbsp;职&nbsp;&nbsp;名
                    <asp:TextBox ID="txtPost" runat="server" Enabled="false"></asp:TextBox>&nbsp;
                    <input type="button" id="btnSelectPost" class="button" value="选择职名" onclick="openPage()" />
                    <asp:Button ID="bstnQuery" runat="server" Text="查 询" CssClass="displayNone" OnClick="btnQuery_Click" />
                </div>
                <div id="grid">
                    <yyc:SmartGridView ID="grdBook" runat="server" AutoGenerateColumns="False" PageSize="15"
                        AllowPaging="True" DataKeyNames="book_id" AllowSorting="True" DataSourceID="ObjectDataSourceBook"
                        Width="98%" OnRowCreated="grdBook_RowCreated">
                        <Columns>
                            <asp:BoundField HeaderText="教材名称" DataField="book_name" SortExpression="book_name">
                                <headerstyle width="200px" />
                                <itemstyle width="200px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="编制单位" DataField="short_name" SortExpression="short_name">
                                <headerstyle width="100px" />
                                <itemstyle width="100px" Wrap="false"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="xiaoji" HeaderText="小计" SortExpression="xiaoji">
                                <headerstyle width="50px" />
                                <itemstyle width="50px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="单选" DataField="dan" SortExpression="dan">
                                <headerstyle width="50px" />
                                <itemstyle width="50px" Wrap="false"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="多选" DataField="duo" SortExpression="duo">
                                <headerstyle width="50px" />
                                <itemstyle width="50px" Wrap="false"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="判断" DataField="pan" SortExpression="pan">
                                <headerstyle width="50px" />
                                <itemstyle width="50px" Wrap="false"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="综合选择" DataField="wan" SortExpression="wan">
                                <headerstyle width="60px" />
                                <itemstyle width="60px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="完形填空子题" DataField="wanz" SortExpression="wanz" Visible="false" />
                        </Columns>
                    </yyc:SmartGridView>
                </div>
            </div>
            <div>
                <asp:ObjectDataSource ID="ObjectDataSourceBook" runat="server" SelectMethod="Get"
                    TypeName="OjbData" OnSelected="ObjectDataSourceBook_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hfSelect" Name="sql" PropertyName="Value" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <input type="hidden" runat="server" id="hfPostID" />
                <asp:HiddenField ID="hfSelect" runat="server" />
                <asp:HiddenField ID="hfPost" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
