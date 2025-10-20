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
            var inscricoes = _context.InscricaoTurma
                .Include(i => i.Pessoa)
                .Include(i => i.Turma);
            return View(await inscricoes.ToListAsync());
        }

        // GET: Inscricoes/Create
        public IActionResult Create()
        {
            ViewBag.Pessoas = _context.Pessoa.OrderBy(p => p.Nome).ToList();
            ViewBag.Turmas = _context.Turma.OrderBy(t => t.Nome).ToList();
            return View();
        }

        // POST: Inscricoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PessoaId,TurmaId")] InscricaoTurma inscricao)
        {
            if (ModelState.IsValid)
            {
                // Prevent duplicate enrollment
                bool exists = await _context.InscricaoTurma.AnyAsync(i => i.PessoaId == inscricao.PessoaId && i.TurmaId == inscricao.TurmaId);
                if (exists)
                {
                    ModelState.AddModelError("", "Este aluno já está inscrito nesta turma.");
                    ViewBag.Pessoas = _context.Pessoa.OrderBy(p => p.Nome).ToList();
                    ViewBag.Turmas = _context.Turma.OrderBy(t => t.Nome).ToList();
                    return View(inscricao);
                }

                _context.Add(inscricao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Pessoas = _context.Pessoa.OrderBy(p => p.Nome).ToList();
            ViewBag.Turmas = _context.Turma.OrderBy(t => t.Nome).ToList();
            return View(inscricao);
        }

        // POST: Inscricoes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var inscricao = await _context.InscricaoTurma.FindAsync(id);
            if (inscricao != null)
            {
                _context.InscricaoTurma.Remove(inscricao);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
