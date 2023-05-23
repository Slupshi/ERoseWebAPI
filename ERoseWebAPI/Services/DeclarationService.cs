using ERoseWebAPI.Data;
using ERoseWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ERoseWebAPI.Services
{
    public class DeclarationService : IDeclarationService
    {
        private readonly ERoseDbContext _context;
        private readonly IAccidentService _accidentService;
        private readonly IHeroService _heroService;

        public DeclarationService(ERoseDbContext context, IAccidentService accidentService, IHeroService heroService)
        {
            _context = context;
            _accidentService = accidentService;
            _heroService = heroService;
        }

        public async Task<Declaration?> GetDeclarationAsync(int id) => await _context.Declarations.FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<Declaration?>> GetDeclarationsAsync() => await _context.Declarations.Include(d => d.Accident).Select(d => new Declaration()
        {
            Id = d.Id,
            CityName = d.CityName,
            Description = d.Description,
            CreatedAt = d.CreatedAt,
            Latitude = d.Latitude,
            Longitude = d.Longitude,
            Accident = new Accident()
            {
                Id = d.Accident.Id,
                Name = d.Accident.Name,
                IconCode = d.Accident.IconCode,
                IconFontFamily = d.Accident.IconFontFamily,
                IconFontPackage = d.Accident.IconFontPackage,
            }
        }).ToListAsync();

        public async Task<Declaration?> PostDeclarationAsync(Declaration model)
        {
            if (model.Accident?.Id != null)
            {
                Accident? accident = await _accidentService.GetAccidentAsync(model.Accident.Id);
                if (accident != null)
                {
                    model.Accident = accident;
                }
            }

            model.Description = model.Description.Trim();

            model.CreatedAt = DateTime.Now;

            Declaration? newDeclaration = (await _context.Declarations.AddAsync(model)).Entity;
            await _context.SaveChangesAsync();
            return newDeclaration;
        }

        public async Task<Declaration?> PutDeclarationAsync(Declaration model)
        {
            if (model.Accident?.Id != null)
            {
                Accident? accident = await _accidentService.GetAccidentAsync(model.Accident.Id);
                if (accident != null)
                {
                    model.Accident = accident;
                }
            }

            Declaration? dbDeclaration = await GetDeclarationAsync(model.Id);

            model.Description = model.Description.Trim();

            model.CreatedAt = dbDeclaration?.CreatedAt;
            model.UpdatedAt = DateTime.Now;

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteDeclarationAsync(int id)
        {
            Declaration? dbDeclaration = await GetDeclarationAsync(id);
            if (dbDeclaration != null)
            {
                _context.Declarations.Remove(dbDeclaration);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeclarationExistsAsync(int id)
        {
            Declaration? Declaration = await _context.Declarations.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
            if (Declaration != null)
            {
                return true;
            }
            return false;
        }
    }
}
