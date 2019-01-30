<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" Codebehind="ComputerApplyTab_two.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.ComputerApplyTab_two" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript" language="javascript">
    function Edit(id)
    {
    	var flagUpdate=document.getElementById("HfUpdateRight").value; 
                      
        if(flagUpdate=="False")
         {
            alert("您没有权限使用该操作！");                       
            return;
         } 
    	
        var ret = window.showModalDialog("ComputerApplyDetail.aspx?ID="+id+"&&mode=EditTwo&math="+Math.random(),"ComputerApplyDetail","status:false;dialogWidth:645px;dialogHeight:400px");
    	if(ret != "" && ret!=undefined) {
    		form1.Refresh.value = ret;
    		form1.submit();
    		form1.Refresh.value = "";
    	}
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="margin-top:20px;">
                <asp:Button ID="btnDelete" runat="server" Text="删除信息" CssClass="displayNone" OnClick="btnDelete_Click" />
            </div>
            <ComponentArt:Grid ID="grdEntity2" runat="server" PageSize="20" Width="98%" RunningMode="Callback">
                <Levels>
                    <ComponentArt:GridLevel DataKeyField="COMPUTER_ROOM_APPLY_ID">
                        <Columns>
                            <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                            <ComponentArt:GridColumn DataField="COMPUTER_ROOM_APPLY_ID" HeadingText="微机教室申请"
                                Visible="false" />
                            <ComponentArt:GridColumn DataField="short_name" HeadingText="站段名" />
                            <ComponentArt:GridColumn DataField="apply_short_name" HeadingText="被申请站段名" />
                            <ComponentArt:GridColumn DataField="COMPUTER_ROOM_NAME" HeadingText="被申请教室名" />
                            <ComponentArt:GridColumn DataField="COMPUTER_ROOM_NAME" HeadingText="教室名" />
                            <ComponentArt:GridColumn DataField="APPLY_START_TIME" HeadingText="开始时间" Align="Left" />
                            <ComponentArt:GridColumn DataField="APPLY_END_TIME" HeadingText="结束时间职名" Align="Left" />
                            <ComponentArt:GridColumn DataField="APPLY_COMPUTER_NUMBER" HeadingText="申请机位" Align="Left" />
                            <ComponentArt:GridColumn DataField="Reply_Status_Name" HeadingText="申请状态" Align="Left" />
                            <ComponentArt:GridColumn DataField="REPLY_DATE" HeadingText="回复时间" Align="Left" />
                        </Columns>
                    </ComponentArt:GridLevel>
                </Levels>
                <ClientTemplates>
                    <ComponentArt:ClientTemplate ID="CTEdit">
                        <a href="#"
                         onclick="Edit(## DataItem.getMember('COMPUTER_ROOM_APPLY_ID').get_value()##)"   class="underline">
                            <img src="../Common/Image/edit_col_edit.gif" style="border: 0" alt="回复" /></a> 
                    </ComponentArt:ClientTemplate>
                </ClientTemplates>
            </ComponentArt:Grid>
        </div>
        <input type="hidden" name="DeleteID" />
        <input type="hidden" name="Refresh" />
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
    </form>
</body>
</html>
