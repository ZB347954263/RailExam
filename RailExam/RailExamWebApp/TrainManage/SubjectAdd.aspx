<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" Codebehind="SubjectAdd.aspx.cs"
    Inherits="RailExamWebApp.TrainManage.SubjectAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <title>无标题页</title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
    function closeAndRef()
    {
           window.returnValue="ref";
           window.close();
    }
    function CheckInputLength(oInput , maxLength)
        { 
            if(oInput.value.length>maxLength)
            {
                oInput.value = oInput.value.substring(0,maxLength); 
            }
        }
    function checkTxt()
    {
     
       var txtSubjectName=document.getElementById("txtSubjectName");
       var txtPassResult=document.getElementById("txtPassResult");
        var txtMemo=document.getElementById("txtMemo");
      
       if(txtSubjectName.value=="")
       {
          alert("请输入培训班科目！");
          txtSubjectName.focus();
          return false;
       }
       if(txtPassResult.value=="")
       {
         alert("请输入及格分数！");
         txtPassResult.focus();
         return false;
       }
    	if(txtSubjectName.value.lenth>50) {
    		alert("输入长度超过50个字符！");
    		 txtSubjectName.focus();
    		return false;
    		
    	}
    	if(txtMemo.value.lenth>50) {
    		alert("输入长度超过50个字符！");
    		 txtSubjectName.focus();
    		return false;
    		
    	}
    }
    function checkNum(obj)
    {
    	var reg = /^\d+(\.\d{1,2})?$/ ;
    	if (obj.value != "")
    	{
    		if (!reg.test(obj.value))
    		{
    			alert("请输入数字！");
    			obj.value = "";
    			obj.focus();
    			return;
    		}
    		if (Math.round(obj.value) >= 1000)
    		{
    			alert("最大为三位数字！");
    			obj.value = "";
    			obj.focus();
    			return;
    		}
    	}
    }
    function checkIsNum(obj) {
    	var reg = /^\d+()?$/ ;
    	if (obj.value != "")
    	{
    		if (!reg.test(obj.value))
    		{
    			alert("请输入数字！");
    			obj.value = "";
    			obj.focus();
    			return;
    		}
    	}
    }
    function clo()
    {
       window.close();
       return false;
    }
//    function checkBox(obj)
//    {
//      document.getElementById("chkIs").checked=false;
//      document.getElementById("chkNot").checked=false;
//      obj.checked=true;
//    }
    
    function selectBook() {
    	var bookIds = document.getElementById("hfBookIds").value;
    	var selectedBook = window.showModalDialog("/RailExamBao/Common/SelectStrategyChapter.aspx?itemTypeID=1&flag=4&item=no&bookIds="+bookIds, 
        '', 'help:no; status:no; dialogWidth:350px;dialogHeight:660px');

    	if(selectedBook == undefined) {
    		return;
    	}
    	
    	var bookList = selectedBook.split('$');

    	var strBookName = "";
    	var strBookId = "";
    	for(var i=0; i<bookList.length;i++) {
    		var bookInfo = bookList[i].split('|');
    		
    		if(i==0) {
    			strBookName = bookInfo[2];
    			strBookId = bookInfo[0];
    		}
    		else {
    			strBookName += "|"+ bookInfo[2];
    			strBookId += "|"+ bookInfo[0];
    		}
    	}
 
    	document.getElementById("txtBook").value = strBookName;
    	document.getElementById("hfBookIds").value = strBookId;
    }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="page" style="width: 603px">
            <div id="head">
                <div id="location">
                    <div id="separator">
                    </div>
                    <div id="current" runat="server">
                        新增培训科目</div>
                </div>
            </div>
            <div id="content" style="width: 610px">
                <table class="contentTable">
                    <tr>
                        <td style="width: 15%;">
                            培训班科目<span class="require">*</span>
                        </td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtSubjectName" runat="server" MaxLength="50"></asp:TextBox>
                        </td>
                        <td style="width: 15%;">
                            &nbsp;&nbsp;及格分数<span class="require">*</span>
                        </td>
                        <td style="width: 35%;">
                            <asp:TextBox ID="txtPassResult" runat="server" onblur="checkNum(this)"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;是否机考<span class="require">*</span>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkIs" runat="server" Text="机考" />
                        </td>
                        <td>
                            &nbsp;&nbsp;课时
                        </td>
                        <td>
                            <asp:TextBox ID="txtHour" runat="server" onblur="checkIsNum(this)" MaxLength="4"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;教材</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtBook" runat="server" TextMode="MultiLine" ReadOnly="true"></asp:TextBox><img
                                id="ImgSelectBook" style="cursor: hand;" onclick="selectBook();" src="../Common/Image/search.gif"
                                alt="选择教材" border="0" />
                            <asp:HiddenField runat="server" ID="hfBookIds" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;备注
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtMemo" Width="99%" runat="server" TextMode="MultiLine" Rows="5" MaxLength="50"
                                onpropertychange="CheckInputLength(this,50)"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td colspan="4" align="center">
                            <asp:ImageButton ID="imgSave" runat="server" ImageUrl="../Common/Image/save.gif"
                                CausesValidation="false" OnClientClick="return checkTxt()" OnClick="imgSave_Click" />&nbsp;&nbsp;
                            &nbsp;&nbsp;
                            <asp:ImageButton ID="imgClose" runat="server" ImageUrl="../Common/Image/close.gif"
                                OnClientClick="return clo()" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div>
        </div>
    </form>
</body>
</html>
