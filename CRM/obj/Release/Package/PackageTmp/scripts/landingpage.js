var UserProfile = null;
var isLoginWithGmail = false;
var isRegistrationbtnclicked = false;
$(document).ready(function () {
    
    try{

       
        //if (grecaptcha != undefined) {
        //    $(".loginmsg").css("margin-top", "92px");
        //}
        //else {
        //    $(".loginmsg").css("margin-top", "0px");
        //}
    }
    catch(err){
       // $(".loginmsg").css("margin-top", "0px");
    }
    
    
    $(".clickForgotPassword").click(function () {
        $(".divlogin").hide();
        $(".divforgotpassword").show();
        $(".titlelogin").text("RESET PASSWORD");
    });
    $(".clickbacktologin").click(function () {
        $(".divlogin").show();
        $(".divforgotpassword").hide();
        $(".titlelogin").text("LOGIN");
    });
    $(".btnResetPassword").click(function () {
        $(".msgForgotPassword").css("color", "#fcc102");
        var msg = "";
        var isvalid = true;
        var txtMobileNo = $("#txtMobileNo").val().trim();
        var txtForgotPasswordOTP=$("#txtForgotPasswordOTP").val().trim();
        var txtForgotPassword=$("#txtForgotPassword").val().trim();
        var txtForgotConfirmPassword=$("#txtForgotConfirmPassword").val().trim();

        if (txtMobileNo == "") {
            msg = "Please enter mobile no and send OTP";
            isvalid = false;
            $("#txtMobileNo").focus();
        }
        else if (txtForgotPasswordOTP == "") {
            msg = "Please enter OTP sent on your no. If OTP not recevied click on SENT OTP";
            isvalid = false;
            $("#txtForgotPasswordOTP").focus();
        }
        else if (txtForgotPassword=="")
        {
            msg = "Please enter New Password";
            isvalid = false;
            $("#txtForgotPassword").focus();
        }
        else if (txtForgotConfirmPassword == "") {
            msg = "Please enter Confirm New Password";
            isvalid = false;
            $("#txtForgotConfirmPassword").focus();
        }
        else if (txtForgotPassword != txtForgotConfirmPassword) {
            msg = "New Password and Confirm New Password not matched";
            isvalid = false;
            $("#txtForgotConfirmPassword").focus();
        }
        if (grecaptcha != undefined) {
            var rcres = grecaptcha.getResponse();
            if (rcres.length == 0) {
                msg = "Please verify CAPTCHA";
                isvalid = false;
                return;
            }
        }
        if (isvalid) {
            $(".msgForgotPassword").css("color", "white");
            msg = "Please wait";
            ResetPassword();
        }
        $(".msgForgotPassword").html(msg);


    });

    $('#txtRegMobileNo').keyup(function () {
        this.value = this.value.replace(/[^0-9\.]/g, '');
    });
    $(".childmenu").hide();
    $(".childmenuLogout").hide();
    
    //$("body").click(function () {
    //    $(".childmenu").hide();
    //});
    $("#aboutus").click(function () {
        $(".childmenu").toggle();
    });
    $("#logout").click(function () {
        $(".childmenuLogout").toggle();
    });
    $(".childmenu a").click(function () {
        $(".childmenu").hide();
    });
    $(".childmenuLogout a").click(function () {
        $(".childmenuLogout").hide();
    });

   
    //setInterval(function () {
        var width = $(document).width();
        if (width <= 426) {
            $(".mobilview").show();
            $(".menu-parent").hide();
            $(".navbar-nav").css("background-color", "black");

        }
        else {
            $(".mobilview").hide();
            $(".menu-parent").show();
            $(".navbar-nav").css("background-color", "transparent");
        }

    //}, 1000);
    

    $(".btnNewUser").click(function () {

        var FirstName = $("#txtFirstName").val().trim();
        var LastName = $("#txtLastName").val().trim();
        var UserName = $("#txtRegUserName").val().trim();
        var EmailID = $("#txtRegEmailId").val();
        var MobileNo = $("#txtRegMobileNo").val();
        var Password = $("#txtRegPassword").val();
        var ConfirmPassword = $("#txtRegConfirmPassword").val();
        var RoleId = 4;//
        var data = {
            FirstName: FirstName,
            LastName: LastName,
            UserName: UserName,
            EmailID: EmailID,
            MobileNo: MobileNo,
            Password, Password,
            RoleId: RoleId
        };
        if (isvalidate(data, 'newuser')) {
            if (isRegistrationbtnclicked == false) {
                isRegistrationbtnclicked = true;
                $.ajax({
                    type: "POST",
                    url: "/UserDetail/SaveData",
                    data: JSON.stringify(data),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        //$(".registermsg").html(data);
                        if (data == "success") {
                            $("#txtEmailId").val($("#txtRegMobileNo").val().trim());
                            $("#txtPassword").val($("#txtRegPassword").val());
                            $(".btnNewUser").hide();
                            $("#UserRegister").find("input").hide();
                            $(".btnLogin").trigger("click");
                            //$(".registermsg").html("Your Registration done. Please wait we redirect to dashboard");
                        }
                    }
                });
            }
        }
    });
    $(".btnLogin").click(function () {
        Login();
    });
    $('.loginonenter').bind('keypress', function (e) {
        if (e.keyCode == 13) {
            Login();
        }
    });
    $(".btnLogout").click(function () {
        var data = {};


        $.ajax({
            type: "POST",
            url: "/UserDetail/Logout",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                window.location.href = window.location.origin;


            }
        });

    });
    $(".password_hide").click(function () {
        $(this).parents(".form-group").find("input").attr("type", "text");
        $(this).parents(".form-group").find(".password_show").show();
        $(this).hide();
    });
    $(".password_show").click(function () {
        $(this).parents(".form-group").find("input").attr("type", "password");
        $(this).parents(".form-group").find(".password_hide").show();
        $(this).hide();
    });

    $("#gSignIn").click(function () {
        isLoginWithGmail = true;
        if (UserProfile != null) {
            LoginAndRegistrationWithGmail(UserProfile);
        }
    });
    setTimeout(function () {
        renderButton();
    }, 1000);

    $(".UserRegister").click(function () {
        $("input").val('');
        $(".validatemsg").html('');
        $(".registermsg").html('');
    });

    $(document).ready(function () {
        $('#txtRegPassword').keyup(function () {
            $('#registermsg').html(checkStrength($('#txtRegPassword').val()))
        })
        $('#txtRegPassword').focus(function () {
            $('#registermsg').html(checkStrength($('#txtRegPassword').val()))
            $('#registermsg').hide();
        })
        $('#txtRegPassword').blur(function () {
            $('#registermsg').html('');
            $('#registermsg').hide();
        })
        
    });
});
function checkStrength(password) {
    var strength = 0
    if (password.length < 6) {
        $('#registermsg').removeClass()
        $('#registermsg').addClass('Short')
        return 'Too short'
    }
    if (password.length > 7) strength += 1
    // If password contains both lower and uppercase characters, increase strength value.  
    if (password.match(/([a-z].*[A-Z])|([A-Z].*[a-z])/)) strength += 1
    // If it has numbers and characters, increase strength value.  
    if (password.match(/([a-zA-Z])/) && password.match(/([0-9])/)) strength += 1
    // If it has one special character, increase strength value.  
    if (password.match(/([!,%,&,@,#,$,^,*,?,_,~])/)) strength += 1
    // If it has two special characters, increase strength value.  
    if (password.match(/(.*[!,%,&,@,#,$,^,*,?,_,~].*[!,%,&,@,#,$,^,*,?,_,~])/)) strength += 1
    // Calculated strength value, we can return messages  
    // If value is less than 2  
    if (strength < 2) {
        $('#registermsg').removeClass()
        $('#registermsg').addClass('Weak')
        return 'Password Weak'
    } else if (strength == 2) {
        $('#registermsg').removeClass()
        $('#registermsg').addClass('Good')
        return 'Password Good'
    } else {
        $('#registermsg').removeClass()
        $('#registermsg').addClass('Strong')
        return 'Password Strong'
    }
}
function Login() {
    var EmailID = $("#txtEmailId").val().trim();
    var Password = $("#txtPassword").val();
    var data = {
        EmailID: EmailID,
        Password: Password
    };
    try{
        if (grecaptcha != undefined) {
            var rcres = grecaptcha.getResponse();
            if (rcres.length==0) {
                $(".loginmsg").html("Please verify CAPTCHA");
                captcha=false;
                return;
            }
        }
    }catch(err){}
    if (isvalidate(data, 'login')) {
        $.ajax({
            type: "POST",
            url: "/UserDetail/CheckLogin",
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {

                if (data == "Success") {
                    //location.reload();
                    window.location = location.origin + "/Dashboard";
                }
                else {
                    $(".loginmsg").html(data);
                }

            }
        });
    }
}
function renderButton() {
    if (gapi != undefined) {
        gapi.signin2.render('gSignIn', {
            'scope': 'profile email',
            'width': 240,
            'height': 50,
            'longtitle': true,
            'theme': 'dark',
            'onsuccess': onSuccess,
            'onfailure': onFailure
        });
    }

}
// Sign-in success callback
function onSuccess(googleUser) {
    // Get the Google profile data (basic)
    //var profile = googleUser.getBasicProfile();
    UserProfile = googleUser;
    // Retrieve the Google account data
    if (googleUser.wt != undefined) {
        gapi.client.load('oauth2', 'v2', function () {
            var request = gapi.client.oauth2.userinfo.get({
                'userId': 'me'
            });
            request.execute(function (resp) {
                console.log("new function");
                console.log(resp);
                if (isLoginWithGmail == true) {
                    //LoginAndRegistrationWithGmail(UserProfile);
                }
            });
        });
    }
}
// Sign-in failure callback
function onFailure(error) {
    alert(error);
}
// Sign out the user
function signOut() {
    var auth2 = gapi.auth2.getAuthInstance();
    auth2.signOut().then(function () {
    });
    auth2.disconnect();
}



//function SetLoginWithGmail() {
//    isLoginWithGmail = true;
//}
//function loginWithGmail() {
//    isLoginWithGmail = true;
//    var myParams = {
//        'clientid': '171573718084-nklio2q81pldqfq9n123sgp8od28cc9d.apps.googleusercontent.com', //You need to set client id
//        'cookiepolicy': 'single_host_origin',
//        'callback': 'loginCallback', //callback function
//        'approvalprompt': 'force',
//        'scope': 'https://www.googleapis.com/auth/plus.login https://www.googleapis.com/auth/plus.profile.emails.read'
//    };
//    var a = gapi.auth.signIn(myParams);
//}
//function loginCallback(t) {

//}

//function signinCallback(t) {
//    debugger;
//    var LoginWithGmailquerystring = getUrlVars()["LoginWithGmail"];
//    if (t.wt != undefined && isLoginWithGmail == true) {

//        //window.location.href = "http://" + window.location.host + window.location.pathname;

//    }
//    if (t.error != undefined && t.error == "immediate_failed_user_logged_out") {
//        var data = {};
//        var d = AjaxCall("UserDetail", "GmailLogoutChecking", data, false);
//        if (d == "logout") {
//            location.reload();
//        }
//    }

//}
function LoginAndRegistrationWithGmail(googleUser) {
    //debugger;
    isLoginWithGmail = false;
    var profile = googleUser.getBasicProfile();
    var FirstName = profile.getGivenName();
    var LastName = profile.getFamilyName();
    var EmailID = profile.getEmail();
    var GmailImageUrl = profile.getImageUrl()
    var LoginWithGmail = 1;
    var data = {
        FirstName: FirstName,
        LastName: LastName,
        EmailID: EmailID,
        LoginWithGmail: LoginWithGmail,
        GmailImageUrl: GmailImageUrl
    };
    AjaxCall("UserDetail", "LoginAndRegistrationWithGmail", data, false);

}

function isvalidate(d, form) {
    var isvalid = true;
    return true;
    var msg = [];
    if (form == "login") {
        if (d.EmailID == "") {
            msg.push("Please Enter User Name");
        }
        if (d.Password == "") {
            msg.push("Please Enter Password");
        }
        if (msg.length > 0) {
            var error = "<ul>";
            for (var i = 0; i < msg.length; i++) {
                error += "<li>" + msg[i] + "</li>";
            }
            error += "</ul>";
            alert(error);
            isvalid = false;
        }
    }
    if (form == "newuser") {
        if (d.FirstName == "") {
            msg.push("Please Enter First Name")
        }
        if (d.UserName == "") {
            msg.push("Please Enter User Name");
        }
        if (d.EmailID == "") {
            msg.push("Please Enter Email Id");
        }
        else {
            if (isEmail(d.EmailID) == false) {
                msg.push("Invalid Email Id");
            }
        }
        if (d.MobileNo == "") {
            msg.push("Please Enter Mobile No");
        }
        else {
            //if (validate_Phone_Number(d.MobileNo) == 1) {
            //    msg.push("Invalid Mobile no");
            //}
        }
        if (d.Password == "") {
            msg.push("Please Enter Password");
        }
        if (d.ConfirmPassword == "") {
            msg.push("Please Enter Confirm Password");
        }

        //if (d.Password != "" && d.ConfirmPassword != "" && d.Password != d.ConfirmPassword) {
        //    msg.push("Password and Confirm password not match");
        //}

        if (msg.length > 0) {
            var error = "<ul>";
            for (var i = 0; i < msg.length; i++) {
                error += "<li>" + msg[i] + "</li>";
            }
            error += "</ul>";
            //alert(error);
            $(".registermsg").html(error);
            isvalid = false;
        }
    }
    return isvalid;
}
function UploadFile(id, hdncntrl, type) {
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
        var progressbarhtml = '<div class="progress"><div class="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%"></div></div>';
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
                var html = '<div class="' + id + '_addedFile"><span class="files" data-savedfilename="' + result + '">' + files[0].name + '</span>' +
                    '<span class="remove" data-hdncntrl="' + hdncntrl + '" data-savedfilename="' + result + '" onclick="removethis(this)">x</span>';

                debugger;
                if (id == "fu_Script_ProductServiceImages") {
                    $("#" + id).parents(".file-upload").find(".filename").append(html);
                    var ids = "";
                    $(".fu_Script_ProductServiceImages_addedFile .files").each(function () {
                        ids += $(this).attr("data-savedfilename") + ",";
                    });
                    
                    ids = ids.replace('undefined,', '').replace('undefined', '');
                    $("#" + hdncntrl).val(ids);
                }
                else {
                    $("#" + id).parents(".file-upload").find(".filename").html(html);
                }
            },
            xhr: function () {  // Custom XMLHttpRequest
                    var myXhr = $.ajaxSettings.xhr();
                    if (myXhr.upload) { // Check if upload property exists
                        //myXhr.upload.onprogress = progressHandlingFunction
                        myXhr.upload.addEventListener('progress', progressHandlingFunction,false); // For handling the progress of the upload
                    }
                    return myXhr;
                },
            error: function (err) {
                console.log(err.statusText);
            }
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
function fileValidation(file, type) {
    $("#" + file).parents(".file-upload").find(".error-message").html('');
    var fileInput = document.getElementById(file);
    var filePath = fileInput.value;
    if (filePath != null) {
        // Allowing file type
        var allowedExtensions = "";
        var extravalidation = "";
        if (type.indexOf(",") > 0) {
            allowedExtensions = type.split(",")[0];
            extravalidation = type.split(",")[1];
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
            allowedExtensions = ".avi|.mov|.wmv|.mp4";
        }
        else if (type = "audio") {
            allowedExtensions = ".mp3|.wav";
        }
        if (allowedExtensions != "") {
            regex = new RegExp("(.*?)\.(" + allowedExtensions + ")$");
            if (!(regex.test(filePath))) {
                if (type == "image") {
                    allowedExtensions = ".jpg, .jpeg, .png";
                    if (extravalidation != "") {
                        allowedExtensions = allowedExtensions + "or ." + extravalidation;
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
function ViewFile(filename) {
    var file = '<img src="/Uploads/' + filename + '" style="width:100%; height:300px;"/>';
    $("#ImageView").find(".modal-body-content").html(file);
    $("#ImageView").modal();
}

function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);

}
function validate_Phone_Number(number) {
    count = number.length;
    if (number[0] != " " && number[0] != "-" && number[count - 1] != " " && number[count - 1] != "-") {
        temp = number.replace(" ", "");
        temp = temp.replace("-", "");
        if ($.isNumeric(temp)) {
            if (temp.length >= 7 && temp.length <= 10) {
                flag = 1;
                for (i = 1; i < count; i++) {
                    if (number[i] == "-" || number[i] == " ") {
                        if (number[i - 1] == "-" || number[i - 1] == " " || number[i + 1] == "-" || number[i + 1] == " ") {
                            flag = 0;
                        }
                    }
                }

                if (flag == 1) {
                    valid = 1;
                }
                else {
                    valid = 0;
                }
            }
            else {
                valid = 0;
            }
        }
        else {

            valid = 0;
        }

    }
    else {
        valid = 0;
    }
    return valid;
}
function _popupVC(html, title) {
    $("#common-modal").find(".modal-title").html(title);
    $("#common-modal").find(".modal-subtitle").html(html);
    $("#common-modal").modal();
}
function AjaxCallHtml(Controller, method, Contentdata, title) {
    // $(".page-loader-wrapper").show();
    $.ajax({
        url: "/" + Controller + "/" + method,
        contentType: 'application/html; charset=utf-8',
        type: 'GET',
        data: Contentdata,
        dataType: 'html',
        success: function (result) {
            _popupVC(result, title);
            //       $(".page-loader-wrapper").hide();
        }
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
                _popupVC(data, "Message", "success");
            }
            else {
                if (method == "LoginAndRegistrationWithGmail") {
                    window.location.href = window.location.origin + "/dashboard";
                }
                if (method == "validateUser") {
                    if (data == "validuser") {
                        SentOtp('txtRegMobileNo');
                    }
                    else {
                        $(".validatemsg").html(data);
                    }
                }
                if (method == "SentOtp") {
                    $(".validatemsg").html(data);
                    $(".VarifyOtp").show();
                }
                if (method == "SentOtpForgotPassword") {

                    if (data != "Mobile no not exists") {
                        timer = 0;
                        $(".msgForgotPassword").css("color", "white");
                        timer = 45;
                        cl = setInterval(function () {

                            timer--;
                            $(".btnForgotSentOTP").text("Resent(" + timer + ")");
                            if (timer == 0) {
                                $(".btnForgotSentOTP").text("Resent");
                                clearInterval(cl);
                            }
                        }, 1000);
                    }
                    $(".msgForgotPassword").html(data);

                }
                if (method == "ResetPassword") {
                    $(".msgForgotPassword").css("color", "white");
                    $(".msgForgotPassword").html(data);
                }
                if (method == "VarifyOTP") {
                    if (data == "Success") {
                        $(".Mobilenoarea").hide();
                        $(".validatemsg").html('');
                        $(".OtherData").show();
                        $(".VarifyOtp").hide();
                    }
                    else {
                        $(".validatemsg").html(data);
                    }
                }
                if (method == "") {

                }
                return data;
            }
        }
    });

}
function viewfaq() {
    var data = {};
    AjaxCallHtml("Home", "FAQ", data, "FAQ");
}
function privacypolicy() {
    var data = {};
    AjaxCallHtml("Home", "PrivacyPolicy", data, "Privacy Policy");
}
function termandconditions() {
    var data = {};
    AjaxCallHtml("Home", "TermAndConditions", data, "Term And Conditions");
}

function _ScriptDetails() {
    var data = {};
    AjaxCallHtml("Orders", "_ScriptDetails", data, "Video Details");
}
function _VORequirements() {
    var data = {};
    AjaxCallHtml("Basket", "_VORequirements", data, "VO REQUIREMENT");
}
function SaveScriptDetails(OrderId, btn) {
    var data = {
        OrderId: OrderId,
        Script_Required: $("#ScriptRequiredYes").is(":checked"),
        Script_VideoConcept: $("#Script_VideoConcept").val(),
        Script_TargetAudience: $("#Script_TargetAudience").val(),
        Script_ExlpainVideoRequirement: $("#Script_ExlpainVideoRequirement").val(),
        Script_FillVideoRequirement: $("#Script_FillVideoRequirement").val(),
        Script_BenifitesForCustomer: $("#Script_BenifitesForCustomer").val(),
        Script_CompanyName: $("#Script_CompanyName").val(),
        Script_CompanyWebsite: $("#Script_CompanyWebsite").val(),
        Script_LogoName: $("#Script_LogoName").val(),
        Script_ProductServiceImages: $("#Script_ProductServiceImages").val()
    };
    //debugger;
    $("#SaveScriptDetails").html('');
    if (data.Script_VideoConcept.trim() == '' || data.Script_TargetAudience.trim() == '' || data.Script_ExlpainVideoRequirement.trim() == '' || data.Script_FillVideoRequirement.trim() == '' || data.Script_BenifitesForCustomer.trim() == '' || data.Script_CompanyName == '') {
        var msg = 'Please fill all mandatory fields';
        $("#scriptmessage").html(msg);
        return;
    }


    $("#hdnScript_VideoConcept").val(Script_VideoConcept);
    $("#hdnScript_TargetAudience").val(Script_TargetAudience);
    $("#hdnScript_ExlpainVideoRequirement").val(Script_ExlpainVideoRequirement);
    $("#hdnScript_FillVideoRequirement").val(Script_FillVideoRequirement);
    $("#hdnScript_BenifitesForCustomer").val(Script_BenifitesForCustomer);
    $("#hdnScript_CompanyName").val(Script_CompanyName);

    AjaxCall("Orders", "SaveScriptDetails", data, false);
    $(btn).parents(".modal").find(".close").trigger("click");
}
function SaveVideoSelected(SampleVideoId, SampleVideoName, SampleVideoPrice, YoutubeLink) {
    var data = {
        SampleVideoId: SampleVideoId,
        SampleVideoPrice: SampleVideoPrice,
        SampleVideoName: SampleVideoName,
        YoutubeLink: YoutubeLink
    }
    AjaxCall("Basket", "SaveVideoSelected", data, false);

}
function SampleVideo_Step1Save() {
    $($(".custom")[1]).css("border-color", "#fcc012");
   // var val = $("#VideoResolution").val();
    if (VideoResolution.indexOf("Custom") >= 0) {
        
            var width = $(".customwidth").val();
            var height = $(".customheight").val();
            var size = "(widthXheight)";
            if (parseInt(width) == 0) {
                $($(".custom")[1]).css("border-color", "red");
                return;
            }
            else {
                size = size.replace("width",width);
            }
            if (parseInt(height) == 0) {
                $($(".custom")[1]).css("border-color", "red");
                return;
            }
            else{
                size = size.replace("height", height);
            }

            if (VideoResolution == "Custom") {
                VideoResolution = VideoResolution + size;
            }
        
    }
    var data = {
        VideoResolution: VideoResolution,
        ED_VideoDuration: $("#ddVideoDuration").val()
    }
    AjaxCall("Basket", "SampleVideo_Step1Save", data, false);
    window.location.reload();

}
function SampleVideo_Step2_Submit() {
    var Script_Required = $("#ScriptRequiredYes").is(":checked");
    var data = { Script_Required: Script_Required };
    AjaxCall("Basket", "SampleVideo_Step2_Submit", data, false);
}
function SampleVideo_Step3Save_1() {
    var VO_Required = $("#VORequiredYes").is(":checked");
    var data = {
        VO_Required: VO_Required,
    }
    var price = AjaxCall("Basket", "SampleVideo_Step3Save_1", data, false);

}
function SampleVideo_Step3Save_2(btn) {
    var VO_Required = $("#VORequiredYes").is(":checked");
    var VO_Language = $(".VO_Language:checked").attr("data-language");
    var VO_Gender = $(".VO_Gender:checked").attr("data-gender");
    var VO_SampleId = $(".VoiceArtistSounds:checked").attr("data-VoiceArtistSoundsId");
    var data = {
        VO_Required: VO_Required,
        VO_Language: VO_Language,
        VO_Gender: VO_Gender,
        VO_SampleId: VO_SampleId
    }
    var price = AjaxCall("Basket", "SampleVideo_Step3Save_2", data, false);
    $(btn).parents(".modal").find(".close").trigger("click");
    window.location.reload();
}

function OrderNow() {

    var hdnScript_VideoConcept = $("#hdnScript_VideoConcept").val();
    var hdnScript_TargetAudience = $("#hdnScript_TargetAudience").val();
    var hdnScript_ExlpainVideoRequirement = $("#hdnScript_ExlpainVideoRequirement").val();
    var hdnScript_FillVideoRequirement = $("#hdnScript_FillVideoRequirement").val();
    var hdnScript_BenifitesForCustomer = $("#hdnScript_BenifitesForCustomer").val();
    var hdnScript_CompanyName = $("#hdnScript_CompanyName").val();

    if (hdnScript_VideoConcept == "" || hdnScript_TargetAudience == "" || hdnScript_ExlpainVideoRequirement == "" || hdnScript_FillVideoRequirement == "" || hdnScript_BenifitesForCustomer == "" || hdnScript_CompanyName == "") {
        _popupVC("BEFORE ORDERING, YOU NEED TO FILL THE VIDEO FORM IN THE STEP 04 SECTION IT'S MANDATORY TO UNDERSTAND YOUR REQUIREMENTS.", "HELLO, JUST TO KNOW");
    }
    else {
        var data = {
            VideoResolution: VideoResolution,
            ED_VideoDuration: $("#ddVideoDuration").val()
        }
        //AjaxCall("Basket", "SampleVideo_Step1Save", data, false);


        //SampleVideo_Step2_Submit();
        SampleVideo_Step3Save_1();
        var data = {};
        AjaxCall("Basket", "OrderNow", data, false);
        window.location.href = location.origin + "/Basket/MyCart";
    }


}
function contactus_details() {
    var Name = $(".section-content").find(".name").val();
    var Email = $(".section-content").find(".email").val();
    var ContactNo = $(".section-content").find(".contact_no").val();
    var Messages = $(".section-content").find(".message").val();
    var data = {
        Name: Name,
        Emailid: Email,
        MobileNo: ContactNo,
        MessageInfo: Messages
    };
    if (data.Name != "" && data.Emailid != "" && data.MobileNo != "" && data.MessageInfo != "") {
        AjaxCall("EnquiryDetails", "NewEnquiry", data, true);
        $(".section-content").find(".name").val('');
        $(".section-content").find(".email").val('');
        $(".section-content").find(".contact_no").val('');
        $(".section-content").find(".message").val('');
    }

}
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

function validateUser() {
    var data = {
        MobileNo: $('#txtRegMobileNo').val().trim()
    }
    if (data.MobileNo.length > 9) {
        AjaxCall("UserDetail", "validateUser", data, false);
    }
    else {
        if (data.MobileNo == "") {
            $(".validatemsg").html("Please enter  mobile no");
        }
        else {
            $(".validatemsg").html("Please enter valid mobile no");
        }
    }
}
var timer = 0;

function SentOtp(txt) {
    $(".btnProceed").hide();
    var data = {
        MobileNo: $("#" + txt).val().trim()

    };
    if (timer == 0) {
        var d = AjaxCall("OtpManagement", "SentOtp", data, false);
        timer = 45;
        cl=setInterval(function () {
            
            timer--;
            $(".btnResendOtp").text("Resent(" + timer + ")");
            if (timer == 0) {
                $(".btnResendOtp").text("Resent");
                clearInterval(cl);
            }
        },1000);

    }
}
function SentOtpForgotPassword() {
    $(".msgForgotPassword").html("");
    var data = {
        MobileNo: $("#txtMobileNo").val().trim()

    };
    if (data.MobileNo == "") {
        $(".msgForgotPassword").css("color", "#fcc102");
        $(".msgForgotPassword").html("Please enter mobile no");
        return;
    }
    try {
        if (grecaptcha != undefined) {
            var rcres = grecaptcha.getResponse();
            if (rcres.length == 0) {
                $(".msgForgotPassword").html("Please verify CAPTCHA");
                return;
            }
        }
    } catch (err) { }
    if (timer == 0) {
        var d = AjaxCall("OtpManagement", "SentOtpForgotPassword", data, false);
    }
}
function VarifyOTP(txtmob, txtotp) {
    var data = {
        MobileNo: $("#" + txtmob).val().trim(),
        OTP: $("#" + txtotp).val().trim()

    };
    var d = AjaxCall("OtpManagement", "VarifyOTP", data, false);

}
function ResetPassword() {
    var data = {
        MobileNo: $("#txtMobileNo").val().trim(),
        OTP: $("#txtForgotPasswordOTP").val().trim(),
        Password:$("#txtForgotPassword").val()
    };
    var d = AjaxCall("UserDetail", "ResetPassword", data, false);
}
function RemoveScript() {
    var data = {};
    var price = AjaxCall("Basket", "RemoveScript", data, false);
}
function RemoveVoiceOver(VO_SampleId) {
    var data = {
        VO_SampleId: VO_SampleId
    };
    var price = AjaxCall("Basket", "RemoveVoiceOver", data, false);
}
function removethis(id) {
    $(id).parent("div").remove();
    var hdncntrl = $(id).attr("data-hdncntrl");
    var savedfilename = $(id).attr("data-savedfilename");
    $("#" + hdncntrl).val('');
    /*if (id == "fu_Script_ProductServiceImages") {
        $("#" + id).parents(".file-upload").find(".filename").append(html);
        var ids = "";
        $(".fu_Script_ProductServiceImages_addedFile").each(function () {
            ids += $(this).find(".files").attr("data-savedfilename") + ",";
        });
        $("#" + hdncntrl).val(ids);
    }*/
    if (savedfilename != "") {
        DeleteUploadedFile(savedfilename);
    }
}
function DeleteUploadedFile(filename) {
    var data = { FileName: filename };
    AjaxCall("FileManager", "DeleteUploadedFile", data, false);
}

function SaveFeedback(data) {
    AjaxCall("CustomerFeedback", "SaveFeedback", data, false);
    _popupVC('Thank you for your valuable feedback','');

}



      wow = new WOW(
                    {
                        boxClass:     'wow',      // default
                        animateClass: 'animated', // default
                        offset:       1,          // default
                        mobile:       true,       // default
                        live:         true        // default
                    }
                  )
wow.init();

