using FilmFlow.Shared;
using FilmFlow.Shared.Enums;

namespace FilmFlow.Tests
{
	public class TariffTypeTest
	{
		[Fact]
		public void TariffTypeTest3D()
		{
			var defaultPriceThreeD = Tariffs.GetTariffPrice(TariffType.Normal, true); // price for normal ticket + 3d should be 11
			var childrenPriceThreeD = Tariffs.GetTariffPrice(TariffType.Children, true); // price for children ticket + 3d should be 9.50
			var studentPriceThreeD = Tariffs.GetTariffPrice(TariffType.Students, true); // price for students ticket + 3d should be 9.50
			var seniorPriceThreeD = Tariffs.GetTariffPrice(TariffType.Seniors, true); // price for children ticket + 3d should be 9.50
			var secretPriceThreeD = Tariffs.GetTariffPrice(TariffType.Secret, true); // price for children ticket + 3d should be 8.50

			Assert.Equal(11m, defaultPriceThreeD);
			Assert.Equal(9.5m, childrenPriceThreeD);
			Assert.Equal(9.5m, studentPriceThreeD);
			Assert.Equal(9.5m, seniorPriceThreeD);
			Assert.Equal(8.5m, secretPriceThreeD);
		}

		[Fact]
		public void TariffTypeTestNormal() 
		{
			var defaultPrice = Tariffs.GetTariffPrice(TariffType.Normal, false); // price for normal ticket + 3d should be 8.50
			var childrenPrice = Tariffs.GetTariffPrice(TariffType.Children, false); // price for children ticket + 3d should be 7
			var studentPrice = Tariffs.GetTariffPrice(TariffType.Students, false); // price for students ticket + 3d should be 7
			var seniorPrice = Tariffs.GetTariffPrice(TariffType.Seniors, false); // price for children ticket + 3d should be 7
			var secretPrice = Tariffs.GetTariffPrice(TariffType.Secret, false); // price for children ticket should be 6

			Assert.Equal(8.5m, defaultPrice);
			Assert.Equal(7m, childrenPrice);
			Assert.Equal(7m, studentPrice);
			Assert.Equal(7m, seniorPrice);
			Assert.Equal(6m, secretPrice);
		}
	}
}
