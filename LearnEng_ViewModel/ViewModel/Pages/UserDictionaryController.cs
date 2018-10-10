using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using DataVirtualization;
using LearnEng_ViewModel.ViewModel.DataVirtualizationList;
using LearnEng_ViewModel.WCFContact_Server;

namespace LearnEng_ViewModel.ViewModel.Pages
{
    public class UserDictionaryController:DependencyObject
    {
        public UserDictionaryController()
        {
            ApplicationSettings applicationSettings = ApplicationSettings.FetchApplicationSettings();
            DataVirtualizationListController = new DataVirtualizationListController<UserDictionaryEntryDTO>(new UserDictionaryListItemsProvider(),
                applicationSettings.NumberOfEntrisInPageOfUserDictionaryList, applicationSettings.TimeoutOfPageOfUserDictionaryList);

            DataVirtualizationListController.DataVirtualizationListCommands.Add(new AddUserDictionaryEntryCommand());
            DataVirtualizationListController.DataVirtualizationListCommands.Add(new EditUserDictionaryEntryCommand());


        }



        public DataVirtualizationListController<UserDictionaryEntryDTO> DataVirtualizationListController
        {
            get { return (DataVirtualizationListController < UserDictionaryEntryDTO >) GetValue(DataVirtualizationListControllerProperty); }
            set { SetValue(DataVirtualizationListControllerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataVirtualizationListControllerProperty =
            DependencyProperty.Register("DataVirtualizationListController", typeof(DataVirtualizationListController<UserDictionaryEntryDTO>), typeof(UserDictionaryController));

    }

    public class UserDictionaryListItemsProvider : IItemsProvider<UserDictionaryEntryDTO>
    {
        public int FetchCount()
        {
            return MainWindowController.WCFClient.FetchCountUserDictionary(MainWindowController.RetriveMainWindowController().User.GUID);
        }

        public IList<UserDictionaryEntryDTO> FetchRange(int startIndex, int pageCount, out int overallCount)
        {
            IList<UserDictionaryEntryDTO> result = MainWindowController.WCFClient.FetchRangeUserDictionary(startIndex,
                pageCount, MainWindowController.RetriveMainWindowController().User.GUID);

            overallCount = FetchCount();

            return result;
        }
    }

    #region Commands of User dictionary list

    public class AddUserDictionaryEntryCommand : ICommandForDataVirtualizationList {

        public AddUserDictionaryEntryCommand()
        {
            RepresentationName = "Add word";
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainWindowController.RetriveMainWindowController().OpenUserDictionaryEntryInEditor(((UserDictionaryEntryDTO)parameter));
        }

        public event EventHandler CanExecuteChanged;
        public string RepresentationName { get; set; }
        public BitmapImage RepresentationPicture { get; set; }
    }


    public class EditUserDictionaryEntryCommand : ICommandForDataVirtualizationList
    {
        public EditUserDictionaryEntryCommand()
        {
            RepresentationName = "Edit";
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)  //(DataVirtualization.DataWrapper<UserDictionaryEntryDTO>)  
        {
            MainWindowController.RetriveMainWindowController().OpenUserDictionaryEntryInEditor(((UserDictionaryEntryDTO)parameter));
        }

        public event EventHandler CanExecuteChanged;
        public string RepresentationName { get; set; }
        public BitmapImage RepresentationPicture { get; set; }
    }

    #endregion

}


