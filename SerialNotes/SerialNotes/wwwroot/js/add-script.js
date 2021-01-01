'use strict'


document.addEventListener("DOMContentLoaded", (e) => {
    SetDate();
})



async function Add(form) {
    if (FormValid(form) === true) {
        await fetch(form.action, {
            method: 'POST',
            body: new FormData(form)
        }).then(res => {
            return res.json();
        }).then(data => {
            if (data.status > 0 && data.status !== null) {
                form.reset()
                SetDate()
            }
            document.body.innerHTML += data.renderHtml

            const modal = document.querySelector('#modal-notify')
            document.querySelector('[data-close]').addEventListener('click', (e) => {
                modal.parentNode.removeChild(modal)
            })
        }).catch(ex => {
            console.error(ex)
        })
    }
}

function SetDate() {
    const date = new Date();
    let day = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
    let month = (date.getMonth() + 1) < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1
    document.querySelector('[data-date-picker]').value = `${date.getFullYear()}-${month}-${day}`
}

function FormValid(form) {
    let result = true;

    const validInput = [0, 2, 3, 4, 5];
    Array.from(form.elements).forEach((element, i) => {
        validInput.forEach(index => {
            if (i === index) {
                if (element.value === null || element.value === '') {
                    element.classList.add('err');
                    element.addEventListener('focus', () => {
                        element.classList.remove('err')
                    })
                    result = false;
                }
            }
        })
    });
    return result;
}

function FormValidTwoXD(form) {
    let result = true;

    Array.from(form.elements).forEach((element, i) => {       
        if (element.value === null || element.value === '') {
            element.classList.add('err');
            element.addEventListener('focus', () => {
                element.classList.remove('err')
            })
            result = false;
        }
    });
    return result;
}


function OpenListSerial() {

    const closeBtns = document.querySelectorAll('#close-btn')
    const modal = document.querySelector('#modal-serial-list')

    modal.classList.remove('hidden')
    closeBtns.forEach(closeBtn => {
        closeBtn.addEventListener('click', () => {
            modal.classList.add('hidden')
        })
    })


    const els = document.querySelectorAll('[data-serial-selected]')

    els.forEach(el => {
        el.addEventListener('click', () => {
            document.querySelector('[data-serial-name]').value = el.innerText.trim()
            document.querySelector('[data-serial-name]').classList.remove('err')
            modal.classList.add('hidden')
        })
    })

}


const btns = document.querySelectorAll('[data-btn-add]')
btns.forEach(btn => {
    btn.addEventListener('click', (e) => {
        if (!btn.className.includes('btn__filter_activ')) {
            btn.classList.add('btn__filter_activ')
        }
    })
    btn.classList.remove('btn__filter_activ')
})


async function AddView(url) {


    await fetch(url, {
        method: 'GET'
    }).then(res => {
        return res.json();
    }).then(data => {
        document.querySelector('[data-add-content]').innerHTML = data.renderHtml       
    }).catch(ex => {
        console.error(ex)
    })
    SetDate()
}

async function SerialAdd(form) {
    if (FormValidTwoXD(form)) {
        await fetch(form.action, {
            method: 'POST',
            body: new FormData(form)
        }).then(res => {
            return res.json()
        }).then(data => {
            if (data.status > 0 && data.status !== null) {
                form.reset()
                SetDate()
            }
            document.body.innerHTML += data.renderHtml

            const modal = document.querySelector('#modal-notify')
            document.querySelector('[data-close]').addEventListener('click', (e) => {
                modal.parentNode.removeChild(modal)
            })
        })
    }
}