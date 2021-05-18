/*
    Date: 2020/08/14
    Company: Innova Marketing Systems S.A.S
    Programmer: Diego Arenas Tangarife
    Comments: Funciones usadas en la landing V2 
 */

//// Abre el form o desliza suavemente hasta el form
function openModalOrSmoothToForm() {
    if (screen.width > 776) {
        $('.Cotizador').modal('show');
    } else {
        $('html, body').animate({
            scrollTop: $("#frm-contact-lading").offset().top
        }, 1200);
    }
}

//// Abre el popup de cotizador
function openFormPopup() {
    if (screen.width > 776) {
        setTimeout(() => {
            const Toast = Swal.mixin({
                toast: true,
                showClass: {
                    popup: 'animate__animated animate__headShake animate__infinite'
                },
                hideClass: {
                    popup: 'animate__animated animate__bounceOutRight'
                },
                position: 'top-end',
                showConfirmButton: true,
                confirmButtonText: '<h4 style="colo:#228dcb">¡Cotiza Aquí!</h4>',
            });

            Toast.fire()
                .then((result) => {
                    if (result.isConfirmed) {
                        $('.Cotizador').modal('show');
                    }
                });
        }, 1000);
    } else {
        setTimeout(() => {
            const Toast = Swal.mixin({
                toast: true,
                showClass: {
                    popup: 'animate__animated animate__bounceInRight'
                },
                hideClass: {
                    popup: 'animate__animated animate__bounceOutRight'
                },
                position: 'top-end',
                showConfirmButton: true,
                confirmButtonText: '¡Cotiza Aquí!',
                showCancelButton: true,
                cancelButtonText: '<i class="fa fa-times"></i>'
            });

            Toast.fire()
                .then((result) => {
                    if (result.isConfirmed) {
                        $('html, body').animate({
                            scrollTop: $("#frm-contact-lading").offset().top
                        }, 1200);
                    }
                });
        }, 1000);
    }
}

$("#closeModalLanding").on('click', function () {
    openFormPopup();
});
