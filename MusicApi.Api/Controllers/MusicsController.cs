using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Api.Resources;
using MusicApi.Core.Models;
using MusicApi.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicApi.Api.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class MusicsController : ControllerBase
    {

        private readonly IMusicService _musicService;
        private readonly IMapper _mapper;

        public MusicsController(IMusicService musicService, IMapper mapper)
        {
            this._musicService = musicService;
            this._mapper = mapper;
        }
        /// <summary>
        /// Get All Musics
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Music>>> GetAllMusics()
        {
            var musics = await _musicService.GetAllWithArtist();
            var musicResources = _mapper.Map<IEnumerable<Music>, IEnumerable<MusicResource>>(musics);
            return Ok(musicResources);
        }
    }
}