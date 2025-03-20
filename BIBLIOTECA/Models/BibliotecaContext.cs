using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BIBLIOTECA.Models;

/*  
    Contexto de base de datos para la aplicación GESTOR DE BIBLIOTECA (BibliotecaContext)

    - Propósito:
        - Representa el contexto de la base de datos mediante Entity Framework Core.
        - Define las entidades y su configuración para mapear la base de datos.

    - Estructura:
        1. **Constructor**:
            - `BibliotecaContext()`: Constructor vacío.
            - `BibliotecaContext(DbContextOptions<BibliotecaContext> options)`: Constructor que permite inyección de dependencias con opciones de configuración.

        2. **Definición de tablas (DbSet)**:
            - `DbSet<Autore> Autores`: Representa la tabla "autores".
            - `DbSet<Libro> Libros`: Representa la tabla "libros".

        3. **Configuración de conexión a la base de datos** (`OnConfiguring`):
            - Si `optionsBuilder` no está configurado, se establece una conexión SQL Server con autenticación integrada.
            - Se advierte sobre la importancia de mover la cadena de conexión a una configuración segura.

        4. **Configuración del modelo de datos** (`OnModelCreating`):
            - **Tabla "autores"**:
                - Clave primaria: `autorId`.
                - Propiedades configuradas con nombres de columna y restricciones.
            - **Tabla "libros"**:
                - Clave primaria: `libroId`.
                - Relación con "autores" (FK `autorId` → `Autores`).
                - Propiedades configuradas con nombres de columna y restricciones.

        5. **Método parcial para extender la configuración** (`OnModelCreatingPartial`):
            - Permite agregar configuraciones adicionales sin modificar el código generado.

    - Notas:
        - La conexión a la base de datos usa `TrustServerCertificate=True`, lo que permite evitar problemas de certificados en entornos de desarrollo.
        - Se recomienda mover la cadena de conexión fuera del código fuente para mayor seguridad.
*/

public partial class BibliotecaContext : DbContext
{
    public BibliotecaContext()
    {
    }

    public BibliotecaContext(DbContextOptions<BibliotecaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
             optionsBuilder.UseSqlServer("server=localhost\\SQLEXPRESS; database=biblioteca; integrated security=true; TrustServerCertificate=True");


        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.AutorId).HasName("PK__autores__A07C6A44F76F86FB");

            entity.ToTable("autores");

            entity.Property(e => e.AutorId).HasColumnName("autorId");
            entity.Property(e => e.FechaFallecimientoAutor).HasColumnName("fechaFallecimientoAutor");
            entity.Property(e => e.FechaNacimientoAutor).HasColumnName("fechaNacimientoAutor");
            entity.Property(e => e.NacionalidadAutor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nacionalidadAutor");
            entity.Property(e => e.NombreAutor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombreAutor");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.LibroId).HasName("PK__libros__18C65CAB2E39F483");

            entity.ToTable("libros");

            entity.Property(e => e.LibroId).HasColumnName("libroId");
            entity.Property(e => e.AutorId).HasColumnName("autorId");
            entity.Property(e => e.AñoPublicacionLibro).HasColumnName("añoPublicacionLibro");
            entity.Property(e => e.EditorialLibro)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("editorialLibro");
            entity.Property(e => e.GeneroLibro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("generoLibro");
            entity.Property(e => e.NumeroPaginasLibro).HasColumnName("numeroPaginasLibro");
            entity.Property(e => e.TituloLibro)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("tituloLibro");

            entity.HasOne(d => d.Autor).WithMany(p => p.Libros)
                .HasForeignKey(d => d.AutorId)
                .HasConstraintName("FK_libros_autores");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
