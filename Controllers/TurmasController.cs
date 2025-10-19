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
        public async Task<IActionResult> Create([Bind("Id,Nome,DataInicio,DataFim,Status")] Turma turma, int? SelectedModalidadeId)
        {
            if (ModelState.IsValid)
            {
                turma.DataCriacao = DateTime.Now;
                _context.Add(turma);
                await _context.SaveChangesAsync();

                // Vincula a modalidade selecionada (uma por turma)
                if (SelectedModalidadeId.HasValue && SelectedModalidadeId.Value > 0)
                {
                    var modalidadeExists = await _context.Modalidade.AnyAsync(m => m.Id == SelectedModalidadeId.Value);
                    if (!modalidadeExists)
                    {
                        ModelState.AddModelError("SelectedModalidadeId", "Modalidade inválida.");
                        return View(turma);
                    }

                    _context.ModalidadeTurma.Add(new ModalidadeTurma
                    {
                        ModalidadeId = SelectedModalidadeId.Value,
                        TurmaId = turma.Id
                    });
                    await _context.SaveChangesAsync();
                }

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataInicio,DataFim,Status,DataCriacao")] Turma turma, int? SelectedModalidadeId)
        {
            if (id != turma.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turma);

                    // Atualiza o vínculo da modalidade (garante no máximo uma modalidade por turma)
                    var currentLinks = await _context.ModalidadeTurma
                        .Where(mt => mt.TurmaId == turma.Id)
                        .ToListAsync();

                    if (SelectedModalidadeId.HasValue && SelectedModalidadeId.Value > 0)
                    {
                        var modalidadeExists = await _context.Modalidade.AnyAsync(m => m.Id == SelectedModalidadeId.Value);
                        if (!modalidadeExists)
                        {
                            ModelState.AddModelError("SelectedModalidadeId", "Modalidade inválida.");
                            return View(turma);
                        }

                        // Se já existir o mesmo vínculo, apenas remove os demais; senão recria
                        if (!currentLinks.Any(l => l.ModalidadeId == SelectedModalidadeId.Value))
                        {
                            _context.ModalidadeTurma.RemoveRange(currentLinks);
                            _context.ModalidadeTurma.Add(new ModalidadeTurma
                            {
                                ModalidadeId = SelectedModalidadeId.Value,
                                TurmaId = turma.Id
                            });
                        }
                        else
                        {
                            _context.ModalidadeTurma.RemoveRange(currentLinks.Where(l => l.ModalidadeId != SelectedModalidadeId.Value));
                        }
                    }
                    else
                    {
                        // Sem modalidade selecionada: remove vínculos existentes
                        if (currentLinks.Any())
                            _context.ModalidadeTurma.RemoveRange(currentLinks);
                    }

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
                // Remove vínculos com modalidades antes de excluir a turma
                var links = _context.ModalidadeTurma.Where(mt => mt.TurmaId == id);
                _context.ModalidadeTurma.RemoveRange(links);

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
