using Microsoft.EntityFrameworkCore;
using controleDeGastos.Domain.Entities;

namespace controleDeGastos.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Ano> Anos => Set<Ano>();
    public DbSet<Mes> Meses => Set<Mes>();
    public DbSet<Receita> Receitas => Set<Receita>();
    public DbSet<Investimento> Investimentos => Set<Investimento>();
    public DbSet<TipoDespesa> TiposDespesa => Set<TipoDespesa>();
    public DbSet<CategoriaDespesa> CategoriasDespesa => Set<CategoriaDespesa>();
    public DbSet<SubCategoriaDespesa> SubCategoriasDespesa => Set<SubCategoriaDespesa>();
    public DbSet<Despesa> Despesas => Set<Despesa>();
    public DbSet<DespesaRecorrente> DespesasRecorrentes => Set<DespesaRecorrente>();
    public DbSet<Saldo> Saldos => Set<Saldo>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplica todas as configurações do assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Configurações básicas inline (serão movidas para Configurations depois)
        
        // Usuario
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(200);
            entity.Property(e => e.SenhaHash).IsRequired().HasMaxLength(200);
            entity.Property(e => e.RefreshToken).HasMaxLength(200);
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Ano
        modelBuilder.Entity<Ano>(entity =>
        {
            entity.HasKey(e => e.AnoId);
            entity.Property(e => e.Numero).IsRequired();
            entity.HasIndex(e => new { e.Numero, e.UsuarioId }).IsUnique();

            entity.HasOne(e => e.Usuario)
                  .WithMany(u => u.Anos)
                  .HasForeignKey(e => e.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Mes
        modelBuilder.Entity<Mes>(entity =>
        {
            entity.HasKey(e => e.MesId);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Numero).IsRequired();
            
            entity.HasOne(e => e.Ano)
                  .WithMany(a => a.Meses)
                  .HasForeignKey(e => e.AnoId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Receita
        modelBuilder.Entity<Receita>(entity =>
        {
            entity.HasKey(e => e.ReceitaId);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Valor).HasPrecision(18, 2);
            
            entity.HasOne(e => e.Mes)
                  .WithMany(m => m.Receitas)
                  .HasForeignKey(e => e.MesId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Investimento
        modelBuilder.Entity<Investimento>(entity =>
        {
            entity.HasKey(e => e.InvestimentoId);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Valor).HasPrecision(18, 2);
            entity.Property(e => e.PercentualReceita).HasPrecision(5, 2);
            
            entity.HasOne(e => e.Mes)
                  .WithMany(m => m.Investimentos)
                  .HasForeignKey(e => e.MesId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // TipoDespesa
        modelBuilder.Entity<TipoDespesa>(entity =>
        {
            entity.HasKey(e => e.TipoDespesaId);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(50);
        });

        // CategoriaDespesa
        modelBuilder.Entity<CategoriaDespesa>(entity =>
        {
            entity.HasKey(e => e.CategoriaId);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
        });

        // SubCategoriaDespesa
        modelBuilder.Entity<SubCategoriaDespesa>(entity =>
        {
            entity.HasKey(e => e.SubCategoriaId);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            
            entity.HasOne(e => e.Categoria)
                  .WithMany(c => c.SubCategorias)
                  .HasForeignKey(e => e.CategoriaId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Despesa
        modelBuilder.Entity<Despesa>(entity =>
        {
            entity.HasKey(e => e.DespesaId);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Valor).HasPrecision(18, 2);
            entity.Property(e => e.PercentualReceita).HasPrecision(5, 2);
            
            entity.HasOne(e => e.Mes)
                  .WithMany(m => m.Despesas)
                  .HasForeignKey(e => e.MesId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasOne(e => e.TipoDespesa)
                  .WithMany(t => t.Despesas)
                  .HasForeignKey(e => e.TipoDespesaId)
                  .OnDelete(DeleteBehavior.Restrict);
                  
            entity.HasOne(e => e.SubCategoria)
                  .WithMany(s => s.Despesas)
                  .HasForeignKey(e => e.SubCategoriaId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.DespesaRecorrente)
                  .WithMany(dr => dr.Despesas)
                  .HasForeignKey(e => e.DespesaRecorrenteId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // DespesaRecorrente
        modelBuilder.Entity<DespesaRecorrente>(entity =>
        {
            entity.HasKey(e => e.DespesaRecorrenteId);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Valor).HasPrecision(18, 2);

            entity.HasOne(e => e.Usuario)
                  .WithMany()
                  .HasForeignKey(e => e.UsuarioId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.TipoDespesa)
                  .WithMany()
                  .HasForeignKey(e => e.TipoDespesaId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.SubCategoria)
                  .WithMany()
                  .HasForeignKey(e => e.SubCategoriaId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Saldo (1:1 com Mes)
        modelBuilder.Entity<Saldo>(entity =>
        {
            entity.HasKey(e => e.SaldoId);
            entity.Property(e => e.Receita).HasPrecision(18, 2);
            entity.Property(e => e.Investimentos).HasPrecision(18, 2);
            entity.Property(e => e.DespesasFixas).HasPrecision(18, 2);
            entity.Property(e => e.DespesasVariaveis).HasPrecision(18, 2);
            entity.Property(e => e.DespesasExtras).HasPrecision(18, 2);
            entity.Property(e => e.DespesasAdicionais).HasPrecision(18, 2);
            entity.Property(e => e.Balanco).HasPrecision(18, 2);
            entity.Property(e => e.Observacao).HasMaxLength(500);
            
            entity.HasOne(e => e.Mes)
                  .WithOne(m => m.Saldo)
                  .HasForeignKey<Saldo>(e => e.MesId)
                  .OnDelete(DeleteBehavior.Cascade);
                  
            entity.HasIndex(e => e.MesId).IsUnique();
        });

        // Seed Data - TipoDespesa
        modelBuilder.Entity<TipoDespesa>().HasData(
            new TipoDespesa { TipoDespesaId = 1, Nome = "Fixa" },
            new TipoDespesa { TipoDespesaId = 2, Nome = "Variável" },
            new TipoDespesa { TipoDespesaId = 3, Nome = "Extra" },
            new TipoDespesa { TipoDespesaId = 4, Nome = "Adicional" }
        );

        // Seed Data - CategoriaDespesa
        modelBuilder.Entity<CategoriaDespesa>().HasData(
            new CategoriaDespesa { CategoriaId = 1, Nome = "Habitação" },
            new CategoriaDespesa { CategoriaId = 2, Nome = "Transporte" },
            new CategoriaDespesa { CategoriaId = 3, Nome = "Saúde" },
            new CategoriaDespesa { CategoriaId = 4, Nome = "Educação" },
            new CategoriaDespesa { CategoriaId = 5, Nome = "Impostos" },
            new CategoriaDespesa { CategoriaId = 6, Nome = "Alimentação" },
            new CategoriaDespesa { CategoriaId = 7, Nome = "Cuidados pessoais" },
            new CategoriaDespesa { CategoriaId = 8, Nome = "Lazer" },
            new CategoriaDespesa { CategoriaId = 9, Nome = "Vestuário" },
            new CategoriaDespesa { CategoriaId = 10, Nome = "Manutenção/prevenção" },
            new CategoriaDespesa { CategoriaId = 11, Nome = "Outros" }
        );

        // Seed Data - SubCategoriaDespesa
        modelBuilder.Entity<SubCategoriaDespesa>().HasData(
            // Habitação
            new SubCategoriaDespesa { SubCategoriaId = 1, CategoriaId = 1, Nome = "Aluguel" },
            new SubCategoriaDespesa { SubCategoriaId = 2, CategoriaId = 1, Nome = "Condomínio" },
            new SubCategoriaDespesa { SubCategoriaId = 3, CategoriaId = 1, Nome = "Luz" },
            new SubCategoriaDespesa { SubCategoriaId = 4, CategoriaId = 1, Nome = "Água" },
            new SubCategoriaDespesa { SubCategoriaId = 5, CategoriaId = 1, Nome = "Spotify" },
            new SubCategoriaDespesa { SubCategoriaId = 6, CategoriaId = 1, Nome = "Telefone celular" },
            new SubCategoriaDespesa { SubCategoriaId = 7, CategoriaId = 1, Nome = "Gás" },
            new SubCategoriaDespesa { SubCategoriaId = 8, CategoriaId = 1, Nome = "Mensalidade TV" },
            new SubCategoriaDespesa { SubCategoriaId = 9, CategoriaId = 1, Nome = "Animais" },
            new SubCategoriaDespesa { SubCategoriaId = 10, CategoriaId = 1, Nome = "Internet" },
            // Transporte
            new SubCategoriaDespesa { SubCategoriaId = 11, CategoriaId = 2, Nome = "Prestação da moto" },
            new SubCategoriaDespesa { SubCategoriaId = 12, CategoriaId = 2, Nome = "Seguro da moto" },
            new SubCategoriaDespesa { SubCategoriaId = 13, CategoriaId = 2, Nome = "Transporte por app" },
            new SubCategoriaDespesa { SubCategoriaId = 14, CategoriaId = 2, Nome = "Combustível" },
            new SubCategoriaDespesa { SubCategoriaId = 15, CategoriaId = 2, Nome = "Estacionamento" },
            // Alimentação
            new SubCategoriaDespesa { SubCategoriaId = 16, CategoriaId = 6, Nome = "Supermercado" },
            new SubCategoriaDespesa { SubCategoriaId = 17, CategoriaId = 6, Nome = "Feira" },
            new SubCategoriaDespesa { SubCategoriaId = 18, CategoriaId = 6, Nome = "Hortifruti" },
            new SubCategoriaDespesa { SubCategoriaId = 19, CategoriaId = 6, Nome = "Padaria" },
            // Saúde
            new SubCategoriaDespesa { SubCategoriaId = 20, CategoriaId = 3, Nome = "Plano de saúde" },
            new SubCategoriaDespesa { SubCategoriaId = 21, CategoriaId = 3, Nome = "Medicamentos" },
            new SubCategoriaDespesa { SubCategoriaId = 22, CategoriaId = 3, Nome = "Consultas" },
            // Educação
            new SubCategoriaDespesa { SubCategoriaId = 23, CategoriaId = 4, Nome = "Cursos" },
            new SubCategoriaDespesa { SubCategoriaId = 24, CategoriaId = 4, Nome = "Livros" },
            // Lazer
            new SubCategoriaDespesa { SubCategoriaId = 25, CategoriaId = 8, Nome = "Restaurantes" },
            new SubCategoriaDespesa { SubCategoriaId = 26, CategoriaId = 8, Nome = "Cinema" },
            new SubCategoriaDespesa { SubCategoriaId = 27, CategoriaId = 8, Nome = "Viagens" },
            // Outros
            new SubCategoriaDespesa { SubCategoriaId = 28, CategoriaId = 11, Nome = "Outros" }
        );
    }
}
