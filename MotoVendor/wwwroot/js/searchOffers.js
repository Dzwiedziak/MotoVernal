let advancedSearch = false
function switchAdvancedSearch() {
    advancedSearch = !advancedSearch
    const advancedKeyGroups = document.querySelectorAll('.advanced-key-group')
    advancedKeyGroups.forEach(function (group) {
        if (advancedSearch) {
            group.classList.remove('hidden');
            group.querySelectorAll('input, select').forEach(element => {
                element.disabled = false;
            });
        } else {
            group.classList.add('hidden')
            group.querySelectorAll('input, select').forEach(element => {
                element.disabled = true;
            });
        }
    });
}
document.addEventListener("DOMContentLoaded", function () {
    const submitButton = document.querySelector('#advanced-search')
    submitButton.addEventListener('click', function () {
        switchAdvancedSearch()
    });
});