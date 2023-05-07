function validateQuantityInput(input) {
    if (input.value < 1) {
        input.value = 1;
    }
}