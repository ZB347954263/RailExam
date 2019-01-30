<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TJ_SelectExam.aspx.cs" EnableEventValidation="false"
    Inherits="RailExamWebApp.RandomExam.TJ_SelectExam" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择考试</title>
    <base target="_self"/>
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
         
      function init() 
      {
      	    var search = window.dialogArguments;
      	    document.getElementById("hfSql").value = search.document.getElementById("hfSql").value;
      	     document.getElementById("hfExamID").value = search.document.getElementById("hfExamId").value;
      	    if(document.getElementById("hfRefresh").value=="") {
      	        __doPostBack("btnQuery");
      	    }
      }

    </script>

</head>
<body onload="init(); " >
    <form id="form1" runat="server">
         <asp:Button ID="btnQuery" runat="server" Text="查  询" CssClass="button" OnClick="btnQuery_Click" Visible="false" />
        <table border="0" style="width: 99%;">
            <tr>
                <td style="width: 100%; height: 380px;" valign="top" align="center">
                    <table style="width: 100%; height: 380px">
                        <tr>
                            <td style="vertical-align: top;">
                                <yyc:SmartGridView ID="grdEntity" runat="server" PageSize="10" OnRowCreated="grdEntity_RowCreated"
                                    AutoGenerateColumns="False" AllowSorting="True" AllowPaging="True" DataKeyNames="Random_Exam_ID"
                                    DataSourceID="ObjectDataSource1" OnPageIndexChanging="grdEntity_PageIndexChanging"
                                    OnRowDataBound="grdEntity_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <headertemplate>
                                          <asp:CheckBox ID="chkAll" runat="server"/>
                                            </headertemplate>
                                            <itemtemplate>
                                                <asp:CheckBox ID="item" runat="server" />
                                                <span style="display:none" runat="server" id="spanID"><%# Eval("Random_Exam_ID")%></span>
                                            </itemtemplate>
                                            <headerstyle width="50px" />
                                            <itemstyle width="50px" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Random_Exam_ID" HeaderText="Random_Exam_ID" Visible="False"
                                            ReadOnly="True" SortExpression="Random_Exam_ID" />
                                        <asp:BoundField DataField="Exam_Name" HeaderText="考试名称" ReadOnly="True" SortExpression="Exam_Name">
                                            <headerstyle width="180px" />
                                            <itemstyle width="180px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Begin_Time" HeaderText="开始时间" ReadOnly="True" SortExpression="Begin_Time">
                                            <headerstyle width="150px" />
                                            <itemstyle width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="End_Time" HeaderText="结束时间" ReadOnly="True" SortExpression="End_Time">
                                            <headerstyle width="150px" />
                                            <itemstyle width="150px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="StationName" HeaderText="单位" ReadOnly="True" SortExpression="StationName">
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
            <tr>
                <td style="width: 90%;" align="center">
                    <asp:Button ID="btnAll" runat="server" Text="全  选" class="button" OnClick="btnAll_Click" />
                    <asp:Button ID="btnOK" runat="server" Text="确  定" class="button" OnClick="btnOK_Click" />
                    <input type="button" id="btnClose" value="关  闭" class="button" onclick="window.close()" />
                </td>
            </tr>
        </table>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="OjbData"
            OnSelected="ObjectDataSource1_Selected">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfSelect" Type="String" PropertyName="Value" Name="sql" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="hfid" runat="server" />
        <asp:HiddenField ID="hfSelect" runat="server" />
        <asp:HiddenField ID="hfSql" runat="server" />
         <asp:HiddenField ID="hfExamID" runat="server" />
        <asp:HiddenField ID="hfRefresh" runat="server" />
    </form>
</body>
</html>
