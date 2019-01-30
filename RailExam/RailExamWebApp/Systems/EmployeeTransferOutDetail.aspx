<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeTransferOutDetail.aspx.cs" Inherits="RailExamWebApp.Systems.EmployeeTransferOutDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>调出员工</title>
   <script type="text/javascript">
      function selectEmployee()
      {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
            //alert(document.getElementById("hfEmployeeID").value);
            var ret = window.open("SelectEmployee.aspx?nowEmployeeID="+ document.getElementById("hfNowEmployeeID").value +"&employeeID="+document.getElementById("hfEmployeeID").value,'SelectEmployee','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);	
            ret.focus();
      }
      
      function selectEmpty()
      {
          if(!window.confirm("您确定要清空所选员工吗？"))
         {
            return;
         }
         
         document.getElementById("hfEmployeeID").value = "";
         document.getElementById("txtEmployee").value = "";
      }
      
      function showConfirm()
      {
         if(document.getElementById("hfEmployeeID").value == "")
         {
            alert("请选择员工！");
            return false;
         }
         
         if(!window.confirm("您确定要将所选员工调入所选单位吗？"))
         {
            return false;
         }
         
         return true;
      }
   </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center">
            <br />
            <table>
                <tr>
                    <td>
                        <font style="color: #2D67CF;">调入单位：</font>
                    </td>
                    <td colspan="2" align="left">
                        <asp:DropDownList ID="ddlOrg" runat="server"></asp:DropDownList>
                    </td>
                </tr>
               <tr>
                    <td>
                        <font style="color: #2D67CF;">调出员工：</font>
                    </td>
                    <td align="left">
                        <asp:TextBox id="txtEmployee" ReadOnly="true" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    </td>
                    <td>
                        <input type="button" onclick="selectEmployee()" class="button" value="选  择" /><br /><br />
                         <input type="button" onclick="selectEmpty()" class="button" value="清  空" />
                    </td>
                </tr> 
                <tr>
                    <td colspan="3">
                        <br />
                        <asp:Button ID="btnOK" Text="确   定" CssClass="button" runat="server"  OnClientClick="return showConfirm();" OnClick="btnOk_Click" /></td>
                </tr>
            </table>
        </div>
        <asp:HiddenField  ID="hfEmployeeID" runat="server"/>
        <asp:HiddenField runat="server" ID="hfNowEmployeeID"/>
    </form>
</body>
</html>
