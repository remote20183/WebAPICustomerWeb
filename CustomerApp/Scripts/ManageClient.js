
$(document).ready(function () { $("input").attr("autocomplete", "off"); });
$("#btn-submit").on("click", function () {
    $("#lblError").removeClass("success").removeClass("error").text('');
    var retval = true;
    $("#myForm .required").each(function () {
        if (!$(this).val()) {
            $(this).addClass("error");
            retval = false;
        }
        else {
            $(this).removeClass("error");
        }
    });
    var email = $("#Email").val();
    if (email && !isEmail(email)) {
        $("#Email").addClass("error");
        retval = false;
    }
    if ($("#Mobile").val().length < 10) {
        $("#Mobile").addClass("error");
        retval = false;
    }
    if (retval) {
        var data = {
            Id: $("#Id").val().trim(),
            Email: $("#Email").val().trim(),
            UID: $("#UID").val().trim(),
            ExpiryDate: $("#ExpiryDate").val().trim(),
            Mobile: $("#Mobile").val().trim(),
        }
        $.ajax({
            type: "POST",
            url: "/Home/ManageClient",
            data: { model: data },
            success: function (data) {
                if (data.status === "Fail") {
                    $("#lblError").addClass("error").text(data.error).show();
                }
                else {
                    window.location.href = '/Home/ClientList'
                }
            }
        });
    }
});
