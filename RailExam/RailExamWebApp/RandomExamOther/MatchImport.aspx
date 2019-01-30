<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MatchImport.aspx.cs" Inherits="RailExamWebApp.RandomExamOther.MatchImport" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>导入Excel</title>
    <script type="text/javascript" src="/RailExamBao/Common/JS/Common.js"></script>

    <script type="text/javascript">
          function selectOrganization()
        {
            var selectedOrganization = window.showModalDialog('../Common/SelectOrganization.aspx?Type=Station', 
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:600px;scroll:no;');

            if(! selectedOrganization)
            {
                return;
            }
            
            document.getElementById("hfOrg").value = selectedOrganization.split('|')[0];
            document.getElementById("hfOrgName").value = selectedOrganization.split('|')[1];
            document.getElementById("txtOrg").value = selectedOrganization.split('|')[1];
        }
      
      function showProgressBar(filename,orgid) {
            var ret = showCommonDialog("/RailExamBao/Systems/ImportExcel.aspx?FileName=" + escape(filename)+"&OrgID="+orgid+"&ImportType=2", 'help:no; status:no; dialogWidth:320px;dialogHeight:30px');
            if(ret)
            {
                if (ret.indexOf("refresh")>=0) {
                   alert(ret.split('|')[1]); 
                   form1.Refresh.value = 'refresh';
                   form1.submit();
                   form1.Refresh.value = '';
                }
                else
                {
                    alert(ret);
                }
            }
        } 
        
        function showProgressBarUpload()
       {
         if(form1.txtOrg.value == "")
          {
            alert('单位不能为空！');
            return;
          } 
          
          
         var ret = showCommonDialog("/RailExamBao/RandomExamOther/MatchImportUpload.aspx?OrgID="+document.getElementById("hfOrg").value, 'help:no; status:no; dialogWidth:320px;dialogHeight:30px');

            if(ret)
            {
                if (ret.indexOf("refresh")>=0) {
                   alert(ret.split('|')[1]);
                   form1.Refresh.value = 'refresh';
                   form1.submit();
                   form1.Refresh.value = '';
                }
                else
                {
                    alert(ret);
                }
            }
          
       }
        
        function addButton(item)
        {
            var value = "";

            if (item.getMember('OperateMode').get_text() == "1")
            {
                value = "<a onclick='addGrid(\"" + item.getMember('EmployeeErrorID').get_text() + "\");' href='#'><img alt='新增' border='0'  src='../Common/Image/edit_col_save.gif' /></a>";
            }
            else if(item.getMember('OperateMode').get_text() == "2")
            {
                value = "<a onclick='editGrid(\"" + item.getMember('EmployeeErrorID').get_text() + "\");'  href='#'> <img alt='修改' border='0'  src='../Common/Image/edit_col_edit.gif' /></a>";
            }

            return value;
        }
        
        function addGrid(id)
        {
            if(!confirm("您确定要新增该员工信息吗？"))
            {
                return;
            }
            
            form1.add.value = id;
            form1.submit();
        }
        
         function editGrid(id)
        {
            if(!confirm("您确定要修改该员工信息吗？"))
            {
                return;
            }
            
            form1.update.value = id;
            form1.submit();
        }
        
        function valid()
        {
          if(form1.txtOrg.value == "")
          {
            alert('请先选择单位！');
            return false;
          } 
          
          return true;
        }
      
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div  id="page">
                   <div id="head">
                <div id="location">
                    <div id="desktop" onclick="window.location='../Main/Desktop.aspx'"></div><div id="parent">
                        系统管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        员工匹配导入</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
                <div id="button">
                </div>
            </div> 
            <div id="content" style="text-align: center">
                <table>
                    <tr>
                        <td align="left">
                            站&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;段：<asp:TextBox ID="txtOrg" runat="server" ReadOnly="true" Width="230px">
                            </asp:TextBox>
                            <img id="ImgSelectOrg" style="cursor: hand;" name="ImgSelectOrg" onclick="selectOrganization();"
                                src="../Common/Image/search.gif" alt="选择组织机构" border="0" runat="server"/>
                        </td>
                    </tr>
                    <tr style="display:none">
                        <td align="left">
                            Excel文件：<asp:FileUpload ID="File1" runat="server" Width="251px" />
                        </td>
                    </tr>
                </table>
               <asp:Button ID="btnBind" runat="server" OnClientClick="return valid();" CssClass="buttonEnableLong" Text="查询上次匹配结果" OnClick="btnBind_Click"  />&nbsp;&nbsp;
               <input type="button" class="button" value="上传文档" onclick="showProgressBarUpload();" />&nbsp;&nbsp;
               <asp:Button ID="btnInput" runat="server" CssClass="button" Text="匹配导入" Visible="false"  OnClick="btnInput_Click" />&nbsp;&nbsp;
                <br />
                <br />
                <asp:Label ID="lbltitle" runat="server" Text="核对需要导入的员工信息"></asp:Label><br />
                <div style="overflow: auto; width: 100%; height: 450px;">
                    <ComponentArt:Grid ID="Grid1" runat="server" PageSize="20">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="EmployeeErrorID">
                                <Columns>
                                    <ComponentArt:GridColumn AllowSorting="false" HeadingText="操作" DataCellClientTemplateId="EditTemplate"
                                    EditControlType="EditCommand" Width="50" FixedWidth="true" />
                                    <ComponentArt:GridColumn DataField="ExcelNo" HeadingText="顺号" Width="40" />
                                    <ComponentArt:GridColumn DataField="WorkNo" HeadingText="工资编号" Width="120" />
                                    <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="姓名" Width="80" />
                                    <ComponentArt:GridColumn DataField="Sex" HeadingText="性别" Width="50" />
                                    <ComponentArt:GridColumn DataField="OrgPath" HeadingText="组织机构" Width="100" />
                                    <ComponentArt:GridColumn DataField="PostPath" HeadingText="工作岗位" Width="100" />
                                    <ComponentArt:GridColumn DataField="ErrorReason" HeadingText="核对结果"  Width="500"/>
                                    <ComponentArt:GridColumn DataField="OperateMode" Visible="false" />
                                     <ComponentArt:GridColumn DataField="EmployeeErrorID" Visible="false" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="EditTemplate">
                            ## addButton(DataItem) ##
                        </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfOrg" runat="server" />
        <asp:HiddenField ID="hfOrgName" runat="server" />
        <input type="hidden" name="Refresh"/>
        <input type="hidden" name= "add" />
        <input type="hidden" name="update" />
    </form>
</body>
</html>
