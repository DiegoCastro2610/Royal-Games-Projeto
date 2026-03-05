using RoyalGames.Dtos.JogosDto;
using RoyalGames.Models;

namespace RoyalGames.Applications.Conversores
{
    public class JogoParaDto
    {
        public static LerJogoDto ConverterJogoParaDto(Jogo jogo)
        {
            return new LerJogoDto
            {
                JogoID = jogo.JogoId,
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Descricao = jogo.Descricao,
                StatusJogo = jogo.StatusJogo,

                Genero = jogo.Generos.Select(Genero => Genero.Nome).ToList(),
                UsuarioId = jogo.UsuarioId,
                ClassificacaoId = jogo.ClassificacaoIndicativaId,
                Plataforma = jogo.Plataformas.Select(Plataforma => Plataforma.Nome).ToList()
            };

        }
    }
}
