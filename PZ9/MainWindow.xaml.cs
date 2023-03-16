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
using System.IO;

namespace PZ9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ColorRGB mcolor { get; set; }
        public Color clr { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            mcolor = new ColorRGB();
            mcolor.red = 0;
            mcolor.green = 0;
            mcolor.blue = 0;
            this.lbl1.Background = new SolidColorBrush(Color.FromRgb(mcolor.red, mcolor.green, mcolor.blue));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.inkCanvas1.Strokes.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.inkCanvas1.EditingMode = InkCanvasEditingMode.None;
            string imgPath = @"..\file.gif"; //Куда сохраняется файл
            MemoryStream ms = new MemoryStream();  
            FileStream fs = new FileStream(imgPath, FileMode.Create); 

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)inkCanvas1.Width, (int)inkCanvas1.Height, 96, 96, PixelFormats.Default);
            rtb.Render(inkCanvas1);

            GifBitmapEncoder gifEnc = new GifBitmapEncoder(); 
            gifEnc.Frames.Add(BitmapFrame.Create(rtb));
            gifEnc.Save(fs);
            fs.Close();
            this.inkCanvas1.EditingMode = InkCanvasEditingMode.Ink;
            MessageBox.Show("Файл сохранен, " + imgPath); 
        }

        private void sld_Color_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            string crlName = slider.Name; 
            double value = slider.Value; 
                                         
            if (crlName.Equals("sld_RedColor"))
            {
                mcolor.red = Convert.ToByte(value);
            }
            if (crlName.Equals("sld_GreenColor"))
            {
                mcolor.green = Convert.ToByte(value);
            }
            if (crlName.Equals("sld_BlueColor"))
            {
                mcolor.blue = Convert.ToByte(value);
            }

            
            clr = Color.FromRgb(mcolor.red, mcolor.green, mcolor.blue);
            
            this.lbl1.Background = new SolidColorBrush(Color.FromRgb(mcolor.red, mcolor.green, mcolor.blue));
            
            this.inkCanvas1.DefaultDrawingAttributes.Color = clr;
        }

        private void btn_Select_Click(object sender, RoutedEventArgs e)
        {
            this.inkCanvas1.EditingMode = InkCanvasEditingMode.Select;
        }

        private void btn_AddText_Click(object sender, RoutedEventArgs e)
        {
            TextBox tb = new TextBox
            {
                Width = 100,
                Height = 50,
                BorderThickness = new Thickness(1),
                BorderBrush = new SolidColorBrush(Color.FromRgb(5, 5, 5)),
                Margin = new Thickness(20, 20, 0, 0)
            };
            this.inkCanvas1.Children.Add(tb);
            tb.Focus();
        }

        private void set_color_Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            this.lbl1.Background = btn.Background;
            
            this.inkCanvas1.DefaultDrawingAttributes.Color = ((System.Windows.Media.SolidColorBrush)(btn.Background)).Color;
        }
    }
}


public class ColorRGB
{
    public byte red { get; set; }
    public byte green { get; set; }
    public byte blue { get; set; }
}