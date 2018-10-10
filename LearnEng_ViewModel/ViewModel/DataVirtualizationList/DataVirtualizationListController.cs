using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using LearnEng_ViewModel.WCFContact_Server; 
using DataVirtualization;

namespace LearnEng_ViewModel.ViewModel.DataVirtualizationList
{
    public class DataVirtualizationListController<TElementsType>:DependencyObject where TElementsType: class 
    {
        public DataVirtualizationListController(IItemsProvider<TElementsType> itemsProvider, int numberOfEntrisInPage = 25, int timeoutOfPage = 10000)
        {
            NumberOfEntrisInPage = numberOfEntrisInPage;
            TimeoutOfPage = timeoutOfPage;
            _itemsProvider = itemsProvider;
            DataVirtualizationListCommands = new ObservableCollection<ICommandForDataVirtualizationList>();

            RefreshDataVirtualizationListGrid();

        }

        private IItemsProvider<TElementsType> _itemsProvider = null; 

        public int NumberOfEntrisInPage { get; set; }

        /// <summary>
        /// Time than page that currently not viewed will be preserved before garbage collected. In miliseconds
        /// </summary>
        public int TimeoutOfPage { get; set; }

        public ObservableCollection<ICommandForDataVirtualizationList> DataVirtualizationListCommands
        {
            get { return (ObservableCollection<ICommandForDataVirtualizationList>)GetValue(DataVirtualizationListCommandsProperty); }
            set { SetValue(DataVirtualizationListCommandsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataVirtualizationListCommandsProperty =
            DependencyProperty.Register("DataVirtualizationListCommands", typeof(ObservableCollection<ICommandForDataVirtualizationList>), typeof(DataVirtualizationListController<TElementsType>));




        public AsyncVirtualizingCollection<TElementsType> DataVirtualizationListDataProvider
        {
            get { return (AsyncVirtualizingCollection<TElementsType>)GetValue(DataVirtualizationListDataProviderProperty); }
            set { SetValue(DataVirtualizationListDataProviderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataVirtualizationListDataProvider.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataVirtualizationListDataProviderProperty =
            DependencyProperty.Register("DataVirtualizationListDataProvider", typeof(AsyncVirtualizingCollection<TElementsType>), typeof(DataVirtualizationListController<TElementsType>));





        private AsyncVirtualizingCollection<TElementsType> FetchVirtualizingCollectionForUserDictionaryList()
        {
            var ItemsList = new AsyncVirtualizingCollection<TElementsType>(_itemsProvider, NumberOfEntrisInPage, TimeoutOfPage);
            return ItemsList;
        }

        public void RefreshDataVirtualizationListGrid()
        {
            DataVirtualizationListDataProvider = FetchVirtualizingCollectionForUserDictionaryList();
        }

    }


    public interface ICommandForDataVirtualizationList:ICommand
    {
        string RepresentationName { get; set; }
        BitmapImage RepresentationPicture { get; set; }
    }
}


