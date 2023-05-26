using ERoseWebAPI.Models;

namespace ERoseWebAPI.Data.Seeders
{
    public class HeroSeeder
    {
        private static readonly List<Hero> _defaultHeroes = new()
        {
            new Hero()
            {
                HeroName = "Superman",
                HeroScore = 10000,
                FirstName = "Clark",
                LastName = "Kent",
                Password = "toto",
                Email = "super.man@gmail.com",
                PhoneNumber = "1234567890",
                Longitude = 50,
                Latitude = 50,
                CreatedAt = DateTime.Now,
            },
            new Hero()
            {
                HeroName = "Batman",
                HeroScore = 5000,
                FirstName = "Bruce",
                LastName = "Wayne",
                Password = "toto",
                Email = "bruce.wayne@gmail.com",
                PhoneNumber = "0123456789",
                Longitude = 20,
                Latitude = 40,
                CreatedAt = DateTime.Now,
            },
        };

        private static readonly Random _random = new Random();

        public static void Seed(ERoseDbContext context)
        {
            foreach (var hero in _defaultHeroes)
            {
                if (!context.Heroes.Any(h => h.HeroName == hero.HeroName))
                {
                    var dbAccidents = context.AccidentTypes.OrderBy(a => a.Id).ToList();
                    var accidents = new List<AccidentType>
                    {
                        dbAccidents.ElementAt(_random.Next(dbAccidents.First().Id, dbAccidents.Last().Id)),
                        dbAccidents.ElementAt(_random.Next(dbAccidents.First().Id, dbAccidents.Last().Id)),
                        dbAccidents.ElementAt(_random.Next(dbAccidents.First().Id, dbAccidents.Last().Id)),
                    };
                    hero.AccidentTypes = accidents;

                    context.Heroes.Add(hero);
                    context.SaveChanges();
                }
            }
        }
    }
}
