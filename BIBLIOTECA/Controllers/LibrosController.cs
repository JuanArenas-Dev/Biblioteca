using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BIBLIOTECA.Models;

namespace BIBLIOTECA.Controllers
{
    public class LibrosController : Controller
    {
        private readonly BibliotecaContext _context;

        public LibrosController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: Libros

        /// <summary>
        /// Método que obtiene la lista de libros desde la base de datos 
        /// e incluye la información del autor asociado.
        /// </summary>
        /// <returns>Retorna la vista con la lista de libros.</returns>
        public async Task<IActionResult> Index()
        {
            var bibliotecaContext = _context.Libros.Include(l => l.Autor);
            return View(await bibliotecaContext.ToListAsync());
        }


        // GET: Libros/Details/5

        /// <summary>
        /// Método que obtiene los detalles de un libro específico basado en su ID.
        /// Incluye la información del autor asociado.
        /// </summary>
        /// <param name="id">ID del libro a consultar.</param>
        /// <returns>Retorna la vista con los detalles del libro si existe, de lo contrario, retorna NotFound().</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(m => m.LibroId == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libros/Create
        /// <summary>
        /// Método para mostrar el formulario de creación de un nuevo libro.
        /// Carga la lista de autores disponibles en la base de datos para seleccionar uno en el formulario.
        /// </summary>
        /// <returns>Retorna la vista de creación con la lista de autores.</returns>
        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_context.Autores, "AutorId", "NombreAutor");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LibroId,TituloLibro,AutorId,GeneroLibro,EditorialLibro,AñoPublicacionLibro,NumeroPaginasLibro")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Autores, "AutorId", "NombreAutor", libro.AutorId);
            return View(libro);
        }



        // GET: Libros/Edit/5
        /// <summary>
        /// Método para mostrar el formulario de edición de un libro específico.
        /// Busca el libro por su ID y carga los datos en el formulario.
        /// </summary>
        /// <param name="id">ID del libro a editar.</param>
        /// <returns>Retorna la vista de edición con los datos del libro.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            ViewData["AutorId"] = new SelectList(_context.Autores, "AutorId", "NombreAutor", libro.AutorId);
            return View(libro);
        }

        // POST: Libros/Edit/5
        /// <summary>
        /// Método que procesa la edición de un libro.
        /// Valida los datos ingresados y actualiza el registro en la base de datos.
        /// </summary>
        /// <param name="id">ID del libro que se está editando.</param>
        /// <param name="libro">Objeto Libro con los datos editados.</param>
        /// <returns>Redirige al índice si la edición es exitosa, de lo contrario, retorna la vista con los errores.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LibroId,TituloLibro,AutorId,GeneroLibro,EditorialLibro,AñoPublicacionLibro,NumeroPaginasLibro")] Libro libro)
        {
            if (id != libro.LibroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.LibroId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Autores, "AutorId", "AutorId", libro.AutorId);
            return View(libro);
        }

        // GET: Libros/Delete/5
        /// <summary>
        /// Método para mostrar la vista de confirmación antes de eliminar un libro.
        /// Obtiene el libro a eliminar basado en su ID y lo envía a la vista.
        /// </summary>
        /// <param name="id">ID del libro a eliminar.</param>
        /// <returns>Retorna la vista de confirmación con los detalles del libro si se encuentra, de lo contrario, devuelve un error 404.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(m => m.LibroId == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libros/Delete/5
        /// <summary>
        /// Método que confirma la eliminación de un libro de la base de datos.
        /// Este método se ejecuta después de la confirmación del usuario en la vista.
        /// </summary>
        /// <param name="id">ID del libro a eliminar.</param>
        /// <returns>Redirige a la lista de libros después de eliminar el registro.</returns>
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Método auxiliar que verifica si un libro existe en la base de datos.
        /// </summary>
        /// <param name="id">ID del libro a verificar.</param>
        /// <returns>Retorna 'true' si el libro existe, 'false' si no.</returns>
        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.LibroId == id);
        }
    }
}
