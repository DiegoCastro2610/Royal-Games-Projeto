using System;
using System.Collections.Generic;

namespace RoyalGames.Models;

public partial class LogAlteracaoJogo
{
    public int LogAlteracaoJogoId { get; set; }

    public DateTime DataAlteracao { get; set; }

    public string NomeAnterior { get; set; } = null!;

    public decimal? PrecoAnterior { get; set; }

    public int? JogoId { get; set; }

    public virtual Jogo? Jogo { get; set; }
}
