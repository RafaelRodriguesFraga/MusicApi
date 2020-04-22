using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicApi.Core.Models;
using MusicApi.Core.Repositories;

namespace MusicApi.Data.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        private MusicDbContext MusicDbContext
        {
            get { return Context as MusicDbContext; }
        }

        public ArtistRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Artist>> GetAllWithMusicsAsync()
        {
            return await MusicDbContext.Artists
                .Include(a => a.Musics)
                .ToListAsync();
        }

        public Task<Artist> GetWithMusicsByIdAsync(int id)
        {
            return MusicDbContext.Artists
                  .Include(a => a.Musics)
                  .SingleOrDefaultAsync(a => a.Id == id);
        }
    }    
}
