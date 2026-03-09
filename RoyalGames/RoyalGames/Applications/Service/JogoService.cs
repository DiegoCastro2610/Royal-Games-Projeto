using RoyalGames.Applications.Conversores;
using RoyalGames.Applications.Regras;
using RoyalGames.Dtos.JogosDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using RoyalGames.Models;
using RoyalGames.Controllers;
using VHBurguer.Applications.Conversoes;

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
            if (imagem == null)
            {
                throw new DomainException("Não possui imagem");
            }

            return imagem;
        }

        public LerJogoDto Adicionar(CriarJogoDto jogoDto, int usuarioId)
        {
            ValidarCadastro(jogoDto);

            if (_repository.NomeExiste(jogoDto.Nome))
            {
                throw new DomainException("Jogo já existe");
            }

            Jogo jogo = new Jogo
            {
                UsuarioId = usuarioId,
                Nome = jogoDto.Nome,
                Preco = jogoDto.preco,
                Descricao = jogoDto.Descricao,
                Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem)
            };

            _repository.Adicionar(jogo, jogoDto.GeneroIds);
            return JogoParaDto.ConverterJogoParaDto(jogo);
        }

        public LerJogoDto Atualizar(int id, AtualizarJogoDto jogoDto)
        {
            HorarioAlteracaoJogo.ValidarHorario();

            Jogo jogoBanco = _repository.ObterPorId(id);

            if (jogoBanco == null)
            {
                throw new DomainException("Jogo não encontrado.");
            }

            // produtoIdAtual: -> dois pontos serve para passar o valor do parametro
            if (_repository.NomeExiste(jogoDto.Nome, jogoIdAtual: id))
            {
                throw new DomainException("Já existe outro jogo com esse nome.");
            }

            if (jogoDto.GeneroIds == null || jogoDto.GeneroIds.Count == 0)
            {
                throw new DomainException("Jogo deve ter ao menos uma categoria.");
            }

            if (jogoDto.preco < 0)
            {
                throw new DomainException("Preço deve ser maior que zero.");
            }

            jogoBanco.Nome = jogoBanco.Nome;
            jogoBanco.Preco = jogoBanco.Preco;
            jogoBanco.Descricao = jogoBanco.Descricao;

            if (jogoDto.Imagem != null && jogoDto.Imagem.Length > 0)
            {
                jogoBanco.Imagem = ImagemParaBytes.ConverterImagem(jogoDto.Imagem);
            }

            if (jogoDto.StatusJogo.HasValue)
            {
                jogoBanco.StatusJogo = jogoDto.StatusJogo.Value;
            }

            _repository.Atualizar(jogoBanco, jogoDto.GeneroIds);

            return JogoParaDto.ConverterJogoParaDto(jogoBanco);
        }

        public void Remover(int id)
        {
            HorarioAlteracaoJogo.ValidarHorario();

            Jogo jogo = _repository.ObterPorId(id);

            if (jogo == null)
            {
                throw new DomainException("Jogo não encontrado.");
            }

            _repository.Remover(id);
        }
    }
}
