$(function () {
    $("#LoginActionForm").submit(function (e) {
        e.preventDefault();

        var formAction = $(this).attr("action");
        var formMethod = $(this).attr("method");
        var token = $("[name='__RequestVerificationToken']").val();
        $("#SubmitLoginButton").prop("disabled", true);
        $("#SubmitLoginButton").html("Autenticando...");
        $("#ValidationSummaryLogin").html("");

        $.ajax({
            url: formAction,
            data: {
                __RequestVerificationToken: token,
                Rut: $("#Rut").val(),
                Password: $("#Password").val()
            },
            type: formMethod,
        }).done(function (data) {

            if (data.success) {

                let timerInterval
                Swal.fire({
                    icon: 'success',
                    title: data.title,
                    html: 'Redirigiendo al panel en <b></b> milisegundos.',
                    timer: 2000,
                    timerProgressBar: true,
                    onBeforeOpen: () => {
                        Swal.showLoading()
                        timerInterval = setInterval(() => {
                            const content = Swal.getContent()
                            if (content) {
                                const b = content.querySelector('b')
                                if (b) {
                                    b.textContent = Swal.getTimerLeft()
                                }
                            }
                        }, 100)
                    },
                    onClose: () => {

                        clearInterval(timerInterval);
                        window.location.href = data.action;
                    }
                }).then((result) => {

                    if (result.dismiss === Swal.DismissReason.timer) {
                        window.location.href = data.action;
                    }
                })
            }
            else {

                $("#SubmitLoginButton").html("Iniciar sesión");
                $("#SubmitLoginButton").prop("disabled", false);
                $('#Password').val('');

                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: data.message,
                    confirmButtonColor: '#3fabad',
                    /*footer: '<a href>¿Por qué tengo este problema?</a>'*/
                });

                if (typeof data.errors !== 'undefined') {
                    displayValidationErrors(data.errors);
                }

            }

        }).fail(function (jqXHR, textStatus, errorThrown) {

            $("#SubmitLoginButton").html("Iniciar sesión");
            $("#SubmitLoginButton").prop("disabled", false);
            $('#Password').val('');

            switch (jqXHR.status) {
                case 400:
                    alert("Favor completar las variables correctamente");
                    break;
                case 500:
                    alert("Se ha generado un error en el servidor");
                    break;
                case 403:
                    alert("Ud no está autorizado para ver esta informacion");
                    break;
                default:
                    break;
            }
        });
    });
});

jQuery(document).ajaxStart(function () {
    NProgress.configure({ showSpinner: false });
    NProgress.start();
});

jQuery(document).ajaxStop(function () {
    NProgress.done();
});

function displayValidationErrors(errors) {
    var $div = $("#ValidationSummaryLogin");

    $div.html(`<div class="alert alert-danger alert-dismissible" role="alert">
                            <h4 class="alert-heading" style="margin-bottom: 1rem;">Errores!</h4>
                            <div id="ListOfErors">
                            </div>
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                            </button>
                       </div >`);

    var $list = $("#ListOfErors");

    $list.empty();

    var $length = errors.length;

    $.each(errors, function (idx, errorMessage) {

        var $hr = '<hr>';

        if ((idx + 1) == $length) {
            $hr = '';
        }

        $list.append('<p class="mt-1">' + errorMessage + '</p>' + $hr);
    });
};


