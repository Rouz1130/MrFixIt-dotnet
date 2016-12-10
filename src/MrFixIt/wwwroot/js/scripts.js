var test = "it works";


$(".job-claim").submit(function (event) {
    event.preventDefault();
    $.ajax({
        url: "/Claim/Jobs",
        type: "POST",
        dataType: "json",
        data: $(this).serialize(),
        success: function (result) {
            $("#Claimed").html(result);
        }
    });
});