$(document).ready(function () {
    //when click change profile
    $('#btn_formConfirmPassword').click(function () {
        $('#div_formConfirmPassword').fadeIn(500);
        $('#inputEmail3').val('');
        $('#btn_formConfirmPassword').attr('disabled', true);
        //alert('abc');
    });

    //when click cancel input password
    $('#cancelPassword').click(function () {
        $('#div_formConfirmPassword').fadeOut();
        $('#inputEmail3').val('');
        $('#errorInput').text('');
        $('#btn_formConfirmPassword').attr('disabled', false);

    });

    //when click cancel change profile

    $('#btn_cancelChangeProfile').click(function () {
        $('#btn_formConfirmPassword').attr('disabled', false);
        $('#div_ChangeProfile').fadeOut();
        $('label.error').hide();
        $('#div_ChangePassword').hide();
        $('#btn_HideChangePassword').hide();
        $('#btn_showChangePassword').show();
    });

    //when click show change password

    $('#btn_showChangePassword').click(function () {
        $('#div_ChangePassword').show();
        $('#btn_HideChangePassword').show();
        $(this).hide();
    });

    //when click hide change password
    $('#btn_HideChangePassword').click(function () {
        $('#div_ChangePassword').hide();
        $('#btn_showChangePassword').show();
        $(this).hide();
    });


});

//fucntion show change profile
function ShowFormChangeProfile() {
    $('#div_formConfirmPassword').hide();
    $('#div_ChangeProfile').fadeIn();
    $('#btn_formConfirmPassword').attr('disabled', true);
}

//function show sweet alert when change profile success

function AlertChangedProfile() {
    swal("Good job!", "You clicked the button!", "success");
}




