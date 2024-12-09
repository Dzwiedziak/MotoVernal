document.addEventListener('DOMContentLoaded', function () {
    const fileUpload = document.getElementById('photo-select__file');
    const base64Input = document.getElementById('base64');
    const extensionInput = document.getElementById('extension');
    const form = document.querySelector('form');

    // Ustawienie wartości domyślnych w ukrytych polach, jeśli istnieją dane
    if (base64Input.value === 'defaultBase64Value') {
        base64Input.value = base64Input.value || ''; // Pozostaw puste jeśli brak Base64
    }
    if (extensionInput.value === 'defaultExtension') {
        extensionInput.value = extensionInput.value || ''; // Pozostaw puste jeśli brak Extension
    }

    // Obsługuje zmianę pliku
    fileUpload.addEventListener('change', function (event) {
        const file = event.target.files[0];

        if (file) {
            if (file.type.startsWith('image/')) {
                const reader = new FileReader();
                reader.onload = function (e) {
                    // Przypisanie nowych danych do ukrytych pól (Base64 i Extension)
                    base64Input.value = e.target.result.split(",")[1]; // Base64 (bez nagłówka)
                    extensionInput.value = file.name.split('.').pop(); // Rozszerzenie pliku
                };
                reader.readAsDataURL(file);
            } else {
                alert("Please select a valid image file.");

                // Resetowanie pliku
                fileUpload.value = '';
            }
        }
    });

    // Przy wysyłaniu formularza, jeśli nie wybrano nowego pliku, pozostaw poprzednie wartości
    form.addEventListener('submit', function (event) {
        // Jeśli nie wybrano nowego pliku, pozostaw domyślne wartości (stary obraz)
        if (!fileUpload.files.length) {
            if (base64Input.value === 'defaultBase64Value') {
                base64Input.value = ''; // Zostaw poprzedni Base64, jeśli plik nie został zmieniony
            }
            if (extensionInput.value === 'defaultExtension') {
                extensionInput.value = ''; // Zostaw poprzednie rozszerzenie, jeśli plik nie został zmieniony
            }
        }
    });
});
