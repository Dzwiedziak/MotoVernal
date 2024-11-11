const toggleButton = document.getElementById('toggle-list');
const interestedList = document.querySelector('.interested-list');

toggleButton.addEventListener('click', function () {
    if (interestedList.style.display === 'none' || interestedList.style.display === '') {
        interestedList.style.display = 'flex';
        toggleButton.textContent = '▲';
    } else {
        interestedList.style.display = 'none';
        toggleButton.textContent = '▼';
    }
});