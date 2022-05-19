namespace PokemonGame
{
    public class Pokeball : Ball
    {
        public Pokeball()
        {

        }

        public Pokeball(int count)
        {
            Chance = 0.95;
            Name = "Pokeball";
            Counter = count;

        }
    }
}