using RoyalGames.Dtos.PlataformaDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using RoyalGames.Models;

namespace RoyalGames.Applications.Service
{
    public class PlataformaService
    {
        private readonly IPlataformaRepository _repository;

        public PlataformaService(IPlataformaRepository repository)
        {
            _repository = repository;
        }
        public List<LerPlataformaDto> Listar()
        {
            List<Plataforma> plataformas = _repository.Listar();
            List<LerPlataformaDto> plataformaDtos = plataformas.Select(plataforma => new LerPlataformaDto
            {
                PlataformaId = plataforma.PlataformaId,
                Nome = plataforma.Nome,
            }).ToList();

            return plataformaDtos;
        }
        public LerPlataformaDto ObterPorId(int id)
        {
            Plataforma plataforma = _repository.ObterPorId(id);
            if (plataforma == null)
            {
                throw new DomainException("Plataforma nao encontrada");
            }

            LerPlataformaDto plataformaDto = new LerPlataformaDto
            {
                PlataformaId = plataforma.PlataformaId,
                Nome = plataforma.Nome,
            };
            return plataformaDto;
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("Plataforma deve possuir um nome");
            }
        }

        public void Adicionar(CriarPlataformaDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            if (_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Plataforma já existe");
            }

            Plataforma plataforma = new Plataforma
            {
                Nome = criarDto.Nome,
            };

            _repository.Adicionar(plataforma);
        }

        public void Remover(int id)
        {
            Plataforma plataformaBanco = _repository.ObterPorId(id);

            if (plataformaBanco == null)
            {
                throw new DomainException("Plataforma nao encontrada");
            }

            _repository.Remover(id);
        }
    }
}
