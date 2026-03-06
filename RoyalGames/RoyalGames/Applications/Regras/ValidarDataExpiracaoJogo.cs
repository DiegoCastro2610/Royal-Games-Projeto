using RoyalGames.Exceptions;

namespace RoyalGames.Applications.Regras
{
    public class ValidarDataExpiracaoJogo
    {
        public static void ValidarDataExpiracao(DateTime dataExpiracao)
        {
            if (dataExpiracao <= DateTime.Now)
            {
                throw new DomainException("Data de expiracao deve ser futura");
            }
        }
    }
}
