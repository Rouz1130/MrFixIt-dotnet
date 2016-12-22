var test = "it works";

//claim job function

$(function () {

    $(".claimjob").click(function () {
        alert("clicked");
        var jobid = $(this).siblings('.ThisJobId').val();
        var username = $('.ThisUserName-' + jobid).val();
        $(".HideAfterClick-" + jobid).hide();

        $.ajax({
            url: "/Jobs/Claim",
            data: { jobId: jobid, userName: username },
            type: 'GET',
            success: function (result) {
                $('.ClaimedJob-' + jobid).html(result);
            }
        });
    });
});