<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Default2.aspx.cs" Inherits="RailExamWebApp.Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" AllowPaging="True" AllowSorting="True">
            <Columns>
                <asp:BoundField DataField="LevelNum" HeaderText="LevelNum" SortExpression="LevelNum" />
                <asp:BoundField DataField="ShortName" HeaderText="ShortName" SortExpression="ShortName" />
                <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
                <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                <asp:BoundField DataField="Memo" HeaderText="Memo" SortExpression="Memo" />
                <asp:BoundField DataField="ParentId" HeaderText="ParentId" SortExpression="ParentId" />
                <asp:BoundField DataField="WebSite" HeaderText="WebSite" SortExpression="WebSite" />
                <asp:BoundField DataField="IdPath" HeaderText="IdPath" SortExpression="IdPath" />
                <asp:BoundField DataField="FullName" HeaderText="FullName" SortExpression="FullName" />
                <asp:BoundField DataField="OrganizationId" HeaderText="OrganizationId" SortExpression="OrganizationId" />
                <asp:BoundField DataField="ContactPerson" HeaderText="ContactPerson" SortExpression="ContactPerson" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="OrderIndex" HeaderText="OrderIndex" SortExpression="OrderIndex" />
                <asp:BoundField DataField="PostCode" HeaderText="PostCode" SortExpression="PostCode" />
            </Columns>
            <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页" />
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetOrganizations"
            TypeName="RailExam.BLL.OrganizationBLL" EnablePaging="True" SelectCountMethod="GetCount" SortParameterName="orderBy" DataObjectTypeName="RailExam.Model.Organization">
            <SelectParameters>
                <asp:Parameter Name="organizationId" Type="Int32" />
                <asp:Parameter Name="parentId" Type="Int32" />
                <asp:Parameter Name="idPath" Type="String" />
                <asp:Parameter Name="levelNum" Type="Int32" />
                <asp:Parameter Name="orderIndex" Type="Int32" />
                <asp:Parameter Name="shortName" Type="String" />
                <asp:Parameter Name="fullName" Type="String" />
                <asp:Parameter Name="address" Type="String" />
                <asp:Parameter Name="postCode" Type="String" />
                <asp:Parameter Name="contactPerson" Type="String" />
                <asp:Parameter Name="phone" Type="String" />
                <asp:Parameter Name="webSite" Type="String" />
                <asp:Parameter Name="email" Type="String" />
                <asp:Parameter Name="description" Type="String" />
                <asp:Parameter Name="memo" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
