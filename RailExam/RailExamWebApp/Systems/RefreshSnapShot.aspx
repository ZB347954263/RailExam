<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RefreshSnapShot.aspx.cs"
    Inherits="RailExamWebApp.Systems.RefreshSnapShot" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>

    <script type="text/javascript">
            function SelectBook()
          {
                var selectedBook = window.showModalDialog("/RailExamBao/Common/SelectStrategyChapter.aspx?flag=3", 
                '', 'help:no; status:no; dialogWidth:350px;dialogHeight:660px');
                
                if(! selectedBook)
                {
                    return false;
                }
                    //alert(selectedBook); 
                    var selectedChapter=selectedBook.split('$');
                    var strBookID ="";
                    var strBookName = "";
                    var strType = "";
                    var strTypeName = "";
                     for(var i=0;i<selectedChapter.length;i++)
                    {
                        if(i==0)
                       {
                            strBookID = strBookID  +selectedChapter[i].split('|')[1];
                            strBookName= strBookName + selectedChapter[i].split('|')[2];
                            strType= strType+selectedChapter[i].split('|')[3];
                            strTypeName = strTypeName  + selectedChapter[i].split('|')[2];  
                      }
                       else
                       {
                             strBookID = strBookID + "/" +selectedChapter[i].split('|')[1];
                            strBookName= strBookName +","+ selectedChapter[i].split('|')[2];
                            strType= strType+"/"+selectedChapter[i].split('|')[3];
                            strTypeName = strTypeName + "," + selectedChapter[i].split('|')[2];  
                      }   
                    } 
                     document.getElementById('hfBookID').value=strBookID;
//                  document.getElementById('txtBookName').value  = strBookName;
                     document.getElementById('hfRangeType').value = strType;
                     document.getElementById('hfRangeName').value = strTypeName; 
                    
                    if(selectedBook!="")
                    {
                    form1.Refresh.value = "true";
                    form1.submit();
                    form1.Refresh.value = ""; 
                    }
        } 
            
       function downloadData() {
          var ret = window.showModalDialog("RefreshSelectType.aspx",
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:40px;scroll:no;');

       }
       
       function createData() {
       	  if(!confirm("初始化数据库将会先删除基础数据然后重新下载！您确定要初始化数据库吗？")) {
       	  	return;
       	  }
          var ret = window.showModalDialog("RefreshDownLoad.aspx?type=createData",
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:40px;scroll:no;');

       }
       
       function downloadItem() {
          var ret = window.showModalDialog("RefreshDownLoad.aspx?type=downloadItem",
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:40px;scroll:no;');

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
                        下载数据</div>
                </div>
                <div id="welcomeInfo">
                    <%=RailExamWebApp.Common.Class.PrjPub.WelcomeInfo%>
                </div>
            </div>
            <div id="content">
                <div id="mainContent">
                    <table style="height: 90%">
                        <tr>
                            <td valign="middle">
                                <asp:Label ID="lbl" runat="server"></asp:Label>
                                <br />
                                <input type="button" name="btnData" value="初始化数据库" class="buttonLong" onclick="createData();" />
                                <br />
                                <br />
                                <br />
                                <input type="button" name="btnDownData" value="下载最新数据" class="buttonLong" onclick="downloadData();" /><br />
                                <br />
                                <br />
                                <input type="button" name="btnDownItem" value="下载试题图片" class="buttonLong" onclick="downloadItem();" /><br />
                                <input type="button" name="btnDownBook" value="下载最新教材" style="display: none;" class="buttonLong"
                                    onclick="SelectBook();" /><br />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfBookID" runat="server" />
        <asp:HiddenField ID="hfRangeType" runat="server" />
        <asp:HiddenField ID="hfRangeName" runat="server" />
        <input type="hidden" name="Refresh" />
    </form>
</body>
</html>
