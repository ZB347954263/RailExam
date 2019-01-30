<%@ Page ValidateRequest="false" Language="C#" AutoEventWireup="True" Codebehind="ItemDetail.aspx.cs"
    Inherits="RailExamWebApp.Item.ItemDetail" %>

<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>
<%@ Register Src="../Common/Control/Date/Date.ascx" TagName="Date" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>试题详细信息</title>
    <link href="../Common/Control/Date/CSS/Date.css" rel="stylesheet" type="text/css" />
    <link href="../Common/CSS/Common.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        //按对象ID获取对象 
        function $F(objId)
        {
            return document.getElementById(objId);
        }
        
        //初始化页面
        function init()
        {
            var search = window.location.search;
            var modeIndex = search.indexOf("mode");
            var andIndex = search.indexOf("&");
            
            if(modeIndex == -1)
            {
                alert("缺少参数！");
                return;
            }
            else
            {
                switch(search.substring(modeIndex + 5, andIndex))
                {
                    case "readonly":
                    {
                        var itemTypes = $F("fvItem_lblItemType");
                        var answerCount = $F("fvItem_lblAnswerCount");
                        var answers = $F("fvItem_hfSelectAnswer").value.split("|");
                        var standard = $F("fvItem_hfStandardAnswer").value.split("|");
                        
                        switch(itemTypes.innerText)
                        {
                            case "单选":
                            {
                                for(var i = 0; i < 12; i ++)
                                {
                                    if(i < parseInt(answerCount.innerText))
                                    {
                                        $F("fvItem_lblSelectAnswer_" + i).innerText = answers[i];
                                        // Hide checkbox
                                        $F("fvItem_cbxSelectAnswer_" + i).parentNode.style.display = "none";                                        
                                    }
                                    else
                                    {
                                        $F("fvItem_lblSelectAnswer_" + i).parentNode.parentNode.style.display = "none";
                                    }
                                }
                                for(var i = 0; i < standard.length; i ++)
                                {
                                    for(var j = 0; j < parseInt(answerCount.innerText); j ++)
                                    {
                                        if(standard[i] == j)
                                        {
                                            $F("fvItem_rbnSelectAnswer_" + j).checked = true;
                                        }
                                    }
                                }
                                $F("contentAnswer").style.display = "none"; 
                                break;
                            }
                            case "多选":
                            {
                                for(var i = 0; i < 12; i ++)
                                {
                                    if(i < parseInt(answerCount.innerText))
                                    {
                                        $F("fvItem_lblSelectAnswer_" + i).innerText = answers[i];
                                        // Hide checkbox
                                        $F("fvItem_rbnSelectAnswer_" + i).parentNode.style.display = "none";                                        
                                    }
                                    else
                                    {
                                        $F("fvItem_lblSelectAnswer_" + i).parentNode.parentNode.style.display = "none";
                                    }
                                }
                                for(var i = 0; i < standard.length; i ++)
                                {
                                    for(var j = 0; j < parseInt(answerCount.innerText); j ++)
                                    {
                                        if(standard[i] == j)
                                        {
                                            $F("fvItem_cbxSelectAnswer_" + j).checked = true;
                                        }
                                    }
                                }
                                $F("contentAnswer").style.display = "none"; 
                                break;
                            }
                            case "判断":
                            {
                                 for(var i = 0; i < 12; i ++)
                                {
                                    if(i < parseInt(answerCount.innerText))
                                    {
                                        $F("fvItem_lblSelectAnswer_" + i).innerText = answers[i];
                                        // Hide checkbox
                                        $F("fvItem_cbxSelectAnswer_" + i).parentNode.style.display = "none";                                        
                                    }
                                    else
                                    {
                                        $F("fvItem_lblSelectAnswer_" + i).parentNode.parentNode.style.display = "none";
                                    }
                                }
                                for(var i = 0; i < standard.length; i ++)
                                {
                                    for(var j = 0; j < parseInt(answerCount.innerText); j ++)
                                    {
                                        if(standard[i] == j)
                                        {
                                            $F("fvItem_rbnSelectAnswer_" + j).checked = true;
                                        }
                                    }
                                } 
                                $F("contentAnswer").style.display = "none";                               
                                break; 
                            }  
                           case "填空":
                           {
                             $F("selectTr").style.display = "none"; 
                             $F("contentAnswer").style.display = ""; 
                             break;  
                           } 
                            case "简答":
                            case "论述":
                            {
                                $F("selectTr").style.display = "none";   
                                 $F("contentAnswer").style.display = "";                                        
                                 break;
                            }   
                            case "综合":
                            {
                                $F("selectTr").style.display = "none";    
                                 $F("contentAnswer").style.display = "";                                        
                                break;
                            }
                            default:
                            {
                                break;
                            }                        
                        }
                                                
                        break;
                    }
                    case "edit":
                    {
                        //var selectAnswers = $F("fvItem_hfSelectAnswer");
                        //var standardAnswer = $F("fvItem_hfStandardAnswer");
                        var itemTypes = $F("fvItem_ddlItemTypeEdit");
                        var answerCount = $F("fvItem_ddlAnswerCountEdit");
                        var hasPicture = $F("fvItem_ddlHasPictureEdit"); 
                        var answers = $F("fvItem_hfSelectAnswer").value.split("|");
                        var standard = $F("fvItem_hfStandardAnswer").value.split("|");
                        
                        itemTypes.onchange = itemEditTypeChanged;
                        answerCount.onchange = itemEditCountChanged;
                        hasPicture.onchange = itemEditHasPictureChanged;
                       
                        var itemPicture = hasPicture.options[hasPicture.selectedIndex].text;                                          
                        
                        if(itemPicture == "文本")
                        {
                        	$F("eWebEditorEdit").style.display = "none";
                        	$F("eWebEditorAnswerEdit").style.display = "none";
                        }
                        else
                        {
                        	$F("eWebEditorEdit").style.display = "";
                        	$F("eWebEditorAnswerEdit").style.display = "";
                        }
                        

                        switch(itemTypes.options[itemTypes.selectedIndex].text)
                        {
                            case "单选":
                            {
                                for(var i = 0; i < 12; i ++)
                                {
                                    var str = "eWebEditorEdit" + (i+1);
                                    if(i >= parseInt(answerCount.options[answerCount.selectedIndex].text))
                                    {
                                        $F("fvItem_rbnSelectAnswer_Edit_" + i).parentNode.parentNode.style.display = "none";
                                   }
                                    else
                                    {
                                        $F("fvItem_txtSelectAnswer_Edit_" + i).value = answers[i];
                                   }
                                   if(itemPicture == "文本")
                                   {
                                   		$F(str).style.display = "none";
                                   }
                                   else
                                   {
                                      $F(str).style.display = "";
                                   		window.frames[str].location ='/RailExamBao/ewebeditor/asp/ItemSelectEditor.asp?Mode=Edit&Index='+i;
                                   }

                                    // Hide CheckBox
                                    $F("fvItem_cbxSelectAnswer_Edit_" + i).style.display = "none";
                                    $F("fvItem_cbxSelectAnswer_Edit_" + i).nextSibling.style.display = "none";
                                }
                                for(var i = 0; i < standard.length; i ++)
                                {
                                    for(var j = 0; j < parseInt(answerCount.options[answerCount.selectedIndex].text); j ++)
                                    {
                                        if(standard[i] == j)
                                        {
                                            $F("fvItem_rbnSelectAnswer_Edit_" + j).checked = true;
                                        }
                                    }
                                }
                                 $F("contentAnswer").style.display = "none";   

                                break;
                            }
                            case "多选":
                            {
                                for(var i = 0; i < 12; i ++)
                                {
                                    var str = "eWebEditorEdit" + (i+1);
                                   if(i >= parseInt(answerCount.options[answerCount.selectedIndex].text))
                                    {
                                        $F("fvItem_rbnSelectAnswer_Edit_" + i).parentNode.parentNode.style.display = "none";
                                    }
                                    else
                                    {
                                        $F("fvItem_txtSelectAnswer_Edit_" + i).value = answers[i];
                                    }
                                    
                                   if(itemPicture == "文本")
                                   {
                                   		$F(str).style.display = "none";
                                   }
                                   else
                                   {
                                      $F(str).style.display = "";
                                   		window.frames[str].location ='/RailExamBao/ewebeditor/asp/ItemSelectEditor.asp?Mode=Edit&Index='+i;
                                   } 
                                   
                                    // Hide RadioButton
                                    $F("fvItem_rbnSelectAnswer_Edit_" + i).style.display = "none";
                                    $F("fvItem_rbnSelectAnswer_Edit_" + i).nextSibling.style.display = "none";
                                }
                                for(var i = 0; i < standard.length; i ++)
                                {
                                    for(var j = 0; j < parseInt(answerCount.options[answerCount.selectedIndex].text); j ++)
                                    {
                                        if(standard[i] == j)
                                        {
                                            $F("fvItem_cbxSelectAnswer_Edit_" + j).checked = true;
                                        }
                                    }
                                }
                                 $F("contentAnswer").style.display = "none";   

                                break;
                            }
                            case "判断":
                           {
                                 $F("fvItem_ddlAnswerCountEdit").disabled = true;
                                 $F("fvItem_ddlAnswerCountEdit").value = "2";

                                for(var i = 0; i < 12; i ++)
                                {
                                     var str = "eWebEditorEdit" + (i+1);
                                    if(i >= parseInt(answerCount.options[0].text))
                                    {
                                        $F("fvItem_rbnSelectAnswer_Edit_" + i).parentNode.parentNode.style.display = "none";
                                   }
                                   
                                   if(itemPicture == "文本")
                                   {
                                   		$F(str).style.display = "none";
                                   }

                                    // Hide CheckBox
                                    $F("fvItem_cbxSelectAnswer_Edit_" + i).style.display = "none";
                                    $F("fvItem_cbxSelectAnswer_Edit_" + i).nextSibling.style.display = "none";
                                }

                                $F("fvItem_txtSelectAnswer_Edit_0").style.display = "none";
                                $F("fvItem_txtSelectAnswer_Edit_1").style.display = "none";
                                
                                for(var i = 0; i < standard.length; i ++)
                                {
                                    for(var j = 0; j < parseInt(answerCount.options[answerCount.selectedIndex].text); j ++)
                                    {
                                        if(standard[i] == j)
                                        {
                                            $F("fvItem_rbnSelectAnswer_Edit_" + j).checked = true;
                                        }
                                    }
                                }    
                                 $F("contentAnswer").style.display = "none";                               
                                break;
                           }
                            case "填空":
                            {
                                 $F("fvItem_ddlAnswerCountEdit").disabled = true;
                                 $F("fvItem_ddlAnswerCountEdit").value = "2";
                                 $F("selectEditTr").style.display = "none";  
                                 $F("contentAnswer").style.display = "";
                                 break;
                            }    
                            case "简答":
                            case "论述":
                            {
                                 $F("fvItem_ddlAnswerCountEdit").disabled = true;
                                 $F("fvItem_ddlAnswerCountEdit").value = "2";
                                $F("selectEditTr").style.display = "none";      
                                $F("contentAnswer").style.display = "";                                   
                                 break;
                            }   
                           case "综合":
                            {
                                  $F("fvItem_ddlAnswerCountEdit").disabled = true;
                                 $F("fvItem_ddlAnswerCountEdit").value = "2";
                               $F("selectEditTr").style.display = "none";
                               $F("contentAnswer").style.display = "";                                             
                                break;
                            }  
                            default:
                            {
                                break;
                            }
                        }

                        break;
                    }
                    case "insert":
                    {
                        var itemTypes = $F("fvItem_ddlItemTypeInsert");
                        var answerCount = $F("fvItem_ddlAnswerCountInsert");
                        var hasPicture = $F("fvItem_ddlHasPictureInsert"); 
                        
                        if(search.indexOf("cid") != -1 && search.indexOf("bid") != -1)
                        {
                            var bookChapter = $F("fvItem_txtBookChapterInsert");
                            var bookChapterTree = window.opener.tvView;
                            var bookChapterPath = "";
                            
                            if(bookChapterTree)
                            {
                                var temp = bookChapterTree.get_selectedNode();
                                if(temp == null)
                                {
                                    return;
                                }
                                while(true)
                                {
                                    if(temp.getProperty("isBook") == "true")
                                    {
                                        bookChapterPath += temp.get_text();
                                        break;
                                    }
                                    bookChapterPath += temp.get_text() + "\\";
                                    temp = temp.get_parentNode();
                                }
                                bookChapter.value = bookChapterPath.split('\\').reverse().join("\\");
                            }
                        }
                        else
                        {
                            var categoryName = $F("fvItem_txtCategoryNameInsert");
                        }
                        
                        itemTypes.onchange = itemInsertTypeChanged;
                        answerCount.onchange = itemInsertCountChanged;
                        hasPicture.onchange = itemInsertHasPictureChanged;
                        
                        var itemPicture = hasPicture.options[hasPicture.selectedIndex].text;
                        
                        if(itemPicture == "文本")
                        {
                        	$F("eWebEditorInsert").style.display = "none";
                        	$F("eWebEditorAnswerInsert").style.display = "none";
                        }
                        else
                        {
                        	$F("eWebEditorInsert").style.display = "";
                        	$F("eWebEditorAnswerInsert").style.display= "";
                        }
                        
                        switch(itemTypes.options[itemTypes.selectedIndex].text)
                        {
                            case "单选":
                            {
                                for(var i = 0; i < 12; i ++)
                                {
                                    var str = "eWebEditorInsert" + (i+1);
                                    if(i >= parseInt(answerCount.options[answerCount.selectedIndex].text))
                                    {
                                        $F("fvItem_rbnSelectAnswer_Insert_" + i).parentNode.parentNode.style.display = "none";
                                   }
                                    
                                    
                                   if(itemPicture == "文本")
                                   {
                                   		$F(str).style.display = "none";
                                   }
                                   else
                                   {
                                      $F(str).style.display = "";
                                   		window.frames[str].location ='/RailExamBao/ewebeditor/asp/ItemSelectEditor.asp?Mode=Insert&Index='+i;
                                   }
                                    
                                   // Hide CheckBox
                                    $F("fvItem_cbxSelectAnswer_Insert_" + i).style.display = "none";
                                    $F("fvItem_cbxSelectAnswer_Insert_" + i).nextSibling.style.display = "none";
                                }
                                 $F("contentAnswer").style.display = "none";   
                                break;
                            }
                            case "多选":
                            {
                                for(var i = 0; i < 12; i ++)
                                {
                                     var str = "eWebEditorInsert" + (i+1);
                                   if(i >= parseInt(answerCount.options[answerCount.selectedIndex].text))
                                    {
                                        $F("fvItem_rbnSelectAnswer_Insert_" + i).parentNode.parentNode.style.display = "none";
                                   }                                   
                                    
                                  if(itemPicture == "文本")
                                   {
                                   		$F(str).style.display = "none";
                                   }
                                   else
                                   {
                                      $F(str).style.display = "";
                                   		window.frames[str].location ='/RailExamBao/ewebeditor/asp/ItemSelectEditor.asp?Mode=Insert&Index='+i;
                                   }
                                   // Hide RadioButton
                                    $F("fvItem_rbnSelectAnswer_Insert_" + i).style.display = "none";
                                    $F("fvItem_rbnSelectAnswer_Insert_" + i).nextSibling.style.display = "none";
                                }
                                 $F("contentAnswer").style.display = "none";   
                                break;
                            }
                            case "判断":
                           {
                                 $F("fvItem_ddlAnswerCountInsert").disabled = true;
                                 $F("fvItem_ddlAnswerCountInsert").value = "2";

                                for(var i = 0; i < 12; i ++)
                                {
                                     var str = "eWebEditorInsert" + (i+1);
                                    if(i >= parseInt(answerCount.options[0].text))
                                    {
                                        $F("fvItem_rbnSelectAnswer_Insert_" + i).parentNode.parentNode.style.display = "none";
                                   }
                                   
                                   if(itemPicture == "文本")
                                   {
                                   		$F(str).style.display = "none";
                                   }

                                    // Hide CheckBox
                                    $F("fvItem_cbxSelectAnswer_Insert_" + i).style.display = "none";
                                    $F("fvItem_cbxSelectAnswer_Insert_" + i).nextSibling.style.display = "none";
                                }

                                $F("fvItem_txtSelectAnswer_Insert_0").style.display = "none";
                                $F("fvItem_txtSelectAnswer_Insert_1").style.display = "none";
                                 $F("contentAnswer").style.display = "none";                                
                                break;
                           }
                            case "填空":
                            {
                                 $F("fvItem_ddlAnswerCountInsert").disabled = true;
                                 $F("fvItem_ddlAnswerCountInsert").value = "2";
                                $F("selectInsertTr").style.display = "none"; 
                                $F("contentAnswer").style.display = "";                                        
                                 break;
                            } 
                            case "简答":
                            case "论述":
                            {
                                 $F("fvItem_ddlAnswerCountInsert").disabled = true;
                                 $F("fvItem_ddlAnswerCountInsert").value = "2";
                                $F("selectInsertTr").style.display = "none";  
                                 $F("contentAnswer").style.display = "";                                         
                                 break;
                            }   
                           case "综合":
                            {
                                 $F("fvItem_ddlAnswerCountInsert").disabled = true;
                                 $F("fvItem_ddlAnswerCountInsert").value = "2";
                                $F("selectInsertTr").style.display = "none";   
                                 $F("contentAnswer").style.display = "";                                         
                                break;
                            }  
                            default:
                            {
                                break;
                            }
                        }

                        break;
                    }
                    default:
                    {
                        break;
                    }
                }
            }
        }
        
       //编辑时题型下拉列表事件处理函数 
        function itemEditTypeChanged()
        {
            var itemTypes = $F("fvItem_ddlItemTypeEdit");
            var answerCount = $F("fvItem_ddlAnswerCountEdit").value;
            var hasPicture = $F("fvItem_ddlHasPictureEdit").options[$F("fvItem_ddlHasPictureEdit").selectedIndex].text;
            
            switch(itemTypes.options[itemTypes.selectedIndex].text)
            {
                case "单选":
                {
                   $F("fvItem_ddlAnswerCountEdit").disabled = false;  
                   $F("contentAnswer").style.display = "none";  
                   $F("fvItem_lblSelectAnswer_Edit_0").style.display = "none";
                   $F("fvItem_lblSelectAnswer_Edit_1").style.display = "none";
                   $F("fvItem_txtSelectAnswer_Edit_0").style.display = "";
                   $F("fvItem_txtSelectAnswer_Edit_1").style.display = "";
                   $F("selectEditTr").style.display = ""; 
                    for(var i = 0; i < 12; i ++)
                    {
                        $F("fvItem_rbnSelectAnswer_Edit_" + i).style.display = "";
                        $F("fvItem_rbnSelectAnswer_Edit_" + i).nextSibling.style.display = "";
                        $F("fvItem_cbxSelectAnswer_Edit_" + i).style.display = "none";
                        $F("fvItem_cbxSelectAnswer_Edit_" + i).nextSibling.style.display = "none";
                    }
                    $F("contentAnswer").style.display = "none"; 
                    break;
                }
                case "多选":
                {
                   $F("fvItem_ddlAnswerCountEdit").disabled = false;  
                   $F("fvItem_lblSelectAnswer_Edit_0").style.display = "none";
                   $F("fvItem_lblSelectAnswer_Edit_1").style.display = "none";
                   $F("fvItem_txtSelectAnswer_Edit_0").style.display = "";
                   $F("fvItem_txtSelectAnswer_Edit_1").style.display = "";                  
                   $F("selectEditTr").style.display = ""; 
                    for(var i = 0; i < 12; i ++)
                    {
                        $F("fvItem_rbnSelectAnswer_Edit_" + i).style.display = "none";
                        $F("fvItem_rbnSelectAnswer_Edit_" + i).nextSibling.style.display = "none";
                        $F("fvItem_cbxSelectAnswer_Edit_" + i).style.display = "";
                        $F("fvItem_cbxSelectAnswer_Edit_" + i).nextSibling.style.display = "";
                    }
                    $F("contentAnswer").style.display = "none"; 
                    break;
                }
                case "判断" :
                {
                   $F("selectEditTr").style.display = ""; 
                   for(var i = 0; i < 12; i ++)
                    {
                        $F("fvItem_cbxSelectAnswer_Edit_" + i).style.display = "none";
                        $F("fvItem_cbxSelectAnswer_Edit_" + i).nextSibling.style.display = "none";
                    }

                    $F("fvItem_txtSelectAnswer_Edit_0").style.display = "none";
                    $F("fvItem_txtSelectAnswer_Edit_1").style.display = "none";

                    for(var i=2;i<12;i++)
                    {
                        $F("fvItem_rbnSelectAnswer_Edit_" + i).style.display = "none";
                        $F("fvItem_rbnSelectAnswer_Edit_" + i).nextSibling.style.display = "none";
                    }
                    
                    var theTable = $F("fvItem_rbnSelectAnswer_Edit_0").parentNode.parentNode.parentNode;
            
                    for(var i = 0; i < 12; i ++)
                    {
                        if(i < parseInt(answerCount.options[0].text))
                        {
                            theTable.rows[i].style.display = "";
                        }
                        else
                        {
                            theTable.rows[i].style.display = "none";
                        }
                    }
                     $F("fvItem_ddlAnswerCountEdit").disabled = true;
                     $F("fvItem_ddlAnswerCountEdit").value = "2";
                     $F("contentAnswer").style.display = "none"; 
                    break;
                } 
               case "填空":
                { 
                        $F("fvItem_ddlAnswerCountEdit").disabled = true; 
                        $F("selectEditTr").style.display = "none";
                        $F("contentAnswer").style.display = ""; 
                        break;
                } 
                case "简答":
                case "论述":
                { 
                        $F("fvItem_ddlAnswerCountEdit").disabled = true; 
                        $F("selectEditTr").style.display = "none";
                        $F("contentAnswer").style.display = ""; 
                        break;
                }
                case "综合":
                {
                        $F("fvItem_ddlAnswerCountEdit").disabled = true; 
                        $F("selectEditTr").style.display = "none";
                        $F("contentAnswer").style.display = ""; 
                        break; 
                }  
                default:
                {
                    break;
                }
            }
        }
       
       function itemEditHasPictureChanged()
       {
         var itemTypes = $F("fvItem_ddlItemTypeEdit");
         var hasPicture = $F("fvItem_ddlHasPictureEdit"); 
          var answerCount = $F("fvItem_ddlAnswerCountEdit");
          var itemPicture = hasPicture.options[hasPicture.selectedIndex].text;
          var nowCount =parseInt(answerCount.options[answerCount.selectedIndex].text)
          if(itemPicture == "文本")
          {		
              $F("eWebEditorEdit").style.display = "none";
              window.frames["eWebEditorEdit"].form1.submit();
              $F("fvItem_txtContentEdit").value = window.frames["eWebEditorEdit"].form1.myField.value;
              
              $F("eWebEditorAnswerEdit").style.display = "none";
              window.frames["eWebEditorAnswerEdit"].form1.submit();
              $F("fvItem_txtContentAnswer").value = window.frames["eWebEditorAnswerEdit"].form1.myField.value;
            
             var type = itemTypes.options[itemTypes.selectedIndex].text;
              if( type== "单选" || type == "多选")                
              {
                  for(var i = 0; i < 12; i++)
                  {
                      var strFrame="eWebEditorEdit" + (i+1);
                      $F(strFrame).style.display = "none";
                      window.frames[strFrame].form1.submit();
                      $F("fvItem_txtSelectAnswer_Edit_"+i).value = window.frames[strFrame].form1.myField.value;
                  }
              }
          }
          else
          {
              
              $F("eWebEditorEdit").style.display = "";
             window.frames["eWebEditorEdit"].location ='/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Edit?Type=Content';
             
             $F("eWebEditorAnswerEdit").style.display = "";
             window.frames["eWebEditorAnswerEdit"].location ='/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Edit?Type=Answer';
            var type = itemTypes.options[itemTypes.selectedIndex].text;
              if( type== "单选" || type == "多选")                
              {
                  for(var i = 0; i < 12; i++)
                  {
                      var strFrame="eWebEditorEdit" + (i+1);
                      $F(strFrame).style.display = "";
                      window.frames[strFrame].location ='/RailExamBao/ewebeditor/asp/ItemSelectEditor.asp?Mode=Edit&Index='+i;
                  }
              }
          }
       } 

       //编辑按钮点击事件处理函数 
        function itemEditCountChanged()
        {
            var itemTypes = $F("fvItem_ddlItemTypeEdit");
            var answerCount = $F("fvItem_ddlAnswerCountEdit");
            var theTable = $F("fvItem_rbnSelectAnswer_Edit_0").parentNode.parentNode.parentNode;
            
            for(var i = 0; i < 12; i ++)
            {
                if(i < parseInt(answerCount.options[answerCount.selectedIndex].text))
                {
                    theTable.rows[i].style.display = "";
                }
                else
                {
                    theTable.rows[i].style.display = "none";
                }
            }
        }
        
        function updateBtnClientClicked()
        {
            // 验证
            // 答案部分验证与获取
            var hasPicture = $F("fvItem_ddlHasPictureEdit"); 
            var itemPicture = hasPicture.options[hasPicture.selectedIndex].text;
            var Content=$F("fvItem_txtContentEdit");
            if(itemPicture == "图片")
            {
	            window.frames["eWebEditorEdit"].form1.submit();
	            var str = window.frames["eWebEditorEdit"].form1.myField.value;
	            Content.value  = str;
	        }
	        
            if(Content&&Content.value.length==0)
            {
                 alert("试题内容不能未空！");                            
                 return false;
            }
            var itemTypes = $F("fvItem_ddlItemTypeEdit");
            var answerCount = $F("fvItem_ddlAnswerCountEdit");
            var selectAnswer = $F("fvItem_hfSelectAnswer");
            var standardAnswer = $F("fvItem_hfStandardAnswer");
            
            selectAnswer.value = "";
            standardAnswer.value = "";
            switch(itemTypes.options[itemTypes.selectedIndex].text)
            {
                case "单选":
                {
                    for(var i = 0; i < parseInt(answerCount.options[answerCount.selectedIndex].text); i ++)
                    {
                        if(itemPicture == "图片")
            						{
	                        var strFrame="eWebEditorEdit" + (i+1);
	                        window.frames[strFrame].form1.submit(); 
	                        str =  window.frames[strFrame].form1.myField.value;
	                        $F("fvItem_txtSelectAnswer_Edit_" + i).value = str;
                        }
                        if(!$F("fvItem_txtSelectAnswer_Edit_" + i).value)
                        {
                            alert("选项" + (i + 1) + "的内容不能未空！");
                            if(itemPicture == "图片")
            								{
                               window.frames["eWebEditorEdit"].location = '/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Edit';
	                            for(var j=0;j<i;j++)
	                            {
	                                strFrame="eWebEditorEdit" + (j+1); 
	                                window.frames[strFrame].location ='/RailExamBao/ewebeditor/asp/ItemSelectEditor.asp?Mode=Edit&Index='+j ;
	                            }
	                          } 
                            return false;
                        }
                        selectAnswer.value += $F("fvItem_txtSelectAnswer_Edit_" + i).value + "|";
                        if($F("fvItem_rbnSelectAnswer_Edit_" + i).checked)
                        {
                            standardAnswer.value += i + "|";
                        }
                    }
                    if(! standardAnswer.value)
                    {   
                        alert("请选择一个正确答案！");
                        if(itemPicture == "图片")
            						{
		                        window.frames["eWebEditorEdit"].location = '/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Edit'   ;
		                        for(var i = 0; i < parseInt(answerCount.options[answerCount.selectedIndex].text); i ++)
		                        {
		                            strFrame="eWebEditorEdit" + (i+1); 
		                            window.frames[strFrame].location ='/RailExamBao/ewebeditor/asp/ItemSelectEditor.asp?Mode=Edit&Index='+i;
		                        }
                        }                               
                        return false;
                    }
                    selectAnswer.value = selectAnswer.value.substring(0, selectAnswer.value.length-1);
                    standardAnswer.value = standardAnswer.value.substring(0, standardAnswer.value.length-1);
                    
                    break;
                }
                case "多选":
                {
                    for(var i = 0; i < parseInt(answerCount.options[answerCount.selectedIndex].text); i ++)
                    {
                        if(itemPicture == "图片")
            						{
		                        var strFrame="eWebEditorEdit" + (i+1);
		                        window.frames[strFrame].form1.submit(); 
		                        str =  window.frames[strFrame].form1.myField.value;
		                        $F("fvItem_txtSelectAnswer_Edit_" + i).value = str;
                        }
                        if(! $F("fvItem_txtSelectAnswer_Edit_" + i).value)
                        {
                            alert("选项" + (i + 1) + "的内容不能未空！");
                            if(itemPicture == "图片")
            								{
		                            window.frames["eWebEditorEdit"].location = '/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Edit'   ;
		                            for(var j=0;j<i;j++)
			                           {
			                                strFrame="eWebEditorEdit" + (j+1); 
			                                window.frames[strFrame].location ='/RailExamBao/ewebeditor/asp/ItemSelectEditor.asp?Mode=Edit&Index='+j;
			                           } 
                            }                          
                           return false;
                        }
                        selectAnswer.value += $F("fvItem_txtSelectAnswer_Edit_" + i).value + "|";
                        if($F("fvItem_cbxSelectAnswer_Edit_" + i).checked)
                        {
                            standardAnswer.value += i + "|";
                        }
                    }
                    if(! standardAnswer.value)
                    {   
                        alert("请选择一个正确答案！");
                        if(itemPicture == "图片")
            			{
		                        window.frames["eWebEditorEdit"].location = '/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Edit'  ;
		                        for(var i = 0; i < parseInt(answerCount.options[answerCount.selectedIndex].text); i ++)
		                        {
		                            strFrame="eWebEditorEdit" + (i+1); 
		                            window.frames[strFrame].location ='/RailExamBao/ewebeditor/asp/ItemSelectEditor.asp?Mode=Edit&Index='+i ;
		                        }
                        }                           
                        return false;
                    }
                    selectAnswer.value = selectAnswer.value.substring(0, selectAnswer.value.length-1);
                    standardAnswer.value = standardAnswer.value.substring(0, standardAnswer.value.length-1);
                    
                    break;
                }
                case "判断":
                { 
                    for(var i=0;i<2;i++)
                    {
                       if($F("fvItem_rbnSelectAnswer_Edit_" + i).checked)
                        {
                            standardAnswer.value += i + "|";
                        }
                    }
                    selectAnswer.value = "对|错";
//                    alert( selectAnswer.value );
                    standardAnswer.value = standardAnswer.value.substring(0, standardAnswer.value.length-1);
                   break;
               } 
                case "填空":
               {
                    var StandardAnswer=$F("fvItem_txtContentAnswer");
                    if(itemPicture == "图片")
                    {
	                    window.frames["eWebEditorAnswerEdit"].form1.submit();
	                    var str = window.frames["eWebEditorAnswerEdit"].form1.myField.value;
	                    StandardAnswer.value  = str;
	                }
                    selectAnswer.value="";
                    standardAnswer.value =$F("fvItem_txtContentAnswer").value;
                   break;
               }
               case "简答":
               	case "论述":
               {
                    var StandardAnswer=$F("fvItem_txtContentAnswer");
                    if(itemPicture == "图片")
                    {
	                    window.frames["eWebEditorAnswerEdit"].form1.submit();
	                    var str = window.frames["eWebEditorAnswerEdit"].form1.myField.value;
	                    StandardAnswer.value  = str;
	                }                   
	                selectAnswer.value="";
                    standardAnswer.value =$F("fvItem_txtContentAnswer").value;
                   break;
               }
               case "综合":
               {
                     var StandardAnswer=$F("fvItem_txtContentAnswer");
                    if(itemPicture == "图片")
                    {
	                    window.frames["eWebEditorAnswerEdit"].form1.submit();
	                    var str = window.frames["eWebEditorAnswerEdit"].form1.myField.value;
	                    StandardAnswer.value  = str;
	                }                   
                    selectAnswer.value="";
                    standardAnswer.value =$F("fvItem_txtContentAnswer").value;
                    break;
               }                
               default:
                {
                    break;
                }
            }
           //alert(selectAnswer.value);
           //alert(standardAnswer.value);
            return true;
        }
        
       //新增 时题型下拉列表事件处理函数 
        function itemInsertTypeChanged()
        {
            var itemTypes = $F("fvItem_ddlItemTypeInsert");
            var answerCount = $F("fvItem_ddlAnswerCountInsert");
            var hasPicture =  $F("fvItem_ddlHasPictureInsert").options[$F("fvItem_ddlHasPictureInsert").selectedIndex].text
            
            switch(itemTypes.options[itemTypes.selectedIndex].text)
            {
                case "单选":
                {
                   $F("fvItem_ddlAnswerCountInsert").disabled = false;  
                   $F("fvItem_txtSelectAnswer_Insert_0").style.display = "";
                   $F("fvItem_txtSelectAnswer_Insert_1").style.display = ""; 
                   $F("selectInsertTr").style.display = ""; 
                   $F("fvItem_lblSelectAnswer_Insert_0").style.display = "none"; 
                   $F("fvItem_lblSelectAnswer_Insert_1").style.display = "none"; 
                    for(var i = 0; i < 12; i ++)
                    {
                        $F("fvItem_rbnSelectAnswer_Insert_" + i).style.display = "";
                        $F("fvItem_rbnSelectAnswer_Insert_" + i).nextSibling.style.display = "";
                        $F("fvItem_cbxSelectAnswer_Insert_" + i).style.display = "none";
                        $F("fvItem_cbxSelectAnswer_Insert_" + i).nextSibling.style.display = "none";
                    }
                    $F("contentAnswer").style.display="none";
                    break;
                }
                case "多选":
                {
                   $F("fvItem_lblSelectAnswer_Insert_0").style.display = "none"; 
                   $F("fvItem_lblSelectAnswer_Insert_1").style.display = "none"; 
                   $F("fvItem_txtSelectAnswer_Insert_0").style.display = "";
                   $F("fvItem_txtSelectAnswer_Insert_1").style.display = "";                   
                   $F("fvItem_ddlAnswerCountInsert").disabled = false; 
                   $F("selectInsertTr").style.display = ""; 
                   for(var i = 0; i < 12; i ++)
                    {
                        $F("fvItem_rbnSelectAnswer_Insert_" + i).style.display = "none";
                        $F("fvItem_rbnSelectAnswer_Insert_" + i).nextSibling.style.display = "none";
                        $F("fvItem_cbxSelectAnswer_Insert_" + i).style.display = "";
                        $F("fvItem_cbxSelectAnswer_Insert_" + i).nextSibling.style.display = "";
                    }
                    $F("contentAnswer").style.display="none";

                    break;
                }
                case "判断" :
                {
                   $F("selectInsertTr").style.display = ""; 
                   for(var i = 0; i < 12; i ++)
                    {
                        $F("fvItem_cbxSelectAnswer_Insert_" + i).style.display = "none";
                        $F("fvItem_cbxSelectAnswer_Insert_" + i).nextSibling.style.display = "none";
                    }

                    $F("fvItem_txtSelectAnswer_Insert_0").style.display = "none";
                    $F("fvItem_txtSelectAnswer_Insert_1").style.display = "none";

                    for(var i=2;i<12;i++)
                    {
                        $F("fvItem_rbnSelectAnswer_Insert_" + i).style.display = "none";
                        $F("fvItem_rbnSelectAnswer_Insert_" + i).nextSibling.style.display = "none";
                    }
                                        
                    var theTable = $F("fvItem_rbnSelectAnswer_Insert_0").parentNode.parentNode.parentNode;
            
                    for(var i = 0; i < 12; i ++)
                    {
                        if(i < 2)
                        {
                            theTable.rows[i].style.display = "";
                        }
                        else
                        {
                            theTable.rows[i].style.display = "none";
                        }
                    }
                     $F("fvItem_ddlAnswerCountInsert").disabled = true;
                     $F("fvItem_ddlAnswerCountInsert").value = "2";
                     $F("fvItem_rbnSelectAnswer_Insert_0").style.display="";
                     $F("fvItem_rbnSelectAnswer_Insert_1").style.display="";
                     $F("fvItem_lblSelectAnswer_Insert_0").style.display = ""; 
                     $F("fvItem_lblSelectAnswer_Insert_1").style.display = ""; 
                     $F("contentAnswer").style.display="none";
                     break;
                } 
               case "填空":
                { 
                        $F("fvItem_ddlAnswerCountInsert").disabled = true; 
                        $F("selectInsertTr").style.display = "none";
                        $F("contentAnswer").style.display="";
                        break;
                } 
                case "简答":
                case "论述":
                { 
                        $F("fvItem_ddlAnswerCountInsert").disabled = true; 
                        $F("selectInsertTr").style.display = "none";
                        $F("contentAnswer").style.display="";
                        break;
                }
                case "综合":
                {
                        $F("fvItem_ddlAnswerCountInsert").disabled = true; 
                        $F("selectInsertTr").style.display = "none";
                        $F("contentAnswer").style.display="";
                        break; 
                }  
                default:
                {
                    break;
                }
            }
        }
        
       //试题数目更改事件处理函数 
        function itemInsertCountChanged()
        {
            var itemTypes = $F("fvItem_ddlItemTypeInsert");
            var answerCount = $F("fvItem_ddlAnswerCountInsert");
            var theTable = $F("fvItem_rbnSelectAnswer_Insert_0").parentNode.parentNode.parentNode;
            
            for(var i = 0; i < 12; i ++)
            {
                if(i < parseInt(answerCount.options[answerCount.selectedIndex].text))
                {
                    theTable.rows[i].style.display = "";
                }
                else
                {
                    theTable.rows[i].style.display = "none";
                }
            }
        }
        
        
        function itemInsertHasPictureChanged()
       {
		  var hasPicture = $F("fvItem_ddlHasPictureInsert"); 
          var itemPicture = hasPicture.options[hasPicture.selectedIndex].text;
            var itemTypes = $F("fvItem_ddlItemTypeInsert");
		if(itemPicture == "文本")
          {		
              $F("eWebEditorInsert").style.display = "none";
              window.frames["eWebEditorInsert"].form1.submit();
              $F("fvItem_txtContentInsert").value = window.frames["eWebEditorInsert"].form1.myField.value;
              
               $F("eWebEditorAnswerInsert").style.display = "none";
              window.frames["eWebEditorAnswerInsert"].form1.submit();
              $F("fvItem_txtContentAnswer").value = window.frames["eWebEditorAnswerInsert"].form1.myField.value;
            var type = itemTypes.options[itemTypes.selectedIndex].text;
              if( type== "单选" || type == "多选")                
              {             
                  for(var i = 0; i < 12; i++)
                  {
                      var strFrame="eWebEditorInsert" + (i+1);
                      $F(strFrame).style.display = "none";
                      window.frames[strFrame].form1.submit();
                      $F("fvItem_txtSelectAnswer_Insert_"+i).value = window.frames[strFrame].form1.myField.value;
                  }
              }
          }
          else
          {
              
              $F("eWebEditorInsert").style.display = "";
             window.frames["eWebEditorInsert"].location ='/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Insert&Type=Content';
             
              $F("eWebEditorAnswerInsert").style.display = "";
             window.frames["eWebEditorAnswerInsert"].location ='/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Insert&Type=Answer';
             var type = itemTypes.options[itemTypes.selectedIndex].text;
              if( type== "单选" || type == "多选")                
              {
                  for(var i = 0; i < 12; i++)
                  {
                      var strFrame="eWebEditorInsert" + (i+1);
                      $F(strFrame).style.display = "";
                      window.frames[strFrame].location ='/RailExamBao/ewebeditor/asp/ItemSelectEditor.asp?Mode=Insert&Index='+i;
                  }
              }

          }
       } 
        
       //插入按钮点击事件处理函数 
        function insertBtnClientClicked()
        {
            // 答案部分验证与获取          
            var hasPicture = $F("fvItem_ddlHasPictureInsert");          
            var itemPicture = hasPicture.options[hasPicture.selectedIndex].text;
            var Content=$F("fvItem_txtContentInsert");
            if(itemPicture == "图片")
            {
		            window.frames["eWebEditorInsert"].form1.submit();
		            var str = window.frames["eWebEditorInsert"].form1.myField.value;
		            Content.value  = str;
            }
            if(Content&&Content.value.length==0)
            {
             alert("试题内容不能未空！");
             return false;
            }
            
            var itemTypes = $F("fvItem_ddlItemTypeInsert");
            var answerCount = $F("fvItem_ddlAnswerCountInsert");
            var selectAnswer = $F("fvItem_hfSelectAnswer");
            var standardAnswer = $F("fvItem_hfStandardAnswer");
            
            selectAnswer.value = "";
            standardAnswer.value = "";
            switch(itemTypes.options[itemTypes.selectedIndex].text)
            {
                case "单选":
                {
                    for(var i = 0; i < parseInt(answerCount.options[answerCount.selectedIndex].text); i ++)
                    {
                        if(itemPicture == "图片")
            			{
	                        var strFrame="eWebEditorInsert" + (i+1);
	                        window.frames[strFrame].form1.submit(); 
	                        str =  window.frames[strFrame].form1.myField.value;
	                        $F("fvItem_txtSelectAnswer_Insert_" + i).value = str;
                        }
                        if(! $F("fvItem_txtSelectAnswer_Insert_" + i).value)
                        {
                            alert("选项" + (i + 1) + "的内容不能未空！");
                            {
		                             window.frames["eWebEditorInsert"].location = '/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Insert'  ;
		                            for(var j=0;j<i;j++)
		                           {
		                                strFrame="eWebEditorInsert" + (j+1); 
		                                window.frames[strFrame].location ='/RailExamBao/ewebeditor/asp/ItemSelectEditor.asp?Mode=Insert&Index='+j;
		                           }
		                        }
                            return false;
                        }
                        selectAnswer.value += $F("fvItem_txtSelectAnswer_Insert_" + i).value + "|";
                        if($F("fvItem_rbnSelectAnswer_Insert_" + i).checked)
                        {
                            standardAnswer.value += i + "|";
                        }
                    }
                    if(! standardAnswer.value)
                    {   
                       alert("请选择一个正确答案！");
                       if(itemPicture == "图片")
            			{
	                        window.frames["eWebEditorInsert"].location = '/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Insert'  ;
	                        for(var i = 0; i < parseInt(answerCount.options[answerCount.selectedIndex].text); i ++)
	                        {
	                            strFrame="eWebEditorInsert" + (i+1); 
	                            window.frames[strFrame].location ='/RailExamBao/ewebeditor/asp/ItemSelectEditor.asp?Mode=Insert&Index='+i ;
	                        } 
                       }
                       return false;
                    }
                    selectAnswer.value = selectAnswer.value.substring(0, selectAnswer.value.length-1);
                    standardAnswer.value = standardAnswer.value.substring(0, standardAnswer.value.length-1);
                    
                    break;
                }
                case "多选":
                {
                    for(var i = 0; i < parseInt(answerCount.options[answerCount.selectedIndex].text); i ++)
                    {
                       if(itemPicture == "图片")
            		  {
	                         var strFrame="eWebEditorInsert" + (i+1);
	                         window.frames[strFrame].form1.submit(); 
	                         str =  window.frames[strFrame].form1.myField.value;
	                        $F("fvItem_txtSelectAnswer_Insert_" + i).value = str;                      
                        }
                        if(! $F("fvItem_txtSelectAnswer_Insert_" + i).value)
                        {
                            alert("选项" + (i + 1) + "的内容不能未空！");
                            if(itemPicture == "图片")
            				{
	                             window.frames["eWebEditorInsert"].location = '/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Insert'  ;
		                            for(var j=0;j<i;j++)
		                           {
		                                strFrame="eWebEditorInsert" + (j+1); 
		                                window.frames[strFrame].location ='/RailExamBao/ewebeditor/asp/ItemSelectEditor.asp?Mode=Insert&Index='+j;
		                           } 
		                       }
                           return false;
                        }
                        selectAnswer.value += $F("fvItem_txtSelectAnswer_Insert_" + i).value + "|";
                        if($F("fvItem_cbxSelectAnswer_Insert_" + i).checked)
                        {
                            standardAnswer.value += i + "|";
                        }
                    }
                    if(! standardAnswer.value)
                    {   
                        alert("请选择一个正确答案！");                        
                        if(itemPicture == "图片")
            			{
		                        window.frames["eWebEditorInsert"].location = '/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Insert'  ;
		                        for(var i = 0; i < parseInt(answerCount.options[answerCount.selectedIndex].text); i ++)
		                        {
		                            strFrame="eWebEditorInsert" + (i+1); 
		                            window.frames[strFrame].location ='/RailExamBao/ewebeditor/asp/ItemSelectEditor.asp?Mode=Insert&Index='+i ;
		                        }
                        }                         
                        return false;
                    }
                    selectAnswer.value = selectAnswer.value.substring(0, selectAnswer.value.length-1);
                    standardAnswer.value = standardAnswer.value.substring(0, standardAnswer.value.length-1);
                    
                    break;
                }
                case "判断":
                { 
                    for(var i=0;i<2;i++)
                    {
                       if($F("fvItem_rbnSelectAnswer_Insert_" + i).checked)
                        {
                            standardAnswer.value += i + "|";
                        }
                    }
                    selectAnswer.value = "对|错";
                    standardAnswer.value = standardAnswer.value.substring(0, standardAnswer.value.length-1);
                   break;
               }  
                case "填空":
               {
                     var StandardAnswer=$F("fvItem_txtContentAnswer");
                    if(itemPicture == "图片")
                    {
	                    window.frames["eWebEditorAnswerInsert"].form1.submit();
	                    var str = window.frames["eWebEditorAnswerInsert"].form1.myField.value;
	                    StandardAnswer.value  = str;
	                }                   
                   selectAnswer.value="";
                    standardAnswer.value =$F("fvItem_txtContentAnswer").value;
                   break;
               }
               case "简答":
               case "论述":
               {
                     var StandardAnswer=$F("fvItem_txtContentAnswer");
                    if(itemPicture == "图片")
                    {
	                    window.frames["eWebEditorAnswerInsert"].form1.submit();
	                    var str = window.frames["eWebEditorAnswerInsert"].form1.myField.value;
	                    StandardAnswer.value  = str;
	                } 
	                selectAnswer.value="";
                    standardAnswer.value =$F("fvItem_txtContentAnswer").value;
                   break;
               }
               case "综合":
               {
                     var StandardAnswer=$F("fvItem_txtContentAnswer");
                    if(itemPicture == "图片")
                    {
	                    window.frames["eWebEditorAnswerInsert"].form1.submit();
	                    var str = window.frames["eWebEditorAnswerInsert"].form1.myField.value;
	                    StandardAnswer.value  = str;
	                } 
	                selectAnswer.value="";
                    standardAnswer.value =$F("fvItem_txtContentAnswer").value;
                    break;
               }
                default:
                {
                    break;
                }
            }
            //showHiddenFields(window.document);
            
            return true;
        }
        
        //选择试题分类
        function selectItemCategory()
        {
            var selectedItemCategory = window.showModalDialog('../Common/SelectItemCategory.aspx', 
                '', 'help:no; status:no; dialogWidth:300px;dialogHeight:600px');
                
            if(selectedItemCategory)
            {
                $F("fvItem_hfCategoryId").value = selectedItemCategory.split('|')[0];
                
                if($F("fvItem_txtCategoryNameInsert"))
                {
                    $F("fvItem_txtCategoryNameInsert").value = selectedItemCategory.split('|')[1];
                }
                else
                {
                    $F("fvItem_txtCategoryNameEdit").value = selectedItemCategory.split('|')[1];
                }
            }
        }

       //选择章节
        function selectBookChapter()
        {
        	var selectedBookChapter = window.showModalDialog('../Common/SelectBookChapter.aspx?itemType=1', 
                '', 'help:no; status:no; dialogWidth:320px;dialogHeight:600px');

            if(selectedBookChapter)
            {
                $F("fvItem_hfBookId").value = selectedBookChapter.split('|')[0];
                $F("fvItem_hfChapterId").value = selectedBookChapter.split('|')[1];
                
                if($F("fvItem_txtBookChapterInsert"))
                {
                    $F("fvItem_txtBookChapterInsert").value = selectedBookChapter.split('|')[2];
                }
                else
                {
                    $F("fvItem_txtBookChapterEdit").value = selectedBookChapter.split('|')[2];
                }
            }
        }
    </script>

    <script src="../Common/JS/JSHelper.js" type="text/javascript"></script>

</head>
<body onload="init();">
    <form id="form1" runat="server">
        <div id="page">
            <div id="content">
                <asp:FormView ID="fvItem" runat="server" DataKeyNames="ItemId" DataSourceID="odsItem"
                    EnableViewState="true" OnItemCommand="fvItem_ItemCommand"  OnItemUpdated="fvItem_ItemUpdated">
                    <ItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 10%;">
                                    教材章节
                                </td>
                                <td colspan="7">
                                    <asp:Label ID="lblBookChapter" runat="server" Text='<%# Eval("ChapterPath") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    试题类型
                                </td>
                                <td style="width: 15%;">
                                    <asp:Label ID="lblItemType" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label>
                                </td>
                                <td>
                                    试题难度
                                </td>
                                <td style="width: 15%;">
                                    <asp:Label ID="lblItemDifficulty" runat="server" Text='<%# Eval("DifficultyName") %>'></asp:Label>
                                </td>
                                <td>
                                    缺省分数
                                </td>
                                <td style="width: 15%;">
                                    <asp:Label ID="lblScore" runat="server" Text='<%# Eval("Score") %>'></asp:Label>
                                </td>
                                <td>
                                    选项数目
                                </td>
                                <td style="width: 15%;">
                                    <asp:Label ID="lblAnswerCount" runat="server" Text='<%# Eval("AnswerCount") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    答题时间
                                </td>
                                <td>
                                    <asp:Label ID="lblCompleteTime" runat="server" Text='<%# Eval("CompleteTime") %>'></asp:Label>
                                </td>
                                <td>
                                    试题来源
                                </td>
                                <td>
                                    <asp:Label ID="lblSource" runat="server" Text='<%# Eval("Source") %>'></asp:Label>
                                </td>
                                <td>
                                    试题版本
                                </td>
                                <td>
                                    <asp:Label ID="lblVersion" runat="server" Text='<%# Eval("Version") %>'></asp:Label>
                                </td>
                                <td>
                                    创建时间
                                </td>
                                <td>
                                    <asp:Label ID="lblCreateTime" runat="server" Text='<%# Eval("CreateTime") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    过期日期
                                </td>
                                <td>
                                    <asp:Label ID="lblOutDateDate" runat="server" Text='<%# Eval("OutDateDateString") %>'></asp:Label>
                                </td>
                                <td>
                                    试题状态
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("StatusId") %>'></asp:Label>
                                </td>
                                <td>
                                    出题人
                                </td>
                                <td>
                                    <asp:Label ID="lblCreatePerson" runat="server" Text='<%# Eval("CreatePerson") %>'></asp:Label>
                                </td>
                                <td>
                                    试题用途
                                </td>
                                <td>
                                    <asp:Label ID="lblUsage" runat="server" Text='<%# (int)Eval("UsageId") == 0 ? "用作练习和考试" : "仅用作考试" %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    试题内容
                                </td>
                                <td colspan="7">
                                    <asp:Label ID="lblContent" runat="server" Text='<%# Eval("Content") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr id="contentAnswer" style="display: none">
                                <td>
                                    试题答案
                                </td>
                                <td colspan="7">
                                    <asp:Label ID="lblContentAnswer" runat="server" Text='<%# Eval("StandardAnswer") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr id="selectTr">
                                <td>
                                    候选项
                                </td>
                                <td colspan="7" style="padding: 0; border-width: 0;">
                                    <table class="contentTable">
                                        <tr>
                                            <td style="width: 10%; white-space: nowrap;">
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_0" runat="server" Text="A."
                                                    Enabled="False" />
                                                <asp:CheckBox ID="cbxSelectAnswer_0" runat="server" Text="A." Enabled="False" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSelectAnswer_0" runat="server" Width="100%"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; white-space: nowrap;">
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_1" runat="server" Text="B."
                                                    Enabled="False" />
                                                <asp:CheckBox ID="cbxSelectAnswer_1" runat="server" Text="B." Enabled="False" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSelectAnswer_1" runat="server" Width="100%"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; white-space: nowrap;">
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_2" runat="server" Text="C."
                                                    Enabled="False" />
                                                <asp:CheckBox ID="cbxSelectAnswer_2" runat="server" Text="C." Enabled="False" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSelectAnswer_2" runat="server" Width="100%"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; white-space: nowrap;">
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_3" runat="server" Text="D."
                                                    Enabled="False" />
                                                <asp:CheckBox ID="cbxSelectAnswer_3" runat="server" Text="D." Enabled="False" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSelectAnswer_3" runat="server" Width="100%"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; white-space: nowrap;">
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_4" runat="server" Text="E."
                                                    Enabled="False" />
                                                <asp:CheckBox ID="cbxSelectAnswer_4" runat="server" Text="E." Enabled="False" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSelectAnswer_4" runat="server" Width="100%"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; white-space: nowrap;">
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_5" runat="server" Text="F."
                                                    Enabled="False" />
                                                <asp:CheckBox ID="cbxSelectAnswer_5" runat="server" Text="F." Enabled="False" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSelectAnswer_5" runat="server" Width="100%"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_6" runat="server" Text="G."
                                                    Enabled="False" />
                                                <asp:CheckBox ID="cbxSelectAnswer_6" runat="server" Text="G." Enabled="False" /></td>
                                            <td>
                                                <asp:Label ID="lblSelectAnswer_6" runat="server" Width="100%"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; white-space: nowrap;">
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_7" runat="server" Text="H."
                                                    Enabled="False" />
                                                <asp:CheckBox ID="cbxSelectAnswer_7" runat="server" Text="H." Enabled="False" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSelectAnswer_7" runat="server" Width="100%"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; white-space: nowrap;">
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_8" runat="server" Text="I."
                                                    Enabled="False" />
                                                <asp:CheckBox ID="cbxSelectAnswer_8" runat="server" Text="I." Enabled="False" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSelectAnswer_8" runat="server" Width="100%"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; white-space: nowrap;">
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_9" runat="server" Text="J."
                                                    Enabled="False" />
                                                <asp:CheckBox ID="cbxSelectAnswer_9" runat="server" Text="J." Enabled="False" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSelectAnswer_9" runat="server" Width="100%"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; white-space: nowrap;">
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_10" runat="server"
                                                    Text="K." Enabled="False" />
                                                <asp:CheckBox ID="cbxSelectAnswer_10" runat="server" Text="K." Enabled="False" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSelectAnswer_10" runat="server" Width="100%"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 10%; white-space: nowrap;">
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_11" runat="server"
                                                    Text="L." Enabled="False" />
                                                <asp:CheckBox ID="cbxSelectAnswer_11" runat="server" Text="L." Enabled="False" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSelectAnswer_11" runat="server" Width="100%"></asp:Label></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td>
                                    辅助分类
                                </td>
                                <td colspan="7">
                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryPath") %>' Width="100%"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    母题编号
                                </td>
                                <td colspan="7">
                                    <asp:Label ID="lblMotherCode" runat="server" Text='<%# Eval("MotherCode") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    关键字
                                </td>
                                <td colspan="7">
                                    <asp:Label ID="lblKeyWords" runat="server" Text='<%# Eval("KeyWord") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    备注
                                </td>
                                <td colspan="7">
                                    <asp:Label ID="lblMemo" runat="server" Text='<%# Eval("Memo") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:HiddenField ID="hfSelectAnswer" runat="server" Value='<%# Bind("SelectAnswer") %>' />
                        <asp:HiddenField ID="hfStandardAnswer" runat="server" Value='<%# Bind("StandardAnswer") %>' />
                        <asp:LinkButton ID="btnOk" runat="server" CausesValidation="False" OnClientClick="javascript:self.window.close();"
                            Text='<IMG BORDER="0" SRC="../Common/Image/confirm.gif" />'>
                        </asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 10%;">
                                    教材章节
                                </td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtBookChapterEdit" runat="server" ReadOnly="true" Width="96%" Text='<%# Bind("ChapterPath") %>'>
                                    </asp:TextBox>
                                    <img style="cursor: hand;" onclick="selectBookChapter();" src="../Common/Image/search.gif"
                                        alt="选择教材章节" border="0" />
                                    <asp:RequiredFieldValidator ID="rfvBookChapterEdit" runat="server" EnableClientScript="true"
                                        Display="none" ErrorMessage="“教材章节”不能为空！" ControlToValidate="txtBookChapterEdit">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    试题类型
                                </td>
                                <td style="width: 15%;">
                                    <asp:DropDownList ID="ddlItemTypeEdit" runat="server" DataSourceID="odsItemType"
                                        DataTextField="TypeName" DataValueField="ItemTypeId" SelectedValue='<%# Bind("TypeId")%>'>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    试题难度
                                </td>
                                <td style="width: 15%;">
                                    <asp:DropDownList ID="ddlItemDifficultyEdit" runat="server" DataSourceID="odsItemDifficulty"
                                        DataTextField="DifficultyName" DataValueField="ItemDifficultyId" SelectedValue='<%# Bind("DifficultyId") %>'>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    缺省分数
                                </td>
                                <td style="width: 15%;">
                                    <asp:TextBox ID="txtDefaultScoreEdit" runat="server" Text='<%# Bind("Score") %>'>
                                    </asp:TextBox>
                                </td>
                                <td>
                                    选项数目
                                </td>
                                <td style="width: 15%;">
                                    <asp:DropDownList ID="ddlAnswerCountEdit" runat="server" SelectedValue='<%# Bind("AnswerCount") %>'>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    答题时间
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCompleteTimeEdit" runat="server" Text='<%# Bind("CompleteTimeString") %>'>
                                    </asp:TextBox>
                                </td>
                                <td>
                                    试题版本
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVersionEdit" runat="server" Text='<%# Bind("Version") %>'></asp:TextBox>
                                </td>
                                <td>
                                    出题时间
                                </td>
                                <td>
                                    <uc1:Date ID="dateCreateTimeEdit" runat="server" DateValue='<%# Bind("CreateTime","{0:yyyy-MM-dd}") %>' />
                                </td>
                                <td>
                                    试题种类
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlHasPictureEdit" runat="server" SelectedValue='<%# Bind("HasPicture") %>'>
                                        <asp:ListItem Text="文本" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="图片" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    过期日期
                                </td>
                                <td>
                                    <uc1:Date ID="dateOutDateDateEdit" runat="server" DateValue='<%# Bind("OutDateDateString") %>' />
                                </td>
                                <td>
                                    试题状态
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatusEdit" runat="server" DataSourceID="odsItemStatus"
                                        DataTextField="StatusName" DataValueField="ItemStatusId" SelectedValue='<%# Bind("StatusId") %>'>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    出题人
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCreatePersonEdit" runat="server" Text='<%# Bind("CreatePerson") %>'>
                                    </asp:TextBox>
                                </td>
                                <td>
                                    试题用途
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlUseage" runat="server" SelectedValue='<%# Bind("UsageId") %>'>
                                        <asp:ListItem Text="用作练习和考试" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="仅用作考试" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    试题内容
                                </td>
                                <td colspan="7">
                                    <iframe id="eWebEditorEdit" style="display: none;" src='/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Edit&Type=Content'
                                        frameborder="0" scrolling="no" width="100%" height="120"></iframe>
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="txtContentEdit" runat="server" Width="98%" Height="120px" Text='<%# Bind("Content") %>'
                                        TextMode="MultiLine">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr id="contentAnswer" style="display: none">
                                <td>
                                    试题答案
                                </td>
                                <td colspan="7">
                                    <iframe id="eWebEditorAnswerEdit" style="display: none;" src='/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Edit&Type=Answer'
                                        frameborder="0" scrolling="no" width="100%" height="120"></iframe>
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="txtContentAnswer" runat="server" Width="98%" Height="120px" Text='<%# Eval("StandardAnswer") %>'
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="selectEditTr">
                                <td>
                                    候选项
                                </td>
                                <td colspan="7" style="padding: 0; border-width: 0;">
                                    <table class="contentTable">
                                        <tr>
                                            <td style="width: 5%;">
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Edit_0" runat="server"
                                                    Text="A." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Edit_0" runat="server" Text="A." /></td>
                                            <td style="width: 95%; vertical-align: top;">
                                                <iframe id="eWebEditorEdit1" frameborder="0" scrolling="no" width="100%" height="80">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Edit_0" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox>
                                                <asp:Label ID="lblSelectAnswer_Edit_0" runat="server" Width="98%" Text="对"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Edit_1" runat="server"
                                                    Text="B." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Edit_1" runat="server" Text="B." /></td>
                                            <td>
                                                <iframe id="eWebEditorEdit2" frameborder="0" scrolling="no" width="100%" height="80">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Edit_1" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox>
                                                <asp:Label ID="lblSelectAnswer_Edit_1" runat="server" Width="98%" Text="错"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Edit_2" runat="server"
                                                    Text="C." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Edit_2" runat="server" Text="C." /></td>
                                            <td>
                                                <iframe id="eWebEditorEdit3" frameborder="0" scrolling="no" width="100%" height="80">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Edit_2" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Edit_3" runat="server"
                                                    Text="D." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Edit_3" runat="server" Text="D." /></td>
                                            <td>
                                                <iframe id="eWebEditorEdit4" frameborder="0" scrolling="no" width="100%" height="80">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Edit_3" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Edit_4" runat="server"
                                                    Text="E." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Edit_4" runat="server" Text="E." /></td>
                                            <td>
                                                <iframe id="eWebEditorEdit5" frameborder="0" scrolling="no" width="100%" height="80">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Edit_4" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Edit_5" runat="server"
                                                    Text="F." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Edit_5" runat="server" Text="F." /></td>
                                            <td>
                                                <iframe id="eWebEditorEdit6" frameborder="0" scrolling="no" width="100%" height="80">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Edit_5" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Edit_6" runat="server"
                                                    Text="G." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Edit_6" runat="server" Text="G." /></td>
                                            <td>
                                                <iframe id="eWebEditorEdit7" frameborder="0" scrolling="no" width="100%" height="80">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Edit_6" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Edit_7" runat="server"
                                                    Text="H." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Edit_7" runat="server" Text="H." /></td>
                                            <td>
                                                <iframe id="eWebEditorEdit8" frameborder="0" scrolling="no" width="100%" height="80">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Edit_7" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Edit_8" runat="server"
                                                    Text="I." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Edit_8" runat="server" Text="I." /></td>
                                            <td>
                                                <iframe id="eWebEditorEdit9" frameborder="0" scrolling="no" width="100%" height="80">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Edit_8" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Edit_9" runat="server"
                                                    Text="J." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Edit_9" runat="server" Text="J." /></td>
                                            <td>
                                                <iframe id="eWebEditorEdit10" frameborder="0" scrolling="no" width="100%" height="80">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Edit_9" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Edit_10" runat="server"
                                                    Text="K." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Edit_10" runat="server" Text="K." /></td>
                                            <td>
                                                <iframe id="eWebEditorEdit11" frameborder="0" scrolling="no" width="100%" height="80">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Edit_10" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Edit_11" runat="server"
                                                    Text="L." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Edit_11" runat="server" Text="L." /></td>
                                            <td>
                                                <iframe id="eWebEditorEdit12" frameborder="0" scrolling="no" width="100%" height="80">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Edit_11" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td>
                                    辅助分类
                                </td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtCategoryNameEdit" runat="server" ReadOnly="true" Width="96%"
                                        Text='<%# Bind("CategoryPath") %>'>
                                    </asp:TextBox>
                                    <img style="cursor: hand;" onclick="selectItemCategory();" src="../Common/Image/search.gif"
                                        alt="选择辅助分类" border="0" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    母题编号
                                </td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtMotherCodeEdit" runat="server" Width="99%" Text='<%# Bind("MotherCode") %>'
                                        MaxLength="50">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    关键字
                                </td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtKeyWordEdit" runat="server" Width="99%" Text='<%# Bind("KeyWord") %>'
                                        MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    备注
                                </td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtMemoEdit" runat="server" Width="99%" Text='<%# Bind("Memo") %>'
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:HiddenField ID="hfBookId" runat="server" Value='<%# Bind("BookId") %>' />
                        <asp:HiddenField ID="hfChapterId" runat="server" Value='<%# Bind("ChapterId") %>' />
                        <asp:HiddenField ID="hfOrganizationId" runat="server" Value='<%# Bind("OrganizationId") %>' />
                        <asp:HiddenField ID="hfCategoryId" runat="server" Value='<%# Bind("CategoryId") %>' />
                        <asp:HiddenField ID="hfSelectAnswer" runat="server" Value='<%# Bind("SelectAnswer") %>' />
                        <asp:HiddenField ID="hfStandardAnswer" runat="server" Value='<%# Bind("StandardAnswer") %>' />
                        <asp:LinkButton ID="btnUpdate" runat="server" CausesValidation="False" CommandName="Update"
                            OnClientClick="javascript:return updateBtnClientClicked();" Text='<IMG BORDER="0" SRC="../Common/Image/save.gif" />'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnCancelEdit" runat="server" CausesValidation="False" CommandName="Cancel"
                            OnClientClick="javascript:self.window.close();" Text='<IMG BORDER="0" SRC="../Common/Image/cancel.gif" />'>
                        </asp:LinkButton>
                    </EditItemTemplate>
                    <InsertItemTemplate>
                        <table class="contentTable">
                            <tr>
                                <td style="width: 10%;">
                                    教材章节
                                </td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtBookChapterInsert" runat="server" ReadOnly="true" Width="96%"
                                        Text='<%# Bind("ChapterPath") %>'>
                                    </asp:TextBox>
                                    <img style="cursor: hand;" onclick="selectBookChapter();" src="../Common/Image/search.gif"
                                        alt="选择教材章节" border="0" />
                                    <asp:RequiredFieldValidator ID="rfvBookChapterInsert" runat="server" EnableClientScript="true"
                                        Display="none" ErrorMessage="“教材章节”不能为空！" ControlToValidate="txtBookChapterInsert">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    试题类型
                                </td>
                                <td style="width: 15%;">
                                    <asp:DropDownList ID="ddlItemTypeInsert" runat="server" DataSourceID="odsItemType"
                                        DataTextField="TypeName" DataValueField="ItemTypeId" SelectedValue='<%# Bind("TypeId") %>'>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    试题难度
                                </td>
                                <td style="width: 15%;">
                                    <asp:DropDownList ID="ddlItemDifficultyInsert" runat="server" DataSourceID="odsItemDifficulty"
                                        DataTextField="DifficultyName" DataValueField="ItemDifficultyId" SelectedValue='<%# Bind("DifficultyId") %>'>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    缺省分数
                                </td>
                                <td style="width: 15%;">
                                    <asp:TextBox ID="txtDefaultScoreInsert" runat="server" Text='<%# Bind("Score") %>'>
                                    </asp:TextBox>
                                </td>
                                <td>
                                    选项数目
                                </td>
                                <td style="width: 15%;">
                                    <asp:DropDownList ID="ddlAnswerCountInsert" runat="server" SelectedValue='<%# Bind("AnswerCount") %>'>
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    答题时间
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCompleteTimeInsert" runat="server" Text='<%# Bind("CompleteTimeString") %>'>
                                    </asp:TextBox>
                                </td>
                                <td>
                                    试题版本
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVersionInsert" runat="server" Text='<%# Bind("Version") %>'>
                                    </asp:TextBox>
                                </td>
                                <td>
                                    出题时间
                                </td>
                                <td>
                                    <uc1:Date ID="dateCreateTimeInsert" runat="server" DateValue='<%# Bind("CreateTime","{0:yyyy-MM-dd}") %>' />
                                </td>
                                <td>
                                    试题种类
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlHasPictureInsert" runat="server" SelectedValue='<%# Bind("HasPicture") %>'>
                                        <asp:ListItem Text="文本" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="图片" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    过期日期
                                </td>
                                <td>
                                    <uc1:Date ID="dateOutDateDateInsert" runat="server" DateValue='<%# Bind("OutDateDate","{0:yyyy-MM-dd}") %>' />
                                </td>
                                <td>
                                    试题状态
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlStatusInsert" runat="server" DataSourceID="odsItemStatus"
                                        DataTextField="StatusName" DataValueField="ItemStatusId" SelectedValue='<%# Bind("StatusId") %>'>
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    出题人
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCreatePersonInsert" runat="server" ReadOnly="true" Text='<%# Bind("CreatePerson") %>'>
                                    </asp:TextBox>
                                </td>
                                <td>
                                    试题用途
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlUseage" runat="server" SelectedValue='<%# Bind("UsageId") %>'>
                                        <asp:ListItem Text="用作练习和考试" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="仅用作考试" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    试题内容
                                </td>
                                <td colspan="7">
                                    <iframe id="eWebEditorInsert" style="display: none;" src='/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Insert&Type=Content'
                                        frameborder="0" scrolling="no" width="100%" height="120px"></iframe>
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="txtContentInsert" runat="server" Width="98%" Height="120px" Text='<%# Bind("Content") %>'
                                        TextMode="MultiLine">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr id="contentAnswer" style="display: none">
                                <td>
                                    试题答案
                                </td>
                                <td colspan="7">
                                    <iframe id="eWebEditorAnswerInsert" style="display: none;" src='/RailExamBao/ewebeditor/asp/ItemEditor.asp?Mode=Insert&Type=Answer'
                                        frameborder="0" scrolling="no" width="100%" height="120px"></iframe>
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="txtContentAnswer" runat="server" Width="98%" Height="120px" Text='<%# Eval("StandardAnswer") %>'
                                        TextMode="MultiLine"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id="selectInsertTr">
                                <td>
                                    候选项
                                </td>
                                <td colspan="7" style="padding: 0; border-width: 0;">
                                    <table class="contentTable">
                                        <tr>
                                            <td width="5%">
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Insert_0" runat="server"
                                                    Text="A." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Insert_0" runat="server" Text="A." /></td>
                                            <td>
                                                <iframe id="eWebEditorInsert1" frameborder="0" scrolling="no" width="100%" height="80px">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Insert_0" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox>
                                                <asp:Label ID="lblSelectAnswer_Insert_0" runat="server" Width="98%" Text="对"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Insert_1" runat="server"
                                                    Text="B." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Insert_1" runat="server" Text="B." /></td>
                                            <td>
                                                <iframe id="eWebEditorInsert2" frameborder="0" scrolling="no" width="100%" height="80px">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Insert_1" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox>
                                                <asp:Label ID="lblSelectAnswer_Insert_1" runat="server" Width="98%" Text="错"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Insert_2" runat="server"
                                                    Text="C." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Insert_2" runat="server" Text="C." /></td>
                                            <td>
                                                <iframe id="eWebEditorInsert3" frameborder="0" scrolling="no" width="100%" height="80px">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Insert_2" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Insert_3" runat="server"
                                                    Text="D." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Insert_3" runat="server" Text="D." /></td>
                                            <td>
                                                <iframe id="eWebEditorInsert4" frameborder="0" scrolling="no" width="100%" height="80px">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Insert_3" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Insert_4" runat="server"
                                                    Text="E." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Insert_4" runat="server" Text="E." /></td>
                                            <td>
                                                <iframe id="eWebEditorInsert5" frameborder="0" scrolling="no" width="100%" height="80px">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Insert_4" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Insert_5" runat="server"
                                                    Text="F." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Insert_5" runat="server" Text="F." /></td>
                                            <td>
                                                <iframe id="eWebEditorInsert6" frameborder="0" scrolling="no" width="100%" height="80px">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Insert_5" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Insert_6" runat="server"
                                                    Text="G." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Insert_6" runat="server" Text="G." /></td>
                                            <td>
                                                <iframe id="eWebEditorInsert7" frameborder="0" scrolling="no" width="100%" height="80px">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Insert_6" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Insert_7" runat="server"
                                                    Text="H." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Insert_7" runat="server" Text="H." /></td>
                                            <td>
                                                <iframe id="eWebEditorInsert8" frameborder="0" scrolling="no" width="100%" height="80px">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Insert_7" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Insert_8" runat="server"
                                                    Text="I." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Insert_8" runat="server" Text="I." /></td>
                                            <td>
                                                <iframe id="eWebEditorInsert9" frameborder="0" scrolling="no" width="100%" height="80px">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Insert_8" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Insert_9" runat="server"
                                                    Text="J." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Insert_9" runat="server" Text="J." /></td>
                                            <td>
                                                <iframe id="eWebEditorInsert10" frameborder="0" scrolling="no" width="100%" height="80px">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Insert_9" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Insert_10" runat="server"
                                                    Text="K." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Insert_10" runat="server" Text="K." /></td>
                                            <td>
                                                <iframe id="eWebEditorInsert11" frameborder="0" scrolling="no" width="100%" height="80px">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Insert_10" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:RadioButton GroupName="rbnsEditItem" ID="rbnSelectAnswer_Insert_11" runat="server"
                                                    Text="L." />
                                                <asp:CheckBox ID="cbxSelectAnswer_Insert_11" runat="server" Text="L." /></td>
                                            <td>
                                                <iframe id="eWebEditorInsert12" frameborder="0" scrolling="no" width="100%" height="80px">
                                                </iframe>
                                                &nbsp;&nbsp;
                                                <asp:TextBox ID="txtSelectAnswer_Insert_11" runat="server" Width="98%" TextMode="MultiLine"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr style="display: none">
                                <td>
                                    辅助分类
                                </td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtCategoryNameInsert" runat="server" ReadOnly="true" Width="96%"
                                        Text='<%# Bind("CategoryName") %>'>
                                    </asp:TextBox>
                                    <img style="cursor: hand;" onclick="selectItemCategory();" src="../Common/Image/search.gif"
                                        alt="选择辅助分类" border="0" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    母题编号
                                </td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtMotherCodeInsert" runat="server" Width="99%" Text='<%# Bind("MotherCode") %>'
                                        MaxLength="50">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    关键字
                                </td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtKeyWordInsert" runat="server" Width="99%" Text='<%# Bind("KeyWord") %>'
                                        MaxLength="50">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    备注
                                </td>
                                <td colspan="7">
                                    <asp:TextBox ID="txtMemoInsert" runat="server" Width="99%" Text='<%# Bind("Memo") %>'
                                        TextMode="MultiLine">
                                    </asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:LinkButton ID="btnInsert" runat="server" CausesValidation="True" CommandName="Insert"
                            OnClientClick="javascript:return insertBtnClientClicked();" Text='<IMG SRC="../Common/Image/save.gif" border="0" alt="保存并关闭" />'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnContinue" runat="server" CausesValidation="True" CommandName="Continue"
                            OnClientClick="javascript:return insertBtnClientClicked();" Text='<IMG SRC="../Common/Image/saveadd.gif" border="0" alt="保存并新增" />'>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnCancelInsert" runat="server" CausesValidation="False" CommandName="Cancel"
                            OnClientClick="javascript:self.window.close();" Text='<IMG SRC="../Common/Image/cancel.gif" border="0" alt="取消" />'>
                        </asp:LinkButton>
                        <asp:HiddenField ID="hfBookId" runat="server" Value='<%# Bind("BookId") %>' />
                        <asp:HiddenField ID="hfChapterId" runat="server" Value='<%# Bind("ChapterId") %>' />
                        <asp:HiddenField ID="hfOrganizationId" runat="server" Value='<%# Bind("OrganizationId") %>' />
                        <asp:HiddenField ID="hfCategoryId" runat="server" Value='<%# Bind("CategoryId") %>' />
                        <asp:HiddenField ID="hfSelectAnswer" runat="server" Value='<%# Bind("SelectAnswer") %>' />
                        <asp:HiddenField ID="hfStandardAnswer" runat="server" Value='<%# Bind("StandardAnswer") %>' />
                    </InsertItemTemplate>
                </asp:FormView>
            </div>
        </div>
        <asp:ObjectDataSource ID="odsItem" runat="server" DataObjectTypeName="RailExam.Model.Item"
            DeleteMethod="DeleteItem" InsertMethod="AddItem" SelectMethod="GetItem" TypeName="RailExam.BLL.ItemBLL"
            UpdateMethod="UpdateItem">
            <SelectParameters>
                <asp:QueryStringParameter DefaultValue="-1" Name="itemId" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsItemType" runat="server" SelectMethod="GetItemTypes"
            TypeName="RailExam.BLL.ItemTypeBLL" DataObjectTypeName="RailExam.Model.ItemType">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsItemDifficulty" runat="server" SelectMethod="GetItemDifficulties"
            TypeName="RailExam.BLL.ItemDifficultyBLL" DataObjectTypeName="RailExam.Model.ItemDifficulty">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsItemStatus" runat="server" SelectMethod="GetItemStatuss"
            TypeName="RailExam.BLL.ItemStatusBLL" DataObjectTypeName="RailExam.Model.ItemStatus">
        </asp:ObjectDataSource>
    </form>
</body>

<script type="text/javascript">
    var search = window.location.search;
    var modeIndex = search.indexOf("mode");
    var andIndex = search.indexOf("&");
    
   if(search.substring(modeIndex + 5, andIndex) == "Edit")
  {
    var answerCount = $F("fvItem_ddlAnswerCountEdit");
    var answers = $F("fvItem_hfSelectAnswer").value.split("|");
  }  

</script>

</html>
