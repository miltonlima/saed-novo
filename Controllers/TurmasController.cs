using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class TurmasController : Controller
    {
        private readonly MvcMovieContext _context;

        public TurmasController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Turmas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Turma.ToListAsync());
        }

        // GET: Turmas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var turma = await _context.Turma.FirstOrDefaultAsync(m => m.Id == id);
            if (turma == null)
                return NotFound();
            return View(turma);
        }

        // GET: Turmas/Create
        public IActionResult Create()
        {
            var anoAtual = DateTime.Now.Year;
            var turma = new Turma
            {
                DataInicio = new DateTime(anoAtual, 1, 1),
                DataFim = new DateTime(anoAtual, 12, 31)
            };
            return View(turma);
        }

        // POST: Turmas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,DataInicio,DataFim,Status")] Turma turma)
        {
            if (ModelState.IsValid)
            {
                turma.DataCriacao = DateTime.Now;
                _context.Add(turma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(turma);
        }

        // GET: Turmas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var turma = await _context.Turma.FindAsync(id);
            if (turma == null)
                return NotFound();
            return View(turma);
        }

        // POST: Turmas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataInicio,DataFim,Status,DataCriacao")] Turma turma)
        {
            if (id != turma.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurmaExists(turma.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(turma);
        }

        // GET: Turmas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var turma = await _context.Turma.FirstOrDefaultAsync(m => m.Id == id);
            if (turma == null)
                return NotFound();
            return View(turma);
        }

        // POST: Turmas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turma = await _context.Turma.FindAsync(id);
            if (turma != null)
            {
                _context.Turma.Remove(turma);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool TurmaExists(int id)
        {
            return _context.Turma.Any(e => e.Id == id);
        }
    }
}
