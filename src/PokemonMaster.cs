namespace PokemonGame
{
    public class PokemonMaster
    {

        public PokemonMaster(int noToEvolve, List<(string, string)> evolveTo) {
            NoToEvolve = noToEvolve;
            EvolveTo = InitaliseEvolutions(evolveTo);
        }

        public int NoToEvolve {get;set;}
        public Dictionary<string,string> EvolveTo {get;}

        private Dictionary<string, string> InitaliseEvolutions(List<(string, string)>customEvolutions)
        {
            List<string> subclasses = typeof(Pokemon).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Pokemon)) && GetTables.EntityTypes().Contains(type.ToString().ToLower())).Select(x => x.ToString()).ToList();
            Dictionary<string, string> evolutions = subclasses.ToDictionary(x => x.Split(".")[1], x => "Evolved " + x.Split(".")[1]);
            foreach ((string, string) customEvolution in customEvolutions)
            {
                evolutions[customEvolution.Item1] = customEvolution.Item2;
            }
            return evolutions;

        }

      

        public void PokemonsCanBeEvolved(List<Pokemon> allTables)
        {

            foreach (Pokemon element in allTables)
            {

                //check the evolution limit to evolve(which I set 100exp in Model.cs)
                if (element.Exp >= element.EvolutionLimit && element.HasEvolved == 0 && allTables.Where(x => (element.ToString() == x.ToString()) && (element.Name == x.Name)).Count() > NoToEvolve)
                {
                    Console.WriteLine($"(ID: {element.PokemonId}) {element.Name} --> {EvolveTo[element.Name!]}");
                }

            }

        }

        public void EvolvePokemon(List<Pokemon> allTables)
        {
            int pokeId;
            Pokemon first;
            while (true)
            {
                try
                {
                    Console.Write("Please enter ID to evolve a pokemon: ");
                    if (Int32.TryParse(Console.ReadLine(), out int pokemonIdentity))
                    {
                        pokeId = pokemonIdentity;
                        first = allTables.Where(self => self.PokemonId == pokeId).First();
                        break;
                    }
                    Console.WriteLine("Invalid ID");
                }
                catch
                {
                    Console.WriteLine("There is no such ID"); //pokemon sacrificed itself gg
                }
            }
           //check the evolution limit to evolve(which I set 100exp in Model.cs)
            if (first.Exp >= first.EvolutionLimit && first.HasEvolved == 0 && allTables.Where(x => (first.ToString() == x.ToString()) && (first.Name == x.Name)).Count() > NoToEvolve)
            {





                using (PokemonContext db = new PokemonContext())
                {

                    List<int> sacrificePokemons = new List<int>();


                    int sacrifice;
                    for(int x = 0; x < NoToEvolve; x++ ) {
                    while (true)
                    {
                        try
                        {
                            Console.Write($"Plese enter ID to sacrifice pokemon ({x + 1}): ");
                            if (Int32.TryParse(Console.ReadLine(), out int sacrificedPokemon))
                            {
                                sacrifice = sacrificedPokemon;
                                Pokemon query = allTables.Where(self => self.PokemonId == sacrifice).First();
                                if (query.ToString() != first.ToString()) {
                                    Console.WriteLine("That is not a suitable pokemon to merge with your current pokemon");
                                    continue;
                                }
                                else if (query.PokemonId == first.PokemonId) {
                                    Console.WriteLine("You can't sacrifice the pokemon you want to evolve!");
                                    continue;
                                }
                                break;

                            }//what happens iuf you sacrifice other pokemon
                        }
                        catch
                        {
                            Console.WriteLine("There is no such ID");
                        }
                    }
                    sacrificePokemons.Add(sacrifice);
                    }
                    db.RemoveRange(db.Pokemons.Where(x => sacrificePokemons.Contains(x.PokemonId)));

                    Pokemon pokemonInfoPokemon = db.Pokemons.Where(x => x.PokemonId == pokeId).First();
                    pokemonInfoPokemon!.HasEvolved = 1;
                    pokemonInfoPokemon.Atk *= 2;
                    pokemonInfoPokemon.Exp = 0;
                    pokemonInfoPokemon.HP *= 2; //for pokemon battles so it needs more hp when it evolves
                    pokemonInfoPokemon.Name = EvolveTo[pokemonInfoPokemon.Name!];
                    db.SaveChanges();

                }
                Console.WriteLine("Your pokemon has evolved!");
            }
            else
            {
                Console.WriteLine("Your pokemon does not meet the evolution criterias. Please meet the following conditions to evolve: Exp and Sacrifices");
            }







        }


    }
}