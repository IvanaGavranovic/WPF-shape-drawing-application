using PredmetniZadatak1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PredmetniZadatak1.Model
{
    public class RectangleShape: TemplateShape
    {
        private Coordinates coordinates;
        private int width;
        private int height;

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public RectangleShape(double x, double y, int width, int height, Brush fillColor, Brush borderColor, int borderThickness) : base(fillColor, borderColor, borderThickness)
        {
            coordinates = new Coordinates(x, y);
            Width = width;
            Height = height;
        }

        public override Shape Draw()
        {
            if (Shape != null)
                return Shape;
            Rectangle rectangle = new Rectangle();
            rectangle.Height = Height;
            rectangle.Width = Width;
            rectangle.Fill = FillColor;
            rectangle.Stroke = BorderColor;
            rectangle.StrokeThickness = (double)BorderThickness;
            rectangle.Margin = new System.Windows.Thickness(coordinates.X_coordinate, coordinates.Y_coordinate, 0, 0);
            rectangle.MouseLeftButtonDown += RectangleMouseLeftButtonDown;
            Shape = rectangle;
            return rectangle;
        }
        private void RectangleMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EllipseAndRectangleView ERView = new EllipseAndRectangleView(this, EnumShape.RECTANGLE);
            ERView.ShowDialog();
        }

        public void UpdateShape(Brush fillColor, Brush borderColor, int borderThickness)
        {
            if (Shape == null)
                return;
            Shape.Fill = fillColor;
            Shape.Stroke = borderColor;
            Shape.StrokeThickness = borderThickness;
            FillColor = fillColor;
            BorderColor = borderColor;
            BorderThickness = borderThickness;
        }
    }
}
