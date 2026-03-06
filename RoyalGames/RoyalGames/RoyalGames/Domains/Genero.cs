using System;
using System.Collections.Generic;

namespace RoyalGames.Models;

public partial class Genero
{
    public int GeneroId { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Jogo> Jogos { get; set; } = new List<Jogo>();
}
