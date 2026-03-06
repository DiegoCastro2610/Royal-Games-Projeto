using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RoyalGames.Models;

namespace RoyalGames.Contexts;

public partial class RoyalGamesContext : DbContext
{
    public RoyalGamesContext()
    {
    }

    public RoyalGamesContext(DbContextOptions<RoyalGamesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ClassificacaoIndicativa> ClassificacaoIndicativas { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Jogo> Jogos { get; set; }

    public virtual DbSet<LogAlteracaoJogo> LogAlteracaoJogos { get; set; }

    public virtual DbSet<Plataforma> Plataformas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Jogo> JogoId { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=D11S31-1313840\\SQLEXPRESS;Database=RoyalGames;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClassificacaoIndicativa>(entity =>
        {
            entity.HasKey(e => e.ClassificacaoIndicativaId).HasName("PK__Classifi__892DEC6FEF068E2C");

            entity.ToTable("ClassificacaoIndicativa");

            entity.Property(e => e.ClassificacaoIndicativaId).HasColumnName("ClassificacaoIndicativaID");
            entity.Property(e => e.Classificao)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.GeneroId).HasName("PK__Genero__A99D026893F47E38");

            entity.ToTable("Genero");

            entity.Property(e => e.GeneroId).HasColumnName("GeneroID");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Jogo>(entity =>
        {
            entity.HasKey(e => e.JogoId).HasName("PK__Jogo__59196855F6612EC5");

            entity.ToTable("Jogo", tb =>
                {
                    tb.HasTrigger("trg_AlteracaoJogo");
                    tb.HasTrigger("trg_ExclusaoJogo");
                });

            entity.Property(e => e.JogoId).HasColumnName("JogoID");
            entity.Property(e => e.ClassificacaoIndicativaId).HasColumnName("ClassificacaoIndicativaID");
            entity.Property(e => e.Nome)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.ClassificacaoIndicativa).WithMany(p => p.Jogos)
                .HasForeignKey(d => d.ClassificacaoIndicativaId)
                .HasConstraintName("FK__Jogo__Classifica__534D60F1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Jogos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Jogo__UsuarioID__52593CB8");

            entity.HasMany(d => d.Generos).WithMany(p => p.Jogos)
                .UsingEntity<Dictionary<string, object>>(
                    "JogoGenero",
                    r => r.HasOne<Genero>().WithMany()
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_JogoGenero_Genero"),
                    l => l.HasOne<Jogo>().WithMany()
                        .HasForeignKey("JogoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_JogoGenero_JogoID"),
                    j =>
                    {
                        j.HasKey("JogoId", "GeneroId");
                        j.ToTable("JogoGenero");
                        j.IndexerProperty<int>("JogoId").HasColumnName("JogoID");
                        j.IndexerProperty<int>("GeneroId").HasColumnName("GeneroID");
                    });

            entity.HasMany(d => d.Plataformas).WithMany(p => p.Jogos)
                .UsingEntity<Dictionary<string, object>>(
                    "JogoPlataforma",
                    r => r.HasOne<Plataforma>().WithMany()
                        .HasForeignKey("PlataformaId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_JogoPlataforma_PlataformaID"),
                    l => l.HasOne<Jogo>().WithMany()
                        .HasForeignKey("JogoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_JogoPlataforma_JogoID"),
                    j =>
                    {
                        j.HasKey("JogoId", "PlataformaId");
                        j.ToTable("JogoPlataforma");
                        j.IndexerProperty<int>("JogoId").HasColumnName("JogoID");
                        j.IndexerProperty<int>("PlataformaId").HasColumnName("PlataformaID");
                    });
        });

        modelBuilder.Entity<LogAlteracaoJogo>(entity =>
        {
            entity.HasKey(e => e.LogAlteracaoJogoId).HasName("PK__Log_alte__BB9D2C4FC6BEAB79");

            entity.ToTable("Log_alteracaoJogo");

            entity.Property(e => e.LogAlteracaoJogoId).HasColumnName("Log_AlteracaoJogoID");
            entity.Property(e => e.DataAlteracao).HasPrecision(0);
            entity.Property(e => e.JogoId).HasColumnName("JogoID");
            entity.Property(e => e.NomeAnterior)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PrecoAnterior).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Jogo).WithMany(p => p.LogAlteracaoJogos)
                .HasForeignKey(d => d.JogoId)
                .HasConstraintName("FK__Log_alter__JogoI__5629CD9C");
        });

        modelBuilder.Entity<Plataforma>(entity =>
        {
            entity.HasKey(e => e.PlataformaId).HasName("PK__Platafor__B835678DECAD75CC");

            entity.ToTable("Plataforma");

            entity.Property(e => e.PlataformaId).HasColumnName("PlataformaID");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuario__2B3DE798D180D153");

            entity.ToTable("Usuario", tb => tb.HasTrigger("trg_ExclusaoUsuario"));

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Nome)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Senha).HasMaxLength(32);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
