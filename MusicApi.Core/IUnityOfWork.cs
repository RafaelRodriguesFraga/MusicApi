using MusicApi.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace MusicApi.Core
{
    public interface IUnityOfWork : IDisposable
    {
        IMusicRepository Musics { get; }
        IArtistRepository Artists { get; }
        Task<int> CommitAsync();
    }
}