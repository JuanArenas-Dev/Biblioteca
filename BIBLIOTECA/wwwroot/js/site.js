
$(document).ready(function () {
    $('#dataTable').DataTable({
        "paging": true,       // Agrega paginación
        "searching": true,    // Activa el filtro de búsqueda
        "ordering": true,     // Permite ordenar columnas
        "info": true,         // Muestra información (ej: "Mostrando 1-10 de 50 registros")
        "lengthMenu": [5, 10, 25, 50], // Opciones para mostrar más o menos registros
        "language": {
            "search": "Buscar:",
            "lengthMenu": "Mostrar _MENU_ registros",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ registros",
            "paginate": {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        },
        
        "responsive": true,
        "autoWidth": false
    });
});

$(document).ready(function () {
    $.validator.addMethod("fechaFallecimientoValida", function (value, element) {
        let fechaNacimientoStr = $("#FechaNacimientoAutor").val();

        if (fechaNacimientoStr && value) {
            // Convertir las fechas de dd/mm/yyyy a formato Date de JavaScript
            let partesNacimiento = fechaNacimientoStr.split("/");
            let partesFallecimiento = value.split("/");

            let fechaNacimiento = new Date(partesNacimiento[2], partesNacimiento[1] - 1, partesNacimiento[0]);
            let fechaFallecimiento = new Date(partesFallecimiento[2], partesFallecimiento[1] - 1, partesFallecimiento[0]);

            return fechaNacimiento.getTime() !== fechaFallecimiento.getTime(); // Compara ambas fechas
        }
        return true; // Si no se ingresa una fecha de fallecimiento, es válido
    }, "La fecha de fallecimiento no puede ser igual a la de nacimiento.");

    $("form").validate({
        errorElement: "div", // Usa un <div> para los mensajes
        errorClass: "invalid-feedback", // Clase de Bootstrap para alertas
        highlight: function (element) {
            $(element).addClass("is-invalid"); // Resalta el input en rojo
        },
        unhighlight: function (element) {
            $(element).removeClass("is-invalid"); // Remueve el resaltado si está bien
        },
        rules: {
            // Reglas para validaciones de crear,editar: Libro
            TituloLibro: {
                required: true,
                maxlength: 100
            },
            AñoPublicacionLibro: {
                required: true,
                number: true,
                min:100,
                max: 2025
            },
            GeneroLibro: {
                required: true,
                maxlength: 50,
                pattern: "^[A-Za-zÀ-ÿ\\s]+$" //Solo permite letras
            },
            EditorialLibro:{
                required: true,
                maxlength: 100,
                pattern: "^[A-Za-zÀ-ÿ\\s]+$" //Solo permite letras


            },
            NumeroPaginasLibro: {
                required: true,
                min: 1,
                max: 5000,
                number: true
            }, 

            // Reglas para validaciones de crear,editar: Autor
            NombreAutor: {
                required: true,
                maxlength: 100,
                
            },
            NacionalidadAutor: {
                required: true,
                maxlength: 50,
                pattern: "^[A-Za-zÀ-ÿ\\s]+$" //Solo permite letras

            },
            FechaNacimientoAutor: {
                required: true

            },
            FechaFallecimientoAutor: {
                fechaFallecimientoValida: true
                

            }
        },

        messages: {

            // Mensajes para validaciones de crear,editar: Libro
            TituloLibro: {
                required: "El título del libro, es obligatorio.",
                maxlength: "Solo se permiten máximo 100 caracteres."
            },
            AñoPublicacionLibro: {
                required: "El año es obligatorio.",
                number: "Debe ser un número.",
                min: "No puede ser menor a 100.",
                max: "No puede ser mayor a 2025."
            },
            GeneroLibro:{
                required: "El genero del libro, es obligatorio.",
                maxlength: "Solo se permiten maximo 50 caracteres.",
                pattern : "No se permiten números ni caracteres especiales"
            },
            EditorialLibro: {
                required: "La editorial del libro, es obligatoria.",
                maxlength: "Solo se permiten maximo 100 caracteres.",
                pattern: "No se permiten números ni caracteres especiales"
                
            },
            NumeroPaginasLibro: {
                required: "El número de paginas es obligatorio",
                min: "No puede ser menor a 1",
                max: "No puede ser mayor a 5000",
                number: "Debe ser un número"

            }, 

            //Mensajes para validaciones de crear,editar: Autor
            NombreAutor: {
                required:"El nombre del autor es obligatorio",
                maxlength:"Solo se permiten 100 caracteres",

            },
            NacionalidadAutor: {
                required:"La nacionalidad del autor es obligatoria",
                maxlength: "Solo se permiten 50 caracteres",
                pattern: "Solo se permiten letras"

            },
            FechaNacimientoAutor: {
                required: "La fecha de nacimiento del autor es obligatoria"

            },
            FechaFallecimientoAutor: {
                fechaFallecimientoValida: "La fecha de fallecimiento no puede ser igual a la de nacimiento."

                

            }
        }
    });
});



