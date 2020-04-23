﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Api.Resources;
using MusicApi.Api.Validators;
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

        /// <summary>
        /// Get Music by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MusicResource>> GetMusicByid(int id)
        {
            Music music = await _musicService.GetMusicById(id);
            MusicResource musicResource = _mapper.Map<Music, MusicResource>(music);

            return Ok(musicResource);
        }

        [HttpPost]
        public async Task<ActionResult<MusicResource>> UpdateMusic(int id, [FromBody] SaveMusicResource saveMusicResource)
        {
            SaveMusicResourceValidator validator = new SaveMusicResourceValidator();
            var validationResult = await validator.ValidateAsync(saveMusicResource);

            bool requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            Music musicToBeUpdated = await _musicService.GetMusicById(id);

            if (musicToBeUpdated == null)
                return NotFound();

            Music music = _mapper.Map<SaveMusicResource, Music>(saveMusicResource);
            await _musicService.UpdateMusic(musicToBeUpdated, music);

            var updateMusic = await _musicService.GetMusicById(id);
            var updateMusicResource = _mapper.Map<Music, MusicResource>(updateMusic);

            return Ok(updateMusicResource);
        }

    }


}