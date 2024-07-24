const Colores = {
    grass : '#74CB48',
    fire : '#F57D31',
    water : '#6493EB',
    bug : '#A7B723',
    electric : '#F9CF30',
    ghost : '#70559B',
    normal : '#AAA67F',
    psychic : '#FB5584',
    ice: '#9AD6DF',
    dragon: '#7037FF',
    fairy: '#E69EAC',
    steel: '#B7B9D0',
    dark: '#75574C',
    flying: '#A891EC',
    fighting: '#C12239',
    ground: '#DEC16B',
    rock: '#B69E31',
    poison: '#A43E9E',
    ground: '#DEC16B'
}

let tipos = document.getElementsByClassName("tipoPokemon");

//para cambiar el color de fondo
let root = document.documentElement;
root.style.setProperty("--color", Colores[tipos[0].textContent]);

//para cambiar el color de fondo de las etiquetas para los tipos
for (let i = 0; i < tipos.length; i++) {
    tipos[i].style.backgroundColor = Colores[tipos[i].textContent]
}
