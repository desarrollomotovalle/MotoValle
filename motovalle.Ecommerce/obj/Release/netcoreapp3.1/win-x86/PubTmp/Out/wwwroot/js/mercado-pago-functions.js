/*
    Date: 2020/02/03
    Company: Innova Marketing Systems S.A.S
    Programmer: Diego Arenas Tangarife
    Comments: Use all functions provides for mercado pago developers
 */

///Listener for input functions
function addEvent(to, type, fn) {
    if (document.addEventListener) {
        to.addEventListener(type, fn, false);
    } else if (document.attachEvent) {
        to.attachEvent('on' + type, fn);
    } else {
        to['on' + type] = fn;
    }
};

//exec funcion for keyup and change
addEvent(document.querySelector('#cardNumber'), 'keyup', guessingPaymentMethod);
addEvent(document.querySelector('#cardNumber'), 'change', guessingPaymentMethod);


//Get Bin for credit card number using the first 6 characters of input
function getBin() {
    const cardnumber = document.getElementById("cardNumber");
    return cardnumber.value.substring(0, 6);
};

///Get payment method
function guessingPaymentMethod(event) {
    let bin = getBin();
    if (event.type == "keyup") {
        if (bin.length >= 6) {
            window.Mercadopago.getPaymentMethod({
                "bin": bin
            }, setPaymentMethodInfo);
        }
    } else {
        let spanCreditCard = document.getElementById("creditCardIconId");
        spanCreditCard.className = "fa fa-credit-card";
        setTimeout(function () {
            if (bin.length >= 6) {
                window.Mercadopago.getPaymentMethod({
                    "bin": bin
                }, setPaymentMethodInfo);
            }
        }, 100);
    }
};

//Set payment method
function setPaymentMethodInfo(status, response) {
    if (status == 200) {
        const paymentMethodElement = document.querySelector('input[name=paymentMethodId]');

        if (paymentMethodElement) {
            paymentMethodElement.value = response[0].id;

            document.getElementById("cardNumber").style = firstBoder;
            let spanCreditCard = document.getElementById("creditCardIconId");

            switch (paymentMethodElement.value) {
                case "visa":
                    spanCreditCard.className = "fa fa-cc-visa";
                    break;
                case "master":
                    spanCreditCard.className = "fa fa-cc-mastercard";
                    break;
                case "amex":
                    spanCreditCard.className = "glyphicon glyphicon-credit-card";
                case "diners":
                    spanCreditCard.className = "glyphicon glyphicon-credit-card";
                    break;
                default:
                    spanCreditCard.className = "fa fa-credit-card";
                    break;
            };

            //alert(paymentMethodElement.value);
        } else {
            const input = document.createElement('input');
            input.setAttribute('name', 'paymentMethodId');
            input.setAttribute('type', 'hidden');
            input.setAttribute('value', response[0].id);

            form.appendChild(input);
        }

        getAmount();
        getIdentificationType();

        let amount = document.getElementById("paymentSelectedIdForm").value == "2" ? parseFloat(document.getElementById("partialAmountId").value.replace(/[^0-9\.]/g, '')) : document.querySelector('#amount').value;

        Mercadopago.getInstallments({
            "bin": getBin(),
            "amount": parseFloat(amount),
        }, setInstallmentInfo);

    } else {
        document.getElementById("creditCardIconId").className = "fa fa-credit-card";
        document.getElementById("cardNumber").style = `border-color: rgba(255, 54, 51, 0.5);
                                                               box-shadow: 0 1px 1px rgba(0, 0, 0, 0.075) inset, 0 0 8px rgba(255, 138, 138, 0.3);
                                                               outline: 0 none;`;
    }

    //Get Installement methods
    function setInstallmentInfo(status, response) {
        if (status == "200" || status == "201") {
            var select = document.querySelector("#installments");
            select.innerHTML = "";

            if (response.length > 0) {
                let arrayList = response[0].payer_costs;
                document.querySelector("#issuerIdDiv").value = response[0].issuer.id;

                arrayList.forEach((element) => {
                    let opt = document.createElement("option");
                    opt.appendChild(document.createTextNode(element.recommended_message));
                    opt.value = parseInt(element.installments);
                    select.appendChild(opt);
                });  
            } else {
                document.querySelector("#issuerIdDiv").value = "";
                let optDefault = document.createElement("option");
                optDefault.appendChild(document.createTextNode("36 Cuotas"));
                optDefault.value = 36;
                select.appendChild(optDefault);
            }
        }
    }
};

//Gets token to credit card
function getTokenForCreditCard() {
    Swal.fire({
        title: 'Verificando tarjeta...',
        html: '¡Me cerraré en pocos segundos!',
        onBeforeOpen: () => {
            Swal.showLoading()
        },
        allowOutsideClick: false,
        allowEscapeKey: false
    });

    getRequiredData();
    let divWithDataInfo = document.querySelector('#mercadoPagoPayId');
    setTimeout(() => window.Mercadopago.createToken(divWithDataInfo, sdkResponseHandler), 1800);
}

//Check if token was created successfullly
function verifyToken() {
    let token = document.querySelector("#tokenInputId").value;
    if (token != null && token != "") {
        Swal.fire({
            title: '¡Verificación exitosa!',
            showClass: {
                popup: 'animated rubberBand'
            },
            hideClass: {
                popup: 'animated fadeOutUp faster'
            },
            position: 'top-center',
            icon: 'success',
            showConfirmButton: false,
            timer: 1000,
            allowOutsideClick: false,
            allowEscapeKey: false
        });

        return true;
    } else {
        //Swal.fire({
        //    icon: 'warning',
        //    title: 'Intenta nuevamente',
        //    footer: '<b>Te esperamos.</b>',
        //    allowOutsideClick: false,
        //    allowEscapeKey: false
        //});

        return false;
    }
}

//Is a call back function that returns the token provides for mercado pago or any error when it've sent the required data
function sdkResponseHandler(status, response) {
    if (status != 200 && status != 201) {
        document.querySelector('#tokenInputId').value = "";
        let causes = response.cause;
        let errorLog = "";
        causes.forEach((element) => {
            switch (element.code) {
                case "205":
                    errorLog += "Ingresa el número de tu tarjeta. ";
                    break;
                case "208":
                    errorLog += "Ingresa un mes válido. ";
                    break;
                case "209":
                    errorLog += "Ingresa un año válido. ";
                    break;
                case "212":
                    errorLog += "Selecciona el tipo de documento. ";
                    break;
                case "213":
                    errorLog += "Ingresa el titular de la tarjeta. ";
                    break;
                case "214":
                    errorLog += "Ingresa número de documento. ";
                    break;
                case "221":
                    errorLog += "Ingresa el titular de la tarjeta. ";
                    break;
                case "224":
                    errorLog += "Ingresa el código de seguridad. ";
                    break;
                case "E301":
                    errorLog += "Verifica tu número de tarjeta. ";
                    break;
                case "E302":
                    errorLog += "Verifica tu código de seguridad. ";
                    break;
                case "316":
                    errorLog += "Ingresa el titular de la tarjeta. ";
                    break;
                case "322":
                    errorLog += "Ingresa el código de seguridad. ";
                    break;
                case "322":
                    errorLog += "Revisa tu documento. ";
                    break;
                case "323":
                    errorLog += "Revisa tu documento. ";
                    break;
                case "324":
                    errorLog += "Revisa tu documento. ";
                    break;
                case "325":
                    errorLog += "Verifica el mes de vencimiento. ";
                    break;
                case "326":
                    errorLog += "Verifica el año de vencimiento. ";
                    break;
                case "011":
                    errorLog += "No se puede verificar tarjeta. Favor recargue la página e intente nuevamnete";
                    break;
            }
        });

        if (errorLog != "") {
            Swal.fire({
                icon: 'warning',
                title: 'Verifica los siguientes campos:',
                text: errorLog,
                footer: '<b>Te esperamos.</b>',
                allowOutsideClick: false,
                allowEscapeKey: false
            });
        }

        return false;
    } else {
        document.querySelector('#tokenInputId').value = response.id;
        //verifyToken();
        return true;
    }
};

//Call last functions implemented
function getRequiredData() {
    getAmount();
    getIdentificationType();
    getEmail();
}

//Get amount from summary checout and put on checkout input (Is required to mercado pago)
function getAmount() {
    document.querySelector("#amount").value = document.querySelector("#amountMainId").value;
};

//Get identificationTyoe provides for mercado pago from first page of personal data in checkout view and put on checkout input (Is required to mercado pago)
function getIdentificationType() {
    document.querySelector("#docTypeDiv").value = document.querySelector("#docType").value;
}

//Get email from first page of personal data in checkout view  put on checkout input (Is required to mercado pago)
function getEmail() {
    document.querySelector("#emailDivId").value = document.querySelector("#emaiMainlId").value;
}


