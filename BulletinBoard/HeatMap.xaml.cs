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

namespace BulletinBoard
{
    /// <summary>
    /// HeatMap.xaml 的交互逻辑
    /// </summary>
    public partial class HeatMap : UserControl
    {
        int BlockWidth = 8;
        int BlockHeight = 8;
        public HeatMap(int height,int width,double[] data)
        {
            this.Max = data.Max();
            // TODO 参数检查
            InitializeComponent();
            this.main.Width = BlockWidth * width;
            this.main.Height = BlockWidth * height;
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    var n = new Button();
                    n.SetValue(Canvas.LeftProperty,(double)(j * BlockWidth));
                    n.SetValue(Canvas.TopProperty, (double)(i * BlockWidth));
                    n.Width = 10;
                    n.Height = 10;
                    n.BorderThickness = new Thickness(0);
                    n.Background = new SolidColorBrush(GetColor(data[j * width + i]));
                    this.main.Children.Add(n);
                }
            }
        }

        double Max = 0;

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
