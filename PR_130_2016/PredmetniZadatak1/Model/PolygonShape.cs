using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PredmetniZadatak1.Model
{
    internal class PolygonShape : TemplateShape
    {
        private PointCollection pointColection;
        public PointCollection PointColection { get => pointColection; set => pointColection = value; }

        public PolygonShape(List<System.Windows.Point> points, Brush fillColor, Brush borderColor, int borderThickness) : base(fillColor, borderColor, borderThickness)
        {
            pointColection = new PointCollection(points);
        }

        private void RetVal_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override Shape Draw()
        {
            if (Shape != null)
                return Shape;
            Polygon retVal = new Polygon();
            retVal.Stroke = BorderColor;
            retVal.Fill = FillColor;
            retVal.StrokeThickness = BorderThickness;
            retVal.Points = PointColection;
            retVal.MouseLeftButtonDown += RetVal_MouseLeftButtonDown;
            Shape = retVal;
            return Shape;
        }
    }
}
