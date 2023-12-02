using Bogus;
using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RnM.Api.Models;
using System.Collections;

namespace RnM.Api.DB
{
    public class RnMContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Location> Location { get; set; }

        private IConfiguration _configuration;
        private IWebHostEnvironment _environment;

        public RnMContext(IConfiguration configuration, IWebHostEnvironment environment, DbContextOptions options) : base(options) 
        {
            _configuration = configuration;
            _environment = environment;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var statusConverter = new EnumToStringConverter<CharacterStatus>();
            var genderConverter = new EnumToStringConverter<CharacterGender>();

            modelBuilder
                .Entity<Character>()
                .Property(e => e.Status)
                .HasConversion(statusConverter);

            modelBuilder
                .Entity<Character>()
                .Property(e => e.Gender)
                .HasConversion(genderConverter);
        }
    }
}
