using PredmetniZadatak1.Model;
using PredmetniZadatak1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PredmetniZadatak1
{
    public partial class MainWindow : Window
    {
        #region Fields
        public static Dictionary<string, bool> dictionary;
        static List<Button> allButtons;
        private static bool enableDrawingPoinstForPolygon;
        static private List<Point> pointsForPolygon;
        public delegate void Del(TemplateShape shape);
        public static Del UpdateShapeDel;
        #endregion

        #region Constructor
        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            Title = "Drawing window";
            UpdateShapeDel = UpdateShapeMethod;
            InitializeComponent();         
            dictionary = new Dictionary<string, bool>
            {
                {buttonEllipse.Name, false},
                {buttonRectangle.Name, false},
                {buttonPolygon.Name, false},
                {buttonImage.Name, false}
            };
            allButtons = new List<Button>
            {
                buttonEllipse,
                buttonRectangle,
                buttonPolygon,
                buttonImage,
                buttonUndo,
                buttonRedo,
                buttonClear
            };
            enableDrawingPoinstForPolygon = false;
            pointsForPolygon = new List<Point>();
        }
        #endregion

        #region Button style
        private void InitialLookButton(Button button)
        {
            Style style = FindResource("InitialLookButton") as Style;
            button.Style = style;
        }

        private void InitialLook2Button(Button button)
        {
            Style style = FindResource("InitialLook2Button") as Style;
            button.Style = style;
        }

        private void DragMouseOverButton(Button button)
        {
            Style style = FindResource("DragMouseOverButton") as Style;
            button.Style = style;
        }

        private void DragMouseOverButton2(Button button)
        {
            Style style = FindResource("DragMouseOverButton2") as Style;
            button.Style = style;
        }

        private void SelectedButton(Button button)
        {
            Style style = FindResource("SelectedButton") as Style;
            if (dictionary[button.Name])
                button.Style = style;
            else
                InitialLookButton(button); // ako dva puta za redom kliknem
        }
        #endregion

        #region Button click
        // cilj -> dobiti dictionary sa 1 button-om cija je vrijednost: true, sve ostale: false
        private void SelectButton(string buttonName)
        {
            foreach (var item in dictionary)
            {
                if (item.Key.Equals(buttonName))
                {
                    dictionary[item.Key] = !item.Value;
                    break;
                }
            }
            Dictionary<string, bool> tempDictionary = new Dictionary<string, bool>(dictionary);
            foreach (var item in dictionary)
            {
                if (!item.Key.Equals(buttonName) && item.Value)
                    tempDictionary[item.Key] = false;
            }
            dictionary = tempDictionary;
        }

        private void buttonEllipse_Click(object sender, RoutedEventArgs e)
        {
            SelectButton(buttonEllipse.Name);
            SelectedButton(buttonEllipse);
            InitialLookButton(buttonImage);
            InitialLookButton(buttonPolygon);
            InitialLookButton(buttonRectangle);
        }

        private void buttonRectangle_Click(object sender, RoutedEventArgs e)
        {
            SelectButton(buttonRectangle.Name);
            SelectedButton(buttonRectangle);
            InitialLookButton(buttonImage);
            InitialLookButton(buttonPolygon);
            InitialLookButton(buttonEllipse);
        }

        private void buttonPolygon_Click(object sender, RoutedEventArgs e)
        {
            SelectButton(buttonPolygon.Name);
            SelectedButton(buttonPolygon);
            InitialLookButton(buttonImage);
            InitialLookButton(buttonRectangle);
            InitialLookButton(buttonEllipse);
            EnebleDrawingPlygon();
        }

        private void buttonImage_Click(object sender, RoutedEventArgs e)
        {
            SelectButton(buttonImage.Name);
            SelectedButton(buttonImage);
            InitialLookButton(buttonPolygon);
            InitialLookButton(buttonRectangle);
            InitialLookButton(buttonEllipse);
        }
        #endregion

        #region Mouse drag
        private void MouseEnter(object sender, MouseEventArgs e)
        {
            foreach (Button item in allButtons)
            {
                if (item.IsMouseOver && dictionary.ContainsKey(item.Name))
                    DragMouseOverButton(item);
                else if (item.IsMouseOver)
                    DragMouseOverButton2(item);
            }
        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            foreach (Button item in allButtons)
                if (!item.IsMouseOver && dictionary.ContainsKey(item.Name))
                {
                    if (dictionary[item.Name])                  
                        SelectedButton(item);                 
                    else
                        InitialLookButton(item);
                }
                else if (!item.IsMouseOver)
                    InitialLook2Button(item);
        }
        #endregion
        
        #region Draw shape
        void DrawShapeOnCanvas()
        {
            TemplateShape templateShape = StackClass.NewShape;
            if (templateShape == null)
                return;
            StackClass.ActiveShape.Push(templateShape);
            DrawingCanvas.Children.Add(templateShape.Draw());
        }

        private void PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in dictionary)
            { 
                if (item.Value)
                {
                    Point point = e.GetPosition(DrawingCanvas);

                    if(item.Key.Equals(buttonEllipse.Name) || item.Key.Equals(buttonRectangle.Name))
                    {
                        EllipseAndRectangleView ERView = new EllipseAndRectangleView(point);
                        ERView.ShowDialog();
                        DrawShapeOnCanvas();
                    }
                    else if (item.Key.Equals(buttonImage.Name))
                    {
                        ImageView IView = new ImageView(point);
                        IView.ShowDialog();
                        DrawShapeOnCanvas();
                    }   
                    else if(item.Key.Equals(buttonPolygon.Name) && enableDrawingPoinstForPolygon)
                    {
                        pointsForPolygon.Add(e.GetPosition(DrawingCanvas)); // dodajem tjemena poligona
                        return;
                    }
                }
            }
        }
        void EnebleDrawingPlygon()
        {
            if (!enableDrawingPoinstForPolygon)
            {
                enableDrawingPoinstForPolygon = true;
                pointsForPolygon = new List<Point>();
            }
            else
                DisableDrawingPoint();          
        }
        void DisableDrawingPoint()
        {
            if (enableDrawingPoinstForPolygon)
            {
                enableDrawingPoinstForPolygon = false;
            }
        }

        private void PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!enableDrawingPoinstForPolygon)
                return;
            if (pointsForPolygon.Count == 0)
                return;
            pointsForPolygon = new List<Point>(pointsForPolygon);
            PolygonView PView = new PolygonView(pointsForPolygon);
            PView.ShowDialog();
            pointsForPolygon = new List<Point>();
            DrawShapeOnCanvas();   
        }
        #endregion

        #region Undo
        private void buttonUndo_Click(object sender, RoutedEventArgs e)
        {
            if (StackClass.ClearShape.Count != 0)
            {
                UndoClearAction();
                return;
            }    
            if (StackClass.ActiveShape.Count == 0)
                return;
            TemplateShape shape = StackClass.ActiveShape.Pop();

            DrawingCanvas.Children.RemoveAt(DrawingCanvas.Children.Count - 1);
            StackClass.Undo.Push(shape);
        }
        void UndoClearAction()
        {
            while (StackClass.ClearShape.Count != 0)
            {
                TemplateShape temp = StackClass.ClearShape.Pop();
                DrawingCanvas.Children.Add(temp.Draw());
                StackClass.ActiveShape.Push(temp);
            }
        }
        #endregion

        #region Redo
        private void buttonRedo_Click(object sender, RoutedEventArgs e)
        {
            if (StackClass.Undo.Count == 0)
                return;
            TemplateShape shape = StackClass.Undo.Pop();
            DrawingCanvas.Children.Add(shape.Draw());
            StackClass.ActiveShape.Push(shape);
        }
        #endregion

        #region Clear
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            while (StackClass.ActiveShape.Count != 0)
            {
                StackClass.ClearShape.Push(StackClass.ActiveShape.Pop());
                DrawingCanvas.Children.Clear();
            }
        }
        #endregion

        #region Update shape
        public void UpdateShapeMethod(TemplateShape shape)
        {
            int index = DrawingCanvas.Children.IndexOf(shape.Shape);
            DrawingCanvas.Children.RemoveAt(index);
            DrawingCanvas.Children.Insert(index, shape.Shape);

        }
        #endregion
    }
}
