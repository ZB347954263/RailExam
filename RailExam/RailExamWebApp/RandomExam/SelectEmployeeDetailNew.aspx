<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectEmployeeDetailNew.aspx.cs"
    EnableEventValidation="false" Inherits="RailExamWebApp.Exam.SelectEmployeeDetailNew" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择考生</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
   function SelectPage()
  {
	    var   cleft;   
        var   ctop;   
        cleft=(screen.availWidth-800)*.5;   
        ctop=(screen.availHeight-600)*.5; 
      var search = window.location.search;
      
       if(form1.rbnNew.checked)
      {
            var ret = window.open("/RailExamBao/RandomExam/RandomExamManageFourthAdd.aspx"+search,'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
            ret.focus();
      }
      else if(form1.rbnOld.checked)
      { 
            var ret = window.open("/RailExamBao/RandomExam/RandomExamManageFourthEdit.aspx"+search,'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
            ret.focus();
       }
       else
       {
            var ret = window.open("/RailExamBao/RandomExam/RandomExamManageFourthChoose.aspx"+search,'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
            ret.focus();
       }
  } 
   
   function SelectComputeRoom(id,userIds) {
   	    if(userIds == "") {
   	    	alert("请至少选择一个考生！");
   	    	return;
   	    }

   	    form1.ChooseID.value = userIds;
   	    var ret = window.showModalDialog("/RailExamBao/RandomExam/SelectComputeRoom.aspx?id="+id,window,'dialogWidth:400px;dialogHeight:200px;');
        if(ret != "" )
        {
           form1.RefreshArrange.value =ret;
           form1.submit();
        }
   	
   }
   
   function showExport() 
   {
   	     var id = document.getElementById("hfExamID").value;
         var ret = window.showModalDialog("/RailExamBao/RandomExam/ExportArrangeExel.aspx?id="+id, 
          '', 'help:no; status:no; dialogWidth:320px;dialogHeight:50px;scroll:no;'); 
         if(ret == "true")
         {
           form1.StudentInfo.value = "true";
           form1.submit();
         }
   }
   
   function changeAll(chk) {

   	    __doPostBack("btnCheckAll",chk.checked);
   }
   
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="parent">
                        新增考试</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        选择考生</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td align="left" colspan="2">
                            <b>第四步：选择考生（注意：安排考生微机教室完毕后，务必点击“确定”按钮保存！）</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            添加考生模式
                        </td>
                        <td>
                            <input id="rbnNew" name="rbn" type="radio" checked="checked" />单个添加
                            <input id="rbnOld" name="rbn" type="radio" />批量添加
                            <input id="rbnChoose" name="rbn" type="radio" />添加现有考试中考生
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr style="display: none">
                        <td>
                            站段：</td>
                        <td style="text-align: left">
                            <asp:DropDownList runat="server" ID="ddlOrg" AutoPostBack="true" OnSelectedIndexChanged="ddlOrg_SelectedIndexChanged">
                            </asp:DropDownList></td>
                        <td>
                            车间：</td>
                        <td style="text-align: left">
                            <asp:DropDownList runat="server" ID="ddlWorkShop">
                            </asp:DropDownList></td>
                    </tr>
                    <tr style="display: none">
                        <td>
                            姓名：</td>
                        <td style="text-align: left">
                            <asp:TextBox runat="server" ID="txtName"></asp:TextBox></td>
                        <td>
                            拼音缩写：</td>
                        <td style="text-align: left">
                            <asp:TextBox runat="server" ID="txtPinyin"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button runat="server" ID="btnQuery" CssClass="button" Text="查  询" OnClick="btnQuery_Click" Visible="False"/>&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnInput" Text="添加考生" CssClass="button" OnClientClick="SelectPage()" />&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="ButtonOutPut" Text="移除所选考生" CssClass="buttonLong"
                                OnClick="ButtonOutPut_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="ButtonOutPutAll" Text="移除全部考生" CssClass="buttonLong"
                                OnClick="ButtonOutPutAll_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnSelectComputeRoom" Text="安排微机教室" CssClass="buttonLong"
                                OnClick="btnSelectComputeRoom_Click" />&nbsp;&nbsp;&nbsp;
                            <input type="button" onclick="showExport()" value="导出考生信息" class="buttonLong" />
                            <asp:Button runat="server" ID="btnExcel" Text="导出考生信息" CssClass="buttonLong" OnClick="btnExcel_Click"
                                Visible="False" />
                            <asp:Button runat="server" ID="btnCheckAll" CssClass="displayNone" OnClick="btnCheckAll_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div style="overflow: auto; height: 500px; width: 100%">
                                <asp:GridView ID="gvChoose" runat="server" HeaderStyle-BackColor="ActiveBorder" Width="99%"
                                    DataKeyNames="EmployeeID" AllowPaging="False" AutoGenerateColumns="False" ForeColor="#333333"
                                    GridLines="None" AllowSorting="true" OnSorting="gvChoose_OnSorting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="选择">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" Wrap="false" />
                                            <HeaderTemplate>
                                                <input type="checkbox" id="chkAll" onclick="changeAll(this);" />全选
                                            </HeaderTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect2" runat="server" Visible='<%#((","+ViewState["HasExamId"]+",").Contains(","+DataBinder.Eval(Container, "DataItem.EmployeeID")+",") ?  false:true ) %>' />
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Common/Image/charge.gif" ToolTip="已生成试卷！"
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
                                        <asp:TemplateField HeaderText="序号" SortExpression="RowNum">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("RowNum") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="姓名" SortExpression="EmployeeName">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="LabelName" runat="server" Text='<%# Bind("EmployeeName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="员工编码<br>(身份证号码)" SortExpression="StrWorkNo">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="LabelWorkNo" runat="server" Text='<%# Bind("StrWorkNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="组织机构" SortExpression="OrgName">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" Wrap="false" Width="25%" />
                                            <ItemTemplate>
                                                <asp:Label ID="Labelorgid" runat="server" Text='<%# Bind("OrgName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="职名" SortExpression="PostName">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" Width="20%" Wrap="false" />
                                            <ItemTemplate>
                                                <asp:Label ID="LabelPostName" runat="server" Text='<%# Bind("PostName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="考试地点" SortExpression="ComputeRoom">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Width="20%" Wrap="false" />
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
                        <td align="center" colspan="4">
                            <asp:ImageButton ID="btnLast" runat="server" CausesValidation="False" ImageUrl="../Common/Image/last.gif"
                                OnClick="btnLast_Click" />
                            <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" ImageUrl="../Common/Image/confirm.gif"
                                OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <input type="hidden" name="Refresh" />
        <textarea rows="10" cols="30" name="ChooseID" style="display: none"></textarea>
        <input type="hidden" name="ChooseExamID" />
        <input type="hidden" name="RefreshArrange" />
        <asp:HiddenField runat="server" ID="hfExamID" />
        <input name="StudentInfo" type="hidden" />
    </form>
</body>
</html>
