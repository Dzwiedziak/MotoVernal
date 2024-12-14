document.addEventListener('DOMContentLoaded', function () {
    const textarea = document.getElementById('comment-textarea');
    const buttons = document.getElementById('new-comment-action');
    const cancelBtn = document.getElementById('cancel-btn');
    const loadImageLabel = document.getElementById('load_image_in_add')
    const lineAdd = document.getElementById('line_in_add')

    const commentsInLoad = document.querySelectorAll('.editable-content');  // Zbierz wszystkie komentarze

    commentsInLoad.forEach(comment => {
        
        let content = comment.innerHTML.trim();

        content = content.replace(/\n/g, '<br>');

        comment.innerHTML = content;
    });

    const MAX_FILE_SIZE = 4 * 1024 * 1024;    


    const elementsWithFileLoadings = document.querySelectorAll('.comments-content, .add-comment-content');
    elementsWithFileLoadings.forEach(element => {
        const fileUpload = element.querySelector('.photo-select__file');
        const base64Input = element.querySelector('.base64');
        const extensionInput = element.querySelector('.extension');
        const previewContainer = element.querySelector('.current-image');
        const previewImage = previewContainer ? previewContainer.querySelector('img') : null;

        const loadImageLabel = element.querySelector('.load-image-label');
        loadImageLabel.addEventListener('click', () => {
            fileUpload.click();
        })
            
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
                if (file.size > MAX_FILE_SIZE) {
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
            updateImagePreview(base64Input.value, extensionInput.value);
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
        updateImagePreview(base64Input.value, extensionInput.value);
    });

    const comments = document.querySelectorAll('.comments-content');

    comments.forEach(comment => {
        const editButton = comment.querySelector('.edit-comment-btn');
        let editableSpan = comment.querySelector('.editable-content');
        const cancelEditButton = comment.querySelector('.cancel-edit-btn');
        const loadImageLabel = comment.querySelector('.load-image-label');
        const commentForm = comment.querySelector('.editable-post-content');

        let isEditing = false;
        let editTextarea = comment.querySelector('.editable-post-content__description');
        editTextarea.style.display = 'none';
        let originalContent;

        editButton.addEventListener('click', function () {
            if (!isEditing) {

                originalContent = editableSpan.innerHTML.trim();

                const currentContent = editableSpan.innerHTML.replace(/<br>/g, "\n").trim();
                editTextarea.value = currentContent;
                editTextarea.style.width = '100%';

                const computedHeight = window.getComputedStyle(editableSpan).height;
                editTextarea.style.height = computedHeight;

                editTextarea.addEventListener('input', function () {
                    this.style.height = 'auto';
                    this.style.height = `${this.scrollHeight}px`;
                });

                editTextarea.style.display = 'block';
                editableSpan.style.display = 'none';

                loadImageLabel.style.display = 'inline-block';

                editButton.querySelector('img').src = "/images/accept.png";
                isEditing = true;

                cancelEditButton.style.display = 'flex';


            } else {

                commentForm.submit();
                const newContent = editTextarea.value.trim().split('\n').join('<br>');
                

                editableSpan.innerHTML = newContent;

                editableSpan.style.display = 'block';
                editTextarea.style.display = 'none';

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

            editButton.querySelector('img').src = '/images/draw.png';
        });
    });
});
