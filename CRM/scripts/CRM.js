
function GenerateInvoice(OrderId) {
    var data = { OrderId: OrderId };
    AjaxCall("OrderStatus", "GenerateInvoice", data, true);
}
function UserDetails(UserId) {
    var data = { UserID: UserId };
    AjaxCallHtml("UserDetail", "_UserPersonalDetails", data, "More Details");
}
function _ClientDetails(UserId) {
    var data = { UserID: UserId };
    AjaxCallHtml("UserDetail", "_ClientDetails", data, "Client Detail");
}
function _OrderDetails(OrderId, Script_Required, VO_Required) {
    var data = {
        OrderId: OrderId,
        Script_Required: Script_Required,
        VO_Required: VO_Required
    };
    AjaxCallHtml2("OrderStatus", "_OrderDetails", data, "ORDER DETAILS");
}

function FreelancerInvoice(OrderId) {
    var data = { OrderId: OrderId };
    AjaxCallHtml("OrderStatus", "_FreelancerInvoice", data, "Invoice Generated");

}
function FreelancerInvoiceforAdmin(OrderId, Role, UserId) {
    var data={};
    if (Role == 'script') {
        data={
            OrderId: OrderId ,
            ScriptAssignedUserId:UserId
        }
    }
    if (Role == 'vo') {
        data = {
            OrderId: OrderId,
            VOAssignedUserId: UserId
        }
    }
    if (Role == 'video') {
        data = {
            OrderId: OrderId,
            VideoAssignedUserId: UserId
        }
    }
    AjaxCallHtml3("OrderStatus", "_FreelancerInvoice", data, "Invoice");

}
function ScriptDetails(OrderId) {
    var data = { OrderId: OrderId };
    AjaxCallHtml("Orders", "_ScriptDetails", data, "");
}
function SaveScriptDetails(OrderId) {
    var data = {
        OrderId: OrderId,
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

    //$("#viewpopup").modal("toggle");
    popupCV("This will be treated as your final requirement for your video. Are you sure you want to submit this?", "HELLO, JUST TO KNOW", "SaveScriptDetails", data);

}

function _UpdateProjectFileLink(OrderId) {
    var data = { OrderId: OrderId };
    AjaxCallHtml1("OrderStatus", "_UpdateProjectFileLink", data, "Project Link");
}
function UpdateProjectFileLink(OrderId) {
    var ProjectFileLink = $("#ProjectFileLink").val().trim();
    var data = {
        OrderId: OrderId,
        ProjectFileLink: ProjectFileLink
    };
    AjaxCall("OrderStatus", "UpdateProjectFileLink", data, true);
}

function _ArtistDetails(RoleId, title) {
    var data = { RoleId: RoleId };
    AjaxCallHtml1("UserDetail", "_ArtistDetails", data, title);
}

function _UpdateProjectStatusByArtist(OrderId) {
    var data = { OrderId: OrderId };
    AjaxCallHtml2("OrderStatus", "_UpdateProjectStatusByArtist", data, "Project Details");
}
function UpdateProjectStatusByArtist(fileVersion) {
    var data = {
        OrderId: $("#OrderId").val(),
        ScriptFileName_1: $("#ScriptFileName_1").val(),
        VOFileName_1: $("#VOFileName_1").val(),
        VideoFileName_1: $("#VideoFileName_1").val(),
        ScriptFileName_2: $("#ScriptFileName_2").val(),
        VOFileName_2: $("#VOFileName_2").val(),
        VideoFileName_2: $("#VideoFileName_2").val(),
        ScriptFileName_3: $("#ScriptFileName_3").val(),
        VOFileName_3: $("#VOFileName_3").val(),
        VideoFileName_3: $("#VideoFileName_3").val(),
        ScriptStatus_1: $("#ScriptStatus_1").val(),
        VOStatus_1: $("#VOStatus_1").val(),
        VideoStatus_1: $("#VideoStatus_1").val(),
        ScriptStatus_2: $("#ScriptStatus_2").val(),
        VOStatus_2: $("#VOStatus_2").val(),
        VideoStatus_2: $("#VideoStatus_2").val(),
        ScriptStatus_3: $("#ScriptStatus_3").val(),
        VOStatus_3: $("#VOStatus_3").val(),
        VideoStatus_3: $("#VideoStatus_3").val()
    };

    var roleid=$("#hdnroleid").val();
    if (fileVersion == 1) {

        if ((roleid == 5 && data.ScriptStatus_1 == "") || (roleid == 6 && data.VOStatus_1 == "") || (roleid == 7 && data.VideoStatus_1 == "")) {
            popupV1('Please select status', 'JUST TO KNOW', "success");
            return;
        }
        if ((roleid == 5 && data.ScriptFileName_1 == '') || (roleid == 6 && data.VOFileName_1 == '') || (roleid == 7 && data.VideoFileName_1 == '')) {
            popupV1('Nothing is uploaded', 'JUST TO KNOW', "success");
            return;
        }
        AjaxCall("OrderStatus", "UpdateProjectStatusByArtist1", data, false);
        popupV1('File sent to customer for checking', 'JUST TO KNOW', "success");
        if (data.ScriptFileName_1 != '') { $("#fu_ScriptFileName_1").parents(".file-upload").hide();}
        if (data.VOFileName_1 != '') { $("#fu_VOFileName_1").parents(".file-upload").hide(); }
        if (data.VideoFileName_1 != '') { $("#fu_VideoFileName_1").parents(".file-upload").hide(); }

    }
    if (fileVersion == 2) {
        if ((roleid == 5 && data.ScriptStatus_2 == "") || (roleid == 6 && data.VOStatus_2 == "") || (roleid == 7 && data.VideoStatus_2 == "")) {
            popupV1('Please select status', 'JUST TO KNOW', "success");
            return;
        }
        if ((roleid == 5 && data.ScriptFileName_2 == '') || (roleid == 6 && data.VOFileName_2 == '') || (roleid == 7 && data.VideoFileName_2 == '')) {
            popupV1('Nothing is uploaded', 'JUST TO KNOW', "success");
            return;
        }
        AjaxCall("OrderStatus", "UpdateProjectStatusByArtist2", data, false);
        popupV1('File sent to customer for checking', 'JUST TO KNOW', "success");
        if (data.ScriptFileName_2 != '') { $("#fu_ScriptFileName_2").parents(".file-upload").hide(); }
        if (data.VOFileName_2 != '') { $("#fu_VOFileName_2").parents(".file-upload").hide(); }
        if (data.VideoFileName_2 != '') { $("#fu_VideoFileName_2").parents(".file-upload").hide(); }
    }
    if (fileVersion == 3) {
        //debugger;
        if ((roleid == 5 && data.ScriptStatus_3 == "") || (roleid == 6 && data.VOStatus_3 == "") || (roleid == 7 && data.VideoStatus_3 == "")) {
            popupV1('Please select status', 'JUST TO KNOW', "success");
            return;
        }
        if ((roleid == 5 && data.ScriptFileName_3 == '') || (roleid == 6 && data.VOFileName_3 == '') || (roleid == 7 && data.VideoFileName_3 == '')) {
            popupV1('Nothing is uploaded', 'JUST TO KNOW', "success");
            return;
        }
        AjaxCall("OrderStatus", "UpdateProjectStatusByArtist3", data, false);
        popupV1('File sent to customer for checking', 'JUST TO KNOW', "success");
        if (data.ScriptFileName_3 != '') { $("#fu_ScriptFileName_3").parents(".file-upload").hide(); }
        if (data.VOFileName_3 != '') { $("#fu_VOFileName_3").parents(".file-upload").hide(); }
        if (data.VideoFileName_3 != '') { $("#fu_VideoFileName_3").parents(".file-upload").hide(); }
    }
}

function _ViewFeedback(OrderId, SentTo, FeedbackType, title) {
    var data = {
        OrderId: OrderId,
        FeedbackType: FeedbackType,
        SentTo: SentTo
    }
    AjaxCallHtml1("FeedbackDetails", "_ViewFeedback", data, title);
}
function SaveFeedback(OrderId, SentTo, FeedbackType) {
    $("#msgfeedback").html();
    var FeedbackComments = $("#feedbackComments").val().trim();
    var FilesUploaded = $("#FilesUploaded").val().trim();
    var ClientStatus = $("#ddUpdateClientStatus").val();
    if (FeedbackComments != "" && ClientStatus != "" && ClientStatus!="0") {
        var data = {
            FeedbackComments: FeedbackComments,
            OrderId: OrderId,
            FeedbackType: FeedbackType,
            SentTo: SentTo,
            FilesUploaded: FilesUploaded,
            ClientStatus: ClientStatus
        }
        $(".feedbackComments").val('');
        $("#FilesUploaded").val('');
        AjaxCall("FeedbackDetails", "SaveFeedback", data, true);
        if (FeedbackType.indexOf("ScriptFileName") >= 0) {
            if (ClientStatus == "7") {
                $(".Projectstatus_script").text('Approved');
            }
            else {
                $(".Projectstatus_script").text('YTS');
            }
        }
        if (FeedbackType.indexOf("VOFileName") >= 0) {
            if (ClientStatus == "7") {
                $(".Projectstatus_vo").text('Approved');
            }
            else {
                $(".Projectstatus_vo").text('YTS');
            }
        }
        if (FeedbackType.indexOf("VideoFileName") >= 0) {
            if (ClientStatus == "7") {
                $(".Projectstatus_video").text('Approved');
            }
            else {
                $(".Projectstatus_video").text('YTS');
            }
        }
    }
    else {
        if (ClientStatus == '0') {
            $("#msgfeedback").html('Please select status');
        } else if (FeedbackComments == '') {
            $("#msgfeedback").html('Please enter feedback comments');
        } 
    }
}

function AssignOrderToUser(OrderId) {
    var data = {
        OrderId: OrderId,
        ScriptAssignedUserId: $("#ScriptAssignedUserId").val(),
        VOAssignedUserId: $("#VOAssignedUserId").val(),
        VideoAssignedUserId: $("#VideoAssignedUserId").val()
    }
    AjaxCall("OrderStatus", "AssignOrderToUser", data, false);
    $(".btnupdate").hide();
}
function _CustomerInvoice(OrderId) {
    var data = {
        OrderId: OrderId
    }
    var orderinvoice = "Invoice No: OD" + pad(OrderId, 5);
    AjaxCallHtml2("Orders", "_CustomerInvoice", data, orderinvoice);
}

function _TopUpRequest(OrderId) {
    var data = { OrderId: OrderId };
    AjaxCallHtml("TopUpPayment", "_TopUpRequest", data, "Top Up");
}
function SentTopUpRequest(OrderId) {
    var data = {
        OrderId: OrderId,
        AdditionalDuration: $("#AdditionalDuration").val()
    };
    AjaxCall("TopUpPayment", "SentTopUpRequest", data, true);

}
function _TopUpInvoice(OrderId) {
    var data = { OrderId: OrderId };
    AjaxCallHtml2("TopUpPayment", "_TopUpInvoice", data, "Top Up Invoice");
}
function UpdateTopUpPrice(OrderId) {
    var data = {
        OrderId: OrderId,
        ScriptCharges: $("#ScriptCharges").val(),
        VioceOverCharges: $("#VioceOverCharges").val(),
        VideoCharges: $("#VideoCharges").val(),
        AdditionalServiceCharegs: $("#AdditionalServiceCharegs").val(),
        DiscountCharges: $("#DiscountCharges").val()
    };
    AjaxCall("TopUpPayment", "UpdateTopUpPrice", data, true);
}
function _PaymentDetails(OrderId, Amount) {
    var data = {
        OrderId: OrderId,
        Amount: Amount
    };
    AjaxCallHtml1("OrderStatus", "_PaymentDetails", data, "Payment Details");
}

function ShowMessage(msg, title) {
    var html = "<ul>";
    if (msg.length > 0) {
        for (var i = 0; i < msg.length; i++) {
            html += "<li>" + msg[i] + "</li>";
        }
    }
    html += "</ul>";
    popupV(html, title, "");
}
function editUser() {
    if ($(".rdbuserid:checked").length > 0) {
        var data = {
            UserId: $(".rdbuserid:checked").attr("data-UserId")
        };
        callPage("/UserDetail/NewUser", "Update User Details", data.UserId);
        // popupCV("Are you sure you want to submit?", "Confirmation", "editUser", data);
    }
    else {
        popupV1("Please select User for Edit", "Warning", "warning");
    }

}
function deleteUser() {
    if ($(".rdbuserid:checked").length > 0) {
        var data = {
            UserId: $(".rdbuserid:checked").attr("data-UserId")
        };
        popupCV("Are you sure you want to delete this user?", "Confirmation", "InActiveUser", data);
    }
    else {
        popupV1("Please select User for Delete", "Warning", "");
    }
}
function _ViewSample(UserId) {
    var data = { UserId: UserId };
    AjaxCallHtml("UserDetail", "_ViewSample", data, "Artist Sample");
}

function ChangePassword() {
    var title = "Warning";
    var CurrentPassword = $("#CurrentPassword").val();
    var NewPassword = $("#NewPassword").val();
    var Confirmpassword = $("#ConfirmPassword").val();
    if (CurrentPassword != "" || NewPassword != "" || Confirmpassword != "") {

        if (CurrentPassword == "") {
            popupV1("Please Enter Current password", title, "success");
            return;
        }
        else if (NewPassword == "") {
            popupV1("Please Enter New password", title, "success");
            return;
        }
        else if (Confirmpassword == "") {
            popupV1("Please Enter Confirm password", title, "success");
            return;
        }
        else if (NewPassword != Confirmpassword) {
            popupV1("New Password and confirm password is not match", title, "success");
            return;
        }
        var data = {
            NewPassword: NewPassword,
            CurrentPassword: CurrentPassword,
            ConfirmPassword: ConfirmPassword
        }
        AjaxCall("UserDetail", "ChangePassword", data, true);

    }

}
function UpdateProfile() {
    var FirstName = $("#FirstName").val();
    var LastName = $("#LastName").val();

    var PanCard = $("#PanCard").val();
    var AadharCard = $("#AadharCard").val();
    var BankAccountDetails = $("#BankAccountDetails").val();
    var AlternateMobileNo = $("#AlternateMobileno").val();
    var ProfilePhoto = $("#ProfilePhoto").val();

    var PanCard_text = $("#PanCard_text").val();
    var AadharCard_text = $("#AadharCard_text").val();
    var BankName = $("#BankName").val();
    var AccountHolderName = $("#AccountHolderName").val();
    var IFSC = $("#IFSC").val();
    var BranchName = $("#BranchName").val();

    var SoundSampleHindi1 = $("#SoundSampleHindi1").val();
    var SoundSampleHindi2 = $("#SoundSampleHindi2").val();
    var SoundSampleEnglish1 = $("#SoundSampleEnglish1").val();
    var SoundSampleEnglish2 = $("#SoundSampleEnglish2").val();
    var SoundSampleHindi1_Price = $("#SoundSampleHindi1_Price").val();
    var SoundSampleEnglish1_Price = $("#SoundSampleEnglish1_Price").val();
    var data = {
        FirstName: FirstName,
        LastName: LastName,
        PanCard: PanCard,
        AadharCard: AadharCard,
        BankAccountDetails: BankAccountDetails,
        AlternateMobileNo: AlternateMobileNo,
        ProfilePhoto: ProfilePhoto,
        PanCard_text: PanCard_text,
        AadharCard_text: AadharCard_text,
        BankName: BankName,
        AccountHolderName: AccountHolderName,
        IFSC: IFSC,
        BranchName: BranchName,
        SoundSampleHindi1: SoundSampleHindi1,
        SoundSampleHindi2: SoundSampleHindi2,
        SoundSampleEnglish1: SoundSampleEnglish1,
        SoundSampleEnglish2: SoundSampleEnglish2,
        SoundSampleHindi1_Price: SoundSampleHindi1_Price,
        SoundSampleEnglish1_Price: SoundSampleEnglish1_Price
    }
    AjaxCall("UserDetail", "UpdateProfile", data, true);
    callPage('/UserDetail/MyProfile', 'My Profile', '');
}
function SaveArtistSound() {
    var SoundName = $(".SoundName").val().trim();
    var SoundFileName = $("#" + uploadefileid).val().trim();
    var SoundType = $("#SoundType").val();

    AjaxCall("VoiceArtistSounds", "SaveArtistSound", data, false);
}
function DeleteSound(VoiceArtistSoundsId) {
    var data = {
        VoiceArtistSoundsId: VoiceArtistSoundsId
    };
    AjaxCall("VoiceArtistSounds", "DeleteSound", data, true);
}


function popupconfirm() {
    var data = fndata;
    if (fntype == "editUser") {
        //AjaxCallHtml("UserDetail", "NewUser", data, "Update User Details");
        callPage("/UserDetail/NewUser", "Update User Details", data.UserId);
    }
    if (fntype == "SaveScriptDetails") {
        AjaxCall("Orders", "SaveScriptDetails", data, true);
    }
    if (fntype == "InActiveUser") {
        AjaxCall("UserDetail", "InActiveUser", data, true);
    }
    if (fntype == "InActiveFaq") {
        //debugger;
        AjaxCall("Faq", "InActiveFaq", data, true);
        callPage("/Faq/Index", "Faq");
    }
    if (fntype == "InActiveSocialPage") {
        //debugger;
        AjaxCall("SocialPage", "InActiveSocialPage", data, true);
        callPage("/SocialPage/Index", "SocialPage");
    }
}
function ViewFile(filename) {
    var file = "";
    if (filename == null || filename == "") {
        file = 'File not found';
    }
    else
    {
        if (filename.indexOf(",") > 0) {
            var f = filename.split(",");
            for (var i = 0; i < f.length; i++) {
                if (f[i] != "") {
                    if ((/\.(gif|jpe?g|tiff?|png|webp|bmp)$/i).test(f[i].toLowerCase())) {
                        file += '<img src="/Uploads/' + f[i] + '" style="width:100%; height:300px;"/><br/>';
                    }
                    else {
                        file += '<a href="/Uploads/' + f[i] + '" target="_blank" style="color:#fcc012" download>Click here to download</a><br/>';
                    }
                }
            }
        }
        else {
            if ((/\.(gif|jpe?g|tiff?|png|webp|bmp)$/i).test(filename.toLowerCase())) {
                file = '<img src="/Uploads/' + filename + '" style="width:100%; height:300px;"/>';
            }
            else {
                file = '<a href="/Uploads/' + filename + '" target="_blank" style="color:#fcc012" download>Click here to download</a>';
            }
        }
    }
    $("#viewpopup1").find(".modal-body-content").html(file);
    $("#viewpopup1").find("#viewpopup1-title").html('REVIEW THIS FILE');
    $("#viewpopup1").modal();
}
function closePopup(btn) {

}

function SentOtp(MobileNo) {
    var data = {
        MobileNo: MobileNo

    };
    AjaxCall("OtpManagement", "SentOtp", data, true);
}


function editFaq() {
    if ($(".rbdfaq:checked").length > 0) {
        var data = {
            FaqId: $(".rbdfaq:checked").attr("data-id")
        };
        callPage("/Faq/Create", "Edit FAQ", data.FaqId);
    }

}
function deleteFaq() {
    if ($(".rbdfaq:checked").length > 0) {
        var data = {
            FaqId: $(".rbdfaq:checked").attr("data-id")
        };
        popupCV("Are you sure you want to delete?", "Confirmation", "InActiveFaq", data);
    }
    else {
        popupV("Please select Faq for Delete", "Warning", "");
    }
}
function _GetArtistDashboardByAdmin(UserId) {
    var data = { UserId: UserId };
    AjaxCallHtml1("Dashboard", "_GetArtistDashboardByAdmin", data, "Artist Dashboard");
}

function _FreelancerAGREEMENT_ForScriptWriter() {
    var data = {};
    AjaxCallHtml("Agreements", "_FreelancerAGREEMENT_ForScriptWriter", data, "Freelancer Agreement");
}
function _FreelancerWorkAGREEMENT_ForScriptWriter(OrderId) {
    var data = { OrderId: OrderId };
    AjaxCallHtml("Agreements", "_FreelancerWorkAGREEMENT_ForScriptWriter", data, "Freelancer Work Agreement");
}
function _FreelancerAGREEMENT_ForVoiceOver() {
    var data = {};
    AjaxCallHtml("Agreements", "_FreelancerAGREEMENT_ForVoiceOver", data, "Freelancer Agreement");
}
function _FreelancerWorkAGREEMENT_ForVoiceOver(OrderId) {
    var data = { OrderId: OrderId };
    AjaxCallHtml("Agreements", "_FreelancerWorkAGREEMENT_ForVoiceOver", data, "Freelancer Work Agreement");
}
function _FreelancerAGREEMENT_ForVideo() {
    var data = {};
    AjaxCallHtml("Agreements", "_FreelancerAGREEMENT_ForVideo", data, "Freelancer Agreement");
}
function _FreelancerWorkAGREEMENT_ForVideo(OrderId) {
    var data = { OrderId: OrderId };
    AjaxCallHtml("Agreements", "_FreelancerWorkAGREEMENT_ForVideo", data, "Freelancer Work Agreement");
}

function AcceptAgreement() {
    var data = {};
    AjaxCall("Agreements", "AcceptAgreement", data, true);
}
function AssignedProjectMe() {
    var data = {
        OrderId: OrderId,
        CustomerId: CustomerId
    };
    AjaxCall("OrderStatus", "ScriptAssigned", data, true);
    callPage('/Orders/NewJobs', 'New Job', '');
}

function SendOrderToArtist() {
    var ismailFound = false;
    $("#tbluserdetails").find(".chkartistdetails").each(function () {
        if ($(this).is(":checked")) {
            var data = {
                OrderId: $("#hdnOrderId").val(),
                UserId: $(this).attr("data-userid")
            };
            AjaxCall("OrderSentToArtist", "SendOrderToArtist", data, false);
            ismailFound = true;
        }

    });
    if (ismailFound) {
        $(".tbluserdetailsArea").hide();
        $(".sentMessage").html("Message Sent");
    }
}
function AcceptWorkAgreements(OrderId) {
    var data = {
        OrderId: OrderId
    };
    AjaxCall("OrderStatus", "AcceptWorkAgreements", data, false);
    popupV1('Project Assigned to you', 'JUST TO KNOW', "success");
}

function CheckPassword() {
    $("#NewPassword").attr("disabled", true);
    $("#ConfirmPassword").attr("disabled", true);

    var data = { Password: $("#CurrentPassword").val() };
    AjaxCall("UserDetail", "CheckPassword", data, false);
}
function ApproveDocument(UserId) {
    var data = { UserId: UserId };
    AjaxCall("UserDetail", "ApproveDocument", data, true);
}
function DeclineDocument(UserId) {
    var data = { UserId: UserId };
    AjaxCall("UserDetail", "DeclineDocument", data, true);
}

function SendInvoiceToAdmin(OrderId) {
    var data = { OrderId: OrderId };
    AjaxCall("OrderStatus", "SendInvoiceToAdmin", data, false);
    popupV1('Your Invoice is submitted', 'JUST TO KNOW', "success");
}

function GetAnnouncement() {
    var data = {};
    $.ajax({
        type: "POST",
        url: "/Announcement/GetMyAnnouncement",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {

            var html = '';
            for (var i = 0; i < data.length; i++) {
                html += '<div class="announcement-notification">' +
                '<h4>' + data[i].Subject + '<span class="an_Date">' + data[i].CreatedDate + '</span></h4>';
                if (data[i].FromUser != '') {

                    html += '<h5>From: ' + data[i].FromUser + '</h5>';
                }
                html += '<p>' + data[i].Description + '</p>' +
                //'<div class="btn btn-view " data-Announcementid="' + data[i].Announcementid + '" style="    border: 1px solid white">View</div>' +
                '<div class="btn btn-delete" data-Announcementid="' + data[i].Announcementid + '" style="    border: 1px solid white">Delete</div>' +
                '</div>';
            }

            $(".announcement-list").html(html);
            $(".btn-view").click(function () {
                ViewAnnouncement($(this).attr("data-Announcementid"));
            });
            $(".btn-delete").click(function () {
                DeleteAnnouncement($(this).attr("data-Announcementid"));
            });
        }
    });
}
function ViewAnnouncement(Announcementid) {
    var data = {
        Announcementid: Announcementid
    };
    $.ajax({
        type: "POST",
        url: "/Announcement/ViewAnnouncement",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);
        }
    });
}
function DeleteAnnouncement(Announcementid) {
    $(".btn-delete[data-announcementid='" + Announcementid + "']").parent(".announcement-notification").css("animation-name", "delete").css("animation-duration", "1s");
    setTimeout(function () {
        $(".btn-delete[data-announcementid='" + Announcementid + "']").parent(".announcement-notification").remove();
    }, 1000);
    var data = {
        Announcementid: Announcementid
    };
    $.ajax({
        type: "POST",
        url: "/Announcement/DeleteAnnouncement",
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log(data);

        }
    });
}

function ViewCustomerFeedback(OrderId)
{
    var data = { OrderId: OrderId };
    AjaxCallHtml("CustomerFeedback", "ViewCustomerFeedback", data, "Artist Sample");
}

function SaveSocialPage()
{
    var data = {
        SocialPAgeId: $("#SocialPAgeId").val().trim(),
        SocialPageName: $("#SocialPageName").val().trim(),
        SocialPageLink: $("#SocialPageLink").val().trim()
    }

    AjaxCall("SocialPage", "SaveSocialPage", data, true);
}
function editSocialPage() {
    if ($(".rbdfaq:checked").length > 0) {
        var data = {
            SocialPAgeId: $(".rbdfaq:checked").attr("data-id")
        };
        callPage("/SocialPage/Create", "Edit Social Page", data.SocialPAgeId);
    }

}
function deleteSocialPage() {
    if ($(".rbdfaq:checked").length > 0) {
        var data = {
            FaqId: $(".rbdfaq:checked").attr("data-id")
        };
        popupCV("Are you sure you want to delete?", "Confirmation", "InActiveSocialPage", data);
    }
    else {
        popupV("Please select Faq for Delete", "Warning", "");
    }
}