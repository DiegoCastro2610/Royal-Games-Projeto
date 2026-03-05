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
        public Usuario ObterPorEmail(string email)
        {
            Usuario? EmailUsuario = _context.Usuarios
                .Include(U => U.UsuarioId)
                .Include(U => U.Nome)
                .Include(U => U.StatusUsuario)
                .FirstOrDefault(U => U.Email == email);

            return EmailUsuario;
        }

        public Usuario ObterPorId(int id)
        {
            Usuario? IdUsuario = _context.Usuarios
                .Include(U => U.Nome)
                .Include(U => U.Email)
                .Include(U => U.StatusUsuario)
                .FirstOrDefault(U => U.UsuarioId == id);

            return IdUsuario;
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
