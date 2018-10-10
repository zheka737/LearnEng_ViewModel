using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LearnEng_ViewModel.WCFContact_Server;

namespace LearnEng_ViewModel.ViewModel.Commands
{
    public class OpenDictionaryCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainWindowController.RetriveMainWindowController().OpenUserDictionaryPage();
        }

        public event EventHandler CanExecuteChanged;
    }

    public class OpenHomePageCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MainWindowController.RetriveMainWindowController().OpenHomePage();
        }

        public event EventHandler CanExecuteChanged;
    }

    public class OpenUserDictionaryEntryInEditorCommand : ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (!(parameter is UserDictionaryEntryDTO))
            {
                throw new Exception("Parameter must be of type UserDictionaryEntryDTO");
            }

            MainWindowController.RetriveMainWindowController().OpenUserDictionaryEntryInEditor((UserDictionaryEntryDTO)parameter);
        }

        public event EventHandler CanExecuteChanged;
    }


    public class OpenUserDictionaryEntryInEditorEventArgs : EventArgs
    {
        public UserDictionaryEntryDTO UserDictionaryEntryDTO { get; set; }

        public OpenUserDictionaryEntryInEditorEventArgs(UserDictionaryEntryDTO userDictionaryEntryDTO)
        {
            UserDictionaryEntryDTO = userDictionaryEntryDTO;
        }
    }
}
