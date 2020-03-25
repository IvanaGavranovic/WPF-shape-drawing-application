using Microsoft.Win32;
using PredmetniZadatak1.Model;
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
using System.Windows.Shapes;

namespace PredmetniZadatak1.View
{
    /// <summary>
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView : Window
    {
        private EnumShape shapeToDraw;
        private List<TextBox> textBox;
        private List<Button> buttons;
        private Point pointToDraw;
        private string imgPath;

        public ImageView(Point point)
        {
            Title = "Image window";
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();

            textBox = new List<TextBox>
            {
                textBoxWidth,
                textBoxHeight,
            };
            buttons = new List<Button>
            {
                buttonChoose,
                buttonDraw
            };
            pointToDraw = point;
            shapeToDraw = EnumShape.IMAGE;
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
            foreach (var item in buttons)
            {
                if (item.IsEnabled && item.IsMouseOver)
                    DragMouseOverButton(item);
            }
        }

        private void MouseLeave(object sender, MouseEventArgs e)
        {
            foreach (var item in buttons)
            {
                if (item.IsEnabled && !item.IsMouseOver)
                    InitialLookButton(item);
            }
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

        bool ValidateImagePath()
        {
            if (imgPath.Equals(string.Empty))
            {
                MessageBox.Show("Please choose image!");
                return false;
            }
            return true;
        }

        TemplateShape CreateImage()
        {
            int width = Int32.Parse(textBoxWidth.Text);
            int height = Int32.Parse(textBoxHeight.Text);
            return new ImageShape(pointToDraw.X, pointToDraw.Y, width, height, imgPath);
        }       

        private void buttonDraw_Click(object sender, RoutedEventArgs e)
        {
            TemplateShape ret = null;
            switch (shapeToDraw)
            {
                case EnumShape.IMAGE:
                    if (!(ValidateTexBox(textBoxWidth) && ValidateTexBox(textBoxHeight) && ValidateImagePath()))
                        return;
                    ret = CreateImage();
                    break;
            }
            StackClass.NewShape = ret;
            SelectedButton(buttonDraw);
            Close();
        }

        private void buttonChoose_Click(object sender, RoutedEventArgs e)
        {
            SelectedButton(buttonChoose);
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png|All files (*.*)|*.*";

            dialog.InitialDirectory = @"C:\";
            dialog.Title = "Please choose image!";

            if (dialog.ShowDialog() == true)
            {
                imgPath = dialog.FileName;
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(imgPath);
                logo.EndInit();

                imgImage.Source = logo;
            }
        }
    }
}
