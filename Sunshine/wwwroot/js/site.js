$(function () {
    if ($("div.notification").length) {
        setTimeout(() => {
            $("div.notification").fadeOut();
        }, 3000)
    }
})

const readURL = (input) => {
    if (input.files && input.files[0]) {
        let reader = new FileReader();

        reader.onload = function (e) {
            $("img#imgUploadPreview").attr("src", e.target.result).width(200).height(200);
        };

        reader.readAsDataURL(input.files[0]);
    }
}