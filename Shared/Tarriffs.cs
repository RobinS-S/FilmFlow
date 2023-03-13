using FilmFlow.Shared.Enums;

namespace FilmFlow.Shared
{
    public class Tarriffs
    {
        public static readonly Dictionary<TarriffType, decimal> Prices = new Dictionary<TarriffType, decimal>()
        {
            { TarriffType.NORMAL, 8.50m },
            { TarriffType.CHILDREN, 7m },
            { TarriffType.STUDENTS, 7m },
            { TarriffType.SENIORS, 7m },
            { TarriffType.SECRET, 6m }
        };

        public static readonly decimal THREEDIMENSIONAL_PREMIUM = 2.50m;

        public static decimal GetTarriffPrice(TarriffType type, bool isThreeDimensional = false)
        {
            return isThreeDimensional ? Prices[type] + THREEDIMENSIONAL_PREMIUM : Prices[type];
        }
    }
}
