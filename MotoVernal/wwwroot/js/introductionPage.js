let pageNumber = 0;
const pageMaxIndex = 2;
const navStates = [false, true, true];
let menuState = false

function switchMenuVisibility() {
    menuState = !menuState;
    const menus = document.querySelectorAll('.introduction-page__menu')
    menus.forEach(function (menu) {
        if (menuState) {
            menu.classList.add('introduction-page__menu--visible')
        } else {
            menu.classList.remove('introduction-page__menu--visible')
        }
    });
}

function increasePage() {
    if (pageNumber < pageMaxIndex) {
        pageNumber++;
        slideDownPage()
        updateNavState();
        updateCarouselTransform();
    }
}

function decreasePage() {
    if (pageNumber > 0) {
        pageNumber--;
        slideDownPage()
        updateNavState();
        updateCarouselTransform();
    }
}

function updateCarouselTransform() {
    const carousels = document.querySelectorAll('.introduction-page__carousel');
    carousels.forEach(function (carousel) {
        carousel.style.transform = `translateX(-${pageNumber * 100}%)`;
    });
}

function updateNavState() {
    const navItems = document.querySelectorAll('.introduction-page__nav');

    navItems.forEach((navItem) => {
        if (navStates[pageNumber]) {
            navItem.classList.add('invert');
        } else {
            navItem.classList.remove('invert');
        }
    });
}

function slideUpPage() {
    const slideUpPages = document.querySelectorAll('.slide-up-page');
    slideUpPages.forEach(function (page) {
        page.style.transform = 'translateY(-100%)';
    });

    const navItems = document.querySelectorAll('.introduction-page__nav');
    navItems.forEach((navItem) => {
        navItem.classList.add('invert');
    });
}

function slideDownPage() {
    const slideUpPages = document.querySelectorAll('.slide-up-page');
    slideUpPages.forEach(function (page) {
        page.style.transform = 'translateY(0%)';
    });

    const navItems = document.querySelectorAll('.introduction-page__nav');
    navItems.forEach((navItem) => {
        navItem.classList.remove('invert');
    });
}

window.addEventListener('wheel', (event) => {
    if (event.deltaY > 0) {
        slideUpPage();
    } else {
        slideDownPage()
    }
});

document.addEventListener('DOMContentLoaded', function () {
    setTimeout(() => {
        const video = document.getElementById("landing-page__bg-video");
        if (video) {
            video.play();
        }
    }, 4000);

    const leftButtons = document.querySelectorAll('.move-left');
    leftButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            decreasePage();
        });
    });

    const rightButtons = document.querySelectorAll('.move-right');
    rightButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            increasePage();
        });
    });

    const menuButtons = document.querySelectorAll('.introduction-page__whole-website-nav')
    menuButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            switchMenuVisibility()
        });
    });
});