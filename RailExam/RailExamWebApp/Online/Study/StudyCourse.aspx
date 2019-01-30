<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="StudyCourse.aspx.cs" Inherits="RailExamWebApp.Online.Study.StudyCourse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript">
        function OpenStudyDetail()
        {
            var re = window.open("StudyCourseDetail.aspx","StudyCourseDetail"," Width=600px; Height=600px,status=false,resizable=no,scrollbars=yes",true);		
            re.focus();
        }
        
        function SelectTrainType()
        {
            //var re = window.open("Train/TrainTypeEmployeeSelect.aspx",'ShowEditor','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;           
            var re = window.open("../../Train/TrainTypeEmployeeSelect.aspx","TrainTypeEmployeeSelect",' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);		
            re.focus();
        }
        
        function StudyByKnowledge()
        {
            var   cleft;   
            var   ctop;   
           cleft=(screen.availWidth-800)*.5;   
           ctop=(screen.availHeight-600)*.5;   
            var re = window.open("StudyByKnowledge.aspx","StudyByKnowledge",' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);		
            re.focus();
        }

        function GetExercise(id)
        {
            var   cleft;   
            var   ctop;   
           cleft=(screen.availWidth-800)*.5;   
           ctop=(screen.availHeight-600)*.5; 
           var re= window.open("../../Exercise/ExerciseManage.aspx?id="+id,"ExerciseManage",' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no,scrollbars=yes',true);		
	       re.focus(); 
        }
        
        function ShowBookListMore()
        {
           var   cleft;   
           var   ctop;   
           cleft=(screen.availWidth-800)*.5;   
           ctop=(screen.availHeight-600)*.5;           
           var re = window.open("StudyBookDetail.aspx","StudyBookDetail",' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);		
            re.focus();
        }
        
        function ShowCoursewareListMore()
        {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
            var re = window.open("StudyCoursewareDetail.aspx","StudyCoursewareDetail",' Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);		
            re.focus();
        }

        function OpenIndex(id)
		{
		    var re = window.open('../Book/' + id + '/index.html','index','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
		    re.focus();
		}
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="98%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td height="45" valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td height="8" colspan="4" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td width="20%" align="left">
                                    <img src="../image/CurrentTrainType.gif" width="106" height="21" /></td>
                                <td width="6%" align="center">
                                    <img src="../image/tb02.gif" width="14" height="7" alt="" /></td>
                                <td width="52%" align="left">
                                    <asp:Label ID="lblType" runat="server" Text="Label"></asp:Label></td>
                                <!--<td width="19%" align="left">
                                    <img src="../image/xxfxmb.gif" alt="" onclick="OpenStudyDetail()" style="cursor: hand;"
                                        width="87" height="23" /></td>-->
                                <td align="left"></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="150" valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="100%" height="35" align="left">
                                    <img src="../image/BookList.gif" border="0" width="541" height="34" usemap="#MapBookMore" /></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:GridView ID="gvBook" Width="550px" runat="server" ShowHeader="false" BorderWidth="0"
                                        AutoGenerateColumns="False" DataKeyNames="bookId" BorderColor="ActiveBorder"
                                        HeaderStyle-BackColor="ActiveBorder" CellPadding="3">
                                        <Columns>
                                            <asp:TemplateField HeaderText="教材名称">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" Width="20%" />
                                                <ItemTemplate>
                                                    <img src="../image/tb01.gif" alt="" />
                                                    <asp:Label ID="lbl" runat="server" Text='<%# Bind("bookName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="教材编号">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBookNo" runat="server" Text='<%# Bind("bookNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="编制单位">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrg" runat="server" Text='<%# Bind("publishOrgName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="操作">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="10%" />
                                                <ItemTemplate>
                                                    <a onclick='GetExercise(<%# Eval("bookId") %>)' href="#">练习</a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td height="150" valign="top">
                        <table>
                            <tr>
                                <td width="100%" align="left">
                                    <img src="../image/CoursewareList.gif" border="0" width="541" height="34" usemap="#MapCoursewareMore" /></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:GridView ID="gvCourse" runat="server" Width="550px" ShowHeader="false" BorderWidth="0"
                                        AutoGenerateColumns="False" DataKeyNames="coursewareId" BorderColor="ActiveBorder"
                                        HeaderStyle-BackColor="ActiveBorder" CellPadding="3">
                                        <Columns>
                                            <asp:TemplateField HeaderText="课件名称">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top"  Width="20%" />
                                                <ItemTemplate>
                                                    <img src="../image/tb01.gif" alt="" />
                                                    <asp:HyperLink ID="hlName" Text='<%# Bind("CoursewareName") %>' ToolTip='<%# Bind("ToolTip") %>'
                                                        runat="server" NavigateUrl='<%# Bind("Url") %>' Target="_blank"></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="编制单位">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrg" runat="server" Text='<%# Bind("ProvideOrgName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="编著者">
                                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                                <ItemStyle HorizontalAlign="Center" Wrap="False" Width="10%" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOrg" runat="server" Text='<%# Bind("Authors") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <!--
                <tr>
                    <td height="150" align="center" valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td width="50%" height="35" align="left">
                                    <img src="../image/lxlb.gif" width="252" height="34" /></td>
                                <td width="50%" align="left">
                                    <img src="../image/zylb.gif" width="252" height="34" /></td>
                            </tr>
                            <tr>
                                <td height="115" align="center" valign="top">
                                    <asp:GridView ID="gvExercise" Width="270px"  runat="server" ShowHeader="false"
                                        BorderWidth="0" AutoGenerateColumns="False" DataKeyNames="PaperID" BorderColor="ActiveBorder"
                                        HeaderStyle-BackColor="ActiveBorder" CellPadding="3">
                                        <Columns>
                                            <asp:TemplateField HeaderText="练习名称">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left"  VerticalAlign="Top"/>
                                                <ItemTemplate>
                                                    <img src="../image/tb01.gif" alt="" />
                                                    <asp:Label ID="lblName" Text='<%# Bind("Memo") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    
                                </td>
                                <td align="center"  valign="top">
                                    <asp:GridView ID="gvTask" runat="server"  Width="280px" ShowHeader="false"
                                        BorderWidth="0" AutoGenerateColumns="False" DataKeyNames="PaperID" BorderColor="ActiveBorder"
                                        HeaderStyle-BackColor="ActiveBorder" CellPadding="3">
                                        <Columns>
                                            <asp:TemplateField HeaderText="作业名称">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    <img src="../image/tb01.gif" alt="" />
                                                    <asp:Label ID="lblName" Text='<%# Bind("Memo") %>' runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                -->
            </table>
        </div>
        <map id="MapBookMore">
            <area shape="rect" coords="498,12,542,28" href="#" onclick="ShowBookListMore()"/>
        </map>
        <map id="MapCoursewareMore">
            <area shape="rect" coords="496,10,538,27" href="#" onclick="ShowCoursewareListMore()" />
        </map>
    </form>
</body>
</html>
