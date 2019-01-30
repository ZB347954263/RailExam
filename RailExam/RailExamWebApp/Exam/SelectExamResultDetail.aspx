<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SelectExamResultDetail.aspx.cs"
    Inherits="RailExamWebApp.Exam.SelectExamResultDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考试成绩 - 考卷列表</title> 
   <meta http-equiv= "Content-Type " content= "text/html;charset=gb2312 " /> 
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        //按对象ID获取对象 
        function $F(objId)
        {
            return document.getElementById(objId);              
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

        //评卷按钮点击事件处理函数
        function judgePaper(eid,type)
        {       
            if(!eid || !parseInt(eid))
            {
                alert("不正确的数据！");
                
                return;
            }

            var search = window.location.search;
            var orgID= search.substring(search.indexOf("=")+1);
            
            var flagUpdate=document.getElementById("HfUpdateRight").value; 
            var flagIsAdmin = document.getElementById("hfIsAdmin").value;
            var mode="Edit";
            
//            if(flagIsAdmin == "False" || flagUpdate=="False")
//            {
//                  alert("您没有该操作的权限！");
//                  return;
//            }

            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-900)*.5;   
            ctop=(screen.availHeight-600)*.5;   
            
            if(type == 0)
            {
                var winGradeEdit = window.open("SelectExamResultEmployee.aspx?type=1&eid=" + eid +"&OrgID=" +orgID,
                    "SelectExamResultEmployee", "height=600, width=900,left="+cleft+",top="+ctop+",status=false,resizable=yes", true);
                 winGradeEdit.focus();
            }
            else
            {
                var winGradeEdit = window.open("/RailExamBao/RandomExam/RandomExamResultList.aspx?eid=" + eid +"&OrgID=" +orgID,
                    "RandomExamResultList", "height=600, width=950,left="+cleft+",top="+ctop+",status=false,resizable=yes", true);
                 winGradeEdit.focus();
            }
        }
        
        function DeleteData(name,id)
        {
            if(document.getElementById("hfDeleteRight").value =="False")
            {
                  alert("您没有该操作的权限！");
                  return false;
            }
            
             if(! confirm("删除考试，将会删除该考试的所有成绩以及答卷等信息，您确定要删除“" +name + "”吗？"))
            {
                return false;
            }
            form1.DeleteID.value = id;
            form1.submit();
            form1.DeleteID.value = "";
        }
       
       function selectExamCategory()
        {
            var selectedExamCategory = window.showModalDialog('../Common/SelectExamCategory.aspx', 
                    '', 'help:no; status:no; dialogWidth:340px;dialogHeight:620px');

                if(! selectedExamCategory)
                {
                    return;
                }
                document.getElementById("hfCategoryId").value = selectedExamCategory.split('|')[0];
                document.getElementById("txtCategoryName").value = selectedExamCategory.split('|')[1];
        } 
       
      
  function showCheckExam() 
  {
  	  var search = window.location.search;
      var orgID= search.substring(search.indexOf("=")+1);
  	  
  	  if(orgID == 1) {
  	  	alert("请选择组织机构！");
  	  	 return;
  	  }
  	
  	    var name = document.getElementById("txtExamName").value;
  	    var categoryId = document.getElementById("hfCategoryId").value;
  	    var beginDate = document.getElementById("dateStartDateTime_DateBox").value;
  	    var endDate = document.getElementById("dateEndDateTime_DateBox").value;
       	  
   	    var ret = window.showModalDialog("ShowCheckExam.aspx?OrgID="+orgID+"&name="+name+"&typeId="+categoryId+"&beginDate="+beginDate+"&endDate="+endDate,'','help:no;status:no;dialogWidth:800px;dialogHeight:600px;');
  }  
  
  function init() {
//  	  var isServer = document.getElementById("hfIsServer").value;
//  	
//  	  if(isServer == "False") {
//  	  	document.getElementById("btnExam").style.display = "none";
//  	  }
  }
    </script>

</head>
<body onLoad="init()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <div id="mainContent">
                    <div id="highquery"  >
                        &nbsp;&nbsp;考试名称
                        <asp:TextBox ID="txtExamName" runat="server" Width="100px"></asp:TextBox>
                        考试分类
                        <asp:TextBox ID="txtCategoryName" runat="server" Width="100px" ReadOnly="true">
                        </asp:TextBox>
                        <img style="cursor: hand;" onclick="selectExamCategory();" src="../Common/Image/search.gif"
                            alt="选择考试分类" border="0" />
                        有效时间 从
                        <uc1:Date ID="dateStartDateTime" runat="server" />
                        到
                        <uc1:Date ID="dateEndDateTime" runat="server" />
                        <asp:Button ID="searchButton" CssClass="button" Text="确  定" runat="server" OnClick="searchButton_onClick" />
                         <input type="button" id="btnExam" onclick="showCheckExam();" value="检查试卷完整性" class="buttonLong" />
                    </div>
                        <ComponentArt:Grid ID="examsGrid" runat="server" PageSize="18" >
                            <Levels>
                                <ComponentArt:GridLevel DataKeyField="ExamId">
                                    <Columns>
                                        <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit" HeadingText="操作"
                                            Width="40" />
                                        <ComponentArt:GridColumn DataField="ExamId" Visible="false" />
                                        <ComponentArt:GridColumn DataField="OrgId" Visible="false" />
                                        <ComponentArt:GridColumn DataField="ExamType" Visible="false" />
                                        <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" Width="240" />
                                        <ComponentArt:GridColumn DataField="ValidExamTimeDurationString" HeadingText="有效时间"
                                            Width="130" />
                                        <ComponentArt:GridColumn DataField="ExamineeCount" HeadingText="参考人次" Width="50" />
                                        <ComponentArt:GridColumn DataField="ExamAverageScore" HeadingText="平均成绩" Width="50" />
                                        <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="制卷人" Width="50" />
                                    </Columns>
                                </ComponentArt:GridLevel>
                            </Levels>
                            <ClientTemplates>
                                <ComponentArt:ClientTemplate ID="CTEdit">
                                    <a onclick="DeleteData('## DataItem.getMember('ExamName').get_value() ##',## DataItem.getMember('ExamId').get_value() ##)"
                                        href="#">
                                        <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a>
                                    <img id="img_##DataItem.getMember('ExamId').get_value()##" name="img_##DataItem.getMember('ExamId').get_value()##"
                                        alt="查看成绩" style="cursor: hand; border: 0;" onclick='judgePaper("##DataItem.getMember("ExamId").get_value()##","##DataItem.getMember("ExamType").get_value()##");'
                                        src="../Common/Image/edit_col_edit.gif" />
                                </ComponentArt:ClientTemplate>
                            </ClientTemplates>
                        </ComponentArt:Grid>
                </div>
            </div>
        </div>
<%--        <asp:ObjectDataSource ID="odsExams" runat="server" SelectMethod="GetExamsInfoByOrgID"
            TypeName="RailExam.BLL.ExamBLL">
            <SelectParameters>
                <asp:ControlParameter ControlID="txtExamName" Name="examName" PropertyName="Text"
                    Type="String" DefaultValue="" />
                <asp:ControlParameter ControlID="hfCategoryId" Name="CategoryId" PropertyName="Value"
                    Type="Int32" DefaultValue="-1" />
                <asp:ControlParameter ControlID="dateStartDateTime" DefaultValue="0001-01-01" Name="beginDateTime"
                    PropertyName="DateValue" Type="DateTime" />
                <asp:ControlParameter ControlID="dateEndDateTime" DefaultValue="0001-01-01" Name="endDateTime"
                    PropertyName="DateValue" Type="DateTime" />
                <asp:QueryStringParameter DefaultValue="1" Name="orgID" QueryStringField="Orgid"
                    Type="Int32" />
                <asp:ControlParameter ControlID="hfIsServer" Name="isServerCenter" Type="String"
                    PropertyName="Value" />
                <%-- <asp:ControlParameter ControlID="hfHasTwoServer" Name="hasTwoServer" Type="Boolean"
                    PropertyName="Value" />
                <asp:ControlParameter ControlID="hfIsMainServer" Name="isMainServer" Type="Boolean"
                    PropertyName="Value" />
            </SelectParameters>
        </asp:ObjectDataSource>--%>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="hfDeleteRight" runat="server" />
        <asp:HiddenField ID="hfIsAdmin" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <asp:HiddenField ID="hfIsServer" runat="server" />
        <asp:HiddenField ID="hfHasTwoServer" runat="server" />
        <asp:HiddenField ID="hfIsMainServer" runat="server" />
        <asp:HiddenField ID="hfCategoryId" runat="server" />
        <input type="hidden" name="DeleteID" />
    </form>
       <script type="text/javascript">
        if(window.location.search == "") {
        	 if(window.parent.tvView && window.parent.tvView.get_nodes().get_length() > 0 )//&& document.getElementById("hfIsServer").value!="True"
    	    {
    		    window.parent.tvView.get_nodes().getNode(0).select();
    	    }
        }

    </script> 
</body>
</html>
