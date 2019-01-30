<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ItemConfig.aspx.cs" Inherits="RailExamWebApp.Item.ItemConfig" %>

<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试题设置</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .hiddenElement{ display: none; }
    </style>

    <script type="text/javascript">
        //按钮点击事件处理函数 
        function imgBtns_onClick(btn)
        {
	        switch(btn.type)
	        {
	            case "0":
	            {
	            
	      var flagUpdate=document.getElementById("HfUpdateRight").value;  
          if(flagUpdate=="False")
             {  
             alert("您没有权限使用该操作!");
             return;
                         
              }  
              
              
	                window.location.href = "javascript:__doPostBack('dvItemConfig','Edit$0')";
	                
	                break;
	            }
	            case "1":
	            {
	                window.location.href = "javascript:__doPostBack('dvItemConfig$ctl01','')";
	            
	                break;
	            }
	            case "2":
	            {
	                window.location.href = "javascript:__doPostBack('dvItemConfig','Cancel$0')";
	                
	                break;
	            }
	            default:
	            {
	                alert("状态错误！");
	                
	                break;
	            }
        	}        	
        }
    </script>

    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
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
                <div id="button">
                    <img id="editButton" runat="server" alt="" onclick="imgBtns_onClick(this);" src="../Common/Image/edit.gif"
                        type="0" />
                    <img id="updateButton" runat="server" alt="" onclick="imgBtns_onClick(this);" src="../Common/Image/save.gif"
                        type="1" />
                    <img id="updateCancelButton" runat="server" alt="" onclick="imgBtns_onClick(this);"
                        src="../Common/Image/cancel.gif" type="2" />
                </div>
            </div>
            <div id="content">
                <asp:DetailsView ID="dvItemConfig" runat="server" AutoGenerateRows="False" DataSourceID="odsItemConfig"
                    OnModeChanged="dvItemConfig_OnModeChanged" Width="90%" GridLines="None">
                    <Fields>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <table class="contentTable">
                                    <tr>
                                        <td rowspan="5" width="10%">
                                            试题信息</td>
                                        <td width="10%">
                                            试题类型</td>
                                        <td width="35%">
                                            <asp:DropDownList ID="ddlItemType" DataSourceID="odsItemType" DataValueField="ItemTypeId"
                                                DataTextField="TypeName" SelectedValue='<%# Eval("DefaultTypeId") %>' runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="10%">
                                            试题难度</td>
                                        <td>
                                            <asp:DropDownList ID="ddlItemDifficulty" runat="server" DataSourceID="odsItemDifficulty"
                                                DataTextField="DifficultyName" DataValueField="ItemDifficultyId" SelectedValue='<%# Bind("DefaultDifficultyId") %>'>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            试题分数</td>
                                        <td>
                                            <asp:TextBox ID="txtItemScore" runat="server" Text='<%# Bind("DefaultScore") %>'>
                                            </asp:TextBox>
                                        </td>
                                        <td>
                                            选项数目</td>
                                        <td>
                                            <asp:TextBox ID="txtAnswerCount" runat="server" Text='<%# Bind("DefaultAnswerCount") %>'>
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            答题时间</td>
                                        <td>
                                            <asp:TextBox ID="txtCompleteTime" runat="server" Text='<%# Bind("DefaultCompleteTime") %>'>
                                            </asp:TextBox>
                                        </td>
                                        <td>
                                            试题种类
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlHasPicture" runat="server" SelectedValue='<%# Bind("HasPicture") %>'>
                                                <asp:ListItem Text="文本" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="图片" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            试题版本</td>
                                        <td>
                                            <asp:TextBox ID="txtVersion" runat="server" Text='<%# Bind("DefaultVersion") %>'>
                                            </asp:TextBox>
                                        </td>
                                        <td>
                                            过期日期</td>
                                        <td>
                                            <uc1:Date ID="dateOutDateDate" runat="server" DateValue='<%# Bind("DefaultOutDateDate","{0:yyyy-MM-dd}") %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            状态</td>
                                        <td>
                                            <asp:DropDownList ID="ddlItemStatus" runat="server" DataSourceID="odsItemStatus"
                                                DataTextField="StatusName" DataValueField="ItemStatusId" SelectedValue='<%# Bind("DefaultStatusId") %>'>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            试题用途</td>
                                        <td>
                                            <asp:DropDownList ID="ddlUseage" runat="server" SelectedValue='<%# Bind("DefaultUsageId") %>'>
                                                <asp:ListItem Text="用作练习和考试" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="仅用作考试" Value="1"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr style="display: none;">
                                        <td>
                                            过期提醒</td>
                                        <td>
                                            提前天数</td>
                                        <td>
                                            <asp:TextBox ID="txtRemindDays" runat="server" Text='<%# Bind("DefaultRemindDays") %>'>
                                            </asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtItemConfigID" runat="server" Text='<%#Bind("ItemConfigId") %>'>
                                            </asp:TextBox><br/>
                                            <asp:TextBox ID="txtEmployeeID" runat="server" Text='<%#Bind("EmployeeId") %>'>
                                            </asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <table class="contentTable">
                                    <tr>
                                        <td rowspan="5" width="10%">
                                            试题信息</td>
                                        <td width="10%">
                                            试题类型</td>
                                        <td width="35%">
                                            <asp:Label ID="lblItemType" runat="server" Text='<%# Bind("DefaultTypeName") %>'>
                                            </asp:Label>
                                        </td>
                                        <td width="10%">
                                            试题难度</td>
                                        <td>
                                            <asp:Label ID="lblItemDifficulty" runat="server" Text='<%# Bind("DefaultDifficultyName") %>'>
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 27px">
                                            试题分数</td>
                                        <td style="height: 27px">
                                            <asp:Label ID="lblItemScore" runat="server" Text='<%# Bind("DefaultScore") %>'>
                                            </asp:Label>
                                        </td>
                                        <td style="height: 27px">
                                            选项数目</td>
                                        <td style="height: 27px">
                                            <asp:Label ID="lblAnswerCount" runat="server" Text='<%# Bind("DefaultAnswerCount") %>'>
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            答题时间</td>
                                        <td>
                                            <asp:Label ID="lblCompleteTime" runat="server" Text='<%# Bind("DefaultCompleteTime") %>'>
                                            </asp:Label>
                                        </td>
                                        <td>
                                            试题种类</td>
                                        <td>
                                            <asp:Label ID="lblSource" runat="server" Text='<%# Bind("HasPictureText")%>'>
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            试题版本</td>
                                        <td>
                                            <asp:Label ID="lblVersion" runat="server" Text='<%# Bind("DefaultVersion") %>'>
                                            </asp:Label>
                                        </td>
                                        <td>
                                            过期日期</td>
                                        <td>
                                            <asp:Label ID="lblOutDateDate" runat="server" Text='<%# Bind("DefaultOutDateDate") %>'>
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            状态</td>
                                        <td>
                                            <asp:Label ID="lblItemStatus" runat="server" Text='<%# Bind("DefaultStatusName") %>'>
                                            </asp:Label>
                                        </td>
                                        <td>
                                            试题用途</td>
                                        <td>
                                            <asp:Label ID="lblUseage" runat="server" Text='<%# (int)Eval("DefaultUsageId") == 0 ? "用作练习和考试" : "仅用作考试" %>'>
                                            </asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="display: none;">
                                        <td>
                                            过期提醒</td>
                                        <td>
                                            提前天数</td>
                                        <td>
                                            <asp:Label ID="lblRemindDays" runat="server" Text='<%# Bind("DefaultRemindDays") %>'>
                                            </asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblItemConfigID" runat="server" Text='<%#Bind("ItemConfigId") %>'>
                                            </asp:Label>
                                            <asp:Label ID="lblEmployeeID" runat="server" Text='<%#Bind("EmployeeId") %>'>
                                            </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" UpdateText="保存">
                            <ControlStyle CssClass="hiddenElement" />
                        </asp:CommandField>
                    </Fields>
                </asp:DetailsView>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsItemConfig" runat="server" DataObjectTypeName="RailExam.Model.ItemConfig"
            SelectMethod="GetItemConfig" UpdateMethod="UpdateItemConfig" TypeName="RailExam.BLL.ItemConfigBLL">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfEmployeeID"  Name="employeeID" PropertyName="Value" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsItemType" runat="server" SelectMethod="GetItemTypes"
            TypeName="RailExam.BLL.ItemTypeBLL" DataObjectTypeName="RailExam.Model.ItemType">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsItemDifficulty" runat="server" SelectMethod="GetItemDifficulties"
            TypeName="RailExam.BLL.ItemDifficultyBLL" DataObjectTypeName="RailExam.Model.ItemDifficulty">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsItemStatus" runat="server" SelectMethod="GetItemStatuss"
            TypeName="RailExam.BLL.ItemStatusBLL" DataObjectTypeName="RailExam.Model.ItemStatus">
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="hfEmployeeID" runat="server" />
    </form>
</body>
</html>
