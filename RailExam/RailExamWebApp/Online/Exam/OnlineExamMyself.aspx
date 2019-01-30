<%@ Page Language="C#" AutoEventWireup="true" Codebehind="OnlineExamMyself.aspx.cs"
    Inherits="RailExamWebApp.Online.Exam.OnlineExamMyself" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>自主测试</title>
    <link href="../style/ExamMyself.css" type="text/css" rel="stylesheet" />

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
            document.getElementById('hfOrgName').value = selectedTrainType.split('|')[1];
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
       document.getElementById("hfPostName").value = selectedPost.split('|')[1];
 
   }
  
  function SelectBook()
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
        var selectedBook = window.showModalDialog("SelectBookRange.aspx?itemTypeID=1&flag=2&OrgID="+orgid+"&PostID="+postid+"&Leader="+leader+"&Tech="+techid, 
        '', 'help:no; status:no; dialogWidth:350px;dialogHeight:660px');
  	
//  	     var selectedBook = window.showModalDialog("/RailExamBao/Common/SelectStrategyChapter.aspx?itemTypeID=1&flag=4&item=no&bookIds='1|2'", 
//        '', 'help:no; status:no; dialogWidth:350px;dialogHeight:660px');
        
        if(! selectedBook)
        {
            return false;
        }
           //alert(selectedBook); 
            var selectedChapter=selectedBook.split('$');
            var strBookID ="";
            var strBookName = "";
            var strType = "";
            var strTypeName = "";
             for(var i=0;i<selectedChapter.length;i++)
            {
                if(i==0)
               {
                    strBookID = strBookID  +selectedChapter[i].split('|')[1];
                    strBookName= strBookName + selectedChapter[i].split('|')[2];
                    strType= strType+selectedChapter[i].split('|')[3];
                    strTypeName = strTypeName  + selectedChapter[i].split('|')[2];  
              }
               else
               {
                     strBookID = strBookID + "/" +selectedChapter[i].split('|')[1];
                    strBookName= strBookName +","+ selectedChapter[i].split('|')[2];
                    strType= strType+"/"+selectedChapter[i].split('|')[3];
                    strTypeName = strTypeName + "," + selectedChapter[i].split('|')[2];  
              }   
            } 
             document.getElementById('hfBookID').value=strBookID;
//             document.getElementById('txtBookName').value  = strBookName;
             document.getElementById('hfRangeType').value = strType;
             document.getElementById('hfRangeName').value = strTypeName; 
} 

   function ShowExam()
  {
        if(document.getElementById('hfBookID').value == "")
       {
            alert('请选择教材！');
            return;
       } 
        var bookid = escape(document.getElementById('hfBookID').value); 
        var rangetype = escape(document.getElementById('hfRangeType').value);
        var num = form1.txtNum.value; 
        var time = document.getElementById("txtTime").value;
	    var re = window.open('FreeExamTitle.aspx?BookID='+bookid+'&RangeType='+rangetype+'&Num='+num+'&Time='+time,'index','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
	    re.focus();
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
                            <table cellspacing="8" cellpadding="1" border="0">
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
                                    <td align="left" style="width: 295px">
                                        <asp:DropDownList ID="ddlTech" DataSourceID="odsTechType" DataTextField="TypeName"
                                            DataValueField="TechnicianTypeID" runat="server" Width="222px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 295px">
                                        <span class="SelectBook" onclick="SelectBook()"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 295px">
                                        <div id="NumArea">
                                            <asp:TextBox ID="txtNum" runat="server" Width="88"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="“题数”应为大于1的整数！"
                                                Display="None" MaximumValue="9999" MinimumValue="1" Type="Integer" ControlToValidate="txtNum"></asp:RangeValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" EnableClientScript="true"
                                                Display="none" ErrorMessage="“题数”不能为空！" ControlToValidate="txtNum">
                                            </asp:RequiredFieldValidator></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" id="TimeArea" style="width: 295px">
                                        <asp:TextBox ID="txtTime" runat="server" Width="86px" Text="60"></asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="“时间”应为大于1的整数！"
                                            Display="None" MaximumValue="9999" MinimumValue="1" Type="Integer" ControlToValidate="txtTime"></asp:RangeValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" EnableClientScript="true"
                                            Display="none" ErrorMessage="“时间”不能为空！" ControlToValidate="txtTime">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="StartExamArea">
                            <span id="StartExam">
                                <asp:ImageButton ID="btnStart" runat="server" ImageUrl="~/Online/image/StartExam.gif"
                                    OnClick="btnStart_Click" />
                            </span>
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
        <asp:HiddenField ID="hfBookID" runat="server" />
        <asp:HiddenField ID="hfPostName" runat="server" />
        <asp:HiddenField ID="hfOrgName" runat="server" />
        <asp:HiddenField ID="hfRangeType" runat="server" />
        <asp:HiddenField ID="hfRangeName" runat="server" />
        <asp:ObjectDataSource ID="odsTechType" runat="server" SelectMethod="GetAllTechnicianType"
            TypeName="RailExam.BLL.TechnicianTypeBLL" DataObjectTypeName="RailExam.Model.TechnicianType">
        </asp:ObjectDataSource>
        <asp:ValidationSummary runat="server" ID="vd1" ShowMessageBox="true" ShowSummary="false" />
    </form>
</body>
</html>
