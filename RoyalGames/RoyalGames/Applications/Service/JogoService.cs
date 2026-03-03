using RoyalGames.Applications.Conversores;
using RoyalGames.Dtos.JogosDto;
using RoyalGames.Dtos.UsuarioDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using RoyalGames.Models;

namespace RoyalGames.Applications.Service
{
    public class JogoService
    {
        private readonly IJogoRepository _repository;

        public JogoService(IJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerJogoDto> Listar()
        {
            List<Jogo> jogos = _repository.Listar();
            List<LerJogoDto> jogoDtos = jogos.Select(JogoParaDto.ConverterJogoParaDto).ToList();
            return jogoDtos;
        }

        public LerJogoDto? ObterPorId(int id)
        {
            Jogo jogo = _repository.ObterPorId(id);

            if (jogo == null)
            {
                throw new DomainException("Id de jogo não encontrado");
            }

            return JogoParaDto.ConverterJogoParaDto(jogo);
        }

        public static void ValidarCadastro(CriarJogoDto jogo)
        {
            if (string.IsNullOrWhiteSpace(jogo.Nome))
            {
                throw new DomainException("Jogo não possui nome definido");
            }
            if (jogo.preco <= 0)
            {
                throw new DomainException("Jogo não possui preço definido");
            }
            if (string.IsNullOrWhiteSpace(jogo.Descricao))
            {
                throw new DomainException("Jogo não possui descrição escrita");
            }
            if (jogo.Imagem == null)
            {
                throw new DomainException("Jogo não possui uma imagem");
            }
            if (jogo.GeneroIds == null)
            {
                throw new DomainException("Jogo não possui um genero");
            }
        }
        public byte[] ObterImagem(int id)
        {
            byte[] imagem = _repository.ObterImagem(id);
            if(imagem == null)
            {
                throw new DomainException("Não possui imagem");
            }


        }
    }

}
}
