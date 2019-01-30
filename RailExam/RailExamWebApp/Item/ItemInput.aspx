<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ItemInput.aspx.cs" Inherits="RailExamWebApp.Item.ItemInput" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
        function selectChapter()
        {
            var chapterId =document.getElementById('HfChapterId').value;
            if(chapterId == "")
            {
                chapterId = "0";
            }
	        var selectedChapter = window.showModalDialog('/RailExamBao/Item/SelectBookChapterForItemInput.aspx?chapterId='+chapterId, 
                '', 'help:no; status:no; dialogWidth:370px;dialogHeight:680px');
            
            if(! selectedChapter)
            {
                return;
            }
            //alert(selectedChapter);
            document.getElementById('HfBookId').value =selectedChapter.split('|')[0];
            document.getElementById('HfChapterId').value = selectedChapter.split('|')[1];
            document.getElementById('txtChapterName').value = selectedChapter.split('|')[2];
            document.getElementById('HfRangeType').value = selectedChapter.split('|')[3];
            document.getElementById('HfRangeName').value = selectedChapter.split('|')[2];
        }
        
        function delConfirm()
        {
            if(! confirm("您确定要删除该章节下全部试题吗？"))
            {
                return false;
            }
            return true;
        }
        
//        function inputConfirm()
//        {
//            var filePath = escape(document.getElementById("File1").value);
//            if( filePath== "")
//            {
//                alert("请浏览选择Excel文件！");
//               return false; 
//            }
//        
//            if(document.getElementById("ddlMode"))
//            {
//              if(document.getElementById("ddlMode").value == "2")
//              {
//                 if(! confirm("只导入试题答案需保证带答案的Excel文件顺序与试题录入顺序一致！\r\n                    您确定只导入试题答案吗？"))
//                {
//                    return false;
//                }
//              }
//              return true; 
//            }
//            else
//            {
//               return true;
//          }
//        }
        
        function showProgressBar() {
            var mode = "0";
            var bookID = document.getElementById("HfBookId").value;
            var chapterID = document.getElementById("HfChapterId").value;
            
            if(document.getElementById("ddlMode"))
            {
              mode = document.getElementById("ddlMode").value;
              if(document.getElementById("ddlMode").value == "2")
              {
                 if(! confirm("只导入试题答案需保证带答案的Excel文件顺序与试题录入顺序一致！\r\n                    您确定只导入试题答案吗？"))
                {
                    return;
                }
              }
            }
            
            //var ret = showCommonDialog("/RailExamBao/Item/ItemUpload.aspx?Mode=" + mode+"&BookID="+bookID+"&ChapterID="+chapterID, 'help:no; status:no; dialogWidth:320px;dialogHeight:30px');       
        	var ret = window.showModalDialog("/RailExamBao/Item/ItemUpload.aspx?Mode=" + mode+"&BookID="+bookID+"&ChapterID="+chapterID,
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
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="parent">
                        试题管理</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        试题导入</div>
                </div>
                <div style="text-align: left; color: #2D67CF;">
                    <asp:Label ID="lblTitle" runat="server" Text="教材章节："></asp:Label><asp:Label ID="txtChapterName"
                        runat="server">
                    </asp:Label>
                </div>
            </div>
            <div id="content" style="text-align: center">
                <div style="text-align: center; color: #2D67CF;">
                    <table>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddlMode" runat="server">
                                    <asp:ListItem Value="0">导入全部内容</asp:ListItem>
                                    <asp:ListItem Value="1">不导入标准答案</asp:ListItem>
                                    <asp:ListItem Value="2">只导入标准答案</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <input name="btnInput"   type="button" class="button" value="导入试题" onclick="showProgressBar()" />&nbsp;&nbsp;
                                <asp:Button ID="btnDelAll" runat="server" CssClass="buttonEnableLong" Text="删除全部试题"
                                    OnClientClick="return delConfirm();" OnClick="btnDelAll_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="gird">
                    <ComponentArt:Grid ID="Grid1" runat="server" PageSize="20" Width="840">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="id">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="id" HeadingText="序号" Width="50" />
                                    <ComponentArt:GridColumn DataField="ItemContent" HeadingText="试题内容" Width="200" />
                                    <ComponentArt:GridColumn DataField="ItemType" HeadingText="试题类型" Width="50" />
                                    <ComponentArt:GridColumn DataField="AnswerCount" HeadingText="选项数目" Width="50" />
                                    <ComponentArt:GridColumn DataField="OverDate" HeadingText="过期时间" Width="80" Visible="false" />
                                    <ComponentArt:GridColumn DataField="Answer" HeadingText="正确答案" Width="50" />
                                    <ComponentArt:GridColumn DataField="ErrorReason" HeadingText="原因" Width="300" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="HfChapterId" runat="server" />
        <asp:HiddenField ID="HfBookId" runat="server" />
        <asp:HiddenField ID="HfRangeType" runat="server" />
        <asp:HiddenField ID="HfRangeName" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
        <input type="hidden" name="Refresh" />
    </form>
</body>
</html>
