<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EmployeeExam.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.EmployeeExam" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
    function forbiddenAdd()
    {
        var search=location.search;
        var index=search.indexOf("Type");
        if(index!=-1)
        {
            var type=search.substr(index+5,1);
            if(type=="0")
            {
                var btn=document.getElementById("btnRef");
                btn.style.display="none";
            }
        }
    	
    	if(document.getElementById("hfIsServerCenter").value=="False") {
    		var btn=document.getElementById("btnRef");
            btn.style.display="none";
    	}
    }
    
      function GetResult(id,orgid)
       {
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;   
            
            var winGradeEdit = window.open("/RailExamBao/RandomExam/RandomExamAnswerNew.aspx?id=" +id +"&orgid="+orgid,
                "RandomExamAnswerNew", "height=600, width=800,left="+cleft+",top="+ctop+",status=false,resizable=yes,scrollbars", true);
            winGradeEdit.focus(); 
       } 
      
       function OutPutPaper(id,orgid )
        {   
            //var ret = showCommonDialog("/RailExamBao/RandomExam/OutputPaperAllNew.aspx?eid="+id+"&OrgID="+ orgid +"&Mode=one",'dialogWidth:320px;dialogHeight:30px;');
        	var ret = window.showModalDialog("/RailExamBao/RandomExam/OutputPaperAllA3.aspx?eid="+id+"&OrgID="+ orgid +"&Mode=one",
                        '', 'help:no; status:no; dialogWidth:320px;dialogHeight:50px;scroll:no;');
        	if(ret != "")
            {
//               form1.OutPut.value =ret;
//               form1.submit();
        		  try
        		  {
        		  	    var location = window.location.href;
    		            var newurl = location.substring(0, location.indexOf("RailExamBao/") + 12);
        		  	    var path = "http://10.72.4.196/RailExamBao/Excel/"+ret+".doc";
        		  	    //alert(path);
                        var wdapp = new ActiveXObject("Word.Application");
        		  	    wdapp.visible = false;
        		  	    wdapp.Documents.Open(path);
        		  	    wdapp.Application.Printout(); //自动打印
                        wdapp=null;
                  }
        		  catch(e)
        		  { 
                        alert("无法调用Office对象，请确保您的机器已安装了Office并已将IE安全级别降低！"); 
                  } 
        	}
        }
    </script>
</head>
<body onload="forbiddenAdd()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div style="text-align: right;">
                    <asp:Button ID="btnRef" runat="server" class="button" Text="更  新" OnClick="btnRef_Click" />
                    <asp:Button ID="btnDelete" runat="server" Style="display: none" OnClick="btnDelete_Click" />
                </div>
                <div style="text-align: center;">
                    <ComponentArt:Grid ID="grdEntity" runat="server" PageSize="15" Width="850px">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="employee_exam_id">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="employee_exam_id" Visible="false" />
                                    <ComponentArt:GridColumn DataField="exam_name" HeadingText="考试名称" Width="200" />
                                    <ComponentArt:GridColumn DataField="EXAM_DATE" HeadingText="考试时间"  FormatString="yyyy-MM-dd" Width="80" />
                                    <ComponentArt:GridColumn DataField="exam_address" HeadingText="考试地址" Width="200"/>
                                    <ComponentArt:GridColumn DataField="exam_subject" HeadingText="考试科目" Width="100"/>
                                    <ComponentArt:GridColumn DataField="exam_score" HeadingText="成绩" Width="50"/>
                                    <ComponentArt:GridColumn DataField="Result" HeadingText="是否合格" Width="70" />
                                    <ComponentArt:GridColumn DataField="create_date1" HeadingText="更新时间" Width="80"/>
                                    <ComponentArt:GridColumn DataField="create_person" HeadingText="更新人" Width="80"/>
                                    <ComponentArt:GridColumn DataField="RandomExamResultID" Visible="False"/>
                                    <ComponentArt:GridColumn DataField="OrgID"  Visible="False"/>
                                    <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" Width="120"/>
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="CTEdit">
                                <a onclick="javascript:if(!confirm('您确定要删除此考试情况吗？')){return;}__doPostBack('btnDelete','## DataItem.getMember('employee_exam_id').get_value() ##');"
                                    title="删除考试情况" href="#"><b>删除</b></a>
                                 <a onclick="GetResult(##DataItem.getMember('RandomExamResultID').get_value()##,##DataItem.getMember('OrgID').get_value()##)"
                                    href="#"><b>查看</b></a> <a onclick="OutPutPaper(##DataItem.getMember('RandomExamResultID').get_value()##,##DataItem.getMember('OrgID').get_value()##)"
                                        href="#"><b>打印</b></a>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfID" runat="server" />
        <asp:HiddenField runat="server" ID="hfIsServerCenter"/>
    </form>
</body>
</html>
