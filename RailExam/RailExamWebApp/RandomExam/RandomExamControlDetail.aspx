<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamControlDetail.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamControlDetail" %>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考试监控</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">              
        function judgePaper(examResultId,orgid,statusid)
       {
            if(statusid==1)
            {
                var re= window.open("/RailExamBao/RandomExam/RandomExamAnswerCurrent.aspx?id=" +examResultId +"&orgid="+orgid,
                    "ExamResult",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
                re.focus();
            }
            else if(statusid==2)
            {
                var re= window.open("/RailExamBao/RandomExam/RandomExamAnswer.aspx?id=" +examResultId +"&orgid="+orgid,
                    "ExamResult",'top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=yes,status=no,scrollbars=yes');
                re.focus();
            }
            else
            {
                alert("该考生本次考试还未开始，不能查看试卷！");
               return; 
            }
       }  
       
      function StopConfirm()
       {
            if(! confirm("您确定要终止当前所有考生的考试吗？"))
            {
                return false;
            }
            return true;   
         } 
       
      
       function EndConfirm()
       {
            var count = document.getElementById("hfNowCount").value; 
            var str;
           // alert(count);
            if(count <=0)
            {
                str = "所有考生已完成考试！";
            }
            else
            {
                str = "还有"+ count +"位考生没有完成考试！";
            }
            if(form1.IsServer.value == "True")
            {
                if(! confirm(str+"\r\n结束考试后未完成考试的考生将不能参加考试，\r\n您确定要结束考试吗？"))
                {
                    return false;
                }
            } 
            else
            {
                //结束考试将同步上传考试成绩和答卷，
                if(! confirm(str+"\r\n结束考试后未完成考试的考生将不能参加考试，\r\n您确定要结束考试吗？"))
                {
                    return false;
                }
            }
            return true;   
         }
         
       function DelConfirm()
       {
            if(! confirm("您确定要删除所有考生的试卷吗？"))
            {
                return false;
            }
            return true;   
         }
        
       function OutPutPaper(id,statusid)
        {   
            var orgid = document.getElementById("hfOrgID").value;
            if(statusid != 2)
           {
                alert("该考生考试没有结束，不能导出答卷！");
               return; 
           } 
            //var ret = showCommonDialog("/RailExamBao/RandomExam/OutputPaperAllNew.aspx?eid="+id+"&OrgID="+ orgid +"&Mode=one",'dialogWidth:320px;dialogHeight:30px;');
           var ret = window.showModalDialog("/RailExamBao/RandomExam/OutputPaperAllA3.aspx?eid="+id+"&OrgID="+ orgid +"&Mode=one", 
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:40px;scroll:no;'); 
           if(ret != "" )
            {
               form1.OutPutRandom.value =ret;
               form1.submit();
//           	      try
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
//                  } 
            }
        }  
       
       function StopNowExam(id,statusid)
       {
            var flagUpdate=document.getElementById("HfUpdateRight").value; 
            if(flagUpdate=="False")
            {
                  alert("您没有该操作的权限！");
                return false;          
            }         
           if(statusid==2)
          {
              alert('该学员考试已经结束');
                return false;          
           }
         
         if(! confirm("终止考试后考生将无法继续答题，您确定要终止该考生正在进行的考试吗？"))
         {
            return false;
         }  
       	
       	 if(! confirm("终止考试后考生将无法继续答题，您再次确定要终止该考生正在进行的考试吗？"))
         {
            return false;
         }  
          
          form1.StopExam.value = id;
          form1.submit();
          form1.StopExam.value = "";
       }

       function deleteExam(id,statusid) 
       {
               var flagUpdate=document.getElementById("HfUpdateRight").value; 
                if(flagUpdate=="False")
                {
                      alert("您没有该操作的权限！");
                      return false;  
                }         
               if(statusid==2)
              {
                  alert('该学员考试已经结束，不能删除！');
                 return false;  
              }
       	     
       	      if(statusid==1)
              {
                  alert('该学员考试正在进行，不能删除！');
                  return false;          
              } 
             
             if(! confirm("删除试卷后，考生将无法连接本服务器进行考试。\r\n您确定要删除该考生在本服务器上的考试试卷吗？"))
             {
                return false;
             }  
       	
       	    if(! confirm("删除试卷后，考生将无法连接本服务器进行考试。\r\n您再次确定要删除该考生在本服务器上的考试试卷吗？"))
             {
                return false;
             }  
              
              form1.DeleteExam.value = id;
              form1.submit();
              form1.DeleteExam.value = "";
       } 
       
      function clearExam(id,statusid)  
      {    
               if(statusid==0)
              {
                  alert('该学员考试未开始，不能清除！');
                 return false;  
              }
       	     
       	      if(statusid==1)
              {
                  alert('该学员考试正在进行，不能清除！');
                  return false;          
              } 
             
             if(! confirm("清除考试后，本服务器将无该考生本场考试成绩。\r\n您确定要清除该考生在本服务器上的考试试卷吗？"))
             {
                return false;
             }  
       	
       	    if(! confirm("清除考试后，本服务器将无该考生本场考试成绩。\r\n您再次确定要清除该考生在本服务器上的考试试卷吗？"))
             {
                return false;
             }  
              
              form1.ClearExam.value = id;
              form1.submit();
              form1.ClearExam.value = "";
      }
      
     function replyExam(id,statusid)  
      {      
               if(statusid==0)
              {
                  alert('该学员考试未开始，不能恢复！');
                 return false;  
              }
       	     
       	      if(statusid==1)
              {
                  alert('该学员考试正在进行，不能恢复！');
                  return false;          
              } 
             
             if(! confirm("恢复考试后，该考生将重新继续本场考试，并且时间回退10分钟。\r\n您确定要恢复该考生在本服务器上的考试试卷吗？"))
             {
                return false;
             }  
       	
       	    if(! confirm("恢复考试后，该考生将重新继续本场考试，并且时间回退10分钟。\r\n您再次确定要恢复该考生在本服务器上的考试试卷吗？"))
             {
                return false;
             }  
              
              form1.ReplyExam.value = id;
              form1.submit();
              form1.ReplyExam.value = "";
      } 
       
       function unload()
       {
            window.opener.form1.Refresh.value = "true";
            window.opener.form1.submit();
            window.opener.form1.Refresh.value = "";
       }
      
      //生成试卷
      function GetPaper()
      {
         var search = window.location.search;
//      var ret = showCommonDialog("/RailExamBao/RandomExam/FalseProgressBar.aspx"+search+"&type=Get",'dialogWidth:410px;dialogHeight:30px;');
//      var ret = window.showModalDialog("/RailExamBao/RandomExam/FalseProgressBar.aspx"+search+"&type=Get", 
//                '', 'help:no; status:no; dialogWidth:410px;dialogHeight:30px;scroll:no;'); 
      	var ret = window.showModalDialog("/RailExamBao/RandomExam/DealPaperProgress.aspx"+search+"&type=Get", 
                '', 'help:no; status:no; dialogWidth:360px;dialogHeight:30px;scroll:no;'); 
      	if(ret == "true" )
         {
             form1.IsGet.value = ret;
             form1.submit();
             form1.IsGet.value = "";
         }
         else {
         	alert(ret);
         }
      } 
      
      //结束考试
       function EndPaper()
      {
         var search = window.location.search;
        //var ret = showCommonDialog("/RailExamBao/RandomExam/FalseProgressBar.aspx"+search+"&type=End",'dialogWidth:410px;dialogHeight:30px;');
         var ret = window.showModalDialog("/RailExamBao/RandomExam/FalseProgressBar.aspx"+search+"&type=End", 
        '', 'help:no; status:no; dialogWidth:410px;dialogHeight:30px;scroll:no;'); 
       	if(ret == "true" )
         {
             form1.IsEnd.value = ret;
             form1.submit();
             form1.IsEnd.value = "";
         }
      } 
      
      //开始考试
      function StartPaper()
      {
         var search = window.location.search;
         //var ret = showCommonDialog("/RailExamBao/RandomExam/RandomExamStart.aspx"+search,'dialogWidth:400px;dialogHeight:30px;');
      	 var ret = window.showModalDialog("/RailExamBao/RandomExam/RandomExamStart.aspx"+search, 
                   '', 'help:no; status:no; dialogWidth:400px;dialogHeight:30px;scroll:no;'); 
         if(ret == "true" )
         {
             form1.IsStart.value = ret;
             form1.submit();
             form1.IsStart.value = "";
         }
      } 
      
      
      //上传成绩
      function UploadExam(typeid)
      {
         var search = window.location.search;
         //var ret = showCommonDialog("/RailExamBao/RandomExam/FalseProgressBar.aspx"+search+"&type=Upload",'dialogWidth:400px;dialogHeight:30px;');
        var ret = window.showModalDialog("/RailExamBao/RandomExam/DealPaperProgress.aspx"+search+"&type=Upload&typeid="+typeid, 
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
      
      //回复考试请求
      function ApplyExam()
      {
          var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-850)*.5;   
          ctop=(screen.availHeight-650)*.5; 
          
          var search = window.location.search; 
          var re= window.open("/RailExamBao/RandomExam/RandomExamApplyDetail.aspx"+search,
                    "RandomExamApplyDetail",'Width=850px; Height=650px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
         re.focus();
      }
      
      //添加考生
      function showEmployee(id)
      {
        var search = window.location.search;
      	var ret;
      	
      	if(id == 0) 
      	{
            ret = showCommonDialog("/RailExamBao/RandomExam/SelectEmployeeAfterGetPaper.aspx"+search,'dialogWidth:800px;dialogHeight:600px;');
      	}
      	else {
            ret = showCommonDialog("/RailExamBao/RandomExam/SelectEmployeeAfterGetPaperTrainClass.aspx"+search,'dialogWidth:800px;dialogHeight:600px;');
      	}
      	
         if(ret == "true" )
         {
             form1.submit();
         }
      }
      
      function ShowProgressBar()
      {
         var search = window.location.search; 
         //var ret = showCommonDialog("/RailExamBao/RandomExam/ExportExcel.aspx"+search+"&Type=StudentInfo",'dialogWidth:320px;dialogHeight:30px;');
         var ret = window.showModalDialog("/RailExamBao/RandomExam/ExportExcel.aspx"+search+"&Type=StudentInfo", 
          '', 'help:no; status:no; dialogWidth:320px;dialogHeight:50px;scroll:no;'); 
         if(ret == "true")
         {
           form1.StudentInfo.value = "true";
           form1.submit();
         }
      } 
      
      function refreshGrid() {
      	    var name = document.getElementById("txtEmployeeName").value;
      	    var workNo = document.getElementById("txtWorkNo").value;
      	    var cardNo = document.getElementById("txtIdentityCardNo").value;
      	    searchExamCallBack.callback(escape(name),escape(workNo),escape(cardNo));
      }
      
      function refreshGridCallback_CallbackComplete() {
      	var name = document.getElementById("txtEmployeeName").value;
      	var workNo = document.getElementById("txtWorkNo").value;
      	var cardNo = document.getElementById("txtIdentityCardNo").value;
      	searchExamCallBack.callback(escape(name),escape(workNo),escape(cardNo));
      }
    </script>

</head>
<body onunload="unload()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div style="width: 10%; float: left;">
                    <div style="color: #2D67CF; float: left;">
                        考试监控</div>
                </div>
                <div id="button">
                    <asp:Button ID="btnGetPaper" runat="server" Text="生成所有考生试卷" CssClass="buttonEnableLong"
                        OnClick="btnGetPaper_Click" />
                    <asp:Button ID="btnDelPaper" runat="server" Text="删除所有考生试卷" CssClass="buttonEnableLong"
                        OnClientClick="return DelConfirm()" OnClick="btnDelPaper_Click" />
                    <asp:Button ID="btnAddEmployee" runat="server" Text="添加考生" CssClass="button" Visible="false"
                        OnClick="btnAddEmployee_Click" />
                    <asp:Button ID="btnExcel" runat="server" Text="导出考生信息" OnClientClick="ShowProgressBar()"
                        CssClass="buttonLong" />
                    <asp:Button ID="btnStart" runat="server" Text="开始考试" CssClass="button" OnClick="btnStart_Click" />
                    <asp:Button ID="btnStop" runat="server" Text="终止当前考试" OnClientClick="return StopConfirm()"
                        CssClass="buttonLong" OnClick="btnStop_Click" />
                    <asp:Button ID="btnEnd" runat="server" Text="结束考试" OnClientClick="return EndConfirm()"
                        CssClass="button" OnClick="btnEnd_Click" />
                    <asp:Button ID="btnUploadScore" runat="server" Text="上传考试成绩" Visible="False"
                        CssClass="buttonLong" OnClick="btnUploadScore_Click" />
                    <asp:Button ID="btnUpload" runat="server" Text="上传考试答卷" CssClass="buttonLong" ToolTip="将考试成绩和答卷上传至路局数据库"
                        OnClick="btnUpload_Click" />
                </div>
            </div>
            <div id="content">
                <div style="text-align: left">
                    <table>
                        <tr>
                            <td align="left">
                                <font style="color: #2D67CF;">
                                    <asp:Label ID="lblTitle" runat="server"></asp:Label></font>
                                <asp:Label ID="lblCode" runat="server" ForeColor="red"></asp:Label>
                                &nbsp;&nbsp;<asp:Button ID="btnApply" runat="server" CssClass="buttonLong" OnClientClick="ApplyExam()"
                                    Visible="false" Text="回复考试请求" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="query">
                    &nbsp;&nbsp; 姓名
                    <asp:TextBox ID="txtEmployeeName" runat="server" Width="80">
                    </asp:TextBox>
                    &nbsp;&nbsp;员工编码
                    <asp:TextBox ID="txtWorkNo" runat="server" Width="80"></asp:TextBox>
                    &nbsp;&nbsp;身份证号
                    <asp:TextBox ID="txtIdentityCardNo" runat="server" Width="80"></asp:TextBox>
                    <asp:ImageButton runat="server" ID="btnSearch" ImageUrl="~/Common/Image/confirm.gif"
                        OnClick="btnSearch_Click" />
                </div>
                <div style="text-align: left;">
                    <ComponentArt:CallBack ID="searchExamCallBack" runat="server" Debug="false" PostState="true" 
                        OnCallback="searchExamCallBack_Callback">
                        <Content>
                            <ComponentArt:Grid ID="examsGrid" runat="server" AutoAdjustPageSize="false" PageSize="20" 
                                DataSourceID="odsExams">
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="RandomExamResultId">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="RandomExamResultId" Visible="false"/>
                                            <ComponentArt:GridColumn DataField="ExamineeId" Visible="false" />
                                            <ComponentArt:GridColumn DataField="ExamineeName" HeadingText="考生姓名" Width="60" />
                                            <ComponentArt:GridColumn DataField="WorkNo" HeadingText="员工编码<br>（身份证号码)" Width="105" />
                                            <ComponentArt:GridColumn DataField="OrganizationId" HeadingText="考生单位" Visible="false" />
                                            <ComponentArt:GridColumn DataField="OrganizationName" HeadingText="考生单位" Width="150" />
                                            <ComponentArt:GridColumn DataField="BeginDateTime" HeadingText="开始时间" FormatString="yyyy-MM-dd HH:mm"
                                                Width="100" />
                                            <ComponentArt:GridColumn DataField="EndDateTime" HeadingText="结束时间" FormatString="yyyy-MM-dd HH:mm"
                                                Width="100" />
                                            <ComponentArt:GridColumn DataField="StatusId" HeadingText="考试状态ID" Visible="false" />
                                            <ComponentArt:GridColumn DataField="StatusName" HeadingText="考试状态" Width="55" />
                                            <ComponentArt:GridColumn DataField="Score" HeadingText="分数" Width="30" />
                                            <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="操作"
                                                Width="200" />
                                             <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit1" HeadingText="操作" Visible="False"
                                                Width="150" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="CTEdit">
                                        <a onclick="OutPutPaper(##DataItem.getMember('RandomExamResultId').get_value()##,##DataItem.getMember('StatusId').get_value()##)"
                                            href="#"><b>导出答卷</b></a>&nbsp;&nbsp; <a onclick="StopNowExam(##DataItem.getMember('RandomExamResultId').get_value()##,##DataItem.getMember('StatusId').get_value()##)"
                                                href="#"><b>终止考试</b></a>&nbsp;&nbsp; <a onclick="deleteExam(##DataItem.getMember('RandomExamResultId').get_value()##,##DataItem.getMember('StatusId').get_value()##)"
                                                href="#"><b>删除考试</b></a>
                                    </ComponentArt:ClientTemplate>
                                    <ComponentArt:ClientTemplate ID="CTEdit1">
                                         <a onclick="clearExam(##DataItem.getMember('RandomExamResultId').get_value()##,##DataItem.getMember('StatusId').get_value()##)"
                                                href="#"><b>清除考试</b></a>&nbsp;&nbsp; <a onclick="replyExam(##DataItem.getMember('RandomExamResultId').get_value()##,##DataItem.getMember('StatusId').get_value()##)"
                                                href="#"><b>恢复考试</b></a>
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                            </ComponentArt:Grid>
                            <asp:HiddenField ID="hfSql" runat="server" />
                        </Content>
                    </ComponentArt:CallBack>
                </div>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsExams" runat="server" SelectMethod="GetRandomExamResultInfoByExamID"
            TypeName="RailExam.BLL.RandomExamResultCurrentBLL">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="1" Name="examID" QueryStringField="RandomExamID"
                    Type="int32" />
                <asp:ControlParameter ControlID="hfSql" Name="sql" PropertyName="Value" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="hfIsAdmin" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <input type="hidden" name="IsServer" value='<%=PrjPub.IsServerCenter %>' />
        <input name="OutPutRandom" type="hidden" />
        <input name="StopExam" type="hidden" />
        <input name="DeleteExam" type="hidden" />
        <input name="ClearExam" type="hidden" />
        <input name="ReplyExam" type="hidden" />
        <input name="IsGet" type="hidden" />
        <input name="IsEnd" type="hidden" />
        <input name="IsStart" type="hidden" />
        <input name="IsUpload" type="hidden" />
        <input name="StudentInfo" type="hidden" />
        <asp:HiddenField ID="hfNowCount" runat="server" />
        <ComponentArt:CallBack ID="refreshGridCallback" runat="server" Debug="false" PostState="true" OnCallback="refreshGridCallback_Callback">
            <ClientEvents>
                <CallbackComplete EventHandler="refreshGridCallback_CallbackComplete"></CallbackComplete>
            </ClientEvents>
        </ComponentArt:CallBack>
    </form>
</body>
</html>
