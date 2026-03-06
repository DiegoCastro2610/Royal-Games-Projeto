namespace RoyalGames.Dtos.UsuarioDto
{
    public class LerUsuarioDto
    {

        public int UsuarioId { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool StatusUsuario { get; set; }
    }
}
