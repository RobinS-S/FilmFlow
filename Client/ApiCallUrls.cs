namespace FilmFlow.Client
{
    public class ApiCallUrls
    {
        private const string ApiPrefix = "/api";

        public static string Movies() => $"{ApiPrefix}/movies";
        public static string MovieById(long id) => $"{ApiPrefix}/movies/{id}";
        public static string MovieShowsById(long id) => $"{ApiPrefix}/movies/{id}/shows";

        public static string CinemaHalls() => $"{ApiPrefix}/cinemahalls";
        public static string CinemaHallById(long id) => $"{ApiPrefix}/cinemahalls/{id}";

        public static string CinemaShows() => $"{ApiPrefix}/cinemashows";
        public static string CinemaShowById(long id) => $"{ApiPrefix}/cinemashows/{id}";
        public static string ReservationForCinemaShow(long id) => $"{ApiPrefix}/cinemashows/{id}/reservation";
        public static string ReservedSeatsForShow(long id) => $"{ApiPrefix}/cinemashows/{id}/reserved";

        public static string PayReservationById(long id) => $"{ApiPrefix}/reservations/{id}/pay";
        public static string ReservationById(long id) => $"{ApiPrefix}/reservations/{id}";
        public static string Reservations() => $"{ApiPrefix}/reservations";

		public static string TicketByCode(string code) => $"{ApiPrefix}/tickets/byCode?code={code}";
		public static string TicketQrByCode(long reservationId, string code) => $"{ApiPrefix}/tickets/qrByCode?reservationId={reservationId}&code={code}";
	}
}
