﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecDesp.Domain.Models;
using RecDesp.Models;

namespace RecDesp.Infra
{
    public class MySQLContext : IdentityDbContext<ApplicationUser>
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<ApplicationRole> Role { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Cobranca> Cobrancas { get; set; }
        public DbSet<InstituicaoFinanceira> InstituicoesFinanceiras { get; set; }
        public DbSet<Credito> Creditos { get; set; }
        public DbSet<Transferencia> Transferencias { get; set; }
        public DbSet<Debito> Debitos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {      
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

            modelBuilder.Entity<InstituicaoFinanceira>()
                .HasData(
                    new InstituicaoFinanceira { Id = 1, Nome = "Banco do Brasil" },
                    new InstituicaoFinanceira { Id = 2, Nome = "Bradesco" },
                    new InstituicaoFinanceira { Id = 3, Nome = "Caixa Econômica Federal" },
                    new InstituicaoFinanceira { Id = 4, Nome = "Santander" }
                );
        }
    }
}
