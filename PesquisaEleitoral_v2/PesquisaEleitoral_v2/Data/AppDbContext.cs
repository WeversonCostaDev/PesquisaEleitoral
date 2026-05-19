using Microsoft.EntityFrameworkCore;
using PesquisaEleitoral_v2.Models;

namespace PesquisaEleitoral_v2.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Eleitor> Eleitores { get; set; }
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Pesquisa> Pesquisas { get; set; }
        public DbSet<IntencaoDeVoto> IntencoesDeVoto { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }


}