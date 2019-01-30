<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamStrategyInfo.aspx.cs"
    Inherits="RailExamWebApp.RandomExamOther.RandomExamStrategyInfo" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>组卷策略详细列表</title>

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
		      var re= window.open("StrategyEdit.aspx?itemTypeID="+ itemTypeID+"&mode=Insert&id="+id ,'StrategyEdit',' Width=600px; Height=520px,status=false,	left='+cleft+',top='+ctop+',resizable=no',true);		
				   re.focus(); 		
				   
		}
		
		function tvBookChapter_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            var search = window.location.search;
            var examid = search.substring(search.indexOf("id=")+3);
             var itemType = search.substring(search.indexOf("itemType=")+9,search.indexOf("&id="));
            var type;
             if(node && node.getProperty("isBook") == "true")
            {
                type = "book";
            }
            
            if(node && node.getProperty("isChapter") == "true")
            {
                type = "chapter";
            }
			window.frames["ifStrategyInfo"].location = "RandomExamStrategyDetail.aspx?type="+ type+"&bookChapterID="+ node.get_id()+"&id=" +examid+"&itemType="+itemType;
        }
    </script>

</head>
<body>
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
                            <b>第三步：设置取题范围</b>
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
                            </ComponentArt:CallBack>
                        </td>
                    </tr>
                </table>
                <div id="left">
                    <div style="width: 100%; height: 430px;">
                        <ComponentArt:TreeView ID="tvBookChapter" runat="server" Height="430">
                            <ClientEvents>
                                <NodeSelect EventHandler="tvBookChapter_onNodeSelect" />
                            </ClientEvents>
                        </ComponentArt:TreeView>
                    </div>
                </div>
                <div id="right">
                    <div style="width: 100%; height: 430px;">
                        <iframe id="ifStrategyInfo" frameborder="0" scrolling="auto" width="570px" height="430px">
                        </iframe>
                    </div>
                </div>
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
        </div>
    </form>

    <script type="text/javascript">
        var search = window.location.search;
        var examid = search.substring(search.indexOf("id=")+3);
        var mode =  search.substring(search.indexOf("&mode=")+6,search.indexOf("&itemType="));
        var itemType = search.substring(search.indexOf("itemType=")+9,search.indexOf("&id="));
        if(tvBookChapter && tvBookChapter.get_nodes().get_length() > 0)
        {
            tvBookChapter.get_nodes().getNode(0).select();
           window.frames["ifStrategyInfo"].location = "RandomExamStrategyDetail.aspx?mode="+ mode +"&type=book&bookChapterID="+  tvBookChapter.get_nodes().getNode(0).get_id() +"&id=" +examid+"&itemType="+ itemType;
       }
    </script>

</body>
</html>
