<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemEnabled.aspx.cs" Inherits="RailExamWebApp.Book.ItemEnabled" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>禁用试题</title>
   <script type="text/javascript" src="../Common/JS/Common.js"></script> 
   <script type="text/javascript">
  function logout()
  {
          top.ReturnValue='true';
          top.close();
  }
  
  function ItemEnabledList()
  {
       var search=window.location.search;
      var str= search.substring(search.indexOf("?")+1);
   　 var ret = showCommonDialog('/RailExamBao/Book/ItemEnabledList.aspx?'+str,'dialogWidth:800px;dialogHeight:600px;');
        if(ret == 'true')
        {
            return false; 
        }
       
       return false; 
  }
   </script>
</head>
<body onbeforeunload="logout()">
    <form id="form1" runat="server">
    <div>
        <table width="100%">
               <tr>
                <td align="center" valign="middle" style="height:40px">
                    请选择对该章节试题的操作 
                </td>
               </tr>
               <tr>
                <td align="center"style="height:15px">
                <asp:Button ID ="btnAll" runat="server"  CssClass="button" Text ="全部禁用" OnClick="btnAll_Click" /> &nbsp;&nbsp;
                <asp:Button ID="btnPart" runat="server"  CssClass="button" Text="部分禁用"  OnClientClick="return  ItemEnabledList();"/>&nbsp;&nbsp;
               <asp:Button ID="btnCancel" runat="server"  CssClass="button" OnClientClick="top.ReturnValue='true';top.close(); " Text="不 禁 用" />&nbsp;&nbsp;
                </td>
               </tr>
        </table>
    </div>
    </form>
</body>
</html>
