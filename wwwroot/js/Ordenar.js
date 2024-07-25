function ordenar() {
    let orden = document.getElementById("ordenar");

    if (orden.value == "#") {
        ordenarPorNombre()
        orden.value = "A";
    } else {
        ordenarPorNumero()
        orden.value = "#";
    }
}

function ordenarPorNombre() {
    let main = document.getElementById("main");
    let pokemons = Array.from(main.getElementsByClassName("aCard"));

    pokemons.sort((a, b) => {
        let nombreA = a.querySelector(".nombrePokemon").textContent.toLowerCase();
        let nombreB = b.querySelector(".nombrePokemon").textContent.toLowerCase();

        if (nombreA < nombreB) return -1;
        if (nombreA > nombreB) return 1;
        return 0;
    });

    main.innerHTML = "";
    pokemons.forEach(item => main.appendChild(item));
}

function ordenarPorNumero() {
    let main = document.getElementById("main");
    let pokemons = Array.from(main.getElementsByClassName("aCard"));

    pokemons.sort((a, b) => {
        let numA = parseInt(a.querySelector(".nPokemon").textContent.replace("#", "").trim());
        let numB = parseInt(b.querySelector(".nPokemon").textContent.replace("#", "").trim());

        return numA - numB
    })

    main.innerHTML = "";
    pokemons.forEach(item => main.appendChild(item));
}