<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" Codebehind="ComputerApplyTab_one.aspx.cs"
    Inherits="RailExamWebApp.RandomExamTai.ComputerApplyTab_one" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script type="text/javascript" language="javascript">
    function ReadOnly(id)
    {
        window.showModalDialog("ComputerApplyDetail.aspx?ID="+id+"&&mode=ReadOnlyOne","ComputerApplyDetail","status:false;dialogWidth:645px;dialogHeight:400px");
    }
    function Edit(id)
    {
    	 var flagUpdate=document.getElementById("HfUpdateRight").value; 
                      
        if(flagUpdate=="False")
         {
            alert("您没有权限使用该操作！");                       
            return;
         } 
    	
        var ret = window.showModalDialog("ComputerApplyDetail.aspx?ID="+id+"&&mode=EditOne&math="+Math.random(),"ComputerApplyDetail","status:false;dialogWidth:645px;dialogHeight:400px");
    	if(ret != "" && ret!=undefined) {
    		form1.Refresh.value = ret;
    		form1.submit();
    		form1.Refresh.value = "";
    	}
    }
    
            
    function AddApply(){
        var   cleft;   
        var   ctop;   
        cleft=(screen.availWidth-580)*.5;   
        ctop=(screen.availHeight-250)*.5;        
        var flagUpdate=document.getElementById("HfUpdateRight").value; 
                      
        if(flagUpdate=="False")
         {
            alert("您没有权限使用该操作！");                       
            return;
         } 
         var ret = window.showModalDialog("ComputerApplyDetail.aspx?&&mode=Insert","ComputerApplyDetail","status:false;dialogWidth:645px;dialogHeight:400px");   
    	
    	if(ret != "") {
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
            <div>
                <div id="btn1" style="text-align: right">
                   <input type="button" class="button" value="新 增"  onclick="AddApply()"/>
                </div>
                <asp:Button ID="btnDelete" runat="server" Text="删除信息" CssClass="displayNone" OnClick="btnDelete_Click" />
            </div>
            <ComponentArt:Grid ID="grdEntity1" runat="server" PageSize="20" Width="98%" RunningMode="Callback">
                <Levels>
                    <ComponentArt:GridLevel DataKeyField="COMPUTER_ROOM_APPLY_ID">
                        <Columns>
                            <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                            <ComponentArt:GridColumn DataField="COMPUTER_ROOM_APPLY_ID" HeadingText="微机教室申请"
                                Visible="false" />
                            <ComponentArt:GridColumn DataField="short_name" HeadingText="站段名" />
                            <ComponentArt:GridColumn DataField="apply_short_name" HeadingText="被申请站段名" />
                            <ComponentArt:GridColumn DataField="COMPUTER_ROOM_NAME" HeadingText="被申请教室名" />
                            <ComponentArt:GridColumn DataField="APPLY_START_TIME" HeadingText="开始时间" Align="Left" />
                            <ComponentArt:GridColumn DataField="APPLY_END_TIME" HeadingText="结束时间职名" Align="Left" />
                            <ComponentArt:GridColumn DataField="APPLY_COMPUTER_NUMBER" HeadingText="申请机位" Align="Left" />
                            <ComponentArt:GridColumn DataField="REPLY_STATUS_NAME" HeadingText="申请状态" Align="Left" />
                            <ComponentArt:GridColumn DataField="REPLY_DATE" HeadingText="回复时间" Align="Left" />
                        </Columns>
                    </ComponentArt:GridLevel>
                </Levels>
                <ClientTemplates>
                    <ComponentArt:ClientTemplate ID="CTEdit">
                        <a href="#"
                          onclick="Edit(##DataItem.getMember('COMPUTER_ROOM_APPLY_ID').get_value()##)"  class="underline">
                            <img src="../Common/Image/edit_col_edit.gif" style="border: 0" alt="修改" /></a>
                        <a href="javascript:ReadOnly(##DataItem.getMember('COMPUTER_ROOM_APPLY_ID').get_value()##)"
                            class="underline" style="display: none">【预览】</a> <a onclick="javascript:if(document.getElementById('HfDeleteRight').value=='False'){alert('您没有该操作的权限！');return;}if(!confirm('您确定要删除此该信息吗？')){return;}__doPostBack('btnDelete','## DataItem.getMember('COMPUTER_ROOM_APPLY_ID').get_value() ##');"
                                title="删除信息" href="#">
                                <img src="../Common/Image/edit_col_delete.gif" style="border: 0" alt="删除" /></a>
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
