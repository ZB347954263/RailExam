<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ComputerRoomManage.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.ComputerRoomManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>员工信息</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Common/JS/Common.js"></script>
    <script type="text/javascript">
      function FindRecord()
        {
             if(window.frames["ifEmployeeInfo"].document.getElementById("query").style.display == "none")
                    window.frames["ifEmployeeInfo"].document.getElementById("query").style.display = "";
                else
                    window.frames["ifEmployeeInfo"].document.getElementById("query").style.display = "none";
        }
        
        function selectArow(rowIndex) {
            var t = document.getElementById("grdEntity");
            for (var i = 1; i < t.rows.length; i++) {
                if (i - 1 == rowIndex) {
                    t.rows(i).style.backgroundColor = "#FFEEC2";
                }
                else {
                    if ((i - 1) % 2 == 0) {
                        t.rows(i).style.backgroundColor = "#EFF3FB";
                    }
                    else {
                        t.rows(i).style.backgroundColor = "White";
                    }
                }
            }
        }

        function AddComputerRoom() {
            var selectOrgId = document.getElementById("hfSelectOrg").value;
            if(selectOrgId==null||selectOrgId.length==0)
            {
                alert("请选择一个机构");
                return;
            }
            window.frames["ifEmployeeInfo"].location = 'ComputerDetail.aspx?OrgID=' + selectOrgId+'&Type=2';
        }

        function tvView_onNodeSelect(sender, eventArgs) {
            var node = eventArgs.get_node();
            if (node) {
               
                type = "Org";
                id = node.get_id();
                idpath = node.get_value();
                document.getElementById("hfSelectOrg").value = id;
                window.frames["ifEmployeeInfo"].location = "ComputerManageInfo.aspx?ID=" + id + "&idpath=" + idpath + "&type=" + type;
                node.expand();
            }
        }
        
        function checkedBtnsChangeCallBack_onCallbackComplete(sender, eventArgs) {
        }
        
        function showImport() {
        	var selectOrgId = document.getElementById("hfSelectOrg").value;
            if(selectOrgId==null||selectOrgId.length==0)
            {
                alert("请选择一个机构");
                return;
            }
        	
        	var ret = window.showModalDialog("/RailExamBao/RandomExamTai/ComputerRoomExport.aspx?OrgID="+selectOrgId,
                        '', 'help:no; status:no; dialogWidth:800px;dialogHeight:600px;scroll:no;');
        	if(ret)
            {
                if (ret.indexOf("refresh")>=0) {
                   alert(ret.split('|')[1]);
                   form1.Refresh.value = 'refresh';
                   form1.submit();
                   form1.Refresh.value = '';
                }
                else
                {
                    alert(ret);
                }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="page">
        <div id="head">
            <div id="Div4">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        微机教室管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        微机教室信息</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img id="btnFind" onclick="FindRecord();" src="../Common/Image/find.gif" alt="" /> 
                    <img id="NewButton" onclick="AddComputerRoom();" src="../Common/Image/add.gif" alt=""/>
                    <input type="button" class="buttonLong" value="导入微机教室" onclick="showImport();" style="display: none;"/>
                </div>
            </div>
        </div>
        <div id="content"> 
            <div id="left" style="width: 200px">
                <div id="leftHead">
                    单位
                </div>
                <div id="leftContent">
                    <ComponentArt:CallBack ID="checkedBtnsChangeCallBack" runat="server" PostState="true">
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
                <div id="rightHead">
                    微机教室信息
                </div>
               <div id="rightContentWithNoHead">
                        <iframe id="ifEmployeeInfo" src="ComputerManageInfo.aspx?id=1&idpath=/1&type=Org" frameborder="0"
                            scrolling="auto" class="iframe"></iframe>
               </div>
            </div>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hfSelectOrg" />
    <asp:HiddenField runat="server" ID="hfLevelNum" />
    </form>
</body>
</html>
