using RoyalGames.Contexts;
using RoyalGames.Interfaces;
using RoyalGames.Models;

namespace RoyalGames.Repository
{
    public class PlataformaRepository : IPlataformaRepository
    {
        public readonly RoyalGamesContext _context;

        public PlataformaRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Plataforma> Listar()
        {
            return _context.Plataformas.ToList();
        }

        public Plataforma ObterPorId(int id)
        {
            Plataforma plataforma = _context.Plataformas.FirstOrDefault(p => p.PlataformaId == id);

            return plataforma;
        }

        public bool NomeExiste(string nome, int? plataformaIdAtual = null)
        {
            var consulta = _context.Plataformas.AsQueryable();


            if (plataformaIdAtual.HasValue)
            {
                consulta = consulta.Where(plataforma => plataforma.PlataformaId != plataformaIdAtual.Value);
            }

            return consulta.Any(c => c.Nome == nome);
    }

        public void Adicionar(Plataforma plataforma)
        {
            _context.Plataformas.Add(plataforma);
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Plataforma plataformaBanco = _context.Plataformas.FirstOrDefault(p => p.PlataformaId == id);
           
            if(plataformaBanco == null)
            {
                return;
            }

            _context.Plataformas.Remove(plataformaBanco);
            _context.SaveChanges();
        }
    }
}

