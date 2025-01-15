document.addEventListener('DOMContentLoaded', function () {
    const clearButton = document.getElementById('clear-filters-btn');

    clearButton.addEventListener('click', function () {
        document.getElementById('search').value = '';
        const checkboxes = document.querySelectorAll('input[type="checkbox"]');
        checkboxes.forEach(checkbox => checkbox.checked = false);

        const form = document.querySelector('form');
        if (form) {
            form.submit();
        }
    });
});
