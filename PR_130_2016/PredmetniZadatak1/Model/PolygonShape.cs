﻿using PredmetniZadatak1.View;
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
    internal class PolygonShape : TemplateShape
    {
        private PointCollection pointColection;
        public PointCollection PointColection { get => pointColection; set => pointColection = value; }

        public PolygonShape(List<System.Windows.Point> points, Brush fillColor, Brush borderColor, int borderThickness) : base(fillColor, borderColor, borderThickness)
        {
            pointColection = new PointCollection(points);
        }

        private void Polygon_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override Shape Draw()
        {
            if (Shape != null)
                return Shape;
            Polygon polygon = new Polygon();
            polygon.Stroke = BorderColor;
            polygon.Fill = FillColor;
            polygon.StrokeThickness = BorderThickness;
            polygon.Points = PointColection;
            polygon.MouseLeftButtonDown += PolygonMouseLeftButtonDown;
            Shape = polygon;
            return Shape;
        }

        private void PolygonMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PolygonView PView = new PolygonView(this, EnumShape.POLYGON);                  
            PView.ShowDialog();
        }

        public void UpdateShape(Brush fillColor, Brush borderColor, int borderThickness)
        {
            if (Shape == null)
                return;
            Shape.Stroke = borderColor;
            Shape.Fill = fillColor;
            Shape.StrokeThickness = borderThickness;
            BorderThickness = borderThickness;
            FillColor = fillColor;
            BorderColor = borderColor;
        }
    }
}
