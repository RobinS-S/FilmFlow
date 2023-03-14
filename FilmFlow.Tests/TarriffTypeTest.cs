using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FilmFlow.Shared;
using FilmFlow.Shared.Enums;

namespace FilmFlow.Tests
{
	public class TarriffTypeTest
	{
		[Fact]
		public void TarriffTypeTest3D()
		{
			var defaultPriceThreeD = Tarriffs.GetTarriffPrice(TarriffType.NORMAL, true); // price for normal ticket + 3d should be 11
			var childrenPriceThreeD = Tarriffs.GetTarriffPrice(TarriffType.CHILDREN, true); // price for children ticket + 3d should be 9.50
			var studentPriceThreeD = Tarriffs.GetTarriffPrice(TarriffType.STUDENTS, true); // price for students ticket + 3d should be 9.50
			var seniorPriceThreeD = Tarriffs.GetTarriffPrice(TarriffType.SENIORS, true); // price for children ticket + 3d should be 9.50
			var secretPriceThreeD = Tarriffs.GetTarriffPrice(TarriffType.SECRET, true); // price for children ticket + 3d should be 8.50

			Assert.Equal(11m, defaultPriceThreeD);
			Assert.Equal(9.5m, childrenPriceThreeD);
			Assert.Equal(9.5m, studentPriceThreeD);
			Assert.Equal(9.5m, seniorPriceThreeD);
			Assert.Equal(8.5m, secretPriceThreeD);
		}

		[Fact]
		public void TarriffTypeTestNormal() 
		{
			var defaultPrice = Tarriffs.GetTarriffPrice(TarriffType.NORMAL, false); // price for normal ticket + 3d should be 8.50
			var childrenPrice = Tarriffs.GetTarriffPrice(TarriffType.CHILDREN, false); // price for children ticket + 3d should be 7
			var studentPrice = Tarriffs.GetTarriffPrice(TarriffType.STUDENTS, false); // price for students ticket + 3d should be 7
			var seniorPrice = Tarriffs.GetTarriffPrice(TarriffType.SENIORS, false); // price for children ticket + 3d should be 7
			var secretPrice = Tarriffs.GetTarriffPrice(TarriffType.SECRET, false); // price for children ticket should be 6

			Assert.Equal(8.5m, defaultPrice);
			Assert.Equal(7m, childrenPrice);
			Assert.Equal(7m, studentPrice);
			Assert.Equal(7m, seniorPrice);
			Assert.Equal(6m, secretPrice);

		}

	}

}
