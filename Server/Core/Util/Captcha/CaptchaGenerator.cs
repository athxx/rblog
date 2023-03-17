using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Core.Util.Captcha;

public class CaptchaGenerator
{
    private readonly Random _random;

    public CaptchaGenerator()
    {
        _random = new Random();
    }

    public byte[] GenerateCaptcha(int width = 200, int height = 50, int fontSize = 30)
    {
        // Generate random string for captcha
        string captchaText = GenerateRandomString(6);

        // Create image object
        Bitmap captchaImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);

        // Create graphics object for drawing on the image
        Graphics graphics = Graphics.FromImage(captchaImage);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.Clear(Color.White);

        // Draw random lines on the image
        for (int i = 0; i < 10; i++)
        {
            int x1 = _random.Next(width);
            int y1 = _random.Next(height);
            int x2 = _random.Next(width);
            int y2 = _random.Next(height);
            Color color = Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));
            graphics.DrawLine(new Pen(color, 2), x1, y1, x2, y2);
        }

        // Draw the captcha text on the image
        Font font = new Font("Arial", fontSize);
        graphics.DrawString(captchaText, font, Brushes.Black, new PointF(0, 0));

        // Save the image as a byte array
        using MemoryStream memoryStream = new MemoryStream();
        captchaImage.Save(memoryStream, ImageFormat.Png);
        return memoryStream.ToArray();
    }

    private string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}