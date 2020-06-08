using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BulletinBoard
{
    class PixelButton:Button
    {
        public int Index { get; set; }
        public PixelButton()
        {
            Background = new SolidColorBrush(Colors.Red);
            BorderThickness = new Thickness(0);
        }
    }
}
