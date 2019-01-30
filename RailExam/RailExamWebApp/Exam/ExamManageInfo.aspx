<%@ Page Language="C#" AutoEventWireup="True" Codebehind="ExamManageInfo.aspx.cs"
    Inherits="RailExamWebApp.Exam.ExamManageInfo" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>考试信息</title>

    <script type="text/javascript">              
        function AddRecord()
        {
            var examCategoryID=document.getElementById("HfExamCategoryId").value; 
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;   
               
            
            var ret = window.open('ExamManageFirst.aspx?mode=Insert&&ExamCategoryIDPath='+examCategoryID,'ExamManageFirst','Width=800px; Height=600px,status=false,  left='+cleft+',top='+ctop+',   resizable=no',true);
		    if(ret == "true")
		    {
			    form1.Refresh.value = ret;
			    form1.submit();
			    form1.Refresh.value = "";
		    }
        }
      
        function AttendExam(ExamId,PaperId)
        {
            if(PaperId==0)
            {
                alert('该考试还没有选择试卷！');
                return;
            }

            var w=window.open("ExamKS.aspx?ExamId="+ExamId+"&PaperId="+PaperId,"ExamKS","fullscreen=yes,toolbar=no,scrollbars=yes");	
            w.focus();
        }
        
          function OutPutPaper(id)
        {
           if(id==0)
            {
                alert('该考试还没有选择试卷！');
                return;
            }
            
            form1.OutPut.value = id;
	        form1.submit();
	        form1.OutPut.value = "";        
        }
        
            
        function ManagePaper(id)
        {
            if(id==0)
            {
                alert('该考试还没有选择试卷！');
                return;
            }
            
             var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;   
               

            var re= window.open("../Paper/PaperPreview.aspx?id="+id,"PaperPreview"," Width=800px; Height=600px, left="+cleft+",top="+ctop+",status=false,resizable=no,scrollbars=yes",true);		
            re.focus();
        }

        function Grid_onContextMenu(sender, eventArgs)
        {
             var flagUpdate=document.getElementById("HfUpdateRight").value;   
             var flagDelete=document.getElementById("HfDeleteRight").value;   
              var NowOrgID = document.getElementById("hfOrgID").value; 
             var orgid = eventArgs.get_item().getMember('OrgId').get_value(); 
        
                  var item = ContextMenu1.get_items();
                
        	        if(flagUpdate=="True" && orgid==NowOrgID )
                      {  
                       item.getItemByProperty("Text", "新增").set_enabled(true);                        
                       item.getItemByProperty("Text", "编辑").set_enabled(true);
                      }  
                      else
                      {  
                         item.getItemByProperty("Text", "新增").set_enabled(false);                    
                         item.getItemByProperty("Text", "编辑").set_enabled(false);
                      }              
                                
                       if(flagDelete=="True" && orgid ==NowOrgID)
                      {                     
                          item.getItemByProperty("Text", "删除").set_enabled(true);
                      }  
                      else
                      {
                            item.getItemByProperty("Text", "删除").set_enabled(false);
                    }                        
                      
                      
        
            switch(eventArgs.get_item().getMember('ExamName').get_text())
            {
                default:
                    ContextMenu1.showContextMenu(eventArgs.get_event(), eventArgs.get_item());
                    return false; 
                break;
            }
        }
        
        function ContextMenu_onItemSelect(sender, eventArgs)
        {
            var menuItem = eventArgs.get_item();
            var contextDataNode = menuItem.get_parentMenu().get_contextData();
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5;   
                        
            switch(menuItem.get_text())
            {
                case '查看':
                    var ret = window.open('ExamManageFirst.aspx?mode=ReadOnly&id='+contextDataNode.getMember('ExamId').get_value(),'ExamManageFirst','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    ret.focus();
                    break;

                case '编辑':              
                 
         
                    var ret = window.open('ExamManageFirst.aspx?mode=Edit&id='+contextDataNode.getMember('ExamId').get_value(),'ExamManageFirst','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				    if(ret == "true")
				    {
					    form1.Refresh.value = ret;
					    form1.submit();
					    form1.Refresh.value = "";
				    }
                    break;
            
                case '新增':
                    AddRecord(); 
                    break;
                
                case '删除':
                    if(! confirm("您确定要删除“" + contextDataNode.getMember('ExamName').get_text() + "”吗？"))
                    {
                        return false;
                    }
                    form1.DeleteID.value = contextDataNode.getMember('ExamId').get_value();
                    form1.submit();
                    form1.DeleteID.value = "";
                   
                    break;
            } 
            
            return true;
        }
        
         function UpdateData(id1,orgid)
        {
             var   cleft;   
          var   ctop;   
          cleft=(screen.availWidth-800)*.5;   
          ctop=(screen.availHeight-600)*.5; 
          
            var flagUpdate=document.getElementById("HfUpdateRight").value; 
            var NowOrgID = document.getElementById("hfOrgID").value; 
            
            var mode="Edit";
             if(flagUpdate=="False"  || NowOrgID !=orgid)
             {
                   alert("您没有删除该的权限！");
                   return;
             }
             
             
          var ret = window.open('ExamManageFirst.aspx?mode=Edit&id='+id1,'ExamManageFirst','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
		    if(ret == "true")
		    {
			    form1.Refresh.value = ret;
			    form1.submit();
			    form1.Refresh.value = "";
		    }
        
        }        
        
        function DeleteData(id1,id2,orgid)
        {
                var NowOrgID = document.getElementById("hfOrgID").value; 
                 var flagDelete=document.getElementById("HfDeleteRight").value; 
                 if(flagDelete=="False" || NowOrgID !=orgid)
                 {
                   alert("您没有删除该的权限！");
                   return;
                 }
         
                if(! confirm("您确定要删除“" +id1 + "”吗？"))
                {
                    return false;
                }
                form1.DeleteID.value = id2;
                form1.submit();
                form1.DeleteID.value = "";
        
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="gridPage">
            <div id="query" style="display: none;">
                <asp:DropDownList ID="ddlType" runat="server">
                    <asp:ListItem Value="0" Text="-出题方式-"></asp:ListItem>
                    <asp:ListItem Value="1" Text="手工出题"></asp:ListItem>
                    <asp:ListItem Value="2" Text="随机出题"></asp:ListItem>
                </asp:DropDownList>
                考试名称
                <asp:TextBox ID="txtPaperName" runat="server" Width="100px"></asp:TextBox>
                创建者
                <asp:TextBox ID="txtCreatePerson" runat="server" Width="60px"></asp:TextBox>
                <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="确  定" OnClick="btnQuery_Click" />
            </div>
            <div id="grid">
                <ComponentArt:Grid ID="Grid1" runat="server" DataSourceID="odsExam" PageSize="20"
                    Width="96.6%">
                    <ClientEvents>
                        <ContextMenu EventHandler="Grid_onContextMenu" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="ExamId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="ExamId" HeadingText="编号" Visible="false" />
                                <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" Align="Left" />
                                <ComponentArt:GridColumn DataField="CategoryName" HeadingText="考试分类" Visible="false" />
                                <ComponentArt:GridColumn DataField="CategoryId" Visible="false" />
                                <ComponentArt:GridColumn DataField="CreateMode" HeadingText="出题方式" Visible="false" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate2" HeadingText="出题方式" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="有效时间" />
                                <ComponentArt:GridColumn DataField="BeginTime" HeadingText="开始时间" FormatString="yyyy-MM-dd HH:mm"
                                    Visible="false" />
                                <ComponentArt:GridColumn DataField="EndTime" HeadingText="结束时间" FormatString="yyyy-MM-dd HH:mm"
                                    Visible="false" />
                                <ComponentArt:GridColumn DataField="paperId" HeadingText="出题方式" Visible="false" />
                                <ComponentArt:GridColumn DataField="ExamTime" HeadingText="考试时间" />
                                <ComponentArt:GridColumn DataField="OrgId" Visible ="false" />
                                <ComponentArt:GridColumn DataField="StationName" HeadingText="单位" />
                                <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="创建者" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="CTedit" HeadingText="操作" Width="150" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate1" HeadingText="测试"
                                    Visible="false" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTedit">
                            <a onclick="UpdateData(##DataItem.getMember('ExamId').get_value()##,##DataItem.getMember('OrgId').get_value()## )" href="#"><b>编辑</b></a>
                            <a onclick="DeleteData('## DataItem.getMember('ExamName').get_value() ##',## DataItem.getMember('ExamId').get_value() ##,##DataItem.getMember('OrgId').get_value()##)"
                                href="#"><b>删除</b></a> <a onclick="ManagePaper(## DataItem.getMember('paperId').get_value() ##)"
                                    href="#"><b>预览</b></a>&nbsp;<a onclick="OutPutPaper(## DataItem.getMember('paperId').get_value() ##)"
                                        href="#"><b>导出</b></a>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate1">
                            <a onclick="AttendExam('## DataItem.getMember('ExamId').get_value() ##' ,'## DataItem.getMember('paperId').get_value() ##')"
                                href="#"><b>参加考试</b></a>
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
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate2">
                            ## DataItem.getMember("CreateMode").get_value() == 1 ? "手工出题" : "随机出题" ##
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
            </div>
        </div>
        <ComponentArt:Menu ID="ContextMenu1" ContextMenu="Custom" runat="server" EnableViewState="true">
            <ClientEvents>
                <ItemSelect EventHandler="ContextMenu_onItemSelect" />
            </ClientEvents>
            <Items>
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="view_disabled.gif" look-lefticonurl="view.gif"
                    Text="查看" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="new_disabled.gif" look-lefticonurl="new.gif"
                    Text="新增" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="edit_disabled.gif" look-lefticonurl="edit.gif"
                    Text="编辑" />
                <ComponentArt:MenuItem LookId="BreakItem" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="delete_disabled.gif" look-lefticonurl="delete.gif"
                    Text="删除" />
            </Items>
        </ComponentArt:Menu>
        <asp:ObjectDataSource ID="odsExam" runat="server" SelectMethod="GetExamByExamCategoryIDPath"
            TypeName="RailExam.BLL.ExamBLL">
            <SelectParameters>
                <asp:QueryStringParameter Name="ExamCategoryIDPath" QueryStringField="id" Type="String" />
                <asp:ControlParameter ControlID="txtPaperName" Type="String" PropertyName="Text"
                    Name="ExamName" />
                <asp:ControlParameter ControlID="ddlType" Type="Int32" PropertyName="SelectedValue"
                    Name="CreateMode" />
                <asp:ControlParameter ControlID="txtCreatePerson" Type="String" PropertyName="Text"
                    Name="CreatePerson" />
                <asp:SessionParameter SessionField="StationOrgID" Name="OrgId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="HfExamCategoryId" runat="server" />
       <asp:HiddenField  ID="hfOrgID" runat="server"/> 
        <input type="hidden" id="NewButton" onclick="return AddRecord();" />
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <input type="hidden" name="OutPut" />
    </form>
</body>
</html>
