<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComputerRoomExport.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.ComputerRoomExport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>导入Excel</title>
    <script type="text/javascript">
      function showProgressBarUpload()
       {
         if(form1.txtOrg.value == "")
          {
            alert('单位不能为空！');
            return;
          } 
          
          var ret = window.showModalDialog("/RailExamBao/RandomExamTai/ComputerRoomUpload.aspx?OrgID="+document.getElementById("hfOrg").value,
                        '', 'help:no; status:no; dialogWidth:350px;dialogHeight:50px;scroll:no;');
       	
          //var ret = showCommonDialog("/RailExamBao/Systems/ImportUpload.aspx?OrgID="+document.getElementById("hfOrg").value, 'help:no; status:no; dialogWidth:320px;dialogHeight:30px');

            if(ret)
            {
                if (ret.indexOf("refresh")>=0) {
                   alert(ret.split('|')[1]);
                   form1.Refresh.value = 'refresh';
                   form1.submit();
                   form1.Refresh.value = '';
                }
                else
                {
                    alert(ret);
                }
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
                        员工导入</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                </div>
            </div>
            <div id="content" style="text-align: center">
                <table>
                    <tr>
                        <td align="left">
                            站&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;段：<asp:TextBox ID="txtOrg" runat="server" ReadOnly="true"
                                Width="230px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr runat="server" id="excel">
                        <td align="left">
                            Excel文件：<asp:FileUpload ID="File1" runat="server" Width="251px" />
                        </td>
                    </tr>
                </table>
                <input type="button" class="button" value="上传文档" onclick="showProgressBarUpload();" />&nbsp;&nbsp;
                <br />
                <br />
                <asp:Label ID="lbltitle" runat="server" Text="未导入的员工名单"></asp:Label><br />
                <div id="gird">
                    <ComponentArt:Grid ID="Grid1" runat="server" PageSize="20" Width="97%">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="ExcelNo">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="ExcelNo" HeadingText="顺号" />
                                    <ComponentArt:GridColumn DataField="WorkNo" HeadingText="上岗证号" />
                                    <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="姓名" />
                                    <ComponentArt:GridColumn DataField="Sex" HeadingText="性别" />
                                    <ComponentArt:GridColumn DataField="OrgPath" HeadingText="组织机构" />
                                    <ComponentArt:GridColumn DataField="PostPath" HeadingText="工作岗位" />
                                    <ComponentArt:GridColumn DataField="ErrorReason" HeadingText="原因" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                    </ComponentArt:Grid>
                </div>
                <asp:Button ID="btnExcel" runat="server" Text="导出Excel" CssClass="button" OnClick="btnExcel_Click" />
            </div>
        </div>
        <asp:HiddenField ID="hfOrg" runat="server" />
        <asp:HiddenField ID="hfOrgName" runat="server" />
        <input type="hidden" name="Refresh" />
    </form>
</body>
</html>
