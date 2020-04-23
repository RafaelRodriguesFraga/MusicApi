using MusicApi.Core;
using MusicApi.Core.Models;
using MusicApi.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MusicApi.Services
{
    public class MusicService : IMusicService
    {
        private readonly IUnityOfWork _unityOfWork;
        public MusicService(IUnityOfWork unityOfWork)
        {
            this._unityOfWork = unityOfWork;
        }

        public async Task<Music> CreateMusic(Music newMusic)
        {
            await _unityOfWork.Musics.AddAsync(newMusic);
            await _unityOfWork.CommitAsync();
            return newMusic;
        }

        public async Task DeleteMusic(Music music)
        {
            _unityOfWork.Musics.Remove(music);
            await _unityOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Music>> GetAllWithArtist()
        {
            return await _unityOfWork.Musics.GetAllWithArtistAsync();
        }

        public async Task<Music> GetMusicById(int id)
        {
            return await _unityOfWork.Musics.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Music>> GetMusicsByArtistId(int artistId)
        {
            return await _unityOfWork.Musics.GetAllWithArtistByArtistIdAsync(artistId);
        }

        public async Task UpdateMusic(Music musicToBeUpdated, Music music)
        {
            musicToBeUpdated.Name = music.Name;
            musicToBeUpdated.ArtistId = music.ArtistId;

            await _unityOfWork.CommitAsync();
        }
    }
}
