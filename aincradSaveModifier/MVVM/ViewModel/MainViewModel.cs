using aincradSaveModifier.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aincradSaveModifier.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand StatsViewCommand { get; set; }
        public RelayCommand InventoryViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public StatsViewModel StatsVM {get; set; }
        public InventoryViewModel InventoryVM { get; set; }

        private object _cuurentView;

        public object CurrentView
        {
            get { return _cuurentView; }
            set 
            { 
                _cuurentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            HomeVM= new HomeViewModel();
            CurrentView = HomeVM;
            StatsVM = new StatsViewModel();
            InventoryVM = new InventoryViewModel();

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });
            StatsViewCommand = new RelayCommand(o =>
            {
                CurrentView = StatsVM;
            });
            InventoryViewCommand = new RelayCommand(o =>
            {
                CurrentView = InventoryVM;
            });
        }
    }
}
