namespace RoyalGames.Dtos.JogosDto
{
    public class LerJogoDto
    {
        public int ProdutoId { get; set; }

        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public bool? StatusJogo { get; set; }

        //generos
        public List<string> Genero { get; set; } = new List<string>();
        
        public List<string> GeneroId { get; set; } = new List<string>();

        public int? UsuarioId { get; set; }

        public string? UsuarioNome { get; set; }

        public string? UsuarioEmail { get; set; }
    }
}
