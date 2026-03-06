using RoyalGames.Exceptions;

namespace RoyalGames.Applications.Regras
{
    public class HorarioAlteracaoJogo
    {
        public static void ValidarHorario()
        {
            var agora = DateTime.Now.TimeOfDay;
            var abertura = new TimeSpan(16, 0, 0);
            var fechamento = new TimeSpan(23, 0, 0);

            var estaAberto = agora >= abertura && agora <= fechamento;

            if (estaAberto)
            {
                throw new DomainException("Produto so pode ser alterado fora do horario de funcionamento");
            }
        }
    }
}
