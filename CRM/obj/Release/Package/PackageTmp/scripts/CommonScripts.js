fndata = {};
fntype = "";
var uploadidforProgressbar = "";
function AjaxCallSave(url, Contentdata)
{
    $.ajax({
        type: "POST",
        async: false,
        url: url,
        data: Contentdata,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            alert(data);
        },
        error:function(){
            alert("Something happen wrong!");
        }
    });

}
function callPage(url, pagetitle, parameter) {

    var d = {};
    if (parameter != "") {
        d = { id: parameter };
    }

    $(".container-fluid").hide();
    $(".page-loader-wrapper").show();
    $.ajax({
        url: url,
        contentType: 'application/html; charset=utf-8',
        type: 'GET',
        data: d,
        dataType: 'html',
        success: function (result) {
            $('#dvProductDetails').html(result);
            $('.page-title').html(pagetitle);
            $(".container-fluid").show();
            $(".page-loader-wrapper").hide();
        }
    });
}
function UploadFile(id, hdncntrl,type) {
    if (type != "") {
        if (!fileValidation(id, type)) {
            return;
        }
    }

    // Checking whether FormData is available in browser
    if (window.FormData !== undefined) {

        var fileUpload = $("#" + id).get(0);
        var files = fileUpload.files;

        // Create FormData object
        var fileData = new FormData();

        // Looping over all files and add it to FormData object
        for (var i = 0; i < files.length; i++) {
            fileData.append(files[i].name, files[i]);
        }

        // Adding one more key to FormData object
        //fileData.append('username', ‘Manas’);
        var loader = "<div class='imageloader'></div>";
        var progressbarhtml='<div class="progress"><div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%"></div></div>';
        //$("#" + id).parents(".file-upload").find(".error-message").html(loader);
        $("#" + id).parents(".file-upload").find(".error-message").html(progressbarhtml);
        uploadidforProgressbar = id;
        $.ajax({
            url: '/FileManager/UploadFiles',
            type: "POST",
            contentType: false, // Not to set any content header
            processData: false, // Not to process data
            data: fileData,
            success: function (result) {
                //alert("File uploaded successfully");
                $("#" + hdncntrl).val(result);
                if (id == "fileUploadFile_ProfilePhoto") {
                    $(".profilephoto").prop("src", "/Uploads/" + result);
                }
                    var soundsample = '<div class="soundsmaple" style="display:none"><audio controls=""><source src="/Uploads/' + result + '" type="audio/ogg"><source src="/Uploads/' + result + '" type="audio/mpeg">Your browser does not support the audio element.</audio></div>';
                    $("#" + id).parents(".file-upload").find(".error-message").html('');
                    var html = '<div class="' + id + '_addedFile"><span class="files" data-savedfilename="' + result + '">' + files[0].name + '</span>' +
                        soundsample +
                        '<span class="remove" data-hdncntrl="' + hdncntrl + '" data-savedfilename="' + result + '" onclick="removethis(this)">x</span>';
                    
                
                    if (id == "fu_Script_ProductServiceImages") {
                        $("#" + id).parents(".file-upload").find(".filename").append(html);
                        var ids = "";
                        //$(".fu_Script_ProductServiceImages_addedFile").each(function () {
                        //    ids += $(this).find(".files").attr("data-savedfilename")+",";
                        //});
                        $(".fu_Script_ProductServiceImages_addedFile").find(".files").each(function () {
                            ids += $(this).attr("data-savedfilename") + ",";
                        });
                         
                        $("#" + hdncntrl).val(ids);
                    }
                    else if (id == "fu_feedbackfile") {
                        $("#" + id).parents(".file-upload").find(".filename").append(html);
                        var ids = "";
                        $(".fu_feedbackfile_addedFile").each(function () {
                            ids += $(this).find(".files").attr("data-savedfilename") + ",";
                        });
                        $("#" + hdncntrl).val(ids);
                    }
                    else {
                        $("#" + id).parents(".file-upload").find(".filename").html(html);
                    }
                    $("#" + id).val('');
                    
                    var allowedExtensions = ".mp3|.wav|.MP3|.WAV";
                    var regex = new RegExp("(.*?)\.(" + allowedExtensions + ")$");
                    if (regex.test(result))
                    {
                        $("#" + id).parents(".file-upload").find(".error-message").html(soundsample);
                        $("#" + id).parents(".file-upload").find(".filename").find(".soundsmaple").show();
                    }

            },
            error: function (err) {
                console.log(err.statusText);
                $("#" + id).parents(".file-upload").find(".error-message").html(err.statusText);
            },
            xhr: function () {  // Custom XMLHttpRequest
                var myXhr = $.ajaxSettings.xhr();
                if (myXhr.upload) { // Check if upload property exists
                    //myXhr.upload.onprogress = progressHandlingFunction
                    myXhr.upload.addEventListener('progress', progressHandlingFunction,false); // For handling the progress of the upload
                }
                return myXhr;
            },
        });
    } else {
       alert("FormData is not supported.");
    }

}
function progressHandlingFunction(e) {
    if (e.lengthComputable) {
        var percentComplete = Math.round(e.loaded * 100 / e.total);
        //console.log(percentComplete);
        $("#" + uploadidforProgressbar).parents(".file-upload").find(".error-message").find(".progress").find(".progress-bar").css("width", percentComplete + '%').attr("aria-valuenow", percentComplete);
    }
    else {
        $('#FileProgress span').text('unable to compute');
    }
}
function removethis(id) {
    $(id).parent("div").remove();
    var hdncntrl = $(id).attr("data-hdncntrl");
    var savedfilename=$(id).attr("data-savedfilename");
    $("#" + hdncntrl).val('');
    /*if (id == "fu_Script_ProductServiceImages") {
        $("#" + id).parents(".file-upload").find(".filename").append(html);
        var ids = "";
        $(".fu_Script_ProductServiceImages_addedFile").each(function () {
            ids += $(this).find(".files").attr("data-savedfilename") + ",";
        });
        $("#" + hdncntrl).val(ids);
    }*/
    if ($(id).parent(".fu_feedbackfile_addedFile").length >= 1) {
        var ids = "";
        $(".fu_feedbackfile_addedFile").each(function () {
            ids += $(this).find(".files").attr("data-savedfilename") + ",";
        });
        $("#" + hdncntrl).val(ids);
    }
    if (savedfilename != "") {
        DeleteUploadedFile(savedfilename);
    }
}
function fileValidation(file, type) {
    $("#" + file).parents(".file-upload").find(".error-message").html('');
    var filesize = $("#" + file).attr("data-filesize");
    if (filesize != undefined && filesize != null && filesize != "") {
        var fi = document.getElementById(file);
        var fsize = fi.files.item(0).size;
        var fs = Math.round((fsize / 1024));
        if (fs > parseInt(filesize.replace("kb", ""))) {
            $("#" + file).parents(".file-upload").find(".error-message").html('File size should be less than ' + filesize);
            alert('File size should be less than ' + filesize);
            return false;
        }

    }
    var fileInput = document.getElementById(file);
    var filePath = fileInput.value;
    if (filePath != null) {
        // Allowing file type
        var allowedExtensions = "";
        var extravalidation = "";
        if (type.indexOf(",") > 0) {
            allowedExtensions = type.split(",")[0];
            extravalidation = type.split(",")[1];
            type = allowedExtensions;
        }
        if (type == "all") {
            return true;
        }

        if (type == "image") {
            
            allowedExtensions = "jpg|jpeg|png|JPG|JPEG|PNG";
            if (extravalidation != "") {
                allowedExtensions = allowedExtensions + "|" + extravalidation;
            }
        }
        else if (type == "file") {
            allowedExtensions = "docx|doc";
        }
        else if (type == "video") {
            allowedExtensions = ".mp4";
        }
        else if (type = "audio") {
            allowedExtensions = ".mp3";
        }
        if (allowedExtensions != "") {
            regex = new RegExp("(.*?)\.(" + allowedExtensions + ")$");
            if (!(regex.test(filePath))) {
                if (type == "image") {
                    allowedExtensions = ".jpg, .jpeg, .png";
                    if (extravalidation != "") {
                        allowedExtensions = allowedExtensions + " or ." + extravalidation;
                    }
                }
                //popupV('Invalid file type, file should be ' + allowedExtensions.replace("|", ",")+'', "Warning", "warning");
                var html = 'Invalid file type, file should be ' + allowedExtensions.replace("|", ",");
                $("#" + file).parents(".file-upload").find(".error-message").html(html);
                fileInput.value = '';
                return false;
            }
            else {
                return true;
            }
        }
        else {
            //alert('Invalid file type');
            //popupV("Invalid file type","Warning","warning");
            var html = "Invalid file type";
            $("#" + file).parents(".file-upload").find(".error-message").html(html);
            fileInput.value = '';
            return false;
        }
    }
    else {
        return false;
    }
}
function DeleteUploadedFile(filename) {
    var data = { FileName: filename };
    AjaxCall("FileManager", "DeleteUploadedFile", data, false);
}
function DownloadFile() {
    $(".downloadfile").click(function () {
        var url = $(this).attr("data-url");
        $("#link").click(function (e) {
            e.preventDefault();

            window.location.href = url;
        });
    });

}
function AjaxCall(Controller, method, Contentdata, isviewpopup) {
    $.ajax({
        type: "POST",
        url: "/" + Controller + "/" + method,
        data: JSON.stringify(Contentdata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if (isviewpopup == true) {
                var title = "Just To Know";
                if (data.indexOf("^") > -1) {
                    title = data.split("^")[0];
                    data = data.split("^")[1];
                }

                if (method == 'InActiveUser') {
                    
                    if (CurrentUser == 'Customer') {
                        setUser('Customer'); callPage('/UserDetail/UsersList', 'Customer', '4')
                    }
                    if (CurrentUser == 'ScriptWriter') {
                        setUser('ScriptWriter'); callPage('/UserDetail/UsersList', 'Script Writer', '5');
                    }
                    if (CurrentUser == 'VoiceArtist') {
                        setUser('VoiceArtist'); callPage('/UserDetail/UsersList', 'Voice Over', '6')
                    }
                    if (CurrentUser == 'VideoArtist') {
                        setUser('VideoArtist'); callPage('/UserDetail/UsersList', 'Animation', '7')
                    }
                    if (CurrentUser == 'Manager') {
                        setUser('Manager'); callPage('/UserDetail/UsersList', 'Managers', '8')
                    }

                }
                popupV1(data, title, "success");
            }
            else {
                if (method == "CheckPassword") {
                    if (data == "valid") {
                        $("#NewPassword").attr("disabled", false);
                        $("#ConfirmPassword").attr("disabled", false);
                    }
                }
                return data;
            }

            
        }
    });

}
function AjaxCallHtml(Controller, method, Contentdata, title) {
    $(".page-loader-wrapper").show();
    $.ajax({
        url: "/"+Controller+"/"+method,
        contentType: 'application/html; charset=utf-8',
        type: 'GET',
        data: Contentdata,
        dataType: 'html',
        success: function (result) {
            popupV(result, title, "");
            $(".page-loader-wrapper").hide();
        }
    });

}
function AjaxCallHtml1(Controller, method, Contentdata, title) {
    $(".page-loader-wrapper").show();
    $.ajax({
        url: "/" + Controller + "/" + method,
        contentType: 'application/html; charset=utf-8',
        type: 'GET',
        data: Contentdata,
        dataType: 'html',
        success: function (result) {
            popupV1(result, title, "");
            $(".page-loader-wrapper").hide();
        }
    });

}
function AjaxCallHtml2(Controller, method, Contentdata, title) {
    $(".page-loader-wrapper").show();
    $.ajax({
        url: "/" + Controller + "/" + method,
        contentType: 'application/html; charset=utf-8',
        type: 'GET',
        data: Contentdata,
        dataType: 'html',
        success: function (result) {
            popupV2(result, title, "");
            $(".page-loader-wrapper").hide();
        }
    });

}
function AjaxCallHtml3(Controller, method, Contentdata, title) {
    $(".page-loader-wrapper").show();
    $.ajax({
        url: "/" + Controller + "/" + method,
        contentType: 'application/html; charset=utf-8',
        type: 'GET',
        data: Contentdata,
        dataType: 'html',
        success: function (result) {
            popupV3(result, title, "");
            $(".page-loader-wrapper").hide();
        }
    });

}
function convertNumberToWords(amount) {
    var words = new Array();
    words[0] = '';
    words[1] = 'One';
    words[2] = 'Two';
    words[3] = 'Three';
    words[4] = 'Four';
    words[5] = 'Five';
    words[6] = 'Six';
    words[7] = 'Seven';
    words[8] = 'Eight';
    words[9] = 'Nine';
    words[10] = 'Ten';
    words[11] = 'Eleven';
    words[12] = 'Twelve';
    words[13] = 'Thirteen';
    words[14] = 'Fourteen';
    words[15] = 'Fifteen';
    words[16] = 'Sixteen';
    words[17] = 'Seventeen';
    words[18] = 'Eighteen';
    words[19] = 'Nineteen';
    words[20] = 'Twenty';
    words[30] = 'Thirty';
    words[40] = 'Forty';
    words[50] = 'Fifty';
    words[60] = 'Sixty';
    words[70] = 'Seventy';
    words[80] = 'Eighty';
    words[90] = 'Ninety';
    amount = amount.toString();
    var atemp = amount.split(".");
    var number = atemp[0].split(",").join("");
    var n_length = number.length;
    var words_string = "";
    if (n_length <= 9) {
        var n_array = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0);
        var received_n_array = new Array();
        for (var i = 0; i < n_length; i++) {
            received_n_array[i] = number.substr(i, 1);
        }
        for (var i = 9 - n_length, j = 0; i < 9; i++, j++) {
            n_array[i] = received_n_array[j];
        }
        for (var i = 0, j = 1; i < 9; i++, j++) {
            if (i == 0 || i == 2 || i == 4 || i == 7) {
                if (n_array[i] == 1) {
                    n_array[j] = 10 + parseInt(n_array[j]);
                    n_array[i] = 0;
                }
            }
        }
        value = "";
        for (var i = 0; i < 9; i++) {
            if (i == 0 || i == 2 || i == 4 || i == 7) {
                value = n_array[i] * 10;
            } else {
                value = n_array[i];
            }
            if (value != 0) {
                words_string += words[value] + " ";
            }
            if ((i == 1 && value != 0) || (i == 0 && value != 0 && n_array[i + 1] == 0)) {
                words_string += "Crores ";
            }
            if ((i == 3 && value != 0) || (i == 2 && value != 0 && n_array[i + 1] == 0)) {
                words_string += "Lakhs ";
            }
            if ((i == 5 && value != 0) || (i == 4 && value != 0 && n_array[i + 1] == 0)) {
                words_string += "Thousand ";
            }
            if (i == 6 && value != 0 && (n_array[i + 1] != 0 && n_array[i + 2] != 0)) {
                words_string += "Hundred and ";
            } else if (i == 6 && value != 0) {
                words_string += "Hundred ";
            }
        }
        words_string = words_string.split("  ").join(" ");
    }
    return words_string;
}
function pad(str, max) {
    str = str.toString();
    return str.length < max ? pad("0" + str, max) : str;
}
function popupV(html, title,type) {
    $("#viewpopup").find(".modal-body").html(html);
    $("#viewpopup").find(".feedbackPopup-heading").html(title);
    if ($("#viewpopupmodaltype").hasClass("modal-lg") == false) {
        $("#viewpopupmodaltype").addClass("modal-lg");
    }
    if (type == "success") {
        $("#viewpopupmodaltype").removeClass("modal-lg");
    }
    $("#viewpopup").modal();
}
function popupV1(html, title, type) {
    $("#viewpopup1").find(".modal-body-content").html(html);
    $("#viewpopup1").find(".feedbackPopup-heading").html(title);
    $("#viewpopup1").modal();
}
function popupV2(html, title, type) {
    $("#viewpopup2").find(".modal-body").html(html);
    $("#viewpopup2").find(".modal-title").html(title);
    $("#viewpopup2").modal();
}
function popupV3(html, title, type) {
    $("#viewpopup3").find(".modal-body").html(html);
    $("#viewpopup3").find(".modal-title").html(title);
    $("#viewpopup3").modal();
}
function popupCV(html, title, type,data) {
    $("#viewconfirmpopup").find(".modal-body-content").html(html);
    $("#viewconfirmpopup").find(".feedbackPopup-heading").html(title);
    $("#viewconfirmpopup").modal();
    fntype = type;
    fndata = data;    
}
function SendMail(data) {
  console.log(  AjaxCall("EmailManagement", "SendMail", data));
}
function TestMail() {
    var data =
        {
            EmailTo: $("#TestEmailAddress").val().trim(),
            EmailTemplate: $("#UniqueKey").val().trim()
        };
    if (data.EmailTo == "") {
        //popupV("Please enter email address");
        //return;
        data.EmailTo = "akhilesh.vis17@gmail.com";
    }
    SendMail(data);


}

function ValidationCheck(cntrl) {

    var type = $(cntrl).attr("data-validation");
    var value = $(cntrl).val();

    if (type == "username") {
        if (value.length > 0) {
            var pattern = (/(?<=[A-Z])[a-z]|(?<=[a-z])[A-Z]/);
            var regex_symbols = /[-!$%^&*()_+|~=`{}\[\]:\/;<>?,.@#]/;

            if (value.length < 8) {
                $("#msg1").addClass("red");
            }
            else {
                $("#msg1").addClass("green").removeClass("red");
            }
            if (pattern.test(value) === false) {

                $("#msg2").addClass("red");
            }
            else {
                $("#msg2").addClass("green").removeClass("red");
            }
            if (value.search(/[0-9]/) < 0) {
                $("#msg3").addClass("red");
            }
            else {
                $("#msg3").addClass("green").removeClass("red");
            }
            if (regex_symbols.test(value) === false) {
                $("#msg4").css("color", "red");
            }
            else {
                $("#msg4").css("color", "green").removeClass("red");
            }


        }
        else {
            $(".msg").css("color", "black");
        }

    }






}