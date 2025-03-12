// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Function to convert text to lowercase except the first letter of the first word
function capitalizeText(input) {
    let text = input.value.trim();

    // Convert to lowercase
    text = text.toLowerCase();

    // Capitalize the first letter of the first word
    if (text.length > 0) {
        text = text.charAt(0).toUpperCase() + text.slice(1);
    }

    // Set the modified text back into the input field
    input.value = text;
}

// Listen for input events on the "Name" field and apply the text transformation
document.addEventListener("DOMContentLoaded", function () {
    const nameInput = document.getElementById("Name");
    if (nameInput) {
        nameInput.addEventListener("input", function () {
            capitalizeText(this);
        });
    }
});
