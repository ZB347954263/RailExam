<%@ Page Language="C#" AutoEventWireup="true" Codebehind="InformationSystem.aspx.cs"
    Inherits="RailExamWebApp.AssistBook.InformationSystem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>��������</title>

    <script type="text/javascript">        
        function tvInformation_onNodeSelect(sender, eventArgs)
        {
            // �鿴�ڵ�
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
            // ����������
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
        
        // ��������ڵ�
        // modeΪ����ͬ��/�Ӽ��ı�ʶ
        // 'brother'Ϊͬ����'child'Ϊ�Ӽ�
        function addNode(mode)
        {
            var flagupdate=document.getElementById("HfUpdateRight").value;
            if(flagupdate=="False")
            {
                alert("��û��Ȩ��ʹ�øò�����");                       
                return;
            }
        
            var info_id = 0;
            var node = tvInformation.get_selectedNode();    
            
            if(node)
          	{          	    
     	       info_id = node.get_id();
          	}
          	// ���û��ѡ��ڵ�ͬʱ���ǵ���������Ӽ����򵯳���ʾ����������
          	else if(mode=='child')
          	{
          	    alert('��ѡ��һ���ڵ�');
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
                alert("��û��Ȩ��ʹ�øò�����");                       
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
                
                    var url = 'InformationSystemDetail.aspx?mode=edit&id=' + node.get_id();   // �˴�mode����Ϊ�̶�ֵedit��������Ǵ�Ϊ�޸�ҳ�����������淽���е�����������
     	            var name = 'EditNode';
     	            var features = 'height=330, width=310,,left='+cleft+',top='+ctop+', toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no';
     	            window.open(url,name,features);
                }
                else
                {
                    alert('�ڵ���Ϣ�����޷��༭');
                }
            }
            else
            {
                alert('����ѡ��ڵ�');
            }
        }
        
        function deleteConfirm()
        {
            var node = tvInformation.get_selectedNode();
            if(node)
            {
                return confirm('ȷ��ɾ����?');
            }
            else
            {
                alert('��ѡ��Ҫɾ���Ľڵ�');
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
                        ���ϵȼ�����</div>
                    <div id="separator1">
                    </div>
                    <div id="current1">
                        ���ϵȼ�</div>
                </div>
                <div id="welcomeInfo1">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <asp:Button ID="lnkbtnUp" runat="server" OnClick="lnkbtnUp_Click" CssClass="button"
                        Text="����"></asp:Button>
                    <asp:Button ID="lnkbtnDown" runat="server" OnClick="lnkbtnDown_Click" CssClass="button"
                        Text="����"></asp:Button>
                    <input type="button" value="����ͬ��" class="button" onclick="addNode('brother')" />
                    <input type="button" value="�����¼�" class="button" onclick="addNode('child')" />
                    <input type="button" value="�� ��" class="button" onclick="editNode()" />
                    <asp:Button ID="imgbtnDel" runat="server" CssClass="button" Text="ɾ ��" OnClientClick="return deleteConfirm();"
                        OnClick="imgbtnDel_Click" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        ���ϵȼ�</div>
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
                                ��������...
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
                        ���ϵȼ���ϸ��Ϣ</div>
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
