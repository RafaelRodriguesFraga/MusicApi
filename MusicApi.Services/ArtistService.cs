using MusicApi.Core;
using MusicApi.Core.Models;
using MusicApi.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicApi.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IUnityOfWork _unityOfWork;
        public ArtistService(IUnityOfWork unityOfWork)
        {

        }

        public async Task<Artist> CreateArtist(Artist newArtist)
        {
            await _unityOfWork.Artists.AddAsync(newArtist);

            return newArtist;
        }

        public async Task DeleteArtist(Artist artist)
        {
            _unityOfWork.Artists.Remove(artist);
            await _unityOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Artist>> GetAllArtists()
        {
            return await _unityOfWork.Artists.GetAllAsync();
        }

        public async Task<Artist> GetArtistById(int id)
        {
            return await _unityOfWork.Artists.GetByIdAsync(id);
        }

        public async Task UpdateArtist(Artist artistToBeUpdated, Artist artist)
        {
            artistToBeUpdated.Name = artist.Name;
            await _unityOfWork.CommitAsync();
        }
    }
}
