using ERoseWebAPI.Data;
using ERoseWebAPI.Helpers;
using ERoseWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ERoseWebAPI.Services
{
    public class HeroService : IHeroService
    {
        private readonly ERoseDbContext _context;
        private readonly IAccidentService _accidentService;

        public HeroService(ERoseDbContext context, IAccidentService accidentService)
        {
            _context = context;
            _accidentService = accidentService;
        }


        // </inheritedoc>
        public async Task<Hero?> GetHeroAsync(int id) => await _context.Heroes.AsNoTracking()
                                                                             .Include(h => h.Accidents)
                                                                             .FirstOrDefaultAsync(h => h.Id == id);
        // </inheritedoc>
        public async Task<IEnumerable<Hero?>> GetHeroesAsync() => await _context.Heroes.Include(h => h.Accidents).Select(h => new Hero()
        {
            Id = h.Id,
            HeroName = h.HeroName,
            FirstName = h.FirstName,
            LastName = h.LastName,
            Email = h.Email,
            PhoneNumber = h.PhoneNumber,
            HeroScore = h.HeroScore,
            Latitude = h.Latitude,
            Longitude = h.Longitude,
            Accidents = h.Accidents.Select(a => new Accident()
            {
                Id = a.Id,
                Name = a.Name,
                IconCode = a.IconCode,
                IconFontFamily = a.IconFontFamily,
                IconFontPackage = a.IconFontPackage,
            }).ToList(),
        }).ToListAsync();

        // </inheritdoc>
        public async Task<IEnumerable<Hero?>> GetHeroesByAccidentTypeAsync(IEnumerable<Accident> accidents) => await _context.Heroes.Where(h => h.Accidents != null && h.Accidents.Intersect(accidents).Count() == accidents.Count()).ToListAsync();

        // </inheritdoc>
        public async Task<Hero?> GetHeroesByPhoneNumberAsync(string phoneNumber) => await _context.Heroes.FirstOrDefaultAsync(h => h.PhoneNumber == phoneNumber);

        // </inheritedoc>
        public async Task<Hero?> PostHeroAsync(Hero model)
        {
            if (model.Accidents != null)
            {
                List<Accident> accidents = new List<Accident>();
                foreach (var accident in model.Accidents)
                {
                    if (accident.Id != null)
                    {
                        Accident? dbAccident = await _accidentService.GetAccidentAsync(accident.Id);
                        if (dbAccident != null)
                        {
                            accidents.Add(dbAccident);
                        }
                    }
                }
                model.Accidents = accidents;
            }

            model.FirstName = model.FirstName.Trim();
            model.LastName = model.LastName.Trim();
            model.Email = model.Email.Trim();
            model.PhoneNumber = model.PhoneNumber.Trim();
            model.Password = PasswordHelper.HashPassword(model.Password.Trim());

            model.CreatedAt = DateTime.Now;

            Hero? newHero = (await _context.Heroes.AddAsync(model)).Entity;
            await _context.SaveChangesAsync();
            return newHero;
        }

        // </inheritedoc>
        public async Task<Hero?> PutHeroAsync(Hero model)
        {
            Hero? dbHero = await GetHeroAsync(model.Id);

            if (model.Accidents != null)
            {
                List<Accident> accidents = new List<Accident>();
                foreach (var accident in model.Accidents)
                {
                    if (accident.Id != null)
                    {
                        Accident? dbAccident = await _accidentService.GetAccidentAsync(accident.Id);
                        if (dbAccident != null)
                        {
                            accidents.Add(dbAccident);
                        }
                    }
                }
                model.Accidents = accidents;
            }
            else
            {
                model.Accidents = dbHero.Accidents;
            }

            model.FirstName = model.FirstName.Trim();
            model.LastName = model.LastName.Trim();
            model.Email = model.Email.Trim();
            model.PhoneNumber = model.PhoneNumber.Trim();

            model.CreatedAt = dbHero?.CreatedAt;
            model.Password = model.Password != null ? PasswordHelper.HashPassword(model.Password.Trim()) : dbHero?.Password;
            model.UpdatedAt = DateTime.Now;

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        // </inheritedoc>
        public async Task<bool> DeleteHeroAsync(int id)
        {
            Hero? dbHero = await GetHeroAsync(id);
            if (dbHero != null)
            {
                _context.Heroes.Remove(dbHero);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // </inheritedoc>
        public async Task<bool> HeroExistsAsync(int id)
        {
            Hero? hero = await _context.Heroes.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
            if (hero != null)
            {
                return true;
            }
            return false;
        }
    }
}
