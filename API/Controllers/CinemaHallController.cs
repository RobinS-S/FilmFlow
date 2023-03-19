using AutoMapper;
using FilmFlow.API.Auth;
using FilmFlow.API.Data.Entities;
using FilmFlow.API.Services;
using FilmFlow.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FilmFlow.API.Controllers
{
    [ApiController]
    [Route("api/cinemahalls")]
    public class CinemaHallController : ControllerBase
    {
        private readonly CinemaHallService cinemaHallService;
        private readonly IMapper mapper;

        public CinemaHallController(CinemaHallService cinemaHallService, IMapper mapper)
        {
            this.cinemaHallService = cinemaHallService;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CinemaHallDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var cinemaHalls = await cinemaHallService.GetAll();
            var cinemaHallDtos = mapper.Map<IEnumerable<CinemaHallDto>>(cinemaHalls);
            return Ok(cinemaHallDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CinemaHallDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var cinemaHall = await cinemaHallService.GetById(id);

            if (cinemaHall == null)
            {
                return NotFound();
            }

            var cinemaHallDto = mapper.Map<CinemaHallDto>(cinemaHall);
            return Ok(cinemaHallDto);
        }

        [HttpPost]
        [Authorize(Roles = Roles.AdminRoleName)]
        [ProducesResponseType(typeof(CinemaHallDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CinemaHallDto cinemaHallDto)
        {
            var cinemaHall = mapper.Map<CinemaHall>(cinemaHallDto);
            await cinemaHallService.Create(cinemaHall);

            cinemaHallDto = mapper.Map<CinemaHallDto>(cinemaHall);
            return CreatedAtAction(nameof(GetById), new { id = cinemaHall.Id }, cinemaHallDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.AdminRoleName)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(long id, [FromBody] CinemaHallDto cinemaHallDto)
        {
            if (id != cinemaHallDto.Id)
            {
                return BadRequest();
            }

            var cinemaHall = await cinemaHallService.GetById(id);

            if (cinemaHall == null)
            {
                return NotFound();
            }

            mapper.Map(cinemaHallDto, cinemaHall);
            await cinemaHallService.Update(cinemaHall);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.AdminRoleName)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await cinemaHallService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
