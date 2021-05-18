/*
    Date: 2020/03/09
    Company: Innova Marketing Systems S.A.S
    Programmer: Diego Arenas Tangarife
    Comments: Put here all script functions
 */


//Save blog post using async request like fetch
function saveBlogPost() {
    Swal.fire({
        title: 'Almacenando post...',
        html: '¡Me cerraré en pocos segundos!',
        onBeforeOpen: () => {
            Swal.showLoading()
        },
        allowOutsideClick: false,
        allowEscapeKey: false
    });

    let imageURL;
    let uploadObject = document.getElementById('uploadFiles').ej2_instances[0];
    let obj = document.getElementById('defaultRTE').ej2_instances[0];
    let title = document.getElementById('txtTitle').value;
    let postID = document.getElementById('txtPostID').value;
    let shortDescription = document.getElementById('txtShortDescription').value;
    let fullText = obj.getHtml();   //get component HTML
    let AuthorsID = document.getElementById('txtFKAuthorsID').value;
    let CategoeiesID = document.getElementById('txtFKCategoriesID').value;
    let published = document.getElementById('IsPublished').checked;

    if (uploadObject.filesData.length > 0) {
        imageURL = `/img/${uploadObject.filesData[0].name}`;
    } else {
        imageURL = document.getElementById("txtImageURL").value;
    }

    //Construct JSON Object
    let postObject = JSON.stringify({ PostsId: postID, Title: title, Description: fullText, ShortDescription: shortDescription, FkCategoriesId: CategoeiesID, FkAuthorsId: AuthorsID, ImageURL: imageURL, IsPublished: published });

    //Send to Controller
    const url = '/api/APIPosts';
    const urlReturn = '/Admin/BlogAdmin';
    fetch(url, {
        method: "POST",
        body: postObject,
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    }).then((Data) => {
        if (Data.ok == false) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: Data,
                allowOutsideClick: false,
                allowEscapeKey: false
            });
        } else {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Your work has been saved',
                showConfirmButton: false,
                timer: 2000,
                allowOutsideClick: false,
                allowEscapeKey: false
            }).then(() => document.location.href = urlReturn);
        }
    });
};

//Deletes Blog Post for Id
function deletePost(idPost) {
    
    let url = `/api/APIPosts/Delete/${idPost}`;
    Swal.fire({
        title: '¿Estás seguro de eliminar este Post?',
        text: "No podras revertir esto",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '¡Sí!'
    }).then((result) => {
        if (result.value) {
            fetch(url, {
                method: "DELETE",
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json"
                }
            }).then((serverResponse) => {
                if (serverResponse.ok) {
                    Swal.fire({
                        icon: 'success',
                        title: 'Eliminado',
                        text: 'Post ha sido eliminado correctamente'
                    }).then(() => {
                        document.location = "/Admin/BlogAdmin";
                    })
                }
            });
        }
    })
};


//Unlock user for id user
function unlockoutUser(idUser) {
    var url = `/Admin/UnlockoutUser/${idUser}`;
    var urlReturn = `/Admin/UserManager/`;
    Swal.fire({
        title: '¿Estás seguro?',
        text: "No podras revertir esto",
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '¡Sí!',
        allowOutsideClick: false,
        allowEscapeKey: false
    }).then((result) => {
        if (result.value) {
            Swal.fire({
                title: 'Procesando',
                html: 'Me cerraré en unos segundos',
                onBeforeOpen: () => {
                    Swal.showLoading()
                },
                allowOutsideClick: false,
                allowEscapeKey: false
            });

            fetch(url, {
                method: "GET",
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json"
                }
            }).then((response) => { //Server status response
                if (response.ok) {
                    return response.json();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: '¡Ups!...',
                        text: 'Servidor no responde',
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    });
                };
            }).then((responseResult) => {
                if (responseResult.Status == "Exception") {
                    Swal.fire({
                        icon: 'error',
                        title: '¡Ups!... Algo salió mal =(',
                        text: responseResult.Message,
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    });
                } else {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Tu trabajo ha sido guardado exitosamente.',
                        showConfirmButton: false,
                        timer: 1200,
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    }).then(() => document.location.href = urlReturn);
                }
            });
        }
    });
};


//Sets user in role
function setUserRole(idUser, idRoleOld, idRole) {
    var url = "/Admin/SetUserRole/";
    var urlReturn = `/Admin/RoleManager/${idUser}`;
    var data = { IdUser: idUser, UserName: "", RoleName: "", IdRole: idRoleOld, IdRoleNew: idRole, Roles: "" };
    Swal.fire({
        title: '¿Estás seguro?',
        text: "No podrás revertir esto",
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '¡Sí!',
        allowOutsideClick: false,
        allowEscapeKey: false
    }).then((result) => {
        if (result.value) {
            Swal.fire({
                title: '¡Procesando...!',
                html: 'Me cerraré en unos segundos',
                onBeforeOpen: () => {
                    Swal.showLoading()
                },
                allowOutsideClick: false,
                allowEscapeKey: false
            });

            fetch(url, {
                method: "POST",
                body: JSON.stringify(data),
                headers: {
                    "Accept": "application/json",
                    "Content-Type": "application/json"
                }
            }).then((response) => { //Promise to server
                if (response.ok) {
                    return response.text();
                } else {
                    Swal.fire({
                        icon: 'error',
                        title: '¡Ups!. Algo salió mal =(',
                        text: 'Servidor no responde',
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    })
                };
            }).then((responseResult) => {
                if (responseResult.Status == "ModelError") {
                    Swal.fire({
                        icon: 'infor',
                        title: 'Verifica',
                        text: responseResult.Message,
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    });

                    return false;
                }

                if (responseResult.Status == "Exception") {
                    Swal.fire({
                        icon: 'error',
                        title: '¡Ups!. Algo salió mal :(',
                        text: responseResult.Message,
                        allowOutsideClick: false,
                        allowEscapeKey: false
                    });

                    return false;
                }

                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: '¡Tu trabajo ha sido guardado exitosamente!',
                    showConfirmButton: false,
                    timer: 1500,
                    allowOutsideClick: false,
                    allowEscapeKey: false
                });

                return true;
            }).then((result) => {
                if (result) {
                    setTimeout(() => { document.location.href = urlReturn }, 1500);
                }
            })
        }
    });
};

//Get Colors for MakeId and Product
async function getColorsForProduct() {
    let productId = document.querySelector("#FkProductsId").value;
    Swal.fire({
        title: 'Cargando Colores',
        html: '¡Me cerraré en pocos segundos!',
        allowOutsideClick: false,
        allowEscapeKey: false,
        onBeforeOpen: () => {
            Swal.showLoading()
        }
    });

    let url = `/Admin/GetColorsForProduct/${productId}`;
    let response = await fetch(url, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    });

    if (response.ok) {
        let productsForCategory = await response.json();
        let cbo = document.getElementById("idColors");
        cbo.innerHTML = "";
        productsForCategory.forEach((element) => {
            let opt = document.createElement("option");
            opt.appendChild(document.createTextNode(element.ColorName));
            opt.value = parseInt(element.ColorsId);
            cbo.appendChild(opt);
        });

        cbo.focus();
        setTimeout(() => Swal.close(), 500);
    }
}

//Gets cascade dropdown list of categories and models
async function getProductsForMake(makeId) {
    Swal.fire({
        title: 'Cargando Producros por marca',
        html: '¡Me cerraré en pocos segundos!',
        allowOutsideClick: false,
        allowEscapeKey: false,
        onBeforeOpen: () => {
            Swal.showLoading()
        }
    });

    let url = `/Home/make/${makeId}/products`;
    let response = await fetch(url, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    });

    if (response.ok) {
        let prodcuctsForMake = await response.json();
        let cbo = document.getElementById("FkProductsId");
        cbo.innerHTML = "";
        
        prodcuctsForMake.forEach((element) => {
            let opt = document.createElement("option");
            opt.appendChild(document.createTextNode(element.ProductName));
            opt.value = parseInt(element.ProductsId);
            cbo.appendChild(opt);
        });

        getColorsForProduct();
        cbo.focus();
        setTimeout(() => Swal.close(), 500);
    }
};


//Gets cascade dropdown list of categories and models
async function getModelsForMake(makeId) {
    Swal.fire({
        title: 'Cargando Modelos por marca',
        html: '¡Me cerraré en pocos segundos!',
        allowOutsideClick: false,
        allowEscapeKey: false,
        onBeforeOpen: () => {
            Swal.showLoading()
        }
    });

    let url = `/Home/make/${makeId}/models`;
    let response = await fetch(url, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    });

    if (response.ok) {
        let productsForCategory = await response.json();
        let cbo = document.getElementById("FkModelsId");
        cbo.innerHTML = "";
        
        productsForCategory.forEach((element) => {
            let opt = document.createElement("option");
            opt.appendChild(document.createTextNode(element.ModelName));
            opt.value = parseInt(element.ModelsId);
            cbo.appendChild(opt);
        });

        let modelsId = cbo.value;
        await getCategoryForModel(makeId, modelsId);

        cbo.focus();
        setTimeout(() => Swal.close(), 500);
    }

    disableMasseyFields(makeId);
};

// Enable or disable Massey fields
async function disableMasseyFields(makeId) {
    let url = `/api/Makes/${makeId}`;
    let response = await fetch(url, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    });

    if (response.ok) {
        let makes = await response.json();
        if (!makes.MakeName.toLowerCase().includes("massey")) {
            $('#masseyFields').find('input').val('');
            $('#masseyFields').find('input').prop("disabled", true);
            $("#masseyFields").hide()
            return;
        };

        $('#masseyFields').find('input').prop("disabled", false);
        $("#masseyFields").show()
    }
}

//Get Category for Model
async function getCategoryForModel(makesId, modelsId) {
    Swal.fire({
        title: 'Cargando categoria por modelo',
        html: '¡Me cerraré en pocos segundos!',
        allowOutsideClick: false,
        allowEscapeKey: false,
        onBeforeOpen: () => {
            Swal.showLoading()
        }
    });

    let url = `/Home/make/${makesId}/models/${modelsId}/category`;
    let response = await fetch(url, {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json"
        }
    });

    if (response.ok) {
        let categoryForModel = await response.json();
        let cbo = document.getElementById("FkCategoriesId");
        cbo.innerHTML = "";
        
        let opt = document.createElement("option");
        opt.appendChild(document.createTextNode(categoryForModel.CategoryName));
        opt.value = parseInt(categoryForModel.CategoriesId);
        cbo.appendChild(opt);
        setTimeout(() => Swal.close(), 500);
    }

};

//when is creating Warranty
function saveDescription(option) {
    document.querySelector("#descriptionId").value = document.getElementById('descriptionRichId').value;
    if (option === "warranty") {
        document.frmWarrantyCreate.submit();
    }

    if (option === "product") {
        document.querySelector("#allowShowHiddenId").value = document.querySelector("#cbxAllowShow").checked ? 1 : 0;
        document.querySelector("#isFeaturedId").value = document.querySelector("#cbxIsFeaturedId").checked ? 1 : 0;
        document.frmCreateProduct.submit();
    }
}

//Edit Warranty
function editDescription(option) {
    document.querySelector("#descriptionId").value = document.getElementById('descriptionRichId').ej2_instances[0].getHtml();
    if (option === "warranty") {
        document.frmWarrantyCreate.submit();
    }

    if (option === "product") {
        document.querySelector("#allowShowHiddenId").value = document.querySelector("#cbxAllowShow").checked ? 1 : 0;
        document.querySelector("#isFeaturedId").value = document.querySelector("#cbxIsFeaturedId").checked ? 1 : 0;
        document.querySelector("#categoryHiddenId").value = document.querySelector("#FkCategoriesId").value;

        document.frmEditProduct.submit();
    }
}

// Size validation
function validateSize(from) {
    if (from === "Product") {
       
        if (document.querySelector("#Pictures360").files != null && document.querySelector("#Pictures360").files.length > 0) {
            let control = 0

            Array.prototype.forEach.call(document.querySelector("#Pictures360").files, (item) => {
                control += item.size;
            });

            ///Max 25MB
            if (control > 26214400) {
                document.querySelector("#Pictures360").value = "";
                Swal.fire("Chosen files are too large. Max 25MB");
                return false;
            }
        }
    }

    if (from === "Inventory") {
        if (document.querySelector("#Pictures360File").files != null && document.querySelector("#Pictures360File").files.length > 0) {
            let control = 0

            Array.prototype.forEach.call(document.querySelector("#Pictures360File").files, (item) => {
                control += item.size;
            });

            ///Max 25MB
            if (control > 26214400) {
                document.querySelector("#Pictures360File").value = "";
                Swal.fire("Chosen files are too large. Max 25MB");
                return false;
            }
        }
    }

    if (from === "Document") {
        if (document.querySelector("#File").files != null && document.querySelector("#File").files.length > 0) {
            let control = 0

            Array.prototype.forEach.call(document.querySelector("#File").files, (item) => {
                control += item.size;
            });

            ///Max 25MB
            if (control > 26214400) {
                document.querySelector("#File").value = "";
                Swal.fire("Chosen files are too large. Max 25MB");
                return false;
            }
        }
    }

    if (from === "HeroImageURL") {
        if (document.querySelector("#ImageUrlFile").files != null && document.querySelector("#ImageUrlFile").files.length > 0) {
            let control = 0

            Array.prototype.forEach.call(document.querySelector("#ImageUrlFile").files, (item) => {
                control += item.size;
            });

            ///Max 25MB
            if (control > 26214400) {
                document.querySelector("#ImageUrlFile").value = "";
                Swal.fire("Chosen files are too large. Max 25MB");
                return false;
            }
        }
    }
}

//save Inventory Items 
function saveInventoryItems() {
    document.querySelector("#isNewHiddenId").value = document.querySelector("#isNewId").value
    document.querySelector("#isSoldHiddenId").value = document.querySelector("#isSoldId").value
    document.querySelector("#allowShowHiddenId").value = document.querySelector("#cbxAllowShow").checked ? 1 : 0;
    document.frmInventoryItems.submit();
}

//save Inventory Items 
function saveInterestedCustomer() {
    document.querySelector("#managedHiddenId").value = document.querySelector("#cbxManaged").checked ? 1 : 0;
    document.frmInterestedCustomer.submit();
}


///Disable Upload Field when path have content
function disableUploadPictures(from, what, value) {
    if (from === "Products") {
        if (what === "Cover") {
            if (value != null && value != "") {
                document.getElementById("CoverPicture").value = "";
                document.getElementById("CoverPicture").disabled = true;
            } else {
                document.getElementById("CoverPicture").disabled = false;
            }
        }

        if (what === "360") {
            if (value != null && value != "") {
                document.getElementById("Pictures360").value = "";
                document.getElementById("Pictures360").disabled = true;
            } else {
                document.getElementById("Pictures360").disabled = false;
            }
        } 
    }

    if (from === "Inventory") {
        if (what === "Cover") {
            if (value != null && value != "") {
                document.getElementById("CoverPictureFile").value = "";
                document.getElementById("CoverPictureFile").disabled = true;
            } else {
                document.getElementById("CoverPictureFile").disabled = false;
            }
        }

        if (what === "360") {
            if (value != null && value != "") {
                document.getElementById("Pictures360File").value = "";
                document.getElementById("Pictures360File").disabled = true;
            } else {
                document.getElementById("Pictures360File").disabled = false;
            }
        }
    }

    if (from === "ProductDocument") {
        if (what === "FilePath") {
            if (value != null && value != "") {
                document.getElementById("File").value = "";
                document.getElementById("File").disabled = true;
            } else {
                document.getElementById("File").disabled = false;
            }
        }
    }
    
    if (from === "HeroImages") {
        if (what === "ImageURL") {
            if (value != null && value != "") {
                document.getElementById("ImageUrlFile").value = "";
                document.getElementById("ImageUrlFile").disabled = true;
            } else {
                document.getElementById("ImageUrlFile").disabled = false;
            }
        }
    }
}

////Add accordion for specifications
async function addAccordion() {
    let question = await Swal.mixin({
        input: 'text',
        confirmButtonText: 'Next &rarr;',
        showCancelButton: true,
        progressSteps: ['1'],
        allowOutsideClick: false,
        allowEscapeKey: false
    }).queue([
        {
            title: '¿Cuántas categorías?',
            text: 'Introduce la cantidad'
        }
    ]);

    if (question.value) {
        const quantity = question.value[0];
        let steps = [];
        let question1 = [];
        for (var i = 1; i <= quantity; i++) {
            steps.push(i.toString());
            question1.push('Nombre de categoría')
        }

        let names = await Swal.mixin({
            input: 'text',
            confirmButtonText: 'Next &rarr;',
            showCancelButton: true,
            progressSteps: steps,
            allowOutsideClick: false,
            allowEscapeKey: false
        }).queue(question1);
        
        if (names.value) {
            let id = makeid(5);
            let descriptionPartial = `&nbsp;<div id="${id}" role="tablist" aria-multiselectable="true">`;
            for (var i = 0; i < names.value.length; i++) {
                let div = makeid(5);
                let child = makeid(5);
                if (i == 0) {
                    descriptionPartial += `<div class="panel panel-default"><div class="panel-heading" role="tab" id="${div}">
                    <h5 class="panel-title">
                        <a data-toggle="collapse" data-parent="#${id}" href="#${child}" aria-expanded="true" aria-controls="${child}">
                            <span style="font-family: Arial, Helvetica, sans-serif;">
                                <span style="font-size: 10pt;">
                                    <span style="color: rgb(13, 13, 13); text-decoration: inherit;">
                                        <strong>${names.value[i]}</strong>
                                    </span>
                                </span>
                            </span>
                         </a>
                    </h5>                    
                    </div>
                    <div id="${child}" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="${div}">
                        <div class="panel-body">
                            <table class="e-rte-table table-striped" style="width: 100%; min-width: 0px;">
                            <tbody>
                                <tr>
                                    <td style="width: 50%;" class="">Default colunm 1</td>
                                    <td style="width: 50%;" class="">Default colunm 2</td>
                                </tr>
                                <tr>
                                    <td style="width: 50%;" class="">Default colunm 1</td>
                                    <td style="width: 50%;" class="">Default colunm 2</td>
                                </tr>
                                <tr>
                                    <td style="width: 50%;" class="">Default colunm 1</td>
                                    <td style="width: 50%;" class="">Default colunm 2</td>
                                </tr>
                                <tr>
                                    <td style="width: 50%;" class="">Default colunm 1</td>
                                    <td style="width: 50%;" class="">Default colunm 2</td>
                                </tr>
                                <tr>
                                    <td style="width: 50%;" class="">Default colunm 1</td>
                                    <td style="width: 50%;" class="">Default colunm 2</td>
                                </tr>
                                <tr>
                                    <td style="width: 50%;" class="">Default colunm 1</td>
                                    <td style="width: 50%;" class="">Default colunm 2</td>
                                </tr>
                            </tbody>
                            </table>
                        </div>
                    </div>
                </div></div>`;
                } else {
                    descriptionPartial += `<div class="panel panel-default">
                                    <div class="panel-heading" role="tab" id="${div}">
                                         <h5 class="panel-title">
                                            <a data-toggle="collapse" data-parent="#${id}" href="#${child}" aria-expanded="false" aria-controls="${child}">
                                                <span style="font-family: Arial, Helvetica, sans-serif;">
                                                    <span style="font-size: 10pt;">
                                                        <span style="color: rgb(13, 13, 13); text-decoration: inherit;">
                                                            <strong>${names.value[i]}</strong>
                                                        </span>
                                                    </span>
                                                </span>
                                             </a>
                                        </h5>
                                    </div>
                                    <div id="${child}" class="panel-collapse collapse" role="tabpanel" aria-labelledby="${div}">
                                        <div class="panel-body">
                                             <table class="e-rte-table table-striped" style="width: 100%; min-width: 0px;">
                                                  <tbody>
                                                    <tr>
                                                        <td style="width: 50%;" class="">Default colunm 1</td>
                                                        <td style="width: 50%;" class="">Default colunm 2</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%;" class="">Default colunm 1</td>
                                                        <td style="width: 50%;" class="">Default colunm 2</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%;" class="">Default colunm 1</td>
                                                        <td style="width: 50%;" class="">Default colunm 2</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%;" class="">Default colunm 1</td>
                                                        <td style="width: 50%;" class="">Default colunm 2</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%;" class="">Default colunm 1</td>
                                                        <td style="width: 50%;" class="">Default colunm 2</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 50%;" class="">Default colunm 1</td>
                                                        <td style="width: 50%;" class="">Default colunm 2</td>
                                                    </tr>
                                                </tbody>
                                             </table>
                                        </div>
                                    </div>
                                </div>`;
                }
            }

            descriptionPartial += '</div> <br/>';
            let content = document.getElementById('descriptionRichId').ej2_instances[0].getHtml() ?? '';
            document.getElementById('descriptionRichId').ej2_instances[0].value = content + descriptionPartial;

            const Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000,
                timerProgressBar: true,
                onOpen: (toast) => {
                    toast.addEventListener('mouseenter', Swal.stopTimer)
                    toast.addEventListener('mouseleave', Swal.resumeTimer)
                }
            })

            Toast.fire({
                icon: 'success',
                title: 'Added successfully'
            });
        } else {
            return;
        }
    } else {
        return;
    }
};

////Generates random string
function makeid(length) {
    var result = '';
    var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
    var charactersLength = characters.length;
    for (var i = 0; i < length; i++) {
        result += characters.charAt(Math.floor(Math.random() * charactersLength));
    }
    return result;
}

////Disable or enable ButtonURL
function enableHeroImageButtonUrl(value) {
    if (!value) {
        document.getElementById("ButtonURL").value = '';
        document.getElementById("ButtonURL").disabled = true;
    } else {
        document.getElementById("ButtonURL").disabled = false;
    }
}

function enableSpanText(value) {
    if (!value) {
        document.getElementById("SpanText").value = '';
        document.getElementById("SpanText").disabled = true;

        document.getElementById("SpanComplementText").value = '';
        document.getElementById("SpanComplementText").disabled = true;
    } else {
        document.getElementById("SpanText").disabled = false;
        document.getElementById("SpanComplementText").disabled = false;
    }
}

////Set indexs
function enableIndex(value, originalIndex) {
    if (!value) {
        document.getElementById("Index").readOnly = false;
        document.getElementById("Index").value = originalIndex;
    } else {
        document.getElementById("Index").readOnly = true;
        document.getElementById("Index").value = 1;
    }
}