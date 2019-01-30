<%@ Page Language="C#" AutoEventWireup="True" Codebehind="Employee.aspx.cs" Inherits="RailExamWebApp.Systems.Employee" %>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>员工</title>
    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
        function tvView_onNodeSelect(sender, eventArgs)
        {
            var node = eventArgs.get_node();
            if(node)
            {
                var rbn = $F("rbnOrg");
                
                var type = "";
                var id = "";
                var idpath = ""; 
                if(rbn.checked)
                {
                    type = "Org";
                    id = node.get_id();
                    idpath = node.get_value();
                }
                else
                {
                    type = "Post";
                    id = node.get_id();
                     idpath = node.get_value();
                }

                window.frames["ifEmployeeInfo"].location = "EmployeeInfo.aspx?id=" + id + "&idpath="+ idpath +"&type=" + type;
               node.expand(); 
            }
        }
        
        function $F(objId)
        {
            return document.getElementById(objId);
        }
        
        function rbnClicked(btn)
        {
            if(btn.id=="rbnPost")
            {
                if(form1.Post.checked = true)
                {
                    form1.Org.checked=false;
                    if(tvView && tvView.get_nodes().get_length() > 0)
                    {
                        tvView.get_nodes().getNode(0).select();
                        window.frames["ifEmployeeInfo"].location = "EmployeeInfo.aspx?id=" +  tvView.get_nodes().getNode(0).get_id() + "&idpath="+  tvView.get_nodes().getNode(0).get_value() +"&type=Post";
                   }
                }      
            }
            else
            {
                if(form1.Org.checked = true)
                {
                    form1.Post.checked = false;
                     if(tvView && tvView.get_nodes().get_length() > 0)
                    {
                        tvView.get_nodes().getNode(0).select();
                        window.frames["ifEmployeeInfo"].location = "EmployeeInfo.aspx?id=" +  tvView.get_nodes().getNode(0).get_id() + "&idpath="+  tvView.get_nodes().getNode(0).get_value() +"&type=Org";
                   }               
                }
            }
            checkedBtnsChangeCallBack.callback(btn.id);
        }
        
        function checkedBtnsChangeCallBack_onCallbackComplete(sender, eventArgs)
        {
        	if(tvView && tvView.get_nodes().get_length() > 0)
            {
                tvView.get_nodes().getNode(0).select();
            }
        }
        
        function imgBtns_onClick(btn)
        {
            var flagupdate=document.getElementById("HfUpdateRight").value;
            var tvType=document.getElementById("tvType").value;
            if(flagupdate=="False"&&(btn.action == "edit"||btn.action == "new"))
            {
                alert("您没有权限使用该操作！");                       
                return;
            }
            
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;        
            
            if(btn.action == "new")
            {
                var pageUrl = "";
                if(form1.IsWuhan.value == "True")
                {
                    pageUrl = "EmployeeDetail.aspx";
                }
                else
                {
                    pageUrl = "/RailExamBao/RandomExamOther/EmployeeDetail.aspx";
                }
                var node = tvView.get_selectedNode();
                if(form1.Org.checked == true)
                {
                    if(!node.get_parentNode())
                    {
                           alert('只能在车间或班组新增员工！');
                       return; 
                    } 
                    else
                    {
                        if(!node.get_parentNode().get_parentNode() && form1.hfSuitRange.value == 1)
                        {
                           alert('只能在车间或班组新增员工！');
                           return;                        
                        }  
                    }
                    if(node)
				    {
	                    var ret = window.open(pageUrl+"?mode=Insert&OrgID="+node.get_id(),'BookDetail','Width=800px; Height=600px,status=false,left='+cleft+',top='+ctop+',resizable=no',true);
			            ret.focus();
			        }
			        else
			        {
			            var ret = window.open(pageUrl+"?mode=Insert&OrgID="+1,'BookDetail','Width=800px; Height=600px,status=false,left='+cleft+',top='+ctop+',resizable=no',true);
			            ret.focus();
			        }
                }
                if(form1.Post.checked == true)
                {
                    if(node.get_nodes().get_length() > 0)
                    {
                        alert('请选择职名！');
                        return;
                    }
                    if(node)
				    {
	                    var ret = window.open(pageUrl+"?mode=Insert&Post="+node.get_id(),'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
			            ret.focus();
			        }
			        else
			        {
			            var ret = window.open(pageUrl+"?mode=Insert&Post="+1,'BookDetail','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
			            ret.focus();
			        }
                }
            }
            else
            {
                if(window.frames["ifEmployeeInfo"].document.getElementById("query").style.display == "none")
                    window.frames["ifEmployeeInfo"].document.getElementById("query").style.display = "";
                else
                    window.frames["ifEmployeeInfo"].document.getElementById("query").style.display = "none";
            }
        }
        
         function init()
        {
            if(form1.IsWuhanOnly.value == "True")
            {
                document.getElementById("NewButton").style.display="none";
            }
            else
            {
                document.getElementById("NewButton").style.display="";
            }
            
            if(form1.IsWuhan.value == "False" && document.getElementById("hfSuitRange").value=="1")
            {
                document.getElementById("NewButton").style.display="";
            }
            else
            {
                document.getElementById("DeleteButton").style.display="none";
            }
        }
        
        function deleteOrg()
        {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-400)*.5;   
            ctop=(screen.availHeight-200)*.5;   
            if(form1.IsWuhan.value == "False")
            {
                var ret = window.open('/RailExamBao/RandomExamOther/DeleteEmployeeByOrg.aspx','DeleteEmployeeByOrg','Width=400px; Height=200px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
	            ret.focus();
            }
        }
        
        function exportTemplate()
        {
            var ret = showCommonDialog("/RailExamBao/RandomExamOther/ExportEmployee.aspx?Type=template",'dialogWidth:320px;dialogHeight:30px;');
            if(ret != "")
            {
             document.getElementById("hfRefresh").value = ret;
             form1.submit();
           }
        }
        
        function exportEmployee()
        {
            var node = tvView.get_selectedNode();
            if(node.get_id() == 1)
            {
                alert("请选择站段或车间或班组导出员工信息！");
                return
            }
            var ret = showCommonDialog("/RailExamBao/RandomExamOther/ExportEmployee.aspx?OrgID="+node.get_id()+"&Type=employee",'dialogWidth:320px;dialogHeight:30px;');
            if(ret != "")
            {
             document.getElementById("hfRefresh").value = ret;
             form1.submit();
           }
        }
    </script>

</head>
<body onload="init();">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        系统管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        职员管理</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img id="NewButton" onclick="imgBtns_onClick(this);" src="../Common/Image/add.gif"
                        action="new" alt=""  style="display:none"/>
                    <img id="FindButton" onclick="imgBtns_onClick(this);" src="../Common/Image/find.gif"
                        action="find" alt="" />
                    <img id="DeleteButton" onclick="deleteOrg();" src="../Common/Image/delete.gif"
                        action="DeleteButton" alt="" />
                    <img id="ExportTemplate" onclick="exportTemplate();" src="../Common/Image/ExportTemplate.jpg"
                        action="ExportTemplate" alt="" />
                    <img id="ExportEmployee" onclick="exportEmployee();" src="../Common/Image/ExportEmployee.jpg"
                        action="ExportEmployee" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        <div id="divSwitchBtns">
                            分类:
                            <input id="rbnOrg" name="Org" onclick="rbnClicked(this);" type="radio" checked="checked" />组织机构
                            <input id="rbnPost" name="Post" onclick="rbnClicked(this);" type="radio" />工作岗位
                        </div>
                    </div>
                    <div id="leftContent">
                        <ComponentArt:CallBack ID="checkedBtnsChangeCallBack" runat="server" PostState="true"
                            OnCallback="checkedBtnsChangeCallBack_Callback">
                            <Content>
                                <ComponentArt:TreeView ID="tvView" runat="server" EnableViewState="false">
                                    <ClientEvents>
                                        <NodeSelect EventHandler="tvView_onNodeSelect" />
                                    </ClientEvents>
                                </ComponentArt:TreeView>
                            </Content>
                            <ClientEvents>
                                <CallbackComplete EventHandler="checkedBtnsChangeCallBack_onCallbackComplete" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                    </div>
                </div>
                <div id="right">
                    <div id="rightContentWithNoHead">
                        <iframe id="ifEmployeeInfo" src="EmployeeInfo.aspx?id=1&idpath=/1&type=Org" frameborder="0"
                            scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="tvType" runat="server" />
        <input type="hidden" name="Refresh" />
        <input type="hidden" name="RefreshAdd" />
        <input type="hidden" name="hfSuitRange" value='<%=PrjPub.CurrentLoginUser.SuitRange %>' />
        <input type="hidden" name="IsWuhanOnly" value='<%=PrjPub.IsWuhanOnly()%>' />
         <input type="hidden" name="IsWuhan" value='<%=PrjPub.IsWuhan()%>' />
          <asp:HiddenField ID="hfRefresh" runat="server" />
   </form>

    <script type="text/javascript">
        if(tvView && tvView.get_nodes().get_length() > 0)
        {
            tvView.get_nodes().getNode(0).select();
        }
    </script>

</body>
</html>
