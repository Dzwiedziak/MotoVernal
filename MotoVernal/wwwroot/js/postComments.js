document.addEventListener("DOMContentLoaded", () => {
    const listNews = document.querySelector('.listNews');
    const commmentModal = document.querySelector('#comment-modal');
    let currentCommentsContainer = null;

    const clearButton = document.getElementById('clear-filters-btn');

    clearButton.addEventListener('click', function () {
        document.getElementById('search').value = '';
        document.getElementById('datetime-from').value = '';
        document.getElementById('datetime-to').value = '';
        const checkboxes = document.querySelectorAll('input[type="checkbox"]');
        checkboxes.forEach(checkbox => checkbox.checked = false);
        const selects = document.querySelectorAll('.location');
        selects.forEach(select => select.value = '');

        const form = document.querySelector('form');
        if (form) {
            form.submit();
        }
    });

    function addElementToCommentModal(element) {
        clearCommentModal();
        commmentModal.appendChild(element);
        currentCommentInModal = element;
        element.style.display = 'block';
        listNews.style.filter = 'blur(4px)';
    }
    function clearCommentModal() {
        if(currentCommentsContainer)
            currentCommentsContainer.innerHTML = '';
        if(commmentModal.children[0]) {
            commmentModal.children[0].style.display = 'none';
            currentCommentsContainer.appendChild(commmentModal.children[0]);
        }
        currentCommentsContainer = null;
        commmentModal.innerHTML = '';
        listNews.style.filter = '';
    }
    const posts = document.querySelectorAll('.posts--post');
    posts.forEach(post => {
        const commentsBtn = post.querySelector('.comments-btn');
        const commentsContainer = post.querySelector('.commentsWindow-container');
        const commentWindow = post.querySelector('.commentsWindow');
        commentsBtn.addEventListener('click', (event) => {
            event.stopPropagation();
            addElementToCommentModal(commentWindow);
            currentCommentsContainer = commentsContainer;
        });
    });
    listNews.addEventListener('click', clearCommentModal);
});

