using Net.Codecrete.QrCodeGenerator;
using SkiaSharp;

namespace FilmFlow.API.Misc
{
    public class QrCodeEncoding
    {
        public static byte[] GenerateQrCodeAsPngFromText(string text)
        {
            return QrCode.EncodeText(text, QrCode.Ecc.Medium).ToPng(10, 4, SKColor.Parse("1F0"), SKColor.Parse("FFFFFF")); // Green foreground, white background
        }
    }
}
