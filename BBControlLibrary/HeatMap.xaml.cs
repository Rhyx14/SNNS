using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BBControlLibrary
{
    /// <summary>
    /// HeatMap.xaml 的交互逻辑
    /// </summary>
    public partial class HeatMap : UserControl
    {
        public enum Mode : byte
        {
            Color=0,
            Gray
        }
        int PixelWidth = 8;
        double Max = 0;
        int PWidth { get; set; }
        int PHeight { get; set; }
        List<Pixel> Pixels = new List<Pixel>();
        double[] Data;

        public HeatMap(int height,int width,double[] data,int scale=8,Mode mode=Mode.Color)
        {
            PixelWidth = scale;
            this.PWidth = width;
            this.PHeight = height;
            this.Data = data;
            // TODO 参数检查
            InitializeComponent();
            this.MainCanvas.Width = PixelWidth * PWidth;
            this.MainCanvas.Height = PixelWidth * PHeight;

            this.Max = data.Max();

            if (mode == Mode.Gray)
            {
                for (int j = 0; j < PHeight; j++)
                {
                    for (int i = 0; i < PWidth; i++)
                    {
                        var n = new Pixel(PixelWidth, this.Pixels.Count, OnMouseOver, new SolidColorBrush(GetGrayColor(Data[i * PWidth + j])));

                        n.SetValue(Canvas.LeftProperty, (double)(j * PixelWidth));
                        n.SetValue(Canvas.TopProperty, (double)(i * PixelWidth));

                        this.Pixels.Add(n);
                        this.MainCanvas.Children.Add(n);
                    }
                }
            }
            else if (mode==Mode.Color)
            {
                for (int j = 0; j < PHeight; j++)
                {
                    for (int i = 0; i < PWidth; i++)
                    {
                        var n = new Pixel(PixelWidth,this.Pixels.Count,OnMouseOver,new SolidColorBrush(GetColor(Data[i * PWidth + j])));

                        n.SetValue(Canvas.LeftProperty, (double)(j * PixelWidth));
                        n.SetValue(Canvas.TopProperty, (double)(i * PixelWidth));

                        this.Pixels.Add(n);           
                        this.MainCanvas.Children.Add(n);
                    }
                }
            }
        }

        public void Update(double[] data,Mode mode)
        {
            this.Max = data.Max();
            this.Data = data;
            int index = 0;
            if (mode == Mode.Color)
            {
                for (int j = 0; j < PHeight; j++)
                {
                    for (int i = 0; i < PWidth; i++)
                    {
                        index = i * PWidth + j;
                        var n = Pixels[index];
                        n.Background = new SolidColorBrush(GetColor(Data[index]));
                    }
                }
            }
            else if (mode ==Mode.Gray)
            {
                for (int j = 0; j < PHeight; j++)
                {
                    for (int i = 0; i < PWidth; i++)
                    {
                        index = i * PWidth + j;
                        var n = Pixels[index];
                        n.Background = new SolidColorBrush(GetGrayColor(Data[index]));
                    }
                }
            }

        }

        private void OnMouseOver(object sender, MouseEventArgs e)
        {
            var n = sender as Pixel;
            this.X.Text = $"{n.Index% PWidth}";
            this.Y.Text = $"{n.Index/PWidth}";
            this.Values.Text = $"{Data[n.Index]}";
        }


        Color GetGrayColor(double d)
        {
            var t = (byte)(d / Max * 255);
            if (t>255)
            {
                t = 255;
            }
            return Color.FromRgb(t,t,t);
        }
        Color GetColor(double d)
        {
            //to hsv
            var hi = (int)(d / Max * 234);
            if (hi > 234)
            {
                hi = 234;
            }
            hi = 234 - hi;

            int h = (hi / 60) % 6;
            double f = (double)hi / 60 - h;
            double s = 1;
            double v = 1;

            //int p = v * (1 - s);
            //int q = v * (1 - f * s);
            //int t = v * (1 - (1 - f) * s);

            double p = 0;
            double q = 1 - f;
            double t = f;

            double r =0, g=0, b = 0;
            switch (h)
            {
                case 0:
                    r = v;g = t;b = p;
                    break;
                case 1:
                    r = q;g = v;b = p;
                    break;
                case 2:
                    r = p;g = v;b = t;
                    break;
                case 3:
                    r = p;g = q;b = v;
                    break;
                case 4:
                    r = t;g = p;b = v;
                    break;
                case 5:
                    r = v;g = p;b = q;
                    break;
                default:
                    break;
            }
            r *= 255;
            g *= 255;
            b *= 255;

            //r = r > 255 ? 255 : r;
            //g = g > 255 ? 255 : g;
            //b = b > 255 ? 255 : b;

            return Color.FromRgb((byte)r, (byte)g, (byte)b);
        }
    }
}
