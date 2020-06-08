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
    /// LineChart.xaml 的交互逻辑
    /// </summary>
    public partial class LineChart : UserControl
    {
        int PixelSize = 4;
        double[] Data;
        public LineChart(double[] data,int height=120, int width=240)
        {
            InitializeComponent();
            this.MainCanvas.Width = width;
            this.MainCanvas.Height = height;
            Data = data;

            double max = data.Max();
            double min = data.Min();

            double minmax = max - min;
            if (minmax==0)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    var n = new PixelButton();
                    n.Index = i;
                    n.SetValue(Canvas.LeftProperty, ((double)i / data.Length) * width);
                    n.SetValue(Canvas.TopProperty, (double)height);
                    n.Click += Button_Click;
                    this.MainCanvas.Children.Add(n);
                }
            }
            else
            {
                for (int i = 0; i < data.Length; i++)
                {
                    var n = new PixelButton();
                    n.Index = i;
                    n.SetValue(Canvas.LeftProperty, ((double)i / data.Length) * width);
                    n.SetValue(Canvas.BottomProperty, ((data[i] - min) / minmax) * height);
                    n.Click += Button_Click;
                    this.MainCanvas.Children.Add(n);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var n = sender as PixelButton;
            this.X.Text = $"{n.Index}";
            this.Values.Text = $"{Data[n.Index]}";
        }
    }
}
