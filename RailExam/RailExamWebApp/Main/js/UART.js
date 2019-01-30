function SetFocusPageSize()
{
    if(event.keyCode == 13)
    {
        event.keyCode = 9;
        document.getElementById('btnSetPageSize').click();
    }
}

function SetFocusSearch()
{
    if(event.keyCode == 13)
    {
        event.keyCode = 9;
        document.getElementById('btnSearch').click();
    }
}

function OpenModalDialog(url)
{
    var newWin = null;
    newWin = window.showModalDialog(url,window,'dialogHeight:600px;dialogWidth:800px');
    if(newWin==null)
    {
        return;
    }
    newWin.open();
}
function refreshParent() 
{ 
    var parent=window.dialogArguments;
    parent.location.reload();
}
function checkFormAll(chk)
{
	form = document.getElementById("<%=form1.ClientID%>");
	for(var i=0; i<form.elements.length; i++)
	{
		if (form.elements[i].type=="checkbox")
		{
			form.elements[i].checked = chk;
		}
	}
}