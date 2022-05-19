namespace PokemonGame {
    class GetTables {
             public static List<Pokemon> AllTables()
        {
            using (PokemonContext db = new PokemonContext())
            {
                List<Pokemon> allTables = db.Pokemons.ToList();
                return allTables;
            }
        }

        public static List<String> EntityTypes() {
           using (PokemonContext db = new PokemonContext())
            {
                List<string> entityTables = db.Model.GetEntityTypes().Select(t => t.ClrType.ToString().ToLower()).ToList();
                return entityTables;
            }
        }
    }
}