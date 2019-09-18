using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResizer
{
    class ProgramSync
    {
        static async Task Main(string[] args)
        {
            string sourcePath = Path.Combine(Environment.CurrentDirectory, "images");
            string destinationPath = Path.Combine(Environment.CurrentDirectory, "output");
            string destinationPathAsync = Path.Combine(Environment.CurrentDirectory, "outputAsync");

            ImageProcess imageProcess = new ImageProcess();
            imageProcess.Clean(destinationPath);
            imageProcess.Clean(destinationPathAsync);

            Stopwatch SyncSW = new Stopwatch();
            SyncSW.Start();
            imageProcess.ResizeImages(sourcePath, destinationPath, 2.0);
            SyncSW.Stop();

            Stopwatch AsyncSW = new Stopwatch();
            AsyncSW.Start();
            var tList = imageProcess.ResizeImagesAsync(sourcePath, destinationPathAsync, 2.0);
            await Task.WhenAll(tList);
            AsyncSW.Stop();

            Console.WriteLine($"同步花費時間: {SyncSW.ElapsedMilliseconds} ms");
            Console.WriteLine($"非同步花費時間: {AsyncSW.ElapsedMilliseconds} ms");

            double e = SyncSW.ElapsedMilliseconds - AsyncSW.ElapsedMilliseconds;
            Console.WriteLine($"效能快了: {Math.Round((double)(e / (double)SyncSW.ElapsedMilliseconds), 4) * 100}% ");

            Console.ReadKey();
        }
    }
}
