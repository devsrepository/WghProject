$(document).ready(function () {
    GetAwardList();
    GetListById();
})

var saveAward = function () {
    debugger;
    $formData = new FormData();
    var image1 = document.getElementById('Image1');
    if (image1.files.length > 0) {
        for (var i = 0; i < image1.files.length; i++) {
            $formData.append('Image1-' + i, image1.files[i]);
        }
    }
    var image2 = document.getElementById('Image2');
    if (image2.files.length > 0) {
        for (var i = 0; i < image2.files.length; i++) {
            $formData.append('Image2-' + i, image2.files[i]);
        }
    }
    var id = $("#hdnId").val(); //this is added for edit
    var title = $("#txtTitle").val();
    var details = $("#txtDetails").val();
    //var image1 = $("#txtImage1").val();
   /* var image2 = $("#txtImage2").val();*/
    var type = $("#txtType").val();
    var date = $("#txtDate").val();
    var createdate = $("#txtCreateDate").val();
    var updatedate = $("#txtUpdateDate").val();
    var createdby = $("#txtCreatedby").val();
    var updatedby = $("#txtUpdatedby").val();
    /*//this is commented as data is sending to controller in $formData.*/
    //var model = {
    //    Id:id, //this is added for edit
    //    Title: title,
    //    Details: details,
    //    Image1: image1,
    //    Image2: image2,
    //    Type: type,
    //    Date: date,
    //    CreateDate: createdate,
    //    UpdateDate: updatedate,
    //    CreatedBy: createdby,
    //    UpdatedBy: updatedby
    //};

    $formData.append('Id', id);
    $formData.append('Title', title);
    $formData.append('Details', details);
    //$formData.append('Image1', image2)
    $formData.append('Type', type);
    $formData.append('Date', date);
    $formData.append('CreateDate', createdate);
    $formData.append('Updatedate', updatedate);
    $formData.append('CreatedBy', createdby);
    $formData.append('UpdatedBy', updatedby);

    $.ajax({
        url:"/Award/saveAward",
        method: "POST",
        contentType: false,
        /* data: JSON.stringify(model),*/
        data: $formData,
        /*datatype: "json",*/
        processData:false,
        success: function (response) {
            alert(response.model); // alert(response.Message) this change to alert(response.model)  for edit
            GetAwardList();//added for edit
        }

    });
}
var GetAwardList = function () {
    debugger;
    $.ajax({
        url: "/Award/GetAwardList",
        method: "POST",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        async: false,
        success: function (response) {
            var html = "";
            $("#tblAward tbody").empty();
            //here response.model because we are returning the data in the model format from the controller
            $.each(response.model, function (index, elementValue) {
                html += "<tr><td>" + elementValue.Id + "</td><td>" + elementValue.Title
                    + "</td><td>" + elementValue.Details 
                    + "</td><td><img src='../Content/img/" + elementValue.Image1 + "'style='height:80px; width:80px;'/></td><td>" 
                    + "<img src='../Content/img/" + elementValue.Image2 + "'style='height:80px; width:80px;'/></td><td>" 
                    + elementValue.Type +
                    "</td><td>" + elementValue.Date + "</td><td>" + elementValue.CreateDate + "</td><td>" +
                    elementValue.UpdateDate + "</td><td>" + elementValue.CreatedBy + "</td><td>"
                    + elementValue.UpdatedBy
                    + "</td><td> <input type='button' id='btnDelete' value='Delete' onClick='DeleteAward(" + elementValue.Id + ")'/></td><td><input type='button' id='btnEdit' value='Edit' onClick='EditRecord(" + elementValue.Id + ")' /></td><td><input type='button' id='btnDetails' value='Details' onClick='GetDetails(" + elementValue.Id + ")'/></td><td><input type='button' id='btnView' value='View' onClick='GetDetailIndex(" + elementValue.Id + ")'/></td></tr > ";

            });

            $("#tblAward tbody").append(html);
        }
    });
}

var DeleteAward = function (id) {
    debugger;
    var model = { Id: id };
    $.ajax({
        url: "/Award/DeleteAward",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            alert("record deleted successfully");
            GetAwardList();
        }
    });

}

var EditRecord = function (id) {
    debugger;
    var model = { Id: id };

    $.ajax({
        url: "/Award/EditAwardRow",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            $("#hdnId").val(response.model.Id); // make primarykey Id hidden
            $("#txtTitle").val(response.model.Title);
            $("#txtDetails").val(response.model.Details);
            $("#txtImage1").val(response.model.Image1);
            $("#txtImage2").val(response.model.Image2);
            $("#txtType").val(response.model.Type);
            $("#txtDate").val(response.model.Date);
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
        url: "/Award/EditAwardRow",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {

            $("#CategoryModal").modal('show');

            $("#lblId").text(response.model.Id); // make primarykey Id hidden
            $("#lblTitle").text(response.model.Title);
            $("#lblDetails").text(response.model.Details);
            $("#lblImage1").text(response.model.Image1);
            $("#lblImage2").text(response.model.Image2);
            $("#lblType").text(response.model.Type);
            $("#lblDate").text(response.model.Date);
            $("#lblCreateDate").text(response.model.CreateDate);
            $("#lblUpdateDate").text(response.model.UpdateDate);
            $("#lblCreatedby").text(response.model.CreatedBy);
            $("#lblUpdatedby").text(response.model.UpdatedBy);

        }
    });
}

var GetDetailIndex = function (Id) {
    debugger;
    window.location.href = "/Award/DetailIndex?id=" + Id;
}

var GetListById = function (id) {
    debugger;

    var id = $("#hdnId").val();
    var model = { Id: id };

    $.ajax({
        url: "/Award/GetListById",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            $("#CategoryModal").modal('show');
            var html = "";

            
            $("#tblAwardsLists tbody").empty();

            $.each(response.model, function (index, elementValue) {
                html += "<tr><td>" + elementValue.Id + "</td><td>" + elementValue.Title
                    + "</td><td>" + elementValue.Details + "</td><td>" + elementValue.Image1 +
                    "</td><td>" + elementValue.Image2 + "</td><td>" + elementValue.Type
                    + "</td><td>" + elementValue.Date + "</td><td>" + elementValue.CreateDate
                    + "</td><td>" + elementValue.UpdateDate + "</td><td>" + elementValue.CreatedBy
                    + "</td><td>" + elementValue.UpdatedBy +
                    "</td><td><input type='button' id='btnDelete' value='Delete' onClick='DeleteAward(" + elementValue.Id + ")'/></td></tr>"

            });

            $("#tblAwardsLists tbody").append(html);

        }
    });
}
