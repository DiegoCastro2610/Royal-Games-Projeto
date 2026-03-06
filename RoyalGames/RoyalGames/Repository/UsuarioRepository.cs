using Microsoft.EntityFrameworkCore;
using RoyalGames.Contexts;
using RoyalGames.Interfaces;
using RoyalGames.Models;

namespace RoyalGames.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly RoyalGamesContext _context;

        public UsuarioRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
           return _context.Usuarios.ToList();
        }
        public Usuario? ObterPorEmail(string email)
        {
            return _context.Usuarios.FirstOrDefault(U => U.Email == email);

        }

        public Usuario? ObterPorId(int id)
        {

            return _context.Usuarios.Find(id);
        }

        public bool EmailExiste(string email)
        { 
            return _context.Usuarios.Any(U => U.Email == email);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Atualizar(Usuario usuario)
        {
            Usuario? usuarioDb = _context.Usuarios.FirstOrDefault(U => U.UsuarioId == usuario.UsuarioId);

            if (usuarioDb == null)
            {
                return;
            }

            usuarioDb.Nome = usuario.Nome;
            usuarioDb.Email = usuario.Email;
            usuarioDb.Senha = usuario.Senha;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Usuario? usuario = _context.Usuarios.FirstOrDefault(U => U.UsuarioId == id);

            if (usuario == null)
            {
                return;
            }

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }

    }
}
