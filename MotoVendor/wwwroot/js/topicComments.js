var modal = document.getElementById("commentsWindow");

var commentsBtns = document.querySelectorAll(".comments-btn");

var mainContent = document.querySelector('.listNews');

let scrollPosition = 0;

commentsBtns.forEach(function (btn) {

    btn.onclick = function () {
        scrollPosition = window.pageYOffset || document.documentElement.scrollTop;

        document.body.style.overflow = 'hidden';
        document.body.style.top = `-${scrollPosition}px`;

        modal.style.display = "flex";
        mainContent.classList.add('blur');
    }
});

const textarea = document.getElementById('comment-textarea');
const buttons = document.getElementById('new-comment-action');
const cancelBtn = document.getElementById('cancel-btn');

textarea.addEventListener('input', function () {
    this.style.height = 'auto';
    this.style.height = `${this.scrollHeight}px`;
    if (textarea.value.trim() !== '') {
        buttons.style.display = 'flex';
    } else {
        buttons.style.display = 'none';
    }
});
cancelBtn.addEventListener('click', function () {
    textarea.value = '';
    textarea.style.height = `auto`;
    buttons.style.display = 'none';
});

const comments = document.querySelectorAll('.comments-content');

comments.forEach(comment => {
    const editButton = comment.querySelector('.edit-comment-btn');
    let editableSpan = comment.querySelector('.editable-content');
    const cancelEditButton = comment.querySelector('.cancel-edit-btn');

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

        cancelEditButton.style.display = 'none';

        editButton.querySelector('img').src = '/images/draw.png';
    });
});
document.getElementById('image-upload').addEventListener('change', function () {
    var file = this.files[0];
    var fileName = file ? file.name : 'Add image';
    if (file && file.type.startsWith('image/')) {
        document.getElementById('file-label').textContent = fileName;
    } else {
        alert("Please select a valid image file.");
        this.value = "";
        label.textContent = "Add image";
    }

});




s