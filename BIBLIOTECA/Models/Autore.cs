using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BIBLIOTECA.Models;


/// <summary>
/// Representa la entidad Autor en la base de datos.
/// Contiene información detallada de un autor, incluyendo su nombre, nacionalidad,
/// fecha de nacimiento y fecha de fallecimiento. Además, mantiene una relación con los libros que ha escrito.
/// </summary>
public partial class Autore
{
    /// <summary>
    /// Identificador único del autor en la base de datos.
    /// </summary>
    [Key]
    public int AutorId { get; set; }

    /// <summary>
    /// Nombre completo del autor.
    /// Es un campo obligatorio con un máximo de 100 caracteres.
    /// </summary>
    [Display(Name="Nombre del autor")]
    [Required(ErrorMessage = "El nombre del autor es obligatorio.")]
    [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    public string NombreAutor { get; set; } = null!;

    /// <summary>
    /// Nacionalidad del autor.
    /// Es un campo obligatorio con un máximo de 50 caracteres.
    /// </summary>
    [Display(Name = "Nacionalidad del autor")]
    [Required(ErrorMessage = "La nacionalidad del autor es obligatoria.")]
    [StringLength(100, ErrorMessage = "Máximo 50 caracteres.")]
    [RegularExpression(@"^[A-Za-zÀ-ÿ\s]+$", ErrorMessage = "Solo se permiten letras y espacios.")]
    public string? NacionalidadAutor { get; set; }

    /// <summary>
    /// Fecha nacimiento del autor.
    /// </summary>
    [Display(Name = "Fecha de nacimiento del autor")]
    [Required(ErrorMessage = "La fecha de nacimiento del autor es obligatoria.")]
    public DateOnly? FechaNacimientoAutor { get; set; }

    /// <summary>
    /// Fecha fallecimiento del autor.
    /// </summary>
    [Display(Name = "Fecha de fallecimiento del autor")]
    public DateOnly? FechaFallecimientoAutor { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
