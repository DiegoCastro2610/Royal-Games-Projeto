using RoyalGames.Models;

namespace RoyalGames.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();

        Usuario ObterPorEmail(string email);
        Usuario ObterPorId(int id);
        bool EmailExiste(string email);
        void Adicionar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Remover(int id);
    }
}
