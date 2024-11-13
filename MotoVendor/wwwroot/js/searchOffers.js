let advancedSearch = false
function switchAdvancedSearch() {
    advancedSearch = !advancedSearch
    const advancedKeyGroup = document.querySelectorAll('.advanced-key-group')
    advancedKeyGroup.forEach(function (element) {
        if (advancedSearch) {
            element.classList.remove('hidden')
        } else {
            element.classList.add('hidden')
        }
    });
}
document.addEventListener("DOMContentLoaded", function () {
    const submitButton = document.querySelector('#advanced-search')
    submitButton.addEventListener('click', function () {
        switchAdvancedSearch()
    });
});