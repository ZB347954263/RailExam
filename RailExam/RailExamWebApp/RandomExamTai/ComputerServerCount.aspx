<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComputerServerCount.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.ComputerServerCount" validateRequest=false %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>站段考卷列表</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">
      function ShowProgressBar()
      {
         var search = window.location.search; 
         var ret = showCommonDialog("/RailExamBao/RandomExam/ExportExcel.aspx"+search+"&Type=ExamInfo",'dialogWidth:320px;dialogHeight:30px;');
         if(ret == "true")
         {
           form1.ExamInfo.value = "true";
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
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        微机教室管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        服务器使用情况统计</div>
                </div>
                <div id="button">
                      <asp:Button ID="btnSelect" runat="server" CssClass="buttonSearch" Text="查询" ToolTip="查询符合条件的内容"
                        OnClick="btnSelect_Click" />
                     <asp:Button runat="server" ID="btnOutPut" Text="导出Excel" CssClass="button" OnClick="btnOutPut_Click" />
                </div>
            </div>
            <div id="content">
                 <div style="text-align: left;">
                    时间范围 从
                    <uc1:Date ID="dateStartDateTime" runat="server" />
                    到
                    <uc1:Date ID="dateEndDateTime" runat="server" />
                </div>
                <div id="grid">
                    <yyc:SmartGridView ID="examsGrid" runat="server" AutoGenerateColumns="False" PageSize="15"
                        AllowPaging="True" DataKeyNames="Computer_Server_Name" AllowSorting="True" DataSourceID="ObjectDataSourceBook"
                        Width="98%" OnRowCreated="examsGrid_RowCreated">
                        <Columns>
                            <asp:BoundField HeaderText="站段名称" DataField="Short_Name" SortExpression="Short_Name">
                                <headerstyle width="150px" Wrap="false"/>
                                <itemstyle width="150px" Wrap="false"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="服务器名称" DataField="Computer_Server_Name" SortExpression="Computer_Server_Name" >
                                <headerstyle width="150px" Wrap="false"/>
                                <itemstyle width="150px" Wrap="false"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="使用人次" HeaderText="使用人次" SortExpression="使用人次">
                                <headerstyle width="120px" Wrap="false"/>
                                <itemstyle width="120px" Wrap="false" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="其他单位使用人次" DataField="其他单位使用人次" SortExpression="其他单位使用人次">
                                <headerstyle width="120px" Wrap="false"/>
                                <itemstyle width="120px" Wrap="false"/>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="其他单位使用天数" DataField="其他单位使用天数" SortExpression="其他单位使用天数">
                                <headerstyle width="120px" Wrap="false"/>
                                <itemstyle width="120px" Wrap="false"/>
                            </asp:BoundField>
                        </Columns>
                    </yyc:SmartGridView>
                   <asp:ObjectDataSource ID="ObjectDataSourceBook" runat="server" SelectMethod="Get"
                    TypeName="OjbData" OnSelected="ObjectDataSourceBook_Selected">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hfSelect" Name="sql" PropertyName="Value" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource> 
                <asp:HiddenField ID="hfSelect" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>