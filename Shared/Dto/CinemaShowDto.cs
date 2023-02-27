using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmFlow.Shared.Dto
{
    public class CinemaShowDto
    {
        public long Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public long MovieId { get; set; }

        public long CinemaHallId { get; set; }
    }
}
