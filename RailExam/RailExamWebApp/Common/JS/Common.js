function showCommonDialog(pageName,view)
{
	if(view == null)
	{
  	   view = 'dialogWidth:800px;dialogHeight:600px';
	}
	
	var height = view.substring(view.indexOf("dialogHeight:")+13);	
	height = height.replace(";","");
	return window.showModalDialog('/RailExamBao/Common/DialogPage.aspx?height='+height+'&pageName=' + pageName ,window,'help:no;status:no;' + view);
}

function showCommonDialogFull(pageName,view)
{
	if(view == null)
	{
  	   view = 'dialogWidth:800px;dialogHeight:600px';
	}
	return window.showModalDialog('/RailExamBao/Common/DialogPage.aspx?isfull=true&pageName=' + pageName ,'','help:no;status:no;' + view);
}

