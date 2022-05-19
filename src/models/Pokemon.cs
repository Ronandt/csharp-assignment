using System.Collections.Generic;

namespace PokemonGame {
    public abstract class Pokemon
    {
        public int PokemonId { get; set; }
        public string? Name { get; set; }
        public abstract string? Skill {get;set;}
        public int HP {get;set;}
        public int Exp {get;set;}
        public int Atk {get;set;}
        public int HasEvolved {get;set;}
        public int EvolutionLimit {get;set;} //amount of exp to evolve and try and upgrade the pokemon 

        public override string ToString() {
        return $"{String.Concat(Enumerable.Repeat("-", 30))}\nID: {PokemonId}\nName: {Name}\nHP: {HP}\nExp: {Exp}\nAtk: {Atk}\nSkill: {Skill}\n{String.Concat(Enumerable.Repeat("-", 30))}";
        }

      

        /*public string? EvolveTo {get;set;}*/
    }

    
}