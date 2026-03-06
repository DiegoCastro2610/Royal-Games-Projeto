using System;
using System.Collections.Generic;

namespace RoyalGames.Models;

public partial class ClassificacaoIndicativa
{
    public int ClassificacaoIndicativaId { get; set; }

    public string Classificao { get; set; } = null!;

    public virtual ICollection<Jogo> Jogos { get; set; } = new List<Jogo>();
}
