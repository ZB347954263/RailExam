<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RandomExamApplyAll.aspx.cs"
    Inherits="RailExamWebApp.RandomExam.RandomExamApplyAll"  EnableEventValidation="true"%>

<%@ Import Namespace="RailExamWebApp.Common.Class" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>回复考试请求</title>

    <script type="text/javascript" src="../Common/JS/Common.js"></script>

    <script type="text/javascript">   
       var str = "";
           
       function Grid1_ItemCheckedChange(sender, eventArgs)
      {
         var strID = eventArgs.get_item().getMember('RandomExamApplyID').get_text();
         if(!eventArgs.get_item().getMember(0).get_value())
         {
            if(str == "")
            {
                str = strID;
            }
            else
            {
               str = str+","+strID;
            }
         }
         else
         {
            var strNow = (","+str+",");
            strNow = strNow.replace(","+strID+",",",");
            if(strNow.substring(0,1) == ",")
            {
                strNow = strNow.substring(1);
            }
            
            if(strNow.substring(strNow.length-1) == ",")
            {
              strNow = strNow.substring(0,strNow.length-1);
            }
            str = strNow;
         }             
//         alert(str);
       } 
         
       function GetChoose()
       {
          if(!confirm("确定要通过选中的考试请求吗？"))
          {
                return; 
          }   
         
           form1.ChooseID.value = str;
           form1.submit();
           form1.ChooseID.value = "";
       }
      
      function  checkAll(obj)
      {
           var count = Grid1.get_table().getRowCount();
          for(var i = 0 ; i< count; i++)
          { 
                var strID = Grid1.get_table().getRow(i).getMember("RandomExamApplyID").get_text();
                if(obj.checked)
               {
                    if((","+str+",").indexOf(","+strID+",") <0 )
                    {
                       if(str == "")
                       {
                         str = strID; 
                       }
                       else
                       {
                         str = str + "," + strID; 
                       }     
                    }     
                }
               else
               {
                     if((","+str+",").indexOf(","+strID+",") >= 0 )
                    {
                        var strNow = (","+str+",");
                        strNow = strNow.replace(","+strID+",",",");
                        if(strNow.substring(0,1) == ",")
                        {
                            strNow = strNow.substring(1);
                        }
                        
                        if(strNow.substring(strNow.length-1) == ",")
                        {
                          strNow = strNow.substring(0,strNow.length-1);
                        }
                        str = strNow;
                    }
               } 
          }
          gridCallback.callback();
      }   
      
      function judgePaper(id,employeeid)
      {
            form1.ChooseOneID.value =  id+"|"+employeeid;
            form1.submit();
            form1.ChooseOneID.value = ""; 
       }  
      
      function searchExamCallBack_complete(sender, eventArgs)
      {
              var count = Grid1.get_table().getRowCount();
              //alert(count);
               var n = 0;
              for(var i = 0 ; i< count; i++)
              {
                    var strID = Grid1.get_table().getRow(i).getMember("RandomExamApplyID").get_text();
                    if((","+str+",").indexOf(","+strID+",") >=0 )
                    {
                        Grid1.get_table().getRow(i).setValue(0,"true");
//                      alert(Grid1.get_table().getRow(i).getMember(0).get_value());
                    }
                     if(Grid1.get_table().getRow(i).getMember(0).get_value())
                    {
                        n = n + 1;
                    }   
              }
              
              if(n == count && n != 0)
              {
                 document.getElementById("checkall").checked = true;
              }
              else
              {
                document.getElementById("checkall").checked = false;
              }
         } 
        
        function loadGrid()
        {
            if(!window.Grid1 || window.Grid1.toString() != "[object Object]")
            {
                setTimeout("loadGrid()",50);
                return;  
            }
            
            refreshGrid();
        }
        
        function getGrid(strApply)
        {
            str = strApply;
            if(!window.Grid1 || window.Grid1.toString() != "[object Object]")
            {
                setTimeout("loadGrid()",50);
                return;  
            }
            
            refreshGrid();
        }
        
        function refreshGrid()
        {
            gridCallback.callback();
            setTimeout("refreshGrid()",10000);
        }
        
        function delApply(id)
        {
            if(!confirm("确定要删除该考生的考试请求吗？"))
           {
                return; 
           }  
           
           form1.deleteid.value = id;
           form1.submit();
           form1.deleteid.value = "";
        }
    </script>

</head>
<body onload="loadGrid()">
    <form id="form1" runat="server">
        <div id="page">
            <div id="head">
                <div style="width: 15%; float: left;">
                    <div style="color: #2D67CF; float: left;">
                        回复考试请求</div>
                </div>
                <div id="button">
                    <input type="button" class="button" value="通过请求" onclick="GetChoose()" />
                </div>
            </div>
            <div id="content">
                <div style="text-align: left;">
                    &nbsp;&nbsp;&nbsp;&nbsp;<input type="checkbox" name="checkall" onclick="checkAll(this)" />全选
                </div>
                <div style="text-align: center">
                    <ComponentArt:CallBack ID="gridCallback" runat="server" OnCallback="gridCallback_callback">
                        <ClientEvents>
                            <CallbackComplete EventHandler="searchExamCallBack_complete" />
                        </ClientEvents>
                        <Content>
                            <ComponentArt:Grid ID="Grid1" runat="server" PageSize="20" Width="100%">
                                <ClientEvents>
                                    <ItemCheckChange EventHandler="Grid1_ItemCheckedChange" />
                                </ClientEvents>
                                <Levels>
                                    <ComponentArt:GridLevel DataKeyField="RandomExamApplyID">
                                        <Columns>
                                            <ComponentArt:GridColumn DataField="IsChecked" HeadingText="选择" ColumnType="CheckBox"
                                                AllowEditing="True" Width="40" DataType="System.Boolean" />
                                            <ComponentArt:GridColumn DataField="RandomExamApplyID" HeadingText="编号" Visible="false" />
                                            <ComponentArt:GridColumn DataField="EmployeeName" HeadingText="考生姓名" Width="60" />
                                            <ComponentArt:GridColumn DataField="ExamName" HeadingText="考试名称" Width="90" />
                                            <ComponentArt:GridColumn DataField="RandomExamCode" HeadingText="验证码" Width="40" />
                                            <ComponentArt:GridColumn DataField="EmployeeID" Visible="false" />
                                            <ComponentArt:GridColumn DataField="WorkNo" HeadingText="员工编码" Width="110" />
                                            <ComponentArt:GridColumn DataField="OrgName" HeadingText="考生单位" Width="150" />
                                            <ComponentArt:GridColumn DataField="CodeFlag" HeadingText="验证码是否正确" ColumnType="CheckBox"
                                                Width="100" />
                                            <ComponentArt:GridColumn DataField="IPAddress" HeadingText="IP地址" Width="100" />
                                            <ComponentArt:GridColumn DataField="ApplyStatusName" HeadingText="申请状态" Width="60" />
                                            <ComponentArt:GridColumn DataCellClientTemplateId="CTedit" HeadingText="操作" Width="100"
                                                Align="Center" />
                                        </Columns>
                                    </ComponentArt:GridLevel>
                                </Levels>
                                <ClientTemplates>
                                    <ComponentArt:ClientTemplate ID="CTedit">
                                        <a onclick="judgePaper(##DataItem.getMember('RandomExamApplyID').get_value()##,##DataItem.getMember('EmployeeID').get_value()##)"
                                            href="#"><b>通过请求</b></a>&nbsp;&nbsp; <a onclick="delApply(##DataItem.getMember('RandomExamApplyID').get_value()##)"
                                                href="#"><b>删除</b></a>
                                    </ComponentArt:ClientTemplate>
                                </ClientTemplates>
                            </ComponentArt:Grid>
                        </Content>
                    </ComponentArt:CallBack>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="HfUpdateRight" runat="server" />
        <asp:HiddenField ID="HfDeleteRight" runat="server" />
        <asp:HiddenField ID="hfIsAdmin" runat="server" />
        <input type="hidden" name="IsServer" value='<%=PrjPub.IsServerCenter %>' />
        <input name="OutPutRandom" type="hidden" />
        <input name="ChooseID" type="hidden" />
        <input name="ChooseOneID" type="hidden" />
        <input name="Refresh" type="hidden" />
        <input name="deleteid" type="hidden" />
        <asp:HiddenField ID="hfSelect" runat="server" />
    </form>
</body>
</html>
