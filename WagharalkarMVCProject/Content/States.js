$(document).ready(function () {
    /*getStatesList();*/
    getStatesListddl();
  
})

//var getStatesList = function () {
//    debugger;

//    $.ajax({
//        url: "/State/GetStatesList",
//        method: "POST",
//        contentType: "application/json;charset=utf-8",
//        //data: JSON.stringify(model),//model ne data waps yeil, no need to stringify data is in json format
//        datatype: "json",
//        async: false,
//        success: function (response) {

//            var html = "";
//            $("#tblStates tbody").empty();
//            $.each(response.model, function (index, elementValue) {

//                html += "<tr><td>" + elementValue.StateId + "</td><td>" + elementValue.StateName + "</td></tr>";
//            });

//            $("#tblStates tbody").append(html);

//        }
//    });
//}
var getStatesListddl = function () { 
    debugger;

    $.ajax({
        url: "/State/GetStatesList",
        method: "POST",
        contentType: "application/json;charset=utf-8",
        //data: JSON.stringify(model),//model ne data waps yeil, no need to stringify data is in json format
        datatype: "json",
        async: false,
        success: function (response) {

            var html = "";
            $("#ddlStates").empty();

            html += "<option value='0'>select State</option>"; ///changes 14/02/2024
            $.each(response.model, function (index, elementValue) {

                html += "<option value='" + elementValue.StateId + "'>" + elementValue.StateName + "</option>";
            });

            $("#ddlStates").append(html);

        }
    });
}