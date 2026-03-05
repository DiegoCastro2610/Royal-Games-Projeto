using RoyalGames.Contexts;
using RoyalGames.Interfaces;
using RoyalGames.Models;


namespace RoyalGames.Repository
{


    public class LogAlteracaoProdutoRepository : ILog_AlteracaoJogoRepository
    {
        private readonly RoyalGamesContext _context;

        public LogAlteracaoProdutoRepository(RoyalGamesContext context)
        {
            _context = context;
        }



        public List<LogAlteracaoJogo> Listar()
        {
            List<LogAlteracaoJogo> log = _context.LogAlteracaoJogos
                .OrderByDescending(l => l.DataAlteracao)
                .ToList();

            return log;
        }

        public List<LogAlteracaoJogo> ListarPorJogoId(int jogoId)
        {
            List<LogAlteracaoJogo> AlteracoesJogo = _context.LogAlteracaoJogos
                .Where(log => log.JogoId == jogoId)
                .OrderByDescending(log => log.DataAlteracao)
                .ToList();
            return AlteracoesJogo;


        }
    }
}
    

