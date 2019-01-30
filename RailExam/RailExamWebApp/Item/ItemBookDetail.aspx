<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ItemBookDetail.aspx.cs"
    Inherits="RailExamWebApp.Item.ItemBookDetail" %>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>教材详细信息</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
        function tvOrg_onNodeCheckChange(sender, eventArgs)
        {
            if(document.getElementById('hfMode').value != "ReadOnly")
            {
                var node = eventArgs.get_node();

                if(node.get_nodes().get_length() > 0)
                {
                    //check(node,node.get_checked());
                    if(node.get_checked())
                    {
                        node.checkAll();
                    }
                    else
                    {
                        node.unCheckAll();
                    }
                     node.set_checked(node.get_checked());
                  }
            }
        }

        function tvPost_onNodeCheckChange(sender, eventArgs)
        {
            if(document.getElementById('hfMode').value != "ReadOnly")
            {
//                var node = eventArgs.get_node();

//                if(node.get_nodes().get_length() > 0)
//                {
//                     if(node.get_checked())
//                    {
//                        node.checkAll();
//                    }
//                    else
//                    {
//                        node.unCheckAll();
//                    }
//                     node.set_checked(node.get_checked());
//                }
                 var node = eventArgs.get_node();
                 var state = node.get_checked();
                
                if(state)
                { 
                    node.checkAll();
                    node.set_checked(state); 
                    checkUp(node,node.get_checked());
                }
                else
                {
                    node.unCheckAll();
                    node.set_checked(state); 
                    var n=0;
                    if(node.get_parentNode())
                    {
                        for(var i=0;i<node.get_parentNode().get_nodes().get_length();i++)
                        {
                            if(node.get_parentNode().get_nodes().getNode(i).get_checked())
                            {
                                n = n + 1;
                            }
                        }
                        if(n == 0)
                        {
                            UnChecked(node);
                            IsTop(node);
                        }
                    }
                }            
            }
        }
  
      
    function checkUp(node,state)
    {
        node.set_checked(state);
        if(node.get_parentNode())
        {
            checkUp(node.get_parentNode(),state);
        }
    }    
    
     function UnChecked(node)
    {
        if(node.get_parentNode())
        {
           node.get_parentNode().set_checked(false);
           UnChecked(node.get_parentNode());
        }
    }
    
    function IsTop(node)
    {
        if(node.get_parentNode())
        {
            IsTop(node.get_parentNode());
        }
        else
        {
             IsCheck(node);
        }
    }
    
    function CheckParent(node)
    {
        if(node.get_parentNode())
        {
           node.get_parentNode().set_checked(true);
           CheckParent(node.get_parentNode());
        }
    }
    
    function IsCheck(node)
    {
        if(node.get_nodes().get_length() > 0)
        {
            for(var i=0;i<node.get_nodes().get_length();i++)
            { 
                if(node.get_nodes().getNode(i).get_checked())
                {
                     CheckParent(node.get_nodes().getNode(i));                        
                }
                else
                {
                    IsCheck(node.get_nodes().getNode(i));
                }
            }  
         }
    } 
       
       function checkAllPost()
       {
                for(var i = 0; i < tvPost.get_nodes().get_length(); i ++)
                {  
//                     check(tvPost.get_nodes().getNode(i),form1.chkPost.checked);
                        if(form1.chkPost.checked)
                        {
                            tvPost.get_nodes().getNode(i).checkAll();
                        }
                        else
                        {
                            tvPost.get_nodes().getNode(i).unCheckAll();
                        }
                         tvPost.get_nodes().getNode(i).set_checked(form1.chkPost.checked);   
                }
      } 
       
       function ExamCheck(node)
       {
            if(node.get_length()>0)
            {
                for(var i = 0; i < node.get_length(); i ++)
                {  
                   //alert( node.getNode(i).get_text());
                    if(ExamCheckChild(node.getNode(i)))
                   {
                        return true;
                   } 
                } 
            }
       } 
       
        function ExamCheckChild(node)
        {
            var childNodes = node.get_nodes();
           var count = childNodes.get_length();
           
          if(node.get_checked()==1)
          {
             return true;
          } 
                      
            for(var i = 0; i < count; i ++)
            {
                   if(ExamCheckChild(childNodes.getNode(i)))
                  {
                      return true; 
                 }  
            } 
        }
      
       
        function selectTrainType()
        {
            if(document.getElementById('hfMode').value != "ReadOnly")
            {
                var selectedTrainType = window.showModalDialog('../Common/MultiSelectTrainType.aspx?type=Item&id='+document.getElementById("hfTrainTypeID").value, 
                    '', 'help:no; status:no; dialogWidth:300px;dialogHeight:620px;scroll:no;');

                if(! selectedTrainType)
                {
                    return;
                }

                document.getElementById('hfTrainTypeID').value = selectedTrainType.split('|')[0];
                document.getElementById('txtTrainTypeName').value = selectedTrainType.split('|')[1];
            }
        }

        function selectKnowledge()
        {
            if(document.getElementById('hfMode').value != "ReadOnly")
            {
                var selectedKnowledge = window.showModalDialog('../Common/SelectKnowledge.aspx?type=Item', 
                    '', 'help:no; status:no; dialogWidth:300px;dialogHeight:590px;scroll:no');

                if(! selectedKnowledge)
                {
                    return;
                }

                document.getElementById('hfKnowledgeID').value = selectedKnowledge.split('|')[0];
                document.getElementById('txtKnowledgeName').value = selectedKnowledge.split('|')[1];
            }
        }
        
        function selectOrganization()
        {
            if(document.getElementById('hfMode').value != "ReadOnly")
            {
                var selectedOrganization = window.showModalDialog('../Common/SelectOrganization.aspx?Type=Station', 
                    '', 'help:no; status:no; dialogWidth:320px;dialogHeight:620px;scroll:no;');

                if(! selectedOrganization)
                {
                    return;
                }
                
                document.getElementById("hfPublishOrgID").value = selectedOrganization.split('|')[0];
                document.getElementById("txtPublishOrgName").value = selectedOrganization.split('|')[1];
            }
        }
	 
	 function BookNew()
	 {
	       if(form1.txtBookName.value == "")
          {
            alert('教材名称不能为空！');
            return false;
          } 
           if(form1.txtKnowledgeName.value == "")
          {
            alert('知识体系不能为空！');
            return false;
          } 
          
           if(form1.txtTrainTypeName.value == "")
          {
            alert('培训类别不能为空！');
            return false;
          }
          
          if(form1.txtPublishOrgName.value == "")
          {s
            alert('编制单位不能为空！');
            return false;
          } 
          
        　if(!ExamCheck(tvOrg.get_nodes()))
          {
            alert('请设置组织机构适用范围！');
            return　false;
          }
          
          if(!ExamCheck( tvPost.get_nodes()))
          {
            alert('请设置工作岗位适用范围！');
            return　false;
          }
                    
          if(form1.ddlIsGroup.value == "2")
          {
            alert('请设置班组长适用范围！');
            return false;
          }
          
          if(form1.ddlTech.value == "0")
          {
            alert('请设置技能等级适用范围！');
            return false;
          }
          
          return true;
	 }
	 
	 function SaveNext(id)
	 {
            var ret = showCommonDialog("/RailExamBao/Item/ItemBookChapter.aspx?id="+id,'dialogWidth:800px;dialogHeight:620px;');
            if(ret == "true")
            {
               window.returnValue ="true";
               window.close(); 
            }
	 }
	 function init()
	 {
	    if(form1.SuitRange.value == 0)
	    {
	        document.getElementById("ImgSelectOrg").style.display = "none";
	    }
	 }
    </script>

    <link href="/RailExamWebApp/Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
</head>
<body onload="init();">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        教材详细信息</div>
                </div>
                <div id="button">
                    <asp:Button ID="SaveNextButton" runat="server" Text="保存继续" CssClass="button" CausesValidation="true"
                        OnClientClick="return BookNew();" OnClick="SaveNextButton_Click" />&nbsp;&nbsp;
                </div>
            </div>
            <div id="content">
                <table>
                    <tr>
                        <td style="text-align: center; width: 50%; vertical-align: top;" valign="top">
                            <table class="contentTable">
                                <tr>
                                    <td style="width: 20%">
                                        教材名称<span class="require">*</span></td>
                                    <td style="width: 80%">
                                        <asp:TextBox ID="txtBookName" MaxLength="50" runat="server" Width="230px">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvBookName" runat="server" EnableClientScript="true"
                                            Display="none" ErrorMessage="“教材名称”不能为空！" ControlToValidate="txtBookName">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        知识体系<span class="require">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtKnowledgeName" runat="server" ReadOnly="true" Width="230px">
                                        </asp:TextBox>
                                        <img id="ImgSelectKnowledge" runat="server" style="cursor: hand;" name="ImgSelectKnowledge"
                                            onclick="selectKnowledge();" src="../Common/Image/search.gif" alt="选择知识体系" border="0" />
                                        <asp:RequiredFieldValidator ID="rfvKnowledgeName" runat="server" EnableClientScript="true"
                                            Display="none" ErrorMessage="“知识体系”不能为空！" ControlToValidate="txtKnowledgeName">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        培训类别<span class="require">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtTrainTypeName" runat="server" ReadOnly="true" Width="230px">
                                        </asp:TextBox>
                                        <img id="ImgSelectTrainType" style="cursor: hand;" onclick="selectTrainType();" src="../Common/Image/search.gif"
                                            alt="选择培训类别" border="0" />
                                        <asp:RequiredFieldValidator ID="rfvTypeName" runat="server" EnableClientScript="true"
                                            Display="none" ErrorMessage="“培训类别”不能为空！" ControlToValidate="txtTrainTypeName">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        编制单位<span class="require">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtPublishOrgName" runat="server" ReadOnly="true" Width="230px">
                                        </asp:TextBox>
                                        <img id="ImgSelectOrg" style="cursor: hand;" name="ImgSelectOrg" onclick="selectOrganization();"
                                            src="../Common/Image/search.gif" alt="选择编制单位" border="0" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" EnableClientScript="true"
                                            Display="none" ErrorMessage="“编制单位”不能为空！" ControlToValidate="txtPublishOrgName">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td >
                                        班组长</td>
                                    <td>
                                        <asp:DropDownList ID="ddlIsGroup" runat="server">
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                            <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        技能等级</td>
                                    <td>
                                        <asp:DropDownList ID="ddlTech" DataSourceID="odsTechType" DataTextField="TypeName"
                                            DataValueField="TechnicianTypeID" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td></tr>
                        <tr>
                        <td style="text-align: left;">
                            <table>
                                <tr style="height: 15px;">
                                    <td style="width: 25%; vertical-align: bottom;">
                                        适用范围：组织机构
                                    </td>
                                    <td style="width: 25%; vertical-align: middle;">
                                        适用范围：工作岗位&nbsp;
                                        <input type="checkbox" onclick="checkAllPost()" name="chkPost" />全选</td>
                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:TreeView ID="tvOrg" runat="server" Height="280" Width="170" EnableViewState="true"
                                            KeyboardEnabled="true">
                                            <ClientEvents>
                                                <NodeCheckChange EventHandler="tvOrg_onNodeCheckChange" />
                                            </ClientEvents>
                                        </ComponentArt:TreeView><br />
                                    </td>
                                    <td>
                                        <ComponentArt:TreeView ID="tvPost" runat="server" Height="280" Width="170" EnableViewState="true"
                                            KeyboardEnabled="true">
                                            <ClientEvents>
                                                <NodeCheckChange EventHandler="tvPost_onNodeCheckChange" />
                                            </ClientEvents>
                                        </ComponentArt:TreeView>
                                        <br />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:HiddenField ID="hfMode" runat="server" />
        <asp:HiddenField ID="hfKnowledgeID" runat="server" />
        <asp:HiddenField ID="hfTrainTypeID" runat="server" />
        <asp:HiddenField ID="hfPublishOrgID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
        <asp:ObjectDataSource ID="odsTechType" runat="server" SelectMethod="GetTechnicianType"
            TypeName="RailExam.BLL.TechnicianTypeBLL" DataObjectTypeName="RailExam.Model.TechnicianType">
        </asp:ObjectDataSource>
        <asp:HiddenField ID="hfOrderIndex" runat="server" />
        <input type="hidden" name="SuitRange" value='<%=PrjPub.CurrentLoginUser.SuitRange %>' />
    </form>
</body>
</html>
