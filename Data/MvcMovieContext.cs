using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

    public DbSet<MvcMovie.Models.Movie> Movie { get; set; } = default!;
    public DbSet<Pessoa> Pessoa { get; set; }
    public DbSet<Turma> Turma { get; set; }
    public DbSet<MvcMovie.Models.Modalidade> Modalidade { get; set; } = default!;
    public DbSet<MvcMovie.Models.ModalidadeTurma> ModalidadeTurma { get; set; } = default!;
    public DbSet<InscricaoTurma> InscricaoTurma { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Garantir que o enum StatusTurma seja persistido como INT
        modelBuilder.Entity<MvcMovie.Models.Turma>()
            .Property(t => t.Status)
            .HasConversion<int>();

        // DataCriacao gerada pelo banco (assume coluna DATETIME / TIMESTAMP com DEFAULT CURRENT_TIMESTAMP)
        modelBuilder.Entity<MvcMovie.Models.Turma>()
            .Property(t => t.DataCriacao)
            .ValueGeneratedOnAdd()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Configuração do relacionamento many-to-many entre Modalidade e Turma
        modelBuilder.Entity<MvcMovie.Models.ModalidadeTurma>(entity =>
        {
            entity.ToTable("modalidade_turma");
            entity.HasKey(mt => new { mt.ModalidadeId, mt.TurmaId });

            entity.HasOne(mt => mt.Modalidade)
                .WithMany(m => m.ModalidadesTurmas)
                .HasForeignKey(mt => mt.ModalidadeId);

            entity.HasOne(mt => mt.Turma)
                .WithMany(t => t.ModalidadesTurmas)
                .HasForeignKey(mt => mt.TurmaId);
        });

        modelBuilder.Entity<Pessoa>().ToTable("Pessoa");
        modelBuilder.Entity<Turma>().ToTable("Turma");
        modelBuilder.Entity<InscricaoTurma>().ToTable("InscricaoTurma");

        modelBuilder.Entity<InscricaoTurma>()
            .HasIndex(i => new { i.PessoaId, i.TurmaId })
            .IsUnique();

        modelBuilder.Entity<InscricaoTurma>()
            .HasOne(i => i.Pessoa)
            .WithMany(p => p.Inscricoes)
            .HasForeignKey(i => i.PessoaId);

        modelBuilder.Entity<InscricaoTurma>()
            .HasOne(i => i.Turma)
            .WithMany(t => t.Inscricoes)
            .HasForeignKey(i => i.TurmaId);
    }
    }
}
