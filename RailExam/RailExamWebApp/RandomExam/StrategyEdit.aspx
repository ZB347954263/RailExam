<%@ Page Language="C#" AutoEventWireup="true" Codebehind="StrategyEdit.aspx.cs" Inherits="RailExamWebApp.RandomExam.StrategyEdit"
    EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>组卷策略大题详细信息</title>

    <script type="text/javascript">
        function selectChapter()
        {
        	var search = window.location.search;
        	if(search.indexOf("&id=")>=0)
        	{
        		if(!confirm("更换教材章节，需要重新筛选题目，你确定要更换教材章节吗？")) {
        			return;
        		}
        	}
        	
        	var examId = document.getElementById("hfExamID").value;
            var itemTypeID = document.getElementById("hfItemType").value;
	        var selectedChapter = window.showModalDialog('../Common/SelectStrategyChapter.aspx?RandomExamID='+examId+'&itemTypeID='+itemTypeID, 
                '', 'help:no; status:no; dialogWidth:370px;dialogHeight:680px');
            
            if(! selectedChapter)
            {
                return;
            }

        	if(selectedChapter.split('|')[3] != 3 && selectedChapter.split('|')[3] != 4)
            {   
                alert('请选择教材或者章节！');
                return; 
            }
            
        	
        	if(document.getElementById('HfChapterId').value != selectedChapter.split('|')[1]) {
        		document.getElementById('HfExCludeChaptersId').value = "";
        		document.getElementById('txtExCludeChapters').value = "";
        	}
        	else 
        	{
        		return;
        	}

           // document.getElementById('HfBookId').value =selectedChapter.split('|')[0];
            document.getElementById('HfChapterId').value = selectedChapter.split('|')[1];
            document.getElementById('txtChapterName').value = selectedChapter.split('|')[2];
            document.getElementById('HfRangeType').value = selectedChapter.split('|')[3];
            document.getElementById('HfRangeName').value = selectedChapter.split('|')[2];

        	__doPostBack("btnSelectChapter");
        }
   
        function selectChapterS()
        {
            var RangeType = document.getElementById('HfRangeType').value;

            if(RangeType != "3")
            {   
                alert('请先选择一本教材！');
                return; 
            }
            
            var BookID = document.getElementById('HfChapterId').value;
             var itemTypeID = document.getElementById("hfItemType").value;
            var selectedChapter = window.showModalDialog('../Common/MultiSelectBook.aspx?itemTypeID='+ itemTypeID +'&BookID='+BookID+'&StrategyID='+document.getElementById('HfExCludeChaptersId').value, 
            '', 'help:no; status:no; dialogWidth:300px;dialogHeight:660px');

            if(! selectedChapter)
            {
                return;
            }

            if(selectedChapter=="|") 
            {
                document.getElementById('HfExCludeChaptersId').value = "";
                document.getElementById('txtExCludeChapters').value = "";
            } 
            else 
            {
                document.getElementById('HfExCludeChaptersId').value = selectedChapter.split('|')[0];
                document.getElementById('txtExCludeChapters').value = selectedChapter.split('|')[1];    
            }
        	
        	
        	__doPostBack("btnExClude");
        	//__doPostBack("btnShowMother");
        }
        
        function selectItems() 
        {
        	 var examId = document.getElementById("hfExamID").value; 
            var RangeType = document.getElementById('HfRangeType').value;
            var ChapterID = document.getElementById('HfChapterId').value;
        	var exculdeIDs = document.getElementById('HfExCludeChaptersId').value;
        	var subjectId = document.getElementById('hfSubjectId').value;
        		
            var search = window.location.search;
        	var StrategyID = document.getElementById("hfKeyID").value;
        	var itemTypeID = document.getElementById("hfItemType").value;
            var selectedChapter = window.showModalDialog('/RailExamBao/RandomExam/MultiSelectItem.aspx?subjectId='+ subjectId +'&exculdeIDs='+exculdeIDs+'&examId='+examId+'&itemTypeID='+ itemTypeID +'&RangeType='+ RangeType +'&BookChapterID='+ChapterID+'&id='+StrategyID, 
            window, 'help:no; status:no; dialogWidth:800px;dialogHeight:600px');

            if(! selectedChapter)
            {
                return;
            }
        	
        	__doPostBack("btnSelect");
        }
       
       function logout() 
       {
            var n = window.event.screenX - window.screenLeft;　　　　　　
            var b = n > document.documentElement.scrollWidth - 40;
            if ((b && window.event.clientY < 0) ||  window.event.altKey)
            {
            	//新增
            	var search = window.location.search;
            	if(search.indexOf("&id=")<0)
            	{
            		top.returnValue = "False"+"|"+document.getElementById("hfKeyID").value;
            		top.close();
            	} 
            	else
            	{
            		top.returnValue = "true";
            		top.close();
            	}
            }  
       }
       

    </script>

</head>
<body onbeforeunload="logout()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current">
                        取题范围详细信息</div>
                </div>
            </div>
            <div id="content">
                <table class="contentTable">
                    <tr>
                        <td style="width: 10%">
                            大题名称：</td>
                        <td style="width: 80%">
                            <asp:Label ID="txtSubjectName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            教材章节</td>
                        <td>
                            <asp:TextBox ID="txtChapterName" runat="server" Width="270px" ReadOnly="true">
                            </asp:TextBox>
                            <img id="ImgSelectChapterName" style="cursor: hand;" onclick="selectChapter();" src="../Common/Image/search.gif"
                                alt="选择教材章节" border="0" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="“教材章节”不能为空！" ControlToValidate="txtChapterName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            屏蔽教材章节</td>
                        <td>
                            <asp:TextBox ID="txtExCludeChapters" runat="server" Width="270px" ReadOnly="true">
                            </asp:TextBox>
                            <img id="ImgSelectExCludeChapters" style="cursor: hand;" onclick="selectChapterS();"
                                src="../Common/Image/search.gif" alt="选择屏蔽教材章节" border="0" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            试题类型</td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server" Enabled="false" DataSourceID="odsItemType"
                                DataTextField="TypeName" DataValueField="ItemTypeId">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            可选题数</td>
                        <td>
                            <asp:Label runat="server" ID="lblTotalCount"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            选择题数</td>
                        <td>
                            <asp:TextBox runat="server" ID="lblSelectCount" ReadOnly="true" Width="270px"></asp:TextBox>
                            <img id="Img1" style="cursor: hand;" onclick="selectItems();" src="../Common/Image/search.gif"
                                alt="选择试题" border="0" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            随机题数</td>
                        <td>
                            <asp:TextBox ID="txtNDR" runat="server" Width="100px"></asp:TextBox>(本大题设定的总题数为：<asp:Label
                                runat="server" ID="labelTotalCount"></asp:Label>，还剩<asp:Label runat="server" ID="labelLeaveCount"></asp:Label>题。)
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="随机题数应为数字！"
                                Display="None" MaximumValue="9999" MinimumValue="0" Type="Integer" ControlToValidate="txtNDR"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr id="mother1" runat="server">
                        <td colspan="2">
                            <asp:Label ID="lblMotherInfo" runat="server"></asp:Label></td>
                    </tr>
                    <tr id="mother2" runat="server" style="text-align: center">
                        <td colspan="2">
                            <asp:Button ID="btnSelectChapter" runat="server" CssClass="displayNone" OnClick="btnSelectChapter_Click" />
                            <asp:Button ID="btnSelect" runat="server" CssClass="displayNone" OnClick="btnSelect_Click" />
                            <asp:Button ID="btnExClude" runat="server" CssClass="displayNone" OnClick="btnExClude_Click" />
                            <asp:Button ID="btnShowMother" runat="server" CssClass="displayNone" OnClick="btnShowMother_Click" />
                            <div style="overflow: auto;">
                                <asp:GridView ID="Grid1" runat="server" DataKeyNames="RandomExamStrategyId" HeaderStyle-BackColor="ActiveBorder"
                                    AutoGenerateColumns="False" Width="97%" ForeColor="#333333" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderText="母题名称">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="ChapterName" runat="server" Text='<%# Bind("ChapterName") %>' />
                                                <asp:HiddenField ID="hfChapterId" runat="server" Value='<%# Bind("ChapterId") %>' />
                                                <asp:HiddenField ID="hfKey" runat="server" Value='<%# Bind("RandomExamStrategyId") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="最大题数">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:Label ID="hfMaxItemCount" runat="server" Text='<%# Bind("MaxItemCount") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="题数">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtItemCount" Width="90px" runat="server" Text='<%# Bind("ItemCount") %>'></asp:TextBox>
                                                <asp:RangeValidator ID="RangeValidator1" runat="server" Display="None" ErrorMessage="题数必须为大于0的整数！"
                                                    MaximumValue="99999999" MinimumValue="0" Type="Integer" ControlToValidate="txtItemCount"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注</td>
                        <td>
                            <asp:TextBox ID="txtMemo" runat="server" TextMode="MultiLine">
                            </asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div id="button" style="text-align: center;">
                    <asp:ImageButton ID="SaveButton" runat="server" OnClick="SaveButton_Click" ImageUrl="../Common/Image/save.gif" />
                    <asp:ImageButton ID="CancelButton" runat="server" Visible="false" OnClientClick="return window.close();"
                        ImageUrl="../Common/Image/close.gif" />
                </div>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsItemType" runat="server" SelectMethod="GetItemTypes"
            TypeName="RailExam.BLL.ItemTypeBLL"></asp:ObjectDataSource>
        <asp:HiddenField ID="hfSubjectId" runat="server" />
        <asp:HiddenField ID="HfChapterId" runat="server" />
        <asp:HiddenField ID="HfBookId" runat="server" />
        <asp:HiddenField ID="HfRangeType" runat="server" />
        <asp:HiddenField ID="HfRangeName" runat="server" />
        <asp:HiddenField ID="HfExCludeChaptersId" runat="server" />
        <asp:HiddenField ID="hfItemType" runat="server" />
        <asp:HiddenField ID="hfExamID" runat="server" />
        <asp:HiddenField ID="hfKeyID" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
    </form>
</body>
</html>
