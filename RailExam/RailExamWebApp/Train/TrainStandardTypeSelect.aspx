<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="TrainStandardTypeSelect.aspx.cs" Inherits="RailExamWebApp.Train.TrainStandardTypeSelect" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ѡ����ѵ���</title>
    <script type="text/javascript">
    
    function tvTrainType_onNodeCheckChange(sender, eventArgs)
    {   
         var node = eventArgs.get_node();
         var state = node.get_checked();
        
        if(state == true)
        { 
            check(node,node.get_checked());
            checkUp(node,node.get_checked());
        }
        else
        {
            check(node,node.get_checked());
            node.set_checked(false);
            var n=0;
            if(node.get_parentNode())
            {
                for(var i=0;i<node.get_parentNode().get_nodes().get_length();i++)
                {
                    if(node.get_parentNode().get_nodes().getNode(i).get_checked())
                    {
                        n = n + 1;
                    }
                }
                if(n == 0)
                {
                    UnChecked(node);
                    IsTop(node);
                }
            }
        }  

    }
    //��ĳһ�ڵ����и��ڵ�ȫ��UnCheck
    function UnChecked(node)
    {
        if(node.get_parentNode())
        {
           node.get_parentNode().set_checked(false);
           UnChecked(node.get_parentNode());
        }
    }
    
    //�ҵ�ĳһ�ڵ�ĵ�һ�����ڵ�
    function IsTop(node)
    {
        if(node.get_parentNode())
        {
            IsTop(node.get_parentNode());
        }
        else
        {
             IsCheck(node);
        }
    }
    
    //��ĳһ�ڵ����и��ڵ�ȫ��Check
    function CheckParent(node)
    {
        if(node.get_parentNode())
        {
           node.get_parentNode().set_checked(true);
           CheckParent(node.get_parentNode());
        }
    }
    
    //��ĳһ�ڵ�ĵ�һ�����ڵ㿪ʼ�������ӽڵ㣨�ݹ飩���������ĳһ�ڵ���Check�򽫸ýڵ����и��ڵ�ȫ��Check
    function IsCheck(node)
    {
        if(node.get_nodes().get_length() > 0)
        {
            for(var i=0;i<node.get_nodes().get_length();i++)
            { 
                if(node.get_nodes().getNode(i).get_checked())
                {
                     CheckParent(node.get_nodes().getNode(i));                        
                }
                else
                {
                    IsCheck(node.get_nodes().getNode(i));
                }
            }  
         }
    }
        
    
    function check(node,state)
    {
        node.set_checked(state);
        for(var i=0; i<node.get_nodes().get_length();i++)
        {
            check(node.get_nodes().getNode(i),state);
        }
    }
    
    function checkUp(node,state)
    {
        node.set_checked(state);
        if(node.get_parentNode())
        {
            checkUp(node.get_parentNode(),state);
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <ComponentArt:TreeView ID="tvTrainType" runat="server" Height="530" Width="200" EnableViewState="true" 
            NodeEditingEnabled="false" KeyboardEnabled="true" MultipleSelectEnabled="false"
            KeyboardCutCopyPasteEnabled="false" 
            AutoAssignNodeIDs="false" ExpandNodeOnSelect="false" CollapseNodeOnSelect="false">
            <ClientEvents>
                <NodeCheckChange EventHandler="tvTrainType_onNodeCheckChange" />
            </ClientEvents>
        </ComponentArt:TreeView> <br />
        <asp:Button ID="btnOk" runat="server" text="ȷ��" OnClick="btnOk_Click"/>
    </div>
    </form>
</body>
</html>
