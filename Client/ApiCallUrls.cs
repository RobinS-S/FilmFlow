namespace FilmFlow.Client
{
    public class ApiCallUrls
    {
        private static string apiPrefix = "/api";

        public static string Movies() => $"{apiPrefix}/movies";
        public static string MovieById(long id) => $"{apiPrefix}/movies/{id}";
        public static string MovieShowsById(long id) => $"{apiPrefix}/movies/{id}/shows";

        public static string CinemaHalls() => $"{apiPrefix}/cinemahalls";
        public static string CinemaHallById(long id) => $"{apiPrefix}/cinemahalls/{id}";

        public static string CinemaShows() => $"{apiPrefix}/cinemashows";
        public static string CinemaShowById(long id) => $"{apiPrefix}/cinemashows/{id}";
        public static string ReservationForCinemaShow(long id) => $"{apiPrefix}/cinemashows/{id}/reservation";
        public static string ReservedSeatsForShow(long id) => $"{apiPrefix}/cinemashows/{id}/reserved";
    }
}
