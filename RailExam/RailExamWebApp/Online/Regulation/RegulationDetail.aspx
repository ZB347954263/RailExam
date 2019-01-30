<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="RegulationDetail.aspx.cs" Inherits="RailExamWebApp.Online.Regulation.RegulationDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>政策法规详细信息</title>
    <link href="style/css.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        <!--
        function MM_preloadImages() //v3.0
        {
            var d=document;
            if(d.images)
            {
                if(!d.MM_p) d.MM_p=new Array();
                var i,j=d.MM_p.length,a=MM_preloadImages.arguments;
                for(i=0; i<a.length; i++)
                    if(a[i].indexOf("#")!=0)
                    {
                        d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];
                    }
            }
        }

        function MM_swapImgRestore() //v3.0
        {
            var i,x,a=document.MM_sr;
            for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
        }

        function MM_findObj(n, d)   //v4.01
        {
            var p,i,x;
            if(!d) d=document;
            if((p=n.indexOf("?"))>0&&parent.frames.length)
            {
                d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);
            }
            if(!(x=d[n])&&d.all) x=d.all[n];
            for(i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
            for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
            if(!x && d.getElementById) x=d.getElementById(n); return x;
        }

        function MM_swapImage() //v3.0
        {
            var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array;
            for(i=0;i<(a.length-2);i+=3)
            if((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x;
            if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
        }
        //-->
    </script>
    <style type="text/css">
        <!--
        .style17 {
	        font-size: 14px;
	        color: #1D73CC;
	        font-weight: bold;
        }
        .style18 {
	        font-size: 14px;
	        font-weight: bold;
	        color: #FFFFFF;
        }
        .style19 {font-size: 14px}
        .style24 {font-size: 24px;font-weight:bold;}
        -->
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="1004" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td height="5" bgcolor="#3A527E">
                </td>
            </tr>
            <tr>
                <td>
                    <img src="image/banana.jpg" width="1004" height="236" /></td>
            </tr>
        </table>
        <table width="1004" height="45" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" bgcolor="#1082E9" class="style16">
                    <a href="#" target="_blank" class="white16">首&nbsp;&nbsp;页</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="BulletinList.aspx" target="_blank" class="white16">信息公告</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="NoticeList.aspx" target="_blank" class="white16">通&nbsp;&nbsp;知</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="RegulationList.aspx" target="_blank" class="white16">政策法规</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="#" target="_blank" class="white16">帐户管理</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="#" target="_blank" class="white16">在线学习</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="#" target="_blank" class="white16">在线考试</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="http://localhost/StudyBBS" target="_blank" class="white16">学习论坛</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="../Main/Main.aspx" target="_blank" class="white16">后台管理</a></td>
            </tr>
        </table>
        <table width="1004" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="6">
                </td>
            </tr>
        </table>
        <table width="1004" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="210" valign="top">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="12%" bgcolor="#1082E9" ><img src="image/dot02.jpg" width="26" height="21" /></td>
                            <td width="62%" valign="baseline" bgcolor="#1082E9" class="style18">政策法规</td>
                            <td width="26%" align="center" valign="baseline" bgcolor="#1082E9">
                            <a href="RegulationList.aspx" target="_blank" class="white12">更多&gt;&gt;</a></td>
                        </tr>
                        <tr>
                            <td height="325" colspan="3" align="center">
                                <table width="180" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="130" align="center"><img src="image/pic.gif" width="163" height="106" /></td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top">
                                            <div>
                                                <asp:ObjectDataSource ID="odsRegulation" runat="server" SelectMethod="GetRegulations"
                                                    TypeName="RailExam.BLL.RegulationBLL">
                                                </asp:ObjectDataSource>
                                                <asp:GridView ID="GridView1" runat="server" Width="100%" DataKeyNames="RegulationID" AutoGenerateColumns="False"
                                                    DataSourceID="odsRegulation" ShowHeader="false" BorderWidth="0" AlternatingRowStyle-Wrap="False" 
                                                    AlternatingRowStyle-Width="30%" >
                                                    <Columns>
                                                        <asp:HyperLinkField DataTextField="RegulationName" DataTextFormatString="{0}" DataNavigateUrlFields="RegulationID"
                                                            DataNavigateUrlFormatString="RegulationDetail.aspx?id={0}" Target="_blank" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="right" valign="top">
                    <table width="99%" border="0" cellpadding="0" cellspacing="0" class="kuang">
                        <tr>
                            <td height="28" background="image/hui.gif">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="34">
                                            <img src="image/d0t01.jpg" width="34" height="28" /></td>
                                        <td align="left" class="style17">
                                            当前位置：首页 &gt; 信息公告 &gt; <%=ViewState["RegulationName"].ToString() %>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <table width="94%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td height="75" align="center">
                                            <table width="90%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td height="35" align="center" class="style24"><%=ViewState["RegulationName"].ToString()%></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="1" bgcolor="#C0C0C0"></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table width="95%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td height="15" align="left"></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" class="size14">
                                                        <div>
                                                            <asp:FormView ID="FormView1" runat="server" BorderStyle="Solid" GridLines="Both"
                                                                HorizontalAlign="Left" DataSourceID="odsRegulationDetail" Width="600px" DataKeyNames="RegulationID" >
                                                                <ItemTemplate>
                                                                    <table class="contentTable">
                                                                        <tr>
                                                                            <td style="width: 15%">法规分类名称</td>
                                                                            <td style="width: 35%">
                                                                                <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                                                                            </td>
                                                                            <td style="width: 15%">法规编号</td>
                                                                            <td style="width: 35%">
                                                                                <asp:Label ID="lblRegulationNo" runat="server" Text='<%# Eval("RegulationNo") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>法规名称</td>
                                                                            <td><asp:Label ID="lblRegulationName" runat="server" Text='<%# Eval("RegulationName") %>'></asp:Label></td>
                                                                            <td>版本</td>
                                                                            <td><asp:Label ID="lblVersion" runat="server" Text='<%# Eval("Version") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>文号</td>
                                                                            <td><asp:Label ID="lblFileNo" runat="server" Text='<%# Eval("FileNo") %>'></asp:Label></td>
                                                                            <td>题注</td>
                                                                            <td><asp:Label ID="lblTitleRemark" runat="server" Text='<%# Eval("TitleRemark") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>颁布日期</td>
                                                                            <td><asp:Label ID="lblIssueDate" runat="server" Text='<%# Eval("IssueDate") %>'></asp:Label></td>
                                                                            <td>实施日期</td>
                                                                            <td><asp:Label ID="lblExecuteDate" runat="server" Text='<%# Eval("ExecuteDate") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>状态</td>
                                                                            <td><asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status").ToString() == "1" ? "有效" : "失效" %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>网址</td>
                                                                            <td colspan="3">
                                                                                <asp:Label ID="lblUrl" runat="server" Width="430px" Text='<%# Eval("Url") %>'></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>备注</td>
                                                                            <td colspan="3">
                                                                                <asp:Label ID="lblMemo" runat="server" Width="430px" Text='<%# Eval("Memo") %>'></asp:Label></td>
                                                                        </tr>
                                                                    </table>
                                                                </ItemTemplate>
                                                            </asp:FormView>
                                                            <asp:ObjectDataSource ID="odsRegulationDetail" runat="server" SelectMethod="GetRegulation"
                                                                TypeName="RailExam.BLL.RegulationBLL" EnableViewState="False">
                                                                <SelectParameters>
                                                                    <asp:QueryStringParameter Name="regulationID" QueryStringField="id" Type="Int32" />
                                                                </SelectParameters>
                                                            </asp:ObjectDataSource>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table width="100%"  border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="8"></td>
            </tr>
            <tr>
                <td height="8" bgcolor="#E4E4E4"></td>
            </tr>
            <tr>
                <td height="50" align="center">Powered by：MyPower Ver1.00 站长：liyuan 页面执行时间：625.00毫秒<br />
                    版权所有 Copyright &copy 2006 武汉铁路局在线培训考试系统</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
