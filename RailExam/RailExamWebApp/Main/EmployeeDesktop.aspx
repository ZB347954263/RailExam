<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeeDesktop.aspx.cs"
    Inherits="RailExamWebApp.Main.EmployeeDesktop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
    function showInfo(classID) 
    {
    	window.showModalDialog("FocalPointStudyInfo.aspx?classID=" + classID, "", "help:no; status:no; dialogWidth:600px;scroll:no;");
    }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/EmployeeDesktop.aspx'">
                    </div>
                    <div id="current">
                        桌面</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button" style="color: #023895">
                    您的IP地址为：<asp:Label ID="lblIP" runat="server" />
                </div>
            </div>
            <div id="content">
                当前系统版本号为：<asp:Label ID="lblVersion" runat="server"></asp:Label>
                <br />
                <div style="text-align: left;">
                    <ComponentArt:TabStrip ID="TabStrip1" runat="server" MultiPageId="desktopPage">
                        <Tabs>
                            <ComponentArt:TabStripTab Value="0" Text="正在参加的培训" PageViewId="FirstPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="1" Text="计划参加的培训" PageViewId="SecondPage">
                            </ComponentArt:TabStripTab>
                        </Tabs>
                    </ComponentArt:TabStrip>
                    <ComponentArt:MultiPage ID="desktopPage" Width="100%" runat="server" RunningMode="Callback">
                        <ComponentArt:PageView ID="FirstPage">
                            <div style="text-align: center; height: 220px;">
                                <ComponentArt:Grid ID="grid1" runat="server" AutoAdjustPageSize="false" PageSize="6">
                                    <levels>
                                            <ComponentArt:GridLevel>
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="trainplan_type_name" HeadingText="培训类别" />
                                                    <ComponentArt:GridColumn DataField="trainplan_project_name" HeadingText="培训项目" />
                                                    <ComponentArt:GridColumn DataField="train_plan_name" HeadingText="计划名" />
                                                    <ComponentArt:GridColumn DataField="sponsor_unit" HeadingText="主办单位" />
                                                    <ComponentArt:GridColumn DataField="undertake_unit" HeadingText="承办单位" />
                                                    <ComponentArt:GridColumn DataField="train_class_name1" HeadingText="培训班" />
                                                    <ComponentArt:GridColumn DataField="begin_date" HeadingText="开始时间" FormatString="yyyy-MM-dd" />
                                                    <ComponentArt:GridColumn DataField="end_date" HeadingText="结束时间" FormatString="yyyy-MM-dd"  />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                       </levels>
                                </ComponentArt:Grid>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="SecondPage">
                            <div style="text-align: center; height: 220px;">
                                <ComponentArt:Grid ID="grid2" runat="server" AutoAdjustPageSize="false" PageSize="5">
                                    <levels>
                                        <ComponentArt:GridLevel>
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="trainplan_type_name" HeadingText="培训类别" />
                                                <ComponentArt:GridColumn DataField="trainplan_project_name" HeadingText="培训项目" />
                                                <ComponentArt:GridColumn DataField="train_plan_name" HeadingText="计划名" />
                                                <ComponentArt:GridColumn DataField="sponsor_unit" HeadingText="主办单位" />
                                                <ComponentArt:GridColumn DataField="undertake_unit" HeadingText="承办单位" />
                                                <ComponentArt:GridColumn DataField="class_name" HeadingText="培训班" />
                                                <ComponentArt:GridColumn DataField="begin_date" HeadingText="开始时间" FormatString="yyyy-MM-dd" />
                                                <ComponentArt:GridColumn DataField="end_date" HeadingText="结束时间" FormatString="yyyy-MM-dd" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </levels>
                                </ComponentArt:Grid>
                            </div>
                        </ComponentArt:PageView>
                    </ComponentArt:MultiPage>
                </div>
               <div style="text-align: left;">
                    <ComponentArt:TabStrip ID="TabStrip2" runat="server" MultiPageId="MultiPage1">
                        <Tabs>
                            <ComponentArt:TabStripTab Value="0" Text="正在参加的考试" PageViewId="FirstPage1">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="1" Text="计划参加的考试" PageViewId="SecondPage1">
                            </ComponentArt:TabStripTab>
                        </Tabs>
                    </ComponentArt:TabStrip>
                    <ComponentArt:MultiPage ID="MultiPage1" Width="100%" runat="server" RunningMode="Callback">
                        <ComponentArt:PageView ID="FirstPage1">
                            <div style="text-align: center; height: 220px;">
                                <ComponentArt:Grid ID="grid3" runat="server" AutoAdjustPageSize="false" PageSize="6">
                                    <levels>
                                            <ComponentArt:GridLevel>
                                                <Columns>
                                                    <ComponentArt:GridColumn DataField="Exam_Name" HeadingText="考试名称" />
                                                    <ComponentArt:GridColumn DataField="begin_time" HeadingText="开始时间" FormatString="yyyy-MM-dd" />
                                                    <ComponentArt:GridColumn DataField="end_time" HeadingText="结束时间" FormatString="yyyy-MM-dd"  />
                                                    <ComponentArt:GridColumn DataField="Exam_Style_Name" HeadingText="培训项目" />
                                                    <ComponentArt:GridColumn DataField="Start_Mode_Name" HeadingText="开考模式" />
                                                     <ComponentArt:GridColumn DataField="Exam_Org_Name" HeadingText="出题单位" /> 
                                                    <ComponentArt:GridColumn DataField="Exam_Address" HeadingText="考试地点" />
                                                </Columns>
                                            </ComponentArt:GridLevel>
                                       </levels>
                                </ComponentArt:Grid>
                            </div>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="SecondPage1">
                            <div style="text-align: center; height: 220px;">
                                <ComponentArt:Grid ID="grid4" runat="server" AutoAdjustPageSize="false" PageSize="5">
                                    <levels>
                                        <ComponentArt:GridLevel>
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="Exam_Name" HeadingText="考试名称" />
                                                <ComponentArt:GridColumn DataField="begin_time" HeadingText="开始时间" FormatString="yyyy-MM-dd" />
                                                <ComponentArt:GridColumn DataField="end_time" HeadingText="结束时间" FormatString="yyyy-MM-dd"  />
                                                <ComponentArt:GridColumn DataField="Exam_Style_Name" HeadingText="培训项目" />
                                                <ComponentArt:GridColumn DataField="Start_Mode_Name" HeadingText="开考模式" />
                                                <ComponentArt:GridColumn DataField="Exam_Org_Name" HeadingText="出题单位" />
                                                <ComponentArt:GridColumn DataField="Exam_Address" HeadingText="考试地点" />
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
    </form>
</body>
</html>
