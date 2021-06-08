/*
    Date: 2020/02/03
    Company: Innova Marketing Systems S.A.S
    Programmer: Diego Arenas Tangarife
    Comments: Put here all script functions
 */

//Comsume subscription methods and returns state of petition. 
const $frmSubscription = document.querySelector("#frmSubscription");
if ($frmSubscription) {
    $frmSubscription.addEventListener('submit', async (event) => {
        event.preventDefault();
        if (!$(event.target).valid()) {
            return;
        }

        const url = `/Home/Index/`;
        Swal.fire({
            title: 'Creando subscripción!',
            html: '¡Me cerraré en pocos segundos!',
            onBeforeOpen: () => {
                Swal.showLoading()
            },
            allowOutsideClick: false,
            allowEscapeKey: false
        });

        const request = await fetch(url, {
            method: "POST",
            body: new FormData(event.currentTarget),
            headers: {
                "Accept": "application/json"
            }
        });

        if (request.ok) {
            const subsciptionResponse = await request.json();
            switch (subsciptionResponse.State) {
                case "Ok":
                    Swal.fire({
                        title: '¡Te has suscrito correctamente!',
                        position: 'top-end',
                        icon: 'success',
                        showConfirmButton: false,
                        timer: 3000,
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    });

                    $("#frmSubscription")[0].reset();
                    break;
                case "ModelError":
                    const errorMessage = subsciptionResponse.Errors.reduce((acc, el) => { return acc + `- ${el.ErrorMessage}<br/>` }, "");
                    Swal.fire({
                        icon: 'warning',
                        title: 'Por favor verifica',
                        html: errorMessage,
                        footer: '<b>Te esperamos.</b>',
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    });

                    break;
                case "Error":
                    Swal.fire({
                        icon: 'error',
                        title: 'Algo salió mal :(',
                        text: 'Lo sentimos.',
                        footer: '<b><a href="/Info/ContactUs">Por favor infórmanos</a></b>',
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    });

                    break;
                default:
                    Swal.fire({
                        icon: 'info',
                        title: 'Ya estás suscrito',
                        text: '¡Gracias!'
                    });
            }
        }
    });
}

//Disable element on leds form
function enableRetakeCarName() {
    const retake = document.querySelector("#cmbRetakeInterestedId").value;
    if (retake === "1") {
        document.querySelector("#Retake").disabled = false;
    } else {
        document.querySelector("#Retake").disabled = true;
    }
}

//Get Leads
const $frmInventoryLeads = document.querySelector("#frmInventoryLeads");
if ($frmInventoryLeads) {
    $frmInventoryLeads.addEventListener('submit', async (event) => {
        debugger;
        event.preventDefault();
        const url = `/Home/InterestedCustomers/`;
        Swal.fire({
            title: 'Creando registro!',
            html: '¡Me cerraré en pocos segundos!',
            onBeforeOpen: () => {
                Swal.showLoading()
            },
            allowOutsideClick: false,
            allowEscapeKey: false
        });

        const request = await fetch(url, {
            method: "POST",
            body: new FormData($frmInventoryLeads),
            headers: {
                "Accept": "application/json"
            }
        });

        if (request.ok) {
            const subsciptionResponse = await request.json();
            switch (subsciptionResponse.State) {
                case "Ok":
                    $("#frmInventoryLeads")[0].reset();
                    document.querySelector("#cmbRetakeInterestedId").value = "0";
                    enableRetakeCarName();

                    Swal.fire({
                        title: '¡Nos comunicaremos contigo en breve!',
                        position: 'top-end',
                        icon: 'success',
                        showConfirmButton: false,
                        timer: 3000,
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    });
                    break;
                case "ModelError":
                    const message = subsciptionResponse.Errors.reduce((acc, el) => { return acc + `- ${el.ErrorMessage}<br>` }, "");
                    Swal.fire({
                        icon: 'warning',
                        title: 'Verifica lo siguiente:',
                        html: message,
                        footer: '<b>Te esperamos.</b>',
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    });

                    break;
                case "Error":
                    Swal.fire({
                        icon: 'error',
                        title: 'Algo salió mal :(',
                        text: 'Lo sentimos.',
                        footer: '<b><a href="/Info/ContactUs">Por favor infórmanos</a></b>',
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    });

                    break;
                default:
                    Swal.fire({
                        icon: 'info',
                        title: 'Ya tienes una solicitud de información en proceso',
                        text: '¡Gracias!'
                    });
            }
        }
    });
}


//Gets cascade dropdown list of categories and models
async function getProducts(makeId, categoryId) {
    Swal.fire({
        title: 'Cargando Modelos',
        html: '¡Me cerraré en pocos segundos!',
        allowOutsideClick: false,
        allowEscapeKey: false,
        onBeforeOpen: () => {
            Swal.showLoading()
        }
    });

    if (document.querySelector(".validation-summary-errors")) {
        document.querySelector(".validation-summary-errors").innerHTML = '';
    }

    document.getElementById("categoryId").value = categoryId;
    let url = `./Home/make/${makeId}/category/${categoryId}`;
    let response = await fetch(url, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    });

    if (response.ok) {
        let productsForCategory = await response.json();
        let cbo = document.getElementById("idModels");
        cbo.innerHTML = "";

        let optDefault = document.createElement("option");
        optDefault.appendChild(document.createTextNode("Cualquier modelo"));
        optDefault.value = 0;
        cbo.appendChild(optDefault);

        productsForCategory.forEach((element) => {
            let opt = document.createElement("option");
            opt.appendChild(document.createTextNode(element.ModelName));
            opt.value = parseInt(element.ModelsId);
            cbo.appendChild(opt);
        });

        cbo.focus();
        //To get all models on change select input
        cbo.setAttribute("onchange", "recordsPerPage()");
        setTimeout(() => Swal.close(), 700);

        ///To get all products for model and render inmediatly
        recordsPerPage();
    }
};

// Gets full url to videos form youtube... to render on partial view
function getYoutubeVideoPartial(url) {
    url = url.replace("watch?v=", "embed/")
    let change = `<iframe width="560" height="315" src="${url}" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>`;
    document.getElementById("modal-content").innerHTML = change;
};

// Get content for agreement
function getAgreementPartial(agreement, title) {
    document.getElementById("modal-content").innerHTML = agreement;
    document.getElementById("modal-header").innerHTML = `<h3 class="modal-title" id="exampleModalScrollableTitle">${title}</h3>`;
};

// Shows picture on home page
function getOnLoadPicture(subject, source) {
    Swal.fire({
        title: subject,
        imageUrl: source,
        width: 560,
        imageWidth: 560,
        imageHeight: 560,
        imageAlt: 'Custom image',
        showConfirmButton: true,
        showClass: {
            popup: 'animated fadeInDown slower'
        },
        hideClass: {
            popup: 'animated fadeOutUp faster'
        }
    });
}

//Add Item to shopping cart
async function addToCart(productId, inventoryItemsId, quantity) {
    const cookieConsent = getCookie("MotovalleCookieConsent");
    if (cookieConsent === "") {
        Swal.fire({
            icon: 'warning',
            title: 'Acepta nuestra política de cookies',
            text: 'Te esperamos',
            footer: `<b><a href="/Home/Privacy/">MÁS INFORMACIÓN</a></b>`,
            allowOutsideClick: false,
            allowEscapeKey: false
        });

        return;
    }

    let data = { ProductId: productId, InventoryItemId: inventoryItemsId, Quantity: quantity };
    let url = `/ShoppingCart/AddItemToCart/`;
    Swal.fire({
        title: 'Agregando Ítem al carrito',
        html: '¡Me cerraré en pocos segundos!',
        onBeforeOpen: () => {
            Swal.showLoading()
        },
        allowOutsideClick: false,
        allowEscapeKey: false
    });

    let request = await fetch(url, {
        method: "POST",
        body: JSON.stringify(data),
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    });

    if (request.ok) {
        let shoppingCartRecordResponse = await request.json();
        switch (shoppingCartRecordResponse.State) {
            case "Ok":
                await Swal.fire({
                    title: '¡Agregado con éxito!',
                    showClass: {
                        popup: 'animated fadeInUp faster'
                    },
                    hideClass: {
                        popup: 'animated fadeOutRight faster'
                    },
                    text: "Continuar comprando",
                    footer: '<a href="/ShoppingCart"><b>Ir al Carrito</b></a>',
                    position: 'top-end',
                    icon: 'success',
                    showConfirmButton: true,
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    timer: 10000,
                    timerProgressBar: true
                });

                url = `/ShoppingCart/GetShoppingCartSummaryViewComponent/`;
                fetch(url, {
                    method: "GET",
                    headers: {
                        "Accept": "application/json",
                        "Content-Type": "application/json"
                    }
                }).then((serverResponse) => {
                    if (serverResponse.ok) {
                        return serverResponse.text();
                    }
                }).then((htmlContent) => {
                    document.getElementById("idDivVC").innerHTML = htmlContent;
                });

                break;
            case "NoAuthorize":
                Swal.fire({
                    icon: 'warning',
                    title: 'Debes iniciar sesión',
                    text: 'Te esperamos',
                    footer: `<b><a href="/Identity/Account/Login?ReturnUrl=%2FFord%2FProduct%2F${productId}%2FInventory%2F${inventoryItemsId}%2FDetails">INICIA SESIÓN</a>/</b><b><a href="/Identity/Account/Register">REGÍSTRATE</a></b>`,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

                break;
            default:
                Swal.fire({
                    icon: 'error',
                    title: 'Algo salió mal =(',
                    text: 'Lo sentimos',
                    footer: '<b><a href="/Info/ContactUs">Por favor infórmanos</a></b>',
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });
        }
    } else {
        Swal.fire({
            icon: 'error',
            title: 'Algo salió mal =(',
            text: 'Lo sentimos',
            footer: '<b><a href="/Info/ContactUs">Por favor infórmanos</a></b>',
            allowOutsideClick: false,
            allowEscapeKey: false
        });
    }
};

//Remove Item from cart
function removeItemFromCart(shoppingCartRecordsId) {
    let url = `/ShoppingCart/RemoveItem/${shoppingCartRecordsId}`;
    Swal.fire({
        title: 'Eliminando producto',
        html: '¡Me cerraré en pocos segundos!',
        allowOutsideClick: false,
        allowEscapeKey: false,
        onBeforeOpen: () => {
            Swal.showLoading()
        }
    });

    fetch(url, {
        method: "DELETE",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    }).then((serverResponse) => {
        if (serverResponse.ok) {
            return serverResponse.text();
        }
    }).then((content) => {
        if (content != "Error") {
            return true;
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Algo salió mal =(',
                text: 'Lo sentimos',
                footer: '<b><a href="/Info/ContactUs">Por favor infórmanos</a></b>',
                allowOutsideClick: false,
                allowEscapeKey: false
            });
        };
    }).then(() => {
        url = `/ShoppingCart/GetShoppingCartCheckout/`;
        document.getElementById("shoppingItemRecordsDisplayId").innerHTML = `<div class="text-center"><img src="/images/flags/loading.gif" alt="Cargando..."></div>`
        fetch(url, {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        }).then((serverResponse) => {
            if (serverResponse.ok) {
                return serverResponse.text();
            }
        }).then((htmlContent) => {
            document.getElementById("shoppingItemRecordsDisplayId").innerHTML = htmlContent;
        });
    }).then(() => {
        url = `/ShoppingCart/GetShoppingCartSummaryCheckout/`;
        document.getElementById("shoppingItemRecordsSummaryDisplayId").innerHTML = `<div class="text-center"><img src="/images/flags/loading.gif" alt="Cargando..."></div>`

        fetch(url, {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        }).then((serverResponse) => {
            if (serverResponse.ok) {
                return serverResponse.text();
            }
        }).then((htmlSummaryContent) => {
            document.getElementById("shoppingItemRecordsSummaryDisplayId").innerHTML = htmlSummaryContent;
        });
    }).then(() => {
        url = `/ShoppingCart/GetShoppingCartSummaryViewComponent/`;
        fetch(url, {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        }).then((serverResponse) => {
            if (serverResponse.ok) {
                return serverResponse.text();
            }
        }).then((htmlContentDivVC) => {
            document.getElementById("idDivVC").innerHTML = htmlContentDivVC;
            setTimeout(() => {
                Swal.close();
                document.querySelector("#totalAmountFoundingRequestDivId").value = number_format(document.querySelector("#amountMainId").value, 2);
            }, 500);


            //document.querySelector("#totalAmountFoundingRequestDivId").value = document.querySelector("#amountMainId").value;
        });
    });
};

//Default option
function onBuilding() {
    Swal.fire({
        icon: 'info',
        title: 'Opción en construcción',
        text: 'Dános una espera',
        footer: '<b><a href="/Info/ContactUs">Contáctanos</a></b>',
        allowOutsideClick: false,
        allowEscapeKey: false
    });
}

//Get pagintation and records per page
function recordsPerPage() {
    let makesName = document.getElementById("makesName").value;
    makesName = makesName.replace(/\s/g, '');
    let makesId = document.getElementById("makes").value;
    let categoryId = document.getElementById("categoryId").value;
    let modelId = document.getElementById("idModels").value;
    let minYear = document.getElementById("fordMinYearId").value;
    let maxYear = document.getElementById("fordMaxYearId").value;
    let minPrice = document.getElementById("fordPriceMinId").value;
    let maxPrice = document.getElementById("fordPriceMaxId").value;
    let sortBy = document.getElementById("fordSortById").value;
    let recordsSelected = document.getElementById("dropDownListRecordsPerPage").value;
    let url = `/Home/RecordsPerPage/`;
    let bodyData = { CategoryId: categoryId, MakeId: makesId, ModelId: modelId, PriceRangeMin: minPrice, PriceRangeMax: maxPrice, YearMin: minYear, YearMax: maxYear, RecordsPerPage: recordsSelected, PageNumber: 1, SortBy: sortBy };

    fetch(url,
        {
            method: "POST",
            body: JSON.stringify(bodyData),
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        }).then((serverResponse) => {
            if (serverResponse.ok) {
                return serverResponse.json();
            };
        }).then((response) => {
            if (response != null) {
                Swal.fire({
                    title: 'Cargando...',
                    html: '¡Me cerraré en pocos segundos!',
                    allowOutsideClick: false,
                    allowEscapeKey: false,
                    onBeforeOpen: () => {
                        Swal.showLoading()
                    }
                });

                let datacontent = '';
                let ButtonContent = '';
                let formatter = new Intl.NumberFormat('en-US', {
                    style: 'currency',
                    currency: 'USD',
                });

                document.getElementById("itemRecordsDisplay").innerHTML = '';
                document.getElementById("paginationDivId").innerHTML = '';
                response.Items.forEach((item) => {

                    datacontent += `<div class="b-items__cars-one wow zoomInUp" data-wow-delay="0.25s">
                                                    <div class="b-items__cars-one-img">
                                                        <img src="/${item.PictureUrl ?? ''}")" alt="${item.ProductName}" style="max-width:270px; max-height:230px;" />`;

                    if (item.IsFeatured) {
                        datacontent += '<span class="b-items__cars-one-img-type m-premium">DESTACADO</span>';
                    }

                    if (item.YoutubeUrl !== "" || item.YoutubeUrl !== null) {
                        datacontent += `<a data-toggle="modal" data-target="#idDetailsModal" href="#" onclick="getYoutubeVideoPartial('${item.YoutubeUrl}')" class="b-items__cars-one-img-video"><span class="fa fa-film"></span>VIDEO</a>`;
                    }

                    let km = makesName.toLowerCase().includes("carshow") ? `${new Intl.NumberFormat().format(item.Millage)} Km` : '0 Km';
                    let href = makesName.toLowerCase().includes("carshow") ? `<a href="${makesName}/Product/${item.ProductId}/Inventory/${item.InventoryItemsId}/Details" class="btn m-btn pull-right">VER DETALLES<span class="fa fa-angle-right"></span></a>` : `<a href="${makesName}/Product/${item.ProductId}/Inventory" class="btn m-btn pull-right">VER STOCK<span class="fa fa-angle-right"></span></a>`;

                    datacontent += `</div>
                                            <div class="b-items__cars-one-info">
                                                <h2>${item.ProductName}</h2>
                                                <div class="row s-noRightMargin">
                                                    <div class="col-md-9 col-xs-12">
                                                        <p>${item.ProductDescription}</p>
                                                        <div class="m-width row m-smallPadding">
                                                            <div class="col-xs-6">
                                                                <div class="row m-smallPadding">
                                                                    <div class="col-xs-6">
                                                                        <span class="b-items__cars-one-info-title">Tipo:</span>
                                                                        <span class="b-items__cars-one-info-title">Kilometraje:</span>
                                                                        <span class="b-items__cars-one-info-title">Trasmisión:</span>
                                                                    </div>
                                                                    <div class="col-xs-6">
                                                                        <span class="b-items__cars-one-info-value">${item.CategoryName}</span>
                                                                        <span class="b-items__cars-one-info-value">${km}</span>
                                                                        <span class="b-items__cars-one-info-value">${item.TransmissionName ?? 'N/A'}</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-6">
                                                                <div class="row m-smallPadding">
                                                                    <div class="col-xs-5">
                                                                        <span class="b-items__cars-one-info-title">Caballos de fuerza:</span>
                                                                        <span class="b-items__cars-one-info-title">Modelo:</span>
                                                                        <span class="b-items__cars-one-info-title">Especs.:</span>
                                                                    </div>
                                                                    <div class="col-xs-7">
                                                                        <span class="b-items__cars-one-info-value">${item.HorsePower} Hp</span>
                                                                        <span class="b-items__cars-one-info-value">${item.Year}</span>
                                                                        <span class="b-items__cars-one-info-value">${item.Passengers}-Pasajeros, ${item.PassengersDoors}-Puertas</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3 col-xs-12">
                                                        <div class="b-items__cars-one-info-price">
                                                            <div class="pull-right">
                                                                <h3>Precio Venta:</h3>
                                                                <h4>${formatter.format(item.SalesPrice)}</h4>
                                                                <span class="b-items__cars-one-info-title ">Inventario</span>
                                                            </div>
                                                            ${href}
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>`;
                });


                if (response.PagesCount > 0) {

                    ButtonContent += `<a href="javascript:;" onclick="paginationRecords(1, '${makesName}')" class="m-left"><span class="fa fa-angle-left"></span></a>`;
                    ButtonContent += `<span class="m-active"><a href="javascript:;" onclick="paginationRecords(1, '${makesName}')">1</a></span>`;
                    if (response.PagesCount > 1) {
                        for (var d = 2; d <= response.PagesCount; d++) {
                            if (d == response.PagesCount) {
                                ButtonContent += `<span><a href="javascript:;" onclick="paginationRecords(${d}, '${makesName}')">${d}</a></span>`;
                                ButtonContent += `<a href="javascript:;" onclick="paginationRecords(${d}, '${makesName}')" class="m-right"><span class="fa fa-angle-right"></span></a>`;
                            } else {
                                ButtonContent += `<span><a href="javascript:;" onclick="paginationRecords(${d}, '${makesName}')">${d}</a></span>`;
                            }
                        }
                    } else {
                        ButtonContent += `<a href="javascript:;" onclick="paginationRecords(1, '${makesName}')" class="m-right"><span class="fa fa-angle-right"></span></a>`;
                    }

                    document.getElementById("paginationDivId").innerHTML = ButtonContent;
                } else {
                    datacontent += `<div class="alert alert-info text-center wow zoomInUp" role="alert" data-wow-delay="0.5s">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <strong>No hay registros para mostrar</strong> &#128577;
                    </div>`;
                }

                document.getElementById("itemRecordsDisplay").innerHTML = datacontent;
            }

            //$('html,body').animate({
            //    scrollTop: 800 - $('#search').height()
            //}, 800);

            setTimeout(() => Swal.close(), 1200);
        });
};

//Get pagintatio footer
function paginationRecords(pageNumber, makesName) {
    Swal.fire({
        title: `Cargando página ${pageNumber.toString()}`,
        html: '¡Me cerraré en pocos segundos!',
        allowOutsideClick: false,
        allowEscapeKey: false,
        onBeforeOpen: () => {
            Swal.showLoading()
        }
    });

    makesName = makesName.replace(/\s/g, '');
    let makesId = document.getElementById("makes").value;
    let categoryId = document.getElementById("categoryId").value;
    let modelId = document.getElementById("idModels").value;
    let minYear = document.getElementById("fordMinYearId").value;
    let maxYear = document.getElementById("fordMaxYearId").value;
    let minPrice = document.getElementById("fordPriceMinId").value;
    let maxPrice = document.getElementById("fordPriceMaxId").value;
    let sortBy = document.getElementById("fordSortById").value;
    let recordsSelected = document.getElementById("dropDownListRecordsPerPage").value;
    let url = `/Home/Pagination/`;
    let bodyData = { CategoryId: categoryId, MakeId: makesId, ModelId: modelId, PriceRangeMin: minPrice, PriceRangeMax: maxPrice, YearMin: minYear, YearMax: maxYear, RecordsPerPage: recordsSelected, PageNumber: pageNumber, SortBy: sortBy };

    fetch(url,
        {
            method: "POST",
            body: JSON.stringify(bodyData),
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        }).then((serverResponse) => {
            if (serverResponse.ok) {
                return serverResponse.json();
            };
        }).then((response) => {
            if (response != null) {

                //appending the fetched value to the table
                var formatter = new Intl.NumberFormat('en-US', {
                    style: 'currency',
                    currency: 'USD',
                });

                document.getElementById("itemRecordsDisplay").innerHTML = '';
                document.getElementById("paginationDivId").innerHTML = '';

                let datacontent = '';
                let ButtonContent = '';

                response.Items.forEach((item) => {
                    datacontent += `<div class="b-items__cars-one wow zoomInUp" data-wow-delay="0.25s">
                                                        <div class="b-items__cars-one-img">
                                                            <img src="/${item.PictureUrl}")" alt="${item.ProductName}" style="max-width:270px; max-height:230px;" />`;
                    if (item.IsFeatured) {
                        datacontent += '<span class="b-items__cars-one-img-type m-premium">DESTACADO</span>';
                    }

                    if (item.YoutubeUrl !== "" || item.YoutubeUrl !== null) {
                        datacontent += `<a data-toggle="modal" data-target="#idDetailsModal" href="#" onclick="getYoutubeVideoPartial('${item.YoutubeUrl}')" class="b-items__cars-one-img-video"><span class="fa fa-film"></span>VIDEO</a>`;
                    }

                    let km = makesName.toLowerCase().includes("carshow") ? `${new Intl.NumberFormat().format(item.Millage)} Km` : '0 Km';
                    let href = makesName.toLowerCase().includes("carshow") ? `<a href="${makesName}/Product/${item.ProductId}/Inventory/${item.InventoryItemsId}/Details" class="btn m-btn pull-right">VER DETALLES<span class="fa fa-angle-right"></span></a>` : `<a href="${makesName}/Product/${item.ProductId}/Inventory" class="btn m-btn pull-right">VER STOCK<span class="fa fa-angle-right"></span></a>`;

                    datacontent += `</div>
                                        <div class="b-items__cars-one-info">
                                            <h2>${item.ProductName}</h2>
                                            <div class="row s-noRightMargin">
                                                <div class="col-md-9 col-xs-12">
                                                    <p>${item.ProductDescription}</p>
                                                    <div class="m-width row m-smallPadding">
                                                        <div class="col-xs-6">
                                                            <div class="row m-smallPadding">
                                                                <div class="col-xs-6">
                                                                    <span class="b-items__cars-one-info-title">Tipo:</span>
                                                                    <span class="b-items__cars-one-info-title">Kilometraje:</span>
                                                                    <span class="b-items__cars-one-info-title">Trasmisión:</span>
                                                                </div>
                                                                <div class="col-xs-6">
                                                                    <span class="b-items__cars-one-info-value">${item.CategoryName}</span>
                                                                    <span class="b-items__cars-one-info-value">${km}</span>
                                                                    <span class="b-items__cars-one-info-value">${item.TransmissionName ?? 'N/A'}</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-xs-6">
                                                            <div class="row m-smallPadding">
                                                                <div class="col-xs-5">
                                                                    <span class="b-items__cars-one-info-title">Caballos de fuerza:</span>
                                                                    <span class="b-items__cars-one-info-title">Modelo:</span>
                                                                    <span class="b-items__cars-one-info-title">Especs.:</span>
                                                                </div>
                                                                <div class="col-xs-7">
                                                                    <span class="b-items__cars-one-info-value">${item.HorsePower} Hp</span>
                                                                    <span class="b-items__cars-one-info-value">${item.Year}</span>
                                                                    <span class="b-items__cars-one-info-value">${item.Passengers}-Pasajeros, ${item.PassengersDoors}-Puertas</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-3 col-xs-12">
                                                    <div class="b-items__cars-one-info-price">
                                                        <div class="pull-right">
                                                            <h3>Precio Venta:</h3>
                                                            <h4>${formatter.format(item.SalesPrice)}</h4>
                                                            <span class="b-items__cars-one-info-title ">Inventario</span>
                                                        </div>
                                                        ${href}
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>`;
                });

                if (response.PagesCount > 0) {
                    ButtonContent += `<a href="javascript:;" onclick="paginationRecords(1, '${makesName}')" class="m-left"><span class="fa fa-angle-left"></span></a>`;
                    for (var d = 1; d <= response.PagesCount; d++) {
                        if (pageNumber == d) {
                            ButtonContent += `<span href="javascript:;" class="m-active"><a href="#" onclick="paginationRecords(${d}, '${makesName}') id="pageNumberSelectedId">${d}</a></span>`;
                        }
                        else {
                            ButtonContent += `<span><a href="javascript:;" onclick="paginationRecords(${d}, '${makesName}')">${d}</a></span>`;
                        }

                        if (d == response.PagesCount) {
                            ButtonContent += `<a href="javascript:;" onclick="paginationRecords(${d}, '${makesName}')" class="m-right"><span class="fa fa-angle-right"></span></a>`;
                        }
                    }

                    document.getElementById("paginationDivId").innerHTML = ButtonContent;
                } else {
                    datacontent += `<div class="alert alert-info text-center wow zoomInUp" role="alert" data-wow-delay="0.5s">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <strong>No hay registros para mostrar</strong> &#128577;
                    </div>`;
                }

                document.getElementById("itemRecordsDisplay").innerHTML = datacontent;
            }
        });

    $('html,body').animate({
        scrollTop: 1200 - $('#search').height()
    }, 800);

    setTimeout(() => Swal.close(), 1200);
};

// Hidden function when the radio buttons are selected on Checkout view
function radioButtonHandler() {
    isCreditCard = document.getElementById("idRbCreditCard").checked;
    isFunding = document.getElementById("idRbFinancial").checked;
    isCreditCardPartial = document.getElementById("idRbCreditCardPartial").checked;

    if (isCreditCard) {
        document.getElementById("idRbCreditCard").value = true;
        document.getElementById("idRbFinancial").value = false;
        document.getElementById("creditCardDivId").style.display = "block";
        document.getElementById("financialDivId").style.display = "none";
        document.getElementById("paymentSelectedIdForm").value = "0";
        $('#financialDivId').find('input').val('');
        $('#creditCardValuesId').find('input').val('');
        document.getElementById("partialAmountId").disabled = true;
        document.getElementById("partialAmountId").value = "$0";
        document.getElementById("partialAmountValidationId").innerHTML = "";

        //Set default mercado pago installments
        let select = document.querySelector("#installments");
        select.innerHTML = "";
        document.querySelector("#issuerIdDiv").value = "";
        let optDefault = document.createElement("option");
        optDefault.appendChild(document.createTextNode("36 Cuotas"));
        optDefault.value = 36;
        select.appendChild(optDefault);
    }

    if (isFunding) {
        document.getElementById("idRbCreditCard").value = false;
        document.getElementById("idRbFinancial").value = true;
        document.getElementById("creditCardDivId").style.display = "none";
        document.getElementById("financialDivId").style.display = "block";
        document.getElementById("paymentSelectedIdForm").value = "1";
        document.querySelector("#totalAmountFoundingRequestDivId").value = number_format(document.querySelector("#amountMainId").value, 2);
        $('#creditCardDivId').find('input').val('');
        document.getElementById("partialAmountId").disabled = true;
        document.getElementById("partialAmountId").value = "$0";

        //Set default mercado pago installments
        let select = document.querySelector("#installments");
        select.innerHTML = "";
        document.querySelector("#issuerIdDiv").value = "";
        let optDefault = document.createElement("option");
        optDefault.appendChild(document.createTextNode("36 Cuotas"));
        optDefault.value = 36;
        select.appendChild(optDefault);
    }

    if (isCreditCardPartial) {
        document.getElementById("idRbCreditCard").value = true;
        document.getElementById("idRbFinancial").value = false;
        document.getElementById("creditCardDivId").style.display = "block";
        document.getElementById("financialDivId").style.display = "none";
        document.getElementById("paymentSelectedIdForm").value = "2";
        $('#financialDivId').find('input').val('');
        $('#creditCardValuesId').find('input').val('');
        document.getElementById("partialAmountId").disabled = false;

        //Set default mercado pago installments
        let select = document.querySelector("#installments");
        select.innerHTML = "";
        document.querySelector("#issuerIdDiv").value = "";
        let optDefault = document.createElement("option");
        optDefault.appendChild(document.createTextNode("36 Cuotas"));
        optDefault.value = 36;
        select.appendChild(optDefault);
    }
}

////Validate Model For Funding Requests
async function validateModelForFundingRequest() {
    let generalResult = false;
    Swal.fire({
        title: 'Verificando información envíada...',
        html: '¡Me cerraré en pocos segundos!',
        allowOutsideClick: false,
        allowEscapeKey: false,
        onBeforeOpen: () => {
            Swal.showLoading()
        }
    });

    const bodyData = getDataForCheckout();
    const url = "/ShoppingCart/ValidateModelForFundingRequest/";
    const serverResponse = await fetch(url,
        {
            method: "POST",
            body: JSON.stringify(bodyData),
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        });

    await wait(2000);

    if (serverResponse.ok) {
        const response = await serverResponse.json();
        switch (response.ControlStatus) {
            case "ModelError":
                let errorLog = "";
                response.Errors.forEach((element) => {
                    errorLog += element + " ";
                });

                await Swal.fire({
                    icon: 'warning',
                    title: 'Verifica los siguientes campos:',
                    text: errorLog,
                    footer: '<b>Te esperamos.</b>',
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

                return generalResult;
            case "Error":
                await Swal.fire({
                    icon: 'error',
                    title: '¡Ups!. Ha ocurrido un error',
                    html: response.Message,
                    footer: '<b>Por favor intenta nuevamente.</b>',
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

                return generalResult;
            case "ModelOk":
                generalResult = true;
                return generalResult;
            default:
                await Swal.fire({
                    icon: 'error',
                    title: 'Respuesta inesperada',
                    text: 'Lo sentimos, intenta más tarde.',
                    footer: '<b><a href="/Info/ContactUs">Por favor infórmanos</a></b>',
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

                return generalResult;
        }
    } else {
        await Swal.fire({
            icon: 'error',
            title: 'Servidor no disponible',
            text: 'Lo sentimos, intentan más tarde.',
            footer: '<b><a href="/Info/ContactUs">Por favor infórmanos</a></b>',
            allowOutsideClick: false,
            allowEscapeKey: false
        });

        return generalResult;
    }

    return generalResult;
};

////Get Santander Bank funding request response
async function tryGetSantanderFundingRequest() {

    let generalResult = { Result: false, ControlStatus: null, Response: null, Message: null, BankName: "Banco Santantader", Bank: 1 };
    const bodyData = getDataForCheckout();
    const url = "/ShoppingCart/TryGetSantanderBankFundingRequest/";
    const serverResponse = await fetch(url,
        {
            method: "POST",
            body: JSON.stringify(bodyData),
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        });

    if (serverResponse.ok) {
        const response = await serverResponse.json();
        switch (response.ControlStatus) {
            case "ModelError":
                let errorLog = "<div style='text-aling:left'>";
                response.Errors.forEach((element) => {
                    errorLog += "- " + element + " <br/>";
                });

                errorLog += "</div>";
                generalResult.ControlStatus = response.ControlStatus;
                generalResult.Message = errorLog;

                //Comment Swal when another bank request exists
                await Swal.fire({
                    icon: 'warning',
                    title: 'Verifica los siguientes campos:',
                    text: errorLog,
                    footer: '<b>Te esperamos.</b>',
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

                break;
            case "Error":
                //Comment Swal when another bank request exists
                await Swal.fire({
                    icon: 'error',
                    title: '¡Ups!. Ha ocurrido un error',
                    html: response.Message,
                    footer: '<b>Por favor intenta nuevamente.</b>',
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

                generalResult.ControlStatus = response.ControlStatus;
                generalResult.Message = response.Message;
                break;
            case "Info":
                //Comment Swal when another bank request exists
                await Swal.fire({
                    icon: 'info',
                    title: 'Información',
                    html: response.Message,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

                generalResult.ControlStatus = response.ControlStatus;
                generalResult.Message = response.Message;
                break;
            case "FundingRequestOk":
                generalResult.Result = true;
                generalResult.Response = response.Response;
                generalResult.Message = response.Message;
                generalResult.ControlStatus = response.ControlStatus;
                break;
            default:
                //Comment Swal when another bank request exists
                await Swal.fire({
                    icon: 'error',
                    title: 'Respuesta inesperada',
                    text: 'Lo sentimos, intenta más tarde.',
                    footer: '<b><a href="/Info/ContactUs">Por favor infórmanos</a></b>',
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

                generalResult.ControlStatus = "Other";
                generalResult.Message = "Respuesta Inesperada";
                break;
        }
    } else {
        //Comment Swal when another bank request exists
        Swal.fire({
            icon: 'error',
            title: 'Servidor no disponible',
            text: 'Lo sentimos, intentan más tarde.',
            footer: '<b><a href="/Info/ContactUs">Por favor infórmanos</a></b>',
            allowOutsideClick: false,
            allowEscapeKey: false
        });

        generalResult.ControlStatus = "Other";
        generalResult.Message = "Servidor no disponible";
        return generalResult;
    }

    return generalResult;
};

////Try to Get West Bank Funding Request
async function tryGetWestBankFundingRequest() {

    let generalResult = { Result: false, ControlStatus: null, Response: null, Message: null, BankName: "Banco de Occidente", Bank: 0 };
    const bodyData = getDataForCheckout();
    const url = "/ShoppingCart/TryGetWestBankFundingRequest/";
    const serverResponse = await fetch(url,
        {
            method: "POST",
            body: JSON.stringify(bodyData),
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json"
            }
        });

    if (serverResponse.ok) {
        const response = await serverResponse.json();
        switch (response.ControlStatus) {
            case "ModelError":
                let errorLog = "<div style='text-aling:left'>";
                response.Errors.forEach((element) => {
                    errorLog += "- " + element + " <br/>";
                });

                errorLog += "</div>";
                generalResult.ControlStatus = response.ControlStatus;
                generalResult.Message = errorLog;

                //Comment Swal when another bank request exists
                await Swal.fire({
                    icon: 'warning',
                    title: 'Verifica los siguientes campos:',
                    text: errorLog,
                    footer: '<b>Te esperamos.</b>',
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

                break;
            case "Error":
                //Comment Swal when another bank request exists
                await Swal.fire({
                    icon: 'error',
                    title: '¡Ups!. Ha ocurrido un error',
                    html: response.Message,
                    footer: '<b>Por favor intenta nuevamente.</b>',
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

                generalResult.ControlStatus = response.ControlStatus;
                generalResult.Message = response.Message;
                break;
            case "Info":
                //Comment Swal when another bank request exists
                await Swal.fire({
                    icon: 'info',
                    title: 'Información',
                    html: response.Message,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

                generalResult.ControlStatus = response.ControlStatus;
                generalResult.Message = response.Message;
                break;
            case "FundingRequestOk":
                generalResult.Result = true;
                generalResult.Response = response.Response;
                generalResult.Message = response.Message;
                generalResult.ControlStatus = response.ControlStatus;

                //Comment Swal when another bank request exists
                await Swal.fire({
                    icon: "success",
                    title: "Solicitud de crédito exitosa",
                    html: response.Message,
                    timerProgressBar: true,
                    showConfirmButton: false,
                    footer: "Finalizando proceso de solicitud... En un momento continuaremos",
                    position: 'top-center',
                    timer: 10000,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

                break;
            default:
                //Comment Swal when another bank request exists
                await Swal.fire({
                    icon: 'error',
                    title: 'Respuesta inesperada',
                    text: 'Lo sentimos, intenta más tarde.',
                    footer: '<b><a href="/Info/ContactUs">Por favor infórmanos</a></b>',
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

                generalResult.ControlStatus = "Other";
                generalResult.Message = "Respuesta Inesperada";
                break;
        }
    } else {
        //Comment Swal when another bank request exists
        await Swal.fire({
            icon: 'error',
            title: 'Servidor no disponible',
            text: 'Lo sentimos, intentan más tarde.',
            footer: '<b><a href="/Info/ContactUs">Por favor infórmanos</a></b>',
            allowOutsideClick: false,
            allowEscapeKey: false
        });

        generalResult.ControlStatus = "Other";
        generalResult.Message = "Servidor no disponible";
        return generalResult;
    }

    return generalResult;
};

///Go To Checkout
async function goToCheckout() {
    const sure = await Swal.fire({
        title: '¿Estás completamente seguro de continuar?',
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '¡Sí!',
        allowOutsideClick: false,
        allowEscapeKey: false
    });

    //continue?
    if (sure.isConfirmed) {

        //Declaration
        let paymentMethodSelected = document.querySelector("#paymentSelectedIdForm").value;
        let time = 0;
        let fundingResult;
        let fundingRequestMessage = "";

        //(0 || 2 --> Credit card)
        if (paymentMethodSelected == "0" || paymentMethodSelected == "2") {
            getTokenForCreditCard();
            time = 3000;
        }

        //Uncomment to get Funding Request
        //(1 - Funding Request) --> Santander Bank request
        if (paymentMethodSelected == "1") {

            ////Check Model
            const modelValidResponse = await validateModelForFundingRequest();
            if (!modelValidResponse) {
                return;
            }

            Swal.fire({
                title: 'Procesando solicitud de crédito...',
                html: '¡Me cerraré en pocos segundos!',
                allowOutsideClick: false,
                allowEscapeKey: false,
                onBeforeOpen: () => {
                    Swal.showLoading()
                }
            });

            /*******************************************************
            //// Uncomment to get all funding request on parallrel
            //// moreover, you must remove message into each funding request
            //// and creates a general result to handler responses

            ////Call All Funding Request at same time
            const fundingRequestsResponses = await Promise.all([
                tryGetSantanderFundingRequest(),
                tryGetWestBankFundingRequest()
            ]);

            ////Handler Response
            for (var i = 0; i < fundingRequestsResponses.length; i++) {
                if (fundingRequestsResponses[i].Result) {
                    //Mostrar un popup donde el usuario seleccione con cual banco continuar
                    await Swal.fire({
                        icon: "success",
                        title: `Solicitud de crédito exitosa (${fundingRequestsResponses[i].BankName})`,
                        html: fundingRequestsResponses[i].Message,
                        timerProgressBar: true,
                        showConfirmButton: false,
                        position: 'top-center',
                        timer: 10000,
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    });
                } else {
                    switch (fundingRequestsResponses[i].ControlStatus) {
                        case "ModelError":
                            await Swal.fire({
                                icon: 'warning',
                                title: `${fundingRequestsResponses[i].BankName} dice: Verifica los siguiente`,
                                html: fundingRequestsResponses[i].Message,
                                footer: '<b>Te esperamos.</b>',
                                allowOutsideClick: false,
                                allowEscapeKey: false
                            });

                            break;
                        case "Error":
                            await Swal.fire({
                                icon: 'error',
                                title: '¡Ups!. Ha ocurrido un error',
                                html: fundingRequestsResponses[i].Message,
                                footer: '<b>Por favor intenta nuevamente.</b>',
                                allowOutsideClick: false,
                                allowEscapeKey: false
                            });

                            break;
                        case "Info":
                            await Swal.fire({
                                icon: 'info',
                                title: `${fundingRequestsResponses[i].BankName} dice:`,
                                html: fundingRequestsResponses[i].Message,
                                allowOutsideClick: false,
                                allowEscapeKey: false
                            });

                            break;
                        default:
                            await Swal.fire({
                                icon: 'error',
                                title: 'Respuesta inesperada',
                                text: 'Lo sentimos, intenta más tarde.',
                                footer: '<b><a href="/Info/ContactUs">Por favor infórmanos</a></b>',
                                allowOutsideClick: false,
                                allowEscapeKey: false
                            });

                            break;
                    }
                }
            }
            ********************************************************/
            fundingResult = await tryGetSantanderFundingRequest();
            if (!fundingResult.Result) {
                return false;
            }

            const formatter = new Intl.NumberFormat('en-US', {
                style: 'currency',
                currency: 'USD',
            });

            const tableHtml = `<p class='h5'><b>${fundingResult.BankName} dice</b>: ${fundingResult.Message}</p>
                               <table style="width: 283px; height: 89px;" border="0">
                                    <tbody>
                                    <tr style="height: 21px;">
                                    <td style="height: 21px; width: 127px;">Monto Solicitado:</td>
                                    <td style="height: 21px; width: 155px;">${formatter.format(fundingResult.Response.MontoSol)}</td>
                                    </tr>
                                    <tr style="height: 21px;">
                                    <td style="height: 21px; width: 127px;">Plazo:</td>
                                    <td style="height: 21px; width: 155px;">${fundingResult.Response.Request.Plazo}</td>
                                    </tr>
                                    <tr style="height: 21px;">
                                    <td style="height: 21px; width: 127px;">Tasa:</td>
                                    <td style="height: 21px; width: 155px;">${fundingResult.Response.Request.Tasa}</td>
                                    </tr>
                                    <tr style="height: 21px;">
                                    <td style="height: 21px; width: 127px;">Cuota Proyectada:</td>
                                    <td style="height: 21px; width: 155px;">${formatter.format(fundingResult.Response.CuotaProyectada)}</td>
                                    </tr>
                                    </tbody>
                               </table>`;

            //Continue again?
            const letsContinue = await Swal.fire({
                title: 'Solicitud de crédito exitosa',
                icon: 'success',
                html: tableHtml,
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Continuar con la compra',
                cancelButtonText: 'No, cancelar'
            });

            if (!letsContinue.isConfirmed) {
                return false;
            };
        }

        ///(0 || 2 --> Credit card) Check token for credit card
        if ((paymentMethodSelected == "0" || paymentMethodSelected == "2")) {
            //Wait while Credit card token is generate
            await wait(time);
            if (!verifyToken()) {
                return false;
            }
            //Wait for token message
            await wait(990);
        }

        const url = "/ShoppingCart/Index/"
        let bodyData = getDataForCheckout();

        //(1 - Funding Request) --> Santander Bank request - Set Semaforo field
        if (paymentMethodSelected == "1") {
            bodyData.FundingRequestViewModel.Semaforo = fundingResult.Response.Semaforo;
            bodyData.FundingRequestViewModel.Bank = fundingResult.Bank;
            bodyData.FundingRequestViewModel.RequestResultMessage = fundingResult.Message;
            fundingRequestMessage = fundingResult.Message;
        }

        Swal.fire({
            title: 'Generando compra...',
            html: '¡Me cerraré en pocos segundos!',
            allowOutsideClick: false,
            allowEscapeKey: false,
            onBeforeOpen: () => {
                Swal.showLoading()
            }
        });

        ///Checkout Call
        const serverResponse = await fetch(url,
            {
                method: "POST",
                body: JSON.stringify(bodyData),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json"
                }
            });

        if (serverResponse.ok) {
            const response = await serverResponse.json();
            let control = { Control: false, OrderId: response.OrderId };

            //Handler Response
            if (response.ControlStatus == "ModelError") {
                grecaptcha.reset();
                let errorLog = "";
                response.Errors.forEach((element) => {
                    errorLog += element.ErrorMessage + " ";
                });

                await Swal.fire({
                    icon: 'warning',
                    title: 'Verifica los siguientes campos:',
                    text: errorLog,
                    footer: '<b>Te esperamos.</b>',
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });
            }

            if (response.ControlStatus == "Exception") {
                let errorLog = "";
                response.Errors.forEach((element) => {
                    errorLog += element.ErrorMessage + " ";
                });

                await Swal.fire({
                    icon: 'error',
                    title: '¡Ups! Lo sentimos',
                    text: errorLog,
                    footer: '<b><a href="/Info/ContactUs">Por favor infórmanos</a></b>',
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });
            }

            if (response.ControlStatus == "CreditCardOk") {
                let message = "";
                let icon = "";
                let title = "Estado de la compra";
                let footer = "Procesando, por favor espera..."
                switch (response.Result.Status.toString()) {
                    case "pending":
                        message = "Pendiente";
                        footer = `En unos minutos te enviaremos la notifiación del estado de pago.`;
                        icon = "warning";
                        break;
                    case "approved":
                        title = "¡Compra exitosa!"
                        message = "¡Gracias por ser parte de la familia Motovalle!";
                        icon = "success";
                        footer = `Listo, ¡se acreditó tu pago!. En tu resumen verás el cargo del monto como ${response.Result.StatementDescriptor}.`;
                        control = { Control: true, OrderId: response.OrderId };
                        break;
                    case "authorized":
                        message = "Autorizado";
                        icon = "success";
                        control = { Control: true, OrderId: response.OrderId };
                        break;
                    case "in_process":
                        title = "Transacción en proceso"
                        icon = "info";
                        switch (response.Result.StatusDetail) {
                            case "pending_contingency":
                                message = "Estamos procesando el pago. En menos de 2 días hábiles te enviaremos por e-mail el resultado.";
                                break;
                            case "pending_review_manual":
                                message = "Estamos procesando el pago. En menos de 2 días hábiles te diremos por e - mail si se acreditó o si necesitamos más información.";
                                break;
                            default:
                                message = "Estamos procesando el pago."
                                break;
                        }

                        control = { Control: true, OrderId: response.OrderId };
                        break;
                    case "in_mediation":
                        message = "En mediación";
                        icon = "info";
                        break;
                    case "rejected":
                        title = "Transacción rechazada";
                        icon = "error";
                        footer = "Por favor verifica.";
                        switch (response.Result.StatusDetail) {
                            case "cc_rejected_bad_filled_card_number":
                                message = "Revisa el número de tu tarjeta.";
                                break;
                            case "cc_rejected_bad_filled_date":
                                message = "Revisa la fecha de vencimiento.";
                                break;
                            case "cc_rejected_bad_filled_other":
                                message = "Revisa los datos ingresados.";
                                break;
                            case "cc_rejected_bad_filled_security_code":
                                message = "Revisa el código de seguridad.";
                                break;
                            case "cc_rejected_blacklist":
                                message = "Tarjeta en lista negra. No pudimos procesar tu pago.";
                                break;
                            case "cc_rejected_call_for_authorize":
                                message = `Debes autorizar ante ${response.Result.PaymentMethodId} el pago de la transacción a Mercado Pago`;
                                break;
                            case "cc_rejected_card_disabled":
                                message = `Llama a ${response.Result.PaymentMethodId} para que active tu tarjeta. El teléfono está al dorso de tu tarjeta.`;
                                break;
                            case "cc_rejected_card_error":
                                message = "No pudimos procesar tu pago. Intenta nuevamente.";
                                break;
                            case "cc_rejected_duplicated_payment":
                                message = "Ya hiciste un pago por ese valor. Si necesitas volver a pagar usa otra tarjeta u otro medio de pago.";
                                break;
                            case "cc_rejected_high_risk":
                                message = "Tu pago fue rechazado por alto riesgo en la transacción. Elige otro medio de pago, te recomendamos con medios en efectivo.";
                                break;
                            case "cc_rejected_insufficient_amount":
                                message = `Tu ${response.Result.PaymentMethodId} no tiene fondos suficientes.`
                                break;
                            case "cc_rejected_invalid_installments":
                                message = `${response.Result.PaymentMethodId} no procesa pagos en installments cuotas.`;
                                break;
                            case "cc_rejected_max_attempts":
                                message = "Llegaste al límite de intentos permitidos. Elige otra tarjeta u otro medio de pago.";
                                break;
                            case "cc_rejected_other_reason":
                                message = `${response.Result.PaymentMethodId} no procesó el pago`;
                                break;
                            default:
                                message = "No pudimos procesar tu pago. Intenta nuevamente.";
                                break;
                        }

                        break;
                    case "cancelled":
                        if (response.Result.MercadoPagoCustomerResponse.Code != "200") {
                            message = response.Result.StatusDetail;
                            icon = "error";
                            footer = "Por favor verifica.";
                            break;
                        }

                        message = response.Result.StatusDetail;
                        icon = "error";
                        break;
                    case "refunded":
                        message = "Reintegrado";
                        icon = "warning";
                        break;
                    case "charged_back":
                        message = "Cargado nuevamente";
                        icon = "info";
                        break;
                    default:
                        message = "Error";
                        icon = "error";
                        break;
                }

                await Swal.fire({
                    icon: icon,
                    title: title,
                    text: message,
                    timerProgressBar: true,
                    showConfirmButton: true,
                    footer: footer,
                    showClass: {
                        popup: 'animated rubberBand'
                    },
                    hideClass: {
                        popup: 'animated fadeOutUp faster'
                    },
                    position: 'top-center',
                    timer: 10000,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });
            }

            if (response.ControlStatus == "FundingRequestOk") {
                const message = `Recuerda: ${fundingRequestMessage}`;
                const icon = "info";
                const title = "Estado de la compra";
                control = { Control: true, OrderId: response.OrderId };
                await Swal.fire({
                    icon: icon,
                    title: title,
                    text: message,
                    timerProgressBar: true,
                    showConfirmButton: false,
                    showClass: {
                        popup: 'animated rubberBand'
                    },
                    hideClass: {
                        popup: 'animated fadeOutUp faster'
                    },
                    position: 'top-center',
                    footer: "Te informaremos cuando se realice alguna actualización. Continuamos procesando, espera...",
                    timer: 7000,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });
            };

            document.querySelector("#tokenInputId").value = "";
            if (!control.Control) {
                return false;
            }

            ////Success Message
            await Swal.fire({
                icon: "success",
                title: "¡Gracias!",
                text: "Estámos finalizando tu compra.",
                footer: "Esperanos unos segundos",
                timerProgressBar: true,
                showClass: {
                    popup: 'animated rubberBand'
                },
                hideClass: {
                    popup: 'animated fadeOutUp faster'
                },
                position: 'top-center',
                timer: 5000,
                showConfirmButton: false,
                allowOutsideClick: false,
                allowEscapeKey: false
            });

            ////Start Order process
            Swal.fire({
                title: 'Generando orden de compra...',
                html: '¡Me cerraré en pocos segundos!',
                onBeforeOpen: () => {
                    Swal.showLoading()
                },
                allowOutsideClick: false,
                allowEscapeKey: false
            });

            ///Get Manual Invoice call
            const url2 = `/Orders/GetManualInvoicePDF/${control.OrderId}/Emails/${true}`;
            const server = await fetch(url2,
                {
                    method: "GET",
                    headers: {
                        "Accept": "application/json",
                        "Content-Type": "application/json"
                    }
                });

            if (server.ok) {
                await Swal.fire({
                    title: 'Orden creada exitosamente',
                    text: "Se ha enviado la orden a tu Email",
                    footer: "Redirigiendo al detalle de la orden. Dános unos segundos.",
                    showClass: {
                        popup: 'animated fadeInUp faster'
                    },
                    hideClass: {
                        popup: 'animated fadeOutRight faster'
                    },
                    position: 'top-end',
                    icon: 'success',
                    showConfirmButton: false,
                    timerProgressBar: true,
                    timer: 5000,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                }).then(() => {
                    location.href = `/Orders/Details/${control.OrderId}`;
                });
            } else {
                await Swal.fire({
                    icon: "error",
                    title: "Lo sentimos",
                    text: "Se ha producido error envíando la orden a tu Email. En instantes lo intentaremos nuevamente.",
                    footer: '<b><a href="/Info/ContactUs">Por favor infórmanos</a></b>',
                    showClass: {
                        popup: 'animated rubberBand'
                    },
                    hideClass: {
                        popup: 'animated fadeOutUp faster'
                    },
                    position: 'top-center',
                    timer: 5000,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

                location.href = `/Orders/Details/${control.OrderId}`;
            }
        } else {
            await Swal.fire({
                icon: 'error',
                title: '¡Ups!. Ha ocurrido un error',
                text: 'Lo sentimos',
                footer: '<b><a href="/Info/ContactUs">Por favor infórmanos</a></b>',
                allowOutsideClick: false,
                allowEscapeKey: false
            });
        };
    } else {
        return;
    }
}

//Go to create order
function getDataForCheckout() {
    //Personal Data
    let customersId = document.querySelector("#customerIdForm") ? document.querySelector("#customerIdForm").value : 0;
    let email = document.querySelector("#emaiMainlId").value;
    let firstName = document.querySelector("#firstNameIdForm").value;
    let lastName = document.querySelector("#lastNameIdForm").value;
    let idNumber = document.querySelector("#docNumber1").value;
    let idtype = document.querySelector("#docType").value;
    let phoneNumber = document.querySelector("#phoneNumberIdForm").value;
    let alternateNumber = document.querySelector("#alternateNumberIdForm").value;
    let companyName = document.querySelector("#companyNameIdForm").value;

    //Addresses
    let country = document.querySelector("#countryIdForm").value;

    let state = document.querySelector("#stateIdForm").value;
    let city = document.querySelector("#cityIdForm").value;
    let address = document.querySelector("#addressIdForm").value;
    let zipCode = document.querySelector("#zipcodeIdform").value;

    let billState = document.querySelector("#billStateIdForm").value;
    let billCity = document.querySelector("#billCityIdForm").value;
    let billAddress = document.querySelector("#billAddressIdForm").value;
    let billZipCode = document.querySelector("#billZipcodeIdForm").value;

    //Payment selected
    let paymentMethodSelected = document.querySelector("#paymentSelectedIdForm").value;

    //CreditCardViewModel
    let cardToken = document.querySelector("#tokenInputId").value;
    let description = document.querySelector("#descriptionDivId").value == "" ? "Compra Tienda Motovalle" : document.querySelector("#descriptionDivId").value;
    let paymentMethodId = document.querySelector("#paymentMethodDivId").value;
    let installments = document.querySelector("#installments").value == "" ? 0 : document.querySelector("#installments").value;
    let issuerId = document.querySelector("#issuerIdDiv").value;
    let partialAmount = document.querySelector("#partialAmountId").value == "" ? 0 : document.querySelector("#partialAmountId").value;
    partialAmount += '';
    partialAmount = parseFloat(partialAmount.replace(/[^0-9\.]/g, ''));

    //Funding request
    let initialFee = document.querySelector("#fundingInitialFeeDivId").value == "" ? 0 : document.querySelector("#fundingInitialFeeDivId").value;
    initialFee += '';
    initialFee = parseFloat(initialFee.replace(/[^0-9\.]/g, ''));
    let fundingInstallments = document.querySelector("#fundingInstallmentsDivId").value;
    let profession = document.querySelector("#fundingProfessionDivId").options[document.querySelector("#fundingProfessionDivId").selectedIndex].text;
    let dateOfBirth = document.querySelector("#fundingDateOfBirthDivId").value == "" ? getDateNow() : document.querySelector("#fundingDateOfBirthDivId").value;
    let cityOfResidence = document.querySelector("#fundingCityOfResidence").value;
    let electronicAgreement = true; //document.querySelector("#fundinfElectronicSignatureAgreementDivId").checked;
    let riskCenterAgreement = true; //document.querySelector("#fundingRiskCenterAgreementDivId").checked;

    //Santander Bank Additional fields
    let economicActivity = document.querySelector("#fundingEconomicActivityDivId").value;
    let indepentActivity = document.querySelector("#fundingIndepentActivityDivId").value;
    let monthlyIncome = document.querySelector("#fundingMonthlyEarnDivId").value == "" ? 0 : document.querySelector("#fundingMonthlyEarnDivId").value;
    monthlyIncome += '';
    monthlyIncome = parseFloat(monthlyIncome.replace(/[^0-9\.]/g, ''));

    //Captcha
    const gCaptcha = document.querySelector("#g-recaptcha-response").value;

    let data = {
        CustomersId: customersId,
        Email: email,
        FristName: firstName,
        LastName: lastName,
        IDNumber: idNumber,
        IDType: idtype,
        PhoneNumber: phoneNumber,
        AlternateNumber: alternateNumber,
        CompanyName: companyName,
        Country: country,
        Address: address,
        State: state,
        City: city,
        ZipCode: zipCode,
        BillAddress: billAddress,
        BillCity: billCity,
        BillState: billState,
        BillZipCode: billZipCode,
        TotalTaxes: null,
        SubTotal: 0,
        ShippingCost: 0,
        Discount: 0,
        Total: 0,
        PartialAmountPaid: partialAmount,
        PaymentMethodSelected: paymentMethodSelected,
        ShoppingCartRecords: null,
        GRecaptchaResponse: gCaptcha,
        CreditCardViewModel: {
            TransactionAmount: 0,
            CardToken: cardToken,
            Description: description,
            PaymentMethodId: paymentMethodId,
            Installments: installments,
            IDNumber: idNumber,
            Email: email,
            IssuerId: issuerId,
            IDNumberType: idtype,
            FirstName: firstName,
            LastName: lastName,
            Address: address,
            ZipCode: zipCode,
            City: city,
            PhoneNumber: phoneNumber,
            ShipAddress: address,
            ShipZipCode: zipCode
        },
        FundingRequestViewModel: {
            CustomersId: customersId,
            TotalAmountRequest: 0,
            InitialFee: initialFee,
            FundingInstallments: fundingInstallments,
            Name: firstName,
            LastName: lastName,
            Profession: profession,
            IDNumber: idNumber,
            IDType: idtype,
            DateOfBirth: dateOfBirth,
            CityOfResidence: cityOfResidence,
            ElectronicSignatureAgreement: electronicAgreement,
            RiskCenterAgreement: riskCenterAgreement,
            SantanderBankEconomicActivity: economicActivity,
            SantanderBankIndependentActivity: indepentActivity,
            MonthlyIncome: monthlyIncome,
            Semaforo: 99,
            RequestResultMessage: null
        }
    }

    return data;
}

////Gets datetime.now
function getDateNow() {
    let today = new Date();
    let dd = today.getDate();
    let mm = today.getMonth() + 1;
    let yyyy = today.getFullYear();

    dd = addZero(dd);
    mm = addZero(mm);

    return `${yyyy}-${mm}-${dd}`;
};

//Adds zeros for number
function addZero(i) {
    if (i < 10) {
        i = '0' + i;
    }
    return i;
}

//Wait function returns a promise
function wait(ms) {
    return new Promise(resolve => {
        setTimeout(resolve, ms);
    });
}

//number to currency format (take from https://gist.github.com/jrobinsonc/5718959)
function number_format(amount, decimals) {
    amount += ''; // por si pasan un numero en vez de un string
    amount = parseFloat(amount.replace(/[^0-9\.]/g, '')); // elimino cualquier cosa que no sea numero o punto

    decimals = decimals || 0; // por si la variable no fue fue pasada

    // si no es un numero o es igual a cero retorno el mismo cero
    if (isNaN(amount) || amount === 0)
        return parseFloat(0).toFixed(decimals);

    // si es mayor o menor que cero retorno el valor formateado como numero
    amount = '' + amount.toFixed(decimals);

    var amount_parts = amount.split('.'),
        regexp = /(\d+)(\d{3})/;

    while (regexp.test(amount_parts[0]))
        amount_parts[0] = amount_parts[0].replace(regexp, '$1' + ',' + '$2');

    return '$' + amount_parts.join('.');
}

//Put currency format to object
function getCurrencyFormatForNumber(obj, event, objSource) {

    if (event.charCode >= 48 && event.charCode <= 57) {
        return false;
    }

    obj.value = number_format(obj.value, 0);
    document.getElementById(objSource).value = parseFloat(obj.value.replace(/[^0-9\.]/g, ''));
}

//Get Validation for partial Amount
function getValidationPartial(value) {
    let partialAmount = parseFloat(value.replace(/[^0-9\.]/g, ''));
    if (partialAmount < 1000000) {
        document.getElementById("partialAmountValidationId").innerHTML = "Monto mínimo debe ser 1'000.000.";
        //document.getElementById("partialAmountId").focus();
        return false;
    }

    if (partialAmount > document.getElementById("amountMainId").value) {
        document.getElementById("partialAmountValidationId").innerHTML = "Monto mínimo no debe superar el valor total de la orden.";
        document.getElementById("partialAmountId").focus();
        return false;
    }

    document.getElementById("partialAmountValidationId").innerHTML = "";
    return true;
}

//Massey Ferfuson Info
function getMasseyInfo(from) {
    Swal.fire({
        title: 'Cargando...',
        html: '¡Me cerraré en pocos segundos!',
        allowOutsideClick: false,
        allowEscapeKey: false,
        onBeforeOpen: () => {
            Swal.showLoading()
        }
    });

    switch (from) {
        case 'Home':
            document.querySelector("#masseyHomeId").style.display = "block";
            document.querySelector("#masseyAboutId").style.display = "none";
            document.querySelector("#masseyPhilosophy").style.display = "none";
            document.querySelector("#masseyBrandValuesId").style.display = "none";
            document.querySelector("#masseyOperatorsClubId").style.display = "none";
            break;
        case 'About':
            document.querySelector("#masseyHomeId").style.display = "none";
            document.querySelector("#masseyAboutId").style.display = "block";
            document.querySelector("#masseyPhilosophy").style.display = "none";
            document.querySelector("#masseyBrandValuesId").style.display = "none";
            document.querySelector("#masseyOperatorsClubId").style.display = "none";
            break;
        case 'Philosophy':
            document.querySelector("#masseyHomeId").style.display = "none";
            document.querySelector("#masseyAboutId").style.display = "none";
            document.querySelector("#masseyPhilosophy").style.display = "block";
            document.querySelector("#masseyBrandValuesId").style.display = "none";
            document.querySelector("#masseyOperatorsClubId").style.display = "none";
            break;
        case 'BrandValues':
            document.querySelector("#masseyHomeId").style.display = "none";
            document.querySelector("#masseyAboutId").style.display = "none";
            document.querySelector("#masseyPhilosophy").style.display = "none";
            document.querySelector("#masseyBrandValuesId").style.display = "block";
            document.querySelector("#masseyOperatorsClubId").style.display = "none";
            break;
        case 'OperatorsClub':
            document.querySelector("#masseyHomeId").style.display = "none";
            document.querySelector("#masseyAboutId").style.display = "none";
            document.querySelector("#masseyPhilosophy").style.display = "none";
            document.querySelector("#masseyBrandValuesId").style.display = "none";
            document.querySelector("#masseyOperatorsClubId").style.display = "block";
            break;
        default:
            document.querySelector("#masseyHomeId").style.display = "block";
            document.querySelector("#masseyAboutId").style.display = "none";
            document.querySelector("#masseyPhilosophy").style.display = "none";
            document.querySelector("#masseyBrandValuesId").style.display = "none";
            document.querySelector("#masseyOperatorsClubId").style.display = "none";
            break;
    }

    setTimeout(() => Swal.close(), 1000);
}

//Ford Info
function getFordInfo(from) {
    Swal.fire({
        title: 'Cargando...',
        html: '¡Me cerraré en pocos segundos!',
        allowOutsideClick: false,
        allowEscapeKey: false,
        onBeforeOpen: () => {
            Swal.showLoading()
        }
    });

    switch (from) {
        case 'Home':
            document.querySelector("#fordHomeId").style.display = "block";
            document.querySelector("#fordIntegralServiceId").style.display = "none";
            document.querySelector("#fordTestDriveId").style.display = "none";
            break;
        case 'IntegralService':
            document.querySelector("#fordHomeId").style.display = "none";
            document.querySelector("#fordIntegralServiceId").style.display = "block";
            document.querySelector("#fordTestDriveId").style.display = "none";
            break;
        case 'TestDrive':
            document.querySelector("#fordHomeId").style.display = "none";
            document.querySelector("#fordIntegralServiceId").style.display = "none";
            document.querySelector("#fordTestDriveId").style.display = "block";
            break;
        default:
            document.querySelector("#fordHomeId").style.display = "block";
            document.querySelector("#fordIntegralServiceId").style.display = "none";
            document.querySelector("#fordTestDriveId").style.display = "none";
            break;
    }

    setTimeout(() => Swal.close(), 1000);
}

//Mazda Info
function getMazdaInfo(from) {
    Swal.fire({
        title: 'Cargando...',
        html: '¡Me cerraré en pocos segundos!',
        allowOutsideClick: false,
        allowEscapeKey: false,
        onBeforeOpen: () => {
            Swal.showLoading()
        }
    });

    switch (from) {
        case 'Home':
            document.querySelector("#mazdaHomeId").style.display = "block";
            document.querySelector("#mazdaServiceId").style.display = "none";
            document.querySelector("#mazdaDesignId").style.display = "none";
            document.querySelector("#mazdaCampaignId").style.display = "none";
            document.querySelector("#mazdaTechId").style.display = "none";
            document.querySelector("#mazdaSpiritId").style.display = "none";
            break;
        case 'Service':
            document.querySelector("#mazdaHomeId").style.display = "none";
            document.querySelector("#mazdaServiceId").style.display = "block";
            document.querySelector("#mazdaCampaignId").style.display = "none";
            document.querySelector("#mazdaDesignId").style.display = "none";
            document.querySelector("#mazdaTechId").style.display = "none";
            document.querySelector("#mazdaSpiritId").style.display = "none";
            break;
        case 'Campaign':
            document.querySelector("#mazdaHomeId").style.display = "none";
            document.querySelector("#mazdaServiceId").style.display = "none";
            document.querySelector("#mazdaCampaignId").style.display = "block";
            document.querySelector("#mazdaDesignId").style.display = "none";
            document.querySelector("#mazdaTechId").style.display = "none";
            document.querySelector("#mazdaSpiritId").style.display = "none";
            break;
        case 'Design':
            document.querySelector("#mazdaHomeId").style.display = "none";
            document.querySelector("#mazdaServiceId").style.display = "none";
            document.querySelector("#mazdaCampaignId").style.display = "none";
            document.querySelector("#mazdaDesignId").style.display = "block";
            document.querySelector("#mazdaTechId").style.display = "none";
            document.querySelector("#mazdaSpiritId").style.display = "none";
            break;
        case 'Tech':
            document.querySelector("#mazdaHomeId").style.display = "none";
            document.querySelector("#mazdaServiceId").style.display = "none";
            document.querySelector("#mazdaCampaignId").style.display = "none";
            document.querySelector("#mazdaDesignId").style.display = "none";
            document.querySelector("#mazdaTechId").style.display = "block";
            document.querySelector("#mazdaSpiritId").style.display = "none";
            break;
        case 'Spirit':
            document.querySelector("#mazdaHomeId").style.display = "none";
            document.querySelector("#mazdaServiceId").style.display = "none";
            document.querySelector("#mazdaCampaignId").style.display = "none";
            document.querySelector("#mazdaDesignId").style.display = "none";
            document.querySelector("#mazdaTechId").style.display = "none";
            document.querySelector("#mazdaSpiritId").style.display = "block";
            break;
        default:
            document.querySelector("#mazdaHomeId").style.display = "block";
            document.querySelector("#mazdaServiceId").style.display = "none";
            document.querySelector("#mazdaDesignId").style.display = "none";
            document.querySelector("#mazdaCampaignId").style.display = "none";
            document.querySelector("#mazdaTechId").style.display = "none";
            document.querySelector("#mazdaSpiritId").style.display = "none";
            break;
    }

    setTimeout(() => Swal.close(), 1000);
}

//Mazda sub content
function getMazdaSubContent(from) {

    Swal.fire({
        title: 'Cargando...',
        html: '¡Me cerraré en pocos segundos!',
        allowOutsideClick: false,
        allowEscapeKey: false,
        onBeforeOpen: () => {
            Swal.showLoading()
        }
    });

    switch (from) {
        case 'FirstYears':
            document.querySelector("#mazdaFirstYears").style.display = "block";
            document.querySelector("#mazdaRotativeEngine").style.display = "none";
            document.querySelector("#mazdaMX5").style.display = "none";
            document.querySelector("#mazdaVictory").style.display = "none";
            document.querySelector("#mazdaZoom").style.display = "none";
            document.querySelector("#mazdaAniversary").style.display = "none";
            document.querySelector("#mazdaFuture").style.display = "none";
            break;
        case 'RotativeEngine':
            document.querySelector("#mazdaFirstYears").style.display = "none";
            document.querySelector("#mazdaRotativeEngine").style.display = "block";
            document.querySelector("#mazdaMX5").style.display = "none";
            document.querySelector("#mazdaVictory").style.display = "none";
            document.querySelector("#mazdaZoom").style.display = "none";
            document.querySelector("#mazdaAniversary").style.display = "none";
            document.querySelector("#mazdaFuture").style.display = "none";
            break;
        case 'MX-5':
            document.querySelector("#mazdaFirstYears").style.display = "none";
            document.querySelector("#mazdaRotativeEngine").style.display = "none";
            document.querySelector("#mazdaMX5").style.display = "block";
            document.querySelector("#mazdaVictory").style.display = "none";
            document.querySelector("#mazdaZoom").style.display = "none";
            document.querySelector("#mazdaAniversary").style.display = "none";
            document.querySelector("#mazdaFuture").style.display = "none";
            break;
        case 'LeMeansVictory':
            document.querySelector("#mazdaFirstYears").style.display = "none";
            document.querySelector("#mazdaRotativeEngine").style.display = "none";
            document.querySelector("#mazdaMX5").style.display = "none";
            document.querySelector("#mazdaVictory").style.display = "block";
            document.querySelector("#mazdaZoom").style.display = "none";
            document.querySelector("#mazdaAniversary").style.display = "none";
            document.querySelector("#mazdaFuture").style.display = "none";
            break;
        case 'Zoom':
            document.querySelector("#mazdaFirstYears").style.display = "none";
            document.querySelector("#mazdaRotativeEngine").style.display = "none";
            document.querySelector("#mazdaMX5").style.display = "none";
            document.querySelector("#mazdaVictory").style.display = "none";
            document.querySelector("#mazdaZoom").style.display = "block";
            document.querySelector("#mazdaAniversary").style.display = "none";
            document.querySelector("#mazdaFuture").style.display = "none";
            break;
        case 'Aniversary':
            document.querySelector("#mazdaFirstYears").style.display = "none";
            document.querySelector("#mazdaRotativeEngine").style.display = "none";
            document.querySelector("#mazdaCampaignId").style.display = "none";
            document.querySelector("#mazdaMX5").style.display = "none";
            document.querySelector("#mazdaVictory").style.display = "none";
            document.querySelector("#mazdaZoom").style.display = "none";
            document.querySelector("#mazdaAniversary").style.display = "block";
            document.querySelector("#mazdaFuture").style.display = "none";
            break;
        case 'Future':
            document.querySelector("#mazdaFirstYears").style.display = "none";
            document.querySelector("#mazdaRotativeEngine").style.display = "none";
            document.querySelector("#mazdaMX5").style.display = "none";
            document.querySelector("#mazdaVictory").style.display = "none";
            document.querySelector("#mazdaZoom").style.display = "none";
            document.querySelector("#mazdaAniversary").style.display = "none";
            document.querySelector("#mazdaFuture").style.display = "block";
            break;
        case 'None':
            document.querySelector("#mazdaFirstYears").style.display = "none";
            document.querySelector("#mazdaRotativeEngine").style.display = "none";
            document.querySelector("#mazdaMX5").style.display = "none";
            document.querySelector("#mazdaVictory").style.display = "none";
            document.querySelector("#mazdaZoom").style.display = "none";
            document.querySelector("#mazdaAniversary").style.display = "none";
            document.querySelector("#mazdaFuture").style.display = "none";
            break;
        default:
            document.querySelector("#mazdaFirstYears").style.display = "block";
            document.querySelector("#mazdaRotativeEngine").style.display = "none";
            document.querySelector("#mazdaMX5").style.display = "none";
            document.querySelector("#mazdaVictory").style.display = "none";
            document.querySelector("#mazdaZoom").style.display = "none";
            document.querySelector("#mazdaAniversary").style.display = "none";
            document.querySelector("#mazdaFuture").style.display = "none";
            break;
    }

    setTimeout(() => Swal.close(), 500);
}

//General JS calls
setTimeout(() => $("#infoCheckoutAlert").alert('close'), 15000);

////To Toggle icon for collapse items
$('.repuestos-button').click(function (event) {
    if ($(event.currentTarget.children).hasClass('fa-angle-down')) {
        $(event.currentTarget.lastElementChild).removeClass("fa-angle-down");
        $(event.currentTarget.lastElementChild).addClass("fa-angle-left");
    }
    else {
        $(event.currentTarget.lastElementChild).removeClass("fa-angle-left");
        $(event.currentTarget.lastElementChild).addClass("fa-angle-down");
    }
});

////Show Floating Cart 
function showFloatingCart(productsId, inventoryItemsId, makes) {
    setTimeout(() => {
        let confirmButtonColor = "";
        switch (makes) {
            case 'Ford':
                confirmButtonColor = "#228dcb";
                break;
            case 'Mazda':
                confirmButtonColor = "#de483d";
                break;
            case 'Carshow':
                confirmButtonColor = "#f76d2b";
                break;
            case 'Massey':
                confirmButtonColor = "#C21B2F";
                break
            default:
                confirmButtonColor = "#3085d6";
        }

        const Toast = Swal.mixin({
            toast: true,
            position: 'center-end',
            showConfirmButton: true,
            padding: "1em",
            confirmButtonText: '<i class="fa fa-plus fa-3x"></i> <h4>¡Sí!</h4>',
            confirmButtonColor: confirmButtonColor,
            showCancelButton: true,
            cancelButtonText: '<i class="fa fa-times"></i>'
        });

        Toast.fire({
            html: '<i class="fa fa-shopping-cart fa-3x"></i> <h4 style="margin: 0 0.5%">Agregar al carrito</h4>'
        }).then((result) => {
            if (result.isConfirmed) {
                open = false;
                addToCart(productsId, inventoryItemsId, 1);
            }
        });
    }, 3000)
}

////Set Same Address to Billing
function CopyAddress() {
    let BillToAddress = document.getElementById('addressIdForm');
    let BillToCity = document.getElementById('cityIdForm');
    let BillToState = document.getElementById('stateIdForm');
    let BillToZipcode = document.getElementById('zipcodeIdform');

    let ShipToAddress = document.getElementById('billAddressIdForm');
    let ShipToCity = document.getElementById('billCityIdForm');
    let ShipToState = document.getElementById('billStateIdForm');
    let ShipToZipcode = document.getElementById('billZipcodeIdForm');

    let copyCheckbox = document.getElementById('chkSameAsBilling');

    if (copyCheckbox.checked) {
        ShipToAddress.value = BillToAddress.value;
        ShipToCity.value = BillToCity.value;
        ShipToState.value = BillToState.value;
        ShipToZipcode.value = BillToZipcode.value;
    }
    else {
        ShipToAddress.value = '';
        ShipToCity.value = '';
        ShipToState.value = '';
        ShipToZipcode.value = '';
    }
}

////Get Cookie
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}