<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StudySelectedDetail.aspx.cs"
    Inherits="RailExamWebApp.Online.Study.StudySelectedDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
    <link href="../../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../../Common/JS/Common.js"></script>

    <script language="javascript" type="text/javascript">
      	function OpenIndex(id)
	{
	    var re = window.open('../Book/' + id + '/index.html','index','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
	    re.focus();
	}
	
    function GetExercise(id)
    { 
//       var re= window.open("/RailExamBao/Exercise/ExerciseManage.aspx?id="+id,"ExerciseManage",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=no,status=no,location=no');		
//       re.focus(); 
    	
    	var width = (window.screen.width - 10);
    	var height = (window.screen.width - 65);
    	var ret = window.showModalDialog("/RailExamBao/Exercise/ExerciseManage.aspx?id="+id, 
                'ExerciseManage', 'help:no; status:no; dialogWidth:'+width+'px;dialogHeight:'+height+'px;scroll:no;'); 
    }
   
   function OpenCourse(id)
	{
	    var re = window.open('/RailExamBao/Courseware/ViewCourseware.aspx?id=' + id,'ViewCourseware','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no');
	    re.focus();
	} 
    


        /*第一种形式 第二种形式 更换显示样式*/
        function setTab(name,cursel,n) {
	        for (i = 1; i <= n; i++) {
		        var menu = document.getElementById(name + i);
		        var con = document.getElementById("con_" + name + "_" + i);
		        menu.className = i == cursel ? "hover" : "";
		        con.style.display = i == cursel ? "block" : "none";
	        }
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        选择学习</div>
                </div>
            </div>
            <div id="content">
                <div style="text-align: left;">
                    <ComponentArt:TabStrip ID="TabStrip1" runat="server" MultiPageId="EmployeeDetailMultiPage">
                        <Tabs>
                            <ComponentArt:TabStripTab Value="0" Text="教 材" PageViewId="FirstPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="1" Text="课 件" PageViewId="SecondPage">
                            </ComponentArt:TabStripTab>
                        </Tabs>
                    </ComponentArt:TabStrip>
                    <ComponentArt:MultiPage ID="EmployeeDetailMultiPage" Width="100%" runat="server"
                        RunningMode="Callback">
                        <ComponentArt:PageView ID="FirstPage">
                            <!--教材-->
                            <div style="text-align: center; height: 260px;">
                                <ComponentArt:Grid ID="Grid1" runat="server" PageSize="10" Width="98%">
                                    <levels>
                                        <ComponentArt:GridLevel DataKeyField="bookId">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="bookId" HeadingText="ID" Visible="false" />
                                                <ComponentArt:GridColumn DataField="bookName" HeadingText="教材名称" Align="Left" />
                                                <ComponentArt:GridColumn DataField="knowledgeId" HeadingText="knowledgeId" Visible="false" />
                                                <ComponentArt:GridColumn DataField="trainTypeId" HeadingText="trainTypeId" Visible="false" />
                                                <ComponentArt:GridColumn DataField="bookNo" HeadingText="教材编号" />
                                                <ComponentArt:GridColumn DataField="publishOrgName" HeadingText="编制单位" />
                                                <ComponentArt:GridColumn DataCellClientTemplateId="ct1" HeadingText="操作" AllowSorting="False" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </levels>
                                    <clienttemplates>
                                        <ComponentArt:ClientTemplate ID="ct1">
                                            <a onclick="GetExercise(##DataItem.getMember('bookId').get_value()## )" href="#"><b>
                                                练习</b></a>
                                        </ComponentArt:ClientTemplate>
                                    </clienttemplates>
                                </ComponentArt:Grid>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="SecondPage">
                            <!--课件-->
                            <div style="text-align: center; height: 240px;">
                                <ComponentArt:Grid ID="Grid2" runat="server" PageSize="10" Width="98%">
                                    <levels>
                                        <ComponentArt:GridLevel DataKeyField="CoursewareID">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="CoursewareID" Visible="false" />
                                                <ComponentArt:GridColumn DataField="CoursewareName" HeadingText="课件名称" Align="Left" />
                                                <ComponentArt:GridColumn DataField="CoursewareTypeID" Visible="false" />
                                                <ComponentArt:GridColumn DataField="TrainTypeID" Visible="false" />
                                                <ComponentArt:GridColumn DataField="ProvideOrgName" HeadingText="资料提供单位" />
                                                <ComponentArt:GridColumn DataField="PublishDate" FormatString="yyyy-MM-dd" HeadingText="完成日期" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </levels>
                                </ComponentArt:Grid>
                            </div>
                        </ComponentArt:PageView>
                    </ComponentArt:MultiPage>
                </div>
            </div>
        </div>
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>
</body>
</html>
