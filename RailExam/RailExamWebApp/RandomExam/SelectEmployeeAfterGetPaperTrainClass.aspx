<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectEmployeeAfterGetPaperTrainClass.aspx.cs"
    EnableEventValidation="false" Inherits="RailExamWebApp.RandomExam.SelectEmployeeAfterGetPaperTrainClass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>添加考生</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
      function showConfirm()
      {
  	      	 if (!confirm("系统将为您添加的学员生成试卷，您确定要添加以上所选学员吗？"))
  		    {
  			    return false;
  		    }

      	    return true;
      }
      function GetPaper(examid,userIds)
      {
      	    if(userIds != "")
      	    {
      		    var search = window.location.search;
      		    //var ret = window.showModalDialog("/RailExamBao/RandomExam/FalseProgressBar.aspx" + search + "&type=GetAfter&addIds="+userIds,'', 'help:no;status:no;dialogWidth:410px;dialogHeight:30px;');
      		   var ret = window.showModalDialog("/RailExamBao/RandomExam/DealPaperProgress.aspx"+search+"&type=GetAfter&addIds="+userIds, 
                '', 'help:no; status:no; dialogWidth:360px;dialogHeight:30px;scroll:no;'); 
      		    if (ret == "true")
      		    {
      			    window.returnValue = "true";
      			    window.close();
      		    }
      	    }
      	    else 
      	    {
      		    window.returnValue = "true";
      		    window.close();
      	    }
      } 
      
      function SelectComputeRoom(id,userIds)
      {
   	    if(userIds == "")
   	    {
   	    	alert("请至少选择一个考生！");
   	    	return;
   	    }

   	    form1.ChooseID.value = userIds;
   	    var ret = window.showModalDialog("/RailExamBao/RandomExam/SelectComputeRoom.aspx?Type=After&id="+id,window,'dialogWidth:380px;dialogHeight:120px;');
        if(ret != "" )
        {
           form1.RefreshArrange.value =ret;
           form1.submit();
        }
   }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="parent">
                        考试监控</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        添加培训班考生</div>
                </div>
            </div>
            <div id="content">
                <table width="100%">
                    <tr style="text-align: center;">
                        <td>
                            安排微机教室：
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlComputerRoom" runat="server">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div style="overflow: auto; height: 400px; width: 100%">
                                <asp:GridView ID="gvChoose" runat="server" HeaderStyle-BackColor="ActiveBorder" Width="99%"
                                    DataKeyNames="EmployeeID" AllowPaging="False" AutoGenerateColumns="False" ForeColor="#333333"
                                    GridLines="None" AllowSorting="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="选择">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect2" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="职员id" Visible="false">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="LabelEmployeeID" runat="server" Text='<%# Bind("EmployeeID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="序号">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("RowNum") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="姓名">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="LabelName" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="员工编码<br>(身份证号码)">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="LabelWorkNo" runat="server" Text='<%# Bind("StrWorkNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="组织机构">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" Wrap="false" Width="30%" />
                                            <ItemTemplate>
                                                <asp:Label ID="Labelorgid" runat="server" Text='<%# Bind("OrgName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="职名">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" Width="35%" Wrap="false" />
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPostName" runat="server" Text='<%# Bind("PostName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="考试地点" SortExpression="PostName">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="left" Width="20%" Wrap="false" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblComputeRoom" runat="server" Text='<%# Bind("ComputeRoom") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="22px" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button ID="btnLast" runat="server" Text="确  定" CssClass="button" OnClientClick="return showConfirm();"
                                OnClick="btnLast_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="取  消" CssClass="button" OnClientClick="top.close();" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <input type="hidden" name="Refresh" />
        <textarea rows="10" cols="30" name="ChooseID" style="display: none"></textarea>
        <asp:HiddenField runat="server" ID="hfAddIds" />
        <input type="hidden" name="RefreshArrange" />
    </form>
</body>
</html>
