//CAMBIAR POR URL LOCAL
const API_URL = 'http://127.0.0.1:5045/api/Quejas';


async function cargarQuejas() {
    const folioInput = document.getElementById('busquedaFolio').value.toLowerCase();
    const estadoFiltro = document.getElementById('filtroEstado').value;

    let url = API_URL;
    if (estadoFiltro) url += `?idEstado=${estadoFiltro}`;

    try {
        const response = await fetch(url);
        if (!response.ok) throw new Error("Error al conectar con la API");
        
        const data = await response.json();
        const tbody = document.getElementById('tablaQuejas');
        tbody.innerHTML = '';

        // Filtrado local por folio
        const filtrados = data.filter(q => q.folio.toLowerCase().includes(folioInput));

        filtrados.forEach(q => {
            tbody.innerHTML += `
                <tr>
                    <td><span class="badge bg-dark">${q.folio}</span></td>
                    <td>${q.titulo}</td>
                    <td>${q.nombreCliente}</td>
                    <td>${renderEstadoBadge(q.idEstado)}</td>
                    <td>
                        <button class="btn btn-sm btn-outline-primary" onclick="verDetalle(${q.idQueja})">Ver Detalle</button>
                    </td>
                </tr>`;
        });
    } catch (error) {
        console.error("Error:", error);
    }
}


async function verDetalle(id) {
    idSeleccionado = id;
    try {
        const response = await fetch(`${API_URL}/${id}`);
        if (!response.ok) throw new Error("No se encontró la queja");
        const q = await response.json(); 

      
        const setVal = (id, val) => {
            const el = document.getElementById(id);
            if (el) el.innerText = val || '';
        };

        setVal('folio', q.folio);
        setVal('titulo', q.titulo);
        setVal('descripcion', q.descripcion);
        setVal('nombreCliente', q.nombreCliente);
        setVal('correo', q.correo);
        setVal('fechaRegistro', new Date(q.fechaRegistro).toLocaleString());

        const elEstado = document.getElementById('idEstado');
        if (elEstado) {
            elEstado.innerText = obtenerNombreEstado(q.idEstado);
            elEstado.className = `badge ${q.idEstado === 6 ? 'bg-danger' : 'bg-info text-dark'}`;
        }

        const btn = document.getElementById('btnGuardarEstatus');
        if (btn) {
            btn.disabled = (q.idEstado === 6);
            btn.innerText = (q.idEstado === 6) ? "Cerrada" : "Actualizar";
        }

      
        const modal = new bootstrap.Modal(document.getElementById('modalDetalle'));
        modal.show();

    } catch (error) {
        console.error("Error al cargar detalle:", error);
        alert("No se pudo cargar el detalle de la queja.");
    }
}


function renderEstadoBadge(id) {
    const nombres = { 1: 'Registrada', 2: 'Análisis', 4: 'Resuelta', 6: 'Cerrada' };
    const colores = { 1: 'secondary', 2: 'info text-dark', 4: 'success', 6: 'danger' };
    return `<span class="badge bg-${colores[id] || 'warning'}">${nombres[id] || 'Pendiente'}</span>`;
}

function obtenerNombreEstado(id) {
    const nombres = { 1: 'Registrada', 2: 'Análisis', 4: 'Resuelta', 6: 'Cerrada' };
    return nombres[id] || 'Desconocido';
}


let idSeleccionado = 0;
async function guardarCambioEstatus() {
    const nuevoId = document.getElementById('nuevoEstadoId').value;
    const comentario = document.getElementById('comentarioEstado').value;
    const usuarioActivo = "Sistema_Web"; 

    if (!comentario || comentario.trim().length < 5) {
        return alert("Por favor, ingresa un comentario válido (mínimo 5 caracteres).");
    }

    
    const url = `${API_URL}/${idSeleccionado}/estatus?nuevoEstadoId=${nuevoId}&usuario=${usuarioActivo}&comentario=${encodeURIComponent(comentario)}`;

    try {
        const response = await fetch(url, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' }
        });

        if (response.ok) {
            alert("Estado actualizado correctamente.");
    
            const modalActual = bootstrap.Modal.getInstance(document.getElementById('modalDetalle'));
            modalActual.hide();
            cargarQuejas(); 
        } else {
            const errorData = await response.json();
            alert("Regla de negocio: " + (errorData.message || "No se pudo realizar el cambio."));
        }
    } catch (error) {
        console.error("Error en la petición:", error);
        alert("Hubo un error de comunicación con el servidor.");
    }
}


document.addEventListener('DOMContentLoaded', cargarQuejas);