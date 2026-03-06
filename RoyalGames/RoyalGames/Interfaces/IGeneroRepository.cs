using RoyalGames.Models;

namespace RoyalGames.Interfaces
{
    public interface IGeneroRepository
    {
        List<Genero> Listar();

        Genero ObterPorId(int id);

        bool NomeExiste(string nome, int? generoIdAtual = null);

        void Adicionar(Genero genero);

        void Remover(int id);
    }
}
