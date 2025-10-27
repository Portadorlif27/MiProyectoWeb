S// Datos de usuario
const usuarioValido = { usuario: "Hector0827", clave: "Hector0827" };

// Variables globales
let saldo = 1000; 
let prestamos = [];

// Elementos
const loginSection = document.getElementById("loginSection");
const appSection = document.getElementById("appSection");
const saldoActual = document.getElementById("saldoActual");
const formPrestamo = document.getElementById("formPrestamo");
const prestamosInfo = document.getElementById("prestamosInfo");
const pagosContainer = document.getElementById("pagosContainer");
const btnLogin = document.getElementById("btnLogin");

// LOGIN
btnLogin.addEventListener("click", () => {
  const usuario = document.getElementById("usuario").value.trim();
  const clave = document.getElementById("clave").value.trim();

  if (usuario === usuarioValido.usuario && clave === usuarioValido.clave) {
    loginSection.style.display = "none";
    appSection.style.display = "block";
    actualizarSaldo();
    mostrarInicio();
  } else {
    alert("Usuario o clave incorrecta. Inténtalo nuevamente.");
  }
});

// ACTUALIZAR SALDO
function actualizarSaldo() {
  saldoActual.textContent = `S/ ${saldo.toFixed(2)}`;
}

// INICIO
function mostrarInicio() {
  document.querySelectorAll(".seccion").forEach(s => (s.style.display = "none"));
  document.getElementById("inicio").style.display = "block";
}

// NAVEGACIÓN
document.querySelectorAll(".nav-link").forEach(link => {
  link.addEventListener("click", e => {
    const target = e.target.dataset.target;
    document.querySelectorAll(".seccion").forEach(s => (s.style.display = "none"));
    document.getElementById(target).style.display = "block";
  });
});

// SOLICITAR PRÉSTAMO
formPrestamo.addEventListener("submit", e => {
  e.preventDefault();
  const monto = parseFloat(document.getElementById("monto").value);
  if (isNaN(monto) || monto <= 0) {
    alert("Ingrese un monto válido.");
    return;
  }

  const fecha = new Date();
  const fechaPago = new Date(fecha);
  fechaPago.setDate(fecha.getDate() + 30); 

  prestamos.push({
    id: prestamos.length + 1,
    monto,
    fecha: fecha.toLocaleDateString(),
    fechaPago: fechaPago.toLocaleDateString(),
    pagado: false
  });

  saldo += monto;
  actualizarSaldo();
  mostrarPrestamos();
  alert(`Préstamo de S/ ${monto.toFixed(2)} agregado exitosamente.`);
  formPrestamo.reset();
});

// MOSTRAR PRÉSTAMOS
function mostrarPrestamos() {
  pagosContainer.innerHTML = "";
  if (prestamos.length === 0) {
    pagosContainer.innerHTML = `<p>No tienes préstamos registrados.</p>`;
    return;
  }

  prestamos.forEach(p => {
    const card = document.createElement("div");
    card.classList.add("card");
    card.innerHTML = `
      <h3>Préstamo #${p.id}</h3>
      <p><b>Monto:</b> S/ ${p.monto.toFixed(2)}</p>
      <p><b>Fecha de solicitud:</b> ${p.fecha}</p>
      <p><b>Fecha de pago:</b> ${p.fechaPago}</p>
      <p><b>Estado:</b> ${p.pagado ? "Pagado ✅" : "Pendiente ⏳"}</p>
      ${!p.pagado ? `<button class="btn" onclick="pagarPrestamo(${p.id})">Pagar</button>` : ""}
    `;
    pagosContainer.appendChild(card);
  });
}

// PAGAR PRÉSTAMO
window.pagarPrestamo = function (id) {
  const prestamo = prestamos.find(p => p.id === id);
  if (!prestamo || prestamo.pagado) return alert("Este préstamo ya está pagado.");
  if (saldo < prestamo.monto) return alert("Saldo insuficiente para pagar el préstamo.");

  saldo -= prestamo.monto;
  prestamo.pagado = true;
  actualizarSaldo();
  mostrarPrestamos();
  alert(`Has pagado el préstamo #${id} exitosamente.`);
};
