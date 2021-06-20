var baseUrl = ""
//http://testsrv1601.enate.local/DCweb



function subscribeClick(e, data) {
    if (data == "Subscribe Comms") {
        let id = e.slice(13);
        packetId = id
     
        addWebhook(id,`${baseUrl}/api/webhook`)
        function addWebhook(item, url) {
            return fetch(url + "/" + item, {
                method: "POST"
            }).then(response => response.json())
                .then(data => {
                
                })
        }
    }
    else if (data == "Unsubscribe Comms") {
        
        let id = e.slice(13);
        
        deleteWebhook(id,`${baseUrl}api/webhook`)
        function deleteWebhook(item , url) {
            return fetch(url + "/" + item, {
                method: "delete"
            }).then(response => response.json())
                .then(data => {
                  
                });
        }
    }
}


async function FirstData() {
    let response = await fetch(`${baseUrl}/api/FirstCall`)
    let data = await response.json()
    return data
}
FirstData().then(data => {
    let obj = JSON.parse(data)
    console.log(obj);
    for (let i = 0; i < obj.length; i++) {
        var x = document.getElementById("mainTable").insertRow(-1)
        var a = x.insertCell(0)
        var b = x.insertCell(1)
        var c = x.insertCell(2)
        var d = x.insertCell(3)
        var e = x.insertCell(4)
        var f = x.insertCell(5)
        var g = x.insertCell(6)
        var h = x.insertCell(7)
        var k = x.insertCell(8)
        var j = x.insertCell(9)


        
        x.setAttribute("id", obj[i].PacketGUID)
        let date = new Date(obj[i].DueDate)
        date = date.toLocaleString()
        let overdue = `<div style="background: #EC6655;color: white;height: 0px;WIDTH: 134px;padding-top: 4px;padding-bottom: 28px;font-weight: bold;">Overdue</div>`
        let dueToday = `<div style="background: #ffc107;color: white;height: 0px;WIDTH: 134px;padding-top:4px;padding-bottom:28px;font-weight: bold;">Due Today</div>`
        let onTaget = `<div style="background: #61E062;color: white;height: 0px;WIDTH: 134px;padding-top: 4px;padding-bottom: 28px;font-weight: bold;">On target</div>`

        if (obj[i].NewCommunication == "1") {
            a.innerHTML = `<td>${obj[i].Reference} <br><div style="padding: 0;width: 0;" class="form-check form-switch"><input data-toggle="tooltip" title="Unsubscribe for communication notification" data-placement="bottom" id="subscribeBtn-${obj[i].PacketGUID}" onclick = "subscribeClick(this.id , 'Unsubscribe Comms')" style="margin-left: 3px" class="form-check-input" type="checkbox" id="flexSwitchCheckDefault" checked /></div></td>`

         
        }
        else if (obj[i].NewCommunication == "0") {
            a.innerHTML = `<td>${obj[i].Reference} <br><div style="padding: 0;width: 0;" class="form-check form-switch"><input data-toggle="tooltip" title="Subscribe for communication notification" data-placement="bottom" id="subscribeBtn-${obj[i].PacketGUID}" onclick = "subscribeClick(this.id , 'Subscribe Comms')" style="margin-left: 3px" class="form-check-input" type="checkbox" id="flexSwitchCheckDefault" /></div></td>`

        }

        b.innerHTML = obj[i].Title
        c.innerHTML = date
        d.innerHTML = obj[i].ProcessTypeName
        e.innerHTML = obj[i].CustomerName
        f.innerHTML = obj[i].ContractName
        g.innerHTML = obj[i].ServiceName
        if (obj[i].RAGStatus == "-1") {
            h.innerHTML = overdue
        } else if (obj[i].RAGStatus == "1") {
            h.innerHTML = onTaget
        } else {
            h.innerHTML = dueToday
        }
        k.innerHTML = `<i id="note-${obj[i].PacketGUID}" onclick="showNoteBox(this.id)" style="font-size: 24px; cursor: pointer" class="fa">&#xf044;</i>`
        j.innerHTML = `<i id="del-${obj[i].PacketGUID}" onclick="deleteRow(this.id)" style="font-size: 24px; cursor: pointer" class="fa">&#xf1f8;</i>`
    }
})


async function getData() {
    let response = await fetch(`${baseUrl}/api/values`)
    let data = await response.json()
    return data
}

setInterval(function () {
    getData().then(data => {
        try {
            let obj = JSON.parse(data)
            console.log(obj)
            for (let i = 0; i < obj.length; i++) {
                var x = document.getElementById("mainTable").insertRow(-1)
                var a = x.insertCell(0)
                var b = x.insertCell(1)
                var c = x.insertCell(2)
                var d = x.insertCell(3)
                var e = x.insertCell(4)
                var f = x.insertCell(5)
                var g = x.insertCell(6)
                var h = x.insertCell(7)
                var k = x.insertCell(8)
                var j = x.insertCell(9)

       

                
                x.setAttribute("id", obj[i].PacketGUID)
                let date = new Date(obj[i].DueDate)
                date = date.toLocaleString()
                let overdue = `<div style="background: #EC6655;color: white;height: 0px;WIDTH: 134px;padding-top: 8px;padding-bottom: 27px;font-weight: bold;">Overdue</div>`
                let dueToday = `<div style="background: #ffc107;color: white;height: 0px;WIDTH: 134px;padding-top:8px;padding-bottom:27px;font-weight: bold;">Due Today</div>`
                let onTaget = `<div style="background: #61E062;color: white;height: 0px;WIDTH: 134px;padding-top: 8px;padding-bottom: 27px;font-weight: bold;">On target</div>`
                if (obj[i].NewCommunication == "1") {
                    a.innerHTML = `<td>${obj[i].Reference} <br><div style="padding: 0;width: 0;" class="form-check form-switch"><input data-toggle="tooltip" title="Unsubscribe for communication notification" data-placement="bottom" id="subscribeBtn-${obj[i].PacketGUID}" onclick = "subscribeClick(this.id , 'Unsubscribe Comms')" style="margin-left: 3px" class="form-check-input" type="checkbox" id="flexSwitchCheckDefault" checked /></div></td>`

                    // a.innerHTML = `<td>${obj[i].Reference} <br> <span> <button id="subscribeBtn-${obj[i].PacketGUID}" style="cursor: pointer;height: 18px;margin-top: 5px;width: 125px;font-size: 11px;padding-right: 7px;font-weight: bold;" onclick = "subscribeClick(this.id)">Unsubcribe Comms</button> </span></td>`

                }
                else if (obj[i].NewCommunication == "0") {
                    a.innerHTML = `<td>${obj[i].Reference} <br><div style="padding: 0;width: 0;" class="form-check form-switch"><input data-toggle="tooltip" title="Subscribe for communication notification" data-placement="bottom" id="subscribeBtn-${obj[i].PacketGUID}" onclick = "subscribeClick(this.id , 'Subscribe Comms')" style="margin-left: 3px" class="form-check-input" type="checkbox" id="flexSwitchCheckDefault" /></div></td>`

                }

                b.innerHTML = obj[i].Title
                c.innerHTML = date
                d.innerHTML = obj[i].ProcessTypeName
                e.innerHTML = obj[i].CustomerName
                f.innerHTML = obj[i].ContractName
                g.innerHTML = obj[i].ServiceName
                if (obj[i].RAGStatus == "-1") {
                    h.innerHTML = overdue
                } else if (obj[i].RAGStatus == "1") {
                    h.innerHTML = onTaget
                } else {
                    h.innerHTML = dueToday
                }
                k.innerHTML = `<i id="note-${obj[i].PacketGUID}" onclick="showNoteBox(this.id)" style="font-size: 24px; cursor: pointer" class="fa">&#xf044;</i>`
                j.innerHTML = `<i id="del-${obj[i].PacketGUID}" onclick="deleteRow(this.id)" style="font-size: 24px; cursor: pointer" class="fa">&#xf1f8;</i>`
            }
        } catch (e) { }
    })
}, 3000)


function deleteRow(e) {
    let deleteID = document.getElementById(e).id
    deleteID = deleteID.slice(4)
    document.getElementById(deleteID).remove()
    console.log(deleteID)
    deletedata(deleteID, `${baseUrl}/api/values`)
    function deletedata(item, url) {
        return fetch(url + "/" + item, {
            method: "delete"
        }).then(response => response.json())
    }
}

var packetGUID, apiKey, ref
function showNoteBox(id) {
    modal.style.display = "block"
    packetGUID = id.slice(5)
    ref = document.getElementById(packetGUID).firstChild.innerHTML
    ref =  ref.substr(0, ref.indexOf('<'))
    document.getElementById("lname").value = ref
    console.log(packetGUID)
}

function addNote(e) {
    var notedata = document.getElementById("noteInput").value
    var xmlhttp = new XMLHttpRequest()
    theUrl = `api/Helpers/${packetGUID}`
    xmlhttp.open("POST", theUrl)
    xmlhttp.setRequestHeader("Content-Type", "application/json")

    xmlhttp.send(
        JSON.stringify({
            Body: notedata,
            CommunicationType: 4,
            IsResolutionCommunication: false
        })
    )
    xmlhttp.onload = function () {
        if (xmlhttp.status === 200 || xmlhttp.status === 204) {
            document.getElementById("myModal").style.display = "none"
        }
    }
}

function closeModel() {
    modal.style.display = "none"
}

var modal = document.getElementById("myModal")
// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0]

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none"
    }
}
// HTMLTableSectionElement

document.getElementById("PacketCreated").style.display = "block"
document.getElementById("first").className += " w3-border-blue"

function openCity(evt, cityName) {
    var i, x, tablinks
    x = document.getElementsByClassName("city")
    for (i = 0; i < x.length; i++) {
        x[i].style.display = "none"
    }
    tablinks = document.getElementsByClassName("tablink")
    for (i = 0; i < x.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" w3-border-blue", "")
    }
    document.getElementById(cityName).style.display = "block"
    evt.currentTarget.firstElementChild.className += " w3-border-blue"
}