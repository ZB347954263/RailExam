<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectEmployeeAfterGetPaper.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.SelectEmployeeAfterGetPaper" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>添加考生</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
       function SelectPage()
      {
 	        var   cleft;
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;    
            var search = window.location.search;
            var id = search.substring(search.indexOf('RandomExamID=')+13,search.indexOf('&OrgID='));
            var orgID = search.substring(search.indexOf('OrgID=')+6);
       	
            var ret = window.open("/RailExamBao/RandomExam/RandomExamManageFourthEdit.aspx?id="+id+"&OrgID="+ orgID +"&selectType=After",'RandomExamManageFourthEdit','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
            ret.focus();
       	
//    	    var ret = showCommonDialog("/RailExamBao/RandomExam/RandomExamManageFourthEdit.aspx?id="+id+"&OrgID="+ orgID +"&selectType=After",'dialogWidth:800px;dialogHeight:600px;');
//             if(ret == "true" )
//             {
//             	//alert('添加成功!');
//             	form1.Refresh.value = ret;
//             	form1.submit();
//             	form1.Refresh.value = "";
//             }
       } 
  
      function GetPaper()
      {
      	var addIds = document.getElementById("hfAddIds").value;
      	//alert(addIds);
      	if(addIds != "")
      	{
      		if (!confirm("系统将为您添加的学员生成试卷，您确定要添加以上学员吗？"))
      		{
      			return;
      		}

      		var search = window.location.search;
      		//var ret = window.showModalDialog("/RailExamBao/RandomExam/FalseProgressBar.aspx" + search + "&type=GetAfter&addIds="+addIds,'', 'help:no;status:no;dialogWidth:410px;dialogHeight:30px;');
  			var ret = window.showModalDialog("/RailExamBao/RandomExam/DealPaperProgress.aspx"+search+"&type=GetAfter&addIds="+addIds, 
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
      
      function DelPaper()
      {
        if(! confirm("系统将为删除您添加的学员，您确定要取消吗？"))
        {
            return false;
        }
        
        return true;
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
                        添加考生</div>
                </div>
            </div>
            <div id="content">
                <table width="100%">
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button runat="server" ID="btnInput" Text="添加考生" CssClass="button" OnClientClick="SelectPage()" />&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="ButtonOutPut" Text="移除所选考生" CssClass="buttonLong"
                                OnClick="ButtonOutPut_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnExcel" Text="导出考生信息" CssClass="buttonLong" OnClick="btnExcel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div style="overflow: auto; height: 400px; width: 750px">
                                <asp:GridView ID="gvChoose" runat="server" HeaderStyle-BackColor="ActiveBorder" Width="99%"
                                    DataKeyNames="EmployeeID" AllowPaging="False" AutoGenerateColumns="False" ForeColor="#333333"
                                    GridLines="None" AllowSorting="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="选择">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect2" runat="server" Visible='<%#((","+ViewState["HasExamId"]+",").Contains(","+DataBinder.Eval(Container, "DataItem.EmployeeID")+",") ?  false:true ) %>' />
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Common/Image/charge.gif" ToolTip="已参加考试！"
                                                    Visible='<%#((","+ViewState["HasExamId"]+",").Contains(","+DataBinder.Eval(Container, "DataItem.EmployeeID")+",") ? true : false) %>' />
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
                                            <HeaderStyle HorizontalAlign="Center"  Wrap="false"/>
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
                            <asp:Button ID="btnLast" runat="server" Text="确  定" CssClass="button"  OnClientClick="GetPaper()"/>
                            <asp:Button ID="btnCancel" runat="server" Text="取  消" CssClass="button" OnClientClick="return DelPaper()" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <input type="hidden" name="Refresh" />
        <textarea rows="10" cols="30" name="ChooseID" style="display: none"></textarea>
        <asp:HiddenField runat="server" ID="hfAddIds"/>
    </form>
</body>
</html>
