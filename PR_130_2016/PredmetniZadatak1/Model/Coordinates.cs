using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredmetniZadatak1.Model
{
    public class Coordinates
    {
        private double x_coordinate;
        private double y_coordinate;

        public double X_coordinate { get => x_coordinate; set => x_coordinate = value; }
        public double Y_coordinate { get => y_coordinate; set => y_coordinate = value; }

        public Coordinates(double x = 0, double y = 0)
        {
            if (x < 0 || y < 0)
                throw new ArgumentOutOfRangeException();
            X_coordinate = x;
            Y_coordinate = y;
        }
    }
}
