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
using BBControlLibrary;

namespace BulletinBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            double[] data = new double[48*48];
            for (int i = 0; i < 48; i++)
            {
                for (int j = 0; j < 48; j++)
                {
                    data[48 * i + j] = j + i;
                }
            }

            this.mainCanvas.Children.Add(
                new HeatMap(48, 48, data)
                );

            //for (int i = 0; i < 48 * 48; i++)
            //{
            //    data[i] = Math.Sin(i * 0.01);
            //}
            //this.mainCanvas.Children.Add(new LineChart(data, 200, 400));

        }
    }
}
