using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PredmetniZadatak1.Model
{
    public abstract class TemplateShape
    {
        private Brush fillColor;
        private Brush borderColor;
        private int borderThickness;

        public Brush FillColor { get => fillColor; set => fillColor = value; }
        public Brush BorderColor { get => borderColor; set => borderColor = value; }
        public int BorderThickness { get => borderThickness; set => borderThickness = value; }
        public TemplateShape(){}
        public TemplateShape(Brush fillColor, Brush borderColor, int borederThickness)
        {
            FillColor = fillColor;
            BorderColor = borderColor;
            BorderThickness = borederThickness;
        }

        public abstract Shape Draw();
    }
}
