using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Epic.WPF.Controls.components
{
    public class Icon : System.Windows.Controls.Control
    {
        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register("type", typeof(string), typeof(Icon), new PropertyMetadata());
        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register("size", typeof(string), typeof(Icon), new PropertyMetadata());
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register("color", typeof(Color), typeof(Icon), new PropertyMetadata());
        public static readonly DependencyProperty CustomProperty = DependencyProperty.Register("custom", typeof(string), typeof(Icon), new PropertyMetadata());


    }
}
