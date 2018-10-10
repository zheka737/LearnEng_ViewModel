using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using LearnEng_ViewModel.ViewModel.Commands;
using LearnEng_ViewModel.WCFContact_Server;
using UserGroupDTO = LearnEng_ViewModel.WCFContact_Server.UserGroupDTO;


namespace LearnEng_ViewModel.ViewModel
{
    public class MainWindowController:DependencyObject
    {
        #region Fields, constructors, properties and dependency properties

        private bool _instanceIsAlreadyCreated = false;
        public static WCFContact_ServerClient WCFClient;
        public static Object lockObject = new Object();
        private UserDTO _user = null;
        public event EventHandler OpenHomePageEvent;
        public event EventHandler OpenUserDictionaryPageEvent;
        public event EventHandler OpenUserDictionaryEntryInEditorEvent;
        public static event EventHandler ShowMessageToUserEvent;
        private static MainWindowController _mainWindowController = null;

        /// <summary>
        /// Set background image of application
        /// </summary>
        public BitmapSource BackgroundImage
        {
            get { return (BitmapSource)GetValue(BackgroundImageProperty); }
            set { SetValue(BackgroundImageProperty, value); }
        }

        /// <summary>
        /// Set background image of application
        /// </summary>
        public static readonly DependencyProperty BackgroundImageProperty =
            DependencyProperty.Register("BackgroundImage", typeof(BitmapSource), typeof(MainWindowController));

        public NavigationUIVisibility AllowUseNavigationPanel
        {
            get { return (NavigationUIVisibility)GetValue(AllowUseNavigationPanelProperty); }
            set { SetValue(AllowUseNavigationPanelProperty, value); }
        }

        /// <summary>
        /// Turns on and off navigation panel in application
        /// </summary>
        public static readonly DependencyProperty AllowUseNavigationPanelProperty =
            DependencyProperty.Register("AllowUseNavigationPanel", typeof(NavigationUIVisibility), typeof(MainWindowController));

        public HeaderFrameController HeaderFrameController
        {
            get { return (HeaderFrameController)GetValue(HeaderFrameControllerProperty); }
            set { SetValue(HeaderFrameControllerProperty, value); }
        }

        /// <summary>
        /// Contains Header Frame Controller of application
        /// </summary>
        public static readonly DependencyProperty HeaderFrameControllerProperty =
            DependencyProperty.Register("HeaderFrameController", typeof(HeaderFrameController), typeof(MainWindowController), new PropertyMetadata(default(HeaderFrameController)));

        /// <summary>
        /// Contains current user
        /// </summary>
        public UserDTO User
        {
            get { return _user; }
            set
            {
                _user = value;
            }
        }

        /// <summary>
        /// Contains all user groups
        /// </summary>
        public List<UserGroupDTO> AllUserGroupsOfCurrentUser()
        {
            WCFContact_ServerClient client = new WCFContact_ServerClient();
            UserGroupDTO[] r2 = client.AllUserGroupsOfUser(User);
            return r2.ToList();
        }

        public MainWindowController()
        {
            WCFClient = new WCFContact_ServerClient();
            User = WCFClient.FindUserByName("Zhenya");
            AllowUseNavigationPanel = NavigationUIVisibility.Visible;

            if (_instanceIsAlreadyCreated)
            {
                throw new Exception("This is Singleton. Instance of this class is already created.");
            }
            else
            {
                _instanceIsAlreadyCreated = true;
            }


            OpenHomePage();

            BackgroundImage = ViewModelHelper.RetriveImageFromResourses(@"BackgroundImage.jpg");
        }

        #endregion

        #region Control of user interface
        public void OpenHomePage()
        {
            OpenHomePageEvent?.Invoke(this, new EventArgs());
        }


        /// <summary>
        /// Opens User dictionary page, where we can browse and manage all UserDictionaryEntries
        /// </summary>
        public void OpenUserDictionaryPage()
        {
            OpenUserDictionaryPageEvent?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Open window that allows edit and save UserDictionaryEntry
        /// </summary>
        /// <param name="dictionaryEntryDto"></param>
        public void OpenUserDictionaryEntryInEditor(UserDictionaryEntryDTO dictionaryEntryDto)
        {
            OpenUserDictionaryEntryInEditorEvent?.Invoke(this, new OpenUserDictionaryEntryInEditorEventArgs(dictionaryEntryDto));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"> Message showed to user</param>
        /// <param name="duration">Duration of showing the message in ms. Defaut 5000ms.</param>
        public static void ShowMessageToUser(string message, int duration = 5000)
        {
            ShowMessageToUserEvent(null, new ShowMessageToUserEventArgs(message, duration));
        }


        #endregion

        #region Service methods, classes

        /// <summary>
        /// For showing message to user
        /// </summary>
        public class ShowMessageToUserEventArgs : EventArgs
        {
            public ShowMessageToUserEventArgs(string message, int duration)
            {
                Message = message;
                Duration = duration;
            }

            public string Message { get; set; }
            public int Duration { get; set; }
        }

        /// <summary>
        /// Returns MainWindowController. There can be only one instance, because this controller
        /// responsipe for overall function of application
        /// </summary>
        /// <returns></returns>
        public static MainWindowController RetriveMainWindowController()
        {
            if (_mainWindowController == null)
                _mainWindowController = new MainWindowController();

            return _mainWindowController;
        }

        #endregion

    }
}
