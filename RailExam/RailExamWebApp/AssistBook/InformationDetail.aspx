<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InformationDetail.aspx.cs"
    Inherits="RailExamWebApp.AssistBook.InformationDetail" %>

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
        function selectTrainType()
        {
            if(document.getElementById('hfMode').value != "ReadOnly")
            {
                var selectedTrainType = window.showModalDialog('InformationLevelSelect.aspx?id='+document.getElementById("hfTrainTypeID").value, 
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
                var selectedKnowledge = window.showModalDialog('InformationSystemSelect.aspx', 
                    '', 'help:no; status:no; dialogWidth:300px;dialogHeight:590px;scroll:no');

                if(! selectedKnowledge)
                {
                    return;
                }

                document.getElementById('hfKnowledgeID').value = selectedKnowledge.split('|')[0];
                document.getElementById('txtKnowledgeName').value = selectedKnowledge.split('|')[1];
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
            
            var re= window.open("InformationChapter.aspx?id="+id+"&Mode=Edit","BookChapter"," Width=800px; Height=600px,left="+cleft+",top="+ctop+",status=false,resizable=no",true);		
            re.focus();  
        }
        
       
     function BookUpdate()
     {
          if(form1.txtBookName.value == "")
          {
            alert('资料名称不能为空！');
            return false;
          } 
           if(form1.txtKnowledgeName.value == "")
          {
            alert('资料领域不能为空！');
            return false;
          } 
          
           if(form1.txtTrainTypeName.value == "")
          {
            alert('资料等级不能为空！');
            return false;
          }
          
          if(form1.txtPublishOrgName.value == "")
          {
            alert('编制单位不能为空！');
            return false;
          }

//          if(form1.UpdateRecord.value =="1")
//          {
//               var search = window.location.search;
//               var str= search.substring(search.indexOf("&")+1);
//               var id = str.substring(str.indexOf("=")+1); 
//               var ret = showCommonDialog("/RailExamBao/Book/BookChapterUpdate.aspx?BookID="+id + "&ChapterID=0&Object=bookinfo",'dialogWidth:600px;dialogHeight:410px;');
//               if(ret == "true")
//	             {
//	                return true;
//	             }
//	        }
//	        else
//	        {
//	            return true;
//	        }
	        
	        return true;
	 }  
	 
	 function BookNew()
	 {
	       if(form1.txtBookName.value == "")
          {
            alert('资料名称不能为空！');
            return false;
          } 
           if(form1.txtKnowledgeName.value == "")
          {
            alert('资料领域不能为空！');
            return false;
          } 
          
           if(form1.txtTrainTypeName.value == "")
          {
            alert('资料等级不能为空！');
            return false;
          }
          
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
          
	       
          return true;
	 }
	 
	 function SaveNext(id)
	 {
            var ret = showCommonDialog("/RailExamBao/AssistBook/InformationChapter.aspx?id="+id + "&Type=Add&Mode=Insert",'dialogWidth:800px;dialogHeight:620px;');
            if(ret == "true")
            {
                    window.opener.frames["ifBookInfo"].form1.Refresh.value ="true";
                   window.opener.frames["ifBookInfo"].form1.submit();
                   window.close(); 
            }
	 }

    </script>

    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
</head>
<body >
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        资料详细信息</div>
                </div>
                <div id="button">
                    <asp:Button ID="btnChapter" runat="server" Text="章节管理" CssClass="button" CausesValidation="false"
                        Visible="false" OnClientClick="ManageChapter();" />&nbsp;&nbsp;
                    <asp:Button ID="SaveButton" runat="server" Text="保  存" CssClass="button" CausesValidation="false"
                        OnClientClick="return BookUpdate();" OnClick="SaveButton_Click" />&nbsp;&nbsp;
                    <asp:Button ID="SaveExitButton" runat="server" Text="保存退出" CssClass="button" CausesValidation="true"
                        OnClientClick="return BookNew();" OnClick="SaveExitButton_Click" />&nbsp;&nbsp;
                    <asp:Button ID="SaveNextButton" runat="server" Text="保存继续" CssClass="button" CausesValidation="true"
                        OnClientClick="return BookNew();" OnClick="SaveNextButton_Click" />&nbsp;&nbsp;
                    <asp:Button ID="CancelButton" runat="server" Text="关  闭" CssClass="button" CausesValidation="false"
                        OnClientClick="window.close();" />
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 15%">
                            资料名称<span class="require">*</span></td>
                        <td style="width: 35%">
                            <asp:TextBox ID="txtBookName" MaxLength="50" runat="server" >
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBookName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="“资料名称”不能为空！" ControlToValidate="txtBookName">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 15%">
                            资料编号</td>
                        <td style="width: 35%">
                            <asp:TextBox ID="txtBookNo" MaxLength="30" runat="server" >
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%">
                            资料领域<span class="require">*</span></td>
                        <td style="width: 35%">
                            <asp:TextBox ID="txtKnowledgeName" runat="server" ReadOnly="true" Width="90%">
                            </asp:TextBox>
                            <img id="ImgSelectKnowledge" runat="server" style="cursor: hand;" name="ImgSelectKnowledge"
                                onclick="selectKnowledge();" src="../Common/Image/search.gif" alt="选择资料领域" border="0" />
                            <asp:RequiredFieldValidator ID="rfvKnowledgeName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="“资料领域”不能为空！" ControlToValidate="txtKnowledgeName">
                            </asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 15%">
                            资料等级<span class="require">*</span></td>
                        <td style="width: 35%">
                            <asp:TextBox ID="txtTrainTypeName" runat="server" ReadOnly="true"  Width="90%">
                            </asp:TextBox>
                            <img id="ImgSelectTrainType" runat="server" style="cursor: hand;" onclick="selectTrainType();" src="../Common/Image/search.gif"
                                alt="选择资料等级" border="0" />
                            <asp:RequiredFieldValidator ID="rfvTypeName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="“资料等级”不能为空！" ControlToValidate="txtTrainTypeName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            编制单位<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtPublishOrgName" runat="server" >
                            </asp:TextBox>
                        </td>
                        <td>
                            编制日期</td>
                        <td>
                            <uc1:date id="datePublishDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            作者</td>
                        <td>
                            <asp:TextBox ID="txtAuthors" MaxLength="100" runat="server"  >
                            </asp:TextBox>
                        </td>
                        <td>
                            关键字</td>
                        <td>
                            <asp:TextBox ID="txtKeyWords" MaxLength="50" runat="server" >
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            页数</td>
                        <td>
                            <asp:TextBox ID="txtPageCount" runat="server" >
                            </asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator2" runat="server" Display="None" ErrorMessage="页数必须为整数！"
                                MaximumValue="9999" MinimumValue="0" Type="Integer" ControlToValidate="txtPageCount"></asp:RangeValidator></td>
                        <td>
                            字数</td>
                        <td>
                            <asp:TextBox ID="txtWordCount" runat="server" >
                            </asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" Display="None" ErrorMessage="字数必须为整数！"
                                MaximumValue="99999999" MinimumValue="0" Type="Integer" ControlToValidate="txtWordCount"></asp:RangeValidator></td>
                    </tr>
                    <tr>
                        <td>
                            内容简介</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtDescription" runat="server" Height="60px" TextMode="MultiLine">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            录入人<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtCreatePerson" runat="server"  ReadOnly="True">
                            </asp:TextBox>
                        </td>
                        <td>
                            录入单位</td>
                        <td>
                            <asp:TextBox ID="txtOrg" runat="server"  ReadOnly="True">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            最后一次更新人</td>
                        <td>
                            <asp:TextBox ID="txtLastPerson" runat="server"  ReadOnly="True">
                            </asp:TextBox>
                        </td>
                        <td>
                            最后一次更新时间</td>
                        <td>
                            <asp:TextBox ID="txtLastDate" runat="server"  ReadOnly="True">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            网址</td>
                        <td colspan="3">
                            <asp:HyperLink ID="hlUrl" runat="server"  Target="_blank">
                            </asp:HyperLink>
                        </td>
                        </tr>
                    <tr>
                        <td>
                            备注</td>
                        <td  colspan="3">
                            <asp:TextBox ID="txtMemo" runat="server" MaxLength="50" >
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:HiddenField ID="hfMode" runat="server" />
        <asp:HiddenField ID="hfKnowledgeID" runat="server" />
        <asp:HiddenField ID="hfTrainTypeID" runat="server" />
        <asp:HiddenField ID="hfEmployeeID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
        <input type="hidden" name="Refresh" />
        <input type="hidden" name="RefreshCover" />
        <input type="hidden" name="RefreshUpdate" />
        <input type="hidden" name="UpdateRecord" value='<%=PrjPub.FillUpdateRecord %>' />
        <asp:HiddenField ID="hfOrderIndex" runat="server" />
        <input type="hidden" name="SuitRange" value='<%=PrjPub.CurrentLoginUser.SuitRange %>' />
    </form>
</body>
</html>
