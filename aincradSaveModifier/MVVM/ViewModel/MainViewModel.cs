﻿using aincradSaveModifier.Core;
using System;
using System.Net.Sockets;

namespace aincradSaveModifier.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand StatsViewCommand { get; set; }
        public RelayCommand InventoryViewCommand { get; set; }


        public HomeViewModel HomeVM { get; set; }
        public StatsViewModel StatsVM { get; set; }
        public InventoryViewModel InventoryVM { get; set; }


        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set 
            { _currentView = value;
                OnPropertyChanged();
            }
        }
        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            StatsVM = new StatsViewModel();
            InventoryVM = new InventoryViewModel();

            CurrentView = HomeVM;

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
