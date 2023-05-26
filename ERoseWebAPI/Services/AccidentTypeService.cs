using ERoseWebAPI.Data;
using ERoseWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ERoseWebAPI.Services
{
    public class AccidentTypeService : IAccidentTypeService
    {
        private readonly ERoseDbContext _context;

        public AccidentTypeService(ERoseDbContext context)
        {
            _context = context;
        }

        public async Task<AccidentType?> GetAccidentTypeAsync(int id) => await _context.AccidentTypes.FirstOrDefaultAsync(a => a.Id == id);
        public async Task<AccidentType?> GetAccidentTypeAsNoTrackingAsync(int id) => await _context.AccidentTypes.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<AccidentType?>> GetAccidentTypesAsync() => await _context.AccidentTypes.ToListAsync();

        public async Task<AccidentType?> PostAccidentTypeAsync(AccidentType model)
        {
            model.Name = model.Name.Trim();
            model.Description = model.Description.Trim();

            model.CreatedAt = DateTime.Now;

            AccidentType? newAccident = (await _context.AccidentTypes.AddAsync(model)).Entity;
            await _context.SaveChangesAsync();
            return newAccident;
        }

        public async Task<AccidentType?> PutAccidentTypeAsync(AccidentType model)
        {
            AccidentType? dbAccident = await GetAccidentTypeAsNoTrackingAsync(model.Id);

            model.Name = model.Name.Trim();
            model.Description = model.Description.Trim();

            model.CreatedAt = dbAccident?.CreatedAt;
            model.UpdatedAt = DateTime.Now;

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAccidentTypeAsync(int id)
        {
            AccidentType? dbAccident = await GetAccidentTypeAsync(id);
            if (dbAccident != null)
            {
                _context.AccidentTypes.Remove(dbAccident);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AccidentTypeExistsAsync(int id)
        {
            AccidentType? accident = await _context.AccidentTypes.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
            if (accident != null)
            {
                return true;
            }
            return false;
        }
    }
}
