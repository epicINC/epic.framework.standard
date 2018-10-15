using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Epic.WPF.Controls
{
    public enum ButtonType
    {
        Default,
        Primary,
        Ghost,
        Dashed,
        Text,
        Info,
        Success,
        Warning,
        Error,
    }

    public enum ButtonShape
    {
        Default,
        Circle
    }

    public enum ButtonSize
    {
        Default,
        Small,
        Large
    }

    public class Button : System.Windows.Controls.Button
    {
        public static readonly DependencyProperty ButtonTypeProperty = DependencyProperty.Register("Type", typeof(ButtonType), typeof(Button), new PropertyMetadata(ButtonType.Default));
        public static readonly DependencyProperty ButtonShapeProperty = DependencyProperty.Register("Shape", typeof(ButtonShape), typeof(Button), new PropertyMetadata(ButtonShape.Default));
        public static readonly DependencyProperty ButtonSizeProperty = DependencyProperty.Register("Size", typeof(ButtonSize), typeof(Button), new PropertyMetadata(ButtonSize.Default));
        public static readonly DependencyProperty ButtonLongProperty = DependencyProperty.Register("Long", typeof(bool), typeof(Button), new PropertyMetadata(false));
        public static readonly DependencyProperty ButtonLoadingProperty = DependencyProperty.Register("Loading", typeof(bool), typeof(Button), new PropertyMetadata(false));
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(Button), new FrameworkPropertyMetadata(default(CornerRadius), FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender));
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(Button), new PropertyMetadata());

        static Button()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Button), new FrameworkPropertyMetadata(typeof(Button)));
        }

        public Button()
        {
            this.DefaultStyleKey = typeof(Button);
            
        }

        

        public ButtonType Type
        {
            get { return (ButtonType)this.GetValue(ButtonTypeProperty); }
            set { this.SetValue(ButtonTypeProperty, value); }
        }

        public ButtonShape Shape
        {
            get { return (ButtonShape)this.GetValue(ButtonShapeProperty); }
            set { this.SetValue(ButtonShapeProperty, value); }
        }

        public ButtonSize Size
        {
            get { return (ButtonSize)this.GetValue(ButtonSizeProperty); }
            set { this.SetValue(ButtonSizeProperty, value); }
        }

        public string Icon
        {
            get { return (string)this.GetValue(IconProperty); }
            set { this.SetValue(IconProperty, value); }
        }

        public bool Long
        {
            get { return (bool)this.GetValue(ButtonLongProperty); }
            set { this.SetValue(ButtonLongProperty, value); }
        }

        public bool Loading
        {
            get { return (bool)this.GetValue(ButtonLoadingProperty); }
            set { this.SetValue(ButtonLoadingProperty, value); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }



    }
}
