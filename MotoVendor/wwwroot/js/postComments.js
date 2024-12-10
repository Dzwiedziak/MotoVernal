document.addEventListener("DOMContentLoaded", () => {
    const listNews = document.querySelector('.listNews');
    const commmentModal = document.querySelector('#comment-modal');
    let currentCommentsContainer = null;
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

