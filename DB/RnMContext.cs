using Microsoft.EntityFrameworkCore;
using RnM.Api.Models;

namespace RnM.Api.DB
{
    public class RnMContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Episode> Episodes { get; set; }

        private IConfiguration _configuration;

        public RnMContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            optionsBuilder.UseSqlite($"Data Source={_configuration.GetValue<string>("Database")}.sqlite");
        }
    }
}
