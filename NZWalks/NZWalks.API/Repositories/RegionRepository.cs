using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext _nZWalksDBContext;
        public RegionRepository(NZWalksDBContext nZWalksDBContext)
        {
            _nZWalksDBContext = nZWalksDBContext;
        }

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = new Guid();
            await _nZWalksDBContext.AddAsync(region);
            await _nZWalksDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await _nZWalksDBContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await _nZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await GetAsync(id);
            if(region == null)
            {
                return null;
            }

            _nZWalksDBContext.Regions.Remove(region);
            await _nZWalksDBContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await _nZWalksDBContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(existingRegion == null)
            {
                return null;
            }
            existingRegion.Area = region.Area;
            existingRegion.Code = region.Code;
            existingRegion.Lat = region.Lat;
            existingRegion.Long = region.Long;
            existingRegion.Population = region.Population;
            await _nZWalksDBContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
