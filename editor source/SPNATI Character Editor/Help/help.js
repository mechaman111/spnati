var __readyHandler = null;

function navigate(url)
{
	var iframe = document.getElementById("mainFrame");
	if(!__readyHandler)
	{
		__readyHandler = iframe.addEventListener("load", setFocus);
	}
	iframe.src = url;
}

function setFocus()
{
	//This doesn't work right now, but the idea is to get keyboard scrolling working immediately without clicking in the frame
	var iframe = document.getElementById("mainFrame");
	setTimeout(function() {
		iframe.focus(); 
		iframe.contentWindow.focus();
	}, 100);
}