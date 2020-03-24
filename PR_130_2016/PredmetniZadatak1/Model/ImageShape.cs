using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PredmetniZadatak1.Model
{
    internal class ImageShape : TemplateShape
    {
        private Coordinates coordinates;
        private int width;
        private int height;
        private string path;

        public Coordinates Coordinates { get => coordinates; set => coordinates = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
        public string Path { get => path; set => path = value; }

        public ImageShape(double x, double y, int width, int height, string path)
        {
            coordinates = new Coordinates(x, y);
            Width = width;
            Height = height;
            Path = path;
        }


        public override Shape Draw()
        {
            System.Windows.Shapes.Rectangle retVal = new System.Windows.Shapes.Rectangle();
            retVal.Margin = new System.Windows.Thickness(coordinates.X_coordinate, coordinates.Y_coordinate, 0, 0);
            retVal.Height = Height;
            retVal.Width = Width;

            retVal.Stroke = new SolidColorBrush();
            retVal.StrokeThickness = 0;
            retVal.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(path, UriKind.Absolute))
            };
            return retVal;
        }
    }
}
