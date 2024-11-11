const uploadImage = document.getElementById('profil-img');
const fileUpload = document.getElementById('file-upload');

uploadImage.addEventListener('click', function () {
    fileUpload.click();
});

fileUpload.addEventListener('change', function (event) {
    const file = event.target.files[0];
    if (file && file.type.startsWith('image/')) {
        const reader = new FileReader();
        reader.onload = function (e) {
            uploadImage.src = e.target.result;
        };
        reader.readAsDataURL(file);
    } else {
        alert("Please select a valid image file.");
    }
});
