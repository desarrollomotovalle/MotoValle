// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//var myCarousel = document.querySelector('#carouselExampleIndicators')
//var carousel = new bootstrap.Carousel(myCarousel, {
//    interval: 10000,
//    wrap: true
//})

/*--------------------------------------------------------------
# Funcion para enviar información al servidor
--------------------------------------------------------------*/
function enviarInformacion() {

    var myHeaders = new Headers();
    myHeaders.append("Accept", "application/json");
    myHeaders.append("Authorization", "Basic QWRtaW4ubW90b3ZhbGxlOk1vdG92YWxsZV8yMDIyKg==");
    myHeaders.append("Content-Type", "application/json");

    var name = document.getElementById('name').value;
    var phoneNumber = document.getElementById('phoneNumber').value;
    var email = document.getElementById('email').value;
    var descripcionConvenio = document.getElementById('descripcionConvenio').value;

    var raw = JSON.stringify({
        "firstName": name,
        "contactInformation": {
            "phone": [
                {                    
                    "number": "57" + phoneNumber
                }
            ],
            "email": [
                {
                    "address": email
                }
            ]
        },        
        "tags": descripcionConvenio,
        "from": "petronas@motovalle.com",
        "to": email,
        "text": "-"
    });

    var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: raw,
        redirect: 'follow'
    };

    console.log(raw);

    fetch("https://api.messaging-service.com/people/2/persons", requestOptions)
        .then(response => response.text())
        //.then(result => console.log(result))
        //.catch(error => alert('Hizo lo que debe hacer'));
        .then(result => {
            if (result.includes(40001) || result.includes('Duplicate')) {
                console.log(result);
                script1();
                script2();

            } else {
                limpiarCampos();
                console.log(result);

            }
        })
}

/*--------------------------------------------------------------
# Funcion para enviar mensaje de texto al cliente
--------------------------------------------------------------*/
function script1() {

    var phoneNumber = document.getElementById('phoneNumber').value;    

    var myHeaders = new Headers();
    myHeaders.append("Authorization", "Basic QWRtaW4ubW90b3ZhbGxlOk1vdG92YWxsZV8yMDIyKg==");
    myHeaders.append("Accept", "application/json");
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
        "from": "InfoSMS",
        "to": "57" + phoneNumber,
        "text": "Bienvenido al Centro de Servicio Motovalle Petronas, gracias por registrarte! A partir de este momento tienes un Bono de Descuento por $100.000 en tu proximo cambio de aceite. Visita nuestras sedes, Cali: https://rebrand.ly/PetronasCali o Bogota: https://rebrand.ly/PetronasBogota   Agenda tu cita Aqui: https://rebrand.ly/MotovallePetronas"
    });

    var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: raw,
        redirect: 'follow'
    };

    fetch("https://api.messaging-service.com/sms/1/text/single", requestOptions)
        .then(response => response.text())
        .then(result => console.log(result))
        .catch(error => console.log('error', error));

}

/*--------------------------------------------------------------
# Funcion para enviar correo electrónico al cliente
--------------------------------------------------------------*/
function script2() {

    var email = document.getElementById('email').value;    

    var myHeaders = new Headers();
    myHeaders.append("Accept", "application/json");
    myHeaders.append("Authorization", "Basic QWRtaW4ubW90b3ZhbGxlOk1vdG92YWxsZV8yMDIyKg==");
    myHeaders.append("Content-Transfer-Encoding", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");

    var formdata = new FormData();
    formdata.append("from", "Motovalle Petronas <Informacion@petronas.motovalle.com>");
    /*formdata.append("to", "carlospulgarinz@gmail.com");*/
    formdata.append("to", email);
    formdata.append("subject", "Bienvenido a Motovalle Petronas");
    formdata.append("text", "Bienvenido");
    formdata.append("html", "<!DOCTYPE html>\n\n<html>\n    <meta charset=\"UTF-8\"/> \n    <head>\n    </head>\n\n    <style type=\"text/css\">\n  url('https://fonts.googleapis.com/css2?family=Roboto:wght@500&display=swap'); \n\n    </style>\n    \n    <body>\n\n        <div class=\"container\" id=\"header\" style=\"text-align: center; font-family: 'Roboto', sans-serif;\">\n\n\n            <p style=\"font-size: 20px;\">\n                <br>\n                <b style=\"font-size: 30px;\">¡Gracias por registrarte!</b>\n                <br>\n            </p>\n            <p class=\"textoBienvenida\" style=\"font-size: 20px; width: 550px; margin-left: auto; margin-right: auto;\">\n                Te damos la bienvenida a nuestro Centro de Servicio multimarca <b>MOTOVALLE PETRONAS</b>,\n                a partir de este momento cuentas con los siguientes beneficios para tu vehículo:<br><br>\n            </p>\n                \n            <div>\n                <img src=\"https://motovalle.com/image/LandingQR/Correo/Correo/images/bonootros.png\" width=\"350px\">\n            </div>\n\n            <br>\n\n\n            <table class=\"container\" style=\"margin-left:auto;margin-right:auto; margin-top: 20px;text-align: center;\">\n                    <b style=\"font-size: 22px;\"><b>Contactanos:</b>\n                <td>\n                    <tr>\n                        <td>\n                            <a href=\"https://api.whatsapp.com/send?phone=+573168789158\" style=\"color: aliceblue;\"> \n                                <img src=\"https://motovalle.com/image/LandingQR/Correo/Correo/images/wpp.png\" width=\"200px\">\n                            </a>\n                        </td>\n                        <td style=\"width: 30px\"></td>\n                        <td>\n                            <a href=\"tel:6024883000\" style=\"color: aliceblue;\">\n                                <img src=\"https://motovalle.com/image/LandingQR/Correo/Correo/images/TEL.png\" width=\"200px\">\n                            </a>\n                        </td>\n                    </tr>\n                </td>\n            </table>\n\n            \n            <br>\n\n            <b>Visita Nuestras Sedes:</b>\n\n            <table class=\"container\" style=\"margin-left:auto;margin-right:auto; border-collapse: collapse; text-align: center;\">\n                    \n                <td>\n                    <tr>\n                        <td>\n                            <p style=\"font-size: 20px;\" >Cali</p>\n                        </td>\n                        <td>\n                            <p  style=\"font-size: 20px;\">Bogota</p>\n                        </td>\n                    </tr>\n                    <tr style=\" background-color: #00aa9e;\">\n                        <td style=\"padding-top:5px; padding-left: 5px; padding-bottom: 5px; padding-right: 5px;\">\n                            <a href=\"https://goo.gl/maps/nvFsBdLDAJ3KRZ9K7\">\n                                <img src=\"https://motovalle.com/image/LandingQR/Correo/Correo/images/Mapa1.png\" width=\"300px\">\n                            </a>\n                        </td>\n\n                        <td  style=\"padding-top:5px; padding-right: 5px; padding-bottom: 5px; padding-left: 5px;\">\n                            <a href=\"https://goo.gl/maps/VocvmwHvj1tvtWY78\">\n                                <img src=\"https://motovalle.com/image/LandingQR/Correo/Correo/images/Mapa2.png\" width=\"300px\">\n                            </a>\n                        </td>\n                    </tr>\n                </td>\n            </table>\n\n        </div>\n        <br>\n\n        <div class=\"container\" id=\"header\" style=\"text-align: center;\">\n\n            <img src=\"https://motovalle.com/image/LandingQR/Correo/Correo/images/footer.png\" width=\"500px\">\n\n        </div>\n        \n    \n    </body>\n\n</html>\n");

    var requestOptions = {
        method: 'POST',
        headers: myHeaders,
        body: formdata,
        redirect: 'follow'
    };

    fetch("https://api.messaging-service.com/email/1/send", requestOptions)
        .then(response => response.text())
        .then(result => console.log(result))
        .catch(error => console.log('error', error));
}

/*--------------------------------------------------------------
# Funcion para limpiar los campos de texto del formulario
--------------------------------------------------------------*/
function limpiarCampos() {
    document.getElementById("name").value = "";
    document.getElementById("NroDocumento").value = "";
    document.getElementById("phoneNumber").value = "";
    document.getElementById("email").value = "";
    document.getElementById("Linea").value = "";
    document.getElementById("Modelo").value = "";
    document.getElementById("Placa").value = "";
    var ciudadRegistro = document.getElementById("CiudadRegistro");
    ciudadRegistro.selectedIndex = 0;
    var marca = document.getElementById("Marca");
    marca.selectedIndex = 0;


}

/*--------------------------------------------------------------
# Funcion para enviar la información al servidor
--------------------------------------------------------------*/
function abrirModal() {
    event.preventDefault();
    mostrarModal().then((result) => {
        if (result.isConfirmed) {
            var fmrEnviar = document.getElementById("fmrEnviar");
            fmrEnviar.submit();
        }
    })
}

/*--------------------------------------------------------------
# Funcion para mostrar el mensaje de confirmación
--------------------------------------------------------------*/
function mostrarModal(titulo = "¿Desea guardar los cambios?", texto = "La información se almacenará en la Base de Datos") {
    // Importante clocar el return
    return Swal.fire({
        title: titulo,
        text: texto,
        icon: 'question',
        showCancelButton: true,
        cancelButtonText: 'No, cancelar',
        confirmButtonColor: '#00A19C',
        cancelButtonColor: '#898F9C',
        confirmButtonText: 'Si, guardar'
    })
}

/*--------------------------------------------------------------
# Funcion para convertir placa en mayuscula
--------------------------------------------------------------*/
function mayus(e) {
    e.value = e.value.toUpperCase();
}