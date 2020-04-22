using Microsoft.EntityFrameworkCore;
using MusicApi.Core.Models;
using MusicApi.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicApi.Data.Repositories
{
    public class MusicRepository : Repository<Music>, IMusicRepository
    {
        private MusicDbContext MusicDbContext
        {
            get { return Context as MusicDbContext; }
        }

        public MusicRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Music>> GetAllWithArtistAsync()
        {
            return await MusicDbContext.Musics
                 .Include(m => m.Artist)
                 .ToListAsync();
        }

        public async Task<IEnumerable<Music>> GetAllWithArtistByArtistIdAsync(int artistId)
        {
            return await MusicDbContext.Musics
                  .Include(m => m.Artist)
                  .Where(m => m.ArtistId == artistId)
                  .ToListAsync();
        }

        public async Task<Music> GetWithArtistByIdAsync(int id)
        {
            return await MusicDbContext.Musics
               .Include(m => m.Artist)
               .SingleOrDefaultAsync(m => m.Id == id);
        }
    }
}
