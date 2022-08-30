using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

string item = Directory.GetCurrentDirectory();
string path = item + @"\" + "pictures";

//Create new folder to hold rotated pictures
string new_path = item + @"\" + "rotated_pictures";
if (Directory.Exists(new_path) == false)
{
    Directory.CreateDirectory(new_path);
}

//Initialize Bitmap/image
Bitmap bitmap1;

//FUNCTION
//Accepts image path, rotate then save to new directory
if (Directory.Exists(path))
{
    List<Bitmap> fruits = new List<Bitmap>();
    foreach (string fileName in Directory.GetFiles(path))
    {
        bitmap1 = (Bitmap)Bitmap.FromFile(fileName);
        fruits.Add(bitmap1);
    }
    //BEGIN STOPWATCH
    var watch = Stopwatch.StartNew();
        Parallel.ForEach(fruits, i =>
        {
            i.RotateFlip(RotateFlipType.Rotate270FlipY);
            int threadID = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine("Thread " + threadID);
        }
        );
        watch.Stop();
        TimeSpan ts = watch.Elapsed;
        Console.WriteLine("Total time elapsed: " + ts);
    for (int i = 0; i < fruits.Count; i++)
    {
        Bitmap fileName = fruits[i];
        string fileSavePath = Path.Combine(new_path, "image_rotated_" + i);
        Image copy = fileName;
        copy.Save(fileSavePath, ImageFormat.Jpeg);
   
    }
}
else
{
    Console.WriteLine("{0} is not a valid file or directory.", path);
}
