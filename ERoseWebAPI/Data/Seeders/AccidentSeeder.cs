using ERoseWebAPI.Models;

namespace ERoseWebAPI.Data.Seeders
{
    public class AccidentSeeder
    {
        private static readonly List<Accident> _defaultAccidents = new()
        {
            new Accident()
            {
                Name = "Incendie",
                Description = "Le feu ça brûle.\nSi vous voyez une habitation, un bâtiment public ou toutes poubelles entrain de brûler, signalez le au plus vite !",
                IconCode = "0xf06f2",
                IconFontFamily = "MaterialIcons",
                CreatedAt = DateTime.Now,
            },
            new Accident()
            {
                Name = "Accident routier",
                Description = "Un carambolage ? Un débris sur la route ou tout simplement une voiture emmanchée dans un platane ?\nAlors vous êtes dans la bonne catégorie",
                IconCode = "0xf5e1",
                IconFontFamily = "FontAwesomeSolid",
                IconFontPackage = "font_awesome_flutter",
                CreatedAt = DateTime.Now,
            },
            new Accident()
            {
                Name = "Accident fluviale",
                Description = "Un bateau est censé être sur l'eau, ni sur terre ni sous l'eau",
                IconCode = "0xf07cf",
                IconFontFamily = "MaterialIcons",
                CreatedAt = DateTime.Now,
            },
            new Accident()
            {
                Name = "Accident aérien",
                Description = "Un avion au sol qui n'est pas sur ces roues, ou qui est éventré",
                IconCode = "0xe556",
                IconFontFamily = "FontAwesomeSolid",
                IconFontPackage = "font_awesome_flutter",
                CreatedAt = DateTime.Now,
            },
            new Accident()
            {
                Name = "Eboulement",
                Description = "Si vous constatez des rochers sur la routes ou bien sur votre maison (celles de vos voisins comprises)",
                IconCode = "0xf06f6",
                IconFontFamily = "MaterialIcons",
                CreatedAt = DateTime.Now,
            },
            new Accident()
            {
                Name = "Invasion de serpent",
                Description = "Les serpents n'ont rien à faire en liberté en plein centre-ville !",
                IconCode = "0xe579",
                IconFontFamily = "FontAwesomeSolid",
                IconFontPackage = "font_awesome_flutter",
                CreatedAt = DateTime.Now,
            },
            new Accident()
            {
                Name = "Fuite de gaz",
                Description = "Les flatulences de vos camarades ne sont pas inclues !",
                IconCode = "0xe4e9",
                IconFontFamily = "FontAwesomeSolid",
                IconFontPackage = "font_awesome_flutter",
                CreatedAt = DateTime.Now,
            },
            new Accident()
            {
                Name = "Manifestation",
                Description = "Si vous voyez des pavés ou des molotovs voler",
                IconCode = "0xe54b",
                IconFontFamily = "FontAwesomeSolid",
                IconFontPackage = "font_awesome_flutter",
                CreatedAt = DateTime.Now,
            },
            new Accident()
            {
                Name = "Braquage",
                Description = "En général cet incident est caractérisé par des personnes cagoulées entrain de voler d'autres personnes.\nCependant parfois elles peuvent aussi se revêtir d'un costume cravate.",
                IconCode = "0xf81d",
                IconFontFamily = "FontAwesomeSolid",
                IconFontPackage = "font_awesome_flutter",
                CreatedAt = DateTime.Now,
            },
            new Accident()
            {
                Name = "Evasion de prisonnier",
                Description = "Une personne habillée en tenue de prisonnier (hors halloween)",
                IconCode = "0xf70c",
                IconFontFamily = "FontAwesomeSolid",
                IconFontPackage = "font_awesome_flutter",
                CreatedAt = DateTime.Now,
            },
        };

        public static void Seed(ERoseDbContext context)
        {
            foreach (var accident in _defaultAccidents)
            {
                if (!context.Accidents.Any(a => a.Name == accident.Name))
                {
                    context.Add(accident);
                    context.SaveChanges();
                }
            }
        }
    }
}
