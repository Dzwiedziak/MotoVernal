const textarea = document.getElementById('comment-textarea');
const buttons = document.getElementById('new-comment-action');
const cancelBtn = document.getElementById('cancel-btn');
const loadImageLabel = document.getElementById('load_image_in_add')
const lineAdd = document.getElementById('line_in_add')
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
    }
});
cancelBtn.addEventListener('click', function () {
    textarea.value = '';
    textarea.style.height = `auto`;
    buttons.style.display = 'none';
    loadImageLabel.style.display = 'none';
    lineAdd.style.display = 'none';
});

const comments = document.querySelectorAll('.comments-content');

comments.forEach(comment => {
    const editButton = comment.querySelector('.edit-comment-btn');
    let editableSpan = comment.querySelector('.editable-content');
    const cancelEditButton = comment.querySelector('.cancel-edit-btn');
    const loadImageLabel = comment.querySelector('.load-image-label');

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

        editButton.querySelector('img').src = '/images/draw.png';
    });
});

