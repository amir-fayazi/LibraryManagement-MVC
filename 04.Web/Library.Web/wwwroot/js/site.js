document.addEventListener("DOMContentLoaded", () => {
    const input = document.getElementById("CoverFile");
    const preview = document.getElementById("cover-preview");
    if (input && preview) {
        input.addEventListener("change", () => {
            const file = input.files && input.files[0];
            if (file && file.type.startsWith("image/")) {
                preview.src = URL.createObjectURL(file);
            }
        });
    }
});
