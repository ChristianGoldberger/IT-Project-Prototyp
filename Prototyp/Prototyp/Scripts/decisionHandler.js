$(document).ready(function () {
	$("#btnSave").click(getWeight);
});
function getWeight()
{
	var weight = document.querySelector('input[name="Entscheidung"]:checked').value;
	var questionKey = document.getElementById('questionKey').innerHTML;
	var url = "SaveDecision"
	$.getJSON(url, { weight: weight, questionKey: questionKey }, function (check) {
		if (check == 0)
		{
			//Zeige nächste Frage
		}
	});
}