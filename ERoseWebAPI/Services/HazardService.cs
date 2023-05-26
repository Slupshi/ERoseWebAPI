using ERoseWebAPI.Data;
using ERoseWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ERoseWebAPI.Services
{
    public class HazardService : IHazardService
    {
        private readonly ERoseDbContext _context;
        private readonly IAccidentTypeService _accidentTypeService;

        public HazardService(ERoseDbContext context, IAccidentTypeService accidentTypeService)
        {
            _context = context;
            _accidentTypeService = accidentTypeService;
        }

        public async Task<Hazard?> GetHazardAsync(int id) => await _context.Hazards.FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<Hazard?>> GetHazardsAsync() => await _context.Hazards.Include(d => d.AccidentType).Select(d => new Hazard()
        {
            Id = d.Id,
            CityName = d.CityName,
            Description = d.Description,
            CreatedAt = d.CreatedAt,
            Latitude = d.Latitude,
            Longitude = d.Longitude,
            AccidentType = new AccidentType()
            {
                Id = d.AccidentType.Id,
                Name = d.AccidentType.Name,
                IconCode = d.AccidentType.IconCode,
                IconFontFamily = d.AccidentType.IconFontFamily,
                IconFontPackage = d.AccidentType.IconFontPackage,
            }
        }).ToListAsync();

        public async Task<Hazard?> PostHazardAsync(Hazard model)
        {
            if (model.AccidentType?.Id != null)
            {
                AccidentType? accident = await _accidentTypeService.GetAccidentTypeAsync(model.AccidentType.Id);
                if (accident != null)
                {
                    model.AccidentType = accident;
                }
            }

            model.Description = model.Description.Trim();

            model.CreatedAt = DateTime.Now;

            Hazard? newDeclaration = (await _context.Hazards.AddAsync(model)).Entity;
            await _context.SaveChangesAsync();
            return newDeclaration;
        }

        public async Task<Hazard?> PutHazardAsync(Hazard model)
        {
            if (model.AccidentType?.Id != null)
            {
                AccidentType? accident = await _accidentTypeService.GetAccidentTypeAsync(model.AccidentType.Id);
                if (accident != null)
                {
                    model.AccidentType = accident;
                }
            }

            Hazard? dbDeclaration = await GetHazardAsync(model.Id);

            model.Description = model.Description.Trim();

            model.CreatedAt = dbDeclaration?.CreatedAt;
            model.UpdatedAt = DateTime.Now;

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteHazardAsync(int id)
        {
            Hazard? dbDeclaration = await GetHazardAsync(id);
            if (dbDeclaration != null)
            {
                _context.Hazards.Remove(dbDeclaration);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> HazardExistsAsync(int id)
        {
            Hazard? Declaration = await _context.Hazards.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
            if (Declaration != null)
            {
                return true;
            }
            return false;
        }
    }
}
