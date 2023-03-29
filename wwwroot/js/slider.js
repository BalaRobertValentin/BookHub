let sliderIndex = 0;
const sliderImages = document.querySelectorAll(".slider img");
const numberOfVisibleImages = 3;

function updateSlider() {
    sliderImages.forEach((img, index) => {
        img.style.transform = `translateX(-${sliderIndex * 100}%)`;
    });
}

document.getElementById("nextBtn").addEventListener("click", () => {
    sliderIndex++;
    if (sliderIndex > sliderImages.length - numberOfVisibleImages) {
        sliderIndex = 0;
    }
    updateSlider();
});

document.getElementById("prevBtn").addEventListener("click", () => {
    sliderIndex--;
    if (sliderIndex < 0) {
        sliderIndex = sliderImages.length - numberOfVisibleImages;
    }
    updateSlider();
});