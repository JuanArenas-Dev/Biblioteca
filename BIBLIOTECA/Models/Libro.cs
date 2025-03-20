using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BIBLIOTECA.Models;


/// <summary>
/// Representa la entidad Libro en la base de datos.
/// Contiene información detallada sobre un libro, como su título, autor, género, editorial,
/// año de publicación y número de páginas.
/// </summary>
public partial class Libro
{
    /// <summary>
    /// Identificador único del libro.
    /// </summary>
    public int LibroId { get; set; }

    /// <summary>
    /// Título del libro. Es un campo obligatorio con un máximo de 100 caracteres.
    /// </summary>
    [Display(Name="Titulo del libro ")]
    [Required(ErrorMessage = "El titulo es obligatorio.")]
    [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    public string TituloLibro { get; set; } = null!;

    /// <summary>
    /// Nombre del autor del linro. Es un campo obligatorio.
    /// </summary>
    [Display(Name = "Nombre del autor")]
    [Required(ErrorMessage = "El autor es obligatorio.")]
    public int? AutorId { get; set; }


    /// <summary>
    /// Género literario del libro. Es obligatorio, tiene un máximo de 100 caracteres 
    /// y solo permite letras y espacios.
    /// </summary>
    [Display(Name = "Genero")]
    [Required(ErrorMessage = "El genero es obligatorio.")]
    [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    [RegularExpression(@"^[A-Za-zÀ-ÿ\s]+$", ErrorMessage = "Solo se permiten letras y espacios.")]
    public string? GeneroLibro { get; set; }


    /// <summary>
    /// Nombre de la editorial del libro. Es obligatorio, tiene un máximo de 100 caracteres 
    /// y solo permite letras y espacios.
    /// </summary>
    [Display(Name = "Editorial ")]
    [Required(ErrorMessage = "La editorial es obligatoria.")]
    [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
    [RegularExpression(@"^[A-Za-zÀ-ÿ\s]+$", ErrorMessage = "Solo se permiten letras y espacios.")]
    public string? EditorialLibro { get; set; }

    /// <summary>
    /// Año de publicación del libro. Es obligatorio y debe ser un valor numérico.
    /// </summary>
    [Display(Name = "Año de publicacion")]
    [Required(ErrorMessage = "El año es obligatorio.")]
    public int? AñoPublicacionLibro { get; set; }

    /// <summary>
    /// Número de páginas del libro. Es obligatorio y debe ser un valor numérico.
    /// </summary>
    [Display(Name = "Número de paginas")]
    [Required(ErrorMessage = "El número de paginas es obligatorio.")]
    public int? NumeroPaginasLibro { get; set; }


    /// <summary>
    /// Propiedad de navegación que establece la relación con la entidad Autor.
    /// Permite acceder a los datos del autor asociado al libro.
    /// </summary>
    public virtual Autore? Autor { get; set; }
}
