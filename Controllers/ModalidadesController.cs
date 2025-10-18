using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.Services;

namespace MvcMovie.Controllers
{
    public class ModalidadesController : Controller
    {
        private readonly MvcMovieContext _context;
        private readonly ModalidadeService _modalidadeService;

        public ModalidadesController(MvcMovieContext context)
        {
            _context = context;
            _modalidadeService = new ModalidadeService(context);
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _context.Modalidade
                .Include(m => m.ModalidadesTurmas)
                    .ThenInclude(mt => mt.Turma)
                .ToListAsync();
            return View(lista);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var modalidade = await _context.Modalidade
                .Include(m => m.ModalidadesTurmas)
                    .ThenInclude(mt => mt.Turma)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modalidade == null) return NotFound();
            return View(modalidade);
        }

        public IActionResult Create()
        {
            // Seleciona apenas turmas que não estão em nenhuma modalidade
            var turmasNaoAtribuidas = _context.Turma
                .Where(t => !_context.ModalidadeTurma.Any(mt => mt.TurmaId == t.Id))
                .ToList();
            ViewBag.Turmas = turmasNaoAtribuidas;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Modalidade modalidade, int[] selectedTurmas)
        {
            if (ModelState.IsValid)
            {
                if (selectedTurmas != null)
                {
                    // Verifica se alguma das turmas já tem modalidade
                    if (await _modalidadeService.ExisteCicloEntreModalidades(modalidade.Id, selectedTurmas))
                    {
                        ModelState.AddModelError("", "Uma ou mais turmas selecionadas já estão vinculadas a outras modalidades. Não é possível vincular a mesma turma a mais de uma modalidade.");
                    }
                    else
                    {
                        foreach (var turmaId in selectedTurmas)
                        {
                            modalidade.ModalidadesTurmas.Add(new ModalidadeTurma
                            {
                                ModalidadeId = modalidade.Id,
                                TurmaId = turmaId
                            });
                        }
                        
                        _context.Add(modalidade);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    _context.Add(modalidade);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Turmas = _context.Turma
                .Where(t => !_context.ModalidadeTurma.Any(mt => mt.TurmaId == t.Id))
                .ToList();
            return View(modalidade);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var modalidade = await _context.Modalidade
                .Include(m => m.ModalidadesTurmas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modalidade == null) return NotFound();

            // Turmas já atribuídas a esta modalidade
            var turmasAtribuidas = modalidade.ModalidadesTurmas.Select(mt => mt.TurmaId).ToList();
            // Turmas não atribuídas a nenhuma modalidade ou já atribuídas a esta modalidade
            var turmasDisponiveis = _context.Turma
                .Where(t => !_context.ModalidadeTurma.Any(mt => mt.TurmaId == t.Id && mt.ModalidadeId != modalidade.Id))
                .ToList();
            ViewBag.Turmas = turmasDisponiveis;
            ViewBag.SelectedTurmas = turmasAtribuidas;
            return View(modalidade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Modalidade modalidade, int[] selectedTurmas)
        {
            if (id != modalidade.Id) return NotFound();
            if (ModelState.IsValid)
            {
                try
                {
                    var modalidadeToUpdate = await _context.Modalidade
                        .Include(m => m.ModalidadesTurmas)
                        .FirstOrDefaultAsync(m => m.Id == id);

                    if (modalidadeToUpdate == null) return NotFound();

                    // Verifica se alguma das novas turmas selecionadas já tem modalidade
                    // excluindo as turmas que já pertencem a esta modalidade
                    var turmasAtuais = modalidadeToUpdate.ModalidadesTurmas.Select(mt => mt.TurmaId).ToArray();
                    var novasTurmas = selectedTurmas?.Except(turmasAtuais).ToArray() ?? Array.Empty<int>();

                    if (novasTurmas.Length > 0 && await _modalidadeService.ExisteCicloEntreModalidades(modalidade.Id, novasTurmas))
                    {
                        ModelState.AddModelError("", "Uma ou mais turmas selecionadas já estão vinculadas a outras modalidades. Não é possível vincular a mesma turma a mais de uma modalidade.");
                        ViewBag.Turmas = new MultiSelectList(_context.Set<Turma>(), "Id", "Nome", selectedTurmas);
                        return View(modalidade);
                    }

                    modalidadeToUpdate.Nome = modalidade.Nome;
                    modalidadeToUpdate.ModalidadesTurmas.Clear();

                    if (selectedTurmas != null)
                    {
                        foreach (var turmaId in selectedTurmas)
                        {
                            modalidadeToUpdate.ModalidadesTurmas.Add(new ModalidadeTurma
                            {
                                ModalidadeId = modalidade.Id,
                                TurmaId = turmaId
                            });
                        }
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Set<Modalidade>().AnyAsync(e => e.Id == modalidade.Id)) return NotFound();
                    throw;
                }
            }
            // Turmas não atribuídas a nenhuma modalidade ou já atribuídas a esta modalidade
            var turmasDisponiveis = _context.Turma
                .Where(t => !_context.ModalidadeTurma.Any(mt => mt.TurmaId == t.Id && mt.ModalidadeId != modalidade.Id))
                .ToList();
            ViewBag.Turmas = turmasDisponiveis;
            ViewBag.SelectedTurmas = selectedTurmas;
            return View(modalidade);
        }

        // Remova o Delete se não quiser implementar agora
    }
}
