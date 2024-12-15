document.addEventListener('DOMContentLoaded', function () {
    const selectElement = document.getElementById('sort_by');
    const sortByValue = new URLSearchParams(window.location.search).get('sortBy');

    if (sortByValue) {
        selectElement.value = sortByValue;
    }
});