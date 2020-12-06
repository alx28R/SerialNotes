'use strict'



document.addEventListener("DOMContentLoaded", () => {
    StatsRender('stats/liststats')
    document.querySelector('[data-btn]').classList.add('btn__filter_activ')
})


async function StatsRender(url) {
    const btns = document.querySelectorAll('[data-btn]')
    btns.forEach(btn => {
        btn.addEventListener('click', () => {
            if (!btn.className.includes('btn__filter_activ'))
                btn.classList.add('btn__filter_activ')

        })
        btn.classList.remove('btn__filter_activ')
    })


    let labels = []
    let results = []
    await fetch(url,
        {
            method: 'GET'
        }).then(res => {           
            return res.json()
        }).then(data => {
            let obj = data.statsListSerial !== null ? data.statsListSerial : data.statsListAvg
            Array.from(obj).forEach(el => {
                labels.push(el.serialName)
                results.push(el.result)
            })
            Table(obj)
            results.push(0)
            results.push(Math.max.apply(null, results) + .5)
        }).catch(ex => {
            console.error(ex)
        })

    Diagramm(labels, results);

}

function Diagramm(labels, results) {

    const canvasCont = document.querySelector('.stats-canvas')
    canvasCont.innerHTML = '';
    const canvas = document.createElement('canvas')
    canvas.classList.add('myChart')
    canvasCont.append(canvas);

    let ctx = canvas.getContext('2d')
   
    let chart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Сериал',
                backgroundColor: 'rgba(130, 114, 176, 0.66)',
                borderColor: 'rgba(130, 114, 176, 0.66)',
                data: results
            }]
        },
        options: {}
    });
    chart.update();
}

function Table(data) {
    const listContainer = document.querySelector('.stats-list-container')
    listContainer.innerHTML = '';
    Array.from(data).forEach((el, i) => {

        let listEl = TableElement(el, i+1)

        listContainer.append(listEl);
    })

}


function TableElement(el, i) {
    const listEl = document.createElement('div')
    listEl.classList.add('stats__list-item')

    const posDiv = document.createElement('div');
    posDiv.classList.add('list-item__position')
    posDiv.innerHTML = `${i}`

    const nameDiv = document.createElement('div')
    nameDiv.classList.add('list-item__name')
    nameDiv.innerHTML = `${el.serialName}`

    const resultDiv = document.createElement('div')
    resultDiv.classList.add('list-item__result')
    resultDiv.innerHTML = `${el.result}`

    listEl.append(posDiv)
    listEl.append(nameDiv)
    listEl.append(resultDiv)

    return listEl
}

