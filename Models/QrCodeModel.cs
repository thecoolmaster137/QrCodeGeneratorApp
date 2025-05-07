namespace QrCodeGeneratorApp.Models
{
    public class QrCodeModel
    {
        public string Text { get; set; }
        public string Format { get; set; } = "png";
        public byte[] QrCodeImage { get; set; }
    }
}
