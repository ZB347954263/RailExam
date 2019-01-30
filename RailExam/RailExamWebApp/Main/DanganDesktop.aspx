<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DanganDesktop.aspx.cs" 
    Inherits="RailExamWebApp.Main.DanganDesktop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
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

        function AssignAccount(id)
        {
           var flagUpdate=document.getElementById("HfUpdateRight").value; 
	        if(flagUpdate=="False")
             {
                alert("您没有权限使用该操作！");
                return;
             } 
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-500)*.5;   
            ctop=(screen.availHeight-240)*.5;  
             
            var re = window.open("../Systems/AssignAccount.aspx?id="+id,'AssignAccount','Width=500px; Height=240px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
	        re.focus();
        }
        
        function addEmployee() {
            var selectOrgId = document.getElementById("hfSelectOrg").value;
        	if(selectOrgId == "") {
        		alert("请选择一个车间或班组！");
        		return;
            }
        }
        
        function showEmployee(id) {
        	var ret = window.showModalDialog('../RandomExamTai/EmployeeArchives.aspx?ID='+id,"EmployeeArchives","status:false;dialogWidth:800px;dialogHeight:650px");
    	    if(ret != "") {
    		    form1.Refresh.value = ret;
    		    form1.submit();
    		    form1.Refresh.value = "";
    	    }

        }
        
       function Update(id)
       {
          if(! confirm("您确定要为该员工初始化密码吗？"))
          {
            return;
         }
        
          form1.UpdatePsw.value = id;
		  form1.submit();
		  form1.UpdatePsw.value = "";
      }
      
      function Validate()
      {
        var age1=document.getElementById("txtAge1").value;
        var age2=document.getElementById("txtAge2").value;
        if(IsNaN(age1)||IsNaN(age2))
        {
            alert("年龄必须为数字");
            return false;
        }
      }
      
      function imgSelectPost_Click()
      {
        var selectedPost = window.showModalDialog('/RailExamBao/Common/SelectPost.aspx', 
            '', 'help:no; status:no; dialogWidth:320px;dialogHeight:620px;scroll:no;');
        if(!selectedPost)
        {
            return;
        }

        var id=selectedPost.split('|')[0];
        var name=selectedPost.split('|')[1];
        document.getElementById("txtPostName").value=name;
        document.getElementById("hfPostID").value=id;        
      }
      
     function imgSelectNowPost_Click()
      {
        var selectedPost = window.showModalDialog('/RailExamBao/Common/SelectPost.aspx', 
            '', 'help:no; status:no; dialogWidth:320px;dialogHeight:620px;scroll:no;');
        if(!selectedPost)
        {
            return;
        }

        var id=selectedPost.split('|')[0];
        var name=selectedPost.split('|')[1];
        document.getElementById("txtNowPostName").value=name;
        document.getElementById("hfNowPostID").value=id;        
      } 
      
     
        function exportEmployee()
        {
           var flagUpdate=document.getElementById("HfUpdateRight").value; 
           if(flagUpdate=="False")
            {
               alert("您没有权限使用该操作！");
               return;
           }
        	document.getElementById("btnExcel").click();
            var ret = window.showModalDialog("/RailExamBao/RandomExamTai/ExportTemplate.aspx?Type=employee&Source=Dangan&math="+Math.random(),'','help:no;status:no;dialogWidth:320px;dialogHeight:30px;');
            if(ret != "")
            {
                 document.getElementById("hfRefreshExcel").value = "true";
	      		 document.getElementById("btnExcel").click();
	      		 document.getElementById("hfRefreshExcel").value = "";
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
                        <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                        </div>
                        <div id="parent">
                            电子档案</div>
                        <div id="separator">
                        </div>
                        <div id="current">
                            档案</div>
                    </div>
                    <div id="welcomeInfo">
                        <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                    </div>
                    <div id="button">
                        <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="查 询" OnClick="btnQuery_Click"
                            OnClientClick="return Validate()" />
                        <input type="button" value="导出Excel" class="button" onclick="exportEmployee();"/> 
                         <asp:Button ID="btnExcel" runat="server" Text="" OnClick="btnExcel_Click" Style="display: none;" />
                    </div>
                </div>
            </div>
            <div id="query">
                <table class="contentTable" style="width: 100%; text-align: left">
                    <tr>
                        <td class="contentTd" style="width: 7%">
                            站 段
                        </td>
                        <td class="contentTd" style="width: 18%">
                            <asp:DropDownList ID="dropStation" runat="server" AutoPostBack="True" DataTextField="short_name"
                                DataValueField="org_id" OnSelectedIndexChanged="dropStation_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="contentTd" style="width: 7%">
                            车 间
                        </td>
                        <td class="contentTd" style="width: 18%">
                            <asp:DropDownList ID="dropShop" runat="server" DataTextField="short_name" DataValueField="org_id">
                            </asp:DropDownList>
                        </td>
                        <td class="contentTd" style="width: 7%">
                            姓 名
                        </td>
                        <td class="contentTd" style="width: 18%">
                            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        </td>
                        <td class="contentTd" style="width: 7%">
                            拼音码
                        </td>
                        <td class="contentTd" style="width: 18%">
                            <asp:TextBox ID="txtPinYin" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentTd">
                            性 别
                        </td>
                        <td class="contentTd">
                            <asp:DropDownList ID="ddlSex" runat="server">
                                <asp:ListItem Value="" Text="-请选择-"></asp:ListItem>
                                <asp:ListItem Value="男" Text="男"></asp:ListItem>
                                <asp:ListItem Value="女" Text="女"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="contentTd">
                            年 龄
                        </td>
                        <td class="contentTd">
                            <asp:TextBox ID="txtAge1" runat="server" Width="40px"></asp:TextBox>&nbsp; 到<asp:TextBox
                                ID="txtAge2" runat="server" Width="40px"></asp:TextBox>
                        </td>
                        <td class="contentTd">
                            身份证号
                        </td>
                        <td class="contentTd">
                            <asp:TextBox ID="txtIDCard" runat="server"></asp:TextBox>
                        </td>
                        <td class="contentTd">
                            岗位合格证
                        </td>
                        <td class="contentTd">
                            <asp:TextBox ID="txtWorkNo" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentTd">
                            职 名
                        </td>
                        <td class="contentTd">
                            <asp:TextBox ID="txtPostName" runat="server" Width="100px"></asp:TextBox>
                            <input type="image" id="imgSelectPost" src="../Common/Image/search.gif" alt="选择职名"
                                onclick="imgSelectPost_Click()" />
                        </td>
                          <td class="contentTd">
                              现职名
                        </td>
                        <td class="contentTd">
                            <asp:TextBox ID="txtNowPostName" runat="server" Width="100px"></asp:TextBox>
                            <input type="image" id="Image1" src="../Common/Image/search.gif" alt="选择职名"
                                onclick="imgSelectNowPost_Click()" />
                        </td>
                        <td class="contentTd">
                            是否在岗
                        </td>
                        <td class="contentTd">
                            <asp:DropDownList ID="ddlStatus" runat="server">
                                <asp:ListItem Value="1" Selected="True">在岗</asp:ListItem>
                                <asp:ListItem Value="0">未在岗</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="contentTd">
                            政治面貌
                        </td>
                        <td class="contentTd">
                            <asp:DropDownList ID="dropPolitical" runat="server" DataTextField="political_status_name"
                                DataValueField="political_status_id">
                            </asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td class="contentTd">
                            班组长
                        </td>
                        <td class="contentTd" colspan="3">
                            <asp:CheckBoxList ID="chkListBZZ" runat="server" BorderStyle="None" DataTextField="level_name"
                                DataValueField="workgroupleader_level_id" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </td>
                        <td style="width: 10%;">
                            证书名称</td>
                        <td style="width: 39%;">
                            <asp:DropDownList ID="drop_certificate" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drop_certificate_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 13%;">
                            证书级别
                        </td>
                        <td style="width: 41%;">
                            <asp:DropDownList ID="drop_certificate_Level" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentTd">
                            技术职称
                        </td>
                        <td class="contentTd" colspan="3">
                            <asp:CheckBoxList ID="chkListJSZC" runat="server" BorderStyle="None" DataTextField="type_name"
                                DataValueField="technician_title_type_id" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </td>
                        <td style="width: 13%;">
                            其他证书复审查状态
                        </td>
                        <td style="width: 41%;">
                            <asp:DropDownList ID="ddlCheck" runat="server">
                                <asp:ListItem Value="0">全部</asp:ListItem>
                                <asp:ListItem Value="1">已复审</asp:ListItem>
                                <asp:ListItem Value="2">未复审</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td style="width: 13%;">
                            其他证书超过有效期状态
                        </td>
                        <td style="width: 41%;">
                            <asp:DropDownList ID="ddlDate" runat="server">
                                <asp:ListItem Value="0">全部</asp:ListItem>
                                <asp:ListItem Value="1">未超过</asp:ListItem>
                                <asp:ListItem Value="2">已超过</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentTd">
                            技能等级
                        </td>
                        <td class="contentTd" colspan="7">
                            <asp:CheckBoxList ID="chkListJNDJ" runat="server" BorderStyle="None" DataTextField="type_name"
                                DataValueField="technician_type_id" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td class="contentTd">
                            学 历
                        </td>
                        <td class="contentTd" colspan="7">
                            <asp:CheckBoxList ID="chkListEduBg" runat="server" BorderStyle="None" DataTextField="education_level_name"
                                DataValueField="education_level_id" RepeatDirection="Horizontal">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="left" style="width: 100%">
                <div id="leftContent">
                    <yyc:SmartGridView ID="grdEntity" runat="server" DataKeyNames="EMPLOYEE_ID" PageSize="9"
                        AllowSorting="True" OnRowCreated="grdEntity_RowCreated" DataSourceID="ObjectDataSource1">
                        <Columns>
                            <asp:BoundField DataField="EMPLOYEE_ID" Visible="False" />
                            <asp:BoundField DataField="EMPLOYEE_NAME" HeaderText="员工姓名" SortExpression="EMPLOYEE_NAME">
                                <headerstyle wrap="False"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="SEX" HeaderText="性别" SortExpression="SEX">
                                <headerstyle wrap="False"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Post_Name" HeaderText="职务" SortExpression="Post_Name">
                                <headerstyle wrap="False"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="是否在岗" DataField="IsOnPostName">
                                <headerstyle wrap="False"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MEMO" HeaderText="备注" SortExpression="Memo">
                                <headerstyle wrap="False"></headerstyle>
                                <itemstyle wrap="False"></itemstyle>
                            </asp:BoundField>
                            <asp:TemplateField>
                                <headertemplate>
                                    操作
                                </headertemplate>
                                <itemtemplate>
                                    <a class="underline" onclick='showEmployee(<%#Eval("EMPLOYEE_ID")%>)' href="#"><b>档案</b></a> 
                                    <a style="DISPLAY: none" class="underline" href='EmployeeInfoDetail.aspx?ID=<%#Eval("EMPLOYEE_ID")%>&Type=0'>查看</a> 
                                    <a onclick='AssignAccount(<%#Eval("EMPLOYEE_ID")%>)' href="#"><b>登录帐户</b></a> 
                                    <a onclick='Update(<%#Eval("EMPLOYEE_ID")%>)' href="#"><b>初始化密码</b></a> 
                                </itemtemplate>
                                <headerstyle width="20%" horizontalalign="Center" wrap="False" />
                                <itemstyle width="20%" horizontalalign="Center" wrap="False" />
                            </asp:TemplateField>
                        </Columns>
                    </yyc:SmartGridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="OjbData"
                        OnSelected="ObjectDataSource1_Selected">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hfSelect" Type="String" PropertyName="Value" Name="sql" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:HiddenField ID="hfSelect" runat="server" />
                </div>
            </div>
        </div>
        <input type="hidden" name="Refresh" />
        <asp:HiddenField runat="server" ID="hfSelectOrg" />
        <asp:HiddenField runat="server" ID="hfLevelNum" />
        <input type="hidden" name="UpdatePsw" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="hfPostID" runat="server" />
        <asp:HiddenField ID="hfNowPostID" runat="server" />
        <asp:HiddenField ID="hfRefreshExcel" runat="server" />
    </form>
</body>
</html>
