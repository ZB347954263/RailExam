<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SetDataDictionary.aspx.cs"
    Inherits="RailExamWebApp.RandomExamOther.SetDataDictionary" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>

    <script type="text/javascript">
    function deleteLog(logid)
   {
          var flagDelete=document.getElementById("HfDeleteRight").value;   
                  
	       if(flagDelete=="False")
           {                     
                alert("您没有权限使用该操作！");
                return;
           }  
            if(! confirm("您确定要删除该记录吗？"))
            {
                return;
            }
         gridCallBack.callback(logid,"Delete");
   }  
  function AddRecord()
		{
		    var typeValue=document.getElementById("OrgList").value;
	    	var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            }  
            
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-580)*.5;   
            ctop=(screen.availHeight-220)*.5;    
		    window.open("/RailExamBao/RandomExamOther/SetDataDictionaryDetail.aspx?Mode=Insert&TypeValue="+typeValue,'DataDictionaryDetail','Width=580px; Height=220px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
		} 
		
		function editLog(logid)
		{
		    var typeValue=document.getElementById("OrgList").value;
	    	var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            }  
            
            var   cleft;   
            var   ctop;   
            cleft=(screen.availWidth-580)*.5;   
            ctop=(screen.availHeight-220)*.5;    
		    window.open("/RailExamBao/RandomExamOther/SetDataDictionaryDetail.aspx?Mode=Update&ID="+logid+"&TypeValue="+typeValue,'DataDictionaryDetail','Width=580px; Height=220px,left='+cleft+',top='+ctop+',status=false,resizable=no',true);
		} 
		
		function editUP(logid,orderindex)
		{
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            } 
            
            if(orderindex==0)
           {
                alert('该数据项已经排在第一位！');
                return;
           } 
            gridCallBack.callback(logid,"Up");
		}
		function editUP8(logid,orderindex)
		{
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            } 
            
            if(orderindex==1)
           {
                alert('该数据项已经排在第一位！');
                return;
           } 
            gridCallBack.callback(logid,"Up");
		}
		function editUP9(logid,orderindex)
		{
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            } 
            
            if(orderindex==1)
           {
                alert('该数据项已经排在第一位！');
                return;
           } 
            gridCallBack.callback(logid,"Up");
		}
			
		function editUP10(logid,orderindex)
		{
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            } 
            
            if(orderindex==1)
           {
                alert('该数据项已经排在第一位！');
                return;
           } 
            gridCallBack.callback(logid,"Up");
		}
		function editUP11(logid,orderindex)
		{
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            } 
            
            if(orderindex==1)
           {
                alert('该数据项已经排在第一位！');
                return;
           } 
            gridCallBack.callback(logid,"Up");
		}
		function editUP12(logid,orderindex)
		{
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            } 
            
            if(orderindex==1)
           {
                alert('该数据项已经排在第一位！');
                return;
           } 
            gridCallBack.callback(logid,"Up");
		}
		function editUP13(logid,orderindex)
		{
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            } 
            
            if(orderindex==1)
           {
                alert('该数据项已经排在第一位！');
                return;
           } 
            gridCallBack.callback(logid,"Up");
		}
		
		function editDown1(logid,orderindex)
		{
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            } 
           if(orderindex==Grid1.get_recordCount()-1)
           {
                alert('该数据项已经排在最后一位！');
                return;
           } 
           gridCallBack.callback(logid,"Down");
		}
		
		function editDown2(logid,orderindex)
		{
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            } 
           if(orderindex==Grid2.get_recordCount()-1)
           {
                alert('该数据项已经排在最后一位！');
                return;
           } 
           
//          form1.DownID.value =logid;
//         form1.submit();
//         form1.DownID.value = "";   
         gridCallBack.callback(logid,"Down");
		}
		
		function editDown3(logid,orderindex)
		{
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            } 
           if(orderindex==Grid3.get_recordCount()-1)
           {
                alert('该数据项已经排在最后一位！');
                return;
           } 
           gridCallBack.callback(logid,"Down");
		}
		
		function editDown5(logid,orderindex)
		{
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            } 
           if(orderindex==Grid5.get_recordCount()-1)
           {
                alert('该数据项已经排在最后一位！');
                return;
           } 
           gridCallBack.callback(logid,"Down");
		}
		function editDown8(logid,orderindex)
		{
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            } 
           if(orderindex==Grid8.get_recordCount())
           {
                alert('该数据项已经排在最后一位！');
                return;
           } 
           gridCallBack.callback(logid,"Down");
		}
		function editDown9(logid,orderindex)
		{
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            } 
           if(orderindex==Grid10.get_recordCount())
           {
                alert('该数据项已经排在最后一位！');
                return;
           } 
           gridCallBack.callback(logid,"Down");
		}
				function editDown9(logid,orderindex)
		{
		    var flagUpdate=document.getElementById("HfUpdateRight").value;   
                  
	        if(flagUpdate=="False")
            {                     
                alert("您没有权限使用该操作！");
                return;
            } 
           if(orderindex==Grid10.get_recordCount())
           {
                alert('该数据项已经排在最后一位！');
                return;
           } 
           gridCallBack.callback(logid,"Down");
		}
		
				
		function editDown10(logid,orderindex)
		{
			var flagUpdate = document.getElementById("HfUpdateRight").value;

			if (flagUpdate == "False")
			{
				alert("您没有权限使用该操作！");
				return;
			}
			if (orderindex == Grid11.get_recordCount())
			{
				alert('该数据项已经排在最后一位！');
				return;
			}
			gridCallBack.callback(logid, "Down");
		}

		function editDown11(logid,orderindex)
		{
			var flagUpdate = document.getElementById("HfUpdateRight").value;

			if (flagUpdate == "False")
			{
				alert("您没有权限使用该操作！");
				return;
			}
			if (orderindex == Grid12.get_recordCount())
			{
				alert('该数据项已经排在最后一位！');
				return;
			}
			gridCallBack.callback(logid, "Down");
		}
		function editDown12(logid,orderindex)
		{
			var flagUpdate = document.getElementById("HfUpdateRight").value;

			if (flagUpdate == "False")
			{
				alert("您没有权限使用该操作！");
				return;
			}
			if (orderindex == Grid13.get_recordCount())
			{
				alert('该数据项已经排在最后一位！');
				return;
			}
			gridCallBack.callback(logid, "Down");
		}
		function editDown13(logid,orderindex)
		{
			var flagUpdate = document.getElementById("HfUpdateRight").value;

			if (flagUpdate == "False")
			{
				alert("您没有权限使用该操作！");
				return;
			}
			if (orderindex == Grid14.get_recordCount())
			{
				alert('该数据项已经排在最后一位！');
				return;
			}
			gridCallBack.callback(logid, "Down");
		}
		
		function OrgList_onChange()
		{
		    var tbName=document.getElementById("OrgList").value;
//		   if(tbName=="education_level")
//		   {
//		        Grid1.set_Visible(true);
//		        Grid2.set_Visible(false);
//		        Grid3.set_Visible(false);
//		        Grid4.set_Visible(false);
//		        Grid5.set_Visible(false);
//		        Grid1.filter("");
//		   }
//		   else if(tbName=="political_status")
//		   {
//		        Grid1.set_Visible(false);
//		        Grid2.set_Visible(true);
//		        Grid3.set_Visible(false);
//		        Grid4.set_Visible(false);
//		        Grid5.set_Visible(false);
//		        Grid2.filter("");
//		   }
//		    else if(tbName=="workgroupleader_level")
//		   {
//		        Grid1.set_Visible(false);
//		        Grid2.set_Visible(false);
//		        Grid3.set_Visible(true);
//		        Grid4.set_Visible(false);
//		        Grid5.set_Visible(false);
//		        Grid3.filter("");
//		   }
//		    else if(tbName=="technician_type")
//		   {
//		        Grid1.set_Visible(false);
//		        Grid2.set_Visible(false);
//		        Grid3.set_Visible(false);
//		        Grid4.set_Visible(true);
//		        Grid5.set_Visible(false);
//		        Grid4.filter("");
//		   }
//		    else if(tbName=="technician_title_type ")
//		   {
//		        Grid1.set_Visible(false);
//		        Grid2.set_Visible(false);
//		        Grid3.set_Visible(false);
//		        Grid4.set_Visible(false);
//		        Grid5.set_Visible(true);
//		        Grid5.filter("");
//		   }
            gridCallBack.callback("");
		}
		
		function gridCallBack_Complete()
		{
		    var message = document.getElementById("hfMessage").value;
		    if(message != "")
		    {
		        alert(message);
		    }
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
                        系统管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        数据字典</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                    <img id="Button1" onclick="AddRecord();" src="../Common/Image/add.gif" alt="" />
                </div>
            </div>
            <div id="content">
                <div id="left">
                    <div id="leftHead">
                        数据字典</div>
                    <div id="leftContent">
                        <asp:ListBox ID="OrgList" runat="server" Width="100%" Height="100%" AutoPostBack="false">
                            <asp:ListItem Value="education_level">文化程度</asp:ListItem>
                            <asp:ListItem Value="political_status">政治面貌</asp:ListItem>
                            <asp:ListItem Value="workgroupleader_level">班组长类别</asp:ListItem>
                            <asp:ListItem Value="technician_type">职业技能等级</asp:ListItem>
                            <asp:ListItem Value="technician_title_type">专业技术职称</asp:ListItem>
                            <asp:ListItem Value="education_employee_type">职教人员类型</asp:ListItem>
                            <asp:ListItem Value="committee_head_ship">职教委员会职务</asp:ListItem>
                            <asp:ListItem Value="random_exam_modular_type">模块考试类别</asp:ListItem>
                            <asp:ListItem Value="trainplan_type">培训类别</asp:ListItem>
                            <asp:ListItem Value="safe_level">安全等级</asp:ListItem>
                            <asp:ListItem Value="certificate">其他证书</asp:ListItem>
                            <asp:ListItem Value="certificate_level">证书等级</asp:ListItem>
                            <asp:ListItem Value="certificate_unit">发证单位</asp:ListItem>
                            <asp:ListItem Value="train_unit">培训单位</asp:ListItem>
                        </asp:ListBox>
                    </div>
                </div>
                <div id="right">
                    <div id="rightHead">
                        数据字典</div>
                    <div id="rightContent">
                        <ComponentArt:CallBack ID="gridCallBack" runat="server" PostState="true" OnCallback="gridCallBack_Callback"
                            Debug="false">
                            <Content>
                                <ComponentArt:Grid ID="Grid1" runat="server" PageSize="18" Width="97%" Visible="false"
                                    OnNeedRebind="Grid1_NeedRebind">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="EducationLevelID">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="EducationLevelID" Visible="False" />
                                                <ComponentArt:GridColumn DataField="OrderIndex" Visible="false" />
                                                <ComponentArt:GridColumn DataField="EducationLevelName" HeadingText="数据项" />
                                                <ComponentArt:GridColumn DataField="Memo" HeadingText="备注" />
                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit3"
                                                    HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit3">
                                            <a onclick="deleteLog(##DataItem.getMember('EducationLevelID').get_value()##);" href="#">
                                                <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a> <a onclick="editLog(##DataItem.getMember('EducationLevelID').get_value()##);"
                                                    href="#">
                                                    <img alt="编缉" border="0" src="../Common/Image/edit_col_edit.gif" /></a>
                                            <a onclick="editUP(##DataItem.getMember('EducationLevelID').get_value()##,##DataItem.getMember('OrderIndex').get_value()##);"
                                                href="#">
                                                <img id="imgUP" alt="上移" border="0" src="../App_Themes/Default/Images/Menu/move_up.gif" /></a>
                                            <a onclick="editDown1(##DataItem.getMember('EducationLevelID').get_value()##,##DataItem.getMember('OrderIndex').get_value()##);"
                                                href="#">
                                                <img id="imgDown" alt="下移" border="0" src="../App_Themes/Default/Images/Menu/move_down.gif" /></a>
                                        </ComponentArt:ClientTemplate>
                                    </ClientTemplates>
                                </ComponentArt:Grid>
                                <ComponentArt:Grid ID="Grid2" runat="server" PageSize="18" Width="97%" Visible="false"
                                    OnNeedRebind="Grid2_NeedRebind">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="PoliticalStatusID">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="PoliticalStatusID" Visible="False" />
                                                <ComponentArt:GridColumn DataField="OrderIndex" Visible="false" />
                                                <ComponentArt:GridColumn DataField="PoliticalStatusName" HeadingText="数据项" />
                                                <ComponentArt:GridColumn DataField="Memo" HeadingText="备注" />
                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit5"
                                                    HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit5">
                                            <a onclick="deleteLog(##DataItem.getMember('PoliticalStatusID').get_value()##);"
                                                href="#">
                                                <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a> <a onclick="editLog(##DataItem.getMember('PoliticalStatusID').get_value()##);"
                                                    href="#">
                                                    <img alt="编缉" border="0" src="../Common/Image/edit_col_edit.gif" /></a>
                                            <a onclick="editUP(##DataItem.getMember('PoliticalStatusID').get_value()##,##DataItem.getMember('OrderIndex').get_value()##);"
                                                href="#">
                                                <img id="img1" alt="上移" border="0" src="../App_Themes/Default/Images/Menu/move_up.gif" /></a>
                                            <a onclick="editDown2(##DataItem.getMember('PoliticalStatusID').get_value()##,##DataItem.getMember('OrderIndex').get_value()##);"
                                                href="#">
                                                <img id="img2" alt="下移" border="0" src="../App_Themes/Default/Images/Menu/move_down.gif" /></a>
                                        </ComponentArt:ClientTemplate>
                                    </ClientTemplates>
                                </ComponentArt:Grid>
                                <ComponentArt:Grid ID="Grid3" runat="server" PageSize="18" Width="97%" Visible="false"
                                    OnNeedRebind="Grid3_NeedRebind">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="WorkGroupLeaderLevelID">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="WorkGroupLeaderLevelID" Visible="False" />
                                                <ComponentArt:GridColumn DataField="OrderIndex" Visible="false" />
                                                <ComponentArt:GridColumn DataField="LevelName" HeadingText="数据项" />
                                                <ComponentArt:GridColumn DataField="Memo" HeadingText="备注" />
                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit4"
                                                    HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit4">
                                            <a onclick="deleteLog(##DataItem.getMember('WorkGroupLeaderLevelID').get_value()##);"
                                                href="#">
                                                <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a> <a onclick="editLog(##DataItem.getMember('WorkGroupLeaderLevelID').get_value()##);"
                                                    href="#">
                                                    <img alt="编缉" border="0" src="../Common/Image/edit_col_edit.gif" /></a>
                                            <a onclick="editUP(##DataItem.getMember('WorkGroupLeaderLevelID').get_value()##,##DataItem.getMember('OrderIndex').get_value()##);"
                                                href="#">
                                                <img id="img3" alt="上移" border="0" src="../App_Themes/Default/Images/Menu/move_up.gif" /></a>
                                            <a onclick="editDown3(##DataItem.getMember('WorkGroupLeaderLevelID').get_value()##,##DataItem.getMember('OrderIndex').get_value()##);"
                                                href="#">
                                                <img id="img4" alt="下移" border="0" src="../App_Themes/Default/Images/Menu/move_down.gif" /></a>
                                        </ComponentArt:ClientTemplate>
                                    </ClientTemplates>
                                </ComponentArt:Grid>
                                <ComponentArt:Grid ID="Grid4" runat="server" PageSize="18" Width="97%" OnNeedRebind="Grid4_NeedRebind">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="TechnicianTypeID">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="TechnicianTypeID" Visible="False" />
                                                <ComponentArt:GridColumn DataField="TypeName" HeadingText="数据项" />
                                                <ComponentArt:GridColumn DataField="Memo" HeadingText="备注" />
                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit1"
                                                    HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit1">
                                            <a onclick="deleteLog(##DataItem.getMember('TechnicianTypeID').get_value()##);" href="#">
                                                <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a> <a onclick="editLog(##DataItem.getMember('TechnicianTypeID').get_value()##);"
                                                    href="#">
                                                    <img alt="编缉" border="0" src="../Common/Image/edit_col_edit.gif" /></a>
                                        </ComponentArt:ClientTemplate>
                                    </ClientTemplates>
                                </ComponentArt:Grid>
                                <ComponentArt:Grid ID="Grid5" runat="server" PageSize="18" Width="97%" Visible="false"
                                    OnNeedRebind="Grid5_NeedRebind">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="TechnicianTitleTypeID">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="TechnicianTitleTypeID" Visible="False" />
                                                <ComponentArt:GridColumn DataField="OrderIndex" Visible="false" />
                                                <ComponentArt:GridColumn DataField="TypeName" HeadingText="数据项" />
                                                <ComponentArt:GridColumn DataField="TypeLevelName" HeadingText="数据项类别" />
                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit2"
                                                    HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit2">
                                            <a onclick="deleteLog(##DataItem.getMember('TechnicianTitleTypeID').get_value()##);"
                                                href="#">
                                                <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a> <a onclick="editLog(##DataItem.getMember('TechnicianTitleTypeID').get_value()##);"
                                                    href="#">
                                                    <img alt="编缉" border="0" src="../Common/Image/edit_col_edit.gif" /></a>
                                            <a onclick="editUP(##DataItem.getMember('TechnicianTitleTypeID').get_value()##,##DataItem.getMember('OrderIndex').get_value()##);"
                                                href="#">
                                                <img id="img5" alt="上移" border="0" src="../App_Themes/Default/Images/Menu/move_up.gif" /></a>
                                            <a onclick="editDown5(##DataItem.getMember('TechnicianTitleTypeID').get_value()##,##DataItem.getMember('OrderIndex').get_value()##);"
                                                href="#">
                                                <img id="img6" alt="下移" border="0" src="../App_Themes/Default/Images/Menu/move_down.gif" /></a>
                                        </ComponentArt:ClientTemplate>
                                    </ClientTemplates>
                                </ComponentArt:Grid>
                                <ComponentArt:Grid ID="Grid6" runat="server" PageSize="18" Width="97%" Visible="false"
                                    OnNeedRebind="Grid6_NeedRebind">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="EducationEmployeeTypeID">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="EducationEmployeeTypeID" Visible="False" />
                                                <ComponentArt:GridColumn DataField="TypeName" HeadingText="数据项" />
                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit6"
                                                    HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit6">
                                            <a onclick="deleteLog(##DataItem.getMember('EducationEmployeeTypeID').get_value()##)"
                                                href="#">
                                                <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a> <a onclick="editLog(##DataItem.getMember('EducationEmployeeTypeID').get_value()##)"
                                                    href="#">
                                                    <img alt="编缉" border="0" src="../Common/Image/edit_col_edit.gif" /></a>
                                        </ComponentArt:ClientTemplate>
                                    </ClientTemplates>
                                </ComponentArt:Grid>
                                <ComponentArt:Grid ID="Grid7" runat="server" PageSize="18" Width="97%" Visible="false"
                                    OnNeedRebind="Grid7_NeedRebind">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="CommitteeHeadShipID">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="CommitteeHeadShipID" Visible="False" />
                                                <ComponentArt:GridColumn DataField="CommitteeHeadShipName" HeadingText="数据项" />
                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit7"
                                                    HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit7">
                                            <a onclick="deleteLog(##DataItem.getMember('CommitteeHeadShipID').get_value()##)"
                                                href="#">
                                                <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a> <a onclick="editLog(##DataItem.getMember('CommitteeHeadShipID').get_value()##)"
                                                    href="#">
                                                    <img alt="编缉" border="0" src="../Common/Image/edit_col_edit.gif" /></a>
                                        </ComponentArt:ClientTemplate>
                                    </ClientTemplates>
                                </ComponentArt:Grid>
                                <ComponentArt:Grid ID="Grid8" runat="server" PageSize="18" Width="97%" Visible="false"
                                    OnNeedRebind="Grid8_NeedRebind">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="TechnicianTitleTypeID">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="RandomExamModularTypeID" Visible="False" />
                                                <ComponentArt:GridColumn DataField="LevelNum" Visible="false" />
                                                <ComponentArt:GridColumn DataField="RandomExamModularTypeName" HeadingText="数据项" />
                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit8"
                                                    HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit8">
                                            <a onclick="deleteLog(##DataItem.getMember('RandomExamModularTypeID').get_value()##);"
                                                href="#">
                                                <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a> <a onclick="editLog(##DataItem.getMember('RandomExamModularTypeID').get_value()##);"
                                                    href="#">
                                                    <img alt="编缉" border="0" src="../Common/Image/edit_col_edit.gif" /></a>
                                            <a onclick="editUP8(##DataItem.getMember('RandomExamModularTypeID').get_value()##,##DataItem.getMember('LevelNum').get_value()##);"
                                                href="#">
                                                <img id="img7" alt="上移" border="0" src="../App_Themes/Default/Images/Menu/move_up.gif" /></a>
                                            <a onclick="editDown8(##DataItem.getMember('RandomExamModularTypeID').get_value()##,##DataItem.getMember('LevelNum').get_value()##);"
                                                href="#">
                                                <img id="img8" alt="下移" border="0" src="../App_Themes/Default/Images/Menu/move_down.gif" /></a>
                                        </ComponentArt:ClientTemplate>
                                    </ClientTemplates>
                                </ComponentArt:Grid>
                                <ComponentArt:Grid ID="Grid9" runat="server" PageSize="18" Width="97%" Visible="false"
                                    OnNeedRebind="Grid9_NeedRebind">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="trainplan_type_id">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="trainplan_type_id" Visible="False" />
                                                <ComponentArt:GridColumn DataField="trainplan_type_name" HeadingText="数据项" />
                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit9"
                                                    HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit9">
                                            <a onclick="deleteLog(##DataItem.getMember('trainplan_type_id').get_value()##);"
                                                href="#">
                                                <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a> <a onclick="editLog(##DataItem.getMember('trainplan_type_id').get_value()##);"
                                                    href="#">
                                                    <img alt="编缉" border="0" src="../Common/Image/edit_col_edit.gif" /></a>
                                        </ComponentArt:ClientTemplate>
                                    </ClientTemplates>
                                </ComponentArt:Grid>
                                <ComponentArt:Grid ID="Grid10" runat="server" PageSize="18" Width="97%" Visible="false"
                                    OnNeedRebind="Grid10_NeedRebind">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="safe_level_id">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="safe_level_id" Visible="False" />
                                                <ComponentArt:GridColumn DataField="safe_level_name" HeadingText="数据项" />
                                                <ComponentArt:GridColumn DataField="order_index" Visible="false" />
                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit10"
                                                    HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit10">
                                            <a onclick="deleteLog(##DataItem.getMember('safe_level_id').get_value()##);" href="#">
                                                <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a> <a onclick="editLog(##DataItem.getMember('safe_level_id').get_value()##);"
                                                    href="#">
                                                    <img alt="编缉" border="0" src="../Common/Image/edit_col_edit.gif" /></a>
                                            <a onclick="editUP9(##DataItem.getMember('safe_level_id').get_value()##,##DataItem.getMember('order_index').get_value()##);"
                                                href="#">
                                                <img id="img9" alt="上移" border="0" src="../App_Themes/Default/Images/Menu/move_up.gif" /></a>
                                            <a onclick="editDown9(##DataItem.getMember('safe_level_id').get_value()##,##DataItem.getMember('order_index').get_value()##);"
                                                href="#">
                                                <img id="img10" alt="下移" border="0" src="../App_Themes/Default/Images/Menu/move_down.gif" /></a>
                                        </ComponentArt:ClientTemplate>
                                    </ClientTemplates>
                                </ComponentArt:Grid>
                                <ComponentArt:Grid ID="Grid11" runat="server" PageSize="18" Width="97%" Visible="false"
                                    OnNeedRebind="Grid11_NeedRebind">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="CERTIFICATE_ID">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="CERTIFICATE_ID" Visible="False" />
                                                <ComponentArt:GridColumn DataField="CERTIFICATE_NAME" HeadingText="数据项" />
                                                <ComponentArt:GridColumn DataField="order_index" Visible="false" />
                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit11"
                                                    HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit11">
                                            <a onclick="deleteLog(##DataItem.getMember('CERTIFICATE_ID').get_value()##);" href="#">
                                                <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a> <a onclick="editLog(##DataItem.getMember('CERTIFICATE_ID').get_value()##);"
                                                    href="#">
                                                    <img alt="编缉" border="0" src="../Common/Image/edit_col_edit.gif" /></a>
                                            <a onclick="editUP10(##DataItem.getMember('CERTIFICATE_ID').get_value()##,##DataItem.getMember('order_index').get_value()##);"
                                                href="#">
                                                <img id="img11" alt="上移" border="0" src="../App_Themes/Default/Images/Menu/move_up.gif" /></a>
                                            <a onclick="editDown10(##DataItem.getMember('CERTIFICATE_ID').get_value()##,##DataItem.getMember('order_index').get_value()##);"
                                                href="#">
                                                <img id="img12" alt="下移" border="0" src="../App_Themes/Default/Images/Menu/move_down.gif" /></a>
                                        </ComponentArt:ClientTemplate>
                                    </ClientTemplates>
                                </ComponentArt:Grid>
                                <ComponentArt:Grid ID="Grid12" runat="server" PageSize="18" Width="97%" Visible="false"
                                    OnNeedRebind="Grid12_NeedRebind">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="certificate_level_id">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="certificate_level_id" Visible="False" />
                                                <ComponentArt:GridColumn DataField="CERTIFICATE_NAME" HeadingText="证书类别" />
                                                <ComponentArt:GridColumn DataField="certificate_level_name" HeadingText="数据项" />
                                                <ComponentArt:GridColumn DataField="order_index" Visible="false" />
                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit12"
                                                    HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit12">
                                            <a onclick="deleteLog(##DataItem.getMember('certificate_level_id').get_value()##);" href="#">
                                                <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a> <a onclick="editLog(##DataItem.getMember('certificate_level_id').get_value()##);"
                                                    href="#">
                                                    <img alt="编缉" border="0" src="../Common/Image/edit_col_edit.gif" /></a>
                                            <a onclick="editUP11(##DataItem.getMember('certificate_level_id').get_value()##,##DataItem.getMember('order_index').get_value()##);"
                                                href="#">
                                                <img id="img13" alt="上移" border="0" src="../App_Themes/Default/Images/Menu/move_up.gif" /></a>
                                            <a onclick="editDown11(##DataItem.getMember('certificate_level_id').get_value()##,##DataItem.getMember('order_index').get_value()##);"
                                                href="#">
                                                <img id="img14" alt="下移" border="0" src="../App_Themes/Default/Images/Menu/move_down.gif" /></a>
                                        </ComponentArt:ClientTemplate>
                                    </ClientTemplates>
                                </ComponentArt:Grid>
                                <ComponentArt:Grid ID="Grid13" runat="server" OnNeedRebind="Grid13_NeedRebind" PageSize="18"
                                    Visible="false" Width="97%">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="certificate_unit_id">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="certificate_unit_id" Visible="False" />
                                                <ComponentArt:GridColumn DataField="certificate_unit_name" HeadingText="数据项" />
                                                <ComponentArt:GridColumn DataField="order_index" Visible="false" />
                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit13"
                                                    HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit13">
                                            <a href="#" onclick="deleteLog(##DataItem.getMember('certificate_unit_id').get_value()##);">
                                                <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a> <a href="#"
                                                    onclick="editLog(##DataItem.getMember('certificate_unit_id').get_value()##);">
                                                    <img alt="编缉" border="0" src="../Common/Image/edit_col_edit.gif" /></a>
                                            <a href="#" onclick="editUP12(##DataItem.getMember('certificate_unit_id').get_value()##,##DataItem.getMember('order_index').get_value()##);">
                                                <img id="img15" alt="上移" border="0" src="../App_Themes/Default/Images/Menu/move_up.gif" /></a>
                                            <a href="#" onclick="editDown12(##DataItem.getMember('certificate_unit_id').get_value()##,##DataItem.getMember('order_index').get_value()##);">
                                                <img id="img16" alt="下移" border="0" src="../App_Themes/Default/Images/Menu/move_down.gif" /></a>
                                        </ComponentArt:ClientTemplate>
                                    </ClientTemplates>
                                </ComponentArt:Grid>
                                <ComponentArt:Grid ID="Grid14" runat="server" OnNeedRebind="Grid14_NeedRebind" PageSize="18"
                                    Visible="false" Width="97%">
                                    <Levels>
                                        <ComponentArt:GridLevel DataKeyField="train_unit_id">
                                            <Columns>
                                                <ComponentArt:GridColumn DataField="train_unit_id" Visible="False" />
                                                <ComponentArt:GridColumn DataField="train_unit_name" HeadingText="数据项" />
                                                <ComponentArt:GridColumn DataField="order_index" Visible="false" />
                                                <ComponentArt:GridColumn AllowSorting="false" DataCellClientTemplateId="CTEdit14"
                                                    HeadingText="操作" />
                                            </Columns>
                                        </ComponentArt:GridLevel>
                                    </Levels>
                                    <ClientTemplates>
                                        <ComponentArt:ClientTemplate ID="CTEdit14">
                                            <a href="#" onclick="deleteLog(##DataItem.getMember('train_unit_id').get_value()##);">
                                                <img alt="删除" border="0" src="../Common/Image/edit_col_delete.gif" /></a> <a href="#"
                                                    onclick="editLog(##DataItem.getMember('train_unit_id').get_value()##);">
                                                    <img alt="编缉" border="0" src="../Common/Image/edit_col_edit.gif" /></a>
                                            <a href="#" onclick="editUP13(##DataItem.getMember('train_unit_id').get_value()##,##DataItem.getMember('order_index').get_value()##);">
                                                <img id="img17" alt="上移" border="0" src="../App_Themes/Default/Images/Menu/move_up.gif" /></a>
                                            <a href="#" onclick="editDown13(##DataItem.getMember('train_unit_id').get_value()##,##DataItem.getMember('order_index').get_value()##);">
                                                <img id="img18" alt="下移" border="0" src="../App_Themes/Default/Images/Menu/move_down.gif" /></a>
                                        </ComponentArt:ClientTemplate>
                                    </ClientTemplates>
                                </ComponentArt:Grid>
                                <asp:HiddenField ID="hfMessage" runat="server" Value="" />
                            </Content>
                            <ClientEvents>
                                <CallbackComplete EventHandler="gridCallBack_Complete" />
                            </ClientEvents>
                        </ComponentArt:CallBack>
                    </div>
                </div>
            </div>
        </div>
        <input type="hidden" name="LogID" />
        <input type="hidden" name="Refresh" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <input type="hidden" name="UpID" />
        <input type="hidden" name="DownID" />
        &nbsp;
    </form>
</body>
</html>
