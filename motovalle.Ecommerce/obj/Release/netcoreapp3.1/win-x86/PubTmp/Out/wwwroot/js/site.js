/*
    Date: 2020/02/12
    Company: Innova Marketing Systems S.A.S
    Comments: functions for Index view
 */

function goToPost(id) {
    document.location.href = `/Blog/Post/${id}`;
}

//if (document.querySelector(".info-financiera")) {
//    document.querySelector(".info-financiera").addEventListener("click", () => {
//        $(".finacieras-box").animate({
//            width: "toggle"
//        });
//    });
//}

/**
 * Toggle Z-Index
 * */
function togglezIndex() {
    const $breadCumbs = document.querySelector(".b-breadCumbs");
    const $navBar = document.querySelector(".b-nav");
    if ($breadCumbs) {
        $navBar.addEventListener("mouseover", () => {
            $breadCumbs.style.zIndex = "-1";
        });

        $navBar.addEventListener("mouseout", () => {
            $breadCumbs.style.zIndex = "auto";
        });
    }
}

/**
 * Call Wompi For Online payments
 * */
async function callWompiHome() {
    const url = '/Pagos-en-linea/Wompi';
    const request = await fetch(url);
    const $commonModal = document.querySelector("#commonModalId");
    $commonModal.querySelector('h3').textContent = "Pagos en línea";
    $($commonModal).modal("show");
    if (request.ok) {
        const data = await request.text();
        setTimeout(() => {
            $commonModal.querySelector('.modal-body').innerHTML = data;
            setCurrencyMask();
        }, 1000);
    } else {
        const errorMsg = '<div class="alert alert-danger fade-in" role="alert"><b class="lead">Lo sentimos ha ocurrido un error.</b></div>'
        $commonModal.querySelector('.modal-body').innerHTML = errorMsg;
    }
}

/**
 * Set Mask
 * */
function setCurrencyMask() {
    const $currencyMaskInput = document.querySelector(".currency-mask");
    if ($currencyMaskInput) {
        const minAttr = $currencyMaskInput.getAttribute("data-min-amount") ? $currencyMaskInput.getAttribute("data-min-amount") : 1;
        const maxAttr = $currencyMaskInput.getAttribute("data-max-amount") ? $currencyMaskInput.getAttribute("data-max-amount") : null;
        var currencyMask = IMask(
            document.querySelector('.currency-mask'),
            {
                mask: '$num',
                blocks: {
                    num: {
                        // nested masks are available!
                        mask: Number,
                        thousandsSeparator: '.',
                        min: minAttr,
                        max: maxAttr
                    }
                }
            });

        $currencyMaskInput.addEventListener("keyup", () => {
            const mainInputAttr = $currencyMaskInput.getAttribute("data-main");
            const $mainInput = document.getElementById(mainInputAttr);
            if ($mainInput) {
                $mainInput.value = currencyMask.unmaskedValue;
            }

        });
    }
}

/**
 * Print section
 * @param {string} sectionId
 */
function printSection(sectionId) {
    var prtContent = document.getElementById(sectionId);
    var WinPrint = window.open('', '', 'left=0,top=0,width=800,height=900,toolbar=0,scrollbars=0,status=0');
    WinPrint.document.write(prtContent.innerHTML);
    WinPrint.document.close();
    WinPrint.focus();
    WinPrint.print();
    WinPrint.close();
}

////When app starts
(() => {
    togglezIndex();
    setCurrencyMask();
})();