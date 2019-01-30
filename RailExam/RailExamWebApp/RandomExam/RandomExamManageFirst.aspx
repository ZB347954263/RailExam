<%@ Import Namespace="RailExamWebApp.Common.Class" %>

<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamManageFirst.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamManageFirst" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/DateTimeUC.ascx" TagName="Date" TagPrefix="uc1" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>考试基本信息</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">
        function selectExamCategory()
        {
            if(document.getElementById('hfMode').value != "ReadOnly")
            {
        	    var selectedExamCategory = window.showModalDialog('../Common/SelectExamCategory.aspx', 
                    '', 'help:no; status:no; dialogWidth:340px;dialogHeight:620px');

                if(! selectedExamCategory)
                {
                    return;
                }
                
                document.getElementById("hfCategoryId").value = selectedExamCategory.split('|')[0];
                document.getElementById("txtCategoryName").value = selectedExamCategory.split('|')[1];
            }
        }
        
        function init()
        {
               if(document.getElementById("hfHasTrainClass").value == "True")
               {
                    document.getElementById("exmasource1").style.display = "";
                   document.getElementById("chkAllItem").style.display = ""; 
               	    document.getElementById("lblAllItem").style.display = ""; 
               }
               else
                {
                    document.getElementById("exmasource1").style.display = "none";
                   document.getElementById("chkAllItem").style.display = "none"; 
               	 document.getElementById("lblAllItem").style.display = "none"; 
                }
               
               if(document.getElementById("hfIsReset").value == "True")
               {
                    document.getElementById("selectPostQuery").style.display = "none";
               }
            
        }
        
        function chkHasTrainClassOnchange()
        {
            if(document.getElementById("chkHasTrainClass").checked)
            {
                document.getElementById("exmasource1").style.display = "";
                document.getElementById("chkAllItem").style.display = ""; 
            	document.getElementById("lblAllItem").style.display = ""; 
            }
            else
            {
                document.getElementById("exmasource1").style.display = "none";
                document.getElementById("chkAllItem").style.display = "none"; 
            	document.getElementById("lblAllItem").style.display = "none"; 
            }
        }        
                
        function selectPost()
        {
        	 if(!document.getElementById("chkHasTrainClass").checked)
            {
                var selectedPost = window.showModalDialog('../RandomExamOther/MultiSelectPosts.aspx?id='+document.getElementById("hfPostID").value+"&name="+document.getElementById("txtPost").value, 
                '', 'help:no; status:no; dialogWidth:300px;dialogHeight:620px;scroll:no;');

                if(! selectedPost)
                {
                    return;
                }
                document.getElementById('hfPostID').value = selectedPost.split('|')[0];
                document.getElementById('hfPostName').value = selectedPost.split('|')[1];
                document.getElementById('txtPost').value = selectedPost.split('|')[1];
            }
        	else 
        	 {
            	var trainclassID = document.getElementById("hfTrainClassID").value;

            	if (trainclassID == "")
            	{
            		alert("请先选择培训班！");
            		return;
            	}

            	//alert(document.getElementById("hfPostID").value);

            	var selectedPost = window.showModalDialog('SelectPost.aspx?trainClassID=' + trainclassID + '&id=' + document.getElementById("hfPostID").value,
            		'', 'help:no; status:no; dialogWidth:300px;dialogHeight:620px;scroll:no;');

            	if (!selectedPost)
            	{
            		return;
            	}

            	document.getElementById('hfPostID').value = selectedPost.split('|')[0];
            	document.getElementById('hfPostName').value = selectedPost.split('|')[1];
            	document.getElementById('txtPost').value = selectedPost.split('|')[1];
            }
        }
        
      function SaveArrange(strID,strStartMode,strMode)
      {
        var trainclassID = document.getElementById("hfTrainClassID").value;
        var postID = document.getElementById("hfPostID").value;
        var ret = showCommonDialog("/RailExamBao/RandomExam/SaveArrange.aspx?postID="+ postID +"&trainclassID="+ trainclassID +"&examID="+strID,'dialogWidth:300px;dialogHeight:30px;');
         if(ret == "true" )
         {
             window.location = "RandomExamManageSecond.aspx?startmode=" + strStartMode + "&mode=" + strMode + "&id=" + strID;
         }
      }
      
        function inputCallback_onCallbackComplete()
        {
            var exam = document.getElementById("hfExam").value;
            //alert(exam);
            SaveArrange(exam.split('|')[0],exam.split('|')[1],exam.split('|')[2]);
        }
    </script>

    <link href="/RailExamWebApp/Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
</head>
<body onload="init();">
    <form id="form1" runat="server">
        <div id="page" style="overflow-y: auto; height: 680px;">
            <div id="head">
                <div id="location">
                    <div id="parent">
                        新增考试</div>
                    <div id="separator">
                    </div>
                    <div id="current">
                        基本信息</div>
                </div>
            </div>
            <div style="overflow: auto;">
                <table class="contentTable">
                    <tr>
                        <td colspan="2">
                            <b>第一步：填写基本信息</b>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">
                            考试分类<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtCategoryName" runat="server" Width="30%" ReadOnly="true">
                            </asp:TextBox>
                            <img style="cursor: hand;" onclick="selectExamCategory();" src="../Common/Image/search.gif"
                                alt="选择考试分类" border="0" />
                            <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="“考试分类”不能为空！" ControlToValidate="txtCategoryName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            考试名称<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtExamName" runat="server" MaxLength="50" Width="500px">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvExamName" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="请输入试卷名称！" ControlToValidate="txtExamName">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            有效时间<span class="require">*</span></td>
                        <td>
                            <uc1:Date ID="dateBeginTime" runat="server" />
                            至<uc1:Date ID="dateEndTime" runat="server" />
                            <asp:DropDownList ID="ddlType" runat="server" DataSourceID="odsExamType" DataTextField="TypeName"
                                DataValueField="ExamTypeId" Visible="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            考试时间<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtExamTime" runat="server" Width="60px" Text="60"></asp:TextBox>(分钟)
                            <asp:RequiredFieldValidator ID="rfvExamTime" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="“考试时间”不能为空！" ControlToValidate="txtExamTime">
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr id="exmasource">
                        <td>
                            考试来源</td>
                        <td>
                            <asp:CheckBox ID="chkHasTrainClass" runat="server" Text="选择培训班" AutoPostBack="True"
                                OnCheckedChanged="chkHasTrainClass_CheckedChanged" />&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                    ID="btnAddTrainClass" runat="server" CssClass="buttonLong" Text="新增培训班" CausesValidation="false"
                                    OnClick="btnAddTrainClass_Click" />
                        </td>
                    </tr>
                    <tr id="exmasource1">
                        <td>
                            培训班列表</td>
                        <td>
                            <asp:GridView ID="Grid1" runat="server" HeaderStyle-BackColor="ActiveBorder" AutoGenerateColumns="False"
                                ForeColor="#333333" GridLines="None" OnRowCancelingEdit="Grid1_RowCancelingEdit"
                                DataKeyNames="RandomExamTrainClassID" OnRowDataBound="Grid1_RowDataBound" OnRowEditing="Grid1_RowEditing"
                                OnRowUpdating="Grid1_RowUpdating" OnRowDeleting="Grid1_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="RandomExamTrainClassID" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("RandomExamTrainClassID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RandomExamID" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblRandomExamID" runat="server" Text='<%# Bind("RandomExamID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="培训班">
                                        <HeaderStyle HorizontalAlign="Center" Width="400px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblTrainClass" runat="server" Text='<%# Bind("TrainClassName") %>'></asp:Label>
                                            <asp:Label ID="lblTrainClassID" Visible="false" runat="server" Text='<%# Bind("TrainClassID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:HiddenField ID="hfTrainClass" runat="server" Value='<%# Bind("TrainClassID") %>' />
                                            <asp:DropDownList ID="ddlTrainClass" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTrainClassChange">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="科目">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubjectName" runat="server" Text='<%# Bind("TrainClassSubjectName") %>'></asp:Label>
                                            <asp:Label ID="lblSubjectID" Visible="false" runat="server" Text='<%# Bind("TrainClassSubjectID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:HiddenField ID="hfTrainclassSubjectID" runat="server" Value='<%# Bind("TrainClassSubjectID") %>' />
                                            <asp:DropDownList ID="ddlTrainClassSubject" runat="server">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnModify" runat="server" ImageUrl="../Common/Image/edit_col_edit.gif"
                                                AlternateText="修改" CommandName="Edit" CausesValidation="false"></asp:ImageButton>&nbsp;&nbsp;
                                            <asp:ImageButton ID="btnDel" runat="server" ImageUrl="../Common/Image/edit_col_Delete.gif"
                                                AlternateText="删除" CommandName="Delete" OnClientClick="return confirm('您确定要删除该培训班吗？');"
                                                CausesValidation="false"></asp:ImageButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="../Common/Image/edit_col_save.gif"
                                                AlternateText="保存" CommandName="Update" CausesValidation="false"></asp:ImageButton>&nbsp;&nbsp;
                                            <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="../Common/Image/edit_col_cancel.gif"
                                                AlternateText="取消" CommandName="Cancel" CausesValidation="False"></asp:ImageButton>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="exmasource2">
                        <td>
                            选择职名</td>
                        <td>
                            <asp:TextBox ID="txtPost" runat="server" Width="30%" ReadOnly="true">
                            </asp:TextBox>
                            <img id="selectPostQuery" style="cursor: hand;" onclick="selectPost();" src="../Common/Image/search.gif"
                                alt=" 选择职名" border="0" />&nbsp;&nbsp;
                            <asp:CheckBox runat="server" ID="chkAllItem" /><asp:Label runat="server" ID="lblAllItem"
                                Text="选择全部试题"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            模块考试类别</td>
                        <td>
                            <asp:DropDownList ID="ddlModularType" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            考试及格分数<span class="require">*</span></td>
                        <td>
                            <asp:TextBox ID="txtPassScore" runat="server" Width="60px" Text="80"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" EnableClientScript="true"
                                Display="none" ErrorMessage="“考试及格分数”不能为空！" ControlToValidate="txtPassScore">
                               
                            </asp:RequiredFieldValidator>
                            <asp:RangeValidator ID="RangeValidator2" runat="server" ErrorMessage="考试分数必须为数字！"
                                ControlToValidate="txtPassScore" Display="None" MaximumValue="100" MinimumValue="0"
                                Type="Double"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            考试模式</td>
                        <td>
                            <asp:RadioButton ID="rbnExamMode1" runat="server" Text="机考" Checked="true" GroupName="rdaGN" />
                            <asp:RadioButton ID="rbnExamMode2" runat="server" Text="非机考" GroupName="rdaGN"  />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            开考模式</td>
                        <td>
                            <asp:RadioButton ID="rbnStartMode1" runat="server" Text="随到随考" Checked="true" GroupName="rbnStartMode" />
                            <asp:RadioButton ID="rbnStartMode2" runat="server" Text="统一时间考试" GroupName="rbnStartMode" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            考试类型</td>
                        <td>
                            <asp:RadioButton ID="rbnStyle2" runat="server" Text="存档考试" GroupName="rbnStyle" AutoPostBack="true"
                                OnCheckedChanged="rbnStyle2_CheckedChanged" />
                            <asp:RadioButton ID="rbnStyle1" runat="server" Text="不存档考试" GroupName="rbnStyle"
                                AutoPostBack="true" OnCheckedChanged="rbnStyle1_CheckedChanged" />
                        </td>
                    </tr>
                    <tr id="saveTd" runat="server">
                        <td>
                            存档时间
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDate" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDate_SelectedIndexChanged">
                                <asp:ListItem Value="1">永久存档</asp:ListItem>
                                <asp:ListItem Value="2">其他存档时间</asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;
                            <uc2:Date ID="dateSaveDate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            最多参加次数</td>
                        <td>
                            <asp:TextBox ID="txtMET2" runat="Server" Width="60px" Text="1"></asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtMET2"
                                Display="None" ErrorMessage="参加次数最多为3次！" MaximumValue="3" MinimumValue="1" Type="Integer"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            其他属性</td>
                        <td colspan="2">
                            <asp:CheckBox ID="chSeeScore" runat="server" Checked="true" Text="允许考生查看成绩" />
                            &nbsp;&nbsp;<asp:CheckBox ID="chkCanSeeAnswer" runat="server" Checked="true" Text="允许考生查看答卷"
                                AutoPostBack="true" OnCheckedChanged="chkCanSeeAnswer_CheckedChanged" />
                            &nbsp;&nbsp; <asp:CheckBox ID="chkPublicScore" runat="server" Checked="true" Text="允许考生查看考试结果" />
                            &nbsp;&nbsp;<asp:CheckBox ID="chkIsReduceScore" runat="server" Text="答错倒扣分" />
                        </td>
                    </tr>
                    <asp:Panel runat="server" ID="Panel1" Visible="false">
                        <tr>
                            <td rowspan="5">
                                选项</td>
                            <%--<asp:CheckBox ID="chIssave" runat="server" />自动保存答卷--%>
                            <td>
                                <asp:CheckBox ID="chUD" runat="server" />允许管理员手动控制考试</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chAutoScore" runat="server" />是否自动评分</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chSeeAnswer" runat="server" />允许考生查看答卷和答案</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:CheckBox ID="chPublicScore" runat="server" />考试成绩对所有用户公开</td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td>
                            描述</td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" Width="500px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            备注</td>
                        <td>
                            <asp:TextBox ID="txtMemo" runat="server" TextMode="MultiLine" Width="500px">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            创建者</td>
                        <td>
                            <asp:Label ID="lblCreatePerson" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            创建时间</td>
                        <td>
                            <asp:Label ID="lblCreateTime" runat="server">
                            </asp:Label>
                        </td>
                    </tr>
                </table>
                <div id="button" style="text-align: center;">
                    <br />
                    <asp:ImageButton ID="btnSave" runat="server" OnClick="btnSave_Click" ImageUrl="../Common/Image/next.gif" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnCancel" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                        ImageUrl="../Common/Image/cancel.gif" />
                </div>
                <br />
            </div>
        </div>
        <asp:HiddenField ID="hfMode" runat="server" />
        <asp:HiddenField ID="hfCategoryId" runat="server" />
        <asp:ObjectDataSource ID="odsExamType" runat="server" SelectMethod="GetExamTypes"
            TypeName="RailExam.BLL.ExamTypeBLL"></asp:ObjectDataSource>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" />
        <input type="hidden" name="IsWuhanOnly" value='<%=PrjPub.IsWuhanOnly()%>' />
        <asp:HiddenField ID="hfHasTrainClass" Value="False" runat="server" />
        <asp:HiddenField ID="hfTrainClassID" runat="server" />
        <asp:HiddenField ID="hfPostID" runat="server" />
        <asp:HiddenField ID="hfPostName" runat="server" />
        <asp:HiddenField ID="hfIsReset" runat="server" />
        <asp:HiddenField ID="hfOrgID" runat="server" />
        <ComponentArt:CallBack ID="inputCallback" runat="server" OnCallback="inputCallback_Callback">
            <ClientEvents>
                <CallbackComplete EventHandler="inputCallback_onCallbackComplete" />
            </ClientEvents>
            <Content>
                <asp:HiddenField ID="hfExam" runat="server" />
            </Content>
        </ComponentArt:CallBack>
    </form>
</body>
</html>
