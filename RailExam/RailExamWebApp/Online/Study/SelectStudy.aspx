<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectStudy.aspx.cs" Inherits="RailExamWebApp.Online.Study.SelectStudy" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>专业学习</title>
    <link href="../style/SelectStudy.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript">
       function GoBack()
      {
        var search = window.location.search;
        var str = search.substring(search.indexOf('=')+1);
       
        if(str == "student")
       {
            window.location ="/RailExamBao/Online/Study/Study.aspx?type=student";
       }  
       else
       {
            window.location = "/RailExamBao/Online/Study/Study.aspx?type=teacher";
       } 
      }   
    
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
                        
         var re= window.open("StudySelectTree.aspx?OrgID="+orgid+"&PostID="+postid+"&Leader="+leader+"&Tech="+techid,"StudyBook",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=no,status=no');		
         re.focus();  
    }
 
  
    function SelectOrg()
    {
            var selectedTrainType = window.showModalDialog('/RailExamBao/Common/SelectOrganization.aspx?Type=Station', 
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
        <table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0">
            <tr align="center">
                <td valign="middle">
                    <div id="MainArea">
                        <div id="SelectArea">
                            <table cellspacing="24" cellpadding="1" border="0">
                                <tr>
                                    <td nowrap align="left" style="width: 295px">
                                        <asp:TextBox ID="txtOrg" ReadOnly="true" runat="server" Columns="30"></asp:TextBox>
                                        <span class="SelectOrg" onclick="SelectOrg()"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap align="left" style="width: 295px">
                                        <asp:TextBox ID="txtPost" ReadOnly="true" runat="server" Columns="30"></asp:TextBox>
                                        <span class="SelectPost" onclick="SelectPost()"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 295px">
                                        <asp:DropDownList ID="ddlIsGroup" runat="server" Width="222px">
                                            <asp:ListItem Value="0">否</asp:ListItem>
                                            <asp:ListItem Value="1">是</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left"  style="width: 295px">
                                        <asp:DropDownList ID="ddlTech" DataSourceID="odsTechType" DataTextField="TypeName"
                                            DataValueField="TechnicianTypeID" runat="server" Width="222px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="SelectStudyArea">
                            <span id="SelectStudyBook"><a href="#" onclick="ShowStudyBook()">
                                <img src="../image/SelectStudyOK.gif" alt="" /></a></span><%-- <span id="SelectStudyCourseware">
                                    <a href="#" onclick="ShowStudyCourseware()">
                                        <img src="/RailExamBao/Online/image/StudyCourseware.gif" alt="" /></a></span>--%>
                        </div>
                        <div id="FunctionArea">
                            <span class="Function"><span class="FunctionBarLeft"></span><span onclick="GoBack()"
                                class="FunctionName">后退</span><span class="FunctionBarRight"></span> </span>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hfPostID" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <asp:ObjectDataSource ID="odsTechType" runat="server" SelectMethod="GetAllTechnicianType"
            TypeName="RailExam.BLL.TechnicianTypeBLL" DataObjectTypeName="RailExam.Model.TechnicianType">
        </asp:ObjectDataSource>
    </form>
</body>
</html>
