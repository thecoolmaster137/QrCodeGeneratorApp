using Microsoft.AspNetCore.Mvc;
using QRCoder;
using QrCodeGeneratorApp.Models;
using System.IO;

namespace QrCodeGeneratorApp.Controllers
{
    public class QrCodeController : Controller
    {
        public IActionResult Index() => View(new QrCodeModel());

        [HttpPost]
        public IActionResult Generate(QrCodeModel model)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(model.Text ?? "", QRCodeGenerator.ECCLevel.Q);

            var qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);

            model.QrCodeImage = qrCodeImage;
            return View("Index", model);
        }

        public IActionResult Download(string format, string text)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(text ?? "", QRCodeGenerator.ECCLevel.Q);

            // Use PNG byte array for all formats
            var qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);

            string contentType = "image/png";
            string fileName = "qrcode.png";

            // Optional: You can adjust this based on desired format support
            switch (format.ToLower())
            {
                case "jpg":
                    fileName = "qrcode.jpg";
                    break;
                case "bmp":
                    fileName = "qrcode.bmp";
                    break;
            }

            return File(qrCodeImage, contentType, fileName);
        }
    }
}
