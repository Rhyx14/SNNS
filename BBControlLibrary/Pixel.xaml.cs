using System;
using System.Collections.Generic;
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
    /// Pixel.xaml 的交互逻辑
    /// </summary>
    public partial class Pixel : UserControl
    {
        public int Index { get; set; }
        public Pixel(int size, int index, MouseEventHandler mouseEnter,SolidColorBrush brush)
        {
            InitializeComponent();
            this.Index = index;
            this.Width = size;
            this.Height = size;
            this.MouseEnter += mouseEnter;
            this.Background = brush;
        }
    }
}
