let x = document.querySelector('.searchDiv input');
let y = document.querySelector('.searchDiv p');
let b = setTimeout(function() {y.classList.add("nogoal")}, 2000);
let a = setTimeout(function() {x.classList.add("goal")}, 2500);

document.addEventListener("DOMContentLoaded", () => {
    b();
    a();} )
