$(document).ready(function () {
    $("#but_submit").click(function () {
        var email = $("#txt_email").val().trim();
        var passcode = $("#txt_psc").val().trim();

        if (email != "" && passcode != "") {
            $.ajax({
                url: 'Authenticate.cs',
                type: 'post',
                data: { email: email, passcode: passcode },
                success: function (response) {
                    var msg = "";
                    if (response == 1) {
                        window.location = Login.html;
                    } else {
                        msg = "Invalid email or passcode!";
                    }
                    $("#logins").html(msg);
                }
            });
        }
        var msg = "FAIL";
        $Log.html(msg);
    });
});