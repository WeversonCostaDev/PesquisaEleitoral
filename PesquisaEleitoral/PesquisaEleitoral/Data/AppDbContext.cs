using Microsoft.EntityFrameworkCore;
using PesquisaEleitoral.Models;

namespace PesquisaEleitoral.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Eleitor> Eleitores { get; set; }
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<IntencaoDeVoto> IntencoesDeVoto { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { } 
    }
}
