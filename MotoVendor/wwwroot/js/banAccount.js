document.addEventListener('DOMContentLoaded', function () {
    const fileUpload = document.getElementById('photo-select__file');
    const base64Input = document.getElementById('base64');
    const extensionInput = document.getElementById('extension');
    const form = document.querySelector('form');

    base64Input.value = 'defaultBase64Value'; 
    extensionInput.value = 'defaultExtension';

    fileUpload.addEventListener('change', function (event) {
        const file = event.target.files[0];

        if (file) {
            if (file.type.startsWith('image/')) {
                const reader = new FileReader();
                reader.onload = function (e) {
                   
                    base64Input.value = e.target.result.split(",")[1]; 
                    extensionInput.value = file.name.split('.').pop();
                };
                reader.readAsDataURL(file);
            } else {
                alert("Please select a valid image file.");

                fileUpload.value = '';
                base64Input.value = 'defaultBase64Value';
                extensionInput.value = 'defaultExtension';
            }
        } else {

            base64Input.value = 'defaultBase64Value';
            extensionInput.value = 'defaultExtension';
        }
    });

    form.addEventListener('submit', function (event) {

        if (!fileUpload.files.length) {
            base64Input.value = 'defaultBase64Value';
            extensionInput.value = 'defaultExtension';
        }
    });
});
