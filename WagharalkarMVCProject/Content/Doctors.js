$(document).ready(function () {
    GetDoctorsList();
    GetDoctorsListBootstrap();
});

var saveDoctors = function () {
    debugger;
    var $formData = new FormData();

    var image = document.getElementById('Image1');
    if (image.files.length > 0) {
        for (var i = 0; i < image.files.length; i++) {
            $formData.append('Image-' + i, image.files[i]);
        }
    }
    var id = $('#Hid').val();
    var name = $('#txtName').val();
    var designation = $('#txtDesignation').val();
    var description = $('#txtDescription').val();
    var education = $('#txtEducation').val();
    var instagramLink = $('#txtInstagramLink').val();
    var facebookLink = $('#txtFacebookLink').val();
    var linkedinLink = $('#txtLinkedinLink').val();
    //var model = {
    //    Id:id,
    //    Name: name,
    //    Designation: designation,
    //    Description: description,
    //    Education: education,
    //    InstagramLink: instagramLink,
    //    FacebookLink: facebookLink,
    //    LinkedinLink: linkedinLink,
       
    //};
    $formData.append('Id', id);
    $formData.append('Name', name);
    $formData.append('Designation', designation);
    $formData.append('Description', description);
    $formData.append('Education', education);
    $formData.append('InstagramLink', instagramLink);
    $formData.append('FacebookLink', facebookLink);
    $formData.append('LinkedinLink', linkedinLink);

    $.ajax({
        url: "/Doctors/SaveDoctors",
        method: "POST",
        contentType: false,
        data: $formData,
        processData: false,
        success: function (response) {
            alert();
            GetDoctorsList();
        }


    });

}

var GetDoctorsList = function () {
    debugger;
    $.ajax({
        url: "/Doctors/GetDoctorsList",
        method: "POST",
        contentType: "application/json;charset=utf-8",
        //data: JSON.stringify(model),//model ne data waps yeil, no need to stringify data is in json format
        dataType: "json",
        async: false,
        success: function (response) {
            $("#CategoryModal").modal('show');
            var html = "";

            $("#tblDoctors tbody").empty();
            
            $.each(response.model, function (index, elementValue) {

                html += "<tr><td>" + elementValue.Id + "</td><td>" + elementValue.Name + "</td><td><img src='../Content/img/" + elementValue.Image + " 'style='height:80px; width:80px;'/></td><td>"
                    + elementValue.Designation
                    + "</td><td>" + elementValue.Description
                    + "</td><td>" + elementValue.Education
                    + "</td><td>" + elementValue.InstaGramLink
                    + "</td><td>" + elementValue.FacebookLink
                    + "</td><td>" + elementValue.LinkedInLink + "</td></tr>";


            });

            $("#tblDoctors tbody").append(html);
            
        }
    });

}

var GetDoctorsListBootstrap = function () {
    debugger;
    $.ajax({
        url: "/Doctors/GetDoctorsList",
        method: "POST",
        contentType: "application/json;charset=utf-8",
        //data: JSON.stringify(model),//model ne data waps yeil, no need to stringify data is in json format
        dataType: "json",
        async: false,
        success: function (response) {
            
            var html = "";

            $("#DoctorsList").empty();

            $.each(response.model, function (index, elementValue) {
                html += " <div class='col-lg-6'>";
                html += "<div class='member d-flex align-items-start''>";
                html += "<div class='pic'><img src='../Content/img/" + elementValue.Image + " 'style='height:80px; width:80px;'/></div>";
                html += "<div class='member-info'>";
                html += "<h4>" + elementValue.Name + "</h4>";
                html += "<span>" + elementValue.Designation + "</span>";
                html += "<p>" + elementValue.Description + "</p>";
                html += "<div class='social'>";
                html += "<a href=''><i class='ri-twitter-fill'> " + elementValue.InstaGramLink + "</i ></a > ";
                html += "<a href=''><i class='ri-twitter-fill'> " + elementValue.FacebookLink + "</i ></a > ";
                html += "<a href=''><i class='ri-twitter-fill'> " + elementValue.LinkedInLink + "</i ></a > ";
                html += "</div>";
                html += "</div>";
                html += "</div>";



            });

            $("#DoctorsList").append(html);

        }
    });

}