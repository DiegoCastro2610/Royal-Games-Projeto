
using RoyalGames.Models;


namespace RoyalGames.Interfaces
{
    public interface ILog_AlteracaoJogoRepository
    {
      List<LogAlteracaoJogo> Listar();

      List<LogAlteracaoJogo> ListarPorJogoId(int jogoId);
    }
}
