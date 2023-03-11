using AutoMapper;
using FilmFlow.API.Services;
using FilmFlow.Shared.Dto;
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
    }
}
