<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamManageThird.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamManageThird" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>组卷策略详细列表</title>
<script type="text/javascript" src="../Common/JS/Common.js"></script>
    <script type="text/javascript">    
  	    function imgBtns_onClick(btn)
		{
		    window.frames["ifStrategyInfo"].document.getElementById(btn.id).onclick();
		}	
        
		function add()
		{
		      var   cleft;   
              var   ctop;   
              cleft=(screen.availWidth-600)*.5;   
              ctop=(screen.availHeight-520)*.5;             
		 
		
			  var str=   document.getElementById('lbType').value;	
		      var itemTypeID = str.split('|')[0];
		      var id = str.split('|')[1];
			
//		      var re= window.open("StrategyEdit.aspx?itemTypeID="+ itemTypeID+"&mode=Insert&id="+id ,'StrategyEdit',' Width=600px; Height=520px,status=false,	left='+cleft+',top='+ctop+',resizable=no',true);		
//		      re.focus(); 	
			
			  var ret = showCommonDialog("/RailExamBao/RandomExam/StrategyEdit.aspx?itemTypeID="+ itemTypeID+"&mode=Insert&subjectId="+id,'dialogWidth:600px;dialogHeight:520px;');
			  if(ret != "" && ret !=null) 
			  {
			  	 if(ret.indexOf("False")>=0) 
			  	 {
			  	 	form1.Refresh.value = ret.split('|')[1];
			  	 	form1.submit();
			  	 	form1.Refresh.value = "";
			  	 } 
			  	 else 
			  	 {
			  	    subjectCallback.callback();
			  	 }
			  } 
		}
		
		function subjectCallback_CallbackComplete() 
		{
		    var str=   document.getElementById('lbType').value;	
		    var itemTypeID = str.split('|')[0];
		    var id = str.split('|')[1];
			window.frames["ifStrategyInfo"].location = "StrategyInfo.aspx?itemTypeID="+ itemTypeID+"&id=" +id;
		}
		
		function lbType_onChange()
		{
		    var str=   document.getElementById('lbType').value;	
		      var itemTypeID = str.split('|')[0];
		      var id = str.split('|')[1];
			window.frames["ifStrategyInfo"].location = "StrategyInfo.aspx?itemTypeID="+ itemTypeID+"&id=" +id;
		}
		
		function init() {
			var search = window.location.search;
			if(search.indexOf("mode=ReadOnly")>=0) {
				document.getElementById("NewButton").style.display = "none";
				document.getElementById("hfMode").value = "readonly";
			}
		}
		
    </script>

</head>
<body onload="init()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="parent">
                        新增考试</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        取题范围</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td colspan="2">
                            <b>第三步：设置取题范围（注意：编辑完取题范围务必点击“下一步”按钮保存）</b>
                        </td>
                    </tr>
                                        <tr>
                        <td style="width:10%">
                            试卷大题信息：</td>
                        <td>
                            <asp:Label ID="lblSubject" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            组卷策略信息：</td>
                        <td>
                            <ComponentArt:CallBack ID="subjectCallback" runat="server" OnCallback="callback_callback">
                                <Content>
                                    <asp:Label ID="lblSubjectNow" runat="server"></asp:Label>
                                </Content>
                                <ClientEvents>
                                    <CallbackComplete EventHandler="subjectCallback_CallbackComplete"></CallbackComplete>
                                </ClientEvents>
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                    <tr>
                        <td valign="bottom">
                            试卷大题：</td>
                        <td>
                            取题范围列表： &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <input id="NewButton" class="buttonSearch" value="新  增" onclick="add();" type="button" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 15%;">
                            <div style="float: left;">
                                <asp:ListBox ID="lbType" runat="server" Height="500px" Width="180px" onchange="lbType_onChange()">
                                </asp:ListBox>
                            </div>
                        </td>
                        <td>
                            <iframe id="ifStrategyInfo" src="StrategyInfo.aspx?itemTypeID=<%= lbType.SelectedValue.Split('|')[0] %>&id=<%= lbType.SelectedValue.Split('|')[1] %>"
                                frameborder="0" scrolling="auto" width="800px" height="500px"></iframe>
                        </td>
                    </tr>
                </table>
                <div id="button" style="text-align: center;">
                    <br />
                    <asp:ImageButton ID="btnLast" runat="server" CausesValidation="False" OnClick="btnLast_Click"
                        ImageUrl="../Common/Image/last.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnSave" runat="server" OnClick="btnSave_Click" ImageUrl="../Common/Image/next.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" ImageUrl="../Common/Image/cancel.gif"
                        OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>   <input type="hidden" name="Refresh" />     
        <asp:HiddenField runat="server" ID="hfMode"/>
    </form>
</body>
</html>
