$(document).ready(function () {
    GetAppointment();
    GetListById();
});
var saveAppointment = function () {
    debugger;
    var id = $("#hdnId").val() //this is added after edit the row
    var name = $("#txtName").val();
    var email = $("#txtEmail").val(); 
    var city = $("#ddlCity").val();
    var mobileno = $("#txtMobileNo").val();
    var appointmentDate = $("#txtAppointmentDate").val();
    var gender = $("#txtGender").val(); 
    var message = $("#txtMessage").val(); 
    var createdate = $("#dtCreateDate").val();
    var updatedate = $("#dtUpdateDate").val();
    var createdby = $("#txtCreatedby").val();
    var updatedby = $("#txtUpdatedby").val();
    var state = $("#ddlStates").val(); //added for onchange
   
    var model = {
        Id: id, //this is added after edit the row
        Name: name,
        Email: email,
        City: city, MobileNo: mobileno, AppointmentDate: appointmentDate,
        Gender: gender, Message: message, CreateDate: createdate, UpdateDate: updatedate,
        CreatedBy: createdby, UpdatedBy: updatedby, State: state
    };
    if (name == "") {
        debugger;
        /* alert("please enter Mobile");*/
        $("#spnName").text("please enter your valid mobile number");
        $("#txtName").focus();
        return false;
    }
    else {
        $.ajax({
            url: "/Appointment/saveAppointment",
            method: "POST",
            contentType: "application/json;charset=utf-8",
            data: JSON.stringify(model),
            datatype: "json",
            success: function (response) {
                alert(response.model);
                GetAppointment();
            },
            error: function () {
                alert("error occured");
            }
        });
    }
    
}

var GetAppointment = function () {
    $.ajax({
        url: "/Appointment/GetAppointmentList",
        method: "POST",
        contentType: "application/json;charset=utf-8",
        datatype: "json",
        async: false,
        success: function (response) {

            var html = "";
            $("#tblAppointment tbody").empty();

            $.each(response.model, function (index, elementValue) {

                html += "<tr><td>" + elementValue.Id + "</td><td>"
                    + elementValue.Name + "</td><td>" + elementValue.Email +
                    "</td><td>" +
                    elementValue.CityName + "</td><td>" + elementValue.MobileNo + "</td><td>"
                    + elementValue.AppointmentDate +
                    "</td><td>" + elementValue.Gender + "</td><td>" + elementValue.Message +
                    "</td><td>" + elementValue.CreateDate +
                    "</td><td>" + elementValue.UpdateDate + "</td><td>" + elementValue.CreatedBy +
                    "</td><td>" + elementValue.UpdatedBy +
                    "</td><td>" +elementValue.StateName +
                    "</td><td><input type='button' id='btnDelete' value='Delete' onClick='DeleteRecord(" + elementValue.Id + ")'/></td><td><input type='button' id='btnEdit' value='Edit' onClick='EditRecord(" + elementValue.Id + ")'/></td><td><input type='button' id='btnDetails' value='Details' onClick='GetDetails(" + elementValue.Id + ")'/></td><td><input type='button' id='btnView' value='View' onClick='GetDetailIndex(" + elementValue.Id + ")'/></td></tr>";

                
            });

            $("#tblAppointment tbody").append(html);
        }
    }); 
}

var DeleteRecord = function (id) {
    debugger;
    var model = { Id: id };
    $.ajax({
        url: "/Appointment/DeleteAppointmentRow",
        method: "POST",
        data: JSON.stringify(model),
        dataType: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            alert("record deleted successfully");
            GetAppointment();
        }

    });
}

var EditRecord = function (id) {
    debugger;

    var model = { Id: id };

    $.ajax({
        url: "/Appointment/EditAppointmentRow",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            $("#hdnId").val(response.model.Id); //here we are making primary key id hidden as we are going to edit the rows data
            $("#txtName").val(response.model.Name);
            $("#txtEmail").val(response.model.Email);
            $("#ddlCity").val(response.model.City);
            $("#txtMobileNo").val(response.model.MobileNo);
            $("#dtAppointmentDate").val(response.model.AppointmentDate);
            $("#txtGender").val(response.model.Gender);
            $("#txtMessage").val(response.model.Message);
            $("#dtCreateDate").val(response.model.CreateDate);
            $("#dtUpdateDate").val(response.model.UpdateDate);
            $("#txtCreatedby").val(response.model.CreatedBy);
            $("#txtUpdatedby").val(response.model.UpdatedBy);
            $("#ddlStates").val(response.model.State);


        }



    });

}

var ClearData = function () {
    $("#txtName").val("");
    $("#txtEmail").val("");
    $("#ddlCity").val("");
    $("#txtMobileNo").val("");
    $("#dtAppointmentDate").val("");
    $("#txtGender").val("");
    $("#txtMessage").val("");
    $("#dtCreateDate").val("");
    $("#dtUpdateDate").val("");
    $("#txtCreatedby").val("");
    $("#txtUpdatedby").val("");


}

var GetDetails = function (id) {
    debugger;
    var model = { Id: id };
    $.ajax({
        url: "/Appointment/EditAppointmentRow",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,
        success: function (response) {
            debugger;
            $("#CategoryModal").modal('show');
            $("#tblAppointment").empty();
            // to show the popup on details button.

            $("#lblId").text(response.model.Id); //here we are making primary key id hidden as we are going to edit the rows data
            $("#lblName").text(response.model.Name); //here we are replacing val to text bcoz , we want to display texxt of response 
            $("#lblEmail").text(response.model.Email);//to label only
            $("#lblCity").text(response.model.City);
            $("#lblMobileNo").text(response.model.MobileNo);
            $("#lblAppointmentDate").text (response.model.AppointmentDate);
            $("#lblGender").text(response.model.Gender);
            $("#lblMessage").text(response.model.Message);
            $("#lblCreateDate").text(response.model.CreateDate);
            $("#lblUpdateDate").text(response.model.UpdateDate);
            $("#lblCreatedby").text(response.model.CreatedBy);
            $("#lblUpdatedby").text(response.model.UpdatedBy);
            $("#lblState").text(response.model.State);

        }
    });
}

var GetDetailIndex = function (Id) {
    debugger;

    window.location.href ="/Appointment/DetailIndex?id=" + Id;
}

var GetListById = function (id) {
    debugger;

    var id = $("#hdnId").val();
   var model = { Id: id };

    $.ajax({
        url: "/Appointment/GetListById",
        method: "POST",
        data: JSON.stringify(model),
        datatype: "json",
        contentType: "application/json;charset=utf-8",
        async: false,

        success: function (response) {
            debugger;

            var html = "";
            $("#CategoryModal").modal('show');
            $("tblAppointmentRowbyId tbody").empty();

            $.each(response.model, function (index, elementValue) {

                html += "<tr><td>" + elementValue.Id + "</td><td>"
                    + elementValue.Name + "</td><td>" + elementValue.Email +
                    "</td><td>" +
                    elementValue.City + "</td><td>" + elementValue.MobileNo + "</td><td>"
                    + elementValue.AppointmentDate +
                    "</td><td>" + elementValue.Gender + "</td><td>" + elementValue.Message +
                    "</td><td>" + elementValue.CreateDate +
                    "</td><td>" + elementValue.UpdateDate + "</td><td>" + elementValue.CreatedBy +
                    "</td><td>" + elementValue.UpdatedBy + "</td><td>" + elementValue.State +
                   
                    "</td><td><input type='button' id='btnDelete' value='Delete' onClick='DeleteRecord(" + elementValue.Id + ")'/></td></tr>";

            });

            $("tblAppointmentRowbyId tbody").append(html);

            //$("#lblHdnId").val(response.model.Id);
            //$("#lblName").val(response.model.Name);
            //$("#lblEmail").val(response.model.Email);
            //$("#lblCity").val(response.model.City);
        }


    });
}
//var AppointmentDetail = function (Id) {
//    debugger;
//    var model = { Id: Id }
//    $.ajax({
//        url: "/Appointment/AppointmentDetails",
//        method: "post",
//        data: JSON.stringify(model),
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        async: false,
//        success: function (response) {
//            $("#CategoryModal").modal('show');

//            $("#DetailCategory").empty();

//            var html = "";
//            html += "<div class='row'>";
//            html += "<div class='col-sm-6'>";
//            html += "<b>Id:</b>&nbsp&nbsp&nbsp<span>" + response.model.Id + "</span>";
//            html += "</br>";
//            html += "<b>CategoryName:</b>&nbsp&nbsp&nbsp<span>" + response.model.CategoryName + "</span>";
//            html += "</br>";
//            html += "<b>CategoryDetails:</b>&nbsp&nbsp&nbsp<span>" + response.model.CategoryDetails + "</span>";
//            html += "</br>";
//            html += "<b>IsAvailable:</b>&nbsp&nbsp&nbsp<span>" + response.model.IsAvailable + "</span>";
//            html += "</br>";
//            html += "<b>CreatedBy:</b>&nbsp&nbsp&nbsp<span>" + response.model.CreatedBy + "</span>";
//            html += "</br>";
//            html += "<b>CreatedDate:</b>&nbsp&nbsp&nbsp<span>" + response.model.CreatedDate + "</span>";
//            html += "</br>";
//            html += "</div>";
//            html += "<div class='col-sm-6'>";
//            html += "<b>UpdatedBy:</b>&nbsp&nbsp&nbsp<span>" + response.model.UpdatedBy + "</span>";
//            html += "</br>";
//            html += "<b>UpdatedDate:</b>&nbsp&nbsp&nbsp<span>" + response.model.UpdatedDate + "</span>";
//            html += "</br>";
//            html += "</div>";
//            html += "</div>";

//            $("#DetailCategory").append(html);
//        }
//    });
//};