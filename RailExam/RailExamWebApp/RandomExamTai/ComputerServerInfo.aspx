<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ComputerServerInfo.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.ComputerServerInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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

        function AssignAccount(id)
        {
           var flagUpdate=document.getElementById("HfUpdateRight").value; 
	        if(flagUpdate=="False")
             {
                alert("您没有该操作的权限！");
                return;
             } 
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-500)*.5;   
            ctop=(screen.availHeight-240)*.5;  
             
            var re = window.open("../Systems/AssignAccount.aspx?id="+id,'AssignAccount','Width=500px; Height=240px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
	        re.focus();
        }
        
        function updateComputerServer(id)
        {
        	var flagUpdate=document.getElementById("HfUpdateRight").value; 
	        if(flagUpdate=="False")
             {
                alert("您没有该操作的权限！");
                return;
             }
            var orgID=document.getElementById("hfSelectOrg").value;
            location.href = "ComputerServerInfoDetail.aspx?CSID=" + id + "&OrgID="+orgID;
        }        
       
      
       function deleteServer() {
       	var deleteRight = document.getElementById("HfDeleteRight").value;
			
			if(deleteRight == "False" ) {
				alert("您没有该操作的权限！");
				return false;
			}
       	
       	 return confirm('您确定要删除此服务器吗？');
       }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="query" style="display: none;">
                &nbsp;&nbsp;服务器名称
                <asp:TextBox ID="txtName" runat="server" Width="100px"></asp:TextBox>
                <%--<asp:ImageButton ID="btnQuery" runat="server" CausesValidation="False" OnClick="btnQuery_Click"
                    ImageUrl="../Common/Image/confirm.gif" />--%>
                IP地址
                <asp:TextBox ID="txtIP" runat="server" Width="100px"></asp:TextBox>
                <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="确  定" OnClick="btnQuery_Click" />
            </div>
            <div id="left" style="width: 100%">
                <div id="leftContent">
                    <yyc:SmartGridView ID="grdEntity" runat="server" DataKeyNames="COMPUTER_SERVER_ID"
                        PageSize="15" AllowSorting="True" OnRowCreated="grdEntity_RowCreated" DataSourceID="ObjectDataSource1">
                        <Columns>
                            <asp:BoundField DataField="COMPUTER_SERVER_ID" Visible="False" />
                            <asp:BoundField DataField="COMPUTER_SERVER_NO" HeaderText="服务器编号" SortExpression="COMPUTER_SERVER_NO">
                                <headerstyle wrap="False"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="COMPUTER_SERVER_NAME" HeaderText="服务器名称" SortExpression="SEX">
                                <headerstyle wrap="False"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ADDRESS" HeaderText="服务器地址" SortExpression="Post_Name">
                                <headerstyle wrap="False"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="IP地址" DataField="IPADDRESS">
                                <headerstyle wrap="False"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MEMO" HeaderText="备注" SortExpression="Memo">
                                <headerstyle wrap="False"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:TemplateField>
                                <headertemplate>
                                操作
                                
</headertemplate>
                                <itemtemplate>
<A class="underline" onclick='updateComputerServer(<%#Eval("COMPUTER_SERVER_ID")%>)' href="#"><B>编辑</B></A> <asp:LinkButton id="btnDelete" onclick="btnDelete_Click" runat="server" CssClass="underline" __designer:wfdid="w8" CommandName="del" OnClientClick="return deleteServer()" CommandArgument='<%#Eval("COMPUTER_SERVER_ID")%>'><b>删除</b></asp:LinkButton> <A style="DISPLAY: none" class="underline" href='ComputerServerInfoDetail.aspx?ID=<%#Eval("COMPUTER_SERVER_ID")%>&Type=0'>查看</A> 
</itemtemplate>
                                <headerstyle width="20%" horizontalalign="Center" wrap="False" />
                                <itemstyle width="20%" horizontalalign="Center" wrap="False" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False">
                                <itemtemplate>
<asp:Label id="lblID" runat="server" Text='<%# Eval("COMPUTER_SERVER_ID") %>' __designer:wfdid="w7"></asp:Label> 
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
        <input type="hidden" name="Refresh" />
        <asp:HiddenField runat="server" ID="hfSelectOrg" />
        <asp:HiddenField runat="server" ID="hfLevelNum" />
        <input type="hidden" name="UpdatePsw" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>
</body>
</html>
