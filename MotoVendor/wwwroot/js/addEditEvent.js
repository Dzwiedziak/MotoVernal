document.addEventListener('DOMContentLoaded', function () {
    const fileUpload = document.getElementById('photo-select__file');
    const base64Input = document.getElementById('base64');
    const extensionInput = document.getElementById('extension');
    const form = document.querySelector('form');

    if (base64Input.value === 'defaultBase64Value') {
        base64Input.value = base64Input.value || ''; 
    }
    if (extensionInput.value === 'defaultExtension') {
        extensionInput.value = extensionInput.value || ''; 
    }

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

                // Resetowanie pliku
                fileUpload.value = '';
            }
        }
    });

    form.addEventListener('submit', function (event) {
        if (!fileUpload.files.length) {
            if (base64Input.value === 'defaultBase64Value') {
                base64Input.value = ''; 
            }
            if (extensionInput.value === 'defaultExtension') {
                extensionInput.value = '';
            }
        }
    });
});
