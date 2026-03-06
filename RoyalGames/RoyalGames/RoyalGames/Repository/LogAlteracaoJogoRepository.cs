using RoyalGames.Contexts;
using RoyalGames.Interfaces;
using RoyalGames.Models;

namespace RoyalGames.Repository
{
    public class LogAlteracaoJogoRepository : ILogAlteracaoJogoRepository
    {
        private readonly RoyalGamesContext _context;

        public LogAlteracaoJogoRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<LogAlteracaoJogo> Listar()
        {
            List<LogAlteracaoJogo> log = _context.LogAlteracaoJogos.OrderByDescending(l => l.DataAlteracao).ToList();
            return log;
        }

        public List<LogAlteracaoJogo> ListarPorJogoId(int JogoID)
        {
            List<LogAlteracaoJogo> alteracaoJogo = _context.LogAlteracaoJogos.Where(log => log.JogoId == JogoID).OrderByDescending(log => log.DataAlteracao).ToList();
            return alteracaoJogo;
        }
    }
}
