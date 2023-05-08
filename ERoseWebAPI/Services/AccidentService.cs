using ERoseWebAPI.Data;
using ERoseWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ERoseWebAPI.Services
{
    public class AccidentService : IAccidentService
    {
        private readonly ERoseDbContext _context;

        public AccidentService(ERoseDbContext context)
        {
            _context = context;
        }

        public async Task<Accident?> GetAccidentAsync(int id) => await _context.Accidents.FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<Accident?>> GetAccidentsAsync() => await _context.Accidents.ToListAsync();

        public async Task<Accident?> PostAccidentAsync(Accident model)
        {
            model.Name = model.Name.Trim();
            model.Description = model.Description.Trim();

            model.CreatedAt = DateTime.Now;

            Accident? newAccident = (await _context.Accidents.AddAsync(model)).Entity;
            await _context.SaveChangesAsync();
            return newAccident;
        }

        public async Task<Accident?> PutAccidentAsync(Accident model)
        {
            Accident? dbAccident = await GetAccidentAsync(model.Id);

            model.Name = model.Name.Trim();
            model.Description = model.Description.Trim();

            model.CreatedAt = dbAccident?.CreatedAt;
            model.UpdatedAt = DateTime.Now;

            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<bool> DeleteAccidentAsync(int id)
        {
            Accident? dbAccident = await GetAccidentAsync(id);
            if (dbAccident != null)
            {
                _context.Accidents.Remove(dbAccident);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AccidentExistsAsync(int id)
        {
            Accident? accident = await _context.Accidents.AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
            if (accident != null)
            {
                return true;
            }
            return false;
        }
    }
}
