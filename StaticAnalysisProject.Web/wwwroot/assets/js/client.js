function sendData() {
    var formData = new FormData();
    var file = $('#file').prop('files')[0];

    formData.append('file', file);

    $.ajax({
        url: "FileUpload",
        type: "POST",
        contentType: false,
        processData: false,
        dataType: "json",
        data: formData,
        beforeSend: function () {
            $("form").slideUp(function () {
                $(".spinner").slideDown();
            });
        },
        success: function (result) {

            $(".spinner").slideUp();    // Hide loading

            var table = $("<tbody>");   //Prepare table
            var data = jQuery.parseJSON(result[0]);
            var data2 = jQuery.parseJSON(result[1]);

            // Fill data
            $.each(data, function (key, value) {
                var row = $("<tr>")

                if (typeof (value) != "object") {
                    row.append($("<td>").text(key))
                        .append($("<td>")
                            .attr("colspan", 2)
                            .text(value != "" || (typeof (value) == "boolean" || typeof (value) == "number") ? value : "N/A"));

                    $(table).append(row).show('slow');
                } else {
                    if (value != undefined && Object.keys(value).length != 0) {
                        $(table).append(
                            $("<tr>")
                                .append(
                                    $("<td>")
                                        .text(key)
                                        .attr("rowspan", Object.keys(value).length != 0 ? (Object.keys(value).length + 1) : undefined)
                                )
                        ).show('slow');

                        $.each(value, function (key2, value2) {

                            var row2 = $("<tr>").addClass("data-row")

                            if (key == "Sections")
                                row2
                                    .append($("<td>").text(value2.Key != "" ? value2.Key : "N/A"))
                                    .append($("<td>").text(value2.Value != "" ? value2.Value.join(", ") : "N/A"))
                            else if (typeof (key2) != "number")
                                if (typeof (value2) == "object") {
                                    row2
                                        .append($("<td>").text(key2))
                                        .append($("<td>").text(value2 != "" ? value2.join(", ") : "N/A"))
                                } else {
                                    row2
                                        .append($("<td>").text(key2))
                                        .append($("<td>").text(value2 != "" ? value2 : "N/A"))
                                }
                            else
                                row2
                                    .append($("<td>").attr("colspan", 2).text(value2 != "" ? value2 : "N/A"))

                            $(table).append(row2).show('slow');
                        });
                    }
                }
            });

            var isMalware = "File was not detected as malware!";
            if (data2.IsMalware) {
                isMalware = "File was detected as malware!";
            }

            console.log(data2);

            var color = "bg-danger";
            var image = "./assets/images/emoji/scare.svg";
            if (data2.Probability > 0.2 && !data2.IsMalware) {
                color = "bg-warning";
                image = "./assets/images/emoji/not_sure.svg";
            } else if (data.Probability > 0.8 && !data2.IsMalware) {
                color = "bg-success";
                image = "./assets/images/emoji/smile.svg";
            }


            //Append ML
            var card = $("<div>").addClass("container card mb-3 " + color)
                .append($("<div>").addClass("row  justify-content-center")
                    .append(
                        $("<div>").addClass("col-9 card-body")
                            .append(
                                $("<h3>").addClass("card-title").text(isMalware)
                            )
                            .append(
                                $("<p>").addClass("card-text").text("Predicted probability is " + (Math.round(data2.Probability * 10000) / 100) + " %")
                            )
                    ).prepend(
                        $("<div>").addClass("col-3 card-image")
                            .append($("<img>").attr("src", image).attr("alt", "").addClass("image-emoji"))
                    )
                )

            $("h5").text("File report:"); //Replace heading
            $('.output').append($("<table>").addClass("table").append(table)); //Append to page
            $('.output').prepend(card);
        },
    });

    return false;
}