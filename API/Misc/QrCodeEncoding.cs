using Net.Codecrete.QrCodeGenerator;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace FilmFlow.API.Misc
{
    public class QrCodeEncoding
    {
		private const string AvansLogoBase64 = "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";

		public static async Task<byte[]> GenerateQrCodeAsPngFromText(string text)
		{
			var qr = QrCode.EncodeText(text, QrCode.Ecc.Medium);
			var logoData = Convert.FromBase64String(AvansLogoBase64);

			const float logoWidth = 0.15f;
			var stream = new MemoryStream();
			using (var bitmap = qr.ToBitmap(scale: 10, border: 4, Color.ParseHex("c7202d"), Color.White))
			using (var logo = Image.Load<Rgba32>(logoData, out var format))
			{
				var w = (int)Math.Round(bitmap.Width * logoWidth);
				logo.Mutate(logo => logo.Resize(w, 0));

				var topLeft = new Point((bitmap.Width - logo.Width) / 2, (bitmap.Height - logo.Height) / 2);
				bitmap.Mutate(img => img.DrawImage(logo, topLeft, 1));

				await bitmap.SaveAsPngAsync(stream);
			}

			return stream.ToArray();
        }

        public static async Task<Image> GenerateQrCodeAsImageFromText(string text)
        {
            var qr = QrCode.EncodeText(text, QrCode.Ecc.Medium);
            var logoData = Convert.FromBase64String(AvansLogoBase64);

            const float logoWidth = 0.15f;
            var stream = new MemoryStream();
            var bitmap = qr.ToBitmap(scale: 10, border: 4, Color.ParseHex("c7202d"), Color.White);
            using var logo = Image.Load<Rgba32>(logoData, out IImageFormat format);
            var w = (int)Math.Round(bitmap.Width * logoWidth);
            logo.Mutate(logo => logo.Resize(w, 0));

            var topLeft = new Point((bitmap.Width - logo.Width) / 2, (bitmap.Height - logo.Height) / 2);
            bitmap.Mutate(img => img.DrawImage(logo, topLeft, 1));

            await bitmap.SaveAsPngAsync(stream);
            return bitmap;
        }

        public static async Task<byte[]> GenerateTicket(string code, string title, string time, string subtitle, string bodyText)
        {
            var stream = new MemoryStream();
            var logoData = Convert.FromBase64String(AvansLogoBase64);
            var font = SystemFonts.Get("Verdana").CreateFont(24);

            var textOptions = new TextOptions(font)
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
            };

            var image = await GenerateQrCodeAsImageFromText(code);

            var padding = 20;
            var titleHeight = 50 + padding;
            var subtitleHeight = 50 + padding;
            var timeHeight = 50 + padding;
            var bodyTextHeight = 50 * 4 + padding;
            var imageWidth = image.Width / 2;
            var imageHeight = (int)(((double)imageWidth / image.Width) * image.Height);

            var outputImage = new Image<Rgba32>(850, 500, Color.ParseHex("c7202d"));
            outputImage.Mutate(x => x.DrawText(title, font, Color.Black, new PointF(padding, padding)));
            outputImage.Mutate(x => x.DrawText(subtitle, font, Color.GhostWhite, new PointF(padding, titleHeight)));
            outputImage.Mutate(x => x.DrawText(time, font, Color.DarkGray, new PointF(padding, titleHeight + timeHeight)));
            outputImage.Mutate(x => x.DrawText(bodyText, font, Color.Yellow, new PointF(padding, titleHeight + timeHeight + subtitleHeight)));
            outputImage.Mutate(x => x.DrawImage(image, new Point(padding * 2 + 340, padding), PixelColorBlendingMode.Normal, 1.0f));
            await outputImage.SaveAsPngAsync(stream);

            return stream.ToArray();
        }
    }
}
