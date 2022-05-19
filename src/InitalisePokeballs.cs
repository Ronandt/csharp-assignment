namespace PokemonGame {
    public class Initalise {
        public static void InitalisePokeballs() {
              using (PokemonContext db = new PokemonContext())
            {
                try
                {

                    db.Balls.Where(x => x.Id == 1).First();

                }
                catch (System.InvalidOperationException)
                {
                    db.Balls.Add(new Pokeball(0));
                    db.SaveChanges();
                }
            }
        }
    }
}