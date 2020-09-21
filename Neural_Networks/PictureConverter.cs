using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Neural_Networks
{
    public class PictureConverter
    {
        public int Boundary { get; set; } = 128;
        public int Height { get; set; }
        public int Width { get; set; }
        public List<int> Convert(string path)
        {
            var result = new List<int>();
            Bitmap image = new Bitmap(path);//Bitmap — объект, используемый для работы с изображениями, определяемыми данными пикселей.
            var resizeImage = new Bitmap(image, new Size(10,10));
            Height = resizeImage.Height;
            Width = resizeImage.Width;
            for (int y = 0; y < resizeImage.Height; y++)
            {
                for (int x = 0; x < resizeImage.Width; x++)
                {
                    var pixel = resizeImage.GetPixel(x, y);//получение пикселя
                    var value = Brigtness(pixel);
                    result.Add(value);
                }
            }
            return result;
        }

        private int Brigtness(Color pixel)//яркость
        {
            var result = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B;
            return result < Boundary ? 0 : 1;
        }
        
        public void Save(string path, List<int> pixels)
        {
            var image = new Bitmap(Width, Height);
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    var color = pixels[y * Width + x] == 1 ? Color.White : Color.Black;
                    image.SetPixel(x, y, color);
                }
            }
            image.Save(path);
        }
    }
}
