<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ComputerMACInfo.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.ComputerMACInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <title>机位详细信息</title>

    <script type="text/javascript">
    function Save() {  
    	var DT = document.getElementById("grdEntity");
    	for (var i = 1; i < DT.rows.length; i++) {
    		var oneNum=DT.rows[i].cells[0].childNodes[0].innerText;
    		var oneMAC = DT.rows[i].cells[1].childNodes[0].value;
    		if(oneMAC!="")
    		{
    			for (var j = 1; j < DT.rows.length; j++) {
    				if (j != oneNum) {
    					if (oneMAC == DT.rows[j].cells[1].childNodes[0].value) {
    						alert(oneMAC + "已存在，请重新输入！");
    						DT.rows[i].cells[1].childNodes[0].focus();
    						return false;
    					}
    				}
    			}
    		}
    	}
    	return true;
      }
    
    
   function checkMAC(obj) { //检查MAC地址合法性
   	var macStr = obj.value;
   	var pattern = /^s*([A-Fa-f0-9]{2}-){5}[A-Fa-f0-9]{2}s*$/ ;
   	var patternMulti = /^s*[0-9A-Fa-f]{1}[13579bdfBDF]{1}(-[A-Fa-f0-9]{2}){5}s*$/ ;
   	var patternBroadcast = /^s*(((FF)"(ff))-){5}((FF)|(ff)){1}s*$/ ;
   	var patternZero = /^s*(((00)|(00))-){5}((00)|(00)){1}s*$/ ;
   	var flag;


   	if (macStr.length > 0)
   	{
   		flag = pattern.test(macStr);
   		if (!flag) {
   			alert("您输入的MAC地址非法,请重新输入！");
   			obj.focus();
   			obj.value = "";
   			return;

   		}
   		flag = patternBroadcast.test(macStr);
   		if (flag) {
   			alert("您输入的MAC地址非法（广播MAC地址），请重新输入！");
   			obj.focus();
   			obj.value = "";
   			return;
   		}
   		flag = patternMulti.test(macStr);
   		if (flag) {
   			alert("您输入的MAC地址非法（多播MAC地址），请重新输入！");
   			obj.focus();
   			obj.value = "";
   			return;
   		}
   		flag = patternZero.test(macStr);
//   		if (flag) {
//   			alert("您输入的MAC地址非法（全零MAC），请重新输入！");
//   			obj.focus();
//   			obj.value = "";
//   			return;
//   		}
   	}
   }
    </script>

</head>
<body style="width: 95%">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head"  >
                <div id="location">
                    <div id="current" runat="server" >
                        设置MAC地址</div>
                </div>
            </div>
            <div>
                <div style="text-align: right;">
                    <asp:Button ID="btnRef" runat="server" CssClass="button" Text="更新机位" OnClick="btnRef_Click" />
                    <asp:Button ID="btnSave" runat="server" CssClass="button" Text="保  存" OnClientClick="return Save()"
                        OnClick="btnSave_Click" Enabled="false" />
                </div>
                <div id="left" style="text-align: center; margin-top: 10px;"  >
                    <yyc:SmartGridView ID="grdEntity" runat="server" Width="500px" AutoGenerateColumns="False"  AllowPaging="false"
                        OnRowDataBoundDataRow="grdEntity_RowDataBoundDataRow" OnRowCreated="grdEntity_RowCreated" >
                        <Columns>
                            <asp:TemplateField HeaderText="机位">
                                <itemtemplate>
                                    <asp:Label runat="server" ID="lblNum" Text='<%# RowIndex+1%>'></asp:Label>
                               </itemtemplate>
                                <headerstyle width="100px" />
                                <itemstyle width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MAC地址">
                                <itemtemplate>
                                      <input type="text" id="txtMAC"  runat="server" style="width: 90%" onblur="checkMAC(this)"   />
                                </itemtemplate>
                                <headerstyle width="300px" />
                                <itemstyle width="300px" />
                            </asp:TemplateField>
                        </Columns>
                    </yyc:SmartGridView>
                </div>
            </div>
        </div>
         
    </form>
</body>
</html>
