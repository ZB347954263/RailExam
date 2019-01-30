<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamNoExam.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamNoExam" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>未参加考试的学员</title>

    <script type="text/javascript">
   function exportNoExam() {
   	var search = window.location.search;
   	    var ret = window.showModalDialog("/RailExamBao/RandomExam/ExportExcel.aspx"+search+"&Type=noExam", '', 'help:no;status:no;dialogWidth:320px;dialogHeight:30px;');
      	 if(ret != "")
         {
           form1.StudentInfo.value = ret;
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
                    <div id="separator">
                    </div>
                    <div id="current">
                        未参加考试的学员</div>
                </div>
                <div id="button">
                    <asp:Button runat="server" ID="btnOutPut" Text="导出Excel" Visible="false" CssClass="button"
                        OnClick="btnOutPut_Click" />&nbsp;
                    <input type="button" id="btnExcel" class="button" onclick="exportNoExam();" value="导出Excel" />
                </div>
            </div>
            <div id="content">
                <ComponentArt:Grid ID="gradesGrid" runat="server" AllowPaging="true" PageSize="15"
                    Width="100%">
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="EmployeeID">
                            <Columns>
                                <ComponentArt:GridColumn DataField="EmployeeID" HeadingText="编号" Visible="false" />
                                <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="考生姓名" Width="100"
                                    Align="center" />
                                <ComponentArt:GridColumn DataField="StrWorkNo" HeadingText="员工编码<br>(身份证号码)"
                                    Width="120" Align="center" />
                                <ComponentArt:GridColumn DataField="PostName" HeadingText="职名" Align="center" Width="100" />
                                <ComponentArt:GridColumn DataField="OrganizationId" Visible="false" />
                                <ComponentArt:GridColumn DataField="OrgName" HeadingText="组织机构" Width="300" Align="center" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                </ComponentArt:Grid>
            </div>
        </div>
        <input name="StudentInfo" type="hidden" />
    </form>
</body>
</html>
