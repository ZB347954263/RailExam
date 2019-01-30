<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamResultList.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamResultList" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>随机考试 - 考试成绩</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
        //按对象ID获取对象 
        function $F(objId) 
       {
            return document.getElementById(objId);
       } 
    
        function gradesGrid_onItemUpdate(s, e)
        {
        }       
        
        function OutPutPaper(id,orgid )
        {   
        	var flagUpdate=document.getElementById("HfUpdateRight").value; 
            
            if(flagUpdate=="False")
            {
                  alert("您没有该操作的权限！");
                  return;
            }
        	
            var isServer = document.getElementById("hfIsServer").value;
            var NowOrgID = document.getElementById("hfOrganizationId").value;
            if(isServer == "False")
            {
                if(NowOrgID !=orgid)
               {
                    alert("请连接路局考试系统导出该考生的试卷！");
                    return;
               } 
            }
            //var ret = showCommonDialog("/RailExamBao/RandomExam/OutputPaperAllNew.aspx?eid="+id+"&OrgID="+ orgid +"&Mode=one",'dialogWidth:320px;dialogHeight:30px;');
        	var ret = window.showModalDialog("/RailExamBao/RandomExam/OutputPaperAllA3.aspx?eid="+id+"&OrgID="+ orgid +"&Mode=one",
                        '', 'help:no; status:no; dialogWidth:320px;dialogHeight:50px;scroll:no;');
        	if(ret != "")
            {
               form1.OutPut.value =ret;
               form1.submit();
//        		  try
//        		  {
//        		  	    var location = window.location.href;
//    		            var newurl = location.substring(0, location.indexOf("RailExamBao/") + 12);
//        		  	    var path = newurl + "Excel/"+ret+".doc";
//        		  	    //alert(path);
//                        var wdapp = new ActiveXObject("Word.Application");
//        		  	    wdapp.visible = false;
//        		  	    wdapp.Documents.Open(path);
//        		  	    wdapp.Application.Printout(); //自动打印
//                        wdapp=null;
//                  }
//        		  catch(e)
//        		  { 
//                        alert("无法调用Office对象，请确保您的机器已安装了Office并已将IE安全级别降低！"); 
//                  } 
        	}
        }

       //显示或隐藏查询区域
        function QueryRecord()
        {
            if($F("query").style.display == '')
            {
                $F("query").style.display = 'none';
            }
            else
            {
                $F("query").style.display = '';
            }
        } 
       
       function GetResult(id,orgid)
       {
            var isServer = document.getElementById("hfIsServer").value;
            var NowOrgID = document.getElementById("hfOrganizationId").value;
            if(isServer == "False")
            {
                if(NowOrgID !=orgid)
               {
                    alert("请连接路局考试系统查询该考生的试卷！");
                    return;
               } 
            }
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;   
            
            var winGradeEdit = window.open("RandomExamAnswerNew.aspx?id=" +id +"&orgid="+orgid,
                "RandomExamAnswerNew", "height=600, width=800,left="+cleft+",top="+ctop+",status=false,resizable=yes,scrollbars", true);
            winGradeEdit.focus(); 
       } 
      
      function ShowProgressBar(strType)
      {
      	var flagUpdate=document.getElementById("HfUpdateRight").value; 
            
        if(flagUpdate=="False")
        {
              alert("您没有该操作的权限！");
              return;
        }
      	
         var search = window.location.search;
        //var ret = showCommonDialog("/RailExamBao/RandomExam/OutputPaperAllNew.aspx"+search+"&Mode=All&Type="+strType,'dialogWidth:320px;dialogHeight:30px;');
        var ret = window.showModalDialog("/RailExamBao/RandomExam/OutputPaperAllA3.aspx"+search+"&Mode=AllOne&Type="+strType,
                        '', 'help:no; status:no; dialogWidth:320px;dialogHeight:50px;scroll:no;');
      	if(ret != "")
        {
           form1.Refresh.value = ret;
           form1.submit();
//      		  try
//        		  {
//        		  	    var location = window.location.href;
//    		            var newurl = location.substring(0, location.indexOf("RailExamBao/") + 12);
//        		  	    var path = newurl + "Excel/"+ret+".doc";
//                        var wdapp = new ActiveXObject("Word.Application");
//        		  	    wdapp.visible = false;
//        		  	    wdapp.Documents.Open(path);
//        		  	    wdapp.Application.Printout(); //自动打印
//                        wdapp=null;
//                  }
//        		  catch(e)
//        		  { 
//                        alert("无法调用Office对象，请确保您的机器已安装了Office并已将IE安全级别降低！"); 
//               } 
        }
      } 
      
     function ShowProgressBarExcel()
      {
     	var flagUpdate=document.getElementById("HfUpdateRight").value; 
            
        if(flagUpdate=="False")
        {
              alert("您没有该操作的权限！");
              return;
        }
     	
        var orgName = document.getElementById("txtOrganizationName").value;
        var employeeName = document.getElementById("txtExamineeName").value;
        var lowerScore = document.getElementById("txtScoreLower").value;
        var upperScore = document.getElementById("txtScoreUpper").value;
         var search = window.location.search;
        //var ret = showCommonDialog("/RailExamBao/RandomExam/ExportExcel.aspx"+search+"&Mode=Excel&orgName="+orgName+"&emploeeName="+employeeName+"&lowerScore="+lowerScore+"&upperScore="+upperScore,'dialogWidth:320px;dialogHeight:30px;');      
     	var ret = window.showModalDialog("/RailExamBao/RandomExam/ExportExcel.aspx"+search+"&Mode=Excel&orgName="+orgName+"&emploeeName="+employeeName+"&lowerScore="+lowerScore+"&upperScore="+upperScore,
                        '', 'help:no; status:no; dialogWidth:320px;dialogHeight:50px;scroll:no;');
     	if(ret == "true")
        {
           form1.RefreshExcel.value ="true";
           form1.submit();
        }
      } 
     
     
     function ShowProgressBarUpdate()
     {
     	var flagUpdate=document.getElementById("HfUpdateRight").value; 
            
        if(flagUpdate=="False")
        {
              alert("您没有该操作的权限！");
              return;
        }
     	
        var search = window.location.search;
         var id = search.substring(search.indexOf("eid=")+4,search.indexOf("&"));
        var orgid = search.substring(search.indexOf("&OrgID=")+7);
        var ret = window.showModalDialog("/RailExamBao/RandomExam/UpdateEmployee.aspx?RandomExamID="+id+"&OrgID="+ orgid +"&type=exam",
                        '', 'help:no; status:no; dialogWidth:320px;dialogHeight:50px;scroll:no;');
     	if(ret == "true")
        {
           alert("更新成功！");
        }
        else
        {
            alert("更新失败！");
        }
     } 
      
      //上传成绩
      function UploadExam()
      {
      	var flagUpdate=document.getElementById("HfUpdateRight").value; 
            
        if(flagUpdate=="False")
        {
              alert("您没有该操作的权限！");
              return;
        }
      	
         var search = window.location.search;
         var id = search.substring(search.indexOf("eid=")+4,search.indexOf("&"));
         //var ret = showCommonDialog("/RailExamBao/RandomExam/FalseProgressBar.aspx?RandomExamID="+id+"&type=Upload",'dialogWidth:400px;dialogHeight:30px;');
         var ret = window.showModalDialog("/RailExamBao/RandomExam/DealPaperProgress.aspx?RandomExamID="+id+"&type=Upload&typeid=2", 
        '', 'help:no; status:no; dialogWidth:360px;dialogHeight:30px;scroll:no;'); 
 
         if(ret == "true" )
         {
             form1.IsUpload.value = ret;
             form1.submit();
             form1.IsUpload.value = "";
         }
         else
         {
            alert("上传失败！");
         }
      } 
      //弹出未参加考试的学员
      function ShowNoExam()
      {
         var search = window.location.search;
         var ret = showCommonDialog("/RailExamBao/RandomExam/RandomExamNoExam.aspx"+search,'dialogWidth:800px;dialogHeight:600px;');
      }
      
     function examPaper() {
     	var flagUpdate=document.getElementById("HfUpdateRight").value; 
            
            if(flagUpdate=="False")
            {
                  alert("您没有该操作的权限！");
                  return false;
            }

     	return true;
     }
     
     function init() {
     	if(document.getElementById("hfIsServer").value=="False") {
     		document.getElementById("btnUpdate").style.display = "none";
     	}
     	else {
     		document.getElementById("btnUpdate").style.display = "";
     	}
     	
     	if(document.getElementById("hfRoleID").value != "1") {
     			document.getElementById("Button1").style.display = "none";
     			document.getElementById("btnOutPutWord").style.display = "none";
     	}
     }
    </script>

</head>
<body onLoad="init()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div style="text-align: left;width: 10%;float:left;">
<%--            <div id="separator">
                    </div>--%>
                    <div>
                        成绩查询</div>
                </div>
                <div id="button">
                    <asp:Button runat="server" ID="btnOutPut" Text="导出成绩登记表" CssClass="buttonEnableLong"
                        OnClientClick="ShowProgressBarExcel()" />&nbsp;
                    <asp:Button runat="server" ID="btnOutPutWord" Text="批量导出答卷" CssClass="buttonLong" 
                        OnClientClick="ShowProgressBar('All')" />&nbsp;
                    <asp:Button runat="server" ID="Button1" Text="批量导出合格答卷" CssClass="buttonEnableLong" 
                        OnClientClick="ShowProgressBar('Pass')" />&nbsp;
                    <asp:Button runat="server" ID="btnNoExam" Text="未参加考试学员" CssClass="buttonEnableLong"
                        OnClientClick="ShowNoExam()" />&nbsp;
                    <asp:Button runat="server" ID="btnExam" Text="检查考生试卷" CssClass="buttonEnableLong" OnClientClick="return examPaper()"
                         OnClick="btnExam_Click" />&nbsp;
                    <asp:Button ID="btnUpload" runat="server" Text="上传考试答卷" CssClass="buttonLong" ToolTip="将考试成绩和答卷上传至路局数据库"
                        OnClick="btnUpload_Click" />&nbsp;
                     <asp:Button ID="btnUpdate" runat="server" Text="批量更新考生档案" CssClass="buttonEnableLong" ToolTip=""
                        OnClientClick="ShowProgressBarUpdate()" />&nbsp;
                    <input type="button" onclick="QueryRecord();" class="button" value="查   询" />
                </div>
            </div>
            <div id="content">
                <div id="query" style="display: none;">
                    &nbsp;&nbsp;单位名称
                    <asp:TextBox ID="txtOrganizationName" runat="server" Width="80">
                    </asp:TextBox>
                   车间
                    <asp:TextBox ID="txtWorkShop" runat="server" Width="80">
                    </asp:TextBox> 
                    姓名
                    <asp:TextBox ID="txtExamineeName" runat="server" Width="80">
                    </asp:TextBox>
                    <asp:Label ID="lblWorkNo" runat="server" Text="上岗证号"></asp:Label>
                    <asp:TextBox ID="txtWorkNo" runat="server" Width="80"></asp:TextBox>
                    分数 从
                    <asp:TextBox ID="txtScoreLower" runat="server" Width="80">
                    </asp:TextBox>
                    到
                    <asp:TextBox ID="txtScoreUpper" runat="server" Width="80">
                    </asp:TextBox>
                    <asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Common/Image/confirm.gif"
                        OnClick="btnSearch_Click" />
                </div>
                <div id="mainContent">
                    <table style="width: 100%; height: 25px; padding-left: 10px; padding-right: 10px;
                        line-height: 25px; color: #2D67CF; font-size: 12px">
                        <tr>
                            <td style="width: 15%; text-align: right">
                                应考人数：</td>
                            <td style="width: 35%; text-align: left">
                                <asp:Label ID="lblMaxCount" runat="server"></asp:Label>人</td>
                            <td style="width: 15%; text-align: right">
                                实考人数：</td>
                            <td style="width: 35%; text-align: left">
                                <asp:Label ID="lblNowCount" runat="server"></asp:Label>人</td>
                        </tr>
                    </table>
                    <ComponentArt:Grid ID="gradesGrid" runat="server" AllowPaging="true" PageSize="15"
                        Width="100%">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="RandomExamResultID">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="RandomExamResultID" HeadingText="编号" Visible="false" />
                                    <ComponentArt:GridColumn DataField="RandomExamResultIDStation" HeadingText="编号" Visible="false" />
                                    <ComponentArt:GridColumn DataField="ExamineeName" HeadingText="考生姓名" Width="70" />
                                    <ComponentArt:GridColumn DataField="WorkNo" HeadingText="员工编码<br>(身份证号码)" Width="120" />
                                    <ComponentArt:GridColumn DataField="PostName" HeadingText="职名" />
                                    <ComponentArt:GridColumn DataField="OrganizationId" Visible="false" />
                                    <ComponentArt:GridColumn DataField="OrganizationName" HeadingText="考生单位" Width="150" />
                                    <ComponentArt:GridColumn DataField="ExamOrgName" HeadingText="考试地点" Width="100" />
                                    <ComponentArt:GridColumn DataField="BeginDateTime" HeadingText="开始时间" Width="122"
                                        DataType="System.DateTime" />
                                    <ComponentArt:GridColumn DataField="EndDateTime" HeadingText="结束时间" Width="123" DataType="System.DateTime" />
                                    <ComponentArt:GridColumn DataField="BeginDateTime" HeadingText="考试时间" Width="122"
                                        FormatString="yyyy-MM-dd" />
                                    <ComponentArt:GridColumn DataField="Score" HeadingText="成绩" DataType="System.Decimal"
                                        Width="40" />
                                    <ComponentArt:GridColumn AllowSorting="false" HeadingText="操作" DataCellClientTemplateId="EditTemplate"
                                        Width="70" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="EditTemplate">
                                <a onclick="GetResult(##DataItem.getMember('RandomExamResultID').get_value()##,##DataItem.getMember('OrganizationId').get_value()##)"
                                    href="#"><b>查看</b></a> <a onclick="OutPutPaper(##DataItem.getMember('RandomExamResultID').get_value()##,##DataItem.getMember('OrganizationId').get_value()##)"
                                        href="#"><b>导出</b></a>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfOrganizationId" runat="server" />
        <asp:HiddenField ID="hfIsServer" runat="server" />
        <input type="hidden" name="OutPut" />
        <input type="hidden" name="OutOrgID" />
        <input type="hidden" name="Refresh" />
        <input type="hidden" name="RefreshList" />
        <input type="hidden" name="RefreshExcel" />
        <input name="IsUpload" type="hidden" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="hfDeleteRight" runat="server" />
       <asp:HiddenField ID="hfRoleID" runat="server" /> 
    </form>
</body>
</html>
