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
    public class AutoresController : Controller
    {
        private readonly BibliotecaContext _context;

        public AutoresController(BibliotecaContext context)
        {
            _context = context;
        }

        // GET: Autores
        /// <summary>
        /// Método que obtiene la lista de todos los autores almacenados en la base de datos.
        /// </summary>
        /// <returns>Retorna la vista con la lista de autores.</returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Autores.ToListAsync());
        }

        // GET: Autores/Details/5
        /// <summary>
        /// Método que obtiene los detalles de un autor específico.
        /// </summary>
        /// <param name="id">El identificador único del autor.</param>
        /// <returns>Retorna la vista con los detalles del autor si se encuentra en la base de datos; de lo contrario, devuelve un error 404.</returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autore = await _context.Autores
                .FirstOrDefaultAsync(m => m.AutorId == id);
            if (autore == null)
            {
                return NotFound();
            }

            return View(autore);
        }

        // GET: Autores/Create
        /// <summary>
        /// Método que muestra el formulario para crear un nuevo autor.
        /// </summary>
        /// <returns>Retorna la vista del formulario de creación de autores.</returns>
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autores/Create
        /// <summary>
        /// Método que procesa la creación de un nuevo autor.
        /// </summary>
        /// <param name="autore">Objeto que contiene los datos del autor a registrar.</param>
        /// <returns>Redirige a la lista de autores si la validación es exitosa, de lo contrario, muestra la vista con los errores.</returns>

        [HttpPost]// Especifica que este método maneja solicitudes HTTP POST.
        [ValidateAntiForgeryToken]// Protección contra ataques CSRF.
        public async Task<IActionResult> Create([Bind("AutorId,NombreAutor,NacionalidadAutor,FechaNacimientoAutor,FechaFallecimientoAutor")] Autore autore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autore);
        }

        // GET: Autores/Edit/5
        /// <summary>
        /// Muestra el formulario para editar un autor específico.
        /// </summary>
        /// <param name="id">Identificador único del autor a editar.</param>
        /// <returns>Devuelve la vista con los datos del autor si existe, de lo contrario, devuelve un error 404.</returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autore = await _context.Autores.FirstOrDefaultAsync(m => m.AutorId == id);
            if (autore == null)
            {
                return NotFound();
            }
            return View(autore);
        }

        // POST: Autores/Edit/5
        /// <summary>
        /// Procesa la solicitud de edición de un autor en la base de datos.
        /// </summary>
        /// <param name="id">Identificador único del autor a editar.</param>
        /// <param name="autore">Objeto que contiene los datos actualizados del autor.</param>
        /// <returns>Si la edición es exitosa, redirige al índice de autores. Si hay errores, devuelve la vista con el autor.</returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AutorId,NombreAutor,NacionalidadAutor,FechaNacimientoAutor,FechaFallecimientoAutor")] Autore autore)
        {
            if (id != autore.AutorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutoreExists(autore.AutorId))
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
            return View(autore);
        }

        // GET: Autores/Delete/5
        /// <summary>
        /// Obtiene la información de un autor específico para su eliminación.
        /// </summary>
        /// <param name="id">Identificador único del autor a eliminar.</param>
        /// <returns>Devuelve la vista con los datos del autor si existe, de lo contrario, retorna un error 404.</returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autore = await _context.Autores
                .FirstOrDefaultAsync(m => m.AutorId == id);
            if (autore == null)
            {
                return NotFound();
            }

            return View(autore);
        }

        // POST: Autores/Delete/5
        /// <summary>
        /// Confirma la eliminación de un autor en la base de datos.
        /// </summary>
        /// <param name="id">Identificador único del autor a eliminar.</param>
        /// <returns>Redirige a la vista de índice tras eliminar el autor.</returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autore = await _context.Autores.FindAsync(id);
            if (autore != null)
            {
                _context.Autores.Remove(autore);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutoreExists(int id)
        {
            return _context.Autores.Any(e => e.AutorId == id);
        }
    }
}
