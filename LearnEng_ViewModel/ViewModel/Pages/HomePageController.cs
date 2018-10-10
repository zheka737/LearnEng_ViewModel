using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LearnEng_ViewModel.WCFContact_Server;

namespace LearnEng_ViewModel.ViewModel.Pages
{
    public class HomePageController
    {

        public HomePageController()
        {
           AllUserGroupsOfUser = new ObservableCollection<UserGroupDTO>();
           UpdateAllUserGroupsOfUser();
        }

        public ObservableCollection<UserGroupDTO> AllUserGroupsOfUser { get; set; }

        /// <summary>
        /// Load/Update all user groups that showed in combobox on Home page
        /// </summary>
        public void UpdateAllUserGroupsOfUser()
        {
           var allUserGroupsOfCurrentUser = MainWindowController.RetriveMainWindowController().AllUserGroupsOfCurrentUser();

            foreach (UserGroupDTO userGroupDto in allUserGroupsOfCurrentUser)
            {
                AllUserGroupsOfUser.Add(userGroupDto);
            }
        }

    }

    
}
