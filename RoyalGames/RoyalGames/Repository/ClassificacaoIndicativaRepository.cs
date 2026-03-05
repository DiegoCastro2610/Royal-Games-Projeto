using RoyalGames.Contexts;
using RoyalGames.Interfaces;
using RoyalGames.Models;

namespace RoyalGames.Repository
{
    public class ClassificacaoIndicativaRepository : IClassificacaoIndicativa
    {
        private readonly RoyalGamesContext _context;

        public ClassificacaoIndicativaRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<ClassificacaoIndicativa> Listar()
        {
            return _context.ClassificacaoIndicativas.ToList();
        }

        public ClassificacaoIndicativa ObterPorId(int id)
        {
            return _context.ClassificacaoIndicativas.FirstOrDefault(C => C.ClassificacaoIndicativaId == id);
        }

        public void Adicionar(ClassificacaoIndicativa classificacao)
        {
            _context.Add(classificacao);
            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            ClassificacaoIndicativa? classificacao = _context.ClassificacaoIndicativas.FirstOrDefault(C => C.ClassificacaoIndicativaId == id);

            if (classificacao == null)
            {
                return;
            }

            _context.ClassificacaoIndicativas.Remove(classificacao);
            _context.SaveChanges();
        }
    }
}
