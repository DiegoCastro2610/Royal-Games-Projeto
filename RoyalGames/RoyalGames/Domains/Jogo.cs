using System;
using System.Collections.Generic;

namespace RoyalGames.Models;

public partial class Jogo
{
    public int JogoId { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Preco { get; set; }

    public string Descricao { get; set; } = null!;

    public byte[] Imagem { get; set; } = null!;

    public bool StatusJogo { get; set; }

    public int? UsuarioId { get; set; }

    public int? ClassificacaoIndicativaId { get; set; }

    public virtual ClassificacaoIndicativa? ClassificacaoIndicativa { get; set; }

    public virtual ICollection<LogAlteracaoJogo> LogAlteracaoJogos { get; set; } = new List<LogAlteracaoJogo>();

    public virtual Usuario? Usuario { get; set; }

    public virtual ICollection<Genero> Generos { get; set; } = new List<Genero>();

    public virtual ICollection<Plataforma> Plataformas { get; set; } = new List<Plataforma>();
}
