function setDevelopers(event) {

    // location.search = `type=Developer&name=${event.target.name}`;
    $("#games").load(`/Game/Filter?type=Developer&name=${encodeURIComponent(event.target.name)}`)

}

function setGenres(event) {

    //  location.search = `type=Genre&name=${event.target.name}`;
    $("#games").load(`/Game/Filter?type=Genre&name=${encodeURIComponent(event.target.name)}`)


}
function search() {

    $("#games").load(`/Game/Search?search=${document.querySelector('#search').textContent}`)

}