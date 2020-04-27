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

            // Fill data
            $.each(result, function (key, value) {
                var row = $("<tr>")

                if (typeof (value) != "object") {
                    row.append($("<td>").text(key))
                        .append($("<td>").text(value != "" || (typeof (value) == "boolean" || typeof (value) == "number") ? value : "N/A"));


                    $(table).append(row).show('slow');
                } else {
                    if (Object.keys(value).length != 0) {
                        $(table).append(
                            $("<tr>")
                                .append(
                                    $("<td>")
                                        .text(key)
                                        .attr("rowspan", Object.keys(value).length != 0 ? (Object.keys(value).length + 1) : undefined)
                                )
                        ).show('slow');


                        $.each(value, function (key2, value2) {

                            var row2 = $("<tr>")

                            if (typeof (key2) != "number")
                                row2
                                    .append($("<td>").text(key2))
                                    .append($("<td>").text(value2 != "" ? value2 : "N/A"))
                            else
                                row2
                                    .append($("<td>").text(value2 != "" ? value2 : "N/A"))

                            $(table).append(row2).show('slow');
                        });
                    }
                }
            }); 

            $("h5").text("File report:"); //Replace heading
            $('.output').append($("<table>").addClass("table").append(table)); //Append to page
        },
    });

    return false;
}