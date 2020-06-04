using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace BulletinBoard
{
    class HeatNode:Button
    {
        public HeatNode()
        {
            Width = 5;
            Height = 5;
            Background = new SolidColorBrush(Colors.White);
            var tb = new TextBlock();
            tb.FontFamily = new FontFamily("Segoe MDL2 Assets");
            Content = tb;

        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
