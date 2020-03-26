using PredmetniZadatak1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
            if (Shape != null)
                return Shape;
            Rectangle shapeImage = new Rectangle();
            shapeImage.Margin = new Thickness(coordinates.X_coordinate, coordinates.Y_coordinate, 0, 0);
            shapeImage.Height = Height;
            shapeImage.Width = Width;

            shapeImage.Stroke = new SolidColorBrush();
            shapeImage.StrokeThickness = 0;
            shapeImage.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(path, UriKind.Absolute))
            };
            shapeImage.MouseLeftButtonDown += ImageMouseLeftButtonDown;
            Shape = shapeImage;
            return shapeImage;
        }
        private void ImageMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ImageView IView = new ImageView(this, EnumShape.IMAGE);
            IView.ShowDialog();

        }
        public void UpdateShape(string path)
        {
            if (Shape == null)
                return;

            Shape.Fill = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(path, UriKind.Absolute))
            };

            Path = path;

        }
    }
}
