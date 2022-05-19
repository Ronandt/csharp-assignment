
using Microsoft.EntityFrameworkCore;

namespace PokemonGame
{
    public class PokemonContext : DbContext
    {
        public DbSet<Pokemon> Pokemons { get; set; } = null!; //null forgiving operator to remove warning of EFCore
        public DbSet<Pikachu> Pikachus { get; set; } = null!;
        public DbSet<Eevee> Evees { get; set; } = null!;
        public DbSet<Charmander> Charmanders { get; set; } = null!;
        public DbSet<Yuuki>  Yuukis { get; set; } = null!;
        public DbSet<Ball> Balls {get;set;} = null!;
        public DbSet<Pokeball> Pokeballs {get;set;} = null!;
        public string DbPath { get; }

        public PokemonContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "pokemon.db");

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source = {DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pokemon>().Property(self => self.EvolutionLimit).HasDefaultValue(100); //If pokemon reaches this amount of exp, they can try and sacrfiice pokemons to evolve themselves
            modelBuilder.Entity<Pokemon>().Property(self => self.HasEvolved).HasDefaultValue(0);
        
            

        }



    }

}