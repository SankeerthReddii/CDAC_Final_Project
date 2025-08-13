using Microsoft.EntityFrameworkCore;
using Metro_Ticket_Project.Models.Entities;
using Metro_Ticket_Project.Infrastructure.Data;
using Metro_Ticket_Project.Services.Interfaces;

namespace Metro_Ticket_Project.Services.Repositories
{
    public class StationRepository : BaseRepository<Station>, IStationRepository
    {
        public StationRepository(MetroDbContext context) : base(context) { }

        public async Task<int> GetStationIdAsync(string name)
        {
            var station = await _dbSet
                .FirstOrDefaultAsync(s => s.Name == name);
            return station?.Id ?? 0;
        }

        public async Task<Station?> GetStationByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}