$(document).ready(function () {
    GetPhotoGalleryList();
    GetListById();
})
var savePhotoGallery = function () {
    debugger;
    //2.here forms field data taken to the variable.
    var $formData = new FormData();
    var image1 = document.getElementById('Image1');
    if (image1.files.length > 0) {
        for (var i = 0; i < image1.files.length; i++)
        {
            $formData.append('Image1-' + i, image1.files[i]);
        }
    }

    var image2 = document.getElementById('Image2');
    if (image2.files.length > 0) {
        for (var i = 0; i < image2.files.length; i++) {
            $formData.append('Image2-' + i, image2.files[i]);
        }
    }
    var id = $("#hdnId").val();
    var title = $("#txtTitle").val();
    //var image1 = $("#txtImage1").val();
    //var image2 = $("#txtImage2").val();
    var type = $("#txtType").val();
    var createdate = $("#txtCreateDate").val();
    var updatedate = $("#txtUpdateDate").val();
    var createdby = $("#txtCreatedby").val();
    var updatedby = $("#txtUpdatedby").val();

    //here model is not needed as commented. and send data in $formData
    //var model = {
    //    //3. here,forms field data binds to the models fields.
    //    //Title: title-->left Title is PhotoGallerymodel Field,right title is form field.
    //    Id:id,
    //    Title: title, Image1: image1, Image2: image2, Type: type, CreateDate: createdate,
    //    UpdateDate: updatedate, CreatedBy: createdby, UpdatedBy: updatedby
    //};

    $formData.append('Id', id);
    $formData.append('Title', title);
    $formData.append('Type', type);
    $formData.append('Title', title);
    $formData.append('CreateDate', createdate);
    $formData.append('UpdateDate', updatedate);
    $formData.append('CreatedBy', createdby);
    $formData.append('UpdatedBy', updatedby);

    //4.ajax call which send this data to the PhotogalleryController and its method savePhotoGallery
    $.ajax({
        url: "/PhotoGallery/savePhotoGallery",
        method: "POST",
        /* contentType: "application/json;charset=utf-8",*/
        contentType: false,
        /*data: JSON.stringify(model),*/
        data:$formData,
       /* datatype: "json",*/
        processData: false,
      /*  async:false,*/
        success: function (response) {
            alert(response.Message);
            GetPhotoGalleryList();
        }


    });
}

var GetPhotoGalleryList = function () {
    debugger;
    $.ajax({
        url: "/PhotoGallery/GetPhotoGalleryList",
        method: "POST",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        async: false,
        success: function (response) {
          /*  $('#CategoryModal').modal('show');*/

            var html = "";
            $("#tblPhotoGallery tbody").empty();
            //here response.model because we are returning the data in the model format from the controller
            $.each(response.model, function (index, elementValue) {
                html += "<tr><td>" + elementValue.Id + "</td><td>" + elementValue.Title 
                    + "</td><td><img src='../Content/img/" + elementValue.Image1 + " 'style='height:80px; width:80px;'/></td><td>" +
                    "<img src='../Content/img/" + elementValue.Image2 + "'style='height:80px; width:80px;'/></td><td>" + elementValue.Type +
                     "</td><td>" + elementValue.CreateDate + "</td><td>" +
                    elementValue.UpdateDate + "</td><td>" + elementValue.CreatedBy + "</td><td>"
                    + elementValue.UpdatedBy +
                    "</td><td> <input type='button' id='btnDelete' value='Delete' onClick='DeleteRecord(" + elementValue.Id + ")'/></td><td><input type='button' id='btnEdit' value='Edit' onClick='EditRecord(" + elementValue.Id + ")'/></td><td><input type='button' id='btnDetails' value='Details' onClick='GetDetails(" + elementValue.Id + ")'/></td><td><input type='button' id='btnView' value='View' onClick='GetDetailIndex(" + elementValue.Id + ")'/></td></tr>";

            });

            $("#tblPhotoGallery tbody").append(html);
        }
    });
}

var DeleteRecord = function (id) {
    debugger;
    var model = { Id: id };
    $.ajax({
        url: "/PhotoGallery/DeletePhotoGallery",
        method: "POST",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        async: false,
        success: function (response) {
            alert("record deleted successfully");
            GetPhotoGalleryList();
        }
    });
}

var GetDetails = function (id) {

    debugger;
    var model = { Id: id };

    $.ajax({
        url: "/PhotoGallery/EditPhotGalleryRow",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            $('#CategoryModal').modal('show');

            $("#lblId").text(response.model.Id);
            $("#lblTitle").text(response.model.Title);
            $("#lblImage1").text(response.model.Image1);
            $("#lblImage2").text(response.model.Image2);
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
    window.location.href = "/PhotoGallery/DetailIndex?id=" + Id;
}

var GetListById = function (id) {

    debugger;
    var id = $("#hdnId").val();
    var model = { Id: id };

    $.ajax({
        url: "/PhotoGallery/GetListById",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            $('#CategoryModal').modal('show');
            var html = "";
            $("#tblPhotoGallerylist tbody").empty();
            //here response.model because we are returning the data in the model format from the controller
            $.each(response.model, function (index, elementValue) {
                html += "<tr><td>" + elementValue.Id + "</td><td>" + elementValue.Title + "</td><td>" +
                    elementValue.Image1 + "</td><td>" + elementValue.Image2 + "</td><td>" + elementValue.Type +
                    "</td><td>" + elementValue.CreateDate + "</td><td>" +
                    elementValue.UpdateDate + "</td><td>" + elementValue.CreatedBy + "</td><td>"
                    + elementValue.UpdatedBy +
                    "</td><td> <input type='button' id='btnDelete' value='Delete' onClick='DeleteRecord(" + elementValue.Id + ")' /></td></tr>";

            });

            $("#tblPhotoGallerylist tbody").append(html);
        }
    });
}