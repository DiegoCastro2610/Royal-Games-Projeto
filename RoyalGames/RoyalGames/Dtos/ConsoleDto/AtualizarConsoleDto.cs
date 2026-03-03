namespace RoyalGames.Dtos.ConsoleDto
{
    public class AtualizarConsoleDto
    {
        public string Nome { get; set; }

        public decimal Preco { get; set; }

        public string Descricao { get; set; } = null!;

        public IFormFile Imagem { get; set; } = null!;

        public List<int> CategoriasIds { get; set; } = new List<int>();

        public bool? StatusConsole { get; set; }
    }
}
