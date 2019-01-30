<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="NoticeList.aspx.cs" Inherits="RailExamWebApp.Online.Notice.NoticeList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>通知发布</title>
    <link href="../style/css.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
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
    </script>
</head>
<body onload="MM_preloadImages('../image/dqkc01.jpg','../image/kscj01.jpg')">
    <form id="form1" runat="server">
        <table width="1004" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <object codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"
                        width="1004" height="140" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000">
                        <param name="_cx" value="17595">
                        <param name="_cy" value="1720">
                        <param name="FlashVars" value="">
                        <param name="Movie" value="../image/head.swf">
                        <param name="Src" value="../image/head.swf">
                        <param name="WMode" value="Transparent">
                        <param name="Play" value="-1">
                        <param name="Loop" value="-1">
                        <param name="Quality" value="High">
                        <param name="SAlign" value="">
                        <param name="Menu" value="-1">
                        <param name="Base" value="">
                        <param name="AllowScriptAccess" value="always">
                        <param name="Scale" value="ShowAll">
                        <param name="DeviceFont" value="0">
                        <param name="EmbedMovie" value="0">
                        <param name="BGColor" value="">
                        <param name="SWRemote" value="">
                        <param name="MovieData" value="">
                        <param name="SeamlessTabbing" value="1">
                        <param name="Profile" value="0">
                        <param name="ProfileAddress" value="">
                        <param name="ProfilePort" value="0">
                        <embed src="../image/head.swf" quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer"
                            type="application/x-shockwave-flash" width="1004" height="140"> </embed>
                    </object>
                </td>
            </tr>
        </table>
        <table width="1004" height="45" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" bgcolor="#1082E9" class="style16">
                    <a href="../../index.aspx" target="_blank" class="white16">首&nbsp;&nbsp;页</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="BulletinList.aspx" target="_blank" class="white16">信息公告</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="NoticeList.aspx" target="_blank" class="white16">通&nbsp;&nbsp;知</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href='../AccountManage.aspx' target="_blank" class="white16">帐户管理</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="#" target="_blank" class="white16">在线学习</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="../ExamList.aspx" target="_blank" class="white16">在线考试</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="http://localhost/StudyBBS" target="_blank" class="white16">学习论坛</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                    <a href="../../Default.aspx" target="_blank" class="white16">后台管理</a>
                </td>
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
                            <td width="12%" bgcolor="#1082E9">
                                <img src="../image/dot02.jpg" width="26" height="21" /></td>
                            <td width="62%" align="left" valign="baseline" bgcolor="#1082E9">
                                <span class="style18">通&nbsp;&nbsp;知</span></td>
                            <td width="26%" align="center" valign="baseline" bgcolor="#1082E9">
                                <a href="NoticeList.aspx" target="_blank" class="white12">更多&gt;&gt;</a></td>
                        </tr>
                        <tr>
                            <td height="300" colspan="3" align="left" valign="top" class="kuang">
                                <div>
                                    <asp:ObjectDataSource ID="odsNotice" runat="server" SelectMethod="GetNotices" TypeName="RailExam.BLL.NoticeBLL">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="14" Name="nNum" Type="Int32" />
                                             <asp:SessionParameter ConvertEmptyStringToNull="False" DefaultValue="-1" Name="examineeId"
                        SessionField="StudentID" Type="String" />
                         <asp:SessionParameter ConvertEmptyStringToNull="False" DefaultValue="-1" Name="OrgId" SessionField="StudentOrdID" Type="String" />
                                                               
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:GridView ID="GridView1" runat="server" DataKeyNames="NoticeID" AutoGenerateColumns="False"
                                        DataSourceID="odsNotice" ShowHeader="False" PageSize="14" BorderWidth="0px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="标题">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                ·
                                                <asp:HyperLink ID="hlTitle" Text='<%# Eval("Title").ToString().Length <= 14 ? Eval("Title") : Eval("Title").ToString().Substring(0, 14)+"..." %>'
                                                    ToolTip='<%# Eval("Title") %>' runat="server" NavigateUrl='<%# "NoticeDetail.aspx?id=" + Eval("NoticeID") %>'
                                                    Target="_blank"></asp:HyperLink>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="5">
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="12%" bgcolor="#1082E9">
                                <img src="../image/dot02.jpg" width="26" height="21" /></td>
                            <td width="62%" align="left" valign="baseline" bgcolor="#1082E9">
                                <span class="style18">信息公告</span></td>
                            <td width="26%" align="center" valign="baseline" bgcolor="#1082E9">
                                <a href="BulletinList.aspx" target="_blank" class="white12">更多&gt;&gt;</a></td>
                        </tr>
                        <tr>
                            <td height="300" colspan="3" align="left" valign="top" class="kuang">
                                <div>
                                    <asp:ObjectDataSource ID="odsBulletin" runat="server" SelectMethod="GetBulletins"
                                        TypeName="RailExam.BLL.BulletinBLL">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="14" Name="nNum" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:GridView ID="GridView2" runat="server" DataKeyNames="BulletinID" AutoGenerateColumns="False"
                                        DataSourceID="odsBulletin" ShowHeader="False" PageSize="14" BorderWidth="0px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="标题">
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    ·
                                                    <asp:HyperLink ID="hlTitle" Text='<%# Eval("Title").ToString().Length <= 14 ? Eval("Title") : Eval("Title").ToString().Substring(0, 14)+"..." %>' ToolTip='<%# Eval("Title") %>'
                                                        runat="server" NavigateUrl='<%# "BulletinDetail.aspx?id=" + Eval("BulletinID") %>'
                                                        Target="_blank"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="right" valign="top">
                    <table width="99%" border="0" cellpadding="0" cellspacing="0" class="kuang">
                        <tr>
                            <td height="28" background="../image/hui.gif">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="34">
                                            <img src="../image/d0t01.jpg" width="34" height="28" /></td>
                                        <td align="left" class="style17">
                                            通知发布</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td height="620" align="center" valign="top">
                                <table width="98%" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="15" align="center">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table width="96%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td align="left" valign="top" class="size14">
                                                        <div>
                                                            <asp:ObjectDataSource ID="odsNoticeDetail" runat="server" SelectMethod="GetNotices"
                                                                TypeName="RailExam.BLL.NoticeBLL">
                                                                <SelectParameters>
                                                                    <asp:Parameter DefaultValue="-1" Name="nNum" Type="Int32" />
                                                                     <asp:SessionParameter ConvertEmptyStringToNull="False" DefaultValue="-1" Name="examineeId"
                        SessionField="StudentID" Type="String" />
                         <asp:SessionParameter ConvertEmptyStringToNull="False" DefaultValue="-1" Name="OrgId" SessionField="StudentOrdID" Type="String" />
                                                                </SelectParameters>
                                                               
                                                            </asp:ObjectDataSource>
                                                            <asp:GridView ID="GridView3" runat="server" DataKeyNames="NoticeID" Width="100%" 
                                                                AutoGenerateColumns="False" DataSourceID="odsNoticeDetail" ShowHeader="False"
                                                                AllowPaging="True" BorderWidth="0px" PageSize="27">
                                                                <Columns>
                                                                    <asp:HyperLinkField DataTextField="Title" DataTextFormatString="{0}" DataNavigateUrlFields="NoticeID"
                                                                        DataNavigateUrlFormatString="NoticeDetail.aspx?id={0}" Target="_blank" />
                                                                    <asp:BoundField DataField="EmployeeName" ReadOnly="True" />
                                                                    <asp:BoundField DataField="OrgName" ReadOnly="True" />
                                                                    <asp:BoundField DataField="CreateTime" ReadOnly="True" />
                                                                </Columns>
                                                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="首页" LastPageText="尾页" NextPageText="下一页" PreviousPageText="上一页" />
                                                            </asp:GridView>
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
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td height="8">
                </td>
            </tr>
            <tr>
                <td height="8" bgcolor="#E4E4E4">
                </td>
            </tr>
            <tr>
                <td height="50" align="center">
                    Powered by：MyPower Ver1.00 站长：liyuan 页面执行时间：625.00毫秒<br />
                    版权所有 Copyright &copy 2006 武汉铁路局在线培训考试系统</td>
            </tr>
        </table>
    </form>
</body>
</html>
