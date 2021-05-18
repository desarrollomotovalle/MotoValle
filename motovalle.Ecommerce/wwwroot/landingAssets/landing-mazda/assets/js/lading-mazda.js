/*
    Date: 2020/08/14
    Company: Innova Marketing Systems S.A.S
    Programmer: Diego Arenas Tangarife
    Comments: Funciones usadas en la landing V2 
 */

//// Abre el form o desliza suavemente hasta el form
function openModalOrSmoothToMazdaForm() {
    $('.MazdaCotizador').modal('show');
}

//// Abre el popup de cotizador
function openMazdaFormPopup() {
    if (screen.width > 768) {
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
            confirmButtonColor: '#A81E30',
            confirmButtonText: '<h4>¡Cotiza Aquí!</h4>',
        });

        Toast.fire()
            .then((result) => {
                if (result.isConfirmed) {
                    $('.MazdaCotizador').modal('show');
                }
            });
    } else {
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
            confirmButtonColor: '#A81E30',
            confirmButtonText: '¡Cotiza Aquí!',
            //showCancelButton: true,
            //cancelButtonText: "<i class='bx bx-x' ></i>"
        });

        Toast.fire()
            .then((result) => {
                if (result.isConfirmed) {
                    $('.MazdaCotizador').modal('show');
                }
            });
    }
}

$("#closeMazdaModalLanding").on('click', function () {
    openMazdaFormPopup();
});

$('.MazdaCotizador').on('hidden.bs.modal', function (e) {
    openMazdaFormPopup();
});


const $frmMazdaCotizar = document.querySelector("#frmMazdaCotizar");
$frmMazdaCotizar.addEventListener('submit', (event) => {
    event.preventDefault();

    if (!$($frmMazdaCotizar).valid()) {
        return;
    }

    if (document.querySelector(".info-campania")) { 
        $(".cotizacion-box").animate({
            width: "toggle",
            height: "toggle",
            opacity: "toggle"
        });
    }

    Swal.fire({
        title: 'Envíando información...',
        html: '¡Me cerraré en pocos segundos!',
        onBeforeOpen: () => {
            Swal.showLoading()
        },
        allowOutsideClick: false,
        allowEscapeKey: false
    });

    event.target.submit();
});
