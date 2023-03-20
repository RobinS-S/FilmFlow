using FilmFlow.Shared.Enums;

namespace FilmFlow.Shared
{
    public class Tariffs
    {
        public static readonly Dictionary<TariffType, decimal> Prices = new()
        {
            { TariffType.Normal, 8.50m },
            { TariffType.Children, 7m },
            { TariffType.Students, 7m },
            { TariffType.Seniors, 7m },
            { TariffType.Secret, 6m }
        };

        public static readonly decimal ThreeDimensionalPremiumPrice = 2.50m;

        public static decimal GetTariffPrice(TariffType type, bool isThreeDimensional = false)
        {
            return isThreeDimensional ? Prices[type] + ThreeDimensionalPremiumPrice : Prices[type];
        }
    }
}
