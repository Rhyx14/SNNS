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
    /// LineChart2.xaml 的交互逻辑
    /// </summary>
    public partial class LineChart2 : UserControl
    {
        int X_Ub { get; set; }
        int X_Lb { get; set; }
        int Y_Ub { get; set; }
        int Y_Lb { get; set; }

        public LineChart2(int X_Lower,int X_Upper,int Y_Lower,int Y_Upper, int height = 120, int width = 240)
        {
            InitializeComponent();

            this.MainCanvas.Width = width;
            this.MainCanvas.Height = height;

            this.X_Ub = X_Upper;
            this.X_Lb = X_Lower;
            this.Y_Ub = Y_Upper;
            this.Y_Lb = Y_Lower;
        }

        public void Draw(double[] data, Color color)
        {
            var brush = new SolidColorBrush(color);
            var width = this.MainCanvas.Width;
            var x_length = X_Ub - X_Lb;
            var y_length = Y_Ub - Y_Lb;

            for (int i = this.X_Lb; i < this.X_Ub; i++)
            {
                //超过范围了就不绘制
                if (i>data.Length)
                {
                    return;
                }
                if (data[i]<Y_Lb || data[i]>Y_Ub)
                {
                    continue;
                }
                var n = new Pixel(3, brush);
                n.SetValue(Canvas.LeftProperty, ((double)(i-X_Lb) / x_length) * width);
                n.SetValue(Canvas.BottomProperty, ((double)(data[i] - Y_Lb) / y_length) * width);
            }
        }

    }
}
