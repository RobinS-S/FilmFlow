using AutoMapper;
using FilmFlow.API.Auth;
using FilmFlow.API.Data.Models;
using FilmFlow.API.Dto;
using FilmFlow.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FilmFlow.API.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public partial class MovieController : ControllerBase
    {
        private readonly MovieService movieService;
        private readonly MovieReviewService movieReviewService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper _mapper;

        public MovieController(MovieService movieService, MovieReviewService movieReviewService, IMapper mapper)
        {
            this.movieService = movieService;
            this.movieReviewService = movieReviewService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var movies = await movieService.GetAll();
            var movieDtos = _mapper.Map<IEnumerable<MovieDto>>(movies);
            return Ok(movieDtos);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(long id)
        {
            var movie = await movieService.GetById(id);

            if (movie == null)
            {
                return NotFound();
            }

            var movieDto = _mapper.Map<MovieDto>(movie);
            return Ok(movieDto);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] MovieDto movieDto)
        {
            var movie = _mapper.Map<Movie>(movieDto);
            await movieService.Create(movie);

            movieDto = _mapper.Map<MovieDto>(movie);
            return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movieDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(long id, [FromBody] MovieDto movieDto)
        {
            if (id != movieDto.Id)
            {
                return BadRequest();
            }

            var movie = await movieService.GetById(id);

            if (movie == null)
            {
                return NotFound();
            }

            _mapper.Map(movieDto, movie);
            await movieService.Update(movie);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id)
        {
            var movie = await movieService.GetById(id);

            if (movie == null)
            {
                return NotFound();
            }

            await movieService.Delete(id);

            return NoContent();
        }
    }

    // Movie reviews
    public partial class MovieController
    {
        [HttpGet("{movieId}/reviews")]
        [ProducesResponseType(typeof(IEnumerable<MovieReviewDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllReviews(long movieId)
        {
            var reviews = await movieReviewService.GetAllByMovieId(movieId);
            var reviewDtos = _mapper.Map<IEnumerable<MovieReviewDto>>(reviews);
            return Ok(reviewDtos);
        }

        [HttpGet("{movieId}/reviews/{id}")]
        [ProducesResponseType(typeof(MovieReviewDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReviewById(long movieId, long id)
        {
            var review = await movieReviewService.GetByIdForMovie(movieId, id);

            if (review == null)
            {
                return NotFound();
            }

            var reviewDto = _mapper.Map<MovieReviewDto>(review);
            return Ok(reviewDto);
        }

        [HttpPost("{movieId}/reviews")]
        [ProducesResponseType(typeof(MovieReviewDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<IActionResult> CreateReview(long movieId, [FromBody] MovieReviewDto reviewDto)
        {
            var movie = await movieService.GetById(movieId);

            if (movie == null)
            {
                return BadRequest();
            }

            var review = _mapper.Map<MovieReview>(reviewDto);
            review.MovieId = movieId;

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return BadRequest();
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }

            await movieReviewService.Create(review, user);

            reviewDto = _mapper.Map<MovieReviewDto>(review);
            return CreatedAtAction(nameof(GetReviewById), new { movieId, id = review.Id }, reviewDto);
        }

        [HttpPut("{movieId}/reviews/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> UpdateReview(long movieId, long id, [FromBody] MovieReviewDto reviewDto)
        {
            if (id != reviewDto.Id)
            {
                return BadRequest();
            }

            var review = await movieReviewService.GetByIdForMovie(movieId, id);

            if (review == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return BadRequest();
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }

            if (review.UserId != user.Id && !await userManager.IsInRoleAsync(user, Roles.Admin))
            {
                return Forbid();
            }

            _mapper.Map(reviewDto, review);
            await movieReviewService.Update(review);

            return NoContent();
        }

        [HttpDelete("{movieId}/reviews/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> DeleteReview(long movieId, long id)
        {
            var review = await movieReviewService.GetByIdForMovie(movieId, id);

            if (review == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return BadRequest();
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest();
            }

            if (review.UserId != user.Id && !await userManager.IsInRoleAsync(user, Roles.Admin))
            {
                return Forbid();
            }

            await movieReviewService.Delete(id);

            return NoContent();
        }
    }
}
