<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ShowCheckExam.aspx.cs"
    Inherits="RailExamWebApp.Exam.ShowCheckExam" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>路局不完整考试</title>
    <base target="_self" />

    <script type="text/javascript">
        
         function selectArow(obj)
         {
         	var TBL = document.getElementById("grdEntity");
         	for (var i = 1; i < TBL.rows.length; i++)
         	{
         		TBL.rows[i].style.backgroundColor = "White";
         		if (i % 2 != 0)
         			TBL.rows[i].style.backgroundColor = "#EFF3FB";
         	}
         	obj.style.backgroundColor = "#FFEEC2";
         }
         
         function chkAll(obj)
		 {
		 	var TBL = document.getElementById("grdEntity");
		 	for (var i = 1; i < TBL.rows.length; i++)
		 	{
		 		var ob = TBL.rows[i].cells[0].childNodes[0];
		 		if (ob.type == "checkbox")
		 			ob.checked = obj.checked;
		 	}
		 } 
         
        
          function showProgressBar() {
          	var search = window.location.search;
        	var ret = window.showModalDialog("/RailExamBao/RandomExam/FalseProgressBar.aspx" + search+"&type=CheckExam",
            '', 'help:no; status:no; dialogWidth:350px;dialogHeight:50px;scroll:no;');
        	if(ret== "true")
            {
               form1.Refresh.value = 'true';
               form1.submit();
               form1.Refresh.value = '';
            }
        } 
         
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
            <asp:Button ID="btnQuery" runat="server" Text="查询上次检查结果" CssClass="buttonEnableLong" OnClick="btnQuery_Click" />&nbsp;&nbsp;
            <input name="btnInput" type="button" class="button" value="重新检查" onclick="showProgressBar()" /></div>
        <table border="0" style="width: 99%;">
            <tr>
                <td style="width: 100%; height: 380px;" valign="top" align="center">
                    <table style="width: 100%; height: 380px">
                        <tr>
                            <td style="vertical-align: top;">
                                <yyc:SmartGridView ID="grdEntity" runat="server" PageSize="10" OnRowCreated="grdEntity_RowCreated"
                                    AutoGenerateColumns="False" AllowSorting="False" AllowPaging="True" DataKeyNames="Random_Exam_ID"
                                    DataSourceID="ObjectDataSource1" OnPageIndexChanging="grdEntity_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="Random_Exam_ID" HeaderText="Random_Exam_ID" Visible="False"
                                            ReadOnly="True" SortExpression="Random_Exam_ID" />
                                        <asp:BoundField DataField="Employee_Name" HeaderText="姓名" ReadOnly="True">
                                            <headerstyle width="80px" />
                                            <itemstyle width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Exam_Name" HeaderText="考试名称" ReadOnly="True">
                                            <headerstyle width="180px" />
                                            <itemstyle width="180px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Begin_Time" HeaderText="开始时间" ReadOnly="True">
                                            <headerstyle width="150px" />
                                            <itemstyle width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="End_Time" HeaderText="结束时间" ReadOnly="True">
                                            <headerstyle width="150px" />
                                            <itemstyle width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="StationName" HeaderText="考试地点" ReadOnly="True">
                                            <headerstyle width="80px" />
                                            <itemstyle width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ExamStyleName" HeaderText="考试类型" ReadOnly="True" SortExpression="ExamStyleName">
                                            <headerstyle width="80px" />
                                            <itemstyle width="80px" />
                                        </asp:BoundField>
                                    </Columns>
                                </yyc:SmartGridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="OjbData"
            OnSelected="ObjectDataSource1_Selected">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfSelect" Type="String" PropertyName="Value" Name="sql" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="hfSelect" runat="server" />
        <input type="hidden" name="Refresh" />
    </form>
</body>
</html>
