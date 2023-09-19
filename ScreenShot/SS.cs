using System.Drawing;


namespace ScreenShot
{
    class Program
    {
        private static string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private static string imagesFolderPath = Path.Combine(desktopPath, "Images");

        [STAThread]
        static void Main(string[] args)
        {
            // Ensure the "Images" folder exists
            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            // Hook into the Enter key press event
            KeyboardHook.KeyPressed += (sender, e) =>
            {
                if (e.Key == Keys.Enter)
                {
                    // Capture and save a screenshot
                    string screenshotPath = CaptureAndSaveScreenshot(imagesFolderPath);

                    if (!string.IsNullOrEmpty(screenshotPath))
                    {
                        Console.WriteLine($"Screenshot saved as: {screenshotPath}");
                    }
                    else
                    {
                        Console.WriteLine("Failed to capture a screenshot.");
                    }
                }
            };

            // Run the application
            Application.Run();
        }

        static string CaptureAndSaveScreenshot(string outputPath)
        {
            DateTime now = DateTime.Now;
            string fileName = $"screenshot_{now:yyyyMMddHHmmss}.png";
            string filePath = Path.Combine(outputPath, fileName);

            using (var bitmap = new Bitmap(ScreenWidth, ScreenHeight))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(0, 0, 0, 0, new Size(ScreenWidth, ScreenHeight));
                }

                bitmap.Save(filePath, ImageFormat.Png);
            }

            return filePath;
        }

        const int ScreenWidth = 1920; // Set to your screen's width
        const int ScreenHeight = 1080; // Set to your screen's height
    }

}
