using BIBLIOTECA.Models;
using Microsoft.EntityFrameworkCore;

/*  
    Configuración y ejecución de la aplicación GESTOR DE BIBLIOTECA (ASP.NET Core)

    - Propósito:
        - Configura los servicios y middleware necesarios para la ejecución de la aplicación web.

    - Estructura:
        1. **Configuración inicial**:
            - Se crea el `builder` para configurar la aplicación.
            - Se agregan servicios al contenedor, incluyendo:
                - `AddControllersWithViews()`: Habilita el uso de controladores y vistas.
                - `AddDbContext<BibliotecaContext>()`: Configura la conexión a la base de datos SQL Server usando la cadena de conexión "conexion".
        2. **Construcción y ejecución de la aplicación**:
            - Se construye la aplicación con `builder.Build()`.
        3. **Middleware**:
            - Configuración del pipeline de solicitud HTTP:
                - Manejador de excepciones (`UseExceptionHandler("/Home/Error")`) en entorno de producción.
                - `UseHsts()`: Fuerza el uso de HTTPS en producción.
                - `UseHttpsRedirection()`: Redirige automáticamente las solicitudes HTTP a HTTPS.
                - `UseStaticFiles()`: Habilita la carga de archivos estáticos (CSS, JS, imágenes, etc.).
                - `UseRouting()`: Habilita la generación de rutas para los controladores.
                - `UseAuthorization()`: Configura la autorización en la aplicación.
        4. **Definición de rutas**:
            - Se establece una ruta por defecto:
                - Controlador: `Libros`
                - Acción: `Index`
                - Parámetro opcional: `id`
        5. **Ejecución de la aplicación**:
            - `app.Run()`: Inicia el servidor web y mantiene la aplicación en ejecución.
*/


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conexion")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Libros}/{action=Index}/{id?}");   
app.Run();
