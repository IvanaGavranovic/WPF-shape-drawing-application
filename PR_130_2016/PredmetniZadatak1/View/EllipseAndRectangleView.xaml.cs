using PredmetniZadatak1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PredmetniZadatak1.View
{  
    public partial class EllipseAndRectangleView : Window
    {
        private Point pointToDraw;
        private Button drawButton;

        public EllipseAndRectangleView(Point point)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
      
            fillColor.ItemsSource = typeof(Colors).GetProperties();
            borderColor.ItemsSource = typeof(Colors).GetProperties();

            pointToDraw = point;
            ChooseTitle();
        }

        public void ChooseTitle()
        {
            foreach (var item in MainWindow.dictionary)
            {
                if (item.Value)
                {
                    switch (item.Key)
                    {
                        case "buttonEllipse":
                            Title = "Drawing an ellipse";

                            break;

                        case "buttonRectangle":
                            Title = "Drawing a rectangle";
                            break;
                    }
                }
            }
        }

        private void InitialLookButton(Button button)
        {
            Style style = FindResource("InitialLookButton") as Style;
            button.Style = style;
        }
        private void DragMouseOverButton(Button button)
        {
            Style style = FindResource("DragMouseOverButton") as Style;
            button.Style = style;
        }
        private void SelectedButton(Button button)
        {
            Style style = FindResource("SelectedButton") as Style;
            button.Style = style;
        }
        
        private void MouseEnter(object sender, MouseEventArgs e)
        {
            DragMouseOverButton(buttonDraw);
        }
        private void MouseLeave(object sender, MouseEventArgs e)
        {
            InitialLookButton(buttonDraw);
        }

        private void buttonDraw_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (PropertyInfo)borderColor.SelectedItem;
            Color color = (Color)selectedItem.GetValue(null, null);
            SolidColorBrush b = new SolidColorBrush(color);

            EllipseShape ellipse = new EllipseShape(pointToDraw.X, pointToDraw.Y, 100, 200, b, b, 2);
            StackClass.NewShape = ellipse;
            Close();
        }
        
    }
}
