namespace RoyalGames.Interfaces
{
    public interface ILog_AlteracaoJogo
    {
       List<ILog_AlteracaoJogo> Listar();

        List<ILog_AlteracaoJogo> ListarPorJogoId(int jogoId);
    }
}
