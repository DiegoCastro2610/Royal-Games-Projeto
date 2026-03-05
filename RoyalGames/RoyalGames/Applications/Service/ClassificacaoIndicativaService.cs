using RoyalGames.Dtos.ClassificacaoIndicativaDto;
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
                Classificao = classificacao.Classificao
            };

            return lerclassificacaodto;
            
        }


    }
}
