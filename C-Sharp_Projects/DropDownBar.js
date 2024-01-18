$(document).ready(function () {
    $(".rentalchoice").change(function () {
        if ($(".rentalchoice").val() == "Rental") {
            $(".RO").show();
            $(".REO").hide();
            $(".RRO").hide();
        }
        else if ($(".rentalchoice").val() == "RentalEquipment") {
            $(".RO").show();
            $(".REO").show();
            $(".RRO").hide();
        }
        else if ($(".rentalchoice").val() == "RentalRoom") {
            $(".RO").show();
            $(".REO").hide();
            $(".RRO").show();
        }
        else {
            $(".RO").hide();
            $(".REO").hide();
            $(".RRO").hide();
        }

    })
})