using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class InscricoesController : Controller
    {
        private readonly MvcMovieContext _context;

        public InscricoesController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Inscricoes
        public async Task<IActionResult> Index()
        {
            var inscricoes = _context.Inscricao
                .Include(i => i.Pessoa)
                .Include(i => i.Turma)
                .OrderByDescending(i => i.DataInscricao);
            return View(await inscricoes.ToListAsync());
        }

        // GET: Inscricoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            var inscricao = await _context.Inscricao
                .Include(i => i.Pessoa)
                .Include(i => i.Turma)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (inscricao == null)
                return NotFound();
            return View(inscricao);
        }

        // GET: Inscricoes/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Pessoas = await _context.Pessoa.OrderBy(p => p.Nome).ToListAsync();
            ViewBag.Turmas = await _context.Turma.OrderBy(t => t.Nome).ToListAsync();
            return View();
        }

        // POST: Inscricoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PessoaId,TurmaId,Status")] Inscricao inscricao)
        {
            // Validação: verificar se já existe inscrição
            bool exists = await _context.Inscricao
                .AnyAsync(i => i.PessoaId == inscricao.PessoaId && i.TurmaId == inscricao.TurmaId);
            
            if (exists)
            {
                ModelState.AddModelError("", "Este aluno já está inscrito nesta turma.");
                ViewBag.Pessoas = await _context.Pessoa.OrderBy(p => p.Nome).ToListAsync();
                ViewBag.Turmas = await _context.Turma.OrderBy(t => t.Nome).ToListAsync();
                return View(inscricao);
            }

            if (ModelState.IsValid)
            {
                inscricao.DataInscricao = DateTime.Now;
                _context.Add(inscricao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Pessoas = await _context.Pessoa.OrderBy(p => p.Nome).ToListAsync();
            ViewBag.Turmas = await _context.Turma.OrderBy(t => t.Nome).ToListAsync();
            return View(inscricao);
        }

        // GET: Inscricoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var inscricao = await _context.Inscricao.FindAsync(id);
            if (inscricao == null)
                return NotFound();
            ViewBag.Pessoas = await _context.Pessoa.OrderBy(p => p.Nome).ToListAsync();
            ViewBag.Turmas = await _context.Turma.OrderBy(t => t.Nome).ToListAsync();
            return View(inscricao);
        }

        // POST: Inscricoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PessoaId,TurmaId,Status,DataInscricao")] Inscricao inscricao)
        {
            if (id != inscricao.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscricao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscricaoExists(inscricao.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Pessoas = await _context.Pessoa.OrderBy(p => p.Nome).ToListAsync();
            ViewBag.Turmas = await _context.Turma.OrderBy(t => t.Nome).ToListAsync();
            return View(inscricao);
        }

        // GET: Inscricoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();
            var inscricao = await _context.Inscricao
                .Include(i => i.Pessoa)
                .Include(i => i.Turma)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (inscricao == null)
                return NotFound();
            return View(inscricao);
        }

        // POST: Inscricoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inscricao = await _context.Inscricao.FindAsync(id);
            if (inscricao != null)
            {
                _context.Inscricao.Remove(inscricao);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool InscricaoExists(int id)
        {
            return _context.Inscricao.Any(e => e.Id == id);
        }
    }
}
