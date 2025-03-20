using BIBLIOTECA.Models;
using Microsoft.EntityFrameworkCore;

/*  
    Configuraci�n y ejecuci�n de la aplicaci�n GESTOR DE BIBLIOTECA (ASP.NET Core)

    - Prop�sito:
        - Configura los servicios y middleware necesarios para la ejecuci�n de la aplicaci�n web.

    - Estructura:
        1. **Configuraci�n inicial**:
            - Se crea el `builder` para configurar la aplicaci�n.
            - Se agregan servicios al contenedor, incluyendo:
                - `AddControllersWithViews()`: Habilita el uso de controladores y vistas.
                - `AddDbContext<BibliotecaContext>()`: Configura la conexi�n a la base de datos SQL Server usando la cadena de conexi�n "conexion".
        2. **Construcci�n y ejecuci�n de la aplicaci�n**:
            - Se construye la aplicaci�n con `builder.Build()`.
        3. **Middleware**:
            - Configuraci�n del pipeline de solicitud HTTP:
                - Manejador de excepciones (`UseExceptionHandler("/Home/Error")`) en entorno de producci�n.
                - `UseHsts()`: Fuerza el uso de HTTPS en producci�n.
                - `UseHttpsRedirection()`: Redirige autom�ticamente las solicitudes HTTP a HTTPS.
                - `UseStaticFiles()`: Habilita la carga de archivos est�ticos (CSS, JS, im�genes, etc.).
                - `UseRouting()`: Habilita la generaci�n de rutas para los controladores.
                - `UseAuthorization()`: Configura la autorizaci�n en la aplicaci�n.
        4. **Definici�n de rutas**:
            - Se establece una ruta por defecto:
                - Controlador: `Libros`
                - Acci�n: `Index`
                - Par�metro opcional: `id`
        5. **Ejecuci�n de la aplicaci�n**:
            - `app.Run()`: Inicia el servidor web y mantiene la aplicaci�n en ejecuci�n.
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
