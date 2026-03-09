namespace RoyalGames.Dtos.JogosDto
{
    public class CriarJogoDto
    {
        public string Nome { get; set; } = null!;
        public decimal preco { get; set; }
        public string Descricao { get; set; } = null!;
        public IFormFile Imagem { get; set; } = null!;
        public List<int> GeneroIds { get; set; } = new List<int>();

    }
}
