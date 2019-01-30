<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamManageFourthChooseDetail.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamManageFourthChooseDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考试信息</title>
    <script type="text/javascript">              
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="gridPage">
            <div id="query" style="display: none; height: 60px">
                考试名称
                <asp:TextBox ID="txtPaperName" runat="server" Width="100px"></asp:TextBox>
                创建者
                <asp:TextBox ID="txtCreatePerson" runat="server" Width="60px"></asp:TextBox>&nbsp;&nbsp;
               考试类型
               <asp:DropDownList ID="ddlStyle" runat="server">
                    <asp:ListItem Text="--请选择--" Selected="true" Value="0"></asp:ListItem>
                    <asp:ListItem Text="存档考试" Value="1"></asp:ListItem>
                    <asp:ListItem Text="不存档考试" Value="2"></asp:ListItem>
                </asp:DropDownList> 
                <br/> 
                 有效期   
                <asp:DropDownList ID="ddl" runat="server">
                    <asp:ListItem Text="未过期考试" Selected="true" Value="0"></asp:ListItem>
                    <asp:ListItem Text="已过期考试" Value="1"></asp:ListItem>
                    <asp:ListItem Text="全部考试" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="确  定" OnClick="btnQuery_Click" />
            </div>
            <div id="grid">
                <ComponentArt:Grid ID="Grid1" runat="server" DataSourceID="odsExam" PageSize="20"
                    Width="520px">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="RandomExamId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="RandomExamId" HeadingText="编号" Visible="false" />
                                <ComponentArt:GridColumn DataField="OrgId" HeadingText="编号" Visible="false" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate1" HeadingText="选择"  Width="50"/>
                                <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" Align="Left" Width="200" />
                                <ComponentArt:GridColumn DataField="StationName" HeadingText="单位" Width="120" />
                                 <ComponentArt:GridColumn DataField="ExamStyleName" HeadingText="考试类型" />
                               <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="创建者" Width="80" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate1">
                            <input type="radio" id="## DataItem.getMember('RandomExamId').get_value()##" name="rbn"/> 
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsExam" runat="server" SelectMethod="GetExamByExamCategoryIDPath"
            TypeName="RailExam.BLL.RandomExamBLL">
            <SelectParameters>
                <asp:QueryStringParameter Name="ExamCategoryIDPath" QueryStringField="id" Type="String" />
                <asp:ControlParameter ControlID="txtPaperName" Type="String" PropertyName="Text"
                    Name="ExamName" />
                <asp:ControlParameter ControlID="txtCreatePerson" Type="String" PropertyName="Text"
                    Name="CreatePerson" />
                <asp:SessionParameter SessionField="StationOrgID" Name="OrgId" Type="Int32" />
                <asp:ControlParameter ControlID="ddl" Type="Int32" PropertyName="SelectedValue" Name="ExamTimeType"
                    DefaultValue="0" />
                      <asp:ControlParameter ControlID="ddlStyle" Type="Int32" PropertyName="SelectedValue" Name="ExamStyleID"
                    DefaultValue="0" />
                    <asp:ControlParameter ControlID="hfRailSystemID" Type="Int32" PropertyName="Value"
                    Name="railSystemID" DefaultValue="0" />  
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HfExamCategoryId" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <asp:HiddenField ID="hfIsAdmin" runat="server" />
          <asp:HiddenField ID="hfRailSystemID" runat="server" />
    </form>
</body>
</html>
