using RoyalGames.Contexts;
using RoyalGames.Interfaces;
using RoyalGames.Models;

namespace RoyalGames.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        public readonly RoyalGamesContext _context;

        public GeneroRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Genero> Listar()
        {
            return _context.Generos.ToList();
        }

        public Genero ObterPorId(int id)
        {
            Genero genero = _context.Generos.FirstOrDefault(g => g.GeneroId == id);

            return genero;
        }

        public bool NomeExiste(string nome, int? generoIdAtual = null)
        {
            var consulta = _context.Generos.AsQueryable();

            if (generoIdAtual.HasValue)
            {
                consulta = consulta.Where(genero => genero.GeneroId != generoIdAtual.Value);
            }

            return consulta.Any(g => g.Nome == nome);
        }

        public void Adicionar(Genero genero)
        {
            _context.Generos.Add(genero);
            _context.SaveChanges();
        }

        public void Atualizar(Genero genero)
        {
            Genero generoBanco = _context.Generos
                .FirstOrDefault(g => g.GeneroId == genero.GeneroId);

            if (generoBanco == null)
                return;

            generoBanco.Nome = genero.Nome;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Genero generoBanco = _context.Generos
                .FirstOrDefault(g => g.GeneroId == id);

            if (generoBanco == null)
                return;

            _context.Generos.Remove(generoBanco);
            _context.SaveChanges();
        }
    }
}