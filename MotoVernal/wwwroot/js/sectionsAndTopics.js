document.addEventListener('DOMContentLoaded', function () {
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
});
