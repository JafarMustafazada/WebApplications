var urlOrigin = window.location.origin;

function pagination(event) {
    event.preventDefault();
    let instance = event.currentTarget;
    //console.log(urlHost + instance.getAttribute("href"));
    
    fetch(urlOrigin + instance.getAttribute("href") + instance.getAttribute("attributes")).then(res => {
        return res.text()
    }).then(data => {
        document.getElementById(instance.getAttribute("ptagId")).innerHTML = data;
    });
}