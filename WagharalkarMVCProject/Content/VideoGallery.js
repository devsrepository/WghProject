$(document).ready(function () {
    GetVideoGalleryList();
    GetListById();
})
var saveVideoGallery = function () {
    debugger;
    var id = $("#hdnId").val();
    var title = $("#txtTitle").val();
    var youtubeUrl = $("#txtYouTubeUrl").val();
    var type = $("#txtType").val();
    var createdate = $("#txtCreateDate").val(); 
    var updatedate = $("#txtUpdateDate").val();
    var createdby = $("#txtCreatedby").val(); 
    var updatedby = $("#txtUpdatedby").val();

   
    

    var model = {
        Id: id,
        Title: title, YouTubeUrl: youtubeUrl, Type: type, CreateDate: createdate, UpdateDate: updatedate,
        CreatedBy: createdby, UpdatedBy: updatedby
    };

    if (title == "") {
        alert("please enter Title");
    }
    if (youtubeUrl == "") {
        alert("please enter youtubeUrl");
    }
    if (type == "") {
        alert("please enter type");
    }
    else {

        $.ajax({
            url: "/VideoGallery/saveVideoGallery",
            method: "POST",
            data: JSON.stringify(model),
            contentType: "application/json;charset=utf-8",
            datatype: "json",
            async: false,
            success: function (response) {
                alert(response.model);
                GetVideoGalleryList();
            }
        });
    }
}

var GetVideoGalleryList = function () {
    debugger;
    $.ajax({
        url: "/VideoGallery/GetVideoGalleryList",
        method: "POST",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        async: false,
        success: function (response) {
            var html = "";
            $("#tblVideoGallery tbody").empty();
            //here response.model because we are returning the data in the model format from the controller
            $.each(response.model, function (index, elementValue) {
                html += "<tr><td>" + elementValue.Id + "</td><td>" + elementValue.Title + "</td><td>" +
                    elementValue.YouTubeUrl +  "</td><td>" + elementValue.Type +
                    "</td><td>" + elementValue.CreateDate + "</td><td>" +
                    elementValue.UpdateDate + "</td><td>" + elementValue.CreatedBy + "</td><td>"
                    + elementValue.UpdatedBy 
                    + "</td><td> <input type='button' id='btnDelete' value='Delete' onClick='DeleteRecord(" + elementValue.Id + ")'/></td><td><input type='button' id='btnEdit' value='Edit' onClick='EditRecord(" + elementValue.Id + ")'/></td><td><input type='button' id='btnDetails' value='Details' onClick='GetDetails(" + elementValue.Id + ")'/></td><td><input type='button' id='btnView' value='View' onClick='GetDetailIndex(" + elementValue.Id + ")'/> </td></tr>";

            });

            $("#tblVideoGallery tbody").append(html);
        }
    });
}

var DeleteRecord = function (id) {
    debugger;
    var model = { Id: id };
    $.ajax({
        url: "/VideoGallery/DeleteVideoGalleryRow",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            alert("row deleted successfully");
            GetVideoGalleryList();
            GetListById(); //for detailIndex page where page get list after click on delete button.
        }
    });
}

var EditRecord = function (id) {
    debugger;
    var model = { Id:id };
    $.ajax({
        url: "/VideoGallery/EditVideoGalleryRow",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            alert(response);
            console.log(response);
            $("#hdnId").val(response.model.Id);
            $("#txtTitle").val(response.model.Title);
            $("#txtYouTubeUrl").val(response.model.YouTubeUrl);
            $("#txtType").val(response.model.Type);
            $("#txtCreateDate").val(response.model.CreateDate);
            $("#txtUpdateDate").val(response.model.UpdateDate);
            $("#txtCreatedby").val(response.model.CreatedBy);
            $("#txtUpdatedby").val(response.model.UpdatedBy);
        }
    });
}

var GetDetails = function (id) {
    debugger;
    var model = { Id: id };
    $.ajax({
        url: "/VideoGallery/GetVideoGalleryList",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            //alert(response);
            //console.log(response);

            $('#CategoryModal').modal('show');

            $("#lblId").text(response.model.Id);
            $("#lblTitle").text(response.model.Title);
            $("#lblYouTubeUrl").text(response.model.YouTubeUrl);
            $("#lblType").text(response.model.Type);
            $("#lblCreateDate").text(response.model.CreateDate);
            $("#lblUpdateDate").text(response.model.UpdateDate);
            $("#lblCreatedby").text(response.model.CreatedBy);
            $("#lblUpdatedby").text(response.model.UpdatedBy);
        }
    });
}

var GetDetailIndex = function (Id) {
    debugger;
    window.location.href = "/VideoGallery/DetailIndex?id=" + Id;
}

var GetListById = function (id) {
    debugger;

    var id = $("#hdnId").val();
    var model = { Id: id };

    $.ajax({
        url: "/VideoGallery/GetListById",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {

            $('#CategoryModal').modal('show');

            var html = "";
            $("#tblVideoGallerylist tbody").empty();
            //here response.model because we are returning the data in the model format from the controller
            $.each(response.model, function (index, elementValue) {
                html += "<tr><td>" + elementValue.Id + "</td><td>" + elementValue.Title + "</td><td>" +
                    elementValue.YouTubeUrl + "</td><td>" + elementValue.Type +
                    "</td><td>" + elementValue.CreateDate + "</td><td>" +
                    elementValue.UpdateDate + "</td><td>" + elementValue.CreatedBy + "</td><td>"
                    + elementValue.UpdatedBy
                    + "</td><td> <input type='button' id='btnDelete' value='Delete' onClick='DeleteRecord(" + elementValue.Id + ")'/></td></tr>";

            });

            $("#tblVideoGallerylist tbody").append(html);
        }

    });
}
