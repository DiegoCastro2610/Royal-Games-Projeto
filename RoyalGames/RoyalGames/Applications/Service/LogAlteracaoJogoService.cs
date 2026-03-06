using RoyalGames.Dtos.LogProdutoDto;
using RoyalGames.Exceptions;
using RoyalGames.Interfaces;
using RoyalGames.Models;

namespace RoyalGames.Applications.Service
{
    public class LogAlteracaoJogoService
    {
        private readonly ILogAlteracaoJogoRepository _repository;

        public LogAlteracaoJogoService(ILogAlteracaoJogoRepository repository)
        {
            _repository = repository;
        }

        public List<LerLogJogoDto> Listar()
        {
            List<LogAlteracaoJogo> logs = _repository.Listar();

            List<LerLogJogoDto> listaLogJogo = logs.Select(log => new LerLogJogoDto { LogID = log.LogAlteracaoJogoId, JogoID = log.JogoId, NomeAnterior = log.NomeAnterior, PrecoAnterior = log.PrecoAnterior, DataAlteracao = log.DataAlteracao }).ToList();
            return listaLogJogo;
        }

        public List<LerLogJogoDto> ListarPorJogo(int produtoId)
        {
            List<LogAlteracaoJogo> logs = _repository.ListarPorJogoId(produtoId);

            List<LerLogJogoDto>? listaLogJogo = logs.Select(log => new LerLogJogoDto { LogID = log.LogAlteracaoJogoId, JogoID = log.JogoId, NomeAnterior = log.NomeAnterior, PrecoAnterior = log.PrecoAnterior, DataAlteracao = log.DataAlteracao }).ToList();

            if (listaLogJogo.Count == 0)
            {
                listaLogJogo = null;
                throw new DomainException("não teve alteração");
            }

            return listaLogJogo;

        }
    }
}   
