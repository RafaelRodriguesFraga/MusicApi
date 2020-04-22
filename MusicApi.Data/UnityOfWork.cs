using MusicApi.Core;
using MusicApi.Core.Repositories;
using MusicApi.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace MusicApi.Data
{
    public class UnityOfWork : IUnityOfWork
    {
        private readonly MusicDbContext _context;
        private MusicRepository _musicRepository;
        private ArtistRepository _artistRepository;

        public UnityOfWork(MusicDbContext context)
        {
            this._context = context;
        }

        public IMusicRepository Musics => _musicRepository = _musicRepository ?? new MusicRepository(_context);

        public IArtistRepository Artists => _artistRepository = _artistRepository ?? new ArtistRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
