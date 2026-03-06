using Microsoft.EntityFrameworkCore;
using RoyalGames.Dtos.ClassificacaoIndicativaDto;
using RoyalGames.Dtos.UsuarioDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using RoyalGames.Models;
using System.Security.Cryptography;
using System.Text;

namespace RoyalGames.Applications.Service
{
    public class ClassificacaoIndicativaService
    {
        private readonly IClassificacaoIndicativa _repository;

        public ClassificacaoIndicativaService(IClassificacaoIndicativa repository)
        {
            _repository = repository;
        }

        private static LerClassificacaoIndicativaDto LerDto(ClassificacaoIndicativa classificacao)
        {
            LerClassificacaoIndicativaDto lerclassificacaodto = new LerClassificacaoIndicativaDto
            {
                ClassificaoDto = classificacao.Classificao
            };

            return lerclassificacaodto;
            
        }
        public List<LerClassificacaoIndicativaDto> Listar()
        {
            List<ClassificacaoIndicativa> classificacao = _repository.Listar();

            List<LerClassificacaoIndicativaDto> ListaClassificacaoIndicativaDto = classificacao.Select(C => LerDto(C)).ToList();
            return ListaClassificacaoIndicativaDto;
        }



        public LerClassificacaoIndicativaDto ObterPorId(int id)
        {
            ClassificacaoIndicativa classificacao = _repository.ObterPorId(id);

            if (classificacao == null)
            {
                throw new DomainException("Não possui esse id");
            }

            return LerDto(classificacao);
        }





        public LerClassificacaoIndicativaDto Adicionar(CriarClassificacaoIndicativaDto ClassificacaoDto)
        {
            if (ClassificacaoDto == null)
            {
                throw new DomainException("Não existe nada escrito");
            }

            ClassificacaoIndicativa classificacao = new ClassificacaoIndicativa
            {
               Classificao = ClassificacaoDto.Classificao
            };

            _repository.Adicionar(classificacao);

            return LerDto(classificacao);
        }

        

        public void Remover(int id)
        {
            ClassificacaoIndicativa classificacao = _repository.ObterPorId(id);

            if (classificacao == null)
            {
                throw new DomainException("Usuario não existe");
            }

            _repository.Remover(id);
        }
    }
}
