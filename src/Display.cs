namespace PokemonGame {
    class Display {
        public static void MainMenu() {
            Console.WriteLine(@"                                  ,'\
    _.----.        ____         ,'  _\   ___    ___     ____
_,-'       `.     |    |  /`.   \,-'    |   \  /   |   |    \  |`.
\      __    \    '-.  | /   `.  ___    |    \/    |   '-.   \ |  |
 \.    \ \   |  __  |  |/    ,','_  `.  |          | __  |    \|  |
   \    \/   /,' _`.|      ,' / / / /   |          ,' _`.|     |  |
    \     ,-'/  /   \    ,'   | \/ / ,`.|         /  /   \  |     |
     \    \ |   \_/  |   `-.  \    `'  /|  |    ||   \_/  | |\    |
      \    \ \      /       `-.`.___,-' |  |\  /| \      /  | |   |
       \    \ `.__,'|  |`-._    `|      |__| \/ |  `.__,'|  | |   |
        \_.-'       |__|    `-._ |              '-.|     '-.| |   |
                                `'                            '-._|");
             Console.WriteLine($"{String.Concat(Enumerable.Repeat("*", 30))}\n Welcome to Pokemon Pocket App\n{String.Concat(Enumerable.Repeat("*", 30))}");
            List<string> options = new List<string> { "Add pokemon to my pocket", "List pokemon(s) in my Pocket", "Check if I can evolve pokemon", "Evolve pokemon", "Battle", "Pokemon Gacha", "Add Pokeballs"};
            for (int x = 0; x < options.Count; x++)
            {
                Console.WriteLine($"({x + 1}). {options[x]}");
            }
        }

        public static void ViewPokemons(List<string> subclasses) {
        


                    Console.WriteLine(String.Concat(Enumerable.Repeat("=", 30)));
                    Console.WriteLine("POKEMONS YOU CAN CHOOSE");
                    Console.WriteLine(String.Concat(Enumerable.Repeat("=", 30)));
                    foreach (string x in subclasses)
                     {
                        string subclass = x.Split(".")[1];
                        Console.WriteLine(Char.ToString(subclass[0]).ToUpper() + subclass.Substring(1));
                     
                    }
                    Console.WriteLine(String.Concat(Enumerable.Repeat("=", 30)));

           


        }
    }
}