using System.Collections.Generic;
using CodigoDoFuturoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CodigoDoFuturoApi.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) {}

        public DbSet<PessoaJuridica> PessoasJuridicas { get; set; } = default!;

        public DbSet<PessoaFisica> PessoasFisicas { get; set; } = default!;

        public DbSet<Orcamento> Orcamentos { get; set; } = default!;
    }
}

