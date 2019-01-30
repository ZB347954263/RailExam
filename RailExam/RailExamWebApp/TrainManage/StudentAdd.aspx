<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StudentAdd.aspx.cs" Inherits="RailExamWebApp.TrainManage.StudentAdd" %>

<%@ Register TagPrefix="yyc" Namespace="YYControls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
      <link href="../Style/css.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Common/JS/Common.js"></script>
    <title>选择学员</title>

    <script type="text/javascript">
 
    
    function select(obj)
    {
       var TBL=document.getElementById("grdStudent");
        for(var i=1;i<TBL.rows.length;i++)
        {
               TBL.rows[i].style.backgroundColor="White";
               if(i%2!=0)
                TBL.rows[i].style.backgroundColor="#EFF3FB";
        } 
        obj.style.backgroundColor="#FFEEC2";
    }
    function setIDToHid()
    {
      var arr=[];
      var hidID= document.getElementById("hidID");
      var TBL=document.getElementById("grdStudent");
   
      for(var i=1;i<TBL.rows.length;i++)
      { 
        
         var obj=TBL.rows[i].cells[0].childNodes[0];
         if(obj.type=="checkbox")
         {
            if(obj.checked==true)
              arr.push(TBL.rows[i].cells[0].innerText);
         }  
      }
       document.getElementById("hidID").value=arr;
       if(document.getElementById("hidID").value=="")
       {
          alert("请选择学员！");
          return false;
       }
    }
    
    function chkAll(obj)
    {
       var TBL=document.getElementById("grdStudent");
      for(var i=1;i<TBL.rows.length;i++)
      { 
         var ob=TBL.rows[i].cells[0].childNodes[0];
         if(ob.type=="checkbox")
             ob.checked=obj.checked;
      }
    }
    function shutRef()
    {
       window.returnValue="ref";
       window.close();
    }
     function shut()
     {
       window.close();
       return false;
     }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page" style="width: 683px">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current" runat="server">
                        添加学员</div>
                </div>
            </div>
            <div id="content" style="width: 693px">
                <table class="contentTable" width="300px">
                    <tr>
                        <td>
                            <yyc:SmartGridView ID="grdStudent" runat="server" PageSize="15" AllowPaging="true" 
                                AllowSorting="false" AutoGenerateColumns="false" DataKeyNames="train_plan_employee_id"
                                OnRowCreated="grdStudent_RowCreated"  
                                OnPageIndexChanging="grdStudent_PageIndexChanging"  >
                                <Columns>
                                    <asp:TemplateField ItemStyle-Width="50px">
                                        <headertemplate>
                            <asp:CheckBox ID="chkAll" runat="server" />
                            </headertemplate>
                                        <itemtemplate >
                                                <asp:CheckBox ID="item" runat="server"   />
                                                <span style="display:none" runat="server" id="spanID"><%# Eval("train_plan_employee_id")%></span>
                                             </itemtemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="姓名" ReadOnly="True" SortExpression="EMPLOYEE_NAME" />
                                    <asp:BoundField DataField="WORK_NO" HeaderText="员工编码" ReadOnly="True" SortExpression="WORK_NO" />
                                    <asp:BoundField DataField="SEX" HeaderText="性别" ReadOnly="True" SortExpression="SEX" />
                                    <asp:BoundField DataField="short_name" HeaderText="组织机构" ReadOnly="True" SortExpression="short_name" />
                                    <asp:BoundField DataField="post_name" HeaderText="职名" ReadOnly="True" SortExpression="post_name" />
                                    <asp:BoundField DataField="IS_GROUP_LEADERZH" HeaderText="是否班组长" ReadOnly="True"
                                        SortExpression="IS_GROUP_LEADERZH" />
                                </Columns>
                                
                            </yyc:SmartGridView>
                        </td>
                    </tr>
                </table>
                <table width="400px">
                    <tr>
                        <td colspan="4" align="center">
                            <asp:ImageButton ID="imgSave" runat="server" ImageUrl="../Common/Image/save.gif"
                                CausesValidation="false" OnClientClick=" return setIDToHid()" OnClick="imgSave_Click" />&nbsp;&nbsp;
                            &nbsp;&nbsp;
                            <asp:ImageButton ID="imgClose" OnClientClick="return shut()" runat="server" ImageUrl="../Common/Image/close.gif" />
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="hidID" runat="server" />
        <asp:HiddenField ID="hidPlanID" runat="server" />
        </div>
    </form>
</body>
</html>
