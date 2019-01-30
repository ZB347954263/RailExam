<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="GetKnowledgeBook.aspx.cs" Inherits="RailExamWebApp.Common.GetKnowledgeBook" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>按教材体系编号取图书</title>
   <script type="text/javascript">
  function tvBookBook_onNodeCheckChange(sender,eventArgs)
  {
            var node = eventArgs.get_node();
            if(node.get_nodes().get_length() > 0)
            {
                node.set_contentCallbackUrl( "../Common/GetBookChapter.aspx?flag=2&id=" + node.get_id()); 
            }
  }
   </script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
