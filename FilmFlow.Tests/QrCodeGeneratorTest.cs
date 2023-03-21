using FilmFlow.Application;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;

namespace FilmFlow.Tests
{
	public class QrCodeGeneratorTest
	{
		[Fact]
		public async void GeneratesValidQrCodeImage()
		{
			const string text = "TestQrCodeImageStringFilmFlowAvans";
			var qrCode = await QrCodeEncoding.GenerateQrCodeAsImageFromText(text);

			Assert.True(qrCode.Height > 50);
			Assert.True(qrCode.Width > 50);

			var baseEncoded = qrCode.ToBase64String(PngFormat.Instance);
			Assert.True(baseEncoded.Length > 10);
			Assert.EndsWith("==", baseEncoded);
		}
	}
}
