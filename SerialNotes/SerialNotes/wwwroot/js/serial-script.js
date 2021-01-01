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
    form.addEventListener('submit', async (e) => {
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
        if (FormValidTwoXD(form)) {
            await fetch(form.action, {
                method: 'POST',
                body: new FormData(form)
            }).then(res => {
                return res.json()
            }).then(data => {

                document.querySelector('.serial').innerHTML = data.renderHtml
            }).catch(ex => {
                console.error(ex)
            })

            const modal = document.querySelector('#modal')
            modal.parentNode.removeChild(modal)
        }
        return false
    })
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

function CloseModal() {
    const modal = document.querySelector('#modal')
    modal.parentNode.removeChild(modal)
}

async function PartChecked(checkbox,id) {

    const form = document.querySelector(`[data-part="${id}"]`)
    let body = new FormData()
    
    body.append("PartId", form.elements[0].value)
    body.append("IsViewed", checkbox.checked)
   

    await fetch('Serial/Part', {
        method: 'POST',
        body: body
    })
   
}


async function PartAddModal(url) {
    await fetch(url, {
        method: 'GET'
    }).then(res => {
        return res.json()
    }).then(data => {
        document.body.innerHTML += data.renderHtml;
    })

    try {
        const form = document.querySelector('[data-add]')
        form.addEventListener('submit', async (e) => {
            e.preventDefault();

            let body = new FormData();
            body.append("SerialId", form.elements[0].value);
            body.append("P", form.elements[2].value);
            body.append("Season", form.elements[1].value);
            body.append("IsViewed", form.elements[3].checked);


            await fetch('Serial/AddPart', {
                method: "POST",
                body: body
            }).then(res => {
                console.log(res);
                return res.json()
            }).then(data => {
                document.querySelector('.serial').innerHTML = data.renderHtml
            })



            const modal = document.querySelector('#modal')
            modal.parentNode.removeChild(modal)
        })
    } catch (ex) {
        console.log(ex);
    }
       
}


async function PartDelete(url) {
    await fetch(url, {
        method: "POST"
    }).then(res => {
        return res.json()
    }).then(data => {
        document.querySelector('.serial').innerHTML = data.renderHtml
    }).catch(ex => {
        console.log(ex)
    })
}