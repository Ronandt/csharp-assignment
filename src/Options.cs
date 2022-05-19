
namespace PokemonGame
{
    public class Options
    {

        private static PokemonMaster EvolutionMaster = new PokemonMaster(1, new List<(string, string)> { ("Pikachu", "Raichu"), ("Eevee", "Flareon"), ("Charmander", "Charmeleon") });
        //add class name and evoled name to create custom evolution names, instead of Evolved + class name


        public static void AddingPokemon()
        {


            string pokemon;
            int hp;
            int exp;
            int atk;
            List<string> entities = GetTables.EntityTypes();



            while (true)
            {

                List<string> subclasses = typeof(Pokemon).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Pokemon)) && entities.Contains(type.ToString().ToLower())).Select(x => x.ToString().ToLower()).ToList();
                Display.ViewPokemons(subclasses); //display all the pokemon that can be used 
                Console.Write("Select your pokemon (Enter your pokemon's name): ");
                pokemon = Console.ReadLine()!.Trim().ToLower();

                if (subclasses.Contains("pokemongame." + pokemon))
                {
                    break;
                }
                Console.WriteLine("No such pokemon");
            }

            while (true)
            {
                Console.Write("Enter Pokemon's HP: ");
                if (Int32.TryParse(Console.ReadLine()!.Trim().ToLower(), out int parsedHp))
                {
                    hp = parsedHp;
                    break;
                }
                Console.WriteLine("Not valid HP or HP too high/low for pokemon");
            }

            while (true)
            {
                Console.Write("Enter Pokemon's Exp: ");
                if (Int32.TryParse(Console.ReadLine()!.Trim(), out int parsedExp))
                {
                    exp = parsedExp;
                    break;
                }
                Console.WriteLine("Not valid Exp or Exp too high/low for pokemon");
            }

            while (true)
            {
                Console.Write("Enter Pokemon's Atk: ");
                if (Int32.TryParse(Console.ReadLine()!.Trim(), out int parsedAtk))
                {
                    atk = parsedAtk;
                    break;
                }
                Console.WriteLine("Not valid Atk or Atk too high/low for pokemon");
            }
            using (PokemonContext db = new PokemonContext())
            {

                db.Add((Pokemon)Activator.CreateInstance(Type.GetType("PokemonGame." + Char.ToString(pokemon[0]).ToUpper() + pokemon.Substring(1))!, Math.Max(hp, 0), Math.Max(0, exp), Math.Max(atk, 0))!);
                db.SaveChanges();
            }









        }

        public static void DisplayPokemon()
        {

            Console.WriteLine(String.Join("\n", GetTables.AllTables().OrderBy(o => o.HP).Select(x => x.ToString()).ToList()));
            using (PokemonContext db = new PokemonContext())
            {
                Console.WriteLine($"You have: {db.Balls.Where(x => x.Id == 1).First().Counter} Pokeballs");
            }

        }
        public static void CanBeEvolved()
        {
            EvolutionMaster.PokemonsCanBeEvolved(GetTables.AllTables());
        }

        public static void EvolvePokemon()
        {
            EvolutionMaster.EvolvePokemon(GetTables.AllTables());
        }

        public static void PokemonBattle()
        {
            int pokemonId;
            Pokemon first;
            while (true)
            {
                try
                {
                    Console.Write("Please select a pokemon using ID to fight: ");
                    if (Int32.TryParse(Console.ReadLine()!.Trim(), out int pokeID))
                    {
                        pokemonId = pokeID;
                        first = GetTables.AllTables().Where(x => x.PokemonId == pokemonId).First();
                        break;
                    };
                }
                catch
                {
                    Console.WriteLine("There is no Pokemon with this ID in your list");
                    continue;
                }
                Console.WriteLine("Input a proper ID");
            }




            using (PokemonContext db = new PokemonContext())
            {


                Enemy enemy = new Enemy();
                int enemyHighestHit = 0;
                int yourHighestHit = 0;
                int round = 0;
                string winner = default!;
                Console.WriteLine(String.Concat(Enumerable.Repeat("=", 50)));
                Console.WriteLine(@"          />_________________________________
[########[]_________________________________>
         \>

         POKEMON BATTLES
");
                Console.WriteLine(String.Concat(Enumerable.Repeat("=", 50)));

                Console.WriteLine($"You will be facing {enemy.Name} with HP: {enemy.HP} and ATK: {enemy.Atk}");

                Console.WriteLine(String.Concat(Enumerable.Repeat("=", 30)));


                Pokemon pokemonInfoPokemon = db.Pokemons.Where(x => x.PokemonId == pokemonId).First();
                Random random = new Random();
                int initialHP = pokemonInfoPokemon.HP;
                while (true)
                {
                    round += 1;
                    Console.WriteLine($"ROUND {round}");
                    int atkPokemon = random.Next(pokemonInfoPokemon.Atk + 1);
                    if (atkPokemon > yourHighestHit)
                    {
                        yourHighestHit = atkPokemon;
                    }

                    Console.WriteLine($"Your {pokemonInfoPokemon.Name} uses {pokemonInfoPokemon.Skill} and hits {atkPokemon}");
                    if ((float)atkPokemon > (float)pokemonInfoPokemon.Atk * 0.70)
                    {
                        Console.WriteLine("CRITICAL STRIKE!");
                    }
                    enemy.HP -= atkPokemon;
                    Console.WriteLine($"{enemy.Name} is at {enemy.HP}HP");
                    if (enemy.HP < 1)
                    {

                        int expGain = random.Next(50);
                        Console.WriteLine($"You have won!\nYou have gained {expGain} Exp!");
                        pokemonInfoPokemon.Exp += expGain;
                        pokemonInfoPokemon.HP = initialHP;
                        winner = pokemonInfoPokemon.Name!;

                        break;
                    }
                    int enemyAtk = random.Next(enemy.Atk + 1);
                    if (enemyAtk > enemyHighestHit)
                    {
                        enemyHighestHit = enemyAtk;
                    }

                    Console.WriteLine($"{enemy.Name} uses {enemy.Skill} and hits with {enemyAtk}.");
                    if ((float)enemyAtk > (float)enemy.Atk * 0.70)
                    {
                        Console.WriteLine("CRITICAL STRIKE!");
                    }
                    pokemonInfoPokemon.HP -= enemyAtk;

                    Console.WriteLine($"-{enemyAtk} HP, Your {pokemonInfoPokemon.Name} is now at {pokemonInfoPokemon.HP} HP\n");
                    if (pokemonInfoPokemon.HP < 1)
                    {
                        winner = enemy.Name!;
                        Console.WriteLine($"You have lost :(. Your {pokemonInfoPokemon.Name}  died");
                        db.Remove(pokemonInfoPokemon);
                        break;
                    }


                }

                Console.WriteLine(String.Concat(Enumerable.Repeat("=", 30)));
                Console.WriteLine($"Statistics\n{pokemonInfoPokemon.Name} VS {enemy.Name}\nWinner: {winner}\nEnemy Highest Damage: {enemyHighestHit}\nYour Highest Damage: {yourHighestHit}\nRounds: {round}");
                Console.WriteLine(String.Concat(Enumerable.Repeat("=", 30)));
                Console.WriteLine();



                db.SaveChanges();





            }
        }



        public static void Gacha()
        {

            Random random = new Random();
            List<string> entities = GetTables.EntityTypes();

            List<string> subclasses = typeof(Pokemon).Assembly.GetTypes().Where(type => type.IsSubclassOf(typeof(Pokemon)) && entities.Contains(type.ToString().ToLower())).Select(x => x.ToString()).ToList();
            string selection = subclasses[random.Next(subclasses.Count)];
            Console.WriteLine($"You have found a {selection.Split(".")[1]}!");
            while (true)
            {
                Console.Write("Catch? (y/n): ");
                string query = Console.ReadLine().Trim().ToLower();
                if (query == "y")
                {

                    using (PokemonContext db = new PokemonContext())
                    {
                        Pokeball pokeballs = db.Pokeballs.Where(x => x.Id == 1).First();
                        if(pokeballs.Counter < 1) {
                            Console.WriteLine("You do not have enough pokeballs to catch pokemon! Please have more pokeballs.");
                            return;
                        }
                        pokeballs.Counter -= 1;

                        double luck = db.Pokeballs.Where(x => x.Id == 1).First().Chance * 20;

                        db.SaveChanges();
                        List<bool> luckList = new List<bool>();
                        for (int x = 0; x < 20; x++)
                        {
                            luckList.Add(x < luck - 1);
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            Console.WriteLine($"Shaking {i + 1}/3....");
                            Thread.Sleep(1500);
                            if (!luckList[random.Next(luckList.Count)])
                            {
                                Console.WriteLine("Your pokemon has broken out! Try again next time :(");
                                return;
                            }

                        }

                        int atk = random.Next(600);
                        int hp = random.Next(1100);
                        Console.WriteLine(
@"────────▄███████████▄────────
─────▄███▓▓▓▓▓▓▓▓▓▓▓███▄─────
────███▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓███────
───██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██───
──██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██──
─██▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓▓██─
██▓▓▓▓▓▓▓▓▓███████▓▓▓▓▓▓▓▓▓██
██▓▓▓▓▓▓▓▓██░░░░░██▓▓▓▓▓▓▓▓██
██▓▓▓▓▓▓▓██░░███░░██▓▓▓▓▓▓▓██
███████████░░███░░███████████
██░░░░░░░██░░███░░██░░░░░░░██
██░░░░░░░░██░░░░░██░░░░░░░░██
██░░░░░░░░░███████░░░░░░░░░██
─██░░░░░░░░░░░░░░░░░░░░░░░██─
──██░░░░░░░░░░░░░░░░░░░░░██──
───██░░░░░░░░░░░░░░░░░░░██───
────███░░░░░░░░░░░░░░░███────
─────▀███░░░░░░░░░░░███▀─────
────────▀███████████▀────────");
                        Console.WriteLine($"You caught a {selection.Split(".")[1]} with {atk} Atk and {hp} Hp!");


                        db.Add((Pokemon)Activator.CreateInstance(Type.GetType(selection), atk, 0, hp));
                        db.SaveChanges();

                    }

                    break;
                }
                else if (query == "n")
                {
                    Console.WriteLine($"You did not want to catch it so your {selection.Split(".")[1]} ran away! ");
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter if you want to catch!");
                }
            }


        }

        public static void GetPokeballs()
        {
            using (PokemonContext db = new PokemonContext())
            {
                int pokeballsNo;
                while (true)
                {
                    Console.Write("How many pokeballs do you want to get: ");
                    if (Int32.TryParse(Console.ReadLine().Trim(), out int noOfPokeballs))
                    {
                        pokeballsNo = noOfPokeballs;
                        break;
                    }
                    Console.WriteLine("Invalid number of Pokeballs!");
                }

                try
                {

                    var query = db.Balls.Where(x => x.Id == 1).First();
                    query.Counter += pokeballsNo;
                }
                catch (System.InvalidOperationException)
                {
                    db.Balls.Add(new Pokeball(pokeballsNo));
                }

                db.SaveChanges();
            }
        }
    }
}


