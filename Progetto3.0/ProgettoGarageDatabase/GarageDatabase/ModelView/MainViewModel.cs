using GarageDatabase.Classi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageDatabase.View;

namespace GarageDatabase.ModelView
{ 
    class MainViewModel : ObservableObject
    {
        public HomeView HomeVM { get; set; }

        private object currentView;

        public object CurrentView
        {
            get { return currentView; }
            set { currentView = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            HomeVM = new HomeView();
            CurrentView = HomeVM;
        }
    }
}
