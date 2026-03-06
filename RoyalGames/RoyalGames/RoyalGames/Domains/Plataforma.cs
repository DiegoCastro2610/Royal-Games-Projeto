using System;
using System.Collections.Generic;

namespace RoyalGames.Models;

public partial class Plataforma
{
    public int PlataformaId { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Jogo> Jogos { get; set; } = new List<Jogo>();
}
