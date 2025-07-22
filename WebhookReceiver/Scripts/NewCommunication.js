var baseUrl1 = ""
//http://testsrv1601.enate.local/DCweb
async function FirstData() {
    let response = await fetch(`${baseUrl1}/api/webhook`)
    let data = await response.json()
    
    return data
}
FirstData().then(data => {
    let obj = JSON.parse(data)
    for (let i = 0; i < obj.length; i++) {
        var x = document.getElementById("mainTableOfCommunication").insertRow(-1)
        var a = x.insertCell(0)
        var b = x.insertCell(1)
        var c = x.insertCell(2)
        var d = x.insertCell(3)
        var e = x.insertCell(4)
        var f = x.insertCell(5)
        var g = x.insertCell(6)

        a.innerHTML = obj[i].Reference
        b.innerHTML = obj[i].Title
        c.innerHTML = obj[i].FromAddress1
        d.innerHTML = obj[i].ToAddress1
        e.innerHTML = obj[i].AttachmentCount1
        f.innerHTML = obj[i].Body1
        g.innerHTML = `<i id = "${obj[i].GUID}" onclick="deleteRow1(this.id)" style="font-size: 24px; cursor: pointer" class="fa">&#xf1f8;</i>`
    }
})


async function getData1() {
    let response = await fetch(`${baseUrl1}/api/webhookcommunications`)
    let data = await response.json()
    return data
}

setInterval(function () {
    getData1().then(data => {
        try {
            let obj = JSON.parse(data)
            console.log(obj)
            for (let i = 0; i < obj.length; i++) {
                var x = document.getElementById("mainTableOfCommunication").insertRow(-1)
                var a = x.insertCell(0)
                var b = x.insertCell(1)
                var c = x.insertCell(2)
                var d = x.insertCell(3)
                var e = x.insertCell(4)
                var f = x.insertCell(5)
                var g = x.insertCell(6)

                a.innerHTML = obj[i].Reference
                b.innerHTML = obj[i].Title
                c.innerHTML = obj[i].FromAddress1
                d.innerHTML = obj[i].ToAddress1
                e.innerHTML = obj[i].AttachmentCount1
                f.innerHTML = obj[i].Body1
                g.innerHTML = `<i id = "${obj[i].GUID}" onclick="deleteRow1(this.id)" style="font-size: 24px; cursor: pointer" class="fa">&#xf1f8;</i>`
            }
        } catch (e) { }
    })
}, 3000)


function deleteRow1(e) {
    document.getElementById(e).parentElement.parentElement.remove();
    deletedata(e, `${baseUrl1}/api/webhookcommunications`)
    function deletedata(item, url) {
        return fetch(url + "/" + item, {
            method: "delete"
        }).then(response => response.json())
    }
}