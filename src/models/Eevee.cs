namespace PokemonGame { 
      public class Eevee : Pokemon
    {

      public Eevee() {

      }
         public Eevee(int hp, int exp, int atk) {
          HP = hp;
          Exp = exp;
          Skill = "Run Away";
          Name = this.GetType().Name;
          Atk = atk;
      }
        public override string? Skill { get;set; }

    }

}