using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnEng_ViewModel.WCFContact_Server;

namespace LearnEng_ViewModel.ViewModel.Pages
{
    public class UserDictionaryEntryEditorController
    {
        //Store UserDictionaryEntryDTO attached to current UserDictionaryEntryEditorController
        private UserDictionaryEntryDTO _userDictionaryEntryDTO = null;
        //Store finded IntegratedVacabularyEntryDTO for current word value in UserDictionaryEntryDTO
        private IntegratedVacabularyEntryDTO _integratedVacabularyEntry = null;
        //Thrown out when IntegratedVacabularyEntryDTO is changed. Value of new IntegratedVacabularyEntryDTO can be taken from
        public event EventHandler IntegratedVacabularyEntryDtoIsChanged;

        /// <summary>
        /// Returns UserDictionaryEntryDTO attached to current UserDictionaryEntryEditorController
        /// </summary>
        public UserDictionaryEntryDTO UserDictionaryEntryDTO
        {
            get { return _userDictionaryEntryDTO; }
            private set
            {
                _userDictionaryEntryDTO = value;
                FindAndUpdateCurrentIntegratedVacabularyEntryDtoByWord(_userDictionaryEntryDTO.WordOfDictionary.Word, _userDictionaryEntryDTO.WordOfDictionary.LanguageOfWord, 
                        _userDictionaryEntryDTO.WordOfDictionary.LanguageOfTranslation);
            }
        }

        /// <summary>
        /// Store finded IntegratedVacabularyEntryDTO for current word value in UserDictionaryEntryDTO
        /// </summary>
        public IntegratedVacabularyEntryDTO IntegratedVacabularyEntry
        {
            get { return _integratedVacabularyEntry; }
        }


        //Constructor
        public UserDictionaryEntryEditorController(UserDictionaryEntryDTO userDictionaryEntryDto)
        {
            UserDictionaryEntryDTO = userDictionaryEntryDto;
        }

        /// <summary>
        /// Update/Save current attached to controller UserDictionaryEntryDTO in database
        /// </summary>
        public void UpdateSaveEntityInDatabase()
        {
            MainWindowController.WCFClient.UpdateSaveEntityFromDTO(this._userDictionaryEntryDTO);
            MainWindowController.ShowMessageToUser("Entry have been updated.");
        }

        /// <summary>
        /// Finds IntegratedVacabularyEntryDto by provided word string
        /// </summary>
        /// <param name="word"></param>
        public void FindAndUpdateCurrentIntegratedVacabularyEntryDtoByWord(string word, Languages wordLanguage, Languages translationLanguage)
        {
            _integratedVacabularyEntry =
                MainWindowController.WCFClient.FindIntegratedVacabularyEntryForWord(word, wordLanguage,
                    translationLanguage);
            if (IntegratedVacabularyEntryDtoIsChanged != null) IntegratedVacabularyEntryDtoIsChanged(null, null);
        }

    }
}
