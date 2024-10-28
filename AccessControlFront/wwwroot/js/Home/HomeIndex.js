var map;

function initMap() {
    map = new window.google.maps.Map(document.getElementById("map"), {
        center: { lat: -34.542982510209285, lng: -58.712 },
        zoom: 18,
        mapTypeControlOptions: {
            mapTypeIds: ["roadmap", "satellite", "hybrid", "terrain", "styled_map"],
        },
        mapTypeId: 'satellite'
    });
    //map.mapTypes.set("styled_map", new google.maps.StyledMapType(styledMap, { name: "Styled" })); => Define and personalized map style
    map.setMapTypeId("satellite");

    window.google.maps.event.addListenerOnce(map, 'tilesloaded', function () { }); //LOAD THIS AFTER LOAD THE MAP
}

$(document).ready(function () {
    
});