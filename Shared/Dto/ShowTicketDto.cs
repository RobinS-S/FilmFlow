namespace FilmFlow.Shared.Dto
{
    public class ShowTicketDto
    {
        public long Id { get; set; }

        public string Code { get; set; } = null!;

        public string SoldBy { get; set; } = null!;
    }
}
