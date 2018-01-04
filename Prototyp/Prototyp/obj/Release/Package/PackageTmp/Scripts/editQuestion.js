$(document).ready(function () {
	$("#btnBack").click(function ()
	{
		window.history.pushState({},"Question", "/Question/Index")
		
	});
});

