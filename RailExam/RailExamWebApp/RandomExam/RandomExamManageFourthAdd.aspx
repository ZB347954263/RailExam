<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamManageFourthAdd.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamManageFourthAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <%=ViewState["title"].ToString() %>
    </title>

    <script type="text/javascript">
  var search = window.location.search;
  var employeename = "";

  var type = "";
  if(search.indexOf("type=")>=0) {
  	type = search.substring(search.indexOf("type=") + 5);
  }
  
  function CloseBack()
  {
  	   if(type == "Book") {
       		   top.returnValue = "";  	   	       
               window.close(); 
  	   }
  	   else {
  	   	       window.opener.form1.Refresh.value = "true";
               window.opener.form1.submit();
               window.close(); 
  	   }
  }
  
  function SelectOK()
  {
       var obj = document.getElementById("hfrbnID").value;
       if(obj != null && obj != "")
       {
       	    if(type == "Book") {
       		    top.returnValue = document.getElementById("hfrbnID").value+"|"+employeename;
       		    window.close();
       	    }
       	    else {
       		    window.opener.form1.employee.value = document.getElementById("hfrbnID").value;
       		    window.opener.form1.submit();
       		    window.close();
       	    }
       }  

  }
  
  function rbnClick(rbn,name)
  {
    document.getElementById("hfrbnID").value = rbn.id;

  	employeename = name;
  }
  
  function init() {
  	 if(type != "") {
  	 	document.getElementById("btnClose").value = "清  空";
  	 }
  }
    </script>

</head>
<body onload="init();">
    <form id="form1" runat="server">
        <table class="contentTable">
            <tr>
                <td style="width: 10%" align="right">
                    姓名</td>
                <td style="width: 35%" align="left">
                    <asp:TextBox runat="server" ID="txtEmployeeName"></asp:TextBox>
                </td>
                <td style="width: 10%" align="right">
                    姓名拼音码</td>
                <td style="width: 35%" align="left">
                    <asp:TextBox runat="server" ID="txtPinYin"></asp:TextBox>
                </td>
                <td align="center" rowspan="2">
                    <asp:ImageButton runat="server" ID="btnQuery" ImageUrl="~/Common/Image/find.gif"
                        OnClick="btnQuery_Click" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%" align="right">
                    单位</td>
                <td style="width: 20%" align="left">
                    <asp:DropDownList ID="ddlOrg" runat="server">
                    </asp:DropDownList></td>
                <td style="width: 10%" align="right">
                    <asp:Label ID="lblTitle" runat="server"></asp:Label></td>
                <td style="width: 20%" align="left">
                    <asp:TextBox runat="server" ID="txtWorkNo"></asp:TextBox></td>

            </tr>
            <tr>
                <td colspan="5">
                    说明：拼音码为汉字的拼音首字母。姓名中如果包含生僻字，用拼音码无法查出，请在拼音码中用*号代替重试。</td>
            </tr>
        </table>
        <table border="0" style="width: 99%;">
            <tr>
                <td style="width: 100%; height: 380px;" valign="top" align="center">
                    <div style="overflow: auto; width: 100%; height: 380px">
                        <asp:DataGrid ID="Grid1" runat="server" HeaderStyle-BackColor="ActiveBorder" Width="98%"
                            PageSize="15" DataKeyField="EmployeeID" AllowPaging="true" AutoGenerateColumns="False"
                            OnPageIndexChanged="Grid1_PageIndexChanging" GridLines="None" AllowSorting="true"
                            AllowCustomPaging="true" OnSortCommand="Grid1_Sorting">
                            <Columns>
                                <asp:TemplateColumn HeaderText="选择">
                                    <ItemTemplate>
                                        <input type="radio" name="check" id='<%# Eval("EmployeeID") %>' onclick="rbnClick(this,'<%# Eval("EmployeeName") %>')" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="职员id" Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:Label ID="LabelEmployeeID" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="姓名" SortExpression="a.employee_name">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="LabelName" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="员工编码<br>(身份证号码）" SortExpression="a.WORK_NO">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="LabelWorkNo" runat="server" Text='<%# Bind("StrWorkNo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="性别" SortExpression="a.Sex">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSex" runat="server" Text='<%# Bind("Sex") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="组织机构" SortExpression="GetOrgName(a.org_id)">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" Wrap="false" Width="30%" />
                                    <ItemTemplate>
                                        <asp:Label ID="LabelOrgName" runat="server" Text='<%# Bind("OrgName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="职名" SortExpression="b.Post_Name">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" Wrap="false" />
                                    <ItemTemplate>
                                        <asp:Label ID="LabelPostName" runat="server" Text='<%# Bind("PostName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <ItemStyle BackColor="#EFF3FB" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle HorizontalAlign="left" Mode="NumericPages" PageButtonCount="30" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="22px" />
                            <AlternatingItemStyle BackColor="White" />
                            <EditItemStyle BackColor="#2461BF" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 90%;" align="center">
                    <asp:Button runat="server" ID="btnOK" Text="确   定" CssClass="button" OnClientClick="SelectOK()" />
                    <asp:Button runat="server" ID="btnInput" Text="添加所选职员" CssClass="buttonLong" OnClick="btnInput_Click" />
                    <asp:Button runat="server" ID="btnClose" Text="关　闭" CssClass="button" OnClientClick="CloseBack()" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfrbnID" runat="server" />
    </form>

    <script type="text/javascript">
      if(document.getElementById("rbn"))
     {
        document.getElementById("rbn").style.display = "none";
     }
      var  str = document.getElementById("hfrbnID").value;
        if(str != "")
       { 
             if(document.getElementById(str))
             {
                document.getElementById(str).checked = true;
             }
         }
    </script>

</body>
</html>
