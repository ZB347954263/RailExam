<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamComputerSeat.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamComputerSeat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>调整机位</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

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
        
        
        function updateSeat(id,isremove) 
        {
        	if(isremove ==1) {
        		alert("该考生考试已完成，不能调整机位！");
        		return;
        	}
        	
        	 var ret = showCommonDialog("/RailExamBao/RandomExam/RandomExamComputerSeatDetail.aspx?id="+id,'help:no;stuats:no;dialogWidth:400px;dialogHeight:300px;');
             if(ret != "") {
             	form1.refresh.value = "true";
             	form1.submit();
             	form1.refresh.value = "";
             }
        }
        
        function resetSeat(id,isremove) 
        {
        	if(isremove ==1) {
        		alert("该考生考试已完成，不能重置机位！");
        		return;
        	}
        	
        	if(!confirm("您确定要为该考生重置机位吗？"))
        	{
        		return;
        	}
        	
        	form1.reset.value = id;
         	form1.submit();
         	form1.reset.value = "";
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div style="overflow: auto; height: 550px; width: 100%">
                <yyc:SmartGridView ID="grdEntity" runat="server" AllowPaging="true" PageSize="18"
                    OnPageIndexChanging="grdEntity_PageIndexChanging" AllowSorting="false" OnRowCreated="grdEntity_RowCreated">
                    <Columns>
                        <asp:TemplateField>
                            <headertemplate> 操作</headertemplate>
                            <itemstyle width="20%" horizontalalign="Center" wrap="false" />
                            <headerstyle width="20%" horizontalalign="Center" wrap="false" />
                            <itemtemplate>
                                    <a href="#" onclick="updateSeat(<%#Eval("RANDOM_EXAM_RESULT_DETAIL_ID")%>,<%#Eval("Is_Remove")%>)" class="underline"><b>调整机位</b></a>
                                     <a href="#" onclick="resetSeat(<%#Eval("RANDOM_EXAM_RESULT_DETAIL_ID")%>,<%#Eval("Is_Remove")%>)" class="underline" title="将已分配的指纹考试机位重置"><b>重置机位</b></a> 
                                </itemtemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="主键" Visible="false" DataField="RANDOM_EXAM_RESULT_DETAIL_ID">
                        </asp:BoundField>
                        <asp:BoundField HeaderText="姓名" DataField="姓名">
                            <itemstyle horizontalalign="Center" wrap="false" />
                            <headerstyle horizontalalign="Center" wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="微机教室" DataField="微机教室">
                            <itemstyle horizontalalign="Center" wrap="false" />
                            <headerstyle horizontalalign="Center" wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="机位" DataField="机位">
                            <itemstyle horizontalalign="Center" wrap="false" />
                            <headerstyle horizontalalign="Center" wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="员工编码" DataField="员工编码">
                            <itemstyle horizontalalign="Center" wrap="false" />
                            <headerstyle horizontalalign="Center" wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="身份证号码" DataField="身份证号码">
                            <itemstyle horizontalalign="Center" wrap="false" />
                            <headerstyle horizontalalign="Center" wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="单位" DataField="单位">
                            <itemstyle horizontalalign="Center" wrap="false" />
                            <headerstyle horizontalalign="Center" wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="车间" DataField="车间">
                            <itemstyle horizontalalign="Center" wrap="false" />
                            <headerstyle horizontalalign="Center" wrap="false" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="试卷次序" DataField="Exam_SEQ_NO" Visible="false">
                            <itemstyle horizontalalign="Center" wrap="false" />
                            <headerstyle horizontalalign="Center" wrap="false" />
                        </asp:BoundField>
                       <asp:TemplateField HeaderText="完成考试" >
                            <headerstyle horizontalalign="Center" wrap="false" />
                             <itemstyle horizontalalign="Center" wrap="false" />
                            <ItemTemplate>
                            <asp:CheckBox ID="chSelect" runat="server" Enabled="False" Checked='<%#Eval("Is_Remove").ToString()=="1"%>' />
                        </ItemTemplate>
                       </asp:TemplateField> 
                    </Columns>
                </yyc:SmartGridView>
            </div>
        </div>
        <input type="hidden" name="refresh" />
        <input type="hidden" name="reset" />
    </form>
</body>
</html>
