using RoyalGames.Models;

namespace RoyalGames.Interfaces
{
    public interface IClassificacaoIndicativa
    {
        List<ClassificacaoIndicativa> Listar();
        ClassificacaoIndicativa ObterPorId(int id);
        void Adicionar(ClassificacaoIndicativa classificacao);
        void Deletar(int id);

    }
}
