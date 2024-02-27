$(document).ready(function () {
    GetCityList();

})

var GetCityList = function () {
    debugger;
    $.ajax({
        url: "/City/GetCityList?StateId=" + $("#ddlStates").val(), //changes 14/02/2024
        method: "POST",
        contentType: "application/json;charset=utf-8",
        //data: JSON.stringify(model),//model ne data waps yeil, no need to stringify data is in json format
        dataType: "json",
        async: false,
        success: function (response) {
            /*$("#CategoryModal").modal('show');*/
            var html = "";

            $("#ddlCities").empty();
              html += "<option value='0'>select City</option>";
            $.each(response.model, function (index, elementValue) {

               html+="<option value='"+elementValue.CityId +"'>"+elementValue.CityName +"</option>"


            });

            $("#ddlCities").append(html);

        }
    });

}
