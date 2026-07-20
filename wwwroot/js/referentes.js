document.addEventListener("DOMContentLoaded", () => {
    inicializarBuscadorListado();
    inicializarRegistroReferente();
});

function inicializarBuscadorListado() {
    const inputBusqueda = document.getElementById("buscarReferente");

    if (!inputBusqueda) {
        return;
    }

    inputBusqueda.addEventListener("input", () => {
        const termino = normalizarTexto(inputBusqueda.value);
        const filas = document.querySelectorAll(
            ".content-card table tbody tr"
        );

        filas.forEach((fila) => {
            const contenido = normalizarTexto(fila.textContent);
            fila.hidden = !contenido.includes(termino);
        });
    });
}

function inicializarRegistroReferente() {
    const dniInput = document.getElementById("dniNuevoReferente");
    const buscarButton = document.getElementById("btnBuscarPersona");
    const personaEncontrada = document.getElementById("personaEncontrada");
    const datosReferente = document.getElementById("datosReferente");
    const registrarButton = document.getElementById("btnRegistrarReferente");
    const estadoBusqueda = document.getElementById("estadoBusqueda");
    const checkboxMovilizador = document.getElementById("esMovilizador");
    const seccionMovilizador = document.getElementById(
        "datosMovilizadorReferente"
    );
    const tipoVehiculo = document.getElementById(
        "tipoVehiculoReferente"
    );
    const patente = document.getElementById("patenteReferente");

    if (
        !dniInput ||
        !buscarButton ||
        !personaEncontrada ||
        !datosReferente ||
        !registrarButton ||
        !estadoBusqueda
    ) {
        return;
    }
    if (
    checkboxMovilizador &&
    seccionMovilizador &&
    tipoVehiculo &&
    patente
) {
    checkboxMovilizador.addEventListener("change", () => {
        const tambienEsMovilizador = checkboxMovilizador.checked;

        seccionMovilizador.classList.toggle(
            "d-none",
            !tambienEsMovilizador
        );

        tipoVehiculo.required = tambienEsMovilizador;
        patente.required = tambienEsMovilizador;

        registrarButton.innerHTML = tambienEsMovilizador
            ? '<i class="bi bi-check2-circle me-2"></i>Registrar referente y movilizador'
            : '<i class="bi bi-check2-circle me-2"></i>Registrar referente';

        if (!tambienEsMovilizador) {
            tipoVehiculo.value = "";
            patente.value = "";
        }
    });

    patente.addEventListener("input", () => {
        patente.value = patente.value
            .toUpperCase()
            .replace(/[^A-Z0-9]/g, "")
            .slice(0, 10);
    });
}

    dniInput.addEventListener("input", () => {
        dniInput.value = dniInput.value.replace(/\D/g, "").slice(0, 8);
        limpiarResultadoBusqueda();
    });

    dniInput.addEventListener("keydown", (event) => {
        if (event.key === "Enter") {
            event.preventDefault();
            buscarButton.click();
        }
    });

    buscarButton.addEventListener("click", () => {
        const dni = dniInput.value.trim();

        if (!esDniValido(dni)) {
            mostrarEstadoBusqueda(
                "error",
                "Ingresá un DNI válido de 7 u 8 números."
            );

            ocultarDatosPersona();
            return;
        }

        buscarButton.disabled = true;
        buscarButton.innerHTML =
            '<span class="spinner-border spinner-border-sm me-2" aria-hidden="true"></span>Buscando...';

        window.setTimeout(() => {
            mostrarEstadoBusqueda(
                "success",
                "Persona encontrada correctamente."
            );

            personaEncontrada.classList.remove("d-none");
            datosReferente.classList.remove("d-none");
            registrarButton.disabled = false;

            buscarButton.disabled = false;
            buscarButton.innerHTML =
                '<i class="bi bi-search me-2"></i>Buscar persona';
        }, 500);
    });

    function limpiarResultadoBusqueda() {
        estadoBusqueda.className = "search-feedback d-none";
        estadoBusqueda.innerHTML = "";
        ocultarDatosPersona();
    }

   function ocultarDatosPersona() {
    personaEncontrada.classList.add("d-none");
    datosReferente.classList.add("d-none");
    registrarButton.disabled = true;

    if (
        checkboxMovilizador &&
        seccionMovilizador &&
        tipoVehiculo &&
        patente
    ) {
        checkboxMovilizador.checked = false;
        seccionMovilizador.classList.add("d-none");
        tipoVehiculo.required = false;
        patente.required = false;
        tipoVehiculo.value = "";
        patente.value = "";

        registrarButton.innerHTML =
            '<i class="bi bi-check2-circle me-2"></i>Registrar referente';
    }
}

    function mostrarEstadoBusqueda(tipo, mensaje) {
        const esError = tipo === "error";
        const claseEstado = esError
            ? "search-feedback-error"
            : "search-feedback-success";
        const icono = esError
            ? "bi-exclamation-circle"
            : "bi-check-circle";

        estadoBusqueda.className = `search-feedback ${claseEstado}`;
        estadoBusqueda.innerHTML = `
            <i class="bi ${icono}"></i>
            <span>${mensaje}</span>
        `;
    }
}

function esDniValido(dni) {
    return /^\d{7,8}$/.test(dni);
}

function normalizarTexto(texto) {
    return texto
        .toLowerCase()
        .normalize("NFD")
        .replace(/[\u0300-\u036f]/g, "")
        .trim();
}