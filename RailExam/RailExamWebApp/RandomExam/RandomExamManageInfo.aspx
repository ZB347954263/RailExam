<%@ Import Namespace="RailExamWebApp.Common.Class" %>

<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamManageInfo.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamManageInfo" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考试信息</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">              
        function AddRecord()
        {
            var examCategoryID=document.getElementById("HfExamCategoryId").value; 
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-800)*.5;   
            ctop=(screen.availHeight-600)*.5; 
            var isWuhan = document.form1.isWuhan.value;
            var ret;
            if(isWuhan == "True")
            {
                ret = window.open('RandomExamManageFirst.aspx?startmode=Insert&mode=Insert&&ExamCategoryIDPath='+examCategoryID,'ExamManageFirst','Width=1000px; Height=800px,status=true,  left='+cleft+',top='+ctop+',   resizable=no',true);
                ret.focus();
            }
            else
            {
//                ret = window.open('/RailExamBao/RandomExamOther/RandomExamBasicInfo.aspx?startmode=Insert&mode=Insert&&ExamCategoryIDPath='+examCategoryID,'ExamManageFirst','Width=800px; Height=600px,status=false,  left='+cleft+',top='+ctop+',   resizable=no',true);
                  ret = showCommonDialog('/RailExamBao/RandomExamOther/RandomExamBasicInfo.aspx?startmode=Insert&mode=Insert&&ExamCategoryIDPath='+examCategoryID,'dialogWidth:1000px;dialogHeight:800px');
		        if(ret == "true")
		        {
			        form1.Refresh.value = ret;
			        form1.submit();
			        form1.Refresh.value = "";
		        }
		    }
        }  
                    
        function ManageExam(id)
        {     
           var re= window.open("../RandomExam/RandomExamPreview.aspx?id="+id,"RandomExamPreview","fullscreen=yes,toolbar=no,scrollbars=yes");		
           re.focus();
        }
        
        function OutPutExam(id,orgid)
        {
        	 var flagUpdate=document.getElementById("HfUpdateRight").value;   
        	if(flagUpdate=="False")
            {
                  alert("您没有该操作的权限！");
                  return;
            }
        	
            //var ret = showCommonDialog("/RailExamBao/RandomExam/OutputPaperAllNew.aspx?eid="+id+"&OrgID="+ orgid +"&Mode=blank",'dialogWidth:320px;dialogHeight:30px;');
            var ret = window.showModalDialog("/RailExamBao/RandomExam/OutputPaperAllNew.aspx?eid="+id+"&OrgID="+ orgid +"&Mode=blank", 
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:40px;scroll:no;'); 
        	if(ret != "")
            {
               form1.OutPut.value =ret;
               form1.submit();
            }              
        }

        function Grid_onContextMenu(sender, eventArgs)
        {
             var flagUpdate=document.getElementById("HfUpdateRight").value;   
             var flagDelete=document.getElementById("HfDeleteRight").value;   
             var NowOrgID = document.getElementById("hfOrgID").value; 
             var orgid = eventArgs.get_item().getMember('OrgId').get_value(); 
            var flagIsAdmin = document.getElementById("hfIsAdmin").value; 
             var item = ContextMenu1.get_items();
                
        	        if(flagUpdate=="True" && orgid==NowOrgID )
                      {  
                       item.getItemByProperty("Text", "新增").set_enabled(true);                        
                       item.getItemByProperty("Text", "编辑").set_enabled(true);
                       item.getItemByProperty("Text", "复制新增").set_enabled(true);
                       item.getItemByProperty("Text", "生成补考考试").set_enabled(true);    
                      }  
                      else
                      {  
                         item.getItemByProperty("Text", "新增").set_enabled(false);                    
                         item.getItemByProperty("Text", "编辑").set_enabled(false);
                         item.getItemByProperty("Text", "复制新增").set_enabled(false);
                         item.getItemByProperty("Text", "生成补考考试").set_enabled(false); 
                      }              
                                
                       if(flagDelete=="True" && orgid ==NowOrgID)
                      {                     
                          item.getItemByProperty("Text", "删除").set_enabled(true);
                      }  
                      else
                      {
                            item.getItemByProperty("Text", "删除").set_enabled(false);
                       }                        
                      
               if(flagIsAdmin == "True")
               {
                       item.getItemByProperty("Text", "新增").set_enabled(true);                        
                       item.getItemByProperty("Text", "编辑").set_enabled(true);
                       item.getItemByProperty("Text", "删除").set_enabled(true);
                       item.getItemByProperty("Text", "复制新增").set_enabled(true);
                       item.getItemByProperty("Text", "生成补考考试").set_enabled(true);                
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
            var isWuhan = document.form1.isWuhan.value;
    
            switch(menuItem.get_text())
            {
                case '查看':
                    if(isWuhan == "True")
                    {
                       var ret = window.open('RandomExamManageFirst.aspx?startmode=ReadOnly&mode=ReadOnly&id='+contextDataNode.getMember('RandomExamId').get_value(),'RandomExamManageFirst','Width=1000px; Height=800px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
				       ret.focus();
                    }
                    else
                    {
//                      var ret = window.open('/RailExamBao/RandomExamOther/RandomExamBasicInfo.aspx?startmode=ReadOnly&mode=ReadOnly&id='+contextDataNode.getMember('RandomExamId').get_value(),'RandomExamManageFirst','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
                        var ret = showCommonDialog('/RailExamBao/RandomExamOther/RandomExamBasicInfo.aspx?startmode=ReadOnly&mode=ReadOnly&id='+contextDataNode.getMember('RandomExamId').get_value(),'dialogWidth:1000px;dialogHeight:800px');
	                    if(ret == "true")
	                    {
		                    form1.Refresh.value = ret;
		                    form1.submit();
		                    form1.Refresh.value = "";
	                    }                   
	                }     
                    break;
                
               case '复制新增': 
                    //var ret = showCommonDialog("/RailExamBao/RandomExam/SetCopyRandomExamName.aspx?examID="+contextDataNode.getMember('RandomExamId').get_value(),'dialogWidth:320px;dialogHeight:30px;');
                    var ret = window.showModalDialog("/RailExamBao/RandomExam/SetCopyRandomExamName.aspx?examID="+contextDataNode.getMember('RandomExamId').get_value(), 
                        '', 'help:no; status:no; dialogWidth:320px;dialogHeight:40px;scroll:no;'); 
                   if(ret == "true" )
                    {
                       form1.Refresh.value =ret;
                       form1.submit();
                    }
                    break;
               case '生成补考考试':
               	   var ret = window.showModalDialog("/RailExamBao/RandomExam/ResetRandomExam.aspx?examID="+contextDataNode.getMember('RandomExamId').get_value(), 
                        '', 'help:no; status:no; dialogWidth:320px;dialogHeight:40px;scroll:no;'); 
                	if(ret == "true" )
                    {
                       form1.ResetID.value =ret;
                       form1.submit();
                    }
               	    break;
               	case '安排微机教室':
               		
               		var flagUpdate=document.getElementById("HfUpdateRight").value; 
                    if(flagUpdate=="False")
                    {
                          alert("您没有该操作的权限！");
                          return;
                    }
               		
               		form1.arrangeID.value = contextDataNode.getMember('RandomExamId').get_value();
                    form1.submit();
                    form1.arrangeID.value = "";      		
               		break;
                case '查看服务器状态':
   	                window.showModalDialog("/RailExamBao/RandomExam/ShowComputerServer.aspx?id="+contextDataNode.getMember('RandomExamId').get_value(),window,'dialogWidth:800px;dialogHeight:600px;');
                	break;
                case '编辑': 
//                    if(isWuhan == "True")
//                    {
//                        var ret = window.open('RandomExamManageFirst.aspx?startmode=Edit&mode=Edit&id='+contextDataNode.getMember('RandomExamId').get_value(),'RandomExamManageFirst','Width=1000px; Height=800px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//                        ret.focus();
//                    }
//                    else
//                    {
////                      var ret = window.open('/RailExamBao/RandomExamOther/RandomExamBasicInfo.aspx?startmode=Edit&mode=Edit&id='+contextDataNode.getMember('RandomExamId').get_value(),'RandomExamManageFirst','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
//                        var ret = showCommonDialog('/RailExamBao/RandomExamOther/RandomExamBasicInfo.aspx?startmode=Edit&mode=Edit&id='+contextDataNode.getMember('RandomExamId').get_value(),'dialogWidth:1000px;dialogHeight:800px');
//                        if(ret == "true")
//		                {
//			                form1.Refresh.value = ret;
//			                form1.submit();
//			                form1.Refresh.value = "";
//		                }                    
//		            }
                	UpdateData(contextDataNode.getMember('RandomExamId').get_value(),contextDataNode.getMember('OrgId').get_value());
                    break;
            
                case '新增':
                    AddRecord(); 
                    break;
                
                case '删除':
                    var flagIsAdmin = document.getElementById("hfIsAdmin").value;
                    var style = contextDataNode.getMember('ExamStyle').get_text();
                    var paper = contextDataNode.getMember('HasPaper').get_text();
                     if(flagIsAdmin == "False")
                    {
                        if(style == "2" && paper == "true")
                        {
                            alert("该考试为正式考试，并已生成试卷，不能删除！");
                            return false; 
                        }
                    } 
                    if(! confirm("删除考试，将会删除该考试的所有成绩以及答卷等信息，您确定要删除“" + contextDataNode.getMember('ExamName').get_text() + "”吗？"))
                    {
                        return false;
                    }
                    form1.DeleteID.value = contextDataNode.getMember('RandomExamId').get_value();
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
          var isWuhan = document.form1.isWuhan.value;
            var flagIsAdmin = document.getElementById("hfIsAdmin").value;
            var NowOrgID = document.getElementById("hfOrgID").value; 
            var mode="Edit";
            
             var flagUpdate=document.getElementById("HfUpdateRight").value; 
            if(flagUpdate=="False"  || NowOrgID !=orgid)
            {
                  alert("您没有该操作的权限！");
                  return;
            }
             
            if(isWuhan == "True")
            {
                var ret = window.open('RandomExamManageFirst.aspx?startmode=Edit&mode=Edit&id='+id1,'RandomExamManageFirst','Width=1000px; Height=800px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
                ret.focus();
            }
            else
            {
//              var ret = window.open('/RailExamBao/RandomExamOther/RandomExamBasicInfo.aspx?startmode=Edit&mode=Edit&id='+id1,'RandomExamManageFirst','Width=800px; Height=600px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
                var ret = showCommonDialog('/RailExamBao/RandomExamOther/RandomExamBasicInfo.aspx?startmode=Edit&mode=Edit&id='+id1,'dialogWidth:1000px;dialogHeight:800px');
                if(ret == "true")
                {
	                form1.Refresh.value = ret;
	                form1.submit();
	                form1.Refresh.value = "";
                }              
            }  
        } 
         
        function showArrange(id) 
        {
           var ret = showCommonDialog('/RailExamBao/RandomExam/SelectRandomExamArrange.aspx?id='+id,'dialogWidth:800px;dialogHeight:600px');
        }
        
        function DeleteData(rowId,id2,orgid,style,paper)
        {
            var NowOrgID = document.getElementById("hfOrgID").value; 
             var flagDelete=document.getElementById("HfDeleteRight").value; 
             var flagIsAdmin = document.getElementById("hfIsAdmin").value;

            if(flagIsAdmin == "False")
            {
                 if(flagDelete=="False" || NowOrgID !=orgid)
                 {
                   alert("您没有删除该的权限！");
                   return false;
                 }
            }
           
            if(flagIsAdmin == "False")
            {
                if(style == "2" && paper == "true")
                {
                    alert("该考试为正式考试，并已生成试卷，不能删除！");
                    return false; 
                }
            }
            
            if(! confirm("删除考试，将会删除该考试的所有成绩以及答卷等信息，您确定要删除“" + Grid1.getItemFromClientId(rowId).getMember("ExamName").get_text() + "”吗？"))
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
            <div id="grid">
                <div id="query" style="display: none; height: 60px;">
                    <table>
                        <tr>
                            <td colspan="2">
                                考试名称
                                <asp:TextBox ID="txtPaperName" runat="server" Width="100px"></asp:TextBox>
                                创建者
                                <asp:TextBox ID="txtCreatePerson" runat="server" Width="60px"></asp:TextBox>&nbsp;&nbsp;
                                考试类型
                                <asp:DropDownList ID="ddlStyle" runat="server">
                                    <asp:ListItem Text="--请选择--" Selected="true" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="不存档考试" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="存档考试" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%;">
                                <asp:Label ID="lblOrg" runat="server" Text="站段单位"></asp:Label>
                                <asp:DropDownList ID="ddlOrg" runat="server">
                                </asp:DropDownList>
                                有效期
                                <asp:DropDownList ID="ddl" runat="server">
                                    <asp:ListItem Text="未过期考试" Selected="true" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="已过期考试" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="全部考试" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:Button ID="btnQuery" runat="server" CssClass="buttonSearch" Text="确  定" OnClick="btnQuery_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <ComponentArt:Grid ID="Grid1" runat="server" DataSourceID="odsExam" PageSize="18"
                    Width="100%">
                    <ClientEvents>
                        <ContextMenu EventHandler="Grid_onContextMenu" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel DataKeyField="RandomExamId">
                            <Columns>
                                <ComponentArt:GridColumn DataField="RandomExamId" HeadingText="编号" Visible="false" />
                                <ComponentArt:GridColumn DataField="OrgId" HeadingText="编号" Visible="false" />
                                <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" Align="Left" Width="150" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="ClientTemplate3" HeadingText="有效时间"
                                    Width="160" />
                                <ComponentArt:GridColumn DataField="BeginTime" HeadingText="开始时间" FormatString="yyyy-MM-dd HH:mm"
                                    Visible="false" />
                                <ComponentArt:GridColumn DataField="EndTime" HeadingText="结束时间" FormatString="yyyy-MM-dd HH:mm"
                                    Visible="false" />
                                <ComponentArt:GridColumn DataField="ExamTime" HeadingText="考试时间" Width="50" />
                                <ComponentArt:GridColumn DataField="StationName" HeadingText="单位" Width="130" />
                                <ComponentArt:GridColumn DataField="ExamStyle" Visible="false" HeadingText="考试类型" />
                                <ComponentArt:GridColumn DataField="ExamStyleName" HeadingText="考试类型" Width="60" />
                                <ComponentArt:GridColumn DataField="HasPaper" HeadingText="生成试卷" ColumnType="CheckBox"
                                    Width="50" />
                                <ComponentArt:GridColumn DataField="IsComputerExam" HeadingText="机考" ColumnType="CheckBox"
                                    Width="40" />
                                <ComponentArt:GridColumn DataField="CreatePerson" HeadingText="创建者" Width="50" />
                                <ComponentArt:GridColumn DataCellClientTemplateId="CTedit" HeadingText="操作" Width="150"
                                    Align="Center" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="CTedit">
                            <a onclick="UpdateData(##DataItem.getMember('RandomExamId').get_value()## ,##DataItem.getMember('OrgId').get_value()## )"
                                href="#"><b>编辑</b></a> <a onclick="DeleteData('##DataItem.ClientId##',## DataItem.getMember('RandomExamId').get_value() ##,##DataItem.getMember('OrgId').get_value()##,'## DataItem.getMember('ExamStyle').get_value() ##','## DataItem.getMember('HasPaper').get_value() ##' )"
                                    href="#"><b>删除</b></a> <a onclick="ManageExam(## DataItem.getMember('RandomExamId').get_value() ## )"
                                        href="#"><b>预览</b></a> <a onclick="OutPutExam(## DataItem.getMember('RandomExamId').get_value() ##,## DataItem.getMember('OrgId').get_value() ## )"
                                            href="#"><b>导出</b></a>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="ClientTemplate3">
                            ## DataItem.getMember("BeginTime").get_value().getYear()##-##DataItem.getMember("BeginTime").get_value().getMonth()+1##-##DataItem.getMember("BeginTime").get_value().getDate()##
                            <%--                            ##DataItem.getMember("BeginTime").get_value().getHours()##:##DataItem.getMember("BeginTime").get_value().getMinutes()##--%>
                            / ## DataItem.getMember("EndTime").get_value().getYear()##-##DataItem.getMember("EndTime").get_value().getMonth()+1##-##DataItem.getMember("EndTime").get_value().getDate()##
                            <%--                            ##DataItem.getMember("EndTime").get_value().getHours()##:##DataItem.getMember("EndTime").get_value().getMinutes()##
--%>
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
                <ComponentArt:MenuItem LookId="BreakItem" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="new_disabled.gif" look-lefticonurl="new.gif"
                    Text="复制新增" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="new_disabled.gif" look-lefticonurl="new.gif"
                    Text="生成补考考试" />
                <ComponentArt:MenuItem DisabledLook-LeftIconUrl="new_disabled.gif" look-lefticonurl="new.gif"
                    Text="安排微机教室" />
                 <ComponentArt:MenuItem DisabledLook-LeftIconUrl="new_disabled.gif" look-lefticonurl="new.gif"
                    Text="查看服务器状态" />
            </Items>
        </ComponentArt:Menu>
        <asp:ObjectDataSource ID="odsExam" runat="server" SelectMethod="GetExamByExamCategoryIDPath"
            TypeName="RailExam.BLL.RandomExamBLL">
            <SelectParameters>
                <asp:QueryStringParameter Name="ExamCategoryIDPath" QueryStringField="id" Type="String" />
                <asp:ControlParameter ControlID="txtPaperName" Type="String" PropertyName="Text"
                    Name="ExamName" />
                <asp:ControlParameter ControlID="txtCreatePerson" Type="String" PropertyName="Text"
                    Name="CreatePerson" />
                <asp:ControlParameter ControlID="ddlOrg" Type="Int32" PropertyName="SelectedValue"
                    Name="OrgId" />
                <%--                <asp:SessionParameter SessionField="StationOrgID" Name="OrgId" Type="Int32" />
--%>
                <asp:ControlParameter ControlID="ddl" Type="Int32" PropertyName="SelectedValue" Name="ExamTimeType"
                    DefaultValue="0" />
                <asp:ControlParameter ControlID="ddlStyle" Type="Int32" PropertyName="SelectedValue"
                    Name="ExamStyleID" DefaultValue="0" />
                  <asp:ControlParameter ControlID="hfRailSystemID" Type="Int32" PropertyName="Value"
                    Name="railSystemID" DefaultValue="0" /> 
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="HfExamCategoryId" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <asp:HiddenField ID="hfRailSystemID" runat="server" />
        <input type="hidden" id="NewButton" onclick="return AddRecord();" />
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <input type="hidden" name="OutPut" />
        <input type="hidden" name="ResetID" />
        <input type="hidden" name="arrangeID" />
        <asp:HiddenField ID="hfIsAdmin" runat="server" />
        <input type="hidden" name="isWuhan" value='<%=PrjPub.IsWuhan() %>' />
    </form>
</body>
</html>
