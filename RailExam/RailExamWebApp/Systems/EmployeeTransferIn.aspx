<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeTransferIn.aspx.cs" Inherits="RailExamWebApp.Systems.EmployeeTransferIn" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>站段汇总</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">
   function completeTransfer(id)
   {
   	    var updateRight = document.getElementById("HfUpdateRight").value;
			
		if(updateRight == "False" ) {
			alert("您没有该操作的权限！");
			return;
		}
   	
      var ret = showCommonDialog('/RailExamBao/Systems/EmployeeTransferInDetail.aspx?transferID=' + id,'dialogWidth:600px;dialogHeight:250px;');
      if(ret == "true")
      {
           form1.Refresh.value =ret;
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
                        系统管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        员工调入</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                </div>
            </div>
            <div id="content">
                <div>
                    <ComponentArt:Grid ID="grdEntity" runat="server" AutoAdjustPageSize="false" PageSize="19">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="EmployeeTransferID">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="EmployeeTransferID" Visible="false" />
                                    <ComponentArt:GridColumn DataField="EmployeeID" Visible="false" />
                                    <ComponentArt:GridColumn DataField="TransferToOrgID" Visible="false" />
                                    <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="姓名"  Width="80"/>
                                    <ComponentArt:GridColumn DataField="TransferOutOrgName" HeadingText="调出单位" Width="120"/>
                                    <ComponentArt:GridColumn DataField="TransferToOrgName" HeadingText="调入单位" Width="120"/>
                                    <ComponentArt:GridColumn DataField="WorkNo" HeadingText="员工编码" Width="120"/>
                                    <ComponentArt:GridColumn DataField="PostNo" HeadingText="工作证号" Width="100"/>
                                    <ComponentArt:GridColumn DataField="PostName" HeadingText="职名" Width="120"/>
                                    <ComponentArt:GridColumn DataField="TransferOutDate" HeadingText="调出时间"   FormatString="yyyy-MM-dd" Width="100"/>
                                    <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="操作"  Width="50"/>
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="CTEdit">
                                <a onclick="completeTransfer('##DataItem.getMember('EmployeeTransferID').get_value()##');" href="#">
                                    <img alt="调入员工" border="0" src="../Common/Image/edit_col_see.gif" /></a>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </div>
            </div>
            <asp:HiddenField ID="beginTime" runat="server" />
            <asp:HiddenField ID="endTime" runat="server" />
            <input type="hidden" name="Refresh" />
           <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" /> 
        </div>
    </form>
</body>
</html>
