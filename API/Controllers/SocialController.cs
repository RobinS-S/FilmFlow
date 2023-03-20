using AutoMapper;
using FilmFlow.API.Auth;
using FilmFlow.API.Services;
using FilmFlow.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmFlow.API.Controllers
{
    [ApiController]
    [Route("api/Socials")]
    public partial class SocialController : ControllerBase
    {
        private readonly SocialService socialService;
        private readonly IMapper mapper;

        public SocialController(SocialService socialService, IMapper mapper)
        {
            this.socialService = socialService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SocialDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var socials = await socialService.GetAll();
            var socialDtos = mapper.Map<IEnumerable<SocialDto>>(socials);
            return Ok(socialDtos);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SocialDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var social = await socialService.GetById(id);

            if (social == null)
            {
                return NotFound();
            }

            var socialDto = mapper.Map<SocialDto>(social);
            return Ok(socialDto);
        }

		[HttpDelete("{id}")]
		//[Authorize(Roles = Roles.Admin)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(long id)
		{
			var social = await socialService.GetById(id);

			if (social == null)
			{
				return NotFound();
			}

			await socialService.Delete(id);

			return NoContent();
		}
	}
}
