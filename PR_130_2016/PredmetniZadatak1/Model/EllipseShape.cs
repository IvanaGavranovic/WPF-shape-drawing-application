using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PredmetniZadatak1.Model
{
    internal class EllipseShape : TemplateShape
    {
        private Coordinates coordinates;
        private int width;
        private int height;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public EllipseShape(double x, double y, int width, int height, Brush fillColor, Brush borderColor, int borderThickness) : base(fillColor, borderColor, borderThickness)
        {
            coordinates = new Coordinates(x, y);
            Width = width;
            Height = height;
        }
       
        public override Shape Draw()
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Height = Height;
            ellipse.Width = Width;
            ellipse.Fill = FillColor;
            ellipse.Stroke = BorderColor;
            ellipse.StrokeThickness = (double)BorderThickness;
            ellipse.Margin = new System.Windows.Thickness(coordinates.X_coordinate, coordinates.Y_coordinate, 0, 0);
            return ellipse;
        }
    }
}
