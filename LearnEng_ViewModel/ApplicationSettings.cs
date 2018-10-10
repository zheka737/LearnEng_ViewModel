using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnEng_ViewModel
{
    public class ApplicationSettings
    {
        private static ApplicationSettings _applicationSettingsinstance = null;
        private ApplicationSettings()
        {
            if (_applicationSettingsinstance != null)
            {
                throw new Exception("This is a Singleton. And instance is already created.");
            }

            NumberOfEntrisInPageOfUserDictionaryList = 30;
            TimeoutOfPageOfUserDictionaryList = 10000;
        }

        public static ApplicationSettings FetchApplicationSettings()
        {
            return _applicationSettingsinstance ?? (_applicationSettingsinstance = new ApplicationSettings());
        }

        public int NumberOfEntrisInPageOfUserDictionaryList { get;}
        public int TimeoutOfPageOfUserDictionaryList { get; } //msc
    }
}
