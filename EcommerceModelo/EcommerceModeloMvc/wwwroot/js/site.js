function scrollCategorias(valor, classe) {
    document.getElementById(classe)
        .scrollBy({
            left: valor,
            behavior: 'smooth'
        });
}

window.addEventListener('scroll', function () {
    const navbar = document.querySelector('.site-navbar');
    if (navbar) {
        navbar.classList.toggle('scrolled', window.scrollY > 10);
    }
});
