$(document).ready(function () {
    $('#datatableCheating').DataTable({
        "scrollY": "450",
        "scrollCollapse": true,
        "paging": true, 
    });
});

/*Menú de la barra de navegación*/
const navbarElement = document.querySelector(".nav");
const hamburguerElement = document.querySelector(".hamburguer");

hamburguerElement.addEventListener("click", () => {
    navbarElement.classList.toggle("nav--open");
    hamburguerElement.classList.toggle("hamburguer--open");
});

navbarElement.addEventListener("click", () => {
    navbarElement.classList.remove("nav--open");
    hamburguerElement.classList.remove("hamburguer--open");
});
