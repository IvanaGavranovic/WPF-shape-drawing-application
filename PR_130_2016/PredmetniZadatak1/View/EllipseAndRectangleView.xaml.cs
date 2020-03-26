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
        #region Fields
        private EnumShape shapeToDraw;
        private Point pointToDraw;
        private List<TextBox> textBox;
        private List<Label> label;
        TemplateShape shapeToUpdate;
        #endregion

        #region Constructor
        public EllipseAndRectangleView(Point point)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;       
            InitializeComponent();
            ChooseTitle();

            textBox = new List<TextBox>
            {
                textBoxWidth,
                textBoxHeight,
                textBoxThickness
            };
            
            fillColor.ItemsSource = typeof(Colors).GetProperties();
            borderColor.ItemsSource = typeof(Colors).GetProperties();
            pointToDraw = point;
            buttonDraw.Click += buttonDraw_Click;
        }
        #endregion

        #region Constructor for update
        public EllipseAndRectangleView(TemplateShape shape, EnumShape enumShape)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;           
            InitializeComponent();
            Title = "Update shape";

            shapeToUpdate = shape;
            shapeToDraw = enumShape;

            textBox = new List<TextBox>
            {
                textBoxWidth,
                textBoxHeight,
                textBoxThickness
            };
            label = new List<Label>
            {
                labelWidth,
                labelHeight
            };
            textBoxHeight.IsEnabled = false;
            textBoxWidth.IsEnabled = false;
            labelWidth.IsEnabled = false;
            labelHeight.IsEnabled = false;
            fillColor.ItemsSource = typeof(Colors).GetProperties();
            borderColor.ItemsSource = typeof(Colors).GetProperties();

            OldData();

            buttonDraw.Click += buttonDrawUpdate_Click;
        }
        #endregion

        #region Old data
        void DataInTextBox(TextBox textBox, double number)
        {
            textBox.Text = number.ToString();
        }
        private string GetColorName(SolidColorBrush brush)
        {
            var results = typeof(Colors).GetProperties().Where(
             p => (Color)p.GetValue(null, null) == brush.Color).Select(p => p.Name);
            return results.Count() > 0 ? results.First() : String.Empty;
        }
        void SelectColorInCombobox(ComboBox comboBox, Brush brush)
        {
            var list = typeof(Colors).GetProperties();
            string temp = GetColorName((SolidColorBrush)brush);
            var item = list.First(x => x.Name == temp);
            comboBox.SelectedItem = item;
        }
        void OldData()
        {
            switch (shapeToDraw)
            {
                case EnumShape.RECTANGLE:
                    Rectangle rectangle = (Rectangle)shapeToUpdate.Shape;
                    DataInTextBox(textBoxThickness, rectangle.StrokeThickness);
                    SelectColorInCombobox(borderColor, rectangle.Stroke);
                    SelectColorInCombobox(fillColor, rectangle.Fill);
                    break;
                case EnumShape.ELLIPSE:
                    Ellipse ellipse = (Ellipse)shapeToUpdate.Shape;
                    DataInTextBox(textBoxThickness, ellipse.StrokeThickness);
                    SelectColorInCombobox(borderColor, ellipse.Stroke);
                    SelectColorInCombobox(fillColor, ellipse.Fill);
                    break;            
            }
        }
        #endregion


        #region Titles
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
                            shapeToDraw = EnumShape.ELLIPSE;                            
                            break;

                        case "buttonRectangle":
                            Title = "Drawing a rectangle";
                            shapeToDraw = EnumShape.RECTANGLE;                          
                            break;
                    }
                }
            }
        }
      
        #endregion

        #region Mouse position
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
            if (buttonDraw.IsEnabled && buttonDraw.IsMouseOver)
                DragMouseOverButton(buttonDraw);
        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            if(buttonDraw.IsEnabled && !buttonDraw.IsMouseOver)
                InitialLookButton(buttonDraw);
        }

        private void TextboxDrawMouseStyle(TextBox tb)
        {
            Style style = FindResource("TextboxDrawMouseStyle") as Style;
            tb.Style = style;
        }
        private void TextboxStyle(TextBox tb)
        {
            Style style = FindResource("TextboxStyle") as Style;
            tb.Style = style;
        }
        private void MouseEnterTextBox(object sender, MouseEventArgs e)
        {
            foreach (var item in textBox)
            {
                if (item.IsMouseOver)
                    TextboxDrawMouseStyle(item);
                else
                    TextboxStyle(item);
            }
        }
        //private void MouseLeaveTextBox(object sender, MouseEventArgs e)
        //{
        //    foreach (var item in textBox)
        //    {
        //        if (!(item.IsMouseOver))
        //            TextboxStyle(item);
        //    }
        //}
        #endregion

        #region Create shape
        bool ValidateColor(ComboBox comboBox)
        {
            if (borderColor.SelectedItem == null)
            {
                comboBox.BorderBrush = Brushes.Red;
                MessageBox.Show("Please select color!");
                return false;
            }
            comboBox.BorderBrush = Brushes.LightGray;
            return true;
        }

        bool ValidateTexBox(TextBox textBox)
        {
            if (textBox.Text.Length == 0 || string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Style = (Style)FindResource("TextboxErrorStyle");
                MessageBox.Show("Please enter number!");
                return false;
            }

            int num = 0;
            if (!Int32.TryParse(textBox.Text, out num))
            {
                textBox.Style = (Style)FindResource("TextboxErrorStyle");
                MessageBox.Show("Please enter number!");
                return false;
            }
            if (num <= 0)
            {
                textBox.Style = (Style)FindResource("TextboxErrorStyle");
                MessageBox.Show("Number must be greater than zero!");
                return false;
            }
            textBox.Style = (Style)FindResource("TextboxStyle");
            return true;
        }

        TemplateShape CreateEllipse()
        {
            var selectedItem = (PropertyInfo)borderColor.SelectedItem;
            Color color = (Color)selectedItem.GetValue(null, null);
            SolidColorBrush border = new SolidColorBrush(color);

            selectedItem = (PropertyInfo)fillColor.SelectedItem;
            color = (Color)selectedItem.GetValue(null, null);
            SolidColorBrush fill = new SolidColorBrush(color);

            int width = Int32.Parse(textBoxWidth.Text);
            int height = Int32.Parse(textBoxHeight.Text);
            int Thickness = Int32.Parse(textBoxThickness.Text);

            return new EllipseShape(pointToDraw.X, pointToDraw.Y, width, height, fill, border, Thickness);
        }

        TemplateShape CreateRectangle()
        {
            var selectedItem = (PropertyInfo)borderColor.SelectedItem;
            Color color = (Color)selectedItem.GetValue(null, null);
            SolidColorBrush border = new SolidColorBrush(color);

            selectedItem = (PropertyInfo)fillColor.SelectedItem;
            color = (Color)selectedItem.GetValue(null, null);
            SolidColorBrush fill = new SolidColorBrush(color);

            int width = Int32.Parse(textBoxWidth.Text);
            int height = Int32.Parse(textBoxHeight.Text);
            int Thickness = Int32.Parse(textBoxThickness.Text);

            return new RectangleShape(pointToDraw.X, pointToDraw.Y, width, height, fill, border, Thickness);
        }
        #endregion

        #region Update shape
        void UpdateRectangle(RectangleShape rectangle)
        {
            var selectedItem = (PropertyInfo)borderColor.SelectedItem;
            Color color = (Color)selectedItem.GetValue(null, null);
            SolidColorBrush border = new SolidColorBrush(color);

            selectedItem = (PropertyInfo)fillColor.SelectedItem;
            color = (Color)selectedItem.GetValue(null, null);
            SolidColorBrush fill = new SolidColorBrush(color);
            int Thickness = Int32.Parse(textBoxThickness.Text);

            rectangle.UpdateShape(fill, border, Thickness);
        }

        void UpdateEllipse(EllipseShape ellipse)
        {
            var selectedItem = (PropertyInfo)borderColor.SelectedItem;
            Color color = (Color)selectedItem.GetValue(null, null);
            SolidColorBrush border = new SolidColorBrush(color);

            selectedItem = (PropertyInfo)fillColor.SelectedItem;
            color = (Color)selectedItem.GetValue(null, null);
            SolidColorBrush fill = new SolidColorBrush(color);
            int Thickness = Int32.Parse(textBoxThickness.Text);

            ellipse.UpdateShape(fill, border, Thickness);
        }
        #endregion

        #region Draw button
        private void buttonDraw_Click(object sender, RoutedEventArgs e)
        {           
            TemplateShape ret = null;
            switch (shapeToDraw)
            {
                case EnumShape.ELLIPSE:
                    if (!(ValidateTexBox(textBoxWidth) && ValidateTexBox(textBoxHeight) && ValidateTexBox(textBoxThickness) && ValidateColor(borderColor) && ValidateColor(fillColor)))
                        return;
                    ret = CreateEllipse();
                    break;
                case EnumShape.RECTANGLE:
                    if (!(ValidateTexBox(textBoxWidth) && ValidateTexBox(textBoxHeight) && ValidateTexBox(textBoxThickness) && ValidateColor(borderColor) && ValidateColor(fillColor)))
                        return;
                    ret = CreateRectangle();
                    break;
            }
            StackClass.NewShape = ret;
            SelectedButton(buttonDraw);
            Close();
        }

        public void buttonDrawUpdate_Click(object sender, RoutedEventArgs e)
        {         
            switch (shapeToDraw)
            {
                case EnumShape.ELLIPSE:
                    if (!(ValidateTexBox(textBoxThickness) && ValidateColor(borderColor) && ValidateColor(fillColor)))
                        return;
                    UpdateEllipse((EllipseShape)shapeToUpdate);
                    break;
                case EnumShape.RECTANGLE:
                    if (!(ValidateTexBox(textBoxThickness) && ValidateColor(borderColor) && ValidateColor(fillColor)))
                        return;
                    UpdateRectangle((RectangleShape)shapeToUpdate);
                    break;
            }
            MainWindow.UpdateShapeDel(shapeToUpdate);
            SelectedButton(buttonDraw);
            Close();
        }
        #endregion
    }
}
