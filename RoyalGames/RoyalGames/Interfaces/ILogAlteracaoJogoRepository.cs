using RoyalGames.Models;

namespace RoyalGames.Interfaces
{
    public interface ILogAlteracaoJogoRepository
    {
       List<LogAlteracaoJogo> Listar();

        List<LogAlteracaoJogo> ListarPorJogoId(int jogoId);
    }
}
