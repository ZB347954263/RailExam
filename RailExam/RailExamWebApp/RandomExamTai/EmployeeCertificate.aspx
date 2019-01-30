<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeCertificate.aspx.cs" Inherits="RailExamWebApp.RandomExamTai.EmployeeCertificate" EnableEventValidation="false"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <base target="_self" />
    <title></title>
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
 
      function editOrDel(id,action) {
      	if(action=="add")
      		id = document.getElementById("hfID").value;
      	if(action=="add" || action=="edit") {
    	var returnvalue = window.showModalDialog('EmployeeCertificateEdit.aspx?id=' + id +"&mode="+action+"&num=" + Math.random(),
    			'', 'help:no; status:no; dialogWidth:700px;dialogHeight:430px');
      		if(!returnvalue)
      			return;
      	}
    
        document.getElementById("hfAction").value = action;
      	document.getElementById("btnPost").click();
  
      }

      function del(id) {
      	document.getElementById("hfAction").value = "del";
      	document.getElementById("hfDelID").value = id;
      	document.getElementById("btnPost").click();
      }
      
          function forbiddenAdd()
          {
          	var search = location.search;
          	var index = search.indexOf("Type");
          	if (index != -1)
          	{
          		var type = search.substr(index + 5, 1);
          		if (type == "0")
          		{
          			var btn = document.getElementById("btnAdd");
          			btn.style.display = "none";
          		}
          	}

          	if (document.getElementById("hfIsServerCenter").value == "False") {
          		var btn = document.getElementById("btnAdd");
          		btn.style.display = "none";
          	}
          }
    </script>
</head>
<body onload="forbiddenAdd()">
    <form id="form1" runat="server">
 <div id="page">
            <div id="content">
                <div style="text-align: right;">
                    <input id="btnAdd" type="button" value="新  增" class="button" onclick="editOrDel(0,'add')" />
                    <asp:Button ID="btnPost" runat="server" Style="display: none" OnClick="btnPost_Click" />
                     
                </div>
                <div style="text-align: center;">
                    <ComponentArt:Grid ID="grdEntity" runat="server" PageSize="15" Width="850px">
                        <Levels>
                            <ComponentArt:GridLevel DataKeyField="employee_certificate_id">
                                <Columns>
                                    <ComponentArt:GridColumn DataField="employee_certificate_id" Visible="false" />
                                    <ComponentArt:GridColumn DataField="unitName" HeadingText="工作单位" />
                                    <ComponentArt:GridColumn DataField="employee_name" HeadingText="姓名" />
                                    <ComponentArt:GridColumn DataField="sex" HeadingText="性别" />
                                    <ComponentArt:GridColumn DataField="birthday" HeadingText="出生日期" FormatString="yyyy-MM-dd"/>
                                    <ComponentArt:GridColumn DataField="identity_cardno" HeadingText="身份证号码" />
                                    <ComponentArt:GridColumn DataField="education_level_name" HeadingText="文化程度" />
                                    <ComponentArt:GridColumn DataField="postName" HeadingText="职名" />
                                    <ComponentArt:GridColumn DataField="certificate_name" HeadingText="证书名称" />
                                    <ComponentArt:GridColumn DataField="certificate_level_name" HeadingText="证书级别"  />
                                    <ComponentArt:GridColumn DataField="certificate_date" HeadingText="发证日期" FormatString="yyyy-MM-dd"/>
                                    <ComponentArt:GridColumn DataField="train_unit_name" HeadingText="培训单位" />
                                    <ComponentArt:GridColumn DataField="certificate_unit_name" HeadingText="发证单位" />
                                      <ComponentArt:GridColumn DataField="certificate_no" HeadingText="证书号码" />
                                    <ComponentArt:GridColumn DataField="check_date" HeadingText="复审（年度鉴定）日期" FormatString="yyyy-MM-dd"/>
                                     <ComponentArt:GridColumn DataField="check_unit" HeadingText="复审单位" />
                                      <ComponentArt:GridColumn DataField="check_result" HeadingText="复审（年度鉴定）结果" />
                                       <ComponentArt:GridColumn DataField="begin_date" HeadingText="从事本专业的起始时间"  FormatString="yyyy-MM-dd"/>
                                    <ComponentArt:GridColumn DataField="check_cycle"   HeadingText="复审周期" />
                                      <ComponentArt:GridColumn DataField="end_date" HeadingText="有效期截止日期"/>
                                      
                                    <ComponentArt:GridColumn DataCellClientTemplateId="CTEdit" HeadingText="操作" />
                                </Columns>
                            </ComponentArt:GridLevel>
                        </Levels>
                        <ClientTemplates>
                            <ComponentArt:ClientTemplate ID="CTEdit">
                                <a onclick="editOrDel(##DataItem.getMember('employee_certificate_id').get_value()##,'edit')"
                                    title="修改其他证书" href="#" class="underline"><b>修改</b></a>
                                     <a onclick="javascript:if(!confirm('您确定要删除此证书情况吗？')){return;} del('## DataItem.getMember('employee_certificate_id').get_value() ##');"
                                        title="删除其他证书情况" href="#"><b>删除</b></a>
                            </ComponentArt:ClientTemplate>
                        </ClientTemplates>
                    </ComponentArt:Grid>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hfID" runat="server" />
          <asp:HiddenField ID="hfDelID" runat="server" />
          <asp:HiddenField ID="hfAction" runat="server" />
           <asp:HiddenField runat="server" ID="hfIsServerCenter"/>
    </form>
</body>
</html>
