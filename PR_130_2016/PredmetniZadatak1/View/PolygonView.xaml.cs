﻿using PredmetniZadatak1.Model;
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
    public partial class PolygonView : Window
    {
        #region Fields
        private EnumShape shapeToDraw;
        private List<Point> pointsForPolygon;
        private TextBox textBox;
        private Button drawButton;
        TemplateShape shapeToUpdate;
        #endregion

        #region Constructor
        public PolygonView(List<Point> points)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;          
            InitializeComponent();
            Title = "Drawing polygon";

            fillColor.ItemsSource = typeof(Colors).GetProperties();
            borderColor.ItemsSource = typeof(Colors).GetProperties();
            pointsForPolygon = points;
            shapeToDraw = EnumShape.POLYGON;
            buttonDraw.Click += buttonDraw_Click;
        }
        #endregion

        #region Constructor for update
        public PolygonView(TemplateShape shape, EnumShape enumShape)
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;          
            InitializeComponent();
            Title = "Update polygon window";

            shapeToUpdate = shape;
            shapeToDraw = enumShape;
            fillColor.ItemsSource = typeof(Colors).GetProperties();
            borderColor.ItemsSource = typeof(Colors).GetProperties();
            shapeToDraw = EnumShape.POLYGON;

            OldPolygonData();

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
        void OldPolygonData()
        {
            Polygon shape = (Polygon)shapeToUpdate.Shape;
            DataInTextBox(textBoxThickness, shape.StrokeThickness);
            SelectColorInCombobox(borderColor, shape.Stroke);
            SelectColorInCombobox(fillColor, shape.Fill);
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
            if (buttonDraw.IsEnabled && !buttonDraw.IsMouseOver)
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
            if (textBoxThickness.IsMouseOver)
                TextboxDrawMouseStyle(textBoxThickness);
            else
                TextboxStyle(textBoxThickness);
        }
        #endregion

        #region Create Shape
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

        SolidColorBrush CreateColor(ComboBox comboBox)
        {
            var selectedItem = (PropertyInfo)comboBox.SelectedItem;
            Color color = (Color)selectedItem.GetValue(null, null);
            return new SolidColorBrush(color);
        }

        TemplateShape CreatePolygon()
        {
            SolidColorBrush border = CreateColor(borderColor);
            SolidColorBrush Fill = CreateColor(fillColor);
            int borderTh = Int32.Parse(textBoxThickness.Text);

            return new PolygonShape(pointsForPolygon, Fill, border, borderTh);
        }
        #endregion

        #region Update shape
        void UpdatePolygon(PolygonShape polygon)
        {
            SolidColorBrush border = CreateColor(borderColor);
            SolidColorBrush fill = CreateColor(fillColor);
            int Thickness = Int32.Parse(textBoxThickness.Text);

            polygon.UpdateShape(fill, border, Thickness);
        }
        #endregion

        #region Draw button
        private void buttonDraw_Click(object sender, RoutedEventArgs e)
        {
            TemplateShape ret = null;
            switch (shapeToDraw)
            {
                case EnumShape.POLYGON:
                    if (!(ValidateTexBox(textBoxThickness) && ValidateColor(borderColor) && ValidateColor(fillColor)))
                        return;
                    ret = CreatePolygon();
                    break;
            }
            StackClass.NewShape = ret;
            SelectedButton(buttonDraw);
            Close();
        }

        private void buttonDrawUpdate_Click(object sender, RoutedEventArgs e)
        {
            switch (shapeToDraw)
            {
                case EnumShape.POLYGON:
                    if (!(ValidateTexBox(textBoxThickness) && ValidateColor(borderColor) && ValidateColor(fillColor)))
                        return;
                    UpdatePolygon((PolygonShape)shapeToUpdate);
                    break;
            }
            MainWindow.UpdateShapeDel(shapeToUpdate);
            SelectedButton(buttonDraw);
            Close();
        }
        #endregion
    }
}

