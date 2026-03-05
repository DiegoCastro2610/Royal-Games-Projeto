using RoyalGames.Dtos.ClassificacaoIndicativaDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using RoyalGames.Models;

namespace RoyalGames.Applications.Service
{
    public class ClassificacaoIndicativaService
    {
        private readonly IClassificacaoIndicativa _Repository;

        public ClassificacaoIndicativaService(IClassificacaoIndicativa Repository)
        {
            _Repository = Repository;
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
            List<ClassificacaoIndicativa> classificacao = _Repository.Listar();
            List<LerClassificacaoIndicativaDto> listarclassificacaodto = classificacao.Select(C => LerDto(C)).ToList();
            return listarclassificacaodto;
        }


        public LerClassificacaoIndicativaDto ObterPorId(int id)
        {
            ClassificacaoIndicativa classificacao = _Repository.ObterPorId(id);

            if(classificacao == null)
            {
                throw new DomainException("Não Existe essa Classificação");
            }
            return LerDto(classificacao);
        }
        public LerClassificacaoIndicativaDto Adicionar(CriarClassificacaoIndicativaDto classificacaodto)
        {

            ClassificacaoIndicativa classificacao  = new ClassificacaoIndicativa
            {
                Classificao = classificacaodto.ClassificaoDto
            };

            _Repository.Adicionar(classificacao);

            return LerDto(classificacao);
        }
        public void Deletar(int id)
        {
            ClassificacaoIndicativa classificacao = _Repository.ObterPorId(id);

            if(classificacao == null)
            {
                throw new DomainException("não possui essa classificação");
            }
        }
    }
}
