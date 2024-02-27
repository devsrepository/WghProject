$(document).ready(function () {
    getList();
   
    GetDetailsListById();
    getListBootstrap();
})
var saveActivity = function () {
    debugger;
    //for saving the image path.when image upload
    $formData = new FormData();
    //var image1 = $("#txtImage1").val();
    var image1 = document.getElementById('txtImage1');
    if (image1.files.length > 0) {
        for (var i = 0; i < image1.files.length; i++) {
            $formData.append('Image1-' + i, image1.files[i]);
        }
    }
    //var image2 = $("#txtImage2").val();
    //if (image2.files.length > 0) {
    //    for (var i = 0; i < image2.files.length; i++) {
    //        $formData.append('Image2-' + i, image2.files[i]);
    //    }
    //}
    var id = $("#HId").val();
    var title = $("#txtTitle").val();
    var details = $("#txtDetails").val();
    //var image1 = $("#txtImage1").val(); //commented as we are saving this image path to db
    var image2 = $("#txtImage2").val();
    var type = $("#txtType").val();
    var date = $("#txtDate").val();
    var createdate = $("#txtCreateDate").val();
    var updatedate = $("#txtUpdateDate").val();
    var createdby = $("#txtCreatedby").val();
    var updatedby = $("#txtUpdatedby").val();

    $formData.append('ID', id);
    $formData.append('Title', title);
    $formData.append('Details', details);
    $formData.append('Image2', image2);
    $formData.append('Type', type);
    $formData.append('Date', date);
    $formData.append('CreateDate', createdate);
    $formData.append('UpdateDate', updatedate);
    $formData.append('CreatedBy', createdby);
    $formData.append('UpdatedBy', updatedby);
    //var model is commented as we are sending data to controller in $formData.
    //var model = {
    //    ID:id,
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

    $.ajax({
        url: "/Activity/saveActivity",
        method: "POST",
        /*contentType: "application/json;charset=utf-8",*/
        contentType: false,
        /* data: JSON.stringify(model),*/
        data: $formData,
        /*datatype: "json",*/
        processData: false,
        success: function (response) {
           // location.reload(); // for image save
            alert(response.model);
            getList();
        }
    });


}
//3.
var getList = function () {
    $.ajax({
        url: "/Activity/GetActivityList",
        method: "POST",
        contentType: "application/json;charset=utf-8",
        //data: JSON.stringify(model),//model ne data waps yeil, no need to stringify data is in json format
        datatype: "json",
        async: false,
        success: function (response) {

            var html = "";

            $("#tblactivity tbody").empty();

            $.each(response.model, function (index, elementValue) {
                //elementValue--> this is used to fetch and display model value to table
                html += "<tr><td>" + elementValue.ID + "</td><td>" + elementValue.Title + "</td><td>" + elementValue.Details
                    + "</td><td><img src='../Content/img/" + elementValue.Image1 + " ' style='height:80px; width:80px;'/> </td><td>" + "<td></td><img src='../Content/img/" + elementValue.Image2 + "'style='height:80px; width:80px;'/></td><td>" + elementValue.Type + "</td><td>" + elementValue.Date
                    + "</td><td>" + elementValue.CreateDate + "</td><td>" + elementValue.UpdateDate + "</td><td>" + elementValue.CreatedBy + "</td><td>" + elementValue.UpdatedBy
                    + "</td><td> <input type='button' id='btnDelete' value='Delete' onClick='DeleteRecord(" + elementValue.ID + ")' /></td><td><input type='button' id='btnEdit' value='Edit' onClick='EditRecord(" + elementValue.ID + ")'/></td><td><input type='button' id='btnDetails' value='Details' onClick='GetDetails(" + elementValue.ID + ")'/></td><td><input type='button' id='btnDetailIndex' value='View' onClick='GetDetailIndex(" + elementValue.ID + ")'/> </td></tr>";



            }
            );
          
            $("#tblactivity tbody").append(html);
            
        }

        
    }); 


}

var getListBootstrap = function () {
    $.ajax({
        url: "/Activity/GetActivityList",
        method: "POST",
        contentType: "application/json;charset=utf-8",
        //data: JSON.stringify(model),//model ne data waps yeil, no need to stringify data is in json format
        datatype: "json",
        async: false,
        success: function (response) {


            var html = "";

            //$("#tblactivity tbody").empty();
            $("#activity").empty(); //remove tbody

            $.each(response.model, function (index, elementValue) {
               /* html += " <div class='row'>";*/
                html += "<div class='col-lg-4 col-md-6 d-flex align-items-stretch'>";
                html += "<div class='icon-box'>";
                html += "<div class='icon'><img src='../Content/img/" + elementValue.Image1 +" ' style='height:80px; width:80px;'/></div>";
                html += "<h4><a href=''>" + elementValue.Title + "</a></h4>";
                html += "<p>" + elementValue.Details + "</p>";   
                html += "</div>";
                html += "</div>";
               /* html += "</div>";*/

            }
            );

            $("#activity").append(html);

        }


    });


}

var DeleteRecord = function (id) {
    debugger;
    var model = { ID: id };
    $.ajax({
        url: "/Activity/DeleteActivity",
        method: "POST",
        contentType: "application/json;charset=utf-8",
        data: JSON.stringify(model),
        datatype: "json",
        async: false,
        success: function (response) {
            alert("row deleted successfully");
            getList();
        }
    });
}

var EditRecord = function (id) {
    debugger;

    var model = { ID: id };

    $.ajax({
        url: "/Activity/EditActivityRow",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            
            $("#HId").val(response.model.ID); // primary key ID -->
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

var ClearData = function () {
    $("#txtTitle").val("");
    $("#txtDetails").val("");
    $("#txtImage1").val("");
    $("#txtImage2").val("");
    $("#txtType").val("");


}

var GetDetails = function (id) {
    debugger;

    var model = { ID: id };

    $.ajax({
        url: "/Activity/EditActivityRow",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            
            $("#CategoryModal").modal('show');

            $("#lblId").text(response.model.ID); // primary key ID -->
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

var GetDetailIndex = function (ID) {
    debugger;

    window.location.href = "/Activity/DetailIndex?id="+ID;
}

var GetDetailsListById = function (id) {
    debugger;
    
    var id = $("#hdnId").val();
    var model = { ID: id };

    $.ajax({
        url: "/Activity/GetListById",
        method: "POST",
        data: JSON.stringify(model),
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        async: false,
        success: function (response) {
            debugger;
            $("#CategoryModal").modal('show');

            $("#tblactivitys tbody").empty();

            var html = "";

            
            $.each(response.model, function (index, elementValue) {
                //elementValue--> this is used to fetch and display model value to table
                html += "<tr><td>" + elementValue.ID + "</td><td>" + elementValue.Title + "</td><td>" + elementValue.Details
                    + "</td><td>" + elementValue.Image1 + "</td><td>" + elementValue.Image2 + "</td><td>" + elementValue.Type + "</td><td>" + elementValue.Date
                    + "</td><td>" + elementValue.CreateDate + "</td><td>" + elementValue.UpdateDate + "</td><td>" + elementValue.CreatedBy + "</td><td>" + elementValue.UpdatedBy
                    + "</td><td><input type='button' id='btnDelete' value='Delete' onClick='DeleteRecord(" + elementValue.ID + ")'/></td></tr>";

                //$("#HId").val(response.model.ID); // primary key ID -->
                //$("#lblTitle").val(response.model.Title);
                //$("#txtDetails").val(response.model.Details); //take lbl instead of textbox.
                //$("#txtImage1").val(response.model.Image1);
                //$("#txtImage2").val(response.model.Image2);
                //$("#txtType").val(response.model.Type);
                //$("#txtDate").val(response.model.Date);
                //$("#txtCreateDate").val(response.model.CreateDate);
                //$("#txtUpdateDate").val(response.model.UpdateDate);
                //$("#txtCreatedby").val(response.model.CreatedBy);
                //$("#txtUpdatedby").val(response.model.UpdatedBy);

            }
            );

            $("#tblactivitys tbody").append(html);

        }


    });


}

var SaveData = function () {
    debugger;
    $formData = new FormData();
    var Image = document.getElementById('file1');
    if (Image.files.length > 0) {
        for (var i = 0; i < Image.files.length; i++) {
            $formData.append('file1-' + i, Image.files[i]);
        }
    }
    var id = $("#hdnId").val();
    var FirstName = $("#txtfn").val();
    var LastName = $("#txtln").val();
    var Class = $("#txtclass").val();
    var Photo = $("#file1").val();
    var Address = $("#txtadd").val();

    $formData.append('Id', id);
    $formData.append('FirstName', FirstName);
    $formData.append('LastName', LastName);
    $formData.append('Class', Class);
    $formData.append('Photo', Photo);
    $formData.append('Address', Address);


    $.ajax({
        url: "/Student/SaveData",
        method: "post",
        data: $formData,
        contentType: false,
        processData: false,
        async: false,
        success: function (response) {
            location.reload();
            alert(response.message);
        },
    });
}