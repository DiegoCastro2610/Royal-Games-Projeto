using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using RoyalGames.Dtos.GeneroDto;
using RoyalGames.Models;

namespace RoyalGames.Applications.Services
{
    public class GeneroService
    {
        private readonly IGeneroRepository _repository;

        public GeneroService(IGeneroRepository repository)
        {
            _repository = repository;
        }

        public LerGeneroDto converterParaDto(Genero genero)
        {
            LerGeneroDto generoDto = new LerGeneroDto
            {
                Nome = genero.Nome
            };

            return generoDto;
        }

        public List<LerGeneroDto> Listar()
        {
            List<Genero> generos = _repository.Listar();

            List<LerGeneroDto> generoDto = generos.Select(genero => new LerGeneroDto
            {
                GeneroId = genero.GeneroId,
                Nome = genero.Nome,
            }).ToList();

            return generoDto;
        }

        public LerGeneroDto ObterPorId(int id)
        {
            Genero genero = _repository.ObterPorId(id);

            if (genero == null)
            {
                throw new DomainException("Genero nao encontrado");
            }

            return new LerGeneroDto
            {
                GeneroId = genero.GeneroId,
                Nome = genero.Nome,
            };
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Genero deve possuir um nome");
            }
        }

        public LerGeneroDto Adicionar(CriarGeneroDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            if (_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Genero com esse nome ja existe");
            }

            Genero genero = new Genero
            {
                Nome = criarDto.Nome,
            };

            _repository.Adicionar(genero);

            return converterParaDto(genero);
        }


        public void Remover(int id)
        {
            Genero generoBanco = _repository.ObterPorId(id);

            if (generoBanco == null)
            {
                throw new DomainException("Categoria nao encontrada.");
            }

            _repository.Remover(id);
        }
    }
}