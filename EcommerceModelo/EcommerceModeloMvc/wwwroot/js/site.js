function scrollCategorias(valor, classe) {
    document.getElementById(classe)
        .scrollBy({
            left: valor,
            behavior: 'smooth'
        });
}