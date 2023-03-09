using AutoMapper;
using FilmFlow.API.Data.Entities;
using FilmFlow.API.Services;
using FilmFlow.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FilmFlow.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService reservationService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public ReservationController(ReservationService reservationService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.reservationService = reservationService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var user = User.Identity?.Name != null ? await userManager.FindByNameAsync(User.Identity.Name) : null;
            if (user == null)
            {
                return BadRequest();
            }

            var reservations = await reservationService.GetByUserId(user.Id);
            var reservationDtos = mapper.Map<IEnumerable<ReservationDto>>(reservations);
            return Ok(reservationDtos);
        }
    }
}
