document.getElementById('imageUpload').addEventListener('change', readURL, true);
function readURL() {
    const file = document.getElementById("imageUpload").files[0];
    const reader = new FileReader();
    reader.onloadend = function () {
        document.getElementById('clock').style.backgroundImage = "url(" + reader.result + ")";
    }
    if (file) {
        reader.readAsDataURL(file);
    } else {
    }
}