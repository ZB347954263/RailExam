<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ImportEmployee.aspx.cs"
    Inherits="RailExamWebApp.Systems.ImportEmployee" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>导入Excel</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

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
       
       function Valid()
       {
           if(form1.txtOrg.value == "")
          {
            alert('请先选择单位！');
            return false;
          } 
         return true;
       }
       
       function ValidImport()
       {
           if(form1.txtOrg.value == "")
          {
            alert('请先选择单位！');
            return false;
          } 
          
            if(! confirm("当前导入不会删除该站段原有员工信息，只是添加Excel表中员工信息！您确定要导入吗？"))
             {
                return false; 
            }
         return true;
       }
       
       function showProgressBarUpload()
       {
         if(form1.txtOrg.value == "")
          {
            alert('单位不能为空！');
            return;
          } 
          
          var ret = window.showModalDialog("/RailExamBao/Systems/ImportUpload.aspx?OrgID="+document.getElementById("hfOrg").value,
                        '', 'help:no; status:no; dialogWidth:350px;dialogHeight:50px;scroll:no;');
       	
          //var ret = showCommonDialog("/RailExamBao/Systems/ImportUpload.aspx?OrgID="+document.getElementById("hfOrg").value, 'help:no; status:no; dialogWidth:320px;dialogHeight:30px');

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
      
      function showProgressBar(filename,orgid) {
            var ret = window.showModalDialog("/RailExamBao/Systems/ImportExcel.aspx?FileName=" + escape(filename)+"&OrgID="+orgid,
                        '', 'help:no; status:no; dialogWidth:350px;dialogHeight:50px;scroll:no;');
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
        
      function showProgressBarExam(filename,orgid) {
            var ret = showCommonDialog("/RailExamBao/Systems/ImportExcel.aspx?FileName=" + escape(filename)+"&OrgID="+orgid+"&mode=exam", 'help:no; status:no; dialogWidth:320px;dialogHeight:30px');
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
                        员工导入</div>
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
                            站&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;段：<asp:TextBox ID="txtOrg" runat="server" ReadOnly="true"
                                Width="230px">
                            </asp:TextBox>
                            <img id="ImgSelectOrg" style="cursor: hand;" name="ImgSelectOrg" onclick="selectOrganization();"
                                src="../Common/Image/search.gif" alt="选择组织机构" border="0" runat="server" />
                        </td>
                    </tr>
                    <tr runat="server" id="excel">
                        <td align="left">
                            Excel文件：<asp:FileUpload ID="File1" runat="server" Width="251px" />
                        </td>
                    </tr>
                </table>
                <asp:Button ID="btnExam" runat="server" CssClass="button" Text="检查数据" OnClientClick="return Valid();"
                    OnClick="btnExam_Click" Visible="false" />&nbsp;&nbsp;
                <asp:Button ID="btnExamSelect" runat="server" CssClass="buttonEnableLong" Text="检查Excel工作证号"
                    OnClientClick="return Valid();" OnClick="btnExamSelect_Click" Visible="false" />&nbsp;&nbsp;
                <asp:Button ID="btnModify" runat="server" CssClass="button" Text="确定导入" OnClick="btnModify_Click" />
                <asp:Button ID="btnExamSelectAll" runat="server" CssClass="buttonEnableLong" Text="检查全局工作证号"
                    OnClientClick="return Valid();" OnClick="btnExamSelectAll_Click" Visible="false" />&nbsp;&nbsp;
                <asp:Button ID="btnImport" runat="server" CssClass="button" Text="添加导入" OnClientClick="return ValidImport();"
                    OnClick="btnImport_Click" Visible="false" />&nbsp;&nbsp;
                <asp:Button ID="btnImportModify" runat="server" CssClass="button" Text="修改导入" Visible="false"
                    OnClick="btnImportModify_Click" />
                <asp:Button ID="btnBind" runat="server" CssClass="buttonEnableLong" OnClientClick="return Valid();" Text="查询上次导入结果"
                    OnClick="btnBind_Click" />&nbsp;&nbsp;
                <input type="button" class="button" value="上传文件" onclick="showProgressBarUpload();" />&nbsp;&nbsp;
                <asp:Button ID="btnExamOther" runat="server" CssClass="button" Text="检查数据" OnClick="btnExamOther_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnInput" runat="server" CssClass="button" Text="导   入" OnClick="btnInput_Click" />&nbsp;&nbsp;
                <br />
                <br />
                <asp:Label ID="lbltitle" runat="server" Text="未导入的员工名单"></asp:Label><br />
                <div id="gird">
                    <ComponentArt:Grid ID="Grid1" runat="server" PageSize="20" Width="97%">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="ExcelNo">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="ExcelNo" HeadingText="顺号" />
                                    <ComponentArt:GridColumn DataField="WorkNo" HeadingText="上岗证号" />
                                    <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="姓名" />
                                    <ComponentArt:GridColumn DataField="Sex" HeadingText="性别" />
                                    <ComponentArt:GridColumn DataField="OrgPath" HeadingText="组织机构" />
                                    <ComponentArt:GridColumn DataField="PostPath" HeadingText="工作岗位" />
                                    <ComponentArt:GridColumn DataField="ErrorReason" HeadingText="原因" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                    </ComponentArt:Grid>
                </div>
                <asp:Button ID="btnExcel" runat="server" Text="导出Excel" CssClass="button" OnClick="btnExcel_Click" />
            </div>
        </div>
        <asp:HiddenField ID="hfOrg" runat="server" />
        <asp:HiddenField ID="hfOrgName" runat="server" />
        <input type="hidden" name="Refresh" />
    </form>
</body>
</html>
