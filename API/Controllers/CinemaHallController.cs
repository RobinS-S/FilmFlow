using AutoMapper;
using FilmFlow.API.Auth;
using FilmFlow.Domain.Entities;
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
        private readonly CinemaHallService _cinemaHallService;
        private readonly IMapper _mapper;

        public CinemaHallController(CinemaHallService cinemaHallService, IMapper mapper)
        {
            this._cinemaHallService = cinemaHallService;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CinemaHallDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var cinemaHalls = await _cinemaHallService.GetAll();
            var cinemaHallDtos = _mapper.Map<IEnumerable<CinemaHallDto>>(cinemaHalls);
            return Ok(cinemaHallDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CinemaHallDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var cinemaHall = await _cinemaHallService.GetById(id);

            if (cinemaHall == null)
            {
                return NotFound();
            }

            var cinemaHallDto = _mapper.Map<CinemaHallDto>(cinemaHall);
            return Ok(cinemaHallDto);
        }

        [HttpPost]
        [Authorize(Roles = Roles.AdminRoleName)]
        [ProducesResponseType(typeof(CinemaHallDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CinemaHallDto cinemaHallDto)
        {
            var cinemaHall = _mapper.Map<CinemaHall>(cinemaHallDto);
            await _cinemaHallService.Create(cinemaHall);

            cinemaHallDto = _mapper.Map<CinemaHallDto>(cinemaHall);
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

            var cinemaHall = await _cinemaHallService.GetById(id);

            if (cinemaHall == null)
            {
                return NotFound();
            }

            _mapper.Map(cinemaHallDto, cinemaHall);
            await _cinemaHallService.Update(cinemaHall);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.AdminRoleName)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _cinemaHallService.Delete(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }

}
