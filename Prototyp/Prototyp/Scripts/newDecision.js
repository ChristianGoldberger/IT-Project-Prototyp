$(document).ready(function ()
{
	$("#btnSave").click(saveDecision);	
});
function saveDecision()
{
	var title = $("#title").val();
	var description = $("#description").val();
	var url = "Decision/SaveDecision"
	$.getJSON(url, { title: title, description: description }, function (check) {
		if (check == 0)
		{
			window.location.href = "/Decision/New";
			$("#titleForm").removeClass("has-error");
			$("#descriptionForm").removeClass("has-error");
		}
		else if (check == 1)
		{
			$("#titleForm").addClass("has-error");
		}
		else if (check == 2)
		{
			$("#descriptionForm").addClass("has-error");
		}
		else if (check == 3)
		{
			$("#titleForm").addClass("has-error");
			$("#descriptionForm").addClass("has-error");
		}

	});

}