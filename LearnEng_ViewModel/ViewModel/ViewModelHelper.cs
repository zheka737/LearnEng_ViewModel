using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LearnEng_ViewModel.ViewModel
{
    public static class ViewModelHelper
    {


        public static BitmapImage RetriveImageFromResourses(string nameOfImage)
        {

            Assembly assembly = Assembly.GetCallingAssembly();

            BitmapImage bi = new BitmapImage();
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.
            bi.BeginInit();
            bi.UriSource = new Uri(@"pack://application:,,,/" + assembly.GetName().Name + ";component/" + nameOfImage, UriKind.Absolute);
            bi.EndInit();
            return bi;
        }
    }
}
