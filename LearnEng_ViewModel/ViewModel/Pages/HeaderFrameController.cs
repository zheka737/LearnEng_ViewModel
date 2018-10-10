using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LearnEng_ViewModel.ViewModel
{
    public class HeaderFrameController:DependencyObject
    {

        public HeaderFrameController()
        {
            var bi = ViewModelHelper.RetriveImageFromResourses("Logo.png");
            Logo = bi;
        }




        public BitmapSource Logo
        {
            get { return (BitmapSource)GetValue(LogoProperty); }
            set { SetValue(LogoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Logo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LogoProperty =
            DependencyProperty.Register("Logo", typeof(BitmapSource), typeof(HeaderFrameController), new PropertyMetadata(default(BitmapSource)));




    }
}
