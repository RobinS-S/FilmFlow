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
        private readonly SocialService _socialService;
        private readonly IMapper _mapper;

        public SocialController(SocialService socialService, IMapper mapper)
        {
            _socialService = socialService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SocialDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var socials = await _socialService.GetAll();
            var socialDtos = _mapper.Map<IEnumerable<SocialDto>>(socials);
            return Ok(socialDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SocialDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var social = await _socialService.GetById(id);

            if (social == null)
            {
                return NotFound();
            }

            var socialDto = _mapper.Map<SocialDto>(social);
            return Ok(socialDto);
        }

		[HttpDelete("{id}")]
		[Authorize(Roles = Roles.AdminRoleName)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(long id)
		{
			var social = await _socialService.GetById(id);

			if (social == null)
			{
				return NotFound();
			}

			await _socialService.Delete(id);

			return NoContent();
		}
	}
}
