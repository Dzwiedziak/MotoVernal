function mapReactionToNumber(reactionType) {
    switch (reactionType) {
        case 'like':
            return 0;
        case 'dislike':
            return 1;
        default:
            throw new Error('Invalid reaction type');
    }
}
async function addReaction(userId, responseId, reactionType) {
    try {
        const fetchBody = {
            "UserId": userId,
            "TopicResponseId": responseId,
            "ReactionType": mapReactionToNumber(reactionType)
        };
        const response = await fetch(`/api/topics/responses/reactions`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(fetchBody)
        });
        if (response.ok) {
            return await response.text();
        }
        throw new Error("Reaction adding failed");
    } catch (error) {
        console.error("Error adding reaction:", error);
        return null;
    }
}

async function changeReaction(reactionId, reactionType) {
    try {
        const jsonBody = mapReactionToNumber(reactionType);
        const response = await fetch(`/api/topics/responses/reactions/${reactionId}/type`, {
            method: 'PATCH',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify( jsonBody )
        });

        if (response.ok) {
            return await response.text();
        }
        throw new Error("Reaction changing failed");
    } catch (error) {
        console.error("Error changing reaction:", error);
        return null;
    }
}

document.addEventListener("DOMContentLoaded", () => {
    const userId = document.querySelector('.user-id').value;
    const comments = document.querySelectorAll('.comments-content');

    comments.forEach(comment => {
        const responseId = comment.getAttribute('response-id');
        let reactionState = comment.getAttribute('reaction-state') || 'none';
        let reactionId = comment.getAttribute('reaction-id');
        let likeCount = parseInt(comment.querySelector('.like-count')?.textContent) || 0;
        let dislikeCount = parseInt(comment.querySelector('.dislike-count')?.textContent) || 0;

        const likeIcon = comment.querySelector('.like-icon');
        const dislikeIcon = comment.querySelector('.dislike-icon');
        const likeCountElement = comment.querySelector('.like-count');
        const dislikeCountElement = comment.querySelector('.dislike-count');

        async function changeState(nextReactionState) {
            try {
                if (reactionState === 'none') {
                    const response = await addReaction(userId, responseId, nextReactionState);
                    if (response != null) {
                        reactionId = response;
                        reactionState = nextReactionState;
                        if (nextReactionState === 'like') likeCount++;
                        else if (nextReactionState === 'dislike') dislikeCount++;
                    }
                } else if (reactionState === 'like' && nextReactionState === 'dislike') {
                    const response = await changeReaction(reactionId, nextReactionState);
                    if (response != null) {
                        reactionState = nextReactionState;
                        likeCount--;
                        dislikeCount++;
                    }
                } else if (reactionState === 'dislike' && nextReactionState === 'like') {
                    const response = await changeReaction(reactionId, nextReactionState);
                    if (response != null) {
                        reactionState = nextReactionState;
                        dislikeCount--;
                        likeCount++;
                    }
                }
                updateUI();
            } catch (error) {
                console.error("Error updating reaction state:", error);
            }
        }

        function updateUI() {
            if (reactionState === 'like') {
                likeIcon.classList.add('like-icon--liked');
                dislikeIcon.classList.remove('dislike-icon--disliked');
            } else if (reactionState === 'dislike') {
                dislikeIcon.classList.add('dislike-icon--disliked');
                likeIcon.classList.remove('like-icon--liked');
            } else {
                likeIcon.classList.remove('like-icon--liked');
                dislikeIcon.classList.remove('dislike-icon--disliked');
            }
            if (likeCountElement) likeCountElement.textContent = likeCount;
            if (dislikeCountElement) dislikeCountElement.textContent = dislikeCount;
        }

        likeIcon.addEventListener('click', () => changeState('like'));
        dislikeIcon.addEventListener('click', () => changeState('dislike'));
    });
});