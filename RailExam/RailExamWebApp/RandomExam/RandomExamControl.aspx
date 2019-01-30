<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamControl.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamControl" %>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考试信息</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">              
        //评卷按钮点击事件处理函数
        function judgePaper(eid,orgID)
        { 
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5; 
          
            var flagUpdate=document.getElementById("HfUpdateRight").value; 
            var flagIsAdmin = document.getElementById("hfIsAdmin").value;
            var mode="Edit";
            
//            if(flagUpdate=="False")
//            {
//                  alert("您没有该操作的权限！请连接本地考试系统进行考试监控！");
//                  return;
//            }
             
            var ret = window.open('RandomExamControlDetail.aspx?RandomExamID='+eid+'&OrgID='+orgID,'RandomExamControlDetail','Width=1000px; Height=650px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
		    ret.focus();             
		}
		
        
      function judgeSeat(eid,haspaper) 
      {
      	    var flagUpdate=document.getElementById("HfUpdateRight").value; 
            var flagIsAdmin = document.getElementById("hfIsAdmin").value;
            
//            if(flagIsAdmin == "False" || flagUpdate=="False")
//            {
//                  alert("您没有该操作的权限！请连接本地考试系统进行调整机位！");
//                  return;
//            }
      	
      	    if(haspaper == "False") {
      	    	alert("还未生成试卷，不能调整机位！");
      	    	return;
      	    }
      	
      	    var ret = showCommonDialog("/RailExamBao/RandomExam/RandomExamComputerSeat.aspx?RandomExamID="+eid,'help:no;stuats:no;dialogWidth:800px;dialogHeight:600px;');
      }
        
        
        
	  function ShowProgressBar(examID)
      {
         var ret = showCommonDialog("/RailExamBao/RandomExam/ExportExcel.aspx?RandomExamID="+examID+"&Type=StudentInfo",'dialogWidth:320px;dialogHeight:30px;');
         if(ret == "true")
         {
           form1.StudentInfo.value = examID;
           form1.submit();
           form1.StudentInfo.value ="";
         }
      } 
      
      function showConfirm()
      {
            var flagUpdate=document.getElementById("HfUpdateRight").value; 
            var flagIsAdmin = document.getElementById("hfIsAdmin").value;
            
            if(flagIsAdmin == "False" || flagUpdate=="False")
            {
                  alert("您没有该操作的权限！请连接本地考试系统进行考试监控！");
                  return false;
            }
            
            return true;
      }
      
      function showApply()
      {
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-950)*.5;   
          ctop=(screen.availHeight-650)*.5; 
          
          var search = window.location.search; 
          var re= window.open("/RailExamBao/RandomExam/RandomExamApplyAll.aspx?OrgID="+document.getElementById("hfOrgID").value,
                    "RandomExamApplyDetail",'Width=950px; Height=650px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
          re.focus();
      }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'">
                    </div>
                    <div id="parent">
                        考试管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        考试监控</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <asp:Button  CssClass="buttonEnableLong" Text="回复所有考试请求" ID="btnApply" OnClientClick="return showConfirm();" OnClick="btnApply_Click" runat="server"/>
                </div>
            </div>
            <div id="content">
                <div style="text-align: center">
                    <ComponentArt:CallBack ID="searchExamCallBack" runat="server" Debug="false" PostState="true"
                        OnCallback="searchExamCallBack_Callback">
                        <Content>
                            <ComponentArt:Grid ID="examsGrid" runat="server" AutoAdjustPageSize="false" PageSize="19"
                                DataSourceID="odsExams">
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="RandomExamId">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="RandomExamId" Visible="false" />
                                            <ComponentArt:GridColumn DataField="OrgId" Visible="false" />
                                            <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" />
                                            <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="有效时间" />
                                            <ComponentArt:GridColumn DataField="BeginTime" HeadingText="开始时间" FormatString="yyyy-MM-dd HH:mm"
                                                Visible="false" />
                                            <ComponentArt:GridColumn DataField="EndTime" HeadingText="结束时间" FormatString="yyyy-MM-dd HH:mm"
                                                Visible="false" />
                                            <ComponentArt:GridColumn DataField="StationName" HeadingText="单位" />
                                            <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="制卷人" />
                                            <ComponentArt:GridColumn DataField="StartModeName" HeadingText="开考模式" />
                                            <ComponentArt:GridColumn DataField="ExamStyleName" HeadingText="考试类型" />
                                            <ComponentArt:GridColumn DataField="IsStartName" HeadingText="考试状态" />
                                            <ComponentArt:GridColumn ColumnType="CheckBox" DataField="HasPaperDetail" HeadingText="生成试卷" />
                                            <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="操作" Width="50" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="CTEdit">
                                        <img id="img_##DataItem.getMember('RandomExamId').get_value()##" name="img_##DataItem.getMember('RandomExamId').get_value()##"
                                            alt="考试监控" style="cursor: hand; border: 0;" onclick='javascript:judgePaper("##DataItem.getMember("RandomExamId").get_value()##",##DataItem.getMember("OrgId").get_value()##);'
                                            src="../Common/Image/edit_col_edit.gif" />
                                       <img id="img1_##DataItem.getMember('RandomExamId').get_value()##" name="img1_##DataItem.getMember('RandomExamId').get_value()##"
                                            alt="调整考生机位" style="cursor: hand; border: 0;" onclick='javascript:judgeSeat("##DataItem.getMember("RandomExamId").get_value()##","##DataItem.getMember("HasPaperDetail").get_value()##");'
                                            src="../Common/Image/edit_col_see.gif" />
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="ClientTemplate3">
                                        ## DataItem.getMember("BeginTime").get_value().getYear()##-##DataItem.getMember("BeginTime").get_value().getMonth()+1##-##DataItem.getMember("BeginTime").get_value().getDate()##
                                        ##DataItem.getMember("BeginTime").get_value().getHours()##:##DataItem.getMember("BeginTime").get_value().getMinutes()##
                                        / ## DataItem.getMember("EndTime").get_value().getYear()##-##DataItem.getMember("EndTime").get_value().getMonth()+1##-##DataItem.getMember("EndTime").get_value().getDate()##
                                        ##DataItem.getMember("EndTime").get_value().getHours()##:##DataItem.getMember("EndTime").get_value().getMinutes()##
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                            </ComponentArt:Grid>
                        </Content>
                    </ComponentArt:CallBack>
                </div>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsExams" runat="server" SelectMethod="GetControlRandomExamsInfo"
            TypeName="RailExam.BLL.RandomExamBLL">
            <SelectParameters>
                <asp:ControlParameter DefaultValue="1" Name="orgID" ControlID="hfOrgID" Type="int32" />
                <asp:ControlParameter  Name="serverNo" ControlID="hfServerNo" Type="int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="hfIsAdmin" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <input name="Refresh" type="hidden" />
        <input name="StudentInfo" type="hidden" />
        <asp:HiddenField ID="hfServerNo" runat="server" />
    </form>
</body>
</html>
