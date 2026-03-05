
using RoyalGames.Exceptions;


namespace RoyalGames.Applications.Regras
{
    public class HorarioAlteracaoJogo
    {
        public static void ValidarHorario()
        {
            var agora = DateTime.Now.TimeOfDay;
            var abertura = new TimeSpan(10, 0, 0); //16h
            var fechamento = new TimeSpan(23, 0, 0);

            // retorna um true ou false
            var estaAberto = agora >= abertura && agora <= fechamento;

            // se retornar true
            if (estaAberto)
            {
                throw new DomainException("Produto só pode ser alterado fora do horário de funcionamento.");
            }
        }
    }
}
