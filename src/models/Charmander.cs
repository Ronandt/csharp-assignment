namespace PokemonGame
{



    public class Charmander : Pokemon
    {

        public Charmander()
        {

        }
        public Charmander(int hp, int exp, int atk)
        {
            HP = hp;
            Exp = exp;
            Skill = "Solar Flare";
            Name = this.GetType().Name;
            Atk = atk;
        }
           public override string? Skill { get;set; }

    }

}