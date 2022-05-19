namespace PokemonGame {
    class Enemy : Pokemon{

        Random random = new Random();
        public Enemy() {
            
            HP = random.Next(1000);
            Atk = random.Next(500);
            Name = new List<string> {"Charmander", "Pikachu", "Eevee", "Arceus", "Mewtwo", "Mew", "Kyogre"}[random.Next(7)];
            Skill = new List<string> {"Void Smash", "Solar Flare", "Lightning Arc", "Bomb", "Charm"}[random.Next(5)];

        }
        public override string? Skill {get;set;}
    }
}