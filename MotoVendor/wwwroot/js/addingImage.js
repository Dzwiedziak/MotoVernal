document.addEventListener('DOMContentLoaded', function () {
    const textarea = document.getElementById('comment-textarea');
    const buttons = document.getElementById('new-comment-action');
    const cancelBtn = document.getElementById('cancel-btn');
    const loadImageLabel = document.getElementById('load_image_in_add')
    const lineAdd = document.getElementById('line_in_add')

    const fileUpload = document.getElementById('photo-select__file');
    const base64Input = document.getElementById('base64');
    const extensionInput = document.getElementById('extension');
    const previewContainer = document.getElementById('current-image');
    const previewImage = previewContainer ? previewContainer.querySelector('img') : null;

    const commentsInLoad = document.querySelectorAll('.editable-content'); 

    commentsInLoad.forEach(comment => {
        let content = comment.textContent.trim(); 
        comment.textContent = content;
    });


    const MAX_FILE_SIZE = 4 * 1024 * 1024;

    if (base64Input.value === '') {
        base64Input.value = 'defaultBase64Value';
    }
    if (extensionInput.value === '') {
        extensionInput.value = 'defaultExtension';
    }

    function updateImagePreview(base64, extension, prevImage, prevContainer) {
        if (base64 !== 'defaultBase64Value' && extension) {
            if (prevImage) {
                prevImage.src = `data:image/${extension};base64,${base64}`;
                prevContainer.style.display = 'block';
            }
        } else if (prevImage) {
            prevImage.src = '';
            prevContainer.style.display = 'none';
        }
    }
    updateImagePreview(base64Input.value, extensionInput.value, previewImage, previewContainer);

    fileUpload.addEventListener('change', function (event) {
        const file = event.target.files[0];

        if (file) {
            if (file.size > MAX_FILE_SIZE) {
                alert("The file size exceeds the maximum limit of 4MB.");
                fileUpload.value = '';
                base64Input.value = 'defaultBase64Value';
                extensionInput.value = 'defaultExtension';
                updateImagePreview(base64Input.value, extensionInput.value, previewImage, previewContainer);
                return;
            }
            if (file.type.startsWith('image/')) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    base64Input.value = e.target.result.split(",")[1];
                    extensionInput.value = file.name.split('.').pop();
                    updateImagePreview(base64Input.value, extensionInput.value, previewImage, previewContainer);
                };
                reader.readAsDataURL(file);
            } else {
                alert("Please select a valid image file.");
                fileUpload.value = '';
                base64Input.value = 'defaultBase64Value';
                extensionInput.value = 'defaultExtension';
                updateImagePreview(base64Input.value, extensionInput.value, previewImage, previewContainer);
            }
        } else {
            base64Input.value = 'defaultBase64Value';
            extensionInput.value = 'defaultExtension';
            updateImagePreview(base64Input.value, extensionInput.value, previewImage, previewContainer);
        }
    });
    textarea.addEventListener('input', function () {
        this.style.height = 'auto';
        this.style.height = `${this.scrollHeight}px`;
        if (textarea.value.trim() !== '') {
            buttons.style.display = 'flex';
            loadImageLabel.style.display = 'inline-block';
            lineAdd.style.display = 'block';
        } else {
            buttons.style.display = 'none';
            loadImageLabel.style.display = 'none';
            lineAdd.style.display = 'none';
            fileUpload.value = '';
            base64Input.value = 'defaultBase64Value';
            extensionInput.value = 'defaultExtension';
            updateImagePreview(base64Input.value, extensionInput.value, previewImage, previewContainer);
        }
    });
    cancelBtn.addEventListener('click', function () {
        textarea.value = '';
        textarea.style.height = `auto`;
        buttons.style.display = 'none';
        loadImageLabel.style.display = 'none';
        lineAdd.style.display = 'none';
        fileUpload.value = '';
        base64Input.value = 'defaultBase64Value';
        extensionInput.value = 'defaultExtension';
        updateImagePreview(base64Input.value, extensionInput.value, previewImage, previewContainer);
    });

    const comments = document.querySelectorAll('.comments-content');

    comments.forEach(comment => {
        const editButton = comment.querySelector('.edit-comment-btn');
        let editableSpan = comment.querySelector('.editable-content');
        const cancelEditButton = comment.querySelector('.cancel-edit-btn');
        const loadImageLabel = comment.querySelector('.load-image-label');

        const fileUpload = comment.querySelector('.photo-select__file');
        const base64Input = comment.querySelector('.base64');
        const extensionInput = comment.querySelector('.extension');
        const previewContainer = comment.querySelector('.current-image');
        const previewImage = previewContainer ? previewContainer.querySelector('img') : null;

        let isEditing = false;
        let editTextarea;
        let originalContent;

        editButton.addEventListener('click', function () {
            if (!isEditing) {

                originalContent = editableSpan.innerHTML.trim();

                const currentContent = editableSpan.innerHTML.replace(/<br>/g, "\n").trim();
                editTextarea = document.createElement('textarea');
                editTextarea.value = currentContent;
                editTextarea.style.width = '100%';

                const computedHeight = window.getComputedStyle(editableSpan).height;
                editTextarea.style.height = computedHeight;

                editTextarea.addEventListener('input', function () {
                    this.style.height = 'auto';
                    this.style.height = `${this.scrollHeight}px`;
                });

                editableSpan.parentNode.replaceChild(editTextarea, editableSpan);

                loadImageLabel.style.display = 'inline-block';

                editButton.querySelector('img').src = "/images/accept.png";
                isEditing = true;

                cancelEditButton.style.display = 'flex';
                fileUpload.addEventListener('change', function (event) {
                    const file = event.target.files[0];
                    if (file) {
                        if (file.size > MAX_FILE_SIZE) {
                            alert("The file size exceeds the maximum limit of 4MB.");
                            fileUpload.value = '';
                            base64Input.value = 'defaultBase64Value';
                            extensionInput.value = 'defaultExtension';
                            updateImagePreview(base64Input.value, extensionInput.value, previewImage, previewContainer);
                            return;
                        }
                        if (file.type.startsWith('image/')) {
                            const reader = new FileReader();
                            reader.onload = function (e) {
                                base64Input.value = e.target.result.split(",")[1];
                                extensionInput.value = file.name.split('.').pop();
                                updateImagePreview(base64Input.value, extensionInput.value, previewImage, previewContainer);
                            };
                            reader.readAsDataURL(file);
                        } else {
                            alert("Please select a valid image file.");
                            fileUpload.value = '';
                            base64Input.value = 'defaultBase64Value';
                            extensionInput.value = 'defaultExtension';
                            updateImagePreview(base64Input.value, extensionInput.value, previewImage, previewContainer);
                        }
                    }
                });
            } else {

                const newContent = editTextarea.value.trim().split('\n').join('<br>');

                editableSpan = document.createElement('span');
                editableSpan.className = 'editable-content';
                editableSpan.innerHTML = newContent;

                editTextarea.parentNode.replaceChild(editableSpan, editTextarea);

                editButton.querySelector('img').src = '/images/draw.png';

                loadImageLabel.style.display = 'none';

                isEditing = false;

                cancelEditButton.style.display = 'none';
            }
        });

        cancelEditButton.addEventListener('click', function () {

            editableSpan = document.createElement('span');
            editableSpan.className = 'editable-content';
            editableSpan.innerHTML = originalContent;

            editTextarea.parentNode.replaceChild(editableSpan, editTextarea);
            isEditing = false;
            loadImageLabel.style.display = 'none';
            cancelEditButton.style.display = 'none';
            fileUpload.value = '';
            base64Input.value = 'defaultBase64Value';
            extensionInput.value = 'defaultExtension';
            updateImagePreview(base64Input.value, extensionInput.value, previewImage, previewContainer);

            editButton.querySelector('img').src = '/images/draw.png';
        });
    });
});
