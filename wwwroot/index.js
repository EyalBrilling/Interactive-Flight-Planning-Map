let planeIcon;
var map;
var clicked_flight;
let allFlightsdata = [];
let all_ids = [];
let flightLat = [];
let flightaLon = [];
var blueMarkers = [];
var coordinates = [];
var all_external = [];
let markers;
var theLine;
var mapMarkers = [];
var coordinate;
let formated_date = getFormattedDate();
var last_lon_lat;

var myLines = [{
    "type": "LineString",
    "coordinates": coordinates
}, {
    "type": "LineString",
    "coordinates": coordinates
}];
var myStyle = {
    "color": "#ff7800",
    "weight": 5,
    "opacity": 0.65
};


window.addEventListener('DOMContentLoaded', (event) => {
    map = L.map('map').setView([0, 0], 2);
    L.tileLayer('https://api.maptiler.com/maps/streets/{z}/{x}/{y}.png?key=2LNqDe6v0xBDFUL4TTOt', {
        attribution: '<a href="https://www.maptiler.com/copyright/" target="_blank">&copy; MapTiler</a> <a href="https://www.openstreetmap.org/copyright" target="_blank">&copy; OpenStreetMap contributors</a>'
    }).addTo(map);
    planeIcon = L.Icon.extend({
        options: {
            iconSize: [30, 30],
            iconAnchor: [0, 0],
            popupAnchor: [0, 0]
        }
    });
    map.on('click', addMarker);
    init();
   
});

//function external_flights() {
//    //extenal flights handler
//    $.ajax({
//        "url": "http://localhost:56774/api/servers",
//        "method": "GET"
//    }).done(function (response) {
//        for (var p = 0; p < response.length; p++) {
//            $.ajax({
//                "url": response[p].serverURL + "/api/Flights?relative_to=" + formated_date + "&sync_all",
//                "method": "GET"
//            }).done(function (response) {

//                console.log('external_flight: ' + response[p].longitude);

//            });
//        }
//    });

//}



function init() {
    
    let interval = setInterval(function () {

        for (var i = 0; i < this.mapMarkers.length; i++) {
            this.map.removeLayer(this.mapMarkers[i]);
        }
        //delete blue icons
        for (var k = 0; k < blueMarkers.length; k++) {
            this.map.removeLayer(this.blueMarkers[k]);
        }
        /// delete my lines
        
        blueMarkers = [];
        mapMarkers = [];
        all_external = [];
        //mapMarkers = [];
        $('#tbMyFlights').empty();
        let myFLight = '';
        $('#tbExternalFlights').empty();
        allFlightsdata = [];
        all_ids = [];
        formated_date = getFormattedDate();
        console.log('requested date: ' + formated_date);
        settings = {
            "url": "http://localhost:56774/api/Flights?relative_to=" + formated_date + "&sync_all",
            "method": "GET"
        };

        $.ajax(settings).done(function (response) {
            console.log(`test1: ` + response);
            if (!response) return;


            for (var j = 0; j < response.length; j++) {

                allFlightsdata.push(response[j]);
                try {

                    let blackIcon = new planeIcon({ iconUrl: 'airplane-icon.png' });
                    let blueIcon = new planeIcon({ iconUrl: 'airplane-blue-icon.png' });
                    let lat = response[j].latitude;
                    let lon = response[j].longitude;
                    //coordinates.push([lat, lon]);
                    //L.geoJSON(myLines, {
                    //    style: myStyle
                    //}).addTo(map);

                    flightLat.push.lat;
                    flightaLon.push.lon;
                    console.log('draw airplane at lon: ' + lon + ', lat: ' + lat);


                    if (response[j].flight_id == clicked_flight) {
                        let blueMarker = L.marker([lon, lat], { icon: blueIcon }).addTo(map);
                        blueMarkers.push(blueMarker);
                    } else {


                        let theMarker = L.marker([response[j].longitude, response[j].latitude], { icon: blackIcon, id: 'test' + i }).on('click', function () {
                            ///////////////////////////////////////////////////////////////  
                          

                            console.log('icon id: ' + this._leaflet_id);
                            //coordinates = [];
                            //coordinates = [];
                            //if (theLine)
                            // 
                            for (var i = 0; i < all_ids.length; i++) {
                                if (all_ids[i] == this._leaflet_id) {
                                    clicked_flight = allFlightsdata[i].flight_id;
                                    console.log('found requested id: ' + allFlightsdata[i]);
                                    document.getElementById('f_id').innerHTML = allFlightsdata[i].flight_id;
                                    document.getElementById('c_n').innerHTML = allFlightsdata[i].company_name;
                                    document.getElementById('no_p').innerHTML = allFlightsdata[i].passengers;
                                    document.getElementById('s_p').innerHTML = "Lon: " + allFlightsdata[i].longitude + " , Lat: " + allFlightsdata[i].latitude;
                                  

                                    var settings = {
                                        "url": "http://localhost:56774/api/FlightPlan/" + allFlightsdata[i].flight_id,
                                        "method": "GET"
                                    };

                                    $.ajax(settings).done(function (response) {
                                        console.log(response);
                                        last_lon_lat = response.segments[response.segments.length - 1];
                                        document.getElementById('e_p').innerHTML = "Lon: " + last_lon_lat.longitude + " , Lat: " + last_lon_lat.latitude;
                                        coordinates = [];
                                        for (var i = 0; i < response.segments.length; i++) {
                                            let lo = response.segments[i].longitude;
                                            let la = response.segments[i].latitude;
                                            coordinates.push([la, lo]);

                                        }

                                        myLines = [{
                                            "type": "LineString",
                                            "coordinates": coordinates
                                        }, {
                                            "type": "LineString",
                                            "coordinates": coordinates
                                        }];
                                        myStyle = {
                                            "color": "#4cd3c2",
                                            "weight": 5,
                                            "opacity": 0.65
                                        };

                                        
                                        
                                        
                                        // map.removeLayer(theLine);
                                        //var markers = L.markerClusterGroup();
                                        theLine = L.geoJSON(myLines, {
                                            style: myStyle//,
                                            //onEachFeature: function (feature, layer) {
                                            //    layer.myTag = "myGeoJSON"
                                            //}

                                        }).addTo(map);

                                        

                                    });
                                }
                            }
                            let blueMarker = L.marker([lon, lat], { icon: blueIcon }).addTo(map);
                            blueMarkers.push(blueMarker);
                            /////////////////////////////////////////////////

                        }).addTo(map);

                        ////////////////////////////////////////
                        mapMarkers.push(theMarker);
                        console.log('icon id verify: ' + theMarker._leaflet_id);
                        all_ids.push(theMarker._leaflet_id);
                    }
                } catch (err) {
                    console.error(err);
                }


                var table = document.getElementById('tbMyFlights');

                var rowLength = table.rows.length;

                for (var i = 0; i < rowLength; i += 1) {
                    var row = table.rows[i];
                    var cellLength = row.cells.length;
                    for (var y = 0; y < cellLength; y += 1) {
                        var cell = row.cells[y];
                        console.log('loop table test: ' + cell.innerHTML);
                        //do something with every cell here
                    }
                }


                let id = response[j].flight_id;



                if (response[j].is_external == false) {
                    if (clicked_flight == response[j].flight_id) {
                        myFLight = `<tr class="selected-row"><td><input type="button" class="button" id="` + response[j].flight_id + `" size="20" /></td>
                                                <td class="clickable">` + response[j].flight_id + `</td>
                                                <td class="clickable">` + response[j].company_name + `</td></tr>`;
                    } else {
                        myFLight = `<tr><td><input type="button" class="button" id="` + response[j].flight_id + `" size="20" /></td>
                                                <td class="clickable">` + response[j].flight_id + `</td>
                                                <td class="clickable">` + response[j].company_name + `</td></tr>`;
                    }

                    $('#tbMyFlights').append(myFLight);
                } else {
                    all_external.push(response[j]);

                    if (clicked_flight == response[j].flight_id) {
                        myFLight = `<tr class="selected-row"></td>
                                                <td class="clickable">` + response[j].flight_id + `</td>
                                                <td class="clickable">` + response[j].company_name + `</td> </tr>`;
                    } else {
                        myFLight = `<tr></td>
                                                <td class="clickable">` + response[j].flight_id + `</td>
                                                <td class="clickable">` + response[j].company_name + `</td></tr>`;
                    }



                    $('#tbExternalFlights').append(myFLight);


                }

                //if (f_btn.includes('button')) {
                //    deleteFlightById(f_id);
                //}


            }
        });



    }, 2000);
}


$(document).on("click", "input.button", function (e) {
    deleteFlightById(e.target.id);
});

//$(window).on("click", "input", function () {
//    alert('clicked!');
//});
//$("input").on('click',function (e) { // using the unique ID of the button
//    deleteFlightById(e.target.id);
//    //var t = 3;
//});

function addExternalFlight() {
    let root = document.getElementById('tbExternalFlights');
    let rows = root.getElementsByTagName('tr');
    let clone = cloneEl(rows[rows.length - 1]);
    root.appendChild(clone);
}


function fileUpload() {
    var fr = new FileReader();
    fr.onload = function () {
        var settings = {
            "url": "http://localhost:56774/api/FlightPlan",
            "method": "POST",
            "headers": {
                "Content-Type": "application/json"
            },
            // "data": JSON.stringify(fr.result)
            "data": fr.result
        };
        console.log('file content: ' + fr.result);
        $.ajax(settings).done(function (response) {
            console.log(response);
        });
    }

    fr.readAsText(document.getElementById('myfile').files[0]);
}


function reDrewRightTable() {
    $('#tbMyFlights').empty();

    for (var i = 0; i < allFlightsdata.length; i++) {
        let myFLight = `<tr><td><input type="button" class="button" size="20" /></td><td>` + allFlightsdata[i].flight_ID + `</td><td>` + allFlightsdata[i].company_Name + `</td>
                                          </tr>`;
        $('#tbMyFlights').append(myFLight);
    }
}

function addMarker() {
    reDrewRightTable();
    clicked_flight = '';

    document.getElementById('f_id').innerHTML = '';
    document.getElementById('c_n').innerHTML = '';
    document.getElementById('no_p').innerHTML = '';
    document.getElementById('s_p').innerHTML = '';
    document.getElementById('e_p').innerHTML = '';

    if (theLine) {
        map.removeLayer(theLine);
    }
}


var settings = {
    "url": "http://localhost:56774/api/Flights?relative_to=" + formated_date,
    "method": "GET"
};

function deleteFlightById(id) {
    var settings = {
        "url": "http://localhost:56774/api/FlightPlan/" + id,
        "method": "DELETE"
    };

    $.ajax(settings).done(function (response) {
        console.log('delete response: ' + response);
    });
}

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//right table event
$(document).on("click", "#tbMyFlights tr .clickable", function (e) {
    // alert(this.id);

    var row_index = $(this).parent().index();
    var col_index = $(this).index();

    $(this).parent().toggleClass('selected-row');
    var table = document.getElementById("tbMyFlights");
    var row = table.rows[row_index];
    var f_id = row.cells[1].innerHTML;
    var f_btn = row.cells[0].innerHTML;
    // deleteFlightById(cell);

    //if (f_btn.includes('button')) {
    //    deleteFlightById(f_id);
    //}

    clicked_flight = f_id;
    for (var i = 0; i < allFlightsdata.length; i++) {
        if (allFlightsdata[i].flight_id == f_id) {
            document.getElementById('f_id').innerHTML = allFlightsdata[i].flight_id;
            document.getElementById('c_n').innerHTML = allFlightsdata[i].company_name;
            document.getElementById('no_p').innerHTML = allFlightsdata[i].passengers;
            document.getElementById('s_p').innerHTML = "Lon: " + allFlightsdata[i].longitude + " , Lat: " + allFlightsdata[i].latitude;

            var settings = {
                "url": "http://localhost:56774/api/FlightPlan/" + allFlightsdata[i].flight_id,
                "method": "GET"
            };

            $.ajax(settings).done(function (response) {
                console.log(response);
                last_lon_lat = response.segments[response.segments.length - 1];
                document.getElementById('e_p').innerHTML = "Lon: " + last_lon_lat.longitude + " , Lat: " + last_lon_lat.latitude;
                coordinates = [];
                for (var i = 0; i < response.segments.length; i++) {
                    let lo = response.segments[i].longitude;
                    let la = response.segments[i].latitude;
                    coordinates.push([la, lo]);

                }

                myLines = [{
                    "type": "LineString",
                    "coordinates": coordinates
                }, {
                    "type": "LineString",
                    "coordinates": coordinates
                }];
                myStyle = {
                    "color": "#4cd3c2",
                    "weight": 5,
                    "opacity": 0.65
                };

                theLine = L.geoJSON(myLines, {
                    style: myStyle//,
                   

                }).addTo(map);



            });

        }

    }

    
        ///////////////////////////////////////////////////////////////
        //console.log('icon id: ' + this._leaflet_id);
        //coordinates = [];
        //coordinates = [];
        //if (theLine)
         
        //for (var i = 0; i < all_ids.length; i++) {
        //    if (all_ids[i] == this._leaflet_id) {
        //        clicked_flight = allFlightsdata[i].flight_id;
        //        console.log('found requested id: ' + allFlightsdata[i]);
        //        document.getElementById('f_id').innerHTML = allFlightsdata[i].flight_id;
        //        document.getElementById('c_n').innerHTML = allFlightsdata[i].company_name;
        //        document.getElementById('no_p').innerHTML = allFlightsdata[i].passengers;
        //        document.getElementById('s_p').innerHTML = "Lon: " + allFlightsdata[i].longitude + " , Lat: " + allFlightsdata[i].latitude;


                //var settings = {
                //    "url": "http://localhost:56774/api/FlightPlan/" + allFlightsdata[i].flight_id,
                //    "method": "GET"
                //};

        //        $.ajax(settings).done(function (response) {
        //            console.log(response);
        //            last_lon_lat = response.segments[response.segments.length - 1];
        //            document.getElementById('e_p').innerHTML = "Lon: " + last_lon_lat.longitude + " , Lat: " + last_lon_lat.latitude;
        //            coordinates = [];
        //            for (var i = 0; i < response.segments.length; i++) {
        //                let lo = response.segments[i].longitude;
        //                let la = response.segments[i].latitude;
        //                coordinates.push([la, lo]);

        //            }

        //            myLines = [{
        //                "type": "LineString",
        //                "coordinates": coordinates
        //            }, {
        //                "type": "LineString",
        //                "coordinates": coordinates
        //            }];
        //            myStyle = {
        //                "color": "#4cd3c2",
        //                "weight": 5,
        //                "opacity": 0.65
        //            };

        //            //map._panes.markerPane.remove();
        //            // if (theLine)
        //            //map._panes.markerPane.remove();

        //            // map.removeLayer(theLine);
        //            //var markers = L.markerClusterGroup();
        //            theLine = L.geoJSON(myLines, {
        //                style: myStyle//,
        //                //onEachFeature: function (feature, layer) {
        //                //    layer.myTag = "myGeoJSON"
        //                //}

        //            }).addTo(map);



        //        });
        //    }
        //}
        //let blueMarker = L.marker([lon, lat], { icon: blueIcon }).addTo(map);
        //blueMarkers.push(blueMarker);
        /////////////////////////////////////////////////


    //color airplane when click right table
    for (var i = 0; i < mapMarkers.length; i++) {

    }
    console.log('row: ' + row_index + ', col: ' + col_index + ', cell: ' + f_id);
});

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


$(document).on("click", "#tbExternalFlights tr .clickable", function (e) {
    // alert(this.id);

    var row_index = $(this).parent().index();
    var col_index = $(this).index();

    $(this).parent().toggleClass('selected-row');
    var table = document.getElementById("tbExternalFlights");
    var row = table.rows[row_index];
    var f_id = row.cells[0].innerHTML;
    //var f_btn = row.cells[0].innerHTML;
    // deleteFlightById(cell);

    //if (f_btn.includes('button')) {
    //    deleteFlightById(f_id);
    //}

    clicked_flight = f_id;
    for (var i = 0; i < allFlightsdata.length; i++) {
        if (allFlightsdata[i].flight_id == f_id) {
            document.getElementById('f_id').innerHTML = allFlightsdata[i].flight_id;
            document.getElementById('c_n').innerHTML = allFlightsdata[i].company_name;
            document.getElementById('no_p').innerHTML = allFlightsdata[i].passengers;
            document.getElementById('s_p').innerHTML = "Lon: " + allFlightsdata[i].longitude + " , Lat: " + allFlightsdata[i].latitude;
            document.getElementById('e_p').innerHTML = "Lon: " + last_lon_lat.longitude + " , Lat: " + last_lon_lat.latitude;

        }

    }
    
   
    console.log('row: ' + row_index + ', col: ' + col_index + ', cell: ' + f_id);
});

function getFormattedDate() {
    var result = '';
    var date = new Date();
    var day = date.getDay();
    var month = date.getMonth() + 1;
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var seconds = date.getSeconds();

    if (day < 10)
        day = '0' + day;
    if (month < 10)
        month = '0' + month;
    if (hours < 10)
        hours = '0' + hours;
    if (minutes < 10)
        minutes = '0' + minutes;
    if (seconds < 10)
        seconds = '0' + seconds;

    result = date.getFullYear() + '-' + month + '-' + day + 'T' + hours + ':' + minutes + ':' + seconds + 'Z';
    return result;
}


var removeMarkers = function () {
    map.eachLayer(function (layer) {

        if (layer.myTag && layer.myTag === "myGeoJSON") {
            map.removeLayer(layer)
        }

    });

}

function isExternal(flight) {
    for (var i = 0; i < all_external.length; i++)
        if (flight.flight_id == all_external[i].flight_id) return true;
    return false;
}