using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Api.Resources;
using MusicApi.Api.Validators;
using MusicApi.Core.Models;
using MusicApi.Core.Services;

namespace MusicApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;

        public ArtistsController(IArtistService artistService, IMapper mapper)
        {
            _mapper = mapper;
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistResource>>> GetAllArtists()
        {
            var artists = await _artistService.GetAllArtists();
            var artistResources = _mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistResource>>(artists);

            return Ok(artistResources);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistResource>> GetArtistsById(int id)
        {
            var artist = await _artistService.GetArtistById(id);
            ArtistResource artistResource = _mapper.Map<Artist, ArtistResource>(artist);

            return Ok(artistResource);

        }

        [HttpPost]
        public async Task<ActionResult<ArtistResource>> CreateArtist([FromBody] SaveArtistResource saveArtistResource)
        {
            SaveArtistResourceValidator validator = new SaveArtistResourceValidator();
            var validationResult = await validator.ValidateAsync(saveArtistResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            Artist artistToBeCreated = _mapper.Map<SaveArtistResource, Artist>(saveArtistResource);
            Artist newArtist = await _artistService.CreateArtist(artistToBeCreated);
            Artist artist = await _artistService.GetArtistById(newArtist.Id);

            ArtistResource artistResource = _mapper.Map<Artist, ArtistResource>(artist);

            return Ok(artistResource);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ArtistResource>> UpdateArtist(int id, [FromBody] SaveArtistResource saveArtistResource)
        {
            SaveArtistResourceValidator validator = new SaveArtistResourceValidator();
            var validationResult = await validator.ValidateAsync(saveArtistResource);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var artist = _mapper.Map<SaveArtistResource, Artist>(saveArtistResource);

            Artist artistToBeUpdated = await _artistService.GetArtistById(id);

            await _artistService.UpdateArtist(artistToBeUpdated, artist);

            Artist updatedArtist = await _artistService.GetArtistById(id);

            ArtistResource updatedArtistResource = _mapper.Map<Artist, ArtistResource>(updatedArtist);

            return Ok(updatedArtistResource);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteArtist(int id)
        {
            Artist artist = await _artistService.GetArtistById(id);

            await _artistService.DeleteArtist(artist);

            return NoContent();
        }



    }
}