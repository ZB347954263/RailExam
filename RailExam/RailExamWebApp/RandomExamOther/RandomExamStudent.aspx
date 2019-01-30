<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamStudent.aspx.cs"
    Inherits="RailExamWebApp.RandomExamOther.RandomExamStudent" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            var ret = window.open("/RailExamBao/RandomExam/RandomExamManageFourthAdd.aspx"+search,'RandomExamManageFourthAdd','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
            ret.focus();
      }
      else if(form1.rbnOld.checked)
      { 
            var ret = window.open("/RailExamBao/RandomExam/RandomExamManageFourthEdit.aspx"+search,'RandomExamManageFourthEdit','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
            ret.focus();
       }
       else
       {
            var ret = window.open("/RailExamBao/RandomExam/RandomExamManageFourthChoose.aspx"+search,'RandomExamManageFourthChoose','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
            ret.focus();
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
                            <b>第四步：选择考生</b>
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
                    <tr id="trainclass" style="display:none;">
                        <td>
                            培训班列表</td>
                        <td>
                            <asp:GridView ID="Grid1" runat="server" HeaderStyle-BackColor="ActiveBorder" AutoGenerateColumns="False"
                                ForeColor="#333333" GridLines="None" DataKeyNames="RandomExamTrainClassID" OnRowDataBound="Grid1_RowDataBound"
                                OnRowDeleting="Grid1_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="RandomExamTrainClassID" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("RandomExamTrainClassID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RandomExamID" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRandomExamID" runat="server" Text='<%# Bind("RandomExamID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="培训班">
                                        <HeaderStyle HorizontalAlign="Center" Width="30%" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTrainClass" runat="server" Text='<%# Bind("TrainClassName") %>'></asp:Label>
                                            <asp:Label ID="lblTrainClassID" Visible="false" runat="server" Text='<%# Bind("TrainClassID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:HiddenField ID="hfTrainClass" runat="server" Value='<%# Bind("TrainClassID") %>' />
                                            <asp:DropDownList ID="ddlTrainClass" runat="server">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDel" runat="server" ImageUrl="../Common/Image/edit_col_Delete.gif"
                                                AlternateText="删除" CommandName="Delete" OnClientClick="return confirm('您确定要删除该培训班吗？');"
                                                CausesValidation="false"></asp:ImageButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Button runat="server" ID="btnInput" Text="添加考生" CssClass="button" OnClientClick="SelectPage()" />&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="ButtonOutPut" Text="移除所选考生" CssClass="buttonLong"
                                OnClick="ButtonOutPut_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="ButtonOutPutAll" Text="移除全部考生" CssClass="buttonLong"
                                OnClick="ButtonOutPutAll_Click" />&nbsp;&nbsp;&nbsp;
                            <asp:Button runat="server" ID="btnExcel" Text="导出考生信息" CssClass="buttonLong" OnClick="btnExcel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div style="overflow: auto; height: 380px; width: 100%">
                                <asp:GridView ID="gvChoose" runat="server" HeaderStyle-BackColor="ActiveBorder" Width="99%"
                                    DataKeyNames="EmployeeID" AllowPaging="False" AutoGenerateColumns="False" ForeColor="#333333"
                                    GridLines="None" AllowSorting="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="选择">
                                            <HeaderStyle HorizontalAlign="Center" Width="10%" />
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
                                        <asp:TemplateField HeaderText="工资编号">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" Width="15%" />
                                            <ItemTemplate>
                                                <asp:Label ID="LabelWorkNo" runat="server" Text='<%# Bind("WorkNo") %>'></asp:Label>
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
                            <asp:ImageButton ID="btnLast" runat="server" CausesValidation="False" ImageUrl="../Common/Image/last.gif"
                                OnClick="btnLast_Click" />
                            <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" ImageUrl="../Common/Image/close.gif"
                                OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <input type="hidden" name="Refresh" />
        <textarea rows="10" cols="30" name="ChooseID" style="display: none"></textarea>
        <input type="hidden" name="ChooseExamID" />
        <asp:HiddenField ID="hfHasTrainClass" runat="server" />
    </form>
</body>
</html>
