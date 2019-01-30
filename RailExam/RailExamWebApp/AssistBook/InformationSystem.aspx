<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InformationSystem.aspx.cs"
    Inherits="RailExamWebApp.AssistBook.InformationSystem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>资料领域</title>

    <script type="text/javascript">        
        function tvInformation_onNodeSelect(sender, eventArgs)
        {
            // 查看节点
            var node = eventArgs.get_node();
            var theFrame = window.frames["ifInformationDetail"];
            var theItem = document.getElementById("hfSelectedMenuItem");
            
            theItem.value = "";
            theFrame.location = "InformationSystemPreview.aspx?id=" + node.get_id();
            
            node.expand();
        }
        
        function tvInformationChangeCallBack_onCallbackComplete(sender, eventArgs)
        {
            var theItem = document.getElementById("hfSelectedMenuItem");
            if(theItem && theItem.value != "")
            {
                theItem.value == "";
            }
        
            var id = document.getElementById("hfMaxID").value;
            if(id == "0")
            {
                 tvInformation.selectNodeById(sender.get_parameter());
            }
            else
            {
                 tvInformation.selectNodeById(id);
                 tvInformation.findNodeById(id).select();
                document.getElementById("hfMaxID").value = "0"; 
            }
            
            if(!tvInformation.get_selectedNode())
            {
                tvInformation.get_nodes().getNode(0).select();
            } 
        }
         
        function tvInformationChangeCallBack_onLoad(sender, eventArgs)
        {
            // 给树重排序
            if(tvInformation.get_nodes().get_length() > 0)
            {
//                tvInformation.expandAll();
            }
        }
        
        var btnIds = new Array("fvInformation_NewButton", "fvInformation_EditButton", "fvInformation_DeleteButton",
            "fvInformation_UpdateButton", "fvInformation_UpdateCancelButton",
            "fvInformation_InsertButton", "fvInformation_InsertCancelButton","fvInformation_NewButtonBrother");
            
        function imgBtns_onClick(btn)
        {          
              
        }
        
        // 新增级别节点
        // mode为新增同级/子级的标识
        // 'brother'为同级；'child'为子级
        function addNode(mode)
        {
            var flagupdate=document.getElementById("HfUpdateRight").value;
            if(flagupdate=="False")
            {
                alert("您没有权限使用该操作！");                       
                return;
            }
        
            var info_id = 0;
            var node = tvInformation.get_selectedNode();    
            
            if(node)
          	{          	    
     	       info_id = node.get_id();
          	}
          	// 如果没有选择节点同时又是点击的新增子级，则弹出提示并结束函数
          	else if(mode=='child')
          	{
          	    alert('请选择一个节点');
          	    return;
          	}
          	
          	var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-330)*.5;   
            ctop=(screen.availHeight-315)*.5;
          	
     	    var url = 'InformationSystemDetail.aspx?mode='+mode+'&id=' + info_id;
     	    var name = 'NewNode';
     	    var features = 'height=330, width=310,left='+cleft+',top='+ctop+',  toolbar=no, menubar=no,scrollbars=no, resizable=no, location=no, status=no';
     	    window.open(url,name,features);
        }

        function editNode()
        {
            var flagupdate=document.getElementById("HfUpdateRight").value;
            if(flagupdate=="False")
            {
                alert("您没有权限使用该操作！");                       
                return;
            }
        
            var node = tvInformation.get_selectedNode();
            if(node)
            {
                if(node.get_id())
                {
                    var   cleft;   
                    var   ctop;   
                    cleft=(screen.availWidth-330)*.5;   
                    ctop=(screen.availHeight-315)*.5;   
                
                    var url = 'InformationSystemDetail.aspx?mode=edit&id=' + node.get_id();   // 此处mode参数为固定值edit，用来标记此为修改页。区别于上面方法中的新增操作。
     	            var name = 'EditNode';
     	            var features = 'height=330, width=310,,left='+cleft+',top='+ctop+', toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no';
     	            window.open(url,name,features);
                }
                else
                {
                    alert('节点信息有误，无法编辑');
                }
            }
            else
            {
                alert('请先选择节点');
            }
        }
        
        function deleteConfirm()
        {
            var node = tvInformation.get_selectedNode();
            if(node)
            {
                return confirm('确定删除吗?');
            }
            else
            {
                alert('请选择要删除的节点');
                return false;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location1">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent1">
                        资料等级管理</div>
                    <div id="separator1">
                    </div>
                    <div id="current1">
                        资料等级</div>
                </div>
                <div id="welcomeInfo1">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <asp:Button ID="lnkbtnUp" runat="server" OnClick="lnkbtnUp_Click" CssClass="button"
                        Text="上移"></asp:Button>
                    <asp:Button ID="lnkbtnDown" runat="server" OnClick="lnkbtnDown_Click" CssClass="button"
                        Text="下移"></asp:Button>
                    <input type="button" value="新增同级" class="button" onclick="addNode('brother')" />
                    <input type="button" value="新增下级" class="button" onclick="addNode('child')" />
                    <input type="button" value="编 辑" class="button" onclick="editNode()" />
                    <asp:Button ID="imgbtnDel" runat="server" CssClass="button" Text="删 除" OnClientClick="return deleteConfirm();"
                        OnClick="imgbtnDel_Click" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        资料等级</div>
                    <div id="leftContent">
                        <ComponentArt:CallBack ID="tvInformationChangeCallBack" runat="server" OnCallback="tvInformationChangeCallBack_Callback">
                            <Content>
                                <ComponentArt:TreeView ID="tvInformation" runat="server" EnableViewState="true">
                                    <ClientEvents>
                                        <NodeSelect EventHandler="tvInformation_onNodeSelect" />
                                    </ClientEvents>
                                </ComponentArt:TreeView>
                            </Content>
                            <LoadingPanelClientTemplate>
                                加载数据...
                            </LoadingPanelClientTemplate>
                            <ClientEvents>
                                <CallbackComplete EventHandler="tvInformationChangeCallBack_onCallbackComplete" />
                                <Load EventHandler="tvInformationChangeCallBack_onLoad" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        资料等级详细信息</div>
                    <div id="rightContent">
                        <iframe id="ifInformationDetail" frameborder="0" scrolling="auto" class="iframe"></iframe>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfSelectedMenuItem" runat="server" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="hfMaxID" runat="server" Value="0" />
        <asp:HiddenField ID="hfDelID" runat="server" Value="0" />
    </form>

    <script type="text/javascript">
        if(tvInformation && tvInformation.get_nodes().get_length() > 0)
        {
            tvInformation.get_nodes().getNode(0).select();
        }
    </script>

</body>
</html>
