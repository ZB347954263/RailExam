<%@ Import Namespace="RailExamWebApp.Common.Class"%>
<%@ Page Language="C#" AutoEventWireup="True" Codebehind="CoursewareDetail.aspx.cs"
    Inherits="RailExamWebApp.Courseware.CoursewareDetail" %>

<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>课件详细信息</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        function tv1_onNodeCheckChange(sender, eventArgs)
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
   
        function tv2_onNodeCheckChange(sender, eventArgs)
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
   
        function check(node,state)
        {
            node.set_checked(state);
            for(var i = 0; i < node.get_nodes().get_length(); i ++)
            {
                check(node.get_nodes().getNode(i), state);
            }
        }
        
        function checkAllPost()
       {
            for(var i = 0; i < tvPost.get_nodes().get_length(); i ++)
            {  
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
                
                document.getElementById("hfProvideOrgID").value = selectedOrganization.split('|')[0];
                document.getElementById("txtProvideOrgName").value = selectedOrganization.split('|')[1];
            }
        }
        
        function selectTrainType()
        {
	        var selectTrainType = window.showModalDialog('../Common/MultiSelectTrainType.aspx?id='+document.getElementById("hfTrainTypeID").value,
                '', 'help:no; status:no; dialogWidth:300px;dialogHeight:600px');
            
            if(! selectTrainType)
            {
                return;
            }
            
            document.getElementById('hfTrainTypeID').value = selectTrainType.split('|')[0];
            document.getElementById('txtTrainTypeName').value = selectTrainType.split('|')[1];
        }
   
        function selectCoursewareType()
        { 
	        var selectCoursewareType = window.showModalDialog('../Common/SelectCoursewareType.aspx',
                '', 'help:no; status:no; dialogWidth:300px;dialogHeight:600px');
            
            if(! selectCoursewareType)
            {
                return;
            }
            
            document.getElementById('hfCoursewareTypeID').value = selectCoursewareType.split('|')[0];
            document.getElementById('txtCoursewareTypeName').value = selectCoursewareType.split('|')[1];
        }
       
       function Save()
       {
          if(form1.txtCoursewareName.value == "")
          {
            alert('课件名称不能为空！');
            return false;
          } 
          
          if(form1.txtCoursewareTypeName.value == "")
          {
            alert('知识体系不能为空！');
            return false;
          } 
          
//           if(form1.txtTrainTypeName.value == "")
//          {
//            alert('培训类别不能为空！');
//            return false;
//          }
          
          if(form1.txtProvideOrgName.value == "")
          {
            alert('编制单位不能为空！');
            return false;
          } 
          
          if(form1.txtDescription.value.length > 1000)
          {
            alert('内容简介不能为超过1000个字！');
            return false;
          }
          
          if(form1.txtMemo.value.length > 50)
          {
            alert('备注不能为超过50个字！');
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
       
     function init()
	 {
	    if(form1.SuitRange.value == 0)
	    {
	        document.getElementById("ImgSelectOrg").style.display = "none";
	    }
	 } 
    </script>

</head>
<body onload="init();">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        课件详细信息</div>
                </div>
                <div id="button">
                    <asp:ImageButton ID="SaveButton" runat="server" CausesValidation="True" OnClientClick="return Save();"
                        OnClick="SaveButton_Click" ImageUrl="../Common/Image/save.gif" />
                    <asp:ImageButton ID="CancelButton" runat="server" CausesValidation="false" OnClientClick="window.close();"
                        ImageUrl="../Common/Image/close.gif" />
                </div>
            </div>
            <div id="content">
                <table>
                    <tr>
                        <td style="text-align: center; width: 50%; vertical-align: top;" valign="top">
                            <table class="contentTable">
                                <tr>
                                    <td style="width: 20%">
                                        课件名称<span class="require">*</span></td>
                                    <td style="width: 80%" align="left">
                                        <asp:TextBox ID="txtCoursewareName" MaxLength="50" runat="server" Width="230px">
                                        </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCoursewareName" runat="server" EnableClientScript="true"
                                            Display="none" ErrorMessage="“课件名称”不能为空！" ControlToValidate="txtCoursewareName">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        知识体系<span class="require">*</span></td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCoursewareTypeName" runat="server" Width="230px" ReadOnly="true">
                                        </asp:TextBox>
                                        <img id="ImgSelectCoursewareType" style="cursor: hand;" name="ImgSelectCoursewareType"
                                         runat="server"   onclick="selectCoursewareType();" src="../Common/Image/search.gif" alt="选择知识体系"
                                            border="0" />
                                        <asp:RequiredFieldValidator ID="rfvCoursewareTypeName" runat="server" EnableClientScript="true"
                                            Display="none" ErrorMessage="“知识体系”不能为空！" ControlToValidate="txtCoursewareTypeName">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr style="display:none">
                                    <td>
                                        培训类别<span class="require">*</span></td>
                                    <td align="left">
                                        <asp:TextBox ID="txtTrainTypeName" runat="server" Width="230px" ReadOnly="true">
                                        </asp:TextBox>
                                        <img id="ImgSelectTrainType" style="cursor: hand;" onclick="selectTrainType();" src="../Common/Image/search.gif"
                                            alt="选择培训类别" border="0" />
                                        <asp:RequiredFieldValidator ID="rfvTrainTypeName" runat="server" EnableClientScript="true"
                                            Display="none" ErrorMessage="“培训类别”不能为空！" ControlToValidate="txtTrainTypeName">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        资料提供单位<span class="require">*</span></td>
                                    <td align="left">
                                        <asp:TextBox ID="txtProvideOrgName" runat="server" Width="230px" ReadOnly="true">
                                        </asp:TextBox>&nbsp;<img id="ImgSelectOrg" style="cursor: hand;" name="ImgSelectOrg" onclick="selectOrganization();"
                                            src="../Common/Image/search.gif" alt="选择资料提供单位" border="0" />
                                        <asp:RequiredFieldValidator ID="RfvProvideOrgName" runat="server" EnableClientScript="true"
                                            Display="none" ErrorMessage="“资料提供单位”不能为空！" ControlToValidate="txtProvideOrgName">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        完成时间</td>
                                    <td align="left">
                                        <uc1:Date ID="datePublishDate" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        编著者</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAuthors" MaxLength="100" runat="server" Width="230px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        主审</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtRevisers" runat="server" MaxLength="50" Width="230px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        关键字</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtKeyWord" runat="server" MaxLength="50" Width="230px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        内容简介</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Height="105px"
                                            Width="230px">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        课件上传</td>
                                    <td align="left">
                                        <asp:FileUpload ID="File1" runat="server" /><br/>
                                        <span style="color: red">注：上传文件名必须与课件名称一致</span> 
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        浏览地址</td>
                                    <td align="left">
                                        <asp:HyperLink ID="hlUrl" runat="server" Width="230px" Target="_blank"></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        备注</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMemo" runat="server" Width="230px" Height="100px" TextMode="MultiLine">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="text-align: left;">
                            <table>
                                <tr  style="height: 15px;">
                                    <td style="width: 20%; vertical-align:bottom;">
                                        适用范围：组织机构
                                    </td>
                                    <td style="width: 20%; vertical-align:middle;">
                                        适用范围：工作岗位&nbsp;<input type="checkbox" onclick="checkAllPost()" name="chkPost" />全选
                                    </td>
                                    <td style="width: 15%; vertical-align:bottom;">
                                        适用范围：其它
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <ComponentArt:TreeView ID="tvOrg" runat="server" Height="530" Width="165" EnableViewState="true"
                                            KeyboardEnabled="true">
                                            <ClientEvents>
                                                <NodeCheckChange EventHandler="tv1_onNodeCheckChange" />
                                            </ClientEvents>
                                        </ComponentArt:TreeView><br />
                                    </td>
                                    <td>
                                        <ComponentArt:TreeView ID="tvPost" runat="server" Height="530" Width="165" EnableViewState="true"
                                            KeyboardEnabled="true">
                                            <ClientEvents>
                                                <NodeCheckChange EventHandler="tv2_onNodeCheckChange" />
                                            </ClientEvents>
                                        </ComponentArt:TreeView><br />
                                    </td>
                                    <td style="vertical-align: top;">
                                        <br />
                                        班组长
                                        <asp:DropDownList ID="ddlIsGroup" runat="server">
                                            <asp:ListItem Value="2">--请选择--</asp:ListItem>
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                        </asp:DropDownList><br />
                                        <br />
                                        技能等级
                                        <asp:DropDownList ID="ddlTech" DataSourceID="odsTechType" DataTextField="TypeName"
                                            DataValueField="TechnicianTypeID" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:HiddenField ID="hfMode" runat="server" />
            <asp:HiddenField ID="hfProvideOrgID" runat="server" />
            <asp:HiddenField ID="hfCoursewareTypeID" runat="server" />
            <asp:HiddenField ID="hfTrainTypeID" runat="server" />
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                ShowSummary="false" />
            <asp:ObjectDataSource ID="odsTechType" runat="server" SelectMethod="GetTechnicianType"
                TypeName="RailExam.BLL.TechnicianTypeBLL" DataObjectTypeName="RailExam.Model.TechnicianType">
            </asp:ObjectDataSource>
        </div>
        <asp:HiddenField ID="hfOrderIndex" runat="server" />
        <input type="hidden" name="SuitRange" value ='<%=PrjPub.CurrentLoginUser.SuitRange %>' />
    </form>
</body>
</html>
