using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Dictionary<string, bool> dictionary;
        static List<Button> allButtons;

        public MainWindow()
        {
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

        #region Button click
        // cilj -> dobiti dictionary sa 1 button-om cija je vrijednost true, svi ostali false
        private void SelectButton(string buttonName)
        {
            // kliknuti dugmic ce promijeniti svoju vrijednost(boju)
            Dictionary<string, bool> tempDictionary = new Dictionary<string, bool>(dictionary);
            foreach (var item in dictionary)
            {
                if (item.Key.Equals(buttonName))
                {
                    dictionary[item.Key] = !item.Value;
                    break;
                }
            }
            // ostali ce biti iskljuceni
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
                if (item.IsMouseOver)
                    DragMouseOverButton(item);
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
                    InitialLookButton(item);
        }
        #endregion
    }
}
