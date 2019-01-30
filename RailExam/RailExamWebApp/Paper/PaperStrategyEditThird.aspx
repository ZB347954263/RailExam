<%@ Page Language="C#" AutoEventWireup="True" Codebehind="PaperStrategyEditThird.aspx.cs"
    Inherits="RailExamWebApp.Paper.PaperStrategyEditThird" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>组卷策略详细列表</title>

    <script type="text/javascript">    
  	    function imgBtns_onClick(btn)
		{
		    window.frames["ifPaperStrategyBookChapterInfo"].document.getElementById(btn.id).onclick();
		}		
			
			  function ManagePaper(id)
        {          
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5;              
          
           var re = window.open("PaperPreview.aspx?id="+id,"PaperPreview","Width=800px; Height=600px,status=false,resizable=yes,left="+cleft+",top="+ctop+",scrollbars=yes",true);
           re.focus();
        }
        
		function add()
		{
		   var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-600)*.5;   
          ctop=(screen.availHeight-520)*.5;             
		 
		
			  var id=   document.getElementById('lbType').value;	
		        var re= window.open("PaperStrategyEditFourth.aspx?mode=Insert&id="+id ,'PaperStrategyEditFourth',' Width=600px; Height=520px,status=false,	left='+cleft+',top='+ctop+',resizable=no',true);		
				   re.focus(); 		
				   
		}	
		
		function ConfirmDelete()
		{
		  if(confirm("是否保存该策略为组卷策略？"))
				{
				var   cleft;   
                var   ctop;   
                cleft=(screen.availWidth-600)*.5;   
                ctop=(screen.availHeight-400)*.5; 
           	  
	            var selectedPaperStrategy = window.showModalDialog('../Common/GetStrategyName.aspx', 
                    '', 'help:no; status:no; dialogWidth:800px;dialogHeight:600px;');
                
                if(! selectedPaperStrategy)
                {
                    return;
                }   
                
                    document.getElementById('HFStrategyName').value = selectedPaperStrategy; 
					document.form1.Flag.value ="0";
					document.form1.submit();
				}
				else
				{
				document.form1.Flag.value ="1";
				document.form1.submit();
				}
		}		
		
		function lbType_onChange()
		{
		    var id=   document.getElementById('lbType').value;	
			window.frames["ifPaperStrategyBookChapterInfo"].location = "PaperStrategyBookChapterInfo.aspx?id=" +id;
		}
		
		   function mouseIsInXButton()
        {
        return (event.clientY < 0)  &&  ((document.body.offsetWidth - event.clientX) < 30);
        }

        	
		function logout()
		{	
		    if(mouseIsInXButton() || event.altKey || event.ctlKey)
		    {
		    var paperid=document.getElementById("Hfpaperid").value; 
		    var id=document.getElementById("HfstrategyID").value; 		    
		   
		    if(paperid!=null&&paperid!="")
		    {		    
		    window.opener.form1.strategyID.value=id;
		    window.opener.form1.submit();
		     	    
		    }			   
		    }
		}			
		
    </script>

</head>
<body onbeforeunload="logout()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        组卷策略</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        组卷策略详细列表</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td colspan="2">
                            <b>第三步：组卷策略详细列表</b>
                        </td>
                    </tr>
                    <tr>
                        <td valign="bottom">
                            试卷大题：</td>
                        <td>
                            组卷策略列表： &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="NewButton" runat="server" CssClass="buttonSearch" Text="新  增" OnClientClick="add();" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            <div style="float: left;">
                                <asp:ListBox ID="lbType" runat="server" Height="400px" Width="180px" onchange="lbType_onChange()">
                                </asp:ListBox>
                            </div>
                        </td>
                        <td>
                            <iframe id="ifPaperStrategyBookChapterInfo" src="PaperStrategyBookChapterInfo.aspx?id=<%= ViewState["value"].ToString() %>"
                                frameborder="0" scrolling="auto" width="570px" height="400px"></iframe>
                        </td>
                    </tr>
                </table>
                <div id="button" style="text-align: center;">
                    <br />
                    <asp:ImageButton ID="btnLast" runat="server" CausesValidation="False" OnClick="btnLast_Click"
                        ImageUrl="../Common/Image/last.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnSave" runat="server" OnClick="btnSave_Click" ImageUrl="../Common/Image/complete.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnCancel" runat="server" Visible="false" OnClientClick="return window.close();"
                        ImageUrl="../Common/Image/close.gif" />
                    <asp:ImageButton ID="btnPreview" runat="server" ImageUrl="../Common/Image/preview.gif"
                        Visible="false" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnedit" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                        ImageUrl="../Common/Image/edit.gif" Visible="false" />
                </div>
            </div>
        </div> <asp:HiddenField ID="Hfpaperid" runat="server" />
        <asp:HiddenField ID="HfstrategyID" runat="server" />
        <asp:HiddenField ID="HFStrategyName" runat="server" />
        <input type="hidden" name="Flag" />
    </form>
</body>
</html>
