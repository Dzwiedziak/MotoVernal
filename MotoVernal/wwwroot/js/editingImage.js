document.addEventListener('DOMContentLoaded', function () {
    const fileUpload = document.getElementById('photo-select__file');
    const base64Input = document.getElementById('base64');
    const extensionInput = document.getElementById('extension');
    const previewContainer = document.querySelector('.current-image');
    const previewImage = previewContainer ? previewContainer.querySelector('img') : null;

    const MAX_FILE_SIZE = 4 * 1024 * 1024;

    if (base64Input.value === '') {
        base64Input.value = 'defaultBase64Value';
    }
    if (extensionInput.value === '') {
        extensionInput.value = 'defaultExtension';
    }
    function updateImagePreview(base64, extension) {
        if (base64 !== 'defaultBase64Value' && extension) {
            if (previewImage) {
                previewImage.src = `data:image/${extension};base64,${base64}`;
                previewContainer.style.display = 'block';
            }
        } else if (previewImage) {
            previewImage.src = '';
            previewContainer.style.display = 'none';
        }
    }

    updateImagePreview(base64Input.value, extensionInput.value);

    fileUpload.addEventListener('change', function (event) {
        const file = event.target.files[0];

        if (file) {
            if(file.size > MAX_FILE_SIZE) {
                alert("The file size exceeds the maximum limit of 4MB.");
                fileUpload.value = '';
                base64Input.value = 'defaultBase64Value';
                extensionInput.value = 'defaultExtension';
                updateImagePreview(base64Input.value, extensionInput.value);
                return;
            }
            if (file.type.startsWith('image/')) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    base64Input.value = e.target.result.split(",")[1];
                    extensionInput.value = file.name.split('.').pop();
                    updateImagePreview(base64Input.value, extensionInput.value);
                };
                reader.readAsDataURL(file);
            } else {
                alert("Please select a valid image file.");
                fileUpload.value = '';
                base64Input.value = 'defaultBase64Value';
                extensionInput.value = 'defaultExtension';
                updateImagePreview(base64Input.value, extensionInput.value);
            }
        } else {
            base64Input.value = 'defaultBase64Value';
            extensionInput.value = 'defaultExtension';
            updateImagePreview(base64Input.value, extensionInput.value);
        }
    });
});
