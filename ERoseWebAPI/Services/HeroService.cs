using ERoseWebAPI.Data;
using ERoseWebAPI.Helpers;
using ERoseWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ERoseWebAPI.Services
{
    public class HeroService : IHeroService
    {
        private readonly ERoseDbContext _context;
        private readonly IAccidentTypeService _accidentTypeService;

        public HeroService(ERoseDbContext context, IAccidentTypeService accidentTypeService)
        {
            _context = context;
            _accidentTypeService = accidentTypeService;
        }


        // </inheritedoc>
        public async Task<Hero?> GetHeroAsync(int id) => (await _context.Heroes.AsNoTracking().Include(h => h.AccidentTypes).Select(h => new Hero()
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
            AccidentTypes = h.AccidentTypes.Select(a => new AccidentType()
            {
                Id = a.Id,
                Name = a.Name,
                IconCode = a.IconCode,
                IconFontFamily = a.IconFontFamily,
                IconFontPackage = a.IconFontPackage,
            }).ToList(),
        }).ToListAsync()).FirstOrDefault(h => h.Id == id);

        // </inheritedoc>
        public async Task<IEnumerable<Hero?>> GetHeroesAsync() => await _context.Heroes.Include(h => h.AccidentTypes).Select(h => new Hero()
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
            AccidentTypes = h.AccidentTypes.Select(a => new AccidentType()
            {
                Id = a.Id,
                Name = a.Name,
                IconCode = a.IconCode,
                IconFontFamily = a.IconFontFamily,
                IconFontPackage = a.IconFontPackage,
            }).ToList(),
        }).ToListAsync();

        // </inheritdoc>
        public async Task<IEnumerable<Hero?>> GetHeroesByAccidentTypeAsync(IEnumerable<AccidentType> accidents) => await _context.Heroes.Where(h => h.AccidentTypes != null && h.AccidentTypes.Intersect(accidents).Count() == accidents.Count()).ToListAsync();

        // </inheritdoc>
        public async Task<Hero?> GetHeroesByPhoneNumberAsync(string phoneNumber) => await _context.Heroes.FirstOrDefaultAsync(h => h.PhoneNumber == phoneNumber);

        // </inheritdoc>
        public async Task<Hero?> GetHeroesByEmailAsync(string email) => await _context.Heroes.FirstOrDefaultAsync(h => h.Email == email);

        // </inheritedoc>
        public async Task<Hero?> PostHeroAsync(Hero model)
        {
            if (model.AccidentTypes != null)
            {
                List<AccidentType> accidents = new List<AccidentType>();
                foreach (var accident in model.AccidentTypes)
                {
                    if (accident.Id != null)
                    {
                        AccidentType? dbAccident = await _accidentTypeService.GetAccidentTypeAsync(accident.Id);
                        if (dbAccident != null)
                        {
                            accidents.Add(dbAccident);
                        }
                    }
                }
                model.AccidentTypes = accidents;
            }

            if (model.FirstName != null)
            {
                model.FirstName = model.FirstName.Trim();
            }
            if (model.LastName != null)
            {
                model.LastName = model.LastName.Trim();
            }

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

            if (model.AccidentTypes != null)
            {
                List<AccidentType> accidents = new List<AccidentType>();
                foreach (var accident in model.AccidentTypes)
                {
                    if (accident.Id != null)
                    {
                        AccidentType? dbAccident = await _accidentTypeService.GetAccidentTypeAsync(accident.Id);
                        if (dbAccident != null)
                        {
                            accidents.Add(dbAccident);
                        }
                    }
                }
                model.AccidentTypes = accidents;
            }
            else
            {
                model.AccidentTypes = dbHero.AccidentTypes;
            }

            if (model.FirstName != null)
            {
                model.FirstName = model.FirstName.Trim();
            }
            if (model.LastName != null)
            {
                model.LastName = model.LastName.Trim();
            }

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
