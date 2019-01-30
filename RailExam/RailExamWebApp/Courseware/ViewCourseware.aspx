<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ViewCourseware.aspx.cs"  EnableEventValidation="false" 
    Inherits="RailExamWebApp.Courseware.ViewCourseware" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>查看课件</title>
    <link href="ViewCourseware.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" src="/RailExamBao/Common/js/swfobject.js"></script>
    <script type="text/javascript">
       function ShowAll(id)
      {
	     var re = window.open('/RailExamBao/Courseware/ViewSwfFull.aspx?id=' + id,'ViewSwfFull','top=0,left=0,width='+(window.screen.width-10)+',height='+(window.screen.height-65)+',resizable=no,status=no');
	     re.focus();
      }    
       
      function downCourse() {
      	form1.refresh.value = "true";
      	form1.submit();
      	form1.refresh.value = "";
      }
    </script> 

</head>
<body oncontextmenu='return false' ondragstart='return false' onselectstart='return false'
    oncopy='document.selection.empty()' onbeforecopy='return false'>
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        查看课件</div>
                </div>
                <div id="button">
                </div>
            </div>
            <asp:HiddenField ID="hfUrl" runat="server" />
            <div id="content">
                <table class="viewtable">
                    <tr>
                        <td colspan="3" align="center"  class="viewname">
                            <asp:Label ID="lblName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="viewlabel">
                            知识体系
                        </td>
                        <td class="viewcontent">
                            <asp:Label ID="lblType" runat="server"></asp:Label>
                        </td>
                        <td class="viewplayer" rowspan="7">
                            <% if (_fileType == "flv")
{%>
                            <p id="player1">
                            </p>
                            <script type="text/javascript">
                                    var strUrl = document.getElementById("hfUrl").value;
                                    //alert(strUrl); 
	                                var s1 = new SWFObject("/RailExamBao/Common/player.swf","single","400","320","7");
	                                s1.addParam("allowfullscreen","true");
	                                s1.addVariable("file",strUrl);
	                                s1.addVariable("width","400");
	                                s1.addVariable("height","320");
	                                s1.write("player1");
                            </script>
                           <%
                               }
                               else if (_fileType == "swf")
                               {%> 
                            <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0"
                                        width="410" height="320" >
                                        <param name="movie" value='<%=hfUrl.Value%>' />
                                        <param name="quality" value="high" />
                                        <embed src='<%=hfUrl.Value%>' quality="high" pluginspage="http://www.macromedia.com/go/getflashplayer"
                                            type="application/x-shockwave-flash" width="410" height="320"></embed>
                           </object>
                           <a onclick="ShowAll('<%=Request.QueryString.Get("id")%>');" href="#" >全屏</a>
                           <%}
                             else
                             { 
                               %>
                               <font style="font-size:14px;"><a href="#" onclick="downCourse()" style="cursor: hand;" >点击此处下载课件</a></font>
                            <%} %> 
                        </td>
                    </tr>
                    <tr>
                        <td class="viewlabel">
                            培训类别
                        </td>
                        <td class="viewcontent">
                            <asp:Label ID="lblTrainType" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="viewlabel">
                            编制单位
                        </td>
                        <td class="viewcontent">
                            <asp:Label ID="lblOrg" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="viewlabel">
                            完成时间
                        </td>
                        <td class="viewcontent">
                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="viewlabel">
                            编著者
                        </td>
                        <td class="viewcontent">
                            <asp:Label ID="lblAuthor" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="viewlabel">
                            主审
                        </td>
                        <td class="viewcontent">
                            <asp:Label ID="lblChecker" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="viewlabel">
                            关键字
                        </td>
                        <td class="viewcontent">
                            <asp:Label ID="lblKeyword" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="viewlabel">
                            内容简介</td>
                        <td colspan="2" class="viewcontent" style="height: 130px; vertical-align:top;">
                            <asp:Label ID="lblContent" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="viewlabel">
                            备注</td>
                        <td colspan="3" class="viewcontent" style="height: 100px; vertical-align:top; width: 30%;">
                            <asp:Label ID="lblMemo" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <input type="hidden" name="refresh"/>
    </form>
</body>
</html>
