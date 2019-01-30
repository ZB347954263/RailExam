<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TJ_SelectTrainPlan.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.SelectTrainPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>选择培训计划</title>

    <script type="text/javascript">
   function selectValue(obj)
   { 
       
       var radios=document.getElementsByTagName("input"); 
        for(var i=0;i<radios.length;i++)
       {
          if(radios[i].type=="radio")
          { 
            radios[i].checked=false;
          }
       } 
       obj.checked=true;
       var nameid="";
       if(obj.id.indexOf("_")>0)
         nameid=obj.id.split('_')[1]; 
       var planName=document.getElementById("grdEntity$"+nameid+"$hidName").value;
        document.getElementById("hidPlanID").value=obj.value;
        document.getElementById("hidPlanName").value=planName;
   }
   
   function setValue()
   { 
      var planID=  document.getElementById("hidPlanID").value;
      var planName=  document.getElementById("hidPlanName").value;
      var returnValue=[];
      returnValue.push(planID);
      returnValue.push(planName);
      window.returnValue=returnValue;   //返回值为数组 (培训计划ID、培训计划名称)
      this.close();
   }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head" style="width: 820px">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current" runat="server">
                        培训计划信息</div>
                </div>
            </div>
            <div id="content">
                <div style="text-align: right;">
                    <input id="btnAdd" type="button" value="确  定" class="button" onclick="setValue()" />
                </div>
                <div style="text-align: center;">
                    <yyc:SmartGridView ID="grdEntity" runat="server" PageSize="10" AllowPaging="True"
                        AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="TRAIN_PLAN_ID"
                        OnRowCreated="grdEntity_RowCreated" DataSourceID="ObjectDataSource1" OnRowDataBound="grdEntity_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="操作">
                                <headertemplate>
                            选择
                            </headertemplate>
                                <itemtemplate>
                                <asp:RadioButton runat="server" id="radioID" value='<%# Eval("TRAIN_PLAN_ID") %>'></asp:RadioButton>
                               <asp:HiddenField runat="server" id="hidName" value='<%# Eval("TRAIN_PLAN_NAME") %>'></asp:HiddenField>
                          
                           </itemtemplate>
                                <headerstyle width="30px" horizontalalign="Center" wrap="False" />
                                <itemstyle width="30px" horizontalalign="Left" wrap="False" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="TRAIN_PLAN_ID" HeaderText="TRAIN_PLAN_ID" Visible="False"
                                ReadOnly="True" SortExpression="TRAIN_PLAN_ID" />
                            <asp:BoundField DataField="YEAR" HeaderText="年度" ReadOnly="True" SortExpression="YEAR" />
                            <asp:BoundField DataField="TRAIN_PLAN_NAME" HeaderText="计划名称" ReadOnly="True" SortExpression="TRAIN_PLAN_NAME" />
                            <asp:BoundField DataField="LOCATION" HeaderText="培训地点" ReadOnly="True" SortExpression="LOCATION" />
                            <asp:BoundField DataField="BEGINDATE" HeaderText="开班日期" ReadOnly="True" SortExpression="BEGINDATE"
                                DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="ENDDATE" HeaderText="结束日期" ReadOnly="True" SortExpression="ENDDATE"
                                DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="MAKEDATE" HeaderText="制定日期" ReadOnly="True" SortExpression="MAKEDATE"
                                DataFormatString="{0:yyyy-MM-dd}" />
                            <asp:BoundField DataField="MEMO" HeaderText="备注" ReadOnly="True" SortExpression="MEMO"
                                Visible="False" />
                            <asp:BoundField DataField="TRAINPLAN_TYPE_NAME" HeaderText="培训计划类别" ReadOnly="True"
                                SortExpression="TRAINPLAN_TYPE_NAME" />
                            <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="制定人" ReadOnly="True" SortExpression="EMPLOYEE_NAME" />
                            <asp:BoundField DataField="SPONSOR_UNIT_NAME" HeaderText="主办单位" ReadOnly="True" SortExpression="SPONSOR_UNIT_NAME" />
                            <asp:BoundField DataField="UNDERTAKE_UNIT_NAME" HeaderText="承办单位" ReadOnly="True"
                                SortExpression="UNDERTAKE_UNIT_NAME" />
                            <asp:BoundField DataField="TRAIN_PLAN_PHASE_NAME" HeaderText="计划阶段" ReadOnly="True"
                                Visible="False" SortExpression="TRAIN_PLAN_PHASE_NAME" />
                            <asp:BoundField DataField="ASSIST_UNIT" HeaderText="协办单位" ReadOnly="True" SortExpression="ASSIST_UNIT" />
                        </Columns>
                    </yyc:SmartGridView>
                </div>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="Get" TypeName="OjbData">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hidSql" Name="sql" PropertyName="Value" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:HiddenField ID="hidSql" runat="server" />
                <asp:HiddenField ID="hidPlanID" runat="server" />
                <asp:HiddenField ID="hidPlanName" runat="server" />
            </div>
        </div>
    </form>
    <script type="text/javascript">
     var radios=document.getElementsByTagName("input"); 
        for(var i=0;i<radios.length;i++)
       {
          if(radios[i].type=="radio")
          { 
             if(radios[i].value==document.getElementById("hidPlanID").value)
                radios[i].checked=true;
          }
       } 
    </script>
</body>
</html>
