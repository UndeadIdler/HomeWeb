function ajaxPostFeedback()
{
	var type=$("#ddlType").val();
	var username=$("#txtUserName").val();
	var tel=$("#txtTel").val();
	var email=$("#txtEmail").val();
	var content=$("#txtContent").val();
	if(!username) {
		alert("姓名不能为空!");
		return;
	}
	if(!tel) {
		alert("电话不能为空!");
		return;
	}
	if(!content || content.length<5) {
		alert("留言字符不能少于5个!");
		return;
	}
	$.ajax({
		type:		"post",
		dataType:	"html",
		data:		"type="+encodeURIComponent(type)+"&username="+encodeURIComponent(username)+"&tel="+encodeURIComponent(tel)+"&email="+encodeURIComponent(email)+"&content="+encodeURIComponent(content),
		url:		"/plus/feedback.aspx?lan=cn&oper=ajaxPostFeedback&time="+(new Date().getTime()),
		error:		function(XmlHttpRequest,textStatus, errorThrown){if(XmlHttpRequest.responseText!=""){if(XmlHttpRequest.responseText.length>0){alert(XmlHttpRequest.responseText);}}},
		success:	function(d){
			if(d=="ok")
			{
				alert("发送成功,请等待反馈");
				location.reload();
			}
			else
			{
				alert(d);
			}
		}
	});
}