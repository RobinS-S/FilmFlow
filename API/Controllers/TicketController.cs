using AutoMapper;
using FilmFlow.API.Misc;
using FilmFlow.API.Services;
using FilmFlow.Shared.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FilmFlow.API.Controllers
{
    [ApiController]
    [Route("api/tickets")]
    public class TicketController : ControllerBase
    {
        private readonly ShowTicketService ticketService;
        private readonly IMapper mapper;

        public TicketController(ShowTicketService ticketService, IMapper mapper)
        {
            this.ticketService = ticketService;
            this.mapper = mapper;
        }

        [HttpGet("byCode")]
        [ProducesResponseType(typeof(ShowTicketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByCode([FromQuery] string code)
        {
            var ticket = await ticketService.GetByCode(code);
            if(ticket == null)
            {
                return NotFound();
            }

            var ticketDto = mapper.Map<ShowTicketDto>(ticket);
            return Ok(ticketDto);
		}

		[HttpGet("qrByCode")]
		[ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetQrByCode([FromQuery] string code)
		{
			var ticket = await ticketService.GetByCode(code);
			if (ticket == null)
			{
				return NotFound();
			}

			byte[] imageData = QrCodeEncoding.GenerateQrCodeAsPngFromText(ticket.Code);
			return File(imageData, "image/png");
		}
	}
}
