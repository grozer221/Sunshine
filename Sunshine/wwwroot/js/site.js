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
            $("img#imgUploadPreview").attr("src", e.target.result).width(300);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

const copytext = (text) => {
    var $tmp = $("<input>");
    $("body").append($tmp);
    $tmp.val(text).select();
    document.execCommand("copy");
    $tmp.remove();
}