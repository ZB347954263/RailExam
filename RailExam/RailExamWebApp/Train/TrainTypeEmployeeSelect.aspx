<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainTypeEmployeeSelect.aspx.cs" Inherits="RailExamWebApp.Train.TrainTypeEmployeeSelect" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ѡ����ѵ���</title>
    <script type="text/javascript" >
         function tvTrainType_onNodeSelect(sender, eventArgs)
        {
            // �鿴�ڵ�
            var node = eventArgs.get_node();            
            var theFrame = window.frames["ifTrainTypeDetail"];
            theFrame.location = "../Online/Study/StudyCourseDetail.aspx?id=" + node.get_id();
        }   
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="left">
                    <div id="leftHead">��ѵ���</div>
                    <div id="leftContent">
                        <ComponentArt:TreeView ID="tvType" runat="server" EnableViewState="true">
                            <ClientEvents>
                                <NodeSelect EventHandler="tvTrainType_onNodeSelect" />
                            </ClientEvents>
                        </ComponentArt:TreeView>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        ��ѵ�����ϸ��Ϣ
                    </div>
                    <div id="rightContent">
                        <iframe id="ifTrainTypeDetail" frameborder="0" scrolling="auto" class="iframe">
                        </iframe>
                   </div>
                </div>
                <div>
                    <br />
                    <asp:ImageButton ID="btnOK" runat="server" ImageUrl="~/Common/Image/confirm.gif" OnClick="btnOk_Click" />
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        var theFrame = window.frames["ifTrainTypeDetail"];
        theFrame.location = "../Online/Study/StudyCourseDetail.aspx?id=" + tvType.get_nodes().getNode(0).get_id();
        
        // Preload CSS-referenced images
        (new Image()).src = '~/App_Themes/Default/images/Menu/group_background.gif';
        (new Image()).src = '~/App_Themes/Default/images/Menu/break_bg.gif';
    </script>
</body>
</html>