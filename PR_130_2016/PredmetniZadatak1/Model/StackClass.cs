using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredmetniZadatak1.Model
{
    public class StackClass
    {
        private static TemplateShape newShape;
        private static Stack<TemplateShape> undo = new Stack<TemplateShape>();
        private static Stack<TemplateShape> redo;
        private static Stack<TemplateShape> activeShape = new Stack<TemplateShape>();
        private static Stack<TemplateShape> clearShape = new Stack<TemplateShape>();

        public static TemplateShape NewShape
        {
            get
            {
                TemplateShape temp = newShape;
                newShape = null;
                return temp;
            }
            set => newShape = value;
        }
        public static Stack<TemplateShape> ActiveShape { get => activeShape; set => activeShape = value; }
        public static Stack<TemplateShape> ClearShape { get => clearShape; set => clearShape = value; }
        public static Stack<TemplateShape> Undo { get => undo; set => undo = value; }
        public static Stack<TemplateShape> Redo { get => redo; set => redo = value; }

    }
}
