<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssistBookDetail.aspx.cs" Inherits="RailExamWebApp.AssistBook.AssistBookDetail" %>
<%@ Import namespace="RailExamWebApp.Common.Class"%>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>辅导教材详细信息</title>
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
                var node = eventArgs.get_node();

                if(node.get_nodes().get_length() > 0)
                {
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

        function check(node, state)
        {
            var childNodes = node.get_nodes();
            var count = childNodes.get_length();
        
            node.set_checked(state);
            for(var i = 0; i < count; i ++)
            {
                //alert(childNodes.getNode(i));
                check(childNodes.getNode(i),state);
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
                var selectedTrainType = window.showModalDialog('../Common/MultiSelectTrainType.aspx?id='+document.getElementById("hfTrainTypeID").value, 
                    '', 'help:no; status:no; dialogWidth:300px;dialogHeight:590px;scroll:no;');

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
                var selectedKnowledge = window.showModalDialog('../Common/SelectAssistBookCategory .aspx', 
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
                    '', 'help:no; status:no; dialogWidth:320px;dialogHeight:600px;scroll:no;');

                if(! selectedOrganization)
                {
                    return;
                }
                
                document.getElementById("hfPublishOrgID").value = selectedOrganization.split('|')[0];
                document.getElementById("txtPublishOrgName").value = selectedOrganization.split('|')[1];
            }
        }
        
        function ManageChapter()
        {
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5;          
         
            var search = window.location.search;
            var str= search.substring(search.indexOf("&")+1);
            var id = str.substring(str.indexOf("=")+1);        
            var re= window.open("AssistBookChapter.aspx?id="+id+"&Mode=Edit","BookChapter"," Width=800px; Height=600px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);		
            re.focus();  
        }
       
     function BookUpdate()
     {
           if(form1.txtBookName.value == "")
          {
            alert('辅导教材名称不能为空！');
            return false;
          } 
           if(form1.txtKnowledgeName.value == "")
          {
            alert('辅导教材体系不能为空！');
            return false;
          } 
          
//           if(form1.txtTrainTypeName.value == "")
//          {
//            alert('培训类别不能为空！');
//            return false;
//          }
          
          if(form1.txtPublishOrgName.value == "")
          {
            alert('编制单位不能为空！');
            return false;
          } 
          
          if(form1.txtDescription.value.length > 1000)
          {
            alert('内容简介不能为超过1000个字！');
            return false;
          }
          
//           if(!ExamCheck(tvOrg.get_nodes()))
//          {
//            alert('请设置组织机构适用范围！');
//            return　false;
//          }
//          
//          if(!ExamCheck( tvPost.get_nodes()))
//          {
//            alert('请设置工作岗位适用范围！');
//            return　false;
//          }

//         if(form1.ddlIsGroup.value == "2")
//          {
//            alert('请设置班组长适用范围！');
//            return false;
//          }
//          
//          if(form1.ddlTech.value == "0")
//          {
//            alert('请设置技能等级适用范围！');
//            return false;
//          }
          
          if(form1.UpdateRecord.value =="1")
          {
               var search = window.location.search;
               var str= search.substring(search.indexOf("&")+1);
               var id = str.substring(str.indexOf("=")+1); 
                var ret = showCommonDialog("/RailExamBao/AssistBook/AssistBookChapterUpdate.aspx?BookID="+id + "&ChapterID=0&Object=bookinfo",'dialogWidth:600px;dialogHeight:400px;');
                if(ret == "true")
	            {
	                return true;
	            }
	        }
	        else
	        {
	            return true;
	        }
	        
	        return false;
	 }  
	 
	 function BookNew()
	 {
	       if(form1.txtBookName.value == "")
          {
            alert('辅导教材名称不能为空！');
            return false;
          } 
           if(form1.txtKnowledgeName.value == "")
          {
            alert('辅导教材体系不能为空！');
            return false;
          } 
          
//           if(form1.txtTrainTypeName.value == "")
//          {
//            alert('培训类别不能为空！');
//            return false;
//          }
          
          if(form1.txtPublishOrgName.value == "")
          {
            alert('编制单位不能为空！');
            return false;
          } 
          
          if(form1.txtDescription.value.length > 1000)
          {
            alert('内容简介不能为超过1000个字！');
            return false;
          }
          
//        　if(!ExamCheck(tvOrg.get_nodes()))
//          {
//            alert('请设置组织机构适用范围！');
//            return　false;
//          }
//          
//          if(!ExamCheck( tvPost.get_nodes()))
//          {
//            alert('请设置工作岗位适用范围！');
//            return　false;
//          }
//                    
//          if(form1.ddlIsGroup.value == "2")
//          {
//            alert('请设置班组长适用范围！');
//            return false;
//          }
//          
//          if(form1.ddlTech.value == "0")
//          {
//            alert('请设置技能等级适用范围！');
//            return false;
//          }
          
          return true;
	 }
	 
	 function SaveNext(id)
	 {
//        var ret = showCommonDialog("/RailExamBao/ewebeditor/asp/ShowEditor.asp?BookID="+ id+"&Type=Add",'dialogWidth:1000;dialogHeight:800;dialogTop:0;dialogLeft:0;');
            var ret = showCommonDialog("/RailExamBao/AssistBook/AssistBookChapter.aspx?id="+id + "&Type=Add&Mode=Insert",'dialogWidth:800px;dialogHeight:620px;');
            if(ret == "true")
            {
                window.opener.frames["ifBookInfo"].form1.Refresh.value ="true";
               window.opener.frames["ifBookInfo"].form1.submit();
               window.close(); 
            }
	 }
    </script>

    <link href="/RailExamWebApp/Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        辅导教材详细信息</div>
                </div>
                <div id="button">
                    <asp:Button ID="btnChapter" runat="server" Text="章节管理" CssClass="button" CausesValidation="false" Visible="false" OnClientClick="ManageChapter();" />&nbsp;&nbsp;
                    <asp:Button ID="SaveButton" runat="server" Text="保  存" CssClass="button"  CausesValidation="false" OnClientClick="return BookUpdate();"  OnClick="SaveButton_Click"/>&nbsp;&nbsp;
                    <asp:Button ID="SaveExitButton" runat="server" Text="保存退出" CssClass="button"  CausesValidation="true" OnClientClick="return BookNew();" OnClick="SaveExitButton_Click" />&nbsp;&nbsp;
                    <asp:Button ID="SaveNextButton" runat="server" Text="保存继续" CssClass="button"  CausesValidation="true" OnClientClick="return BookNew();" OnClick="SaveNextButton_Click" />&nbsp;&nbsp;
                    <asp:Button ID="CancelButton" runat="server" Text="关  闭" CssClass="button" CausesValidation="false" OnClientClick="window.close();"  />
                </div>
            </div>
            <div id="content">
                <table>
                    <tr>
                         <td style="text-align: center; width: 50%; vertical-align: top;" valign="top">
                            <table class="contentTable">
                                <tr>
                                    <td style="width: 20%">辅导教材名称<span class="require">*</span></td>
                                    <td style="width: 80%">
                                        <asp:TextBox ID="txtBookName" MaxLength="50" runat="server"  Width="230px">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvBookName" runat="server" EnableClientScript="true"
                                            Display="none" ErrorMessage="“辅导教材名称”不能为空！" ControlToValidate="txtBookName">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>辅导教材体系<span class="require">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtKnowledgeName" runat="server" ReadOnly="true" Width="230px">
                                        </asp:TextBox>
                                        <img id="ImgSelectKnowledge" runat="server" style="cursor:hand;" name="ImgSelectKnowledge"  onclick="selectKnowledge();"
                                            src="../Common/Image/search.gif" alt="选择辅导教材体系" border="0" />
                                        <asp:RequiredFieldValidator ID="rfvKnowledgeName" runat="server" EnableClientScript="true"
                                            Display="none" ErrorMessage="“辅导教材体系”不能为空！" ControlToValidate="txtKnowledgeName">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>培训类别</td>
                                    <td>
                                        <asp:TextBox ID="txtTrainTypeName" runat="server" ReadOnly="true" Width="230px">
                                        </asp:TextBox>
                                        <img id="ImgSelectTrainType" style="cursor:hand;" onclick="selectTrainType();" src="../Common/Image/search.gif"
                                            alt="选择培训类别" border="0" />
                                        <asp:RequiredFieldValidator ID="rfvTypeName" runat="server" EnableClientScript="true"
                                            Display="none" ErrorMessage="“培训类别”不能为空！" ControlToValidate="txtTrainTypeName">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>辅导教材编号</td>
                                    <td>
                                        <asp:TextBox ID="txtBookNo" MaxLength="30" runat="server"  Width="230px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>编制单位<span class="require">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtPublishOrgName"  runat="server" ReadOnly="true" Width="230px">
                                        </asp:TextBox>
                                        <img id="ImgSelectOrg" style="cursor:hand;" name="ImgSelectOrg" onclick="selectOrganization();"
                                            src="../Common/Image/search.gif" alt="选择辅导教材体系" border="0" /> 
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" EnableClientScript="true"
                                            Display="none" ErrorMessage="“编制单位”不能为空！" ControlToValidate="txtPublishOrgName">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>编制日期</td>
                                    <td><uc1:Date ID="datePublishDate" runat="server" /></td>
                                </tr>
                                <tr>
                                    <td>编著者</td>
                                    <td>
                                        <asp:TextBox ID="txtAuthors" MaxLength="100" runat="server"  Width="230px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>主审</td>
                                    <td>
                                        <asp:TextBox ID="txtRevisers" MaxLength="50" runat="server" Width="230px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>责任主编</td>
                                    <td>
                                        <asp:TextBox ID="txtBookMaker" MaxLength="20" runat="server"  Width="230px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>封面设计</td>
                                    <td>
                                        <asp:TextBox ID="txtCoverDesigner" MaxLength="20" runat="server"  Width="230px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>关键字</td>
                                    <td>
                                        <asp:TextBox ID="txtKeyWords" MaxLength="50" runat="server"  Width="230px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>页数</td>
                                    <td>
                                        <asp:TextBox ID="txtPageCount" runat="server"  Width="230px">
                                        </asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" Display="None" ErrorMessage="页数必须为整数！"
                                            MaximumValue="9999" MinimumValue="0" Type="Integer" ControlToValidate="txtPageCount"></asp:RangeValidator></td>
                                </tr>
                                <tr>
                                    <td>字数</td>
                                    <td>
                                        <asp:TextBox ID="txtWordCount" runat="server"  Width="230px">
                                        </asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" Display="None" ErrorMessage="字数必须为整数！"
                                            MaximumValue="99999999" MinimumValue="0" Type="Integer" ControlToValidate="txtWordCount"></asp:RangeValidator></td>
                                </tr>
                                <tr>
                                    <td>内容简介</td>
                                    <td>
                                        <asp:TextBox ID="txtDescription" runat="server" Height="60px" Width="230px" TextMode="MultiLine">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>网址</td>
                                    <td>
                                        <asp:HyperLink ID="hlUrl" runat="server" Width="230px" Target="_blank">
                                        </asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td>备注</td>
                                    <td>
                                        <asp:TextBox ID="txtMemo" runat="server"  MaxLength="50" Width="230px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>                       
                        <td style="text-align: left; width: 20%; vertical-align: top;">
                            适用范围：组织机构
                            <ComponentArt:TreeView ID="tvOrg" runat="server" Height="535" Width="170" EnableViewState="true"
                                KeyboardEnabled="true">
                                <ClientEvents>
                                    <NodeCheckChange EventHandler="tvOrg_onNodeCheckChange" />
                                </ClientEvents>
                            </ComponentArt:TreeView><br />
                        </td>
                        <td style="text-align: left; width: 20%; vertical-align: top;">
                            适用范围：工作岗位&nbsp;<input  type="checkbox" onclick="checkAllPost()" name="chkPost"/>全选
                            <ComponentArt:TreeView ID="tvPost" runat="server" Height="530" Width="170" EnableViewState="true"
                                KeyboardEnabled="true">
                                <ClientEvents>
                                    <NodeCheckChange EventHandler="tvPost_onNodeCheckChange" />
                                </ClientEvents>
                            </ComponentArt:TreeView>
                            <br />
                           </td>
                           <td style="text-align: left; width: 10%; vertical-align: top;"> 
                                 适用范围：其它<br /><br />
                                 班组长
                            <asp:DropDownList ID="ddlIsGroup" runat="server">
                                <asp:ListItem Value="2">--请选择--</asp:ListItem>
                                <asp:ListItem Value="1">是</asp:ListItem>
                                <asp:ListItem Value="0">否</asp:ListItem>
                            </asp:DropDownList><br /><br />
                             技能等级
                                <asp:DropDownList ID="ddlTech" DataSourceID="odsTechType"
                                DataTextField="TypeName" DataValueField="TechnicianTypeID" runat="server" >
                            </asp:DropDownList>
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
         <input type="hidden" name="Refresh" />
        <input type="hidden" name="RefreshCover" /> 
         <input type="hidden" name="RefreshUpdate" /> 
          <input type="hidden" name="UpdateRecord" value='<%=PrjPub.FillUpdateRecord %>' />
          <asp:HiddenField ID="hfOrderIndex"  runat="server"/>
    </form>
</body>
</html>
