<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeArchives.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.EmployeeArchives" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>职工档案</title>
</head>
<body>
<form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div style="text-align: left;">
                    <ComponentArt:TabStrip ID="TabStrip1" runat="server" MultiPageId="EmployeeDetailMultiPage">
                        <Tabs>
                            <ComponentArt:TabStripTab Value="0" Text="员工基本信息" PageViewId="FirstPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="1" Text="工作动态" PageViewId="SecondPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="2" Text="学历动态" PageViewId="ThirdPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="3" Text="技能动态" PageViewId="FourthPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="4" Text="技能竞赛情况" PageViewId="FifthPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="5" Text="奖惩情况" PageViewId="SixthPage">
                            </ComponentArt:TabStripTab>
                             <ComponentArt:TabStripTab Value="8" Text="其他资格性证书" PageViewId="ninePage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="6" Text="职工参加培训情况" PageViewId="SeventhPage">
                            </ComponentArt:TabStripTab>
                            <ComponentArt:TabStripTab Value="7" Text="职工参加考试情况" PageViewId="EighthPage">
                            </ComponentArt:TabStripTab>
                        </Tabs>
                    </ComponentArt:TabStrip>
                    <ComponentArt:MultiPage ID="EmployeeDetailMultiPage" Width="100%" runat="server" RunningMode="Callback">
                        <ComponentArt:PageView ID="FirstPage">
                            <iframe id="iframeFirst" style="width: 100%;" height="550px" frameborder="0" src="">
                            </iframe>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="SecondPage">
                            <iframe id="iframeSecond" style="width: 100%;" height="550px"  frameborder="0" src="">
                            </iframe>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="ThirdPage">
                            <iframe id="iframethird" style="width: 100%;" height="550px"  frameborder="0" src="">
                            </iframe>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="FourthPage">
                            <iframe id="iframeFourth" style="width: 100%;" height="550px"  frameborder="0" src="">
                            </iframe>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="FifthPage">
                            <iframe id="iframeFifth" style="width: 100%;" height="550px"  frameborder="0" src="">
                            </iframe>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="SixthPage">
                            <iframe id="iframeSixth" style="width: 100%;" height="550px"  frameborder="0" src="">
                            </iframe>
                        </ComponentArt:PageView>
                           <ComponentArt:PageView ID="ninePage">
                            <iframe id="iframenine" style="width: 100%;" height="550px"  frameborder="0" src="">
                            </iframe>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="SeventhPage">
                            <iframe id="iframeSeventh" style="width: 100%;" height="550px"  frameborder="0" src="">
                            </iframe>
                        </ComponentArt:PageView>
                        <ComponentArt:PageView ID="EighthPage">
                            <iframe id="iframeEighth" style="width: 100%;" height="550px"  frameborder="0" src="">
                            </iframe>
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
    <script type="text/javascript">
        var search = window.location.search;
        window.frames["iframeFirst"].location = "EmployeeInfoDetail.aspx"+search+"&style=open";
        window.frames["iframeSecond"].location = "EmployeeWork.aspx"+search;
        window.frames["iframethird"].location = "EmployeeEducation.aspx"+search;
        window.frames["iframeFourth"].location = "EmployeeSkill.aspx"+search;
        window.frames["iframeFifth"].location = "EmployeeMatch.aspx"+search;
        window.frames["iframeSixth"].location = "EmployeePrize.aspx"+search;
        window.frames["iframeSeventh"].location = "EmployeeTrain.aspx"+search;
         window.frames["iframeEighth"].location = "EmployeeExam.aspx"+search;
         window.frames["iframenine"].location = "EmployeeCertificate.aspx"+search;
    </script>
</body>
</html>
