
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using MvcMovie.Data;

namespace MvcMovie.Services
{
    public class ModalidadeService
    {
        private readonly MvcMovieContext _context;

        public ModalidadeService(MvcMovieContext context)
        {
            _context = context;
        }

        public async Task<bool> ExisteCicloEntreModalidades(int modalidadeId, int[] turmaIds)
        {
            // Se não há turmas selecionadas, não há ciclo
            if (turmaIds == null || turmaIds.Length == 0)
                return false;

            // Busca todas as turmas selecionadas que já estão associadas a outras modalidades
            var turmasComOutrasModalidades = await _context.ModalidadeTurma
                .Where(mt => turmaIds.Contains(mt.TurmaId) && mt.ModalidadeId != modalidadeId)
                .AnyAsync();

            return turmasComOutrasModalidades;
        }
    }
}