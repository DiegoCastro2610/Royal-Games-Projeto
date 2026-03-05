using Microsoft.EntityFrameworkCore;
using RoyalGames.Contexts;
using RoyalGames.Interfaces;
using RoyalGames.Models;


namespace RoyalGames.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private readonly RoyalGamesContext _context;

        public JogoRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Jogo> Listar()
        {
            List<Jogo> ListaJogos = _context.Jogos
                .Include(jogo => jogo.Generos) 
                .Include(jogo => jogo.ClassificacaoIndicativa)
                .ToList();

            return ListaJogos;
        }

        public Jogo ObterPorId(int id) // id = 5 -> Sorvete
        {
            Jogo? Jogo = _context.Jogos
                .Include(jogoDb => jogoDb.Generos)
                .Include(jogoDb => jogoDb.Usuario)

                // Procura no banco (aux produtoDb) e verifica se o ID do produto no banco é igual ao id passado como parâmetro no método ObterPorId
                .FirstOrDefault(jogoDb => jogoDb.JogoId == id);

            return Jogo;
        }

        public byte[] ObterImagem(int id)
        {
            var jogo = _context.Jogos
                .Where(jogo => jogo.JogoId == id)
                .Select(jogo => jogo.Imagem)
                .FirstOrDefault();

            return jogo;
        }

        public bool NomeExiste(string nome, int? jogoIdAtual = null)
        {
          
            var jogoConsultado = _context.Jogos.AsQueryable();


            if (jogoIdAtual.HasValue)
            {
                jogoConsultado = jogoConsultado.Where(jogo => jogo.JogoId != jogoIdAtual.Value);
            }

            return jogoConsultado.Any(jogo => jogo.Nome == nome);
        }

        

        public void Adicionar(Jogo jogo, List<int> generoIds)
        {
            List<Genero> generos = _context.Generos
                .Where(genero => generoIds.Contains(genero.GeneroId))
                .ToList(); // Contains -> retorna true se houver o registro

            jogo.Generos = generos; // adiciona as categorias incluidas ao produto

            _context.Jogos.Add(jogo);
            _context.SaveChanges();
        }

        public void Atualizar(Jogo jogo, List<int> generoIds)
        {
            Jogo? jogoBanco = _context.Jogos
                .Include(jogo => jogo.Generos)
                .FirstOrDefault(jogoAux => jogoAux.JogoId == jogo.JogoId);

            if (jogoBanco == null)
            {
                return;
            }

            jogoBanco.Nome = jogo.Nome;
            jogoBanco.Preco = jogo.Preco;
            jogoBanco.Descricao = jogo.Descricao;

            if (jogo.Imagem != null && jogo.Imagem.Length > 0)
            {
                jogoBanco.Imagem = jogo.Imagem;
            }

            if (jogo.StatusJogo == true)
            {
                jogoBanco.StatusJogo = jogo.StatusJogo;
            }

          
            var generos = _context.Generos
                .Where(genero => generoIds.Contains(genero.GeneroId))
                .ToList();

            jogoBanco.Generos.Clear();

            foreach (var genero in generos)
            {
                jogoBanco.Generos.Add(genero);
            }

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Jogo? jogo = _context.Jogos.FirstOrDefault(jogo => jogo.JogoId == id);

            if (jogo == null)
            {
                return;
            }

            _context.Jogos.Remove(jogo);
            _context.SaveChanges();
        }

    }
}
