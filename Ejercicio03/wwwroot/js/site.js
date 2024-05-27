$(document).ready(function () {
    $('#datatableCheating').DataTable({
        "scrollY": "450",
        "scrollCollapse": false,
        "paging": true, 
    });
});

/*Menú de la barra de navegación*/
const navbarElement = document.querySelector(".nav");
const cortinilla = document.querySelector(".cortinilla");
const hamburguerElement = document.querySelector(".hamburguer");

hamburguerElement.addEventListener("click", () => {
    navbarElement.classList.toggle("nav--open");
    cortinilla.classList.toggle("cortinilla--open");
    hamburguerElement.classList.toggle("hamburguer--open");
});

navbarElement.addEventListener("click", () => {
    navbarElement.classList.remove("nav--open");
    cortinilla.classList.remove("cortinilla--open");
    hamburguerElement.classList.remove("hamburguer--open");
});

