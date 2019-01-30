<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectStudyHaErBin.aspx.cs"
    Inherits="RailExamWebApp.Online.Study.SelectStudyHaErBin" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>选择学习</title>

    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">
    function ShowIndex()
    {
        window.location = "/RailExamBao/Login.aspx";
    }
    
    function ShowAccount() 
    {
        var ret = window.open('/RailExamBao/Default.aspx','','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
        ret.focus();
    }
    
    function ShowAdmin() 
    {
        var ret = window.open('/RailExamBao/Default.aspx','','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
        ret.focus(); 
    }
  
    function ShowStudyBook()
    {
       var orgid = document.getElementById('hfOrgID').value;
       var postid = document.getElementById('hfPostID').value;
       var leader = document.getElementById("ddlIsGroup").value;
       var techid = document.getElementById("ddlTech").value;
       var employeID = document.getElementById("hfEmployeID").value;
       var employeename = document.getElementById("hfEmployeeName").value;
       
       var starttime = new Date().toLocaleString();
          if(form1.txtOrg.value == "")
          {
            alert('请选择单位！');
            return　false;
          }
          
          if(form1.txtPost.value == "")
          {
            alert('请选择职名！');
            return　false;
          }
                        
        var   cleft;   
        var   ctop;   
        cleft=(screen.availWidth-900)*.5;   
        ctop=(screen.availHeight-600)*.5; 
        
         var ret = showCommonDialogFull("/RailExamBao/Online/Study/StudyBookTree.aspx?OrgID="+orgid+"&PostID="+postid+"&Leader="+leader+"&Tech="+techid + "&EmployeeName=" + employeename,'dialogWidth:'+window.screen.width+'px;dialogHeight:'+window.screen.height+'px;');
         if(ret)
         {
            Callback1.callback(starttime,ret,document.getElementById("hfEmployeID").value);          
         }            
  }
  
  function ShowStudyCourseware()
  {
       var orgid = document.getElementById('hfOrgID').value;
       var postid = document.getElementById('hfPostID').value;
       var leader = document.getElementById("ddlIsGroup").value;
       var techid = document.getElementById("ddlTech").value;
      
       
          if(form1.txtOrg.value == "")
          {
            alert('请选择组织机构！');
            return　false;
          }
          
          if(form1.txtPost.value == "")
          {
            alert('请选择工作职名！');
            return　false;
          }
        var   cleft;   
        var   ctop;   
        cleft=(screen.availWidth-900)*.5;   
        ctop=(screen.availHeight-600)*.5;             
        var re= window.open("StudyCoursewareTree.aspx?OrgID="+orgid+"&PostID="+postid+"&Leader="+leader+"&Tech="+techid,"StudyCoursewareTree",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=no,status=no');		
        re.focus();  
   } 
  
    function SelectOrg()
    {
            var selectedTrainType = window.showModalDialog('/RailExamBao/Common/SelectOrganization.aspx?Type=Online', 
                '', 'help:no; status:no; dialogWidth:300px;dialogHeight:620px;scroll:no;');

            if(! selectedTrainType)
            {
                return;
            }

            document.getElementById('hfOrgID').value = selectedTrainType.split('|')[0];
            document.getElementById('txtOrg').value = selectedTrainType.split('|')[1];
    }
  
   function SelectPost()
    {
        var selectedPost = window.showModalDialog('/RailExamBao/Common/SelectPost.aspx', 
            '', 'help:no; status:no; dialogWidth:320px;dialogHeight:620px;scroll:no;');

        if(! selectedPost)
        {
            return;
        }
        
        document.getElementById("hfPostID").value = selectedPost.split('|')[0];
        document.getElementById("txtPost").value = selectedPost.split('|')[1];
   }
   
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center; color: #2D67CF;">
            <table cellspacing="10" cellpadding="1" border="0">
                <tr>
                    <td align="center" valign="middle" colspan="2">
                        <h2>
                            选择学习</h2>
                    </td>
                </tr>
                <tr>
                    <td nowrap valign="middle" style="width: 20%">
                        单 位
                    </td>
                    <td nowrap align="left">
                        <asp:TextBox ID="txtOrg" ReadOnly="True" runat="server" Columns="40"></asp:TextBox>
                        <input id="Button3" type="button" value="请选择" class="button" onclick="SelectOrg()" />
                    </td>
                </tr>
                <tr>
                    <td nowrap valign="middle" style="width: 10%">
                        职 名
                    </td>
                    <td nowrap align="left">
                        <asp:TextBox ID="txtPost" ReadOnly="true" runat="server" Columns="40"></asp:TextBox>
                        <input id="Button1" type="button" class="button" value="请选择" onclick="SelectPost()" />
                    </td>
                </tr>
                <tr>
                    <td nowrap valign="middle" style="width: 10%">
                        班组长
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlIsGroup" runat="server" Width="292">
                            <asp:ListItem Value="0">否</asp:ListItem>
                            <asp:ListItem Value="1">是</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td nowrap valign="middle" style="width: 10%">
                        技能等级
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlTech" DataSourceID="odsTechType" DataTextField="TypeName"
                            DataValueField="TechnicianTypeID" runat="server" Width="292">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <input id="btnOK" type="button" value="确  定" onclick="ShowStudyBook()" class="button" />
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hfPostID" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <asp:ObjectDataSource ID="odsTechType" runat="server" SelectMethod="GetAllTechnicianType"
            TypeName="RailExam.BLL.TechnicianTypeBLL" DataObjectTypeName="RailExam.Model.TechnicianType">
        </asp:ObjectDataSource>
        <asp:HiddenField ID="hfEmployeeName" runat="server" />
        <asp:HiddenField ID="hfEmployeID" runat="server" />
        <ComponentArt:CallBack ID="Callback1" runat="server" OnCallback="Callback1_Callback">
        </ComponentArt:CallBack>
    </form>
</body>
</html>
