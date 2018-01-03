$(document).ready(function ()
{
	$("#btnEdit").click(edit);
	//$("#btnDelete").click(deleteDecision);
});
$('tr').click(function (e)
{
	var txt = $(e.target).attr('id');
	console.log(txt);
});
