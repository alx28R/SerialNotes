'use strict'

async function Search(form) {
    fetch(form.action, {
        method: 'POST',
        body: new FormData(form)
    }).then(res => {
        return res.json();
    }).then(data => {

        const contentNotes = document.querySelector('[data-content-notes]')
        contentNotes.innerHTML = data.html
    }).catch(ex => {
        console.error(ex)
    })
}

async function SearchClear() {
    const text = document.querySelector('[data-adding-date]')
    const date = document.querySelector('[data-text]')

    text.value = ''
    date.value = ''

    fetch('Home/Notes', {
        method: 'POST',
    }).then(res => {
        return res.json()
    }).then(data => {
        const contentNotes = document.querySelector('[data-content-notes]')
        contentNotes.innerHTML = data.renderHtml
    }).catch(ex => {
        console.error(ex)
    })
}


async function DeleteModal(url) {
    await fetch(url, {
        method: 'GET',
    }).then(res => {
        return res.json()
    }).then(data => {
        document.body.innerHTML += data.renderHtml

    }).catch(ex => {
        console.error(ex)
    })

    const closeBtns = document.querySelectorAll('#close-btn')
    const modal = document.querySelector('#modal')

    closeBtns.forEach(closeBtn => {
        closeBtn.addEventListener('click', () => {
            modal.parentNode.removeChild(modal)
        })
    })

    const form = document.querySelector('[data-del]')
    form.addEventListener('submit', async(e) => {
        e.preventDefault();

        await fetch(form.action, {
            method: 'POST',
            body: new FormData(form)
        }).then(res => {
            return res.json()
        }).then(data => {
            modal.parentNode.removeChild(modal)

            const contentNotes = document.querySelector('[data-content-notes]')
            contentNotes.innerHTML = data.renderHtml
        }).catch(ex => {
            console.error(ex)
        })
    })
}

async function EditModal(url) {
    await fetch(url, {
        method: 'GET',
    }).then(res => {
        return res.json()
    }).then(data => {
        document.body.innerHTML += data.renderHtml

    }).catch(ex => {
        console.error(ex)
    })

    const form = document.querySelector('[data-edit]');
        form.addEventListener('submit', async (e) => {
            e.preventDefault();
            if (FormValid(form) === true) {
                await fetch(form.action, {
                    method: 'POST',
                    body: new FormData(form)
                }).then(res => {
                    return res.json()
                }).then(data => {
                    const contentNotes = document.querySelector('[data-content-notes]')
                    contentNotes.innerHTML = data.renderHtml
                })

                const modal = document.querySelector('#modal')
                modal.parentNode.removeChild(modal)
            }

        })
}

function FormValid(form) {
    let result = true;
    const validInput = [1, 3, 4, 5, 6, 7, 8];
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


function CloseModal() {
    const modal = document.querySelector('#modal')
    modal.parentNode.removeChild(modal)
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
