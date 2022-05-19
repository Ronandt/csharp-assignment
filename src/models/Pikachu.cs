namespace PokemonGame {
      public class Pikachu : Pokemon
    {
      public Pikachu() {}
      public Pikachu(int hp, int exp, int atk) {
          HP = hp;
          Exp = exp;
          Skill = "Lightning Bolt";
          Name = this.GetType().Name;
          Atk = atk;
      }

        public override string? Skill { get;set; }

    }

    



}