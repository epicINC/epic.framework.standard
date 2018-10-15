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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Test();
        }

        void Test()
        {

            var url = "http://139.129.208.14/ai/DataFile/00F0B0B/Face/11/000/11000342.jpg";

            return;
            var widthAnimation = new DoubleAnimationUsingKeyFrames();
            var keyFrames = widthAnimation.KeyFrames;

            keyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
            keyFrames.Add(new LinearDoubleKeyFrame(350, TimeSpan.FromSeconds(2)));
            keyFrames.Add(new LinearDoubleKeyFrame(50, TimeSpan.FromSeconds(7)));
            keyFrames.Add(new LinearDoubleKeyFrame(200, TimeSpan.FromSeconds(9)));


        }


    }
}
